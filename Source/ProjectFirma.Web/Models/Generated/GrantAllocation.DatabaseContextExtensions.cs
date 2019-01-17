//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocation]
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
        public static GrantAllocation GetGrantAllocation(this IQueryable<GrantAllocation> grantAllocations, int grantAllocationID)
        {
            var grantAllocation = grantAllocations.SingleOrDefault(x => x.GrantAllocationID == grantAllocationID);
            Check.RequireNotNullThrowNotFound(grantAllocation, "GrantAllocation", grantAllocationID);
            return grantAllocation;
        }

        public static void DeleteGrantAllocation(this IQueryable<GrantAllocation> grantAllocations, List<int> grantAllocationIDList)
        {
            if(grantAllocationIDList.Any())
            {
                grantAllocations.Where(x => grantAllocationIDList.Contains(x.GrantAllocationID)).Delete();
            }
        }

        public static void DeleteGrantAllocation(this IQueryable<GrantAllocation> grantAllocations, ICollection<GrantAllocation> grantAllocationsToDelete)
        {
            if(grantAllocationsToDelete.Any())
            {
                var grantAllocationIDList = grantAllocationsToDelete.Select(x => x.GrantAllocationID).ToList();
                grantAllocations.Where(x => grantAllocationIDList.Contains(x.GrantAllocationID)).Delete();
            }
        }

        public static void DeleteGrantAllocation(this IQueryable<GrantAllocation> grantAllocations, int grantAllocationID)
        {
            DeleteGrantAllocation(grantAllocations, new List<int> { grantAllocationID });
        }

        public static void DeleteGrantAllocation(this IQueryable<GrantAllocation> grantAllocations, GrantAllocation grantAllocationToDelete)
        {
            DeleteGrantAllocation(grantAllocations, new List<GrantAllocation> { grantAllocationToDelete });
        }
    }
}