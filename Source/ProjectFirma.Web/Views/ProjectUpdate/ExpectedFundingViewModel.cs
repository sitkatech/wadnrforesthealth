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

        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedIndirectCost)]
        [JsonIgnore]
        public Money? ProjectEstimatedIndirectCost { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedPersonnelAndBenefitsCost)]
        [JsonIgnore]
        public Money? ProjectEstimatedPersonnelAndBenefitsCost { get; set; }


        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedSuppliesCost)]
        [JsonIgnore]
        public Money? ProjectEstimatedSuppliesCost { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedTravelCost)]
        [JsonIgnore]
        public Money? ProjectEstimatedTravelCost { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ExpectedFundingViewModel()
        {
            
        }

        public ExpectedFundingViewModel(List<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates,
            string comments, Money? projectEstimatedTotalCost, Money? projectEstimatedIndirectCost, Money? projectEstimatedPersonnelAndBenefitsCost, Money? projectEstimatedSuppliesCost, Money? projectEstimatedTravelCost)
        {
            ProjectGrantAllocationRequests = projectGrantAllocationRequestUpdates.Select(x => new ProjectGrantAllocationRequestSimple(x)).ToList();
            Comments = comments;
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectEstimatedIndirectCost = projectEstimatedIndirectCost;
            ProjectEstimatedPersonnelAndBenefitsCost = projectEstimatedPersonnelAndBenefitsCost;
            ProjectEstimatedSuppliesCost = projectEstimatedSuppliesCost;
            ProjectEstimatedTravelCost = projectEstimatedTravelCost;
        }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch,
            List<ProjectGrantAllocationRequestUpdate> currentProjectGrantAllocationRequestUpdates,
            IList<ProjectGrantAllocationRequestUpdate> allProjectGrantAllocationRequestUpdates, Models.ProjectUpdate projectUpdate)
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
                (x, y) =>
                {
                    x.SecuredAmount = y.SecuredAmount;
                    x.UnsecuredAmount = y.UnsecuredAmount;
                });

            projectUpdate.EstimatedTotalCost = ProjectEstimatedTotalCost;
            projectUpdate.EstimatedIndirectCost = ProjectEstimatedIndirectCost;
            projectUpdate.EstimatedPersonnelAndBenefitsCost = ProjectEstimatedPersonnelAndBenefitsCost;
            projectUpdate.EstimatedSuppliesCost = ProjectEstimatedSuppliesCost;
            projectUpdate.EstimatedTravelCost = ProjectEstimatedTravelCost;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProjectGrantAllocationRequests == null)
            {
                yield break;
            }

            if (ProjectGrantAllocationRequests.GroupBy(x => x.GrantAllocationID).Any(x => x.Count() > 1))
            {
                yield return new ValidationResult("Each Funding Source can only be used once.");
            }

            foreach (var projectGrantAllocationRequest in ProjectGrantAllocationRequests)
            {
                if (projectGrantAllocationRequest.AreBothValuesZero())
                {
                    var grantAllocation =
                        HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(x =>
                            x.GrantAllocationID == projectGrantAllocationRequest.GrantAllocationID);
                    yield return new ValidationResult(
                        $"Secured Funding and Unsecured Funding cannot both be zero for Grant Allocation: {grantAllocation.DisplayName}. If the amount of secured or unsecured funding is unknown, you can leave the amounts blank.");
                }
            }
        }
    }
}
