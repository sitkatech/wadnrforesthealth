/*-----------------------------------------------------------------------
<copyright file="ExpendituresValidationResult.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ExpendituresValidationResult
    {
        public static List<string> Validate(List<ProjectGrantAllocationExpenditureBulk> projectGrantAllocationExpenditureBulks, List<ProjectExemptReportingYearSimple> projectExemptReportingYearSimples, string explanation, List<int> expectedYears)
        {
            var errors = new List<string>();
            var emptyRows = projectGrantAllocationExpenditureBulks?.Where(x => x.CalendarYearExpenditures.All(y => !y.MonetaryAmount.HasValue));

            if (projectGrantAllocationExpenditureBulks == null)
            {
                projectGrantAllocationExpenditureBulks = new List<ProjectGrantAllocationExpenditureBulk>();
            }

            if (projectExemptReportingYearSimples == null)
            {
                projectExemptReportingYearSimples = new List<ProjectExemptReportingYearSimple>();
            }

            if (emptyRows?.Any() ?? false)
            {
                errors.Add($"The {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} could not be saved because there are blank rows. Enter a value in all fields or delete grant allocations for which there is no expenditure data to report.");
            }

            // get distinct Grant Allocations
            var projectGrantAllocationExpenditures = projectGrantAllocationExpenditureBulks.SelectMany(x => x.ToProjectGrantAllocationExpenditures()).ToList();
            var projectGrantAllocationExpendituresList = new List<IGrantAllocationExpenditure>(projectGrantAllocationExpenditures);
            errors.AddRange(ValidateImpl(projectExemptReportingYearSimples, explanation, expectedYears, projectGrantAllocationExpendituresList));
            return errors;
        }

        public static List<string> ValidateImpl(List<ProjectExemptReportingYearSimple> projectExemptReportingYearSimples, 
                                                string explanation, 
                                                List<int> expectedYears,
                                                List<IGrantAllocationExpenditure> projectGrantAllocationExpenditures)
        {
            var errors = new List<string>();

            var grantAllocationsIDs = projectGrantAllocationExpenditures.Select(x => x.GrantAllocationID).Distinct().ToList();
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.Where(x => grantAllocationsIDs.Contains(x.GrantAllocationID));

            // validation 1: ensure that we have expenditure values from ProjectUpdate start year to min(endyear, currentyear)
            if (projectExemptReportingYearSimples.Any(x => x.IsExempt) && string.IsNullOrWhiteSpace(explanation))
            {
                errors.Add(FirmaValidationMessages.ExplanationNecessaryForProjectExemptYears);
            }

            if (!projectExemptReportingYearSimples.Any(x => x.IsExempt) && !string.IsNullOrWhiteSpace(explanation))
            {
                errors.Add(FirmaValidationMessages.ExplanationNotNecessaryForProjectExemptYears);
            }

            var exemptReportingYears = projectExemptReportingYearSimples.Where(y => y.IsExempt).Select(y => y.CalendarYear).ToList();

            var everyYearIsExempt = exemptReportingYears.Count == expectedYears.Count;

            if (!everyYearIsExempt)
            {
                if (grantAllocations.Any())
                {
                    var missingGrantAllocationYears = new Dictionary<Models.GrantAllocation, IEnumerable<int>>();
                    foreach (var currentGrantAllocation in grantAllocations)
                    {
                        //Added check for 0 to prevent a user from submitting a 0 value with no comment
                        var yearsWithValues = projectGrantAllocationExpenditures
                            .Where(x => x.GrantAllocationID == currentGrantAllocation.GrantAllocationID &&
                                        x.ExpenditureAmount > 0)
                            .Select(x => x.CalendarYear);
                        var missingYears =
                            expectedYears
                                .GetMissingYears(yearsWithValues).ToList()
                                .Where(year =>
                                    !exemptReportingYears.Contains(year)).ToList();

                        if (missingYears.Any())
                        {
                            missingGrantAllocationYears.Add(currentGrantAllocation, missingYears);
                        }
                    }

                    foreach (var grantAllocation in missingGrantAllocationYears)
                    {
                        var yearsForErrorDisplay = string.Join(", ", FirmaHelpers.CalculateYearRanges(grantAllocation.Value));
                        errors.Add($"Missing Expenditures for {Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} '{grantAllocation.Key.DisplayName}' for the following years: {string.Join(", ", yearsForErrorDisplay)}");
                    }
                }
            }


            // reported expenditures in exempt years - Added check for 0 to prevent a user from submitting a 0 value with no comment
            var yearsWithExpenditures = projectGrantAllocationExpenditures.Where(x => x.ExpenditureAmount > 0).GroupBy(x => x.GrantAllocationID);
            foreach (var grantAllocation in yearsWithExpenditures)
            {
                var exemptYearsWithReportedValues = grantAllocation
                    .Where(x => exemptReportingYears.Contains(x.CalendarYear)).Select(x => x.CalendarYear)
                    .ToList();
                if (exemptYearsWithReportedValues.Any())
                {
                    var grantAllocationName = grantAllocations.SingleOrDefault(x => x.GrantAllocationID == grantAllocation.Key)?.GrantAllocationName;
                    var yearsForErrorDisplay = string.Join(", ", FirmaHelpers.CalculateYearRanges(exemptYearsWithReportedValues));
                    errors.Add($"Grant Allocation {grantAllocationName} has reported values for the exempt years: {yearsForErrorDisplay}");
                }
            }

            return errors;
        }
    }
}
