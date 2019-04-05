/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationExpenditureController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
//using ProjectFirma.Web.Views.ProjectGrantAllocationExpenditure;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.ProjectGrantAllocationExpenditure;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectGrantAllocationExpenditureController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditProjectGrantAllocationExpendituresForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var projectFundingSourceExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            var calendarYearRange = projectFundingSourceExpenditures.CalculateCalendarYearRangeForExpenditures(project);
            var projectExemptReportingYears = project.GetExpendituresExemptReportingYears().Select(x => new ProjectExemptReportingYearSimple(x)).ToList();
            var currentExemptedYears = projectExemptReportingYears.Select(x => x.CalendarYear).ToList();
            projectExemptReportingYears.AddRange(calendarYearRange.Where(x => !currentExemptedYears.Contains(x)).Select((x, index) => new ProjectExemptReportingYearSimple(-(index + 1), project.ProjectID, x)));
            var calendarYearRangeForExpenditures = projectFundingSourceExpenditures.CalculateCalendarYearRangeForExpenditures(project);
            var projectFundingSourceExpenditureBulks = ProjectGrantAllocationExpenditureBulk.MakeFromList(projectFundingSourceExpenditures, calendarYearRangeForExpenditures);
            var viewModel = new EditProjectGrantAllocationExpendituresViewModel(project, projectFundingSourceExpenditureBulks, projectExemptReportingYears);
            return ViewEditProjectGrantAllocationExpenditures(project, viewModel, calendarYearRangeForExpenditures);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectGrantAllocationExpendituresForProject(ProjectPrimaryKey projectPrimaryKey, EditProjectGrantAllocationExpendituresViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            var currentProjectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            if (!ModelState.IsValid)
            {
                var calendarYearRangeForExpenditures = currentProjectGrantAllocationExpenditures.CalculateCalendarYearRangeForExpenditures(project);
                return ViewEditProjectGrantAllocationExpenditures(project, viewModel, calendarYearRangeForExpenditures);
            }

            return UpdateProjectGrantAllocationExpenditures(viewModel, currentProjectGrantAllocationExpenditures, project);
        }

        private static ActionResult UpdateProjectGrantAllocationExpenditures(
            EditProjectGrantAllocationExpendituresViewModel viewModel,
            List<ProjectGrantAllocationExpenditure> currentProjectGrantAllocationExpenditures, Project project)
        {
            HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.Load();
            var allProjectGrantAllocationExpenditures = HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.Local;
            viewModel.UpdateModel(currentProjectGrantAllocationExpenditures, allProjectGrantAllocationExpenditures, project);

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditProjectGrantAllocationExpenditures(Project project, EditProjectGrantAllocationExpendituresViewModel viewModel, List<int> calendarYearRangeForExpenditures)
        {
            var allGrantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList().Select(x => new GrantAllocationSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var showNoExpendituresExplanation = project.GetExpendituresExemptReportingYears().Any();
            var viewData = new EditProjectGrantAllocationExpendituresViewData(new ProjectSimple(project), allGrantAllocations, calendarYearRangeForExpenditures, showNoExpendituresExplanation);
            return RazorPartialView<EditProjectGrantAllocationExpenditures, EditProjectGrantAllocationExpendituresViewData, EditProjectGrantAllocationExpendituresViewModel>(viewData, viewModel);
        }
    }
}
