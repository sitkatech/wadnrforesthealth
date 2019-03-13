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
        public List<ProjectFundingSourceRequestSimple> ProjectFundingSourceRequests { get; set; }

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

        public ExpectedFundingViewModel(List<Models.ProjectFundingSourceRequest> projectFundingSourceRequests, Money? projectEstimatedTotalCost, Money? projectEstimatedIndirectCost, Money? projectEstimatedPersonnelAndBenefitsCost, Money? projectEstimatedSuppliesCost, Money? projectEstimatedTravelCost)
        {
            ProjectFundingSourceRequests = projectFundingSourceRequests
                .Select(x => new ProjectFundingSourceRequestSimple(x)).ToList();
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectEstimatedIndirectCost = projectEstimatedIndirectCost;
            ProjectEstimatedPersonnelAndBenefitsCost = projectEstimatedPersonnelAndBenefitsCost;
            ProjectEstimatedSuppliesCost = projectEstimatedSuppliesCost;
            ProjectEstimatedTravelCost = projectEstimatedTravelCost;
        }

        public void UpdateModel(Models.Project project,
            List<Models.ProjectFundingSourceRequest> currentProjectFundingSourceRequests,
            IList<Models.ProjectFundingSourceRequest> allProjectFundingSourceRequests)
        {
            var projectFundingSourceRequestsUpdated = new List<Models.ProjectFundingSourceRequest>();
            if (ProjectFundingSourceRequests != null)
            {
                // Completely rebuild the list
                projectFundingSourceRequestsUpdated = ProjectFundingSourceRequests
                    .Select(x => x.ToProjectFundingSourceRequest()).ToList();
            }

            currentProjectFundingSourceRequests.Merge(projectFundingSourceRequestsUpdated,
                allProjectFundingSourceRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.FundingSourceID == y.FundingSourceID,
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
            if (ProjectFundingSourceRequests == null)
            {
                yield break;
            }

            if (ProjectFundingSourceRequests.GroupBy(x => x.FundingSourceID).Any(x => x.Count() > 1))
            {
                yield return new ValidationResult("Each funding source can only be used once.");
            }

            foreach (var projectFundingSourceRequest in ProjectFundingSourceRequests)
            {
                if (projectFundingSourceRequest.AreBothValuesZero())
                {
                    var fundingSource =
                        HttpRequestStorage.DatabaseEntities.FundingSources.Single(x =>
                            x.FundingSourceID == projectFundingSourceRequest.FundingSourceID);
                    yield return new ValidationResult(
                        $"Secured Funding and Unsecured Funding cannot both be zero for funding source: {fundingSource.DisplayName}. If the amount of secured or unsecured funding is unknown, you can leave the amounts blank.");
                }
            }
        }
    }
}