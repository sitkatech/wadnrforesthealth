//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem]
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
        public static GrantAllocationAwardPersonnelAndBenefitsLineItem GetGrantAllocationAwardPersonnelAndBenefitsLineItem(this IQueryable<GrantAllocationAwardPersonnelAndBenefitsLineItem> grantAllocationAwardPersonnelAndBenefitsLineItems, int grantAllocationAwardPersonnelAndBenefitsLineItemID)
        {
            var grantAllocationAwardPersonnelAndBenefitsLineItem = grantAllocationAwardPersonnelAndBenefitsLineItems.SingleOrDefault(x => x.GrantAllocationAwardPersonnelAndBenefitsLineItemID == grantAllocationAwardPersonnelAndBenefitsLineItemID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardPersonnelAndBenefitsLineItem, "GrantAllocationAwardPersonnelAndBenefitsLineItem", grantAllocationAwardPersonnelAndBenefitsLineItemID);
            return grantAllocationAwardPersonnelAndBenefitsLineItem;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardPersonnelAndBenefitsLineItem(this IQueryable<GrantAllocationAwardPersonnelAndBenefitsLineItem> grantAllocationAwardPersonnelAndBenefitsLineItems, List<int> grantAllocationAwardPersonnelAndBenefitsLineItemIDList)
        {
            if(grantAllocationAwardPersonnelAndBenefitsLineItemIDList.Any())
            {
                var grantAllocationAwardPersonnelAndBenefitsLineItemsInSourceCollectionToDelete = grantAllocationAwardPersonnelAndBenefitsLineItems.Where(x => grantAllocationAwardPersonnelAndBenefitsLineItemIDList.Contains(x.GrantAllocationAwardPersonnelAndBenefitsLineItemID));
                foreach (var grantAllocationAwardPersonnelAndBenefitsLineItemToDelete in grantAllocationAwardPersonnelAndBenefitsLineItemsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardPersonnelAndBenefitsLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardPersonnelAndBenefitsLineItem(this IQueryable<GrantAllocationAwardPersonnelAndBenefitsLineItem> grantAllocationAwardPersonnelAndBenefitsLineItems, ICollection<GrantAllocationAwardPersonnelAndBenefitsLineItem> grantAllocationAwardPersonnelAndBenefitsLineItemsToDelete)
        {
            if(grantAllocationAwardPersonnelAndBenefitsLineItemsToDelete.Any())
            {
                var grantAllocationAwardPersonnelAndBenefitsLineItemIDList = grantAllocationAwardPersonnelAndBenefitsLineItemsToDelete.Select(x => x.GrantAllocationAwardPersonnelAndBenefitsLineItemID).ToList();
                var grantAllocationAwardPersonnelAndBenefitsLineItemsToDeleteFromSourceList = grantAllocationAwardPersonnelAndBenefitsLineItems.Where(x => grantAllocationAwardPersonnelAndBenefitsLineItemIDList.Contains(x.GrantAllocationAwardPersonnelAndBenefitsLineItemID)).ToList();

                foreach (var grantAllocationAwardPersonnelAndBenefitsLineItemToDelete in grantAllocationAwardPersonnelAndBenefitsLineItemsToDeleteFromSourceList)
                {
                    grantAllocationAwardPersonnelAndBenefitsLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardPersonnelAndBenefitsLineItem(this IQueryable<GrantAllocationAwardPersonnelAndBenefitsLineItem> grantAllocationAwardPersonnelAndBenefitsLineItems, int grantAllocationAwardPersonnelAndBenefitsLineItemID)
        {
            DeleteGrantAllocationAwardPersonnelAndBenefitsLineItem(grantAllocationAwardPersonnelAndBenefitsLineItems, new List<int> { grantAllocationAwardPersonnelAndBenefitsLineItemID });
        }

        public static void DeleteGrantAllocationAwardPersonnelAndBenefitsLineItem(this IQueryable<GrantAllocationAwardPersonnelAndBenefitsLineItem> grantAllocationAwardPersonnelAndBenefitsLineItems, GrantAllocationAwardPersonnelAndBenefitsLineItem grantAllocationAwardPersonnelAndBenefitsLineItemToDelete)
        {
            DeleteGrantAllocationAwardPersonnelAndBenefitsLineItem(grantAllocationAwardPersonnelAndBenefitsLineItems, new List<GrantAllocationAwardPersonnelAndBenefitsLineItem> { grantAllocationAwardPersonnelAndBenefitsLineItemToDelete });
        }
    }
}