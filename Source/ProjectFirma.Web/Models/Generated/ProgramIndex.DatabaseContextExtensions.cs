//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramIndex]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ProgramIndex GetProgramIndex(this IQueryable<ProgramIndex> programIndices, int programIndexID)
        {
            var programIndex = programIndices.SingleOrDefault(x => x.ProgramIndexID == programIndexID);
            Check.RequireNotNullThrowNotFound(programIndex, "ProgramIndex", programIndexID);
            return programIndex;
        }

        public static void DeleteProgramIndex(this IQueryable<ProgramIndex> programIndices, List<int> programIndexIDList)
        {
            if(programIndexIDList.Any())
            {
                programIndices.Where(x => programIndexIDList.Contains(x.ProgramIndexID)).Delete();
            }
        }

        public static void DeleteProgramIndex(this IQueryable<ProgramIndex> programIndices, ICollection<ProgramIndex> programIndicesToDelete)
        {
            if(programIndicesToDelete.Any())
            {
                var programIndexIDList = programIndicesToDelete.Select(x => x.ProgramIndexID).ToList();
                programIndices.Where(x => programIndexIDList.Contains(x.ProgramIndexID)).Delete();
            }
        }

        public static void DeleteProgramIndex(this IQueryable<ProgramIndex> programIndices, int programIndexID)
        {
            DeleteProgramIndex(programIndices, new List<int> { programIndexID });
        }

        public static void DeleteProgramIndex(this IQueryable<ProgramIndex> programIndices, ProgramIndex programIndexToDelete)
        {
            DeleteProgramIndex(programIndices, new List<ProgramIndex> { programIndexToDelete });
        }
    }
}