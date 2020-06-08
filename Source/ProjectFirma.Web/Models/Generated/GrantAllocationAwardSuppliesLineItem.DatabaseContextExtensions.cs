//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardSuppliesLineItem]
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
        public static GrantAllocationAwardSuppliesLineItem GetGrantAllocationAwardSuppliesLineItem(this IQueryable<GrantAllocationAwardSuppliesLineItem> grantAllocationAwardSuppliesLineItems, int grantAllocationAwardSuppliesLineItemID)
        {
            var grantAllocationAwardSuppliesLineItem = grantAllocationAwardSuppliesLineItems.SingleOrDefault(x => x.GrantAllocationAwardSuppliesLineItemID == grantAllocationAwardSuppliesLineItemID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardSuppliesLineItem, "GrantAllocationAwardSuppliesLineItem", grantAllocationAwardSuppliesLineItemID);
            return grantAllocationAwardSuppliesLineItem;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardSuppliesLineItem(this IQueryable<GrantAllocationAwardSuppliesLineItem> grantAllocationAwardSuppliesLineItems, List<int> grantAllocationAwardSuppliesLineItemIDList)
        {
            if(grantAllocationAwardSuppliesLineItemIDList.Any())
            {
                var grantAllocationAwardSuppliesLineItemsInSourceCollectionToDelete = grantAllocationAwardSuppliesLineItems.Where(x => grantAllocationAwardSuppliesLineItemIDList.Contains(x.GrantAllocationAwardSuppliesLineItemID));
                foreach (var grantAllocationAwardSuppliesLineItemToDelete in grantAllocationAwardSuppliesLineItemsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardSuppliesLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardSuppliesLineItem(this IQueryable<GrantAllocationAwardSuppliesLineItem> grantAllocationAwardSuppliesLineItems, ICollection<GrantAllocationAwardSuppliesLineItem> grantAllocationAwardSuppliesLineItemsToDelete)
        {
            if(grantAllocationAwardSuppliesLineItemsToDelete.Any())
            {
                var grantAllocationAwardSuppliesLineItemIDList = grantAllocationAwardSuppliesLineItemsToDelete.Select(x => x.GrantAllocationAwardSuppliesLineItemID).ToList();
                var grantAllocationAwardSuppliesLineItemsToDeleteFromSourceList = grantAllocationAwardSuppliesLineItems.Where(x => grantAllocationAwardSuppliesLineItemIDList.Contains(x.GrantAllocationAwardSuppliesLineItemID)).ToList();

                foreach (var grantAllocationAwardSuppliesLineItemToDelete in grantAllocationAwardSuppliesLineItemsToDeleteFromSourceList)
                {
                    grantAllocationAwardSuppliesLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardSuppliesLineItem(this IQueryable<GrantAllocationAwardSuppliesLineItem> grantAllocationAwardSuppliesLineItems, int grantAllocationAwardSuppliesLineItemID)
        {
            DeleteGrantAllocationAwardSuppliesLineItem(grantAllocationAwardSuppliesLineItems, new List<int> { grantAllocationAwardSuppliesLineItemID });
        }

        public static void DeleteGrantAllocationAwardSuppliesLineItem(this IQueryable<GrantAllocationAwardSuppliesLineItem> grantAllocationAwardSuppliesLineItems, GrantAllocationAwardSuppliesLineItem grantAllocationAwardSuppliesLineItemToDelete)
        {
            DeleteGrantAllocationAwardSuppliesLineItem(grantAllocationAwardSuppliesLineItems, new List<GrantAllocationAwardSuppliesLineItem> { grantAllocationAwardSuppliesLineItemToDelete });
        }
    }
}