using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("FundSourceID: {FundSourceID} - FundSourceName: {FundSourceAllocationName}")]
    public class FundSourceApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (FundSourceTypeName, FundSourceStatusTypeName, OrganizationName, etc.)
        public int FundSourceID { get; set; }
        public string FundSourceNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ConditionsAndRequirements { get; set; }
        public string ComplianceNotes { get; set; }
        public decimal? AwardedFunds { get; set; }
        public string CFDANumber { get; set; }
        public string FundSourceName { get; set; }
        public int? FundSourceTypeID { get; set; }
        public string FundSourceTypeName { get; set; }
        public string ShortName { get; set; }
        public int FundSourceStatusID { get; set; }
        public string FundSourceStatusTypeName { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public List<int> FundSourceFileResourceIDs { get; set; }

        // For use by model binder
        public FundSourceApiJson()
        {
        }

        public FundSourceApiJson(FundSource fundSource)
        {
            FundSourceID = fundSource.FundSourceID;
            FundSourceNumber = fundSource.FundSourceNumber;
            StartDate = fundSource.StartDate;
            EndDate = fundSource.EndDate;
            ConditionsAndRequirements = fundSource.ConditionsAndRequirements;
            ComplianceNotes = fundSource.ComplianceNotes;
            AwardedFunds = fundSource.TotalAwardAmount;
            CFDANumber = fundSource.CFDANumber;
            FundSourceName = fundSource.FundSourceName;
            FundSourceTypeID = fundSource.FundSourceTypeID;
            FundSourceTypeName = fundSource.FundSourceTypeDisplay;
            ShortName = fundSource.ShortName;
            FundSourceStatusID = fundSource.FundSourceStatusID;
            FundSourceStatusTypeName = fundSource.FundSourceStatus.FundSourceStatusName;
            OrganizationID = fundSource.OrganizationID;
            OrganizationName = fundSource.Organization.OrganizationName;
            FundSourceFileResourceIDs = fundSource.FundSourceFileResources?.Select(x => x.FileResourceID).ToList();
        }

        public static List<FundSourceApiJson> MakeFundSourceApiJsonsFromFundSources(List<FundSource> fundSources, bool doAlphaSort = true)
        {
            var outgoingFundSources = fundSources;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed FundSource Numbers and pushing everything else to the bottom.
                //outgoingFundSources = FundSourceAllocation.OrderFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(fundSources);
                outgoingFundSources = outgoingFundSources.OrderBy(g => g.FundSourceNumber).ToList();
            }
            return outgoingFundSources.Select(ga => new FundSourceApiJson(ga)).ToList();
        }
    }
}