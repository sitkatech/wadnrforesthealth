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

    [DebuggerDisplay("FundSourceAllocationID: {FundSourceAllocationID} - FundSourceAllocationName: {FundSourceAllocationName}")]
    public class FundSourceAllocationJson
    {
        public int FundSourceAllocationID { get; set; }
        public string FundSourceAllocationName { get; set; }
        public string FundSourceNumber { get; set; }

        // For use by model binder
        public FundSourceAllocationJson()
        {
        }

        public FundSourceAllocationJson(Models.FundSourceAllocation fundSourceAllocation)
        {
            this.FundSourceAllocationID = fundSourceAllocation.FundSourceAllocationID;
            this.FundSourceNumber = fundSourceAllocation.FundSource.FundSourceNumber;
            this.FundSourceAllocationName = fundSourceAllocation.FundSourceAllocationName;
        }

        public static List<FundSourceAllocationJson> MakeFundSourceAllocationJsonsFromFundSourceAllocations(List<Models.FundSourceAllocation> fundSourceAllocations, bool doAlphaSort = true)
        {
            var outgoingFundSourceAllocations = fundSourceAllocations;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed FundSource Numbers and pushing everything else to the bottom.
                outgoingFundSourceAllocations = Models.FundSourceAllocation.OrderFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(fundSourceAllocations);
            }
            return outgoingFundSourceAllocations.Select(ga => new FundSourceAllocationJson(ga)).ToList();
        }


    }

    public class EditAgreementFundSourceAllocationsViewModel : FormViewModel
    {
        public int AgreementId { get; set; }
        public List<FundSourceAllocationJson> FundSourceAllocationJsons { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementFundSourceAllocationsViewModel()
        {
            FundSourceAllocationJsons = new List<FundSourceAllocationJson>();
        }

        public EditAgreementFundSourceAllocationsViewModel(Models.Agreement agreement)
        {
            AgreementId = agreement.AgreementID;
            // Generate FundSourceAllocationJsons that are ordered descending by first part of FundSourceNumber 
            var fundSourceAllocations = agreement.AgreementFundSourceAllocations.Select(x => x.FundSourceAllocation).ToList();
            FundSourceAllocationJsons = FundSourceAllocationJson.MakeFundSourceAllocationJsonsFromFundSourceAllocations(fundSourceAllocations);
        }

        public void UpdateModel(Models.Agreement agreement)
        {
            // Clear out existing Agreement FundSource Allocations
            agreement.AgreementFundSourceAllocations.ToList().ForEach(aga => aga.DeleteFull(HttpRequestStorage.DatabaseEntities));

            // Create all-new Agreement FundSource Allocations
            var allPossibleFundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.ToList();
            List<int> allSelectedFundSourceAllocationIds = FundSourceAllocationJsons.Select(gaj => gaj.FundSourceAllocationID).ToList();
            var allSelectedFundSourceAllocations = allPossibleFundSourceAllocations.Where(ga => allSelectedFundSourceAllocationIds.Contains(ga.FundSourceAllocationID)).ToList();

            agreement.AgreementFundSourceAllocations = allSelectedFundSourceAllocations.Select(ga => new AgreementFundSourceAllocation(agreement, ga)).ToList();
        }
    }
}