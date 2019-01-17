//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationProjectCode]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteGrantAllocationProjectCode(this List<int> grantAllocationProjectCodeIDList)
        {
            if(grantAllocationProjectCodeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllGrantAllocationProjectCodes.RemoveRange(HttpRequestStorage.DatabaseEntities.GrantAllocationProjectCodes.Where(x => grantAllocationProjectCodeIDList.Contains(x.GrantAllocationProjectCodeID)));
            }
        }

        public static void DeleteGrantAllocationProjectCode(this ICollection<GrantAllocationProjectCode> grantAllocationProjectCodesToDelete)
        {
            if(grantAllocationProjectCodesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllGrantAllocationProjectCodes.RemoveRange(grantAllocationProjectCodesToDelete);
            }
        }

        public static void DeleteGrantAllocationProjectCode(this int grantAllocationProjectCodeID)
        {
            DeleteGrantAllocationProjectCode(new List<int> { grantAllocationProjectCodeID });
        }

        public static void DeleteGrantAllocationProjectCode(this GrantAllocationProjectCode grantAllocationProjectCodeToDelete)
        {
            DeleteGrantAllocationProjectCode(new List<GrantAllocationProjectCode> { grantAllocationProjectCodeToDelete });
        }
    }
}