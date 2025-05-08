/*-----------------------------------------------------------------------
<copyright file="ProjectTypeController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Shared;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Shared.SortOrder;
using DetailViewData = ProjectFirma.Web.Views.ProjectType.DetailViewData;
using Edit = ProjectFirma.Web.Views.ProjectType.Edit;
using EditViewData = ProjectFirma.Web.Views.ProjectType.EditViewData;
using EditViewModel = ProjectFirma.Web.Views.ProjectType.EditViewModel;
using Index = ProjectFirma.Web.Views.ProjectType.Index;
using IndexGridSpec = ProjectFirma.Web.Views.ProjectType.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.ProjectType.IndexViewData;
using Summary = ProjectFirma.Web.Views.ProjectType.Detail;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectTypeController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            return IndexImpl();
        }

        [FirmaAdminFeature]
        public ViewResult Manage()
        {
            return IndexImpl();
        }

        private ViewResult IndexImpl()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ProjectTypeList);
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public GridJsonNetJObjectResult<ProjectType> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList().OrderTaxonomyLeaves().ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectType>(projectTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult Detail(ProjectTypePrimaryKey projectTypePrimaryKey)
        {
            var projectType = projectTypePrimaryKey.EntityObject;
            var currentPersonCanViewProposals = CurrentPerson.CanViewProposals;

            var projectTypeProjects = projectType.Projects.ToList().GetActiveProjectsAndProposalsVisibleToUser(CurrentPerson).Where(x => x.ProjectStage.ShouldShowOnMap()).ToList();

            var projectMapCustomization = new ProjectMapCustomization(ProjectLocationFilterType.ProjectType,
                new List<int> {projectType.ProjectTypeID}, ProjectColorByType.ProjectStage);
            var projectLocationsLayerGeoJson =
                new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()}",
                    Project.MappedPointsToGeoJsonFeatureCollection(projectTypeProjects, true, false), "red", 1,
                    LayerInitialVisibility.Show);
            var projectLocationsMapInitJson = new ProjectLocationsMapInitJson(projectLocationsLayerGeoJson,
                projectMapCustomization, "ProjectTypeProjectMap");

            var projectLocationsMapViewData = new ProjectLocationsMapViewData(projectLocationsMapInitJson.MapDivID,
                                                                              ProjectColorByType.ProjectStage.DisplayName, 
                                                                              MultiTenantHelpers.GetTopLevelTaxonomyTiers(),
                                                                              CurrentPerson.CanViewProposals);

            var taxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();
            var viewData = new DetailViewData(CurrentPerson, projectType, projectLocationsMapInitJson,
                projectLocationsMapViewData, taxonomyLevel);

            return RazorView<Summary, DetailViewData>(viewData);
        }

        [HttpGet]
        [ProjectTypeManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewNew(viewModel);
        }

        [HttpPost]
        [ProjectTypeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }

            var projectType = new ProjectType(viewModel.TaxonomyBranchID, string.Empty, false);
            viewModel.UpdateModel(projectType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.ProjectTypes.Add(projectType);

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"New {FieldDefinition.ProjectType.GetFieldDefinitionLabel()} {projectType.GetDisplayNameAsUrl()} successfully created!");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProjectTypeManageFeature]
        public PartialViewResult Edit(ProjectTypePrimaryKey projectTypePrimaryKey)
        {
            var projectType = projectTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(projectType);
            return ViewEdit(viewModel, projectType.TaxonomyBranch.DisplayName);
        }

        [HttpPost]
        [ProjectTypeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ProjectTypePrimaryKey projectTypePrimaryKey, EditViewModel viewModel)
        {
            var projectType = projectTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, projectType.TaxonomyBranch.DisplayName);
            }

            viewModel.UpdateModel(projectType, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(EditViewModel viewModel)
        {
            return ViewEdit(viewModel, string.Empty);
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, string taxonomyBranchDisplayName)
        {
            var taxonomyBranches = HttpRequestStorage.DatabaseEntities.TaxonomyBranches.ToList()
                .OrderBy(x => x.DisplayName)
                .ToSelectList(x => x.TaxonomyBranchID.ToString(CultureInfo.InvariantCulture), x => x.DisplayName);
            var viewData = new EditViewData(taxonomyBranches, taxonomyBranchDisplayName);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectTypeManageFeature]
        public PartialViewResult DeleteProjectType(ProjectTypePrimaryKey projectTypePrimaryKey)
        {
            var projectType = projectTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectType.ProjectTypeID);
            return ViewDeleteProjectType(projectType, viewModel);
        }

        private PartialViewResult ViewDeleteProjectType(ProjectType projectType,
            ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !projectType.HasDependentObjects() &&
                            HttpRequestStorage.DatabaseEntities.ProjectTypes.Count() > 1;
            var projectTypeDisplayName = FieldDefinition.ProjectType.GetFieldDefinitionLabel();
            var confirmMessage = canDelete
                ? string.Format("Are you sure you want to delete this {0} '{1}'?", projectTypeDisplayName,
                    projectType.DisplayName)
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage(projectTypeDisplayName,
                    SitkaRoute<ProjectTypeController>.BuildLinkFromExpression(x => x.Detail(projectType), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }

        [HttpPost]
        [ProjectTypeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProjectType(ProjectTypePrimaryKey projectTypePrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var projectType = projectTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteProjectType(projectType, viewModel);
            }

            projectType.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [ProjectTypeViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData(ProjectTypePrimaryKey projectTypePrimaryKey)
        {
            var gridSpec = new BasicProjectInfoGridSpec(CurrentPerson, true);
            var projectProjectTypes = projectTypePrimaryKey.EntityObject.GetAssociatedProjects(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectProjectTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProjectTypeManageFeature]
        public PartialViewResult EditSortOrder()
        {
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes;
            EditSortOrderViewModel viewModel = new EditSortOrderViewModel();
            return ViewEditSortOrder(projectTypes, viewModel);
        }

        private PartialViewResult ViewEditSortOrder(IEnumerable<ProjectType> projectTypes,
            EditSortOrderViewModel viewModel)
        {
            EditSortOrderViewData viewData = new EditSortOrderViewData(new List<IHaveASortOrder>(projectTypes),
                FieldDefinition.ProjectType.GetFieldDefinitionLabelPluralized());
            return RazorPartialView<EditSortOrder, EditSortOrderViewData, EditSortOrderViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectTypeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditSortOrder(EditSortOrderViewModel viewModel)
        {
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes;

            if (!ModelState.IsValid)
            {
                return ViewEditSortOrder(projectTypes, viewModel);
            }

            viewModel.UpdateModel(new List<IHaveASortOrder>(projectTypes));
            SetMessageForDisplay(
                $"Successfully Updated {FieldDefinition.ProjectType.GetFieldDefinitionLabel()} Sort Order");
            return new ModalDialogFormJsonResult();
        }
    }
}
