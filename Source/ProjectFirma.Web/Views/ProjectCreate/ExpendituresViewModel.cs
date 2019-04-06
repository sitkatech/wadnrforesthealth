/*-----------------------------------------------------------------------
<copyright file="ExpendituresViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.ProjectUpdate;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class ExpendituresViewModel : FormViewModel, IValidatableObject
    {
        public int? ProjectID { get; set; }

        [DisplayName("Show Validation Warnings?")]
        public bool ShowValidationWarnings { get; set; }

        public List<ProjectGrantAllocationExpenditureBulk> ProjectGrantAllocationExpenditures { get; set; }
        public string Explanation { get; set; }

        public List<ProjectExemptReportingYearSimple> ProjectExemptReportingYears { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ExpendituresViewModel()
        {
        }

        public ExpendituresViewModel(List<Models.ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
                                     List<int> calendarYearsToPopulate, Models.Project project,
                                     List<ProjectExemptReportingYearSimple> projectExemptReportingYears)
        {
            ProjectExemptReportingYears = projectExemptReportingYears;
            Explanation = project.NoExpendituresToReportExplanation;
            ProjectGrantAllocationExpenditures = ProjectGrantAllocationExpenditureBulk.MakeFromList(projectGrantAllocationExpenditures, calendarYearsToPopulate);
            ShowValidationWarnings = true;
        }

        public void UpdateModel(Models.Project project,
                                List<Models.ProjectGrantAllocationExpenditure> currentProjectGrantAllocationExpenditures,
                                IList<Models.ProjectGrantAllocationExpenditure> allProjectGrantAllocationExpenditures)
        {
            var projectGrantAllocationExpendituresUpdated = new List<Models.ProjectGrantAllocationExpenditure>();
            if (ProjectGrantAllocationExpenditures != null)
            {
                // Completely rebuild the list
                projectGrantAllocationExpendituresUpdated = ProjectGrantAllocationExpenditures.SelectMany(x => x.ToProjectGrantAllocationExpenditures()).ToList();
            }


            var currentProjectExemptYears = project.GetExpendituresExemptReportingYears();
            HttpRequestStorage.DatabaseEntities.ProjectExemptReportingYears.Load();
            var allProjectExemptYears = HttpRequestStorage.DatabaseEntities.ProjectExemptReportingYears.Local;
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

            currentProjectGrantAllocationExpenditures.Merge(projectGrantAllocationExpendituresUpdated,
                allProjectGrantAllocationExpenditures,
                (x, y) => x.ProjectID == y.ProjectID && x.GrantAllocationID == y.GrantAllocationID && x.CalendarYear == y.CalendarYear,
                (x, y) => x.ExpenditureAmount = y.ExpenditureAmount);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var errors = new List<ValidationResult>();
            var project = HttpRequestStorage.DatabaseEntities.Projects.Single(x => x.ProjectID == ProjectID);
            var validationErrors = ExpendituresValidationResult.Validate(ProjectGrantAllocationExpenditures,
                ProjectExemptReportingYears, Explanation, project.GetProjectUpdatePlanningDesignStartToCompletionDateRange());
            errors.AddRange(validationErrors.Select(x => new ValidationResult(x)));

            return errors;
        }
    }
}
