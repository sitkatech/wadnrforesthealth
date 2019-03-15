using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<ProgramIndex> GetProgramIndexFindResults(this IQueryable<ProgramIndex> programIndices, string programIndexKeyword)
        {
            return programIndices.Where(x => x.ProgramIndexAbbrev.StartsWith(programIndexKeyword))
                .OrderBy(x => x.ProgramIndexAbbrev)
                .ToList();
        }

        public static IQueryable<ProgramIndex> GetProgramIndicesWithoutHistoricRecords(this IQueryable<ProgramIndex> programIndices)
        {
            return programIndices.Where(x => x.IsHistoric != true);
        }

    }
}