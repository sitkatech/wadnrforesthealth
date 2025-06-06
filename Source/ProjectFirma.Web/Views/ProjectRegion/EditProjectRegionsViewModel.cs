﻿/*-----------------------------------------------------------------------
<copyright file="EditProjectRegionsViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectRegion
{
    public class EditProjectRegionsViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Project Regions")]
        public IEnumerable<int> DNRUplandRegionIDs { get; set; }

        [DisplayName("Notes")]
        public string NoDNRUplandRegionsExplanation { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectRegionsViewModel()
        {
        }

        public EditProjectRegionsViewModel(List<int> dnrUplandRegionIDs, string noDNRUplandRegionsExplanation)
        {
            DNRUplandRegionIDs = dnrUplandRegionIDs;
            NoDNRUplandRegionsExplanation = noDNRUplandRegionsExplanation;
        }

        public void UpdateModel(Models.Project project, List<Models.ProjectRegion> currentProjectRegions, IList<Models.ProjectRegion> allProjectRegions)
        {
            var newProjectRegions = DNRUplandRegionIDs?.Select(x => new Models.ProjectRegion(project.ProjectID, x)).ToList() ?? new List<Models.ProjectRegion>();
            currentProjectRegions.Merge(newProjectRegions, allProjectRegions, (x, y) => x.ProjectID == y.ProjectID && x.DNRUplandRegionID == y.DNRUplandRegionID);
        }

        public void UpdateModel(ProjectUpdateBatch project, List<ProjectRegionUpdate> currentProjectRegions, IList<ProjectRegionUpdate> allProjectRegions)
        {
            var newProjectRegions = DNRUplandRegionIDs?.Select(x => new ProjectRegionUpdate(project.ProjectUpdateBatchID, x)).ToList() ?? new List<ProjectRegionUpdate>();
            currentProjectRegions.Merge(newProjectRegions, allProjectRegions, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.DNRUplandRegionID == y.DNRUplandRegionID);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var errors = new List<ValidationResult>();
            var noRegionsSelected = DNRUplandRegionIDs == null || DNRUplandRegionIDs.Count().Equals(0);
            if (noRegionsSelected && string.IsNullOrWhiteSpace(NoDNRUplandRegionsExplanation))
            {
                errors.Add(
                    new SitkaValidationResult<EditProjectRegionsViewModel, string>(
                        $"Select at least one {Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel()} or provide explanatory information in the Notes section if there are no applicable {Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabelPluralized()} for this Project.",
                        x => x.NoDNRUplandRegionsExplanation));
            }

            return errors;
        }
    }
}
