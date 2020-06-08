//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardLandownerCostShareLineItem]
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
        public static GrantAllocationAwardLandownerCostShareLineItem GetGrantAllocationAwardLandownerCostShareLineItem(this IQueryable<GrantAllocationAwardLandownerCostShareLineItem> grantAllocationAwardLandownerCostShareLineItems, int grantAllocationAwardLandownerCostShareLineItemID)
        {
            var grantAllocationAwardLandownerCostShareLineItem = grantAllocationAwardLandownerCostShareLineItems.SingleOrDefault(x => x.GrantAllocationAwardLandownerCostShareLineItemID == grantAllocationAwardLandownerCostShareLineItemID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardLandownerCostShareLineItem, "GrantAllocationAwardLandownerCostShareLineItem", grantAllocationAwardLandownerCostShareLineItemID);
            return grantAllocationAwardLandownerCostShareLineItem;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardLandownerCostShareLineItem(this IQueryable<GrantAllocationAwardLandownerCostShareLineItem> grantAllocationAwardLandownerCostShareLineItems, List<int> grantAllocationAwardLandownerCostShareLineItemIDList)
        {
            if(grantAllocationAwardLandownerCostShareLineItemIDList.Any())
            {
                var grantAllocationAwardLandownerCostShareLineItemsInSourceCollectionToDelete = grantAllocationAwardLandownerCostShareLineItems.Where(x => grantAllocationAwardLandownerCostShareLineItemIDList.Contains(x.GrantAllocationAwardLandownerCostShareLineItemID));
                foreach (var grantAllocationAwardLandownerCostShareLineItemToDelete in grantAllocationAwardLandownerCostShareLineItemsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardLandownerCostShareLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardLandownerCostShareLineItem(this IQueryable<GrantAllocationAwardLandownerCostShareLineItem> grantAllocationAwardLandownerCostShareLineItems, ICollection<GrantAllocationAwardLandownerCostShareLineItem> grantAllocationAwardLandownerCostShareLineItemsToDelete)
        {
            if(grantAllocationAwardLandownerCostShareLineItemsToDelete.Any())
            {
                var grantAllocationAwardLandownerCostShareLineItemIDList = grantAllocationAwardLandownerCostShareLineItemsToDelete.Select(x => x.GrantAllocationAwardLandownerCostShareLineItemID).ToList();
                var grantAllocationAwardLandownerCostShareLineItemsToDeleteFromSourceList = grantAllocationAwardLandownerCostShareLineItems.Where(x => grantAllocationAwardLandownerCostShareLineItemIDList.Contains(x.GrantAllocationAwardLandownerCostShareLineItemID)).ToList();

                foreach (var grantAllocationAwardLandownerCostShareLineItemToDelete in grantAllocationAwardLandownerCostShareLineItemsToDeleteFromSourceList)
                {
                    grantAllocationAwardLandownerCostShareLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardLandownerCostShareLineItem(this IQueryable<GrantAllocationAwardLandownerCostShareLineItem> grantAllocationAwardLandownerCostShareLineItems, int grantAllocationAwardLandownerCostShareLineItemID)
        {
            DeleteGrantAllocationAwardLandownerCostShareLineItem(grantAllocationAwardLandownerCostShareLineItems, new List<int> { grantAllocationAwardLandownerCostShareLineItemID });
        }

        public static void DeleteGrantAllocationAwardLandownerCostShareLineItem(this IQueryable<GrantAllocationAwardLandownerCostShareLineItem> grantAllocationAwardLandownerCostShareLineItems, GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItemToDelete)
        {
            DeleteGrantAllocationAwardLandownerCostShareLineItem(grantAllocationAwardLandownerCostShareLineItems, new List<GrantAllocationAwardLandownerCostShareLineItem> { grantAllocationAwardLandownerCostShareLineItemToDelete });
        }
    }
}