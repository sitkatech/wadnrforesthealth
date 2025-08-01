//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocation]
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
        public static FundSourceAllocation GetGrantAllocation(this IQueryable<FundSourceAllocation> grantAllocations, int grantAllocationID)
        {
            var grantAllocation = grantAllocations.SingleOrDefault(x => x.GrantAllocationID == grantAllocationID);
            Check.RequireNotNullThrowNotFound(grantAllocation, "GrantAllocation", grantAllocationID);
            return grantAllocation;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocation(this IQueryable<FundSourceAllocation> grantAllocations, List<int> grantAllocationIDList)
        {
            if(grantAllocationIDList.Any())
            {
                var grantAllocationsInSourceCollectionToDelete = grantAllocations.Where(x => grantAllocationIDList.Contains(x.GrantAllocationID));
                foreach (var grantAllocationToDelete in grantAllocationsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocation(this IQueryable<FundSourceAllocation> grantAllocations, ICollection<FundSourceAllocation> grantAllocationsToDelete)
        {
            if(grantAllocationsToDelete.Any())
            {
                var grantAllocationIDList = grantAllocationsToDelete.Select(x => x.GrantAllocationID).ToList();
                var grantAllocationsToDeleteFromSourceList = grantAllocations.Where(x => grantAllocationIDList.Contains(x.GrantAllocationID)).ToList();

                foreach (var grantAllocationToDelete in grantAllocationsToDeleteFromSourceList)
                {
                    grantAllocationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocation(this IQueryable<FundSourceAllocation> grantAllocations, int grantAllocationID)
        {
            DeleteGrantAllocation(grantAllocations, new List<int> { grantAllocationID });
        }

        public static void DeleteGrantAllocation(this IQueryable<FundSourceAllocation> grantAllocations, FundSourceAllocation fundSourceAllocationToDelete)
        {
            DeleteGrantAllocation(grantAllocations, new List<FundSourceAllocation> { fundSourceAllocationToDelete });
        }
    }
}