﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureExpectedController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Shared.PerformanceMeasureControls;

namespace ProjectFirma.Web.Controllers
{
    public class PerformanceMeasureExpectedController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditPerformanceMeasureExpectedsForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var performanceMeasureExpectedSimples = project.PerformanceMeasureExpecteds.OrderBy(pam => pam.PerformanceMeasure.PerformanceMeasureSortOrder).ThenBy(x=>x.PerformanceMeasure.DisplayName).Select(x => new PerformanceMeasureExpectedSimple(x)).ToList();
            var viewModel = new EditPerformanceMeasureExpectedViewModel(performanceMeasureExpectedSimples);
            return ViewEditPerformanceMeasureExpecteds(project, viewModel);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPerformanceMeasureExpectedsForProject(ProjectPrimaryKey projectPrimaryKey, EditPerformanceMeasureExpectedViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPerformanceMeasureExpecteds(project, viewModel);
            }
            var currentPerformanceMeasureExpecteds = project.PerformanceMeasureExpecteds.ToList();
            return UpdatePerformanceMeasureExpecteds(viewModel, currentPerformanceMeasureExpecteds, project);
        }

        private static ActionResult UpdatePerformanceMeasureExpecteds(EditPerformanceMeasureExpectedViewModel viewModel, List<PerformanceMeasureExpected> currentPerformanceMeasureExpecteds, Project project)
        {
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpecteds.Load();
            var allPerformanceMeasureExpecteds = HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpecteds.Local;
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpectedSubcategoryOptions.Load();
            var allPerformanceMeasureExpectedSubcategoryOptions = HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpectedSubcategoryOptions.Local;
            viewModel.UpdateModel(currentPerformanceMeasureExpecteds, allPerformanceMeasureExpecteds, allPerformanceMeasureExpectedSubcategoryOptions, project);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPerformanceMeasureExpecteds(Project project, EditPerformanceMeasureExpectedViewModel viewModel)
        {
            var performanceMeasures = PerformanceMeasureModelExtensions.GetReportablePerformanceMeasures().ToList();
            var viewData = new EditPerformanceMeasureExpectedViewData(project, performanceMeasures);
            return RazorPartialView<EditPerformanceMeasureExpected, EditPerformanceMeasureExpectedViewData, EditPerformanceMeasureExpectedViewModel>(viewData, viewModel);
        }
    }
}
