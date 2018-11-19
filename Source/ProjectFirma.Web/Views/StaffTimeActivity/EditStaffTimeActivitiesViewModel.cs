/*-----------------------------------------------------------------------
<copyright file="EditStaffTimeActivitysViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.ProjectUpdate;

namespace ProjectFirma.Web.Views.StaffTimeActivity
{
    public class EditStaffTimeActivitiesViewModel : FormViewModel, IValidatableObject
    {
        public int ProjectID { get; set; }

        public List<StaffTimeActivitySimple> StaffTimeActivities { get; set; }
        public string Explanation { get; set; }

        public List<ProjectExemptReportingYearSimple> ProjectExemptReportingYears { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditStaffTimeActivitiesViewModel()
        {
        }

        public EditStaffTimeActivitiesViewModel(Models.Project project,
            List<StaffTimeActivitySimple> staffTimeActivityBulks,
            List<ProjectExemptReportingYearSimple> projectExemptReportingYears)
        {
            StaffTimeActivities = staffTimeActivityBulks;
            ProjectExemptReportingYears = projectExemptReportingYears;
            Explanation = project.NoExpendituresToReportExplanation;
            ProjectID = project.ProjectID;
        }

        public void UpdateModel(List<Models.StaffTimeActivity> currentStaffTimeActivitys,
            IList<Models.StaffTimeActivity> allStaffTimeActivitys, Models.Project project)
        {
            var staffTimeActivitysUpdated = new List<Models.StaffTimeActivity>();
            if (StaffTimeActivities != null)
            {
                staffTimeActivitysUpdated = StaffTimeActivities.SelectMany(x => x.ToStaffTimeActivitys()).ToList();
            }

            var currentProjectExemptYears = project.GetExpendituresExemptReportingYears();
            HttpRequestStorage.DatabaseEntities.ProjectExemptReportingYears.Load();
            var allProjectExemptYears = HttpRequestStorage.DatabaseEntities.AllProjectExemptReportingYears.Local;
            var projectExemptReportingYears = new List<ProjectExemptReportingYear>();
            if (ProjectExemptReportingYears != null)
            {
                // Completely rebuild the list
                projectExemptReportingYears =
                    ProjectExemptReportingYears.Where(x => x.IsExempt)
                        .Select(x => new ProjectExemptReportingYear(x.ProjectExemptReportingYearID, x.ProjectID, x.CalendarYear, ProjectExemptReportingType.Expenditures.ProjectExemptReportingTypeID))
                        .ToList();
            }
            currentProjectExemptYears.Merge(projectExemptReportingYears,
                allProjectExemptYears,
                (x, y) => x.ProjectID == y.ProjectID && x.CalendarYear == y.CalendarYear && x.ProjectExemptReportingTypeID == y.ProjectExemptReportingTypeID);

            project.NoExpendituresToReportExplanation = Explanation;

            currentStaffTimeActivitys.Merge(staffTimeActivitysUpdated,
                allStaffTimeActivitys,
                (x, y) => x.ProjectID == y.ProjectID && x.FundingSourceID == y.FundingSourceID && x.CalendarYear == y.CalendarYear,
                (x, y) => x.ExpenditureAmount = y.ExpenditureAmount);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var project = HttpRequestStorage.DatabaseEntities.Projects.Single(x => x.ProjectID == ProjectID);
            var validationErrors = ExpendituresValidationResult.Validate(StaffTimeActivities, ProjectExemptReportingYears, Explanation, project.GetProjectUpdatePlanningDesignStartToCompletionYearRange());
            errors.AddRange(validationErrors.Select(x => new ValidationResult(x)));
            return errors;
        }
    }
}
