using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This JSON class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("GrantStatusID: {GrantStatusID} - GrantStatusName: {GrantStatusName}")]
    public class GrantStatusApiJson
    {
        public int GrantStatusID { get; set; }
        public string GrantStatusName { get; set; }

        // For use by model binder
        public GrantStatusApiJson()
        {
        }

        public GrantStatusApiJson(GrantStatus grantStatus)
        {
            GrantStatusID = grantStatus.GrantStatusID;
            GrantStatusName = grantStatus.GrantStatusName;
        }

        public static List<GrantStatusApiJson> MakeGrantStatusApiJsonsFromGrantStatuses(List<GrantStatus> grantStatuses)
        {
            var outgoingGrantStatuses = grantStatuses;
            return outgoingGrantStatuses.Select(gs => new GrantStatusApiJson(gs)).ToList();
        }
    }
}