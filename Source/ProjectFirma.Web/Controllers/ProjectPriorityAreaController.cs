/*-----------------------------------------------------------------------
<copyright file="ProjectPriorityAreaController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Views.ProjectPriorityArea;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Map;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectPriorityAreaController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectPriorityAreas(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var priorityAreaIDs = project.ProjectPriorityAreas.Select(x => x.PriorityAreaID).ToList();
            var noPriorityAreasExplanation = project.NoPriorityAreasExplanation;
            var viewModel = new EditProjectPriorityAreasViewModel(priorityAreaIDs, noPriorityAreasExplanation);
            return ViewEditProjectPriorityAreas(viewModel, project);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectPriorityAreas(ProjectPrimaryKey projectPrimaryKey, EditProjectPriorityAreasViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditProjectPriorityAreas(viewModel, project);
            }

            var currentProjectPriorityAreas = project.ProjectPriorityAreas.ToList();
            var allProjectPriorityAreas = HttpRequestStorage.DatabaseEntities.ProjectPriorityAreas.Local;
            viewModel.UpdateModel(project, currentProjectPriorityAreas, allProjectPriorityAreas);
            project.NoPriorityAreasExplanation = !string.IsNullOrWhiteSpace(viewModel.NoPriorityAreasExplanation) ? viewModel.NoPriorityAreasExplanation : null;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} PriorityAreas were successfully saved.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectPriorityAreas(EditProjectPriorityAreasViewModel viewModel, Project project)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetPriorityAreaMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectPriorityAreaMap", 0, layers, boundingBox) { AllowFullScreen = false, DisablePopups = true};
            var priorityAreaIDs = viewModel.PriorityAreaIDs ?? new List<int>();
            var priorityAreasInViewModel = HttpRequestStorage.DatabaseEntities.PriorityAreas.Where(x => priorityAreaIDs.Contains(x.PriorityAreaID)).ToList();
            var editProjectPriorityAreasPostUrl = SitkaRoute<ProjectPriorityAreaController>.BuildUrlFromExpression(c => c.EditProjectPriorityAreas(project, null));
            var editProjectPriorityAreasFormID = GetEditProjectPriorityAreasFormID();

            var viewData = new EditProjectPriorityAreasViewData(CurrentPerson, mapInitJson, priorityAreasInViewModel, editProjectPriorityAreasPostUrl, editProjectPriorityAreasFormID, project.HasProjectLocationPoint, project.HasProjectLocationDetail);
            return RazorPartialView<EditProjectPriorityAreas, EditProjectPriorityAreasViewData, EditProjectPriorityAreasViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult FindPriorityAreaByName(string term)
        {
            var searchString = term.Trim();
            return Json(HttpRequestStorage.DatabaseEntities.PriorityAreas
                .Where(x => x.PriorityAreaName.Contains(searchString)).OrderBy(x => x.PriorityAreaName).Take(20).ToList()
                .Select(x => new {x.PriorityAreaName, x.PriorityAreaID}).ToList(), JsonRequestBehavior.AllowGet);
        }

        public static string GetEditProjectPriorityAreasFormID()
        {
            return "editProjectPriorityAreasForm";
        }
    }
}
