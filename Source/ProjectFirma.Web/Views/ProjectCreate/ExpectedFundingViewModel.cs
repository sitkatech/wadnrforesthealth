﻿/*-----------------------------------------------------------------------
<copyright file="ExpectedFundingViewModel.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using Newtonsoft.Json;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class ExpectedFundingViewModel : FormViewModel, IValidatableObject
    {
        public List<ProjectGrantAllocationRequestSimple> ProjectGrantAllocationRequestSimples { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedTotalCost)]
        [JsonIgnore]
        public Money? ProjectEstimatedTotalCost { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundingSource)]
        public List<int> FundingSourceIDs { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundingSourceNote)]
        [StringLength(Models.Project.FieldLengths.ProjectFundingSourceNotes)]
        public string ProjectFundingSourceNotes { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ExpectedFundingViewModel()
        {
        }

        public ExpectedFundingViewModel(List<Models.ProjectGrantAllocationRequest> projectGrantAllocationRequests, Money? projectEstimatedTotalCost, string projectFundingSourceNotes, List<ProjectFundingSource> projectFundingSources)
        {
            ProjectGrantAllocationRequestSimples = projectGrantAllocationRequests.Select(x => new ProjectGrantAllocationRequestSimple(x)).ToList();
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectFundingSourceNotes = projectFundingSourceNotes;
            FundingSourceIDs = projectFundingSources.Select(x => x.FundingSourceID).ToList();
        }

        public void UpdateModel(Models.Project project,
                                List<Models.ProjectGrantAllocationRequest> currentProjectGrantAllocationRequests,
                                IList<Models.ProjectGrantAllocationRequest> allProjectGrantAllocationRequests,
                                List<ProjectFundingSource> currentProjectFundingSources,
                                IList<ProjectFundingSource> allProjectFundingSources)
        {
            //Update the ProjectGrantAllocationRequests
            var dateNow = DateTime.Now;
            var projectGrantAllocationRequestsUpdated = new List<Models.ProjectGrantAllocationRequest>();
            if (ProjectGrantAllocationRequestSimples != null)
            {

                foreach (var projectGrantAllocationRequestSimple in ProjectGrantAllocationRequestSimples)
                {
                    var existingCurrentOne = currentProjectGrantAllocationRequests.SingleOrDefault(x =>
                        x.ProjectID == projectGrantAllocationRequestSimple.ProjectID &&
                        x.GrantAllocationID == projectGrantAllocationRequestSimple.GrantAllocationID);
                    if (existingCurrentOne != null)
                    {
                        var projectGrantAllocationRequestToAdd =
                            projectGrantAllocationRequestSimple.ToProjectGrantAllocationRequest(
                                existingCurrentOne.CreateDate, dateNow, existingCurrentOne.ImportedFromTabularData);
                        projectGrantAllocationRequestsUpdated.Add(projectGrantAllocationRequestToAdd);
                    }
                    else
                    {
                        var projectGrantAllocationRequestToAdd =
                            projectGrantAllocationRequestSimple.ToProjectGrantAllocationRequest(
                                dateNow, null, false);
                        projectGrantAllocationRequestsUpdated.Add(projectGrantAllocationRequestToAdd);
                    }
                }
            }

            currentProjectGrantAllocationRequests.Merge(projectGrantAllocationRequestsUpdated,
                allProjectGrantAllocationRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.GrantAllocationID == y.GrantAllocationID,
                (x, y) =>
                {
                    x.TotalAmount = y.TotalAmount;
                    x.MatchAmount = y.MatchAmount;
                    x.PayAmount = y.PayAmount;
                    x.UpdateDate = y.UpdateDate;
                    x.CreateDate = y.CreateDate;
                    x.ImportedFromTabularData = y.ImportedFromTabularData;
                });

            //Update the ProjectFundingSources
            var projectFundingSourcesUpdated = new List<Models.ProjectFundingSource>();
            if (FundingSourceIDs != null && FundingSourceIDs.Any())
            {
                // Completely rebuild the list
                projectFundingSourcesUpdated = FundingSourceIDs.Select(x => new ProjectFundingSource(project.ProjectID, x)).ToList();
            }

            currentProjectFundingSources.Merge(projectFundingSourcesUpdated,
                allProjectFundingSources,
                (x, y) => x.ProjectID == y.ProjectID && x.FundingSourceID == y.FundingSourceID);

            //Update Project fields
            project.ProjectFundingSourceNotes = ProjectFundingSourceNotes;
            project.EstimatedTotalCost = ProjectEstimatedTotalCost;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            if (ProjectGrantAllocationRequestSimples == null)
            {
                yield break;
            }

            if (ProjectGrantAllocationRequestSimples.GroupBy(x => x.GrantAllocationID).Any(x => x.Count() > 1))
            {
                yield return new ValidationResult("Each grant allocation can only be used once.");
            }

            //foreach (var projectGrantAllocationRequestSimple in ProjectGrantAllocationRequestSimples)
            //{
            //    if (projectGrantAllocationRequestSimple.AreBothValuesZero())
            //    {
            //        var grantAllocation =
            //            HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(x =>
            //                x.GrantAllocationID == projectGrantAllocationRequestSimple.GrantAllocationID);
            //        yield return new ValidationResult(
            //            $"Secured Funding and Unsecured Funding cannot both be zero for Grant Allocation: {grantAllocation.DisplayName}. If the amount of secured or unsecured funding is unknown, you can leave the amounts blank.");
            //    }
            //}
        }
    }
}