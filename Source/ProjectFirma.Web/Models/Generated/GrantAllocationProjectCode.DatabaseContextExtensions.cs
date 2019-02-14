//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationProjectCode]
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
        public static GrantAllocationProjectCode GetGrantAllocationProjectCode(this IQueryable<GrantAllocationProjectCode> grantAllocationProjectCodes, int grantAllocationProjectCodeID)
        {
            var grantAllocationProjectCode = grantAllocationProjectCodes.SingleOrDefault(x => x.GrantAllocationProjectCodeID == grantAllocationProjectCodeID);
            Check.RequireNotNullThrowNotFound(grantAllocationProjectCode, "GrantAllocationProjectCode", grantAllocationProjectCodeID);
            return grantAllocationProjectCode;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationProjectCode(this IQueryable<GrantAllocationProjectCode> grantAllocationProjectCodes, List<int> grantAllocationProjectCodeIDList)
        {
            if(grantAllocationProjectCodeIDList.Any())
            {
                var grantAllocationProjectCodesInSourceCollectionToDelete = grantAllocationProjectCodes.Where(x => grantAllocationProjectCodeIDList.Contains(x.GrantAllocationProjectCodeID));
                foreach (var grantAllocationProjectCodeToDelete in grantAllocationProjectCodesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationProjectCode(this IQueryable<GrantAllocationProjectCode> grantAllocationProjectCodes, ICollection<GrantAllocationProjectCode> grantAllocationProjectCodesToDelete)
        {
            if(grantAllocationProjectCodesToDelete.Any())
            {
                var grantAllocationProjectCodeIDList = grantAllocationProjectCodesToDelete.Select(x => x.GrantAllocationProjectCodeID).ToList();
                var grantAllocationProjectCodesToDeleteFromSourceList = grantAllocationProjectCodes.Where(x => grantAllocationProjectCodeIDList.Contains(x.GrantAllocationProjectCodeID)).ToList();

                foreach (var grantAllocationProjectCodeToDelete in grantAllocationProjectCodesToDeleteFromSourceList)
                {
                    grantAllocationProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationProjectCode(this IQueryable<GrantAllocationProjectCode> grantAllocationProjectCodes, int grantAllocationProjectCodeID)
        {
            DeleteGrantAllocationProjectCode(grantAllocationProjectCodes, new List<int> { grantAllocationProjectCodeID });
        }

        public static void DeleteGrantAllocationProjectCode(this IQueryable<GrantAllocationProjectCode> grantAllocationProjectCodes, GrantAllocationProjectCode grantAllocationProjectCodeToDelete)
        {
            DeleteGrantAllocationProjectCode(grantAllocationProjectCodes, new List<GrantAllocationProjectCode> { grantAllocationProjectCodeToDelete });
        }
    }
}