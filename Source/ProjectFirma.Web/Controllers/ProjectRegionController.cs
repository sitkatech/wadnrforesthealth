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
            var regionIDs = project.ProjectRegions.Select(x => x.RegionID).ToList();
            var regionNotes = string.Empty;// project.ProjectRegionTypeNotes.SingleOrDefault(x=> x.RegionTypeID == regionType.RegionTypeID)?.Notes;
            var viewModel = new EditProjectRegionsViewModel(regionIDs, regionNotes);
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
            var projectRegionTypeNote = string.Empty;// project.ProjectRegionTypeNotes.SingleOrDefault(x => x.RegionTypeID == regionType.RegionTypeID);
            if (!string.IsNullOrWhiteSpace(viewModel.ProjectRegionNotes))
            {
                //if (projectRegionTypeNote == null)
                //{
                //    projectRegionTypeNote = new ProjectRegionTypeNote(project, regionType, viewModel.ProjectRegionNotes);
                //}
                //projectRegionTypeNote.Notes = viewModel.ProjectRegionNotes;
            }
            else
            {
                projectRegionTypeNote?.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Regions were successfully saved.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectRegions(EditProjectRegionsViewModel viewModel, Project project)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetRegionMapLayers( LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectRegionMap", 0, layers, boundingBox) { AllowFullScreen = false, DisablePopups = true};
            var regionIDs = viewModel.RegionIDs ?? new List<int>();
            var regionsInViewModel = HttpRequestStorage.DatabaseEntities.Regions.Where(x => regionIDs.Contains(x.RegionID)).ToList();
            var editProjectRegionsPostUrl = SitkaRoute<ProjectRegionController>.BuildUrlFromExpression(c => c.EditProjectRegions(project, null));
            var editProjectRegionsFormID = GetEditProjectRegionsFormID();

            var viewData = new EditProjectRegionsViewData(CurrentPerson, mapInitJson, regionsInViewModel, editProjectRegionsPostUrl, editProjectRegionsFormID, project.HasProjectLocationPoint, project.HasProjectLocationDetail);
            return RazorPartialView<EditProjectRegions, EditProjectRegionsViewData, EditProjectRegionsViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult FindRegionByName(string term)
        {
            var searchString = term.Trim();
            return Json(HttpRequestStorage.DatabaseEntities.Regions
                .Where(x => x.RegionName.Contains(searchString)).OrderBy(x => x.RegionName).Take(20).ToList()
                .Select(x => new {x.RegionName, x.RegionID}).ToList(), JsonRequestBehavior.AllowGet);
        }

        public static string GetEditProjectRegionsFormID()
        {
            return "editProjectRegionsForm";
        }
    }
}
