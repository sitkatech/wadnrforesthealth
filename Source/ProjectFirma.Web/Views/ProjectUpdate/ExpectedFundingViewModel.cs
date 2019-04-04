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

        public List<ProjectGrantAllocationRequestSimple> ProjectFundingSourceRequests { get; set; }

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

        public ExpectedFundingViewModel(List<ProjectGrantAllocationRequestUpdate> projectFundingSourceRequestUpdates,
            string comments, Money? projectEstimatedTotalCost, Money? projectEstimatedIndirectCost, Money? projectEstimatedPersonnelAndBenefitsCost, Money? projectEstimatedSuppliesCost, Money? projectEstimatedTravelCost)
        {
            ProjectFundingSourceRequests = projectFundingSourceRequestUpdates.Select(x => new ProjectGrantAllocationRequestSimple(x)).ToList();
            Comments = comments;
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectEstimatedIndirectCost = projectEstimatedIndirectCost;
            ProjectEstimatedPersonnelAndBenefitsCost = projectEstimatedPersonnelAndBenefitsCost;
            ProjectEstimatedSuppliesCost = projectEstimatedSuppliesCost;
            ProjectEstimatedTravelCost = projectEstimatedTravelCost;
        }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch,
            List<ProjectGrantAllocationRequestUpdate> currentProjectFundingSourceRequestUpdates,
            IList<ProjectGrantAllocationRequestUpdate> allProjectFundingSourceRequestUpdates, Models.ProjectUpdate projectUpdate)
        {
            var projectFundingSourceRequestUpdatesUpdated = new List<ProjectGrantAllocationRequestUpdate>();
            if (ProjectFundingSourceRequests != null)
            {
                // Completely rebuild the list
                projectFundingSourceRequestUpdatesUpdated = ProjectFundingSourceRequests.Select(x => x.ToProjectFundingSourceRequestUpdate()).ToList();
            }

            currentProjectFundingSourceRequestUpdates.Merge(projectFundingSourceRequestUpdatesUpdated,
                allProjectFundingSourceRequestUpdates,
                (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.FundingSourceID == y.FundingSourceID,
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
            if (ProjectFundingSourceRequests == null)
            {
                yield break;
            }

            if (ProjectFundingSourceRequests.GroupBy(x => x.GrantAllocationID).Any(x => x.Count() > 1))
            {
                yield return new ValidationResult("Each Funding Source can only be used once.");
            }

            foreach (var projectFundingSourceRequest in ProjectFundingSourceRequests)
            {
                if (projectFundingSourceRequest.AreBothValuesZero())
                {
                    var fundingSource =
                        HttpRequestStorage.DatabaseEntities.FundingSources.Single(x =>
                            x.FundingSourceID == projectFundingSourceRequest.GrantAllocationID);
                    yield return new ValidationResult(
                        $"Secured Funding and Unsecured Funding cannot both be zero for funding source: {fundingSource.DisplayName}. If the amount of secured or unsecured funding is unknown, you can leave the amounts blank.");
                }
            }
        }
    }
}
