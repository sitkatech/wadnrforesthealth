using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    ///     This is similar to the class GrantAllocationJson, but this one is specifically meant to be used with the external
    ///     WADNR APIs, and so should
    ///     not be changed casually.
    /// </summary>
    [DebuggerDisplay("GrantAllocationID: {GrantAllocationID} - GrantAllocationName: {GrantAllocationName}")]
    public class GrantAllocationApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (ProgramIndexName, FederalFundCodeName, OrganizationName, etc.)
        public int GrantAllocationID { get; set; }
        public string GrantAllocationName { get; set; }
        public int GrantID { get; set; }
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
        public int? GrantManagerID { get; set; }
        public string GrantManagerName { get; set; }
        public List<int> GrantAllocationFileResourceIDs { get; set; }

        // For use by model binder
        public GrantAllocationApiJson()
        {
        }

        public GrantAllocationApiJson(GrantAllocation grantAllocation)
        {
            GrantAllocationID = grantAllocation.GrantAllocationID;
            GrantAllocationName = grantAllocation.GrantAllocationName;
            GrantID = grantAllocation.GrantID;
            StartDate = grantAllocation.StartDate;
            EndDate = grantAllocation.EndDate;
            AllocationAmount = grantAllocation.AllocationAmount;
            FederalFundCodeID = grantAllocation.FederalFundCodeID;
            FederalFundCodeName = grantAllocation.FederalFundCodeDisplay;
            OrganizationID = grantAllocation.OrganizationID;
            OrganizationName = grantAllocation.Organization?.OrganizationName;
            RegionID = grantAllocation.DNRUplandRegionID;
            RegionName = grantAllocation.RegionNameDisplay;
            DivisionID = grantAllocation.DivisionID;
            DivisionName = grantAllocation.Division != null ? grantAllocation.Division.DivisionDisplayName : null;
            GrantManagerID = grantAllocation.GrantManagerID;
            GrantManagerName = grantAllocation.GrantManager?.FullNameFirstLastAndOrgShortName;
            GrantAllocationFileResourceIDs = grantAllocation.GrantAllocationFileResources?.Select(x => x.FileResourceID).ToList();
        }

        public static List<GrantAllocationApiJson> MakeGrantAllocationApiJsonsFromGrantAllocations(
            List<GrantAllocation> grantAllocations, bool doAlphaSort = true)
        {
            var outgoingGrantAllocations = grantAllocations;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed Grant Numbers and pushing everything else to the bottom.
                outgoingGrantAllocations = GrantAllocation.OrderGrantAllocationsByYearPrefixedGrantNumbersThenEverythingElse(grantAllocations);
            }
            return outgoingGrantAllocations.Select(ga => new GrantAllocationApiJson(ga)).ToList();
        }
    }
}