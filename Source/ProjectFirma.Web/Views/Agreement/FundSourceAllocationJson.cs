using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
}