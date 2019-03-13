/*-----------------------------------------------------------------------
<copyright file="EditProjectFundingSourceRequestsViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using Newtonsoft.Json;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProjectFundingSourceRequest
{
    public class EditProjectFundingSourceRequestsViewModel : FormViewModel, IValidatableObject
    {
        
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

        [Required]
        public bool? ForProject { get; set; }

        public List<ProjectFundingSourceRequestSimple> ProjectFundingSourceRequests { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectFundingSourceRequestsViewModel()
        {
        }

        public EditProjectFundingSourceRequestsViewModel(
            List<Models.ProjectFundingSourceRequest> projectFundingSourceRequests, bool forProject, Money? projectEstimatedTotalCost, Money? projectEstimatedIndirectCost, Money? projectEstimatedPersonnelAndBenefitsCost, Money? projectEstimatedSuppliesCost, Money? projectEstimatedTravelCost)
        {
            ProjectFundingSourceRequests = projectFundingSourceRequests
                .Select(x => new ProjectFundingSourceRequestSimple(x)).ToList();
            ProjectEstimatedTotalCost = projectEstimatedTotalCost;
            ProjectEstimatedIndirectCost = projectEstimatedIndirectCost;
            ProjectEstimatedPersonnelAndBenefitsCost = projectEstimatedPersonnelAndBenefitsCost;
            ProjectEstimatedSuppliesCost = projectEstimatedSuppliesCost;
            ProjectEstimatedTravelCost = projectEstimatedTravelCost;
            ForProject = forProject;
        }

        public void UpdateModel(List<Models.ProjectFundingSourceRequest> currentProjectFundingSourceRequests,
            IList<Models.ProjectFundingSourceRequest> allProjectFundingSourceRequests, Models.Project project)
        {
            var projectFundingSourceRequestsUpdated = new List<Models.ProjectFundingSourceRequest>();
            if (ProjectFundingSourceRequests != null)
            {
                // Completely rebuild the list
                projectFundingSourceRequestsUpdated = ProjectFundingSourceRequests
                    .Select(x => x.ToProjectFundingSourceRequest()).ToList();
            }

            if (ForProject ?? false) // never null
            {
                if (project == null)
                {
                    throw new InvalidOperationException(
                        "Project is required to update Funding Source Requests for a Project");
                }
                project.EstimatedTotalCost = ProjectEstimatedTotalCost;
            }

            currentProjectFundingSourceRequests.Merge(projectFundingSourceRequestsUpdated,
                allProjectFundingSourceRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.FundingSourceID == y.FundingSourceID,
                (x, y) =>
                {
                    x.SecuredAmount = y.SecuredAmount;
                    x.UnsecuredAmount = y.UnsecuredAmount;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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