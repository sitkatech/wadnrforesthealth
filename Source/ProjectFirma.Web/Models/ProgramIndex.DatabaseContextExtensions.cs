using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<ProgramIndex> GetProgramIndexFindResults(this IQueryable<ProgramIndex> programIndices, string programIndexKeyword)
        {
            return programIndices.Where(x => x.ProgramIndexCode.StartsWith(programIndexKeyword))
                .OrderBy(x => x.ProgramIndexCode)
                .ToList();
        }

        public static IQueryable<ProgramIndex> GetProgramIndicesWithoutHistoricRecords(this IQueryable<ProgramIndex> programIndices)
        {
            throw new Exception("Need to port IsHistoric use here to Biennium, which is now in the table. TODO.");
            //return programIndices.Where(x => x.IsHistoric != true);
        }

    }
}