using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This JSON class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("GrantAllocationProgramIndexProjectCodeID: {GrantAllocationProgramIndexProjectCodeID} - GrantAllocationID: {GrantAllocationID} - ProjectCodeID: {ProjectCodeID} - ProjectCodeName: {ProjectCodeName}")]
    public class GrantAllocationProgramIndexProjectCodeApiJson
    {
        public int GrantAllocationProgramIndexProjectCodeID { get; set; }
        public int GrantAllocationID { get; set; }
        public int ProgramIndexID { get; set; }
        public string ProgramIndexCode { get; set; }
        public int? ProjectCodeID { get; set; }
        public string ProjectCodeName { get; set; }

        // For use by model binder
        public GrantAllocationProgramIndexProjectCodeApiJson()
        {
        }

        public GrantAllocationProgramIndexProjectCodeApiJson(GrantAllocationProgramIndexProjectCode grantAllocationProgramIndexProjectCode)
        {
            GrantAllocationProgramIndexProjectCodeID = grantAllocationProgramIndexProjectCode.GrantAllocationProgramIndexProjectCodeID;
            GrantAllocationID = grantAllocationProgramIndexProjectCode.GrantAllocationID;
            ProgramIndexID = grantAllocationProgramIndexProjectCode.ProgramIndexID;
            ProgramIndexCode = grantAllocationProgramIndexProjectCode.ProgramIndex.ProgramIndexCode;
            ProjectCodeID = grantAllocationProgramIndexProjectCode.ProjectCodeID;
            ProjectCodeName = grantAllocationProgramIndexProjectCode.ProjectCode?.ProjectCodeName;
        }

        public static List<GrantAllocationProgramIndexProjectCodeApiJson> MakeGrantAllocationProgramIndexProjectCodeApiJsonsFromGrantAllocationProgramIndexProjectCodes(List<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes, bool doAlphaSort = true)
        {
            var outgoingProgramIndexProjectCodes = grantAllocationProgramIndexProjectCodes;
            if (doAlphaSort)
            {
                outgoingProgramIndexProjectCodes = outgoingProgramIndexProjectCodes.OrderBy(gapipc => gapipc.GrantAllocationProgramIndexProjectCodeDisplayString).ToList();
            }
            return outgoingProgramIndexProjectCodes.Select(gapipc => new GrantAllocationProgramIndexProjectCodeApiJson(gapipc)).ToList();
        }
    }
}