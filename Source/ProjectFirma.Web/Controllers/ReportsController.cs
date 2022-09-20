/*-----------------------------------------------------------------------
<copyright file="ReportsController.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
            var firmaPage = FirmaPageTypeEnum.ReportProjects.GetFirmaPage();
            var projectCustomReportsGridConfigurations = HttpRequestStorage.DatabaseEntities.ProjectCustomGridConfigurations.Where(x => x.IsEnabled && x.ProjectCustomGridTypeID == ProjectCustomGridType.Reports.ProjectCustomGridTypeID).OrderBy(x => x.SortOrder).ToList();
            var viewData = new ProjectsViewData(CurrentPerson, firmaPage, projectCustomReportsGridConfigurations);
            return RazorView<Projects, ProjectsViewData>(viewData);
        }


        [CrossAreaRoute]
        [HttpGet]
        [FirmaAdminFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPageTypeEnum.Reports.GetFirmaPage();
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
            var firmaPage = FirmaPageTypeEnum.ReportAddReport.GetFirmaPage();
            var viewData = new EditViewData(CurrentPerson, firmaPage);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, ReportTemplate reportTemplate)
        {
            var firmaPage = FirmaPageTypeEnum.ReportAddReport.GetFirmaPage();
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

            var canDelete = new ReportTemplateManageFeature().HasPermissionByPerson(CurrentPerson);

            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the \"{reportTemplate.DisplayName}\" Report Template?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"Report Template");

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
            var reportTemplatePrimaryKey = (ReportTemplatePrimaryKey) viewModel.ReportTemplateID;
            var reportTemplate = reportTemplatePrimaryKey.EntityObject;
            var selectedModelIDs = viewModel.ProjectIDList;
            var reportTemplateGenerator = new ReportTemplateGenerator(reportTemplate, selectedModelIDs);
            return reportTemplateGenerator.GenerateAndDownload();
        }
    }
}