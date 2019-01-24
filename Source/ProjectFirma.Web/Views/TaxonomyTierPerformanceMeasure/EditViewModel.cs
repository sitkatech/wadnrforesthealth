/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.TaxonomyTierPerformanceMeasure
{
    public class EditViewModel : FormViewModel
    {
        public int? PrimaryTaxonomyTierID { get; set; }
        public List<TaxonomyTierPerformanceMeasureSimple> TaxonomyTierPerformanceMeasures { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(List<TaxonomyTierPerformanceMeasureSimple> taxonomyTierPerformanceMeasureSimples, int? primaryTaxonomyTierID)
        {
            TaxonomyTierPerformanceMeasures = taxonomyTierPerformanceMeasureSimples;
            PrimaryTaxonomyTierID = primaryTaxonomyTierID;
        }

        public void UpdateModel(List<ProjectTypePerformanceMeasure> currentProjectTypePerformanceMeasures, IList<ProjectTypePerformanceMeasure> allProjectTypePerformanceMeasures)
        {
            var projectTypePerformanceMeasuresUpdated = new List<ProjectTypePerformanceMeasure>();
            if (TaxonomyTierPerformanceMeasures != null)
            {
                // Completely rebuild the list
                var associatePerformanceMeasureTaxonomyLevel = MultiTenantHelpers.GetAssociatePerformanceMeasureTaxonomyLevel();
                if (associatePerformanceMeasureTaxonomyLevel == TaxonomyLevel.Trunk)
                {
                    var taxonomyTrunkIDsSelected = TaxonomyTierPerformanceMeasures.Select(x => x.TaxonomyTierID).ToList();
                    var taxonomyBranchesForTrunk = HttpRequestStorage.DatabaseEntities.TaxonomyBranches
                        .Where(x => taxonomyTrunkIDsSelected.Contains(x.TaxonomyTrunkID))
                        .ToLookup(x => x.TaxonomyTrunkID);
                    projectTypePerformanceMeasuresUpdated = TaxonomyTierPerformanceMeasures.SelectMany(tt =>
                        taxonomyBranchesForTrunk[tt.TaxonomyTierID].SelectMany(tb => tb.ProjectTypes.Select(x =>
                            new ProjectTypePerformanceMeasure(x.ProjectTypeID, tt.PerformanceMeasureID,
                                tt.TaxonomyTierID == PrimaryTaxonomyTierID)))).ToList();

                }
                else if (associatePerformanceMeasureTaxonomyLevel == TaxonomyLevel.Branch)
                {
                    var taxonomyBranchIDsSelected =
                        TaxonomyTierPerformanceMeasures.Select(x => x.TaxonomyTierID).ToList();
                    var projectTypesForBranch = HttpRequestStorage.DatabaseEntities.ProjectTypes
                        .Where(x => taxonomyBranchIDsSelected.Contains(x.TaxonomyBranchID))
                        .ToLookup(x => x.TaxonomyBranchID);
                    projectTypePerformanceMeasuresUpdated = TaxonomyTierPerformanceMeasures.SelectMany(tb =>
                        projectTypesForBranch[tb.TaxonomyTierID].Select(x =>
                            new ProjectTypePerformanceMeasure(x.ProjectTypeID, tb.PerformanceMeasureID,
                                tb.TaxonomyTierID == PrimaryTaxonomyTierID))).ToList();
                }
                else
                {
                    projectTypePerformanceMeasuresUpdated = TaxonomyTierPerformanceMeasures.Select(x => new ProjectTypePerformanceMeasure(x.TaxonomyTierID, x.PerformanceMeasureID, x.TaxonomyTierID == PrimaryTaxonomyTierID)).ToList();
                }
            }
            currentProjectTypePerformanceMeasures.Merge(projectTypePerformanceMeasuresUpdated,
                allProjectTypePerformanceMeasures,
                (x, y) => x.ProjectTypeID == y.ProjectTypeID && x.PerformanceMeasureID == y.PerformanceMeasureID,
                (x, y) => x.IsPrimaryProjectType = y.IsPrimaryProjectType);
        }
    }
}
