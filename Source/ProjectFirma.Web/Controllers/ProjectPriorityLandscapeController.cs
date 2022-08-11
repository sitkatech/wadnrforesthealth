/*-----------------------------------------------------------------------
<copyright file="ProjectPriorityLandscapeController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Views.ProjectPriorityLandscape;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Map;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectPriorityLandscapeController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectPriorityLandscapes(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var priorityLandscapeIDs = project.ProjectPriorityLandscapes.Select(x => x.PriorityLandscapeID).ToList();
            var noPriorityLandscapesExplanation = project.NoPriorityLandscapesExplanation;
            var viewModel = new EditProjectPriorityLandscapesViewModel(priorityLandscapeIDs, noPriorityLandscapesExplanation);
            return ViewEditProjectPriorityLandscapes(viewModel, project);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectPriorityLandscapes(ProjectPrimaryKey projectPrimaryKey, EditProjectPriorityLandscapesViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditProjectPriorityLandscapes(viewModel, project);
            }

            var currentProjectPriorityLandscapes = project.ProjectPriorityLandscapes.ToList();
            var allProjectPriorityLandscapes = HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapes.Local;
            viewModel.UpdateModel(project, currentProjectPriorityLandscapes, allProjectPriorityLandscapes);
            project.NoPriorityLandscapesExplanation = !string.IsNullOrWhiteSpace(viewModel.NoPriorityLandscapesExplanation) ? viewModel.NoPriorityLandscapesExplanation : null;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Priority Landscapes were successfully saved.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectPriorityLandscapes(EditProjectPriorityLandscapesViewModel viewModel, Project project)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetPriorityLandscapeMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectPriorityLandscapeMap", 0, layers, MapInitJson.GetExternalMapLayersForPriorityLandscape(), boundingBox) { AllowFullScreen = false, DisablePopups = true};
            var priorityLandscapeIDs = viewModel.PriorityLandscapeIDs ?? new List<int>();
            var priorityLandscapesInViewModel = HttpRequestStorage.DatabaseEntities.PriorityLandscapes.Where(x => priorityLandscapeIDs.Contains(x.PriorityLandscapeID)).ToList();
            var editProjectPriorityLandscapesPostUrl = SitkaRoute<ProjectPriorityLandscapeController>.BuildUrlFromExpression(c => c.EditProjectPriorityLandscapes(project, null));
            var editProjectPriorityLandscapesFormID = GetEditProjectPriorityLandscapesFormID();

            var viewData = new EditProjectPriorityLandscapesViewData(CurrentPerson, mapInitJson, priorityLandscapesInViewModel, editProjectPriorityLandscapesPostUrl, editProjectPriorityLandscapesFormID, project.HasProjectLocationPoint, project.HasProjectLocationDetail);
            return RazorPartialView<EditProjectPriorityLandscapes, EditProjectPriorityLandscapesViewData, EditProjectPriorityLandscapesViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult FindPriorityLandscapeByName(string term)
        {
            var searchString = term.Trim();
            return Json(HttpRequestStorage.DatabaseEntities.PriorityLandscapes
                .Where(x => x.PriorityLandscapeName.Contains(searchString)).OrderBy(x => x.PriorityLandscapeName).Take(20).ToList()
                .Select(x => new {x.PriorityLandscapeName, x.PriorityLandscapeID}).ToList(), JsonRequestBehavior.AllowGet);
        }

        public static string GetEditProjectPriorityLandscapesFormID()
        {
            return "editProjectPriorityLandscapesForm";
        }
    }
}
