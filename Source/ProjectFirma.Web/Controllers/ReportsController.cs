/*-----------------------------------------------------------------------
<copyright file="ReportsController.cs" company="Environmental Science Associates">
Copyright (c) Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using LtInfo.Common;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.ReportTemplates;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Reports;
using ProjectFirma.Web.Views.Shared;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ProjectFirma.Web.Controllers
{
    public class ReportsController : FirmaBaseController
    {

        [CrossAreaRoute]
        [HttpGet]
        [ReportTemplateGenerateReportsFeature]
        public ViewResult Projects()
        {
            var firmaPage = FirmaPageType.ReportProjects.GetFirmaPage();
            var viewData = new ProjectsViewData(CurrentPerson, firmaPage);
            return RazorView<Projects, ProjectsViewData>(viewData);
        }


        [CrossAreaRoute]
        [HttpGet]
        [FirmaAdminFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPageType.Reports.GetFirmaPage();
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel
            {
                ReportTemplateModelTypeID = (int) ReportTemplateModelTypeEnum.MultipleModels
            };
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var fileResourceInfo = FileResourceModelExtensions.CreateNewFromHttpPostedFileAndSave(viewModel.FileResourceData, CurrentPerson);
            var reportTemplateModelType = ReportTemplateModelType.All.FirstOrDefault(x => x.ReportTemplateModelTypeID == viewModel.ReportTemplateModelTypeID);
            var reportTemplateModel = ReportTemplateModel.All.FirstOrDefault(x => x.ReportTemplateModelID == viewModel.ReportTemplateModelID);
            var reportTemplate = ReportTemplate.CreateNewBlank(fileResourceInfo, reportTemplateModelType, reportTemplateModel);
            reportTemplate.IsSystemTemplate = false;//All report templates generated through the UI are not system templates. System templates are created through the database. 

            ReportTemplateGenerator.ValidateReportTemplate(reportTemplate, out var reportIsValid, out var errorMessage, out var sourceCode);

            if (reportIsValid)
            {
                viewModel.UpdateModel(reportTemplate, fileResourceInfo, CurrentPerson, HttpRequestStorage.DatabaseEntities);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                SetMessageForDisplay($"Report Template \"{reportTemplate.DisplayName}\" successfully created.");
            }
            else
            {
                SetErrorForDisplay($"There was an error with this template: {errorMessage}");
            }

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult Edit(ReportTemplatePrimaryKey reportTemplatePrimaryKey)
        {
            var reportTemplate = reportTemplatePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(reportTemplate);
            return ViewEdit(viewModel, reportTemplate);
        }

        [HttpPost]
        [FirmaAdminFeature]
        public ActionResult Edit(ReportTemplatePrimaryKey reportTemplatePrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var fileResource = (viewModel.FileResourceData != null) ? FileResourceModelExtensions.CreateNewFromHttpPostedFileAndSave(viewModel.FileResourceData, CurrentPerson) : HttpRequestStorage.DatabaseEntities.FileResources.First(x => x.FileResourceID == viewModel.FileResourceID);
            var reportTemplate = reportTemplatePrimaryKey.EntityObject;
            reportTemplate.FileResource = fileResource;

            ReportTemplateGenerator.ValidateReportTemplate(reportTemplate, out var reportIsValid, out var errorMessage, out var sourceCode);

            if (reportIsValid)
            {
                viewModel.UpdateModel(reportTemplate, fileResource, CurrentPerson, HttpRequestStorage.DatabaseEntities);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                SetMessageForDisplay($"Report Template \"{reportTemplate.DisplayName}\" successfully created.");
            }
            else
            {
                SetErrorForDisplay($"There was an error with this template: {errorMessage}");
            }

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var firmaPage = FirmaPageType.ReportAddReport.GetFirmaPage();
            var viewData = new EditViewData(CurrentPerson, firmaPage);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, ReportTemplate reportTemplate)
        {
            var firmaPage = FirmaPageType.ReportAddReport.GetFirmaPage();
            var viewData = new EditViewData(CurrentPerson, firmaPage, reportTemplate);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult Delete(ReportTemplatePrimaryKey reportTemplatePrimaryKey)
        {
            var reportTemplate = reportTemplatePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(reportTemplate.ReportTemplateID);
            return ViewDelete(reportTemplate, viewModel);
        }

        private PartialViewResult ViewDelete(ReportTemplate reportTemplate, ConfirmDialogFormViewModel viewModel)
        {

            var canDelete = new ReportTemplateManageFeature().HasPermissionByPerson(CurrentPerson) && !reportTemplate.IsSystemTemplate;

            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the \"{reportTemplate.DisplayName}\" Report Template?"
                : reportTemplate.IsSystemTemplate ? "You can't delete this Report Template because it is a System Template" : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"Report Template");

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(ReportTemplatePrimaryKey reportTemplatePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var reportTemplate = reportTemplatePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(reportTemplate, viewModel);
            }
            reportTemplate.DeleteFullWithFileResource(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }


        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<ReportTemplate> IndexGridJsonData()
        {
            var hasManagePermissions = new ReportTemplateManageFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new ReportTemplateGridSpec(hasManagePermissions);
            var reportTemplates = HttpRequestStorage.DatabaseEntities.ReportTemplates.OrderBy(x => x.DisplayName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ReportTemplate>(reportTemplates, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [ReportTemplateGenerateReportsFeature]
        public PartialViewResult SelectReportToGenerateFromSelectedProjects()
        {
            return new PartialViewResult();
        }

        [HttpPost]
        [ReportTemplateGenerateReportsFeature]
        public PartialViewResult SelectReportToGenerateFromSelectedProjects(GenerateReportsViewModel viewModel)
        {
            // Get the list of projects and then order them by the order they were received from the post request
            var projectsList = HttpRequestStorage.DatabaseEntities.Projects.Where(x => viewModel.ProjectIDList.Contains(x.ProjectID)).ToList();
            projectsList = projectsList.OrderBy(p => viewModel.ProjectIDList.IndexOf(p.ProjectID)).ToList();
            var reportTemplateSelectListItems =
                HttpRequestStorage.DatabaseEntities.ReportTemplates.ToList().Where(x => x.ReportTemplateModel.ReportTemplateModelID == ReportTemplateModel.Project.PrimaryKey).OrderBy(x => x.DisplayName).ToSelectList(x => x.ReportTemplateID.ToString(),
                    x => $"{x.DisplayName} - {x.Description}");
            var viewData = new GenerateReportsViewData(CurrentPerson, projectsList, reportTemplateSelectListItems);
            return RazorPartialView<GenerateReports, GenerateReportsViewData, GenerateReportsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ReportTemplateGenerateReportsFeature]
        public ActionResult GenerateReportsFromSelectedProjects()
        {
            return new PartialViewResult();
        }

        [HttpPost]
        [ReportTemplateGenerateReportsFeature]
        public ActionResult GenerateReportsFromSelectedProjects(GenerateReportsViewModel viewModel)
        {
            var reportTemplatePrimaryKey = (ReportTemplatePrimaryKey)viewModel.ReportTemplateID;
            var reportTemplate = reportTemplatePrimaryKey.EntityObject;
            var selectedModelIDs = viewModel.ProjectIDList;
            var reportTemplateGenerator = new ReportTemplateGenerator(reportTemplate, selectedModelIDs);
            return reportTemplateGenerator.GenerateAndDownload();
        }

        [ProjectsViewFullListFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData()
        {
            var treatmentTotals = HttpRequestStorage.DatabaseEntities.vTotalTreatedAcresByProjects.ToList();
            var treatmentDictionary = treatmentTotals.ToDictionary(x => x.ProjectID, y => y);
            var programProjectDictionary = HttpRequestStorage.DatabaseEntities.ProjectPrograms.Include(x => x.Program).ToList()
                .GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, x => x.ToList().Select(y => y.Program).ToList());
            var gridSpec = new ProjectListGridSpec(CurrentPerson, treatmentDictionary, programProjectDictionary);
            var allProjectsVisibleToUser = GetListOfActiveProjectsVisibleToUser(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(allProjectsVisibleToUser, gridSpec);
            return gridJsonNetJObjectResult;
        }

        public List<Project> GetListOfActiveProjectsVisibleToUser(Person currentPerson)
        {
            var viewProjectFeature = new ProjectViewFeature();
            var allActiveProjectsWithIncludes = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectGrantAllocationExpenditures).Include(x => x.ProjectImages)
                .Include(x => x.ProjectRegions).Include(x => x.ProjectPriorityLandscapes)
                .Include(x => x.ProjectOrganizations).ToList().GetActiveProjectsVisibleToUser(currentPerson);

            return allActiveProjectsWithIncludes;
        }


        [ProjectEditAsAdminFeature]
        public ActionResult DownloadLandOwnerAssistanceApprovalLetter(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var reportTemplate = ReportTemplate.GetApprovalLetterTemplate();
            var selectedModelIDs = new List<int> { project.PrimaryKey };
            var reportTemplateGenerator = new ReportTemplateGenerator(reportTemplate, selectedModelIDs);
            return reportTemplateGenerator.GenerateAndDownload($"{project.DisplayName} - Financial Assistance Approval Letter");
        }

        [ProjectEditAsAdminFeature]
        public ActionResult DownloadInvoicePaymentRequest(ProjectPrimaryKey projectPrimaryKey, InvoicePaymentRequestPrimaryKey invoicePaymentRequestPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var invoicePaymentRequest = invoicePaymentRequestPrimaryKey.EntityObject;
            var reportTemplate = ReportTemplate.GetInvoicePaymentRequestTemplate();
            var selectedModelIDs = new List<int> { invoicePaymentRequest.PrimaryKey };
            var reportTemplateGenerator = new ReportTemplateGenerator(reportTemplate, selectedModelIDs);
            return reportTemplateGenerator.GenerateAndDownload($"{project.DisplayName} - Invoice Payment Request {invoicePaymentRequest.InvoicePaymentRequestDate:MM-dd-yyyy}");
        }
    }
}