using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<ProgramIndex> GetProgramIndexFindResults(this List<ProgramIndex> programIndices, string programIndexKeyword)
        {
            var tmpProgramIndices = programIndices.ToList();
            return tmpProgramIndices.Where(x => x.ProgramIndexCode.StartsWith(programIndexKeyword))
                .OrderBy(x => x.ProgramIndexCode)
                .ToList();
        }

        public static List<ProgramIndex> GetProgramIndicesInTheCurrentBiennium(this List<ProgramIndex> programIndices)
        {
            return programIndices.Where(pi => pi.Biennium == CurrentBiennium.GetCurrentBienniumFiscalYear()).ToList();
        }

    }
}