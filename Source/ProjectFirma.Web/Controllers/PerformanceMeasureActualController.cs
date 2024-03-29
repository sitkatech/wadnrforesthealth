﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureActualController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.PerformanceMeasureActual;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Shared.SortOrder;

namespace ProjectFirma.Web.Controllers
{
    public class PerformanceMeasureActualController : FirmaBaseController
    {
        [HttpGet]
        [PerformanceMeasureActualFromProjectManageFeature]
        public PartialViewResult EditPerformanceMeasureActualsForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var performanceMeasureActualSimples = project.PerformanceMeasureActuals.OrderBy(pam => pam.PerformanceMeasure.SortOrder).ThenBy(pam=>pam.PerformanceMeasure.DisplayName).Select(x => new PerformanceMeasureActualSimple(x)).ToList();
            var projectExemptReportingYears = project.GetPerformanceMeasuresExemptReportingYears().Select(x => new ProjectExemptReportingYearSimple(x)).ToList();
            var currentExemptedYears = projectExemptReportingYears.Select(x => x.CalendarYear).ToList();
            var endYear = DateTime.Now.Year;
            var startYear = project.GetImplementationStartYear() ?? endYear;
            var possibleYearsToExempt = FirmaDateUtilities.GetRangeOfYears(startYear, endYear).OrderBy(x => x).ToList();
            projectExemptReportingYears.AddRange(
                possibleYearsToExempt.Where(x => !currentExemptedYears.Contains(x)).Select((x, index) => new ProjectExemptReportingYearSimple(-(index + 1), project.ProjectID, x)));

            var viewModel = new EditPerformanceMeasureActualsViewModel(performanceMeasureActualSimples,
                project.PerformanceMeasureActualYearsExemptionExplanation,
                projectExemptReportingYears.OrderBy(x => x.CalendarYear).ToList());
            return ViewEditPerformanceMeasureActuals(project, viewModel);
        }

        [HttpPost]
        [PerformanceMeasureActualFromProjectManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPerformanceMeasureActualsForProject(ProjectPrimaryKey projectPrimaryKey, EditPerformanceMeasureActualsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPerformanceMeasureActuals(project, viewModel);
            }
            var currentPerformanceMeasureActuals = project.PerformanceMeasureActuals.ToList();
            return UpdatePerformanceMeasureActuals(viewModel, currentPerformanceMeasureActuals, project);
        }

        private static ActionResult UpdatePerformanceMeasureActuals(EditPerformanceMeasureActualsViewModel viewModel,
            List<PerformanceMeasureActual> currentPerformanceMeasureActuals,
            Project project)
        {
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureActuals.Load();
            var allPerformanceMeasureActuals = HttpRequestStorage.DatabaseEntities.PerformanceMeasureActuals.Local;
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureActualSubcategoryOptions.Load();
            var allPerformanceMeasureActualSubcategoryOptions = HttpRequestStorage.DatabaseEntities.PerformanceMeasureActualSubcategoryOptions.Local;
            viewModel.UpdateModel(currentPerformanceMeasureActuals, allPerformanceMeasureActuals, allPerformanceMeasureActualSubcategoryOptions, project);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPerformanceMeasureActuals(Project project, EditPerformanceMeasureActualsViewModel viewModel)
        {
            var performanceMeasures = PerformanceMeasureModelExtensions.GetReportablePerformanceMeasures().ToList().SortByOrderThenName().ToList();
            var showExemptYears = project.GetPerformanceMeasuresExemptReportingYears().Any() ||
                                  ModelState.Values.SelectMany(x => x.Errors)
                                      .Any(
                                          x =>
                                              x.ErrorMessage == FirmaValidationMessages.ExplanationNotNecessaryForProjectExemptYears ||
                                              x.ErrorMessage == FirmaValidationMessages.ExplanationNecessaryForProjectExemptYears);
            var viewData = new EditPerformanceMeasureActualsViewData(project, performanceMeasures, showExemptYears);
            return RazorPartialView<EditPerformanceMeasureActuals, EditPerformanceMeasureActualsViewData, EditPerformanceMeasureActualsViewModel>(viewData, viewModel);
        }
    }
}
