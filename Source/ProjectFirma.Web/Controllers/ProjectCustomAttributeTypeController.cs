﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.ProjectCustomAttributeType;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectCustomAttributeTypeController : FirmaBaseController
    {
        [FirmaAdminFeature]
        public ViewResult Manage()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ManageProjectCustomAttributeTypesList);
            var viewData = new ManageViewData(CurrentPerson, firmaPage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<ProjectCustomAttributeType> ProjectCustomAttributeTypeGridJsonData()
        {
            var gridSpec = new ProjectCustomAttributeTypeGridSpec();
            var projectCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes.ToList().OrderBy(x => x.ProjectCustomAttributeTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectCustomAttributeType>(projectCustomAttributeTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ContentResult Description(ProjectCustomAttributeTypePrimaryKey projectCustomAttributeTypePrimaryKey)
        {
            return Content(projectCustomAttributeTypePrimaryKey.EntityObject.ProjectCustomAttributeTypeDescription);
        }

        private List<ProjectTypeJson> GetProjectTypeJsonList()
        {
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList();
            return projectTypes.Select(x => new ProjectTypeJson(x)).OrderBy(x => x.ProjectTypeName).ToList();
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel(GetProjectTypeJsonList());
            return ViewEdit(viewModel, null);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, null);
            }

            var projectCustomAttributeType = new ProjectCustomAttributeType(String.Empty, ProjectCustomAttributeDataType.String, false, false);
            viewModel.UpdateModel(projectCustomAttributeType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes.Add(projectCustomAttributeType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"{FieldDefinition.ProjectCustomAttribute.GetFieldDefinitionLabel()} {projectCustomAttributeType.ProjectCustomAttributeTypeName} succesfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult Edit(ProjectCustomAttributeTypePrimaryKey projectCustomAttributeTypePrimaryKey)
        {
            var projectCustomAttributeType = projectCustomAttributeTypePrimaryKey.EntityObject;
            var projectTypesSelected = projectCustomAttributeType.ProjectTypeProjectCustomAttributeTypes
                .Select(x => x.ProjectType).Select(x => new ProjectTypeJson(x.ProjectTypeName, x.ProjectTypeID, true))
                .ToList();
            var projectTypesUnselected = GetProjectTypeJsonList().Where(x => !projectTypesSelected.Select(y => y.ProjectTypeID).Contains(x.ProjectTypeID));
            projectTypesSelected.AddRange(projectTypesUnselected);
            var projectTypeJsons = projectTypesSelected.OrderBy(x => x.ProjectTypeName).ToList();
            var viewModel = new EditViewModel(projectCustomAttributeType, projectTypeJsons);
            return ViewEdit(viewModel, projectCustomAttributeType);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ProjectCustomAttributeTypePrimaryKey projectCustomAttributeTypePrimaryKey, EditViewModel viewModel)
        {
            var projectCustomAttributeType = projectCustomAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, projectCustomAttributeType);
            }
            viewModel.UpdateModel(projectCustomAttributeType, CurrentPerson);

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, ProjectCustomAttributeType projectCustomAttributeType)
        {
            var instructionsFirmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ManageProjectCustomAttributeTypeInstructions);
            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.ProjectCustomAttributeTypeID)
                ? SitkaRoute<ProjectCustomAttributeTypeController>.BuildUrlFromExpression(x => x.Edit(viewModel.ProjectCustomAttributeTypeID))
                : SitkaRoute<ProjectCustomAttributeTypeController>.BuildUrlFromExpression(x => x.New());
            var viewData = new EditViewData(CurrentPerson, MeasurementUnitType.All, ProjectCustomAttributeDataType.All,
                submitUrl, instructionsFirmaPage, projectCustomAttributeType);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [FirmaAdminFeature]
        public ViewResult Detail(ProjectCustomAttributeTypePrimaryKey projectCustomAttributeTypePrimaryKey)
        {
            var projectCustomAttributeType = projectCustomAttributeTypePrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, projectCustomAttributeType);
            return RazorView<Detail, DetailViewData>(viewData);
        }


        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult DeleteProjectCustomAttributeType(ProjectCustomAttributeTypePrimaryKey projectCustomAttributeTypePrimaryKey)
        {
            var projectCustomAttributeType = projectCustomAttributeTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectCustomAttributeType.ProjectCustomAttributeTypeID);
            return ViewDeleteProjectCustomAttributeType(projectCustomAttributeType, viewModel);
        }

        private PartialViewResult ViewDeleteProjectCustomAttributeType(ProjectCustomAttributeType projectCustomAttributeType, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to delete {FieldDefinition.ProjectCustomAttribute.GetFieldDefinitionLabel()} \"{projectCustomAttributeType.ProjectCustomAttributeTypeName}\"?", true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProjectCustomAttributeType(ProjectCustomAttributeTypePrimaryKey projectCustomAttributeTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var projectCustomAttributeType = projectCustomAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteProjectCustomAttributeType(projectCustomAttributeType, viewModel);
            }

            var message = $"{FieldDefinition.ProjectCustomAttribute.GetFieldDefinitionLabel()} '{projectCustomAttributeType.ProjectCustomAttributeTypeName}' successfully deleted!";
            projectCustomAttributeType.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
    }
}
