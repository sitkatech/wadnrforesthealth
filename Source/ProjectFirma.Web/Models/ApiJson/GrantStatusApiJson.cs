using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This JSON class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("FundSourceStatusID: {FundSourceStatusID} - FundSourceStatusName: {FundSourceStatusName}")]
    public class FundSourceStatusApiJson
    {
        public int FundSourceStatusID { get; set; }
        public string FundSourceStatusName { get; set; }

        // For use by model binder
        public FundSourceStatusApiJson()
        {
        }

        public FundSourceStatusApiJson(FundSourceStatus fundSourceStatus)
        {
            FundSourceStatusID = fundSourceStatus.FundSourceStatusID;
            FundSourceStatusName = fundSourceStatus.FundSourceStatusName;
        }

        public static List<FundSourceStatusApiJson> MakeFundSourceStatusApiJsonsFromFundSourceStatuses(List<FundSourceStatus> fundSourceStatuses)
        {
            var outgoingFundSourceStatuses = fundSourceStatuses;
            return outgoingFundSourceStatuses.Select(gs => new FundSourceStatusApiJson(gs)).ToList();
        }
    }
}