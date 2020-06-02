/*-----------------------------------------------------------------------
<copyright file="ProjectRegionController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.ProjectRegion;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Map;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectRegionController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectRegions(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var regionIDs = project.ProjectRegions.Select(x => x.DNRUplandRegionID).ToList();
            var noRegionsExplanation = project.NoRegionsExplanation;
            var viewModel = new EditProjectRegionsViewModel(regionIDs, noRegionsExplanation);
            return ViewEditProjectRegions(viewModel, project);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectRegions(ProjectPrimaryKey projectPrimaryKey, EditProjectRegionsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditProjectRegions(viewModel, project);
            }

            var currentProjectRegions = project.ProjectRegions.ToList();
            var allProjectRegions = HttpRequestStorage.DatabaseEntities.ProjectRegions.Local;
            viewModel.UpdateModel(project, currentProjectRegions, allProjectRegions);
            project.NoRegionsExplanation = !string.IsNullOrWhiteSpace(viewModel.NoRegionsExplanation) ? viewModel.NoRegionsExplanation : null;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Regions were successfully saved.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectRegions(EditProjectRegionsViewModel viewModel, Project project)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetRegionMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectRegionMap", 0, layers, boundingBox) { AllowFullScreen = false, DisablePopups = true};
            var regionIDs = viewModel.RegionIDs ?? new List<int>();
            var regionsInViewModel = HttpRequestStorage.DatabaseEntities.DNRUplandRegions.Where(x => regionIDs.Contains(x.DNRUplandRegionID)).ToList();
            var editProjectRegionsPostUrl = SitkaRoute<ProjectRegionController>.BuildUrlFromExpression(c => c.EditProjectRegions(project, null));
            var editProjectRegionsFormID = GetEditProjectRegionsFormID();

            var viewData = new EditProjectRegionsViewData(CurrentPerson, mapInitJson, regionsInViewModel, editProjectRegionsPostUrl, editProjectRegionsFormID, project.HasProjectLocationPoint, project.HasProjectLocationDetail);
            return RazorPartialView<EditProjectRegions, EditProjectRegionsViewData, EditProjectRegionsViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult FindRegionByName(string term)
        {
            var searchString = term.Trim();
            return Json(HttpRequestStorage.DatabaseEntities.DNRUplandRegions
                .Where(x => x.DNRUplandRegionName.Contains(searchString)).OrderBy(x => x.DNRUplandRegionName).Take(20).ToList()
                .Select(x => new {RegionName = x.DNRUplandRegionName, RegionID = x.DNRUplandRegionID}).ToList(), JsonRequestBehavior.AllowGet);
        }

        public static string GetEditProjectRegionsFormID()
        {
            return "editProjectRegionsForm";
        }
    }
}
