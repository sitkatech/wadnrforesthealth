//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationPriority]
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
        public static GrantAllocationPriority GetGrantAllocationPriority(this IQueryable<GrantAllocationPriority> grantAllocationPriorities, int grantAllocationPriorityID)
        {
            var grantAllocationPriority = grantAllocationPriorities.SingleOrDefault(x => x.GrantAllocationPriorityID == grantAllocationPriorityID);
            Check.RequireNotNullThrowNotFound(grantAllocationPriority, "GrantAllocationPriority", grantAllocationPriorityID);
            return grantAllocationPriority;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationPriority(this IQueryable<GrantAllocationPriority> grantAllocationPriorities, List<int> grantAllocationPriorityIDList)
        {
            if(grantAllocationPriorityIDList.Any())
            {
                var grantAllocationPrioritiesInSourceCollectionToDelete = grantAllocationPriorities.Where(x => grantAllocationPriorityIDList.Contains(x.GrantAllocationPriorityID));
                foreach (var grantAllocationPriorityToDelete in grantAllocationPrioritiesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationPriorityToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationPriority(this IQueryable<GrantAllocationPriority> grantAllocationPriorities, ICollection<GrantAllocationPriority> grantAllocationPrioritiesToDelete)
        {
            if(grantAllocationPrioritiesToDelete.Any())
            {
                var grantAllocationPriorityIDList = grantAllocationPrioritiesToDelete.Select(x => x.GrantAllocationPriorityID).ToList();
                var grantAllocationPrioritiesToDeleteFromSourceList = grantAllocationPriorities.Where(x => grantAllocationPriorityIDList.Contains(x.GrantAllocationPriorityID)).ToList();

                foreach (var grantAllocationPriorityToDelete in grantAllocationPrioritiesToDeleteFromSourceList)
                {
                    grantAllocationPriorityToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationPriority(this IQueryable<GrantAllocationPriority> grantAllocationPriorities, int grantAllocationPriorityID)
        {
            DeleteGrantAllocationPriority(grantAllocationPriorities, new List<int> { grantAllocationPriorityID });
        }

        public static void DeleteGrantAllocationPriority(this IQueryable<GrantAllocationPriority> grantAllocationPriorities, GrantAllocationPriority grantAllocationPriorityToDelete)
        {
            DeleteGrantAllocationPriority(grantAllocationPriorities, new List<GrantAllocationPriority> { grantAllocationPriorityToDelete });
        }
    }
}