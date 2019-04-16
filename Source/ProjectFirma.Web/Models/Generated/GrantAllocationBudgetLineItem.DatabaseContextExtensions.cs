//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationBudgetLineItem]
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
        public static GrantAllocationBudgetLineItem GetGrantAllocationBudgetLineItem(this IQueryable<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItems, int grantAllocationBudgetLineItemID)
        {
            var grantAllocationBudgetLineItem = grantAllocationBudgetLineItems.SingleOrDefault(x => x.GrantAllocationBudgetLineItemID == grantAllocationBudgetLineItemID);
            Check.RequireNotNullThrowNotFound(grantAllocationBudgetLineItem, "GrantAllocationBudgetLineItem", grantAllocationBudgetLineItemID);
            return grantAllocationBudgetLineItem;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationBudgetLineItem(this IQueryable<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItems, List<int> grantAllocationBudgetLineItemIDList)
        {
            if(grantAllocationBudgetLineItemIDList.Any())
            {
                var grantAllocationBudgetLineItemsInSourceCollectionToDelete = grantAllocationBudgetLineItems.Where(x => grantAllocationBudgetLineItemIDList.Contains(x.GrantAllocationBudgetLineItemID));
                foreach (var grantAllocationBudgetLineItemToDelete in grantAllocationBudgetLineItemsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationBudgetLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationBudgetLineItem(this IQueryable<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItems, ICollection<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItemsToDelete)
        {
            if(grantAllocationBudgetLineItemsToDelete.Any())
            {
                var grantAllocationBudgetLineItemIDList = grantAllocationBudgetLineItemsToDelete.Select(x => x.GrantAllocationBudgetLineItemID).ToList();
                var grantAllocationBudgetLineItemsToDeleteFromSourceList = grantAllocationBudgetLineItems.Where(x => grantAllocationBudgetLineItemIDList.Contains(x.GrantAllocationBudgetLineItemID)).ToList();

                foreach (var grantAllocationBudgetLineItemToDelete in grantAllocationBudgetLineItemsToDeleteFromSourceList)
                {
                    grantAllocationBudgetLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationBudgetLineItem(this IQueryable<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItems, int grantAllocationBudgetLineItemID)
        {
            DeleteGrantAllocationBudgetLineItem(grantAllocationBudgetLineItems, new List<int> { grantAllocationBudgetLineItemID });
        }

        public static void DeleteGrantAllocationBudgetLineItem(this IQueryable<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItems, GrantAllocationBudgetLineItem grantAllocationBudgetLineItemToDelete)
        {
            DeleteGrantAllocationBudgetLineItem(grantAllocationBudgetLineItems, new List<GrantAllocationBudgetLineItem> { grantAllocationBudgetLineItemToDelete });
        }
    }
}