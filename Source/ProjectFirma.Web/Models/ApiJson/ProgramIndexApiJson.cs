using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This JSON class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("ProgramIndexID: {ProgramIndexID} - ProgramIndexCode: {ProgramIndexCode}")]
    public class ProgramIndexApiJson
    {
        public int ProgramIndexID { get; set; }
        public string ProgramIndexCode { get; set; }
        public string ProgramIndexTitle { get; set; }
        public int Biennium { get; set; }
        public string Activity { get; set; }
        public string Program { get; set; }
        public string Subprogram { get; set; }
        public string Subactivity { get; set; }

        // For use by model binder
        public ProgramIndexApiJson()
        {
        }

        public ProgramIndexApiJson(ProgramIndex programIndex)
        {
            ProgramIndexID = programIndex.ProgramIndexID;
            ProgramIndexCode = programIndex.ProgramIndexCode;
            ProgramIndexTitle = programIndex.ProgramIndexTitle;
            Biennium = programIndex.Biennium;
            Activity = programIndex.Activity;
            Program = programIndex.Program;
            Subprogram = programIndex.Subprogram;
            Subactivity = programIndex.Subactivity;
        }

        public static List<ProgramIndexApiJson> MakeProgramIndexApiJsonsFromProgramIndexes(List<ProgramIndex> programIndexes, bool doAlphaSort = true)
        {
            var outgoingProgramIndexes = programIndexes;
            if (doAlphaSort)
            {
                // This sort order is semi-important; we are highlighting properly constructed, year prefixed FundSource Numbers and pushing everything else to the bottom.
                //outgoingProgramIndexes = FundSourceAllocation.OrderFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(programIndexes);
                outgoingProgramIndexes = outgoingProgramIndexes.OrderBy(pi => pi.ProgramIndexCode).ToList();
            }
            return outgoingProgramIndexes.Select(pi => new ProgramIndexApiJson(pi)).ToList();
        }
    }
}