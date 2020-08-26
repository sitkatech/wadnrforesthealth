/*-----------------------------------------------------------------------
<copyright file="ExpendituresViewModel.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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

        public List<ProjectGrantAllocationRequestSimple> ProjectGrantAllocationRequests { get; set; }

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

        public ExpectedFundingViewModel(List<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates,
            string comments, Money? projectEstimatedTotalCost, string projectFundingSourceNotes, List<ProjectFundingSourceUpdate> projectFundingSourceUpdates)
        {
            ProjectGrantAllocationRequests = projectGrantAllocationRequestUpdates.Select(x => new ProjectGrantAllocationRequestSimple(x)).ToList();
            Comments = comments;
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectFundingSourceNotes = projectFundingSourceNotes;
            FundingSourceIDs = projectFundingSourceUpdates.Select(x => x.FundingSourceID).ToList();
        }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch,
                                List<ProjectGrantAllocationRequestUpdate> currentProjectGrantAllocationRequestUpdates,
                                IList<ProjectGrantAllocationRequestUpdate> allProjectGrantAllocationRequestUpdates, 
                                Models.ProjectUpdate projectUpdate,
                                List<ProjectFundingSourceUpdate> currentProjectFundingSourceUpdates,
                                IList<ProjectFundingSourceUpdate> allProjectFundingSourceUpdates)
        {
            var projectGrantAllocationRequestUpdatesUpdated = new List<ProjectGrantAllocationRequestUpdate>();
            if (ProjectGrantAllocationRequests != null)
            {
                // Completely rebuild the list
                projectGrantAllocationRequestUpdatesUpdated = ProjectGrantAllocationRequests.Select(x => x.ToProjectGrantAllocationRequestUpdate()).ToList();
            }

            currentProjectGrantAllocationRequestUpdates.Merge(projectGrantAllocationRequestUpdatesUpdated,
                allProjectGrantAllocationRequestUpdates,
                (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.GrantAllocationID == y.GrantAllocationID,
                (x, y) => { x.TotalAmount = y.TotalAmount; });

            //Update the ProjectFundingSourceUpdates
            var projectFundingSourceUpdatesUpdated = new List<Models.ProjectFundingSourceUpdate>();
            if (FundingSourceIDs.Any())
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
            if (ProjectGrantAllocationRequests == null)
            {
                yield break;
            }

            if (ProjectGrantAllocationRequests.GroupBy(x => x.GrantAllocationID).Any(x => x.Count() > 1))
            {
                yield return new ValidationResult("Each Grant Allocation can only be used once.");
            }
        }
    }
}
