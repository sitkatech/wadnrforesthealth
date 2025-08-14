using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    ///     This is similar to the class FundSourceAllocationJson, but this one is specifically meant to be used with the external
    ///     WADNR APIs, and so should
    ///     not be changed casually.
    /// </summary>
    [DebuggerDisplay("FundSourceAllocationID: {FundSourceAllocationID} - FundSourceAllocationName: {FundSourceAllocationName}")]
    public class FundSourceAllocationApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (ProgramIndexName, FederalFundCodeName, OrganizationName, etc.)
        public int FundSourceAllocationID { get; set; }
        public string FundSourceAllocationName { get; set; }
        public int FundSourceID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AllocationAmount { get; set; }
        public int? FederalFundCodeID { get; set; }
        public string FederalFundCodeName { get; set; }
        public int? OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public int? DivisionID { get; set; }
        public string DivisionName { get; set; }
        public int? FundSourceManagerID { get; set; }
        public string FundSourceManagerName { get; set; }
        public List<int> FundSourceAllocationFileResourceIDs { get; set; }

        // For use by model binder
        public FundSourceAllocationApiJson()
        {
        }

        public FundSourceAllocationApiJson(FundSourceAllocation fundSourceAllocation)
        {
            FundSourceAllocationID = fundSourceAllocation.FundSourceAllocationID;
            FundSourceAllocationName = fundSourceAllocation.FundSourceAllocationName;
            FundSourceID = fundSourceAllocation.FundSourceID;
            StartDate = fundSourceAllocation.StartDate;
            EndDate = fundSourceAllocation.EndDate;
            AllocationAmount = fundSourceAllocation.AllocationAmount;
            FederalFundCodeID = fundSourceAllocation.FederalFundCodeID;
            FederalFundCodeName = fundSourceAllocation.FederalFundCodeDisplay;
            OrganizationID = fundSourceAllocation.OrganizationID;
            OrganizationName = fundSourceAllocation.Organization?.OrganizationName;
            RegionID = fundSourceAllocation.DNRUplandRegionID;
            RegionName = fundSourceAllocation.RegionNameDisplay;
            DivisionID = fundSourceAllocation.DivisionID;
            DivisionName = fundSourceAllocation.Division != null ? fundSourceAllocation.Division.DivisionDisplayName : null;
            FundSourceManagerID = fundSourceAllocation.FundSourceManagerID;
            FundSourceManagerName = fundSourceAllocation.FundSourceManager?.FullNameFirstLastAndOrgShortName;
            FundSourceAllocationFileResourceIDs = fundSourceAllocation.FundSourceAllocationFileResources?.Select(x => x.FileResourceID).ToList();
        }

        public static List<FundSourceAllocationApiJson> MakeFundSourceAllocationApiJsonsFromFundSourceAllocations(
            List<FundSourceAllocation> fundSourceAllocations, bool doAlphaSort = true)
        {
            var outgoingFundSourceAllocations = fundSourceAllocations;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed FundSource Numbers and pushing everything else to the bottom.
                outgoingFundSourceAllocations = FundSourceAllocation.OrderFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(fundSourceAllocations);
            }
            return outgoingFundSourceAllocations.Select(ga => new FundSourceAllocationApiJson(ga)).ToList();
        }
    }
}