using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("GrantID: {GrantID} - GrantName: {GrantAllocationName}")]
    public class GrantApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (GrantTypeName, GrantStatusTypeName, OrganizationName, etc.)
        public int GrantID { get; set; }
        public string GrantNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ConditionsAndRequirements { get; set; }
        public string ComplianceNotes { get; set; }
        public decimal? AwardedFunds { get; set; }
        public string CFDANumber { get; set; }
        public string GrantName { get; set; }
        public int? GrantTypeID { get; set; }
        public string GrantTypeName { get; set; }
        public string ShortName { get; set; }
        public int GrantStatusID { get; set; }
        public string GrantStatusTypeName { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public List<int> GrantFileResourceIDs { get; set; }

        // For use by model binder
        public GrantApiJson()
        {
        }

        public GrantApiJson(Grant grant)
        {
            GrantID = grant.GrantID;
            GrantNumber = grant.GrantNumber;
            StartDate = grant.StartDate;
            EndDate = grant.EndDate;
            ConditionsAndRequirements = grant.ConditionsAndRequirements;
            ComplianceNotes = grant.ComplianceNotes;
            AwardedFunds = grant.TotalAwardAmount;
            CFDANumber = grant.CFDANumber;
            GrantName = grant.GrantName;
            GrantTypeID = grant.GrantTypeID;
            GrantTypeName = grant.GrantTypeDisplay;
            ShortName = grant.ShortName;
            GrantStatusID = grant.GrantStatusID;
            GrantStatusTypeName = grant.GrantStatus.GrantStatusName;
            OrganizationID = grant.OrganizationID;
            OrganizationName = grant.Organization.OrganizationName;
            GrantFileResourceIDs = grant.GrantFileResources?.Select(x => x.FileResourceID).ToList();
        }

        public static List<GrantApiJson> MakeGrantApiJsonsFromGrants(List<Grant> grants, bool doAlphaSort = true)
        {
            var outgoingGrants = grants;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed Grant Numbers and pushing everything else to the bottom.
                //outgoingGrants = GrantAllocation.OrderGrantAllocationsByYearPrefixedGrantNumbersThenEverythingElse(grants);
                outgoingGrants = outgoingGrants.OrderBy(g => g.GrantNumber).ToList();
            }
            return outgoingGrants.Select(ga => new GrantApiJson(ga)).ToList();
        }
    }
}