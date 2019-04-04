/*-----------------------------------------------------------------------
<copyright file="ExpectedFundingViewModel.cs" company="Tahoe Regional Planning Agency">
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

        public ExpectedFundingViewModel(List<Models.ProjectGrantAllocationRequest> projectGrantAllocationRequests, Money? projectEstimatedTotalCost, Money? projectEstimatedIndirectCost, Money? projectEstimatedPersonnelAndBenefitsCost, Money? projectEstimatedSuppliesCost, Money? projectEstimatedTravelCost)
        {
            ProjectGrantAllocationRequestSimples = projectGrantAllocationRequests
                .Select(x => new ProjectGrantAllocationRequestSimple(x)).ToList();
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectEstimatedIndirectCost = projectEstimatedIndirectCost;
            ProjectEstimatedPersonnelAndBenefitsCost = projectEstimatedPersonnelAndBenefitsCost;
            ProjectEstimatedSuppliesCost = projectEstimatedSuppliesCost;
            ProjectEstimatedTravelCost = projectEstimatedTravelCost;
        }

        public void UpdateModel(Models.Project project,
            List<Models.ProjectGrantAllocationRequest> currentProjectGrantAllocationRequests,
            IList<Models.ProjectGrantAllocationRequest> allProjectGrantAllocationRequests)
        {
            var projectGrantAllocationRequestsUpdated = new List<Models.ProjectGrantAllocationRequest>();
            if (ProjectGrantAllocationRequestSimples != null)
            {
                // Completely rebuild the list
                projectGrantAllocationRequestsUpdated = ProjectGrantAllocationRequestSimples
                    .Select(x => x.ToProjectFundingSourceRequest()).ToList();
            }

            currentProjectGrantAllocationRequests.Merge(projectGrantAllocationRequestsUpdated,
                allProjectGrantAllocationRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.GrantAllocationID == y.GrantAllocationID,
                (x, y) =>
                {
                    x.SecuredAmount = y.SecuredAmount;
                    x.UnsecuredAmount = y.UnsecuredAmount;
                });
            project.EstimatedTotalCost = ProjectEstimatedTotalCost;
            project.EstimatedIndirectCost = ProjectEstimatedIndirectCost;
            project.EstimatedPersonnelAndBenefitsCost = ProjectEstimatedPersonnelAndBenefitsCost;
            project.EstimatedSuppliesCost = ProjectEstimatedSuppliesCost;
            project.EstimatedTravelCost = ProjectEstimatedTravelCost;
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
                yield return new ValidationResult("Each funding source can only be used once.");
            }

            foreach (var projectGrantAllocationRequestSimple in ProjectGrantAllocationRequestSimples)
            {
                if (projectGrantAllocationRequestSimple.AreBothValuesZero())
                {
                    var grantAllocation =
                        HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(x =>
                            x.GrantAllocationID == projectGrantAllocationRequestSimple.GrantAllocationID);
                    yield return new ValidationResult(
                        $"Secured Funding and Unsecured Funding cannot both be zero for Grant Allocation: {grantAllocation.DisplayName}. If the amount of secured or unsecured funding is unknown, you can leave the amounts blank.");
                }
            }
        }
    }
}