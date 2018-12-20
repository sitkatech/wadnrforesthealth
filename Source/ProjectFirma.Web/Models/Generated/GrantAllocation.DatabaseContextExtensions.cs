//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocation]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static GrantAllocation GetGrantAllocation(this IQueryable<GrantAllocation> grantAllocations, int grantAllocationID)
        {
            var grantAllocation = grantAllocations.SingleOrDefault(x => x.GrantAllocationID == grantAllocationID);
            Check.RequireNotNullThrowNotFound(grantAllocation, "GrantAllocation", grantAllocationID);
            return grantAllocation;
        }

        public static void DeleteGrantAllocation(this List<int> grantAllocationIDList)
        {
            if(grantAllocationIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllGrantAllocations.RemoveRange(HttpRequestStorage.DatabaseEntities.GrantAllocations.Where(x => grantAllocationIDList.Contains(x.GrantAllocationID)));
            }
        }

        public static void DeleteGrantAllocation(this ICollection<GrantAllocation> grantAllocationsToDelete)
        {
            if(grantAllocationsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllGrantAllocations.RemoveRange(grantAllocationsToDelete);
            }
        }

        public static void DeleteGrantAllocation(this int grantAllocationID)
        {
            DeleteGrantAllocation(new List<int> { grantAllocationID });
        }

        public static void DeleteGrantAllocation(this GrantAllocation grantAllocationToDelete)
        {
            DeleteGrantAllocation(new List<GrantAllocation> { grantAllocationToDelete });
        }
    }
}