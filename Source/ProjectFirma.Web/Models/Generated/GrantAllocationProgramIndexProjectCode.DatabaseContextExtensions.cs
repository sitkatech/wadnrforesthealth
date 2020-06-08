//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationProgramIndexProjectCode]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static GrantAllocationProgramIndexProjectCode GetGrantAllocationProgramIndexProjectCode(this IQueryable<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes, int grantAllocationProgramIndexProjectCodeID)
        {
            var grantAllocationProgramIndexProjectCode = grantAllocationProgramIndexProjectCodes.SingleOrDefault(x => x.GrantAllocationProgramIndexProjectCodeID == grantAllocationProgramIndexProjectCodeID);
            Check.RequireNotNullThrowNotFound(grantAllocationProgramIndexProjectCode, "GrantAllocationProgramIndexProjectCode", grantAllocationProgramIndexProjectCodeID);
            return grantAllocationProgramIndexProjectCode;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationProgramIndexProjectCode(this IQueryable<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes, List<int> grantAllocationProgramIndexProjectCodeIDList)
        {
            if(grantAllocationProgramIndexProjectCodeIDList.Any())
            {
                var grantAllocationProgramIndexProjectCodesInSourceCollectionToDelete = grantAllocationProgramIndexProjectCodes.Where(x => grantAllocationProgramIndexProjectCodeIDList.Contains(x.GrantAllocationProgramIndexProjectCodeID));
                foreach (var grantAllocationProgramIndexProjectCodeToDelete in grantAllocationProgramIndexProjectCodesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationProgramIndexProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationProgramIndexProjectCode(this IQueryable<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes, ICollection<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodesToDelete)
        {
            if(grantAllocationProgramIndexProjectCodesToDelete.Any())
            {
                var grantAllocationProgramIndexProjectCodeIDList = grantAllocationProgramIndexProjectCodesToDelete.Select(x => x.GrantAllocationProgramIndexProjectCodeID).ToList();
                var grantAllocationProgramIndexProjectCodesToDeleteFromSourceList = grantAllocationProgramIndexProjectCodes.Where(x => grantAllocationProgramIndexProjectCodeIDList.Contains(x.GrantAllocationProgramIndexProjectCodeID)).ToList();

                foreach (var grantAllocationProgramIndexProjectCodeToDelete in grantAllocationProgramIndexProjectCodesToDeleteFromSourceList)
                {
                    grantAllocationProgramIndexProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationProgramIndexProjectCode(this IQueryable<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes, int grantAllocationProgramIndexProjectCodeID)
        {
            DeleteGrantAllocationProgramIndexProjectCode(grantAllocationProgramIndexProjectCodes, new List<int> { grantAllocationProgramIndexProjectCodeID });
        }

        public static void DeleteGrantAllocationProgramIndexProjectCode(this IQueryable<GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes, GrantAllocationProgramIndexProjectCode grantAllocationProgramIndexProjectCodeToDelete)
        {
            DeleteGrantAllocationProgramIndexProjectCode(grantAllocationProgramIndexProjectCodes, new List<GrantAllocationProgramIndexProjectCode> { grantAllocationProgramIndexProjectCodeToDelete });
        }
    }
}