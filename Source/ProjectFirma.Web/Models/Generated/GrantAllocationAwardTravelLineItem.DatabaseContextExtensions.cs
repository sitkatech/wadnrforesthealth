//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardTravelLineItem]
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
        public static GrantAllocationAwardTravelLineItem GetGrantAllocationAwardTravelLineItem(this IQueryable<GrantAllocationAwardTravelLineItem> grantAllocationAwardTravelLineItems, int grantAllocationAwardTravelLineItemID)
        {
            var grantAllocationAwardTravelLineItem = grantAllocationAwardTravelLineItems.SingleOrDefault(x => x.GrantAllocationAwardTravelLineItemID == grantAllocationAwardTravelLineItemID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardTravelLineItem, "GrantAllocationAwardTravelLineItem", grantAllocationAwardTravelLineItemID);
            return grantAllocationAwardTravelLineItem;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardTravelLineItem(this IQueryable<GrantAllocationAwardTravelLineItem> grantAllocationAwardTravelLineItems, List<int> grantAllocationAwardTravelLineItemIDList)
        {
            if(grantAllocationAwardTravelLineItemIDList.Any())
            {
                var grantAllocationAwardTravelLineItemsInSourceCollectionToDelete = grantAllocationAwardTravelLineItems.Where(x => grantAllocationAwardTravelLineItemIDList.Contains(x.GrantAllocationAwardTravelLineItemID));
                foreach (var grantAllocationAwardTravelLineItemToDelete in grantAllocationAwardTravelLineItemsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardTravelLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardTravelLineItem(this IQueryable<GrantAllocationAwardTravelLineItem> grantAllocationAwardTravelLineItems, ICollection<GrantAllocationAwardTravelLineItem> grantAllocationAwardTravelLineItemsToDelete)
        {
            if(grantAllocationAwardTravelLineItemsToDelete.Any())
            {
                var grantAllocationAwardTravelLineItemIDList = grantAllocationAwardTravelLineItemsToDelete.Select(x => x.GrantAllocationAwardTravelLineItemID).ToList();
                var grantAllocationAwardTravelLineItemsToDeleteFromSourceList = grantAllocationAwardTravelLineItems.Where(x => grantAllocationAwardTravelLineItemIDList.Contains(x.GrantAllocationAwardTravelLineItemID)).ToList();

                foreach (var grantAllocationAwardTravelLineItemToDelete in grantAllocationAwardTravelLineItemsToDeleteFromSourceList)
                {
                    grantAllocationAwardTravelLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardTravelLineItem(this IQueryable<GrantAllocationAwardTravelLineItem> grantAllocationAwardTravelLineItems, int grantAllocationAwardTravelLineItemID)
        {
            DeleteGrantAllocationAwardTravelLineItem(grantAllocationAwardTravelLineItems, new List<int> { grantAllocationAwardTravelLineItemID });
        }

        public static void DeleteGrantAllocationAwardTravelLineItem(this IQueryable<GrantAllocationAwardTravelLineItem> grantAllocationAwardTravelLineItems, GrantAllocationAwardTravelLineItem grantAllocationAwardTravelLineItemToDelete)
        {
            DeleteGrantAllocationAwardTravelLineItem(grantAllocationAwardTravelLineItems, new List<GrantAllocationAwardTravelLineItem> { grantAllocationAwardTravelLineItemToDelete });
        }
    }
}