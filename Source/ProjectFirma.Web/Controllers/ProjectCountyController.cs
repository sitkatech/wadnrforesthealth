/*-----------------------------------------------------------------------
<copyright file="ProjectCountyController.cs" company="Tahoe Countyal Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Countyal Planning Agency and Sitka Technology Group. All rights reserved.
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
using ProjectFirma.Web.Views.ProjectCounty;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Map;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectCountyController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectCounties(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var regionIDs = project.ProjectCounties.Select(x => x.CountyID).ToList();
            var noCountiesExplanation = project.NoCountiesExplanation;
            var viewModel = new EditProjectCountiesViewModel(regionIDs, noCountiesExplanation);
            return ViewEditProjectCounties(viewModel, project);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectCounties(ProjectPrimaryKey projectPrimaryKey, EditProjectCountiesViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditProjectCounties(viewModel, project);
            }

            var currentProjectCounties = project.ProjectCounties.ToList();
            var allProjectCounties = HttpRequestStorage.DatabaseEntities.ProjectCounties.Local;
            viewModel.UpdateModel(project, currentProjectCounties, allProjectCounties);
            project.NoCountiesExplanation = !string.IsNullOrWhiteSpace(viewModel.NoCountiesExplanation) ? viewModel.NoCountiesExplanation : null;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Counties were successfully saved.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectCounties(EditProjectCountiesViewModel viewModel, Project project)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetCountyMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectCountyMap", 0, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), boundingBox) { AllowFullScreen = false, DisablePopups = true };
            var regionIDs = viewModel.CountyIDs ?? new List<int>();
            var regionsInViewModel = HttpRequestStorage.DatabaseEntities.Counties.Where(x => regionIDs.Contains(x.CountyID)).ToList();
            var editProjectCountiesPostUrl = SitkaRoute<ProjectCountyController>.BuildUrlFromExpression(c => c.EditProjectCounties(project, null));
            var editProjectCountiesFormID = GetEditProjectCountiesFormID();

            var viewData = new EditProjectCountiesViewData(CurrentPerson, mapInitJson, regionsInViewModel, editProjectCountiesPostUrl, editProjectCountiesFormID, project.HasProjectLocationPoint, project.HasProjectLocationDetail);
            return RazorPartialView<EditProjectCounties, EditProjectCountiesViewData, EditProjectCountiesViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult FindCountyByName(string term)
        {
            var searchString = term.Trim();
            return Json(HttpRequestStorage.DatabaseEntities.Counties
                .Where(x => x.CountyName.Contains(searchString)).OrderBy(x => x.CountyName).Take(20).ToList()
                .Select(x => new { CountyName = x.CountyName, CountyID = x.CountyID }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public static string GetEditProjectCountiesFormID()
        {
            return "editProjectCountiesForm";
        }
    }
}
