//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramIndexProjectCode]
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
        public static ProgramIndexProjectCode GetProgramIndexProjectCode(this IQueryable<ProgramIndexProjectCode> programIndexProjectCodes, int programIndexProjectCodeID)
        {
            var programIndexProjectCode = programIndexProjectCodes.SingleOrDefault(x => x.ProgramIndexProjectCodeID == programIndexProjectCodeID);
            Check.RequireNotNullThrowNotFound(programIndexProjectCode, "ProgramIndexProjectCode", programIndexProjectCodeID);
            return programIndexProjectCode;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProgramIndexProjectCode(this IQueryable<ProgramIndexProjectCode> programIndexProjectCodes, List<int> programIndexProjectCodeIDList)
        {
            if(programIndexProjectCodeIDList.Any())
            {
                var programIndexProjectCodesInSourceCollectionToDelete = programIndexProjectCodes.Where(x => programIndexProjectCodeIDList.Contains(x.ProgramIndexProjectCodeID));
                foreach (var programIndexProjectCodeToDelete in programIndexProjectCodesInSourceCollectionToDelete.ToList())
                {
                    programIndexProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProgramIndexProjectCode(this IQueryable<ProgramIndexProjectCode> programIndexProjectCodes, ICollection<ProgramIndexProjectCode> programIndexProjectCodesToDelete)
        {
            if(programIndexProjectCodesToDelete.Any())
            {
                var programIndexProjectCodeIDList = programIndexProjectCodesToDelete.Select(x => x.ProgramIndexProjectCodeID).ToList();
                var programIndexProjectCodesToDeleteFromSourceList = programIndexProjectCodes.Where(x => programIndexProjectCodeIDList.Contains(x.ProgramIndexProjectCodeID)).ToList();

                foreach (var programIndexProjectCodeToDelete in programIndexProjectCodesToDeleteFromSourceList)
                {
                    programIndexProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProgramIndexProjectCode(this IQueryable<ProgramIndexProjectCode> programIndexProjectCodes, int programIndexProjectCodeID)
        {
            DeleteProgramIndexProjectCode(programIndexProjectCodes, new List<int> { programIndexProjectCodeID });
        }

        public static void DeleteProgramIndexProjectCode(this IQueryable<ProgramIndexProjectCode> programIndexProjectCodes, ProgramIndexProjectCode programIndexProjectCodeToDelete)
        {
            DeleteProgramIndexProjectCode(programIndexProjectCodes, new List<ProgramIndexProjectCode> { programIndexProjectCodeToDelete });
        }
    }
}