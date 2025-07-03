/*-----------------------------------------------------------------------
<copyright file="EditAgreementPersonViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Diagnostics;
using LtInfo.Common.Models;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Agreement
{

    [DebuggerDisplay("GrantAllocationID: {GrantAllocationID} - GrantAllocationName: {GrantAllocationName}")]
    public class GrantAllocationJson
    {
        public int GrantAllocationID { get; set; }
        public string GrantAllocationName { get; set; }
        public string GrantNumber { get; set; }

        // For use by model binder
        public GrantAllocationJson()
        {
        }

        public GrantAllocationJson(Models.GrantAllocation grantAllocation)
        {
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantNumber = grantAllocation.Grant.GrantNumber;
            this.GrantAllocationName = grantAllocation.GrantAllocationName;
        }

        public static List<GrantAllocationJson> MakeGrantAllocationJsonsFromGrantAllocations(List<Models.GrantAllocation> grantAllocations, bool doAlphaSort = true)
        {
            var outgoingGrantAllocations = grantAllocations;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed Grant Numbers and pushing everything else to the bottom.
                outgoingGrantAllocations = Models.GrantAllocation.OrderGrantAllocationsByYearPrefixedGrantNumbersThenEverythingElse(grantAllocations);
            }
            return outgoingGrantAllocations.Select(ga => new GrantAllocationJson(ga)).ToList();
        }


    }

    public class EditAgreementGrantAllocationsViewModel : FormViewModel
    {
        public int AgreementId { get; set; }
        public List<GrantAllocationJson> GrantAllocationJsons { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementGrantAllocationsViewModel()
        {
            GrantAllocationJsons = new List<GrantAllocationJson>();
        }

        public EditAgreementGrantAllocationsViewModel(Models.Agreement agreement)
        {
            AgreementId = agreement.AgreementID;
            // Generate GrantAllocationJsons that are ordered descending by first part of GrantNumber 
            var grantAllocations = agreement.AgreementGrantAllocations.Select(x => x.GrantAllocation).ToList();
            GrantAllocationJsons = GrantAllocationJson.MakeGrantAllocationJsonsFromGrantAllocations(grantAllocations);
        }

        public void UpdateModel(Models.Agreement agreement)
        {
            // Clear out existing Agreement Grant Allocations
            agreement.AgreementGrantAllocations.ToList().ForEach(aga => aga.DeleteFull(HttpRequestStorage.DatabaseEntities));

            // Create all-new Agreement Grant Allocations
            var allPossibleGrantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            List<int> allSelectedGrantAllocationIds = GrantAllocationJsons.Select(gaj => gaj.GrantAllocationID).ToList();
            var allSelectedGrantAllocations = allPossibleGrantAllocations.Where(ga => allSelectedGrantAllocationIds.Contains(ga.GrantAllocationID)).ToList();

            agreement.AgreementGrantAllocations = allSelectedGrantAllocations.Select(ga => new AgreementGrantAllocation(agreement, ga)).ToList();
        }
    }
}