using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This JSON class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("FundSourceAllocationProgramIndexProjectCodeID: {FundSourceAllocationProgramIndexProjectCodeID} - FundSourceAllocationID: {FundSourceAllocationID} - ProjectCodeID: {ProjectCodeID} - ProjectCodeName: {ProjectCodeName}")]
    public class FundSourceAllocationProgramIndexProjectCodeApiJson
    {
        public int FundSourceAllocationProgramIndexProjectCodeID { get; set; }
        public int FundSourceAllocationID { get; set; }
        public int ProgramIndexID { get; set; }
        public string ProgramIndexCode { get; set; }
        public int? ProjectCodeID { get; set; }
        public string ProjectCodeName { get; set; }

        // For use by model binder
        public FundSourceAllocationProgramIndexProjectCodeApiJson()
        {
        }

        public FundSourceAllocationProgramIndexProjectCodeApiJson(FundSourceAllocationProgramIndexProjectCode fundSourceAllocationProgramIndexProjectCode)
        {
            FundSourceAllocationProgramIndexProjectCodeID = fundSourceAllocationProgramIndexProjectCode.FundSourceAllocationProgramIndexProjectCodeID;
            FundSourceAllocationID = fundSourceAllocationProgramIndexProjectCode.FundSourceAllocationID;
            ProgramIndexID = fundSourceAllocationProgramIndexProjectCode.ProgramIndexID;
            ProgramIndexCode = fundSourceAllocationProgramIndexProjectCode.ProgramIndex.ProgramIndexCode;
            ProjectCodeID = fundSourceAllocationProgramIndexProjectCode.ProjectCodeID;
            ProjectCodeName = fundSourceAllocationProgramIndexProjectCode.ProjectCode?.ProjectCodeName;
        }

        public static List<FundSourceAllocationProgramIndexProjectCodeApiJson> MakeFundSourceAllocationProgramIndexProjectCodeApiJsonsFromFundSourceAllocationProgramIndexProjectCodes(List<FundSourceAllocationProgramIndexProjectCode> fundSourceAllocationProgramIndexProjectCodes, bool doAlphaSort = true)
        {
            var outgoingProgramIndexProjectCodes = fundSourceAllocationProgramIndexProjectCodes;
            if (doAlphaSort)
            {
                outgoingProgramIndexProjectCodes = outgoingProgramIndexProjectCodes.OrderBy(gapipc => gapipc.FundSourceAllocationProgramIndexProjectCodeDisplayString).ToList();
            }
            return outgoingProgramIndexProjectCodes.Select(gapipc => new FundSourceAllocationProgramIndexProjectCodeApiJson(gapipc)).ToList();
        }
    }
}