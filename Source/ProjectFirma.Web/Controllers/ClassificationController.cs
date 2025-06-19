﻿/*-----------------------------------------------------------------------
<copyright file="ClassificationController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Classification;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.SortOrder;
using DetailViewData = ProjectFirma.Web.Views.Classification.DetailViewData;
using Detail = ProjectFirma.Web.Views.Classification.Detail;
using Index = ProjectFirma.Web.Views.Classification.Index;
using IndexGridSpec = ProjectFirma.Web.Views.Classification.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.Classification.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class ClassificationController : FirmaBaseController
    {
        [ClassificationManageFeature]
        public ViewResult Index(ClassificationSystemPrimaryKey classificationSystemPrimaryKey)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            var viewData = new IndexViewData(CurrentPerson, classificationSystem);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [ClassificationManageFeature]
        public GridJsonNetJObjectResult<Classification> IndexGridJsonData(ClassificationSystemPrimaryKey classificationSystemPrimaryKey)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            var gridSpec = new IndexGridSpec(new ClassificationManageFeature().HasPermissionByPerson(CurrentPerson), classificationSystem);            
            var classifications = classificationSystem.Classifications.SortByOrderThenName().ToList();
            return new GridJsonNetJObjectResult<Classification>(classifications, gridSpec);
        }

        [HttpGet]
        [ClassificationManageFeature]
        public PartialViewResult New(ClassificationSystemPrimaryKey classificationSystemPrimaryKey)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel, classificationSystem);
        }

        [HttpPost]
        [ClassificationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(ClassificationSystemPrimaryKey classificationSystemPrimaryKey, EditViewModel viewModel)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, classificationSystem);
            }
            
            var classification = new Classification(string.Empty, "#BBBBBB", viewModel.DisplayName, classificationSystem.ClassificationSystemID);
            viewModel.UpdateModel(classification, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.Classifications.Add(classification);

            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay(
                $"New {classificationSystem.ClassificationSystemName} {classification.GetDisplayNameAsUrl()} successfully created!");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ClassificationManageFeature]
        public PartialViewResult Edit(ClassificationPrimaryKey classificationPrimaryKey)
        {
            var classification = classificationPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(classification);
            return ViewEdit(viewModel, classification.ClassificationSystem);
        }

        [HttpPost]
        [ClassificationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ClassificationPrimaryKey classificationPrimaryKey, EditViewModel viewModel)
        {
            var classification = classificationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, classification.ClassificationSystem);
            }
            
            viewModel.UpdateModel(classification, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, ClassificationSystem classificationSystem)
        {            
            var viewData = new EditViewData(classificationSystem);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ClassificationManageFeature]
        public PartialViewResult DeleteClassification(ClassificationPrimaryKey classificationPrimaryKey)
        {
            var classification = classificationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(classification.ClassificationID);
            return ViewDeleteClassification(classification, viewModel);
        }

        private PartialViewResult ViewDeleteClassification(Classification classification, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !classification.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {classification.ClassificationSystem.ClassificationSystemName} '{classification.DisplayName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage(classification.ClassificationSystem.ClassificationSystemName, SitkaRoute<ClassificationController>.BuildLinkFromExpression(x => x.Detail(classification), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ClassificationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteClassification(ClassificationPrimaryKey classificationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var classification = classificationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteClassification(classification, viewModel);
            }
            classification.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [ClassificationViewFeature]
        public ViewResult Detail(ClassificationPrimaryKey classificationPrimaryKey)
        {
            var classification = classificationPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, classification);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [ClassificationViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData(ClassificationPrimaryKey classificationPrimaryKey)
        {
            var gridSpec = new ProjectThemeProjectListGridSpec(classificationPrimaryKey);
            var projectClassifications = classificationPrimaryKey.EntityObject.GetAssociatedProjects(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectClassifications, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ClassificationManageFeature]
        public PartialViewResult EditSortOrder(ClassificationSystemPrimaryKey classificationSystemPrimaryKey)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            EditSortOrderViewModel viewModel = new EditSortOrderViewModel();
            return ViewEditSortOrder(classificationSystem, viewModel);
        }

        private PartialViewResult ViewEditSortOrder(ClassificationSystem classificationSystem, EditSortOrderViewModel viewModel)
        {
            EditSortOrderViewData viewData = new EditSortOrderViewData(new List<IHaveASortOrder>(classificationSystem.Classifications), classificationSystem.ClassificationSystemNamePluralized);
            return RazorPartialView<EditSortOrder, EditSortOrderViewData, EditSortOrderViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ClassificationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditSortOrder(ClassificationSystemPrimaryKey classificationSystemPrimaryKey, EditSortOrderViewModel viewModel)
        {
            
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditSortOrder(classificationSystem, viewModel);
            }

            viewModel.UpdateModel(new List<IHaveASortOrder>(classificationSystem.Classifications));
            SetMessageForDisplay("Successfully Updated Classification Sort Order");
            return new ModalDialogFormJsonResult();
        }
    }
}
