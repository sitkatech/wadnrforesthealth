/*-----------------------------------------------------------------------
<copyright file="ExpendituresViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using Newtonsoft.Json;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ExpectedFundingViewModel : FormViewModel, IValidatableObject
    {       
        [DisplayName("Comments")]
        [StringLength(ProjectUpdateBatch.FieldLengths.ExpendituresComment)]
        public string Comments { get; set; }

        public List<ProjectFundSourceAllocationRequestSimple> ProjectFundSourceAllocationRequests { get; set; }

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

        public ExpectedFundingViewModel(List<ProjectFundSourceAllocationRequestUpdate> projectFundSourceAllocationRequestUpdates,
            string comments, Money? projectEstimatedTotalCost, string projectFundingSourceNotes, List<ProjectFundingSourceUpdate> projectFundingSourceUpdates)
        {
            ProjectFundSourceAllocationRequests = projectFundSourceAllocationRequestUpdates.Select(x => new ProjectFundSourceAllocationRequestSimple(x)).ToList();
            Comments = comments;
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectFundingSourceNotes = projectFundingSourceNotes;
            FundingSourceIDs = projectFundingSourceUpdates.Select(x => x.FundingSourceID).ToList();
        }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch,
                                List<ProjectFundSourceAllocationRequestUpdate> currentProjectFundSourceAllocationRequestUpdates,
                                IList<ProjectFundSourceAllocationRequestUpdate> allProjectFundSourceAllocationRequestUpdates, 
                                Models.ProjectUpdate projectUpdate,
                                List<ProjectFundingSourceUpdate> currentProjectFundingSourceUpdates,
                                IList<ProjectFundingSourceUpdate> allProjectFundingSourceUpdates)
        {
            var dateNow = DateTime.Now;
            var projectFundSourceAllocationRequestUpdatesUpdated = new List<ProjectFundSourceAllocationRequestUpdate>();
            if (ProjectFundSourceAllocationRequests != null)
            {

                foreach (var projectFundSourceAllocationRequestSimple in ProjectFundSourceAllocationRequests)
                {
                    var existingCurrentOne = currentProjectFundSourceAllocationRequestUpdates.SingleOrDefault(x =>
                        x.ProjectUpdateBatchID == projectFundSourceAllocationRequestSimple.ProjectID &&
                        x.FundSourceAllocationID == projectFundSourceAllocationRequestSimple.FundSourceAllocationID);
                    if (existingCurrentOne != null)
                    {
                        var projectFundSourceAllocationRequestToAdd =
                            projectFundSourceAllocationRequestSimple.ToProjectFundSourceAllocationRequestUpdate(
                                existingCurrentOne.CreateDate, dateNow, existingCurrentOne.ImportedFromTabularData);
                        projectFundSourceAllocationRequestUpdatesUpdated.Add(projectFundSourceAllocationRequestToAdd);
                    }
                    else
                    {
                        var projectFundSourceAllocationRequestToAdd =
                            projectFundSourceAllocationRequestSimple.ToProjectFundSourceAllocationRequestUpdate(
                                dateNow, null, false);
                        projectFundSourceAllocationRequestUpdatesUpdated.Add(projectFundSourceAllocationRequestToAdd);
                    }
                }
            }

            currentProjectFundSourceAllocationRequestUpdates.Merge(projectFundSourceAllocationRequestUpdatesUpdated,
                allProjectFundSourceAllocationRequestUpdates,
                (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.FundSourceAllocationID == y.FundSourceAllocationID,
                (x, y) =>
                {
                    x.TotalAmount = y.TotalAmount;
                    x.MatchAmount = y.MatchAmount;
                    x.PayAmount = y.PayAmount;
                    x.UpdateDate = y.UpdateDate;
                    x.CreateDate = y.CreateDate;
                    x.ImportedFromTabularData = y.ImportedFromTabularData;
                });

            //Update the ProjectFundingSourceUpdates
            var projectFundingSourceUpdatesUpdated = new List<Models.ProjectFundingSourceUpdate>();
            if (FundingSourceIDs != null && FundingSourceIDs.Any())
            {
                // Completely rebuild the list
                projectFundingSourceUpdatesUpdated = FundingSourceIDs.Select(x => new ProjectFundingSourceUpdate(projectUpdateBatch.ProjectUpdateBatchID, x)).ToList();
            }

            currentProjectFundingSourceUpdates.Merge(projectFundingSourceUpdatesUpdated,
                allProjectFundingSourceUpdates,
                (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.FundingSourceID == y.FundingSourceID);

            //Update Project Update fields
            projectUpdate.ProjectFundingSourceNotes = ProjectFundingSourceNotes;
            projectUpdate.EstimatedTotalCost = ProjectEstimatedTotalCost;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProjectFundSourceAllocationRequests == null)
            {
                yield break;
            }

            //if (ProjectFundSourceAllocationRequests.GroupBy(x => x.FundSourceAllocationID).Any(x => x.Count() > 1))
            //{
            //    yield return new ValidationResult("Each FundSource Allocation can only be used once.");
            //}
        }
    }
}
