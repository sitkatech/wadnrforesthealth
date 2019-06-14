//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardTravelLineItemType]
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
        public static GrantAllocationAwardTravelLineItemType GetGrantAllocationAwardTravelLineItemType(this IQueryable<GrantAllocationAwardTravelLineItemType> grantAllocationAwardTravelLineItemTypes, int grantAllocationAwardTravelLineItemTypeID)
        {
            var grantAllocationAwardTravelLineItemType = grantAllocationAwardTravelLineItemTypes.SingleOrDefault(x => x.GrantAllocationAwardTravelLineItemTypeID == grantAllocationAwardTravelLineItemTypeID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardTravelLineItemType, "GrantAllocationAwardTravelLineItemType", grantAllocationAwardTravelLineItemTypeID);
            return grantAllocationAwardTravelLineItemType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardTravelLineItemType(this IQueryable<GrantAllocationAwardTravelLineItemType> grantAllocationAwardTravelLineItemTypes, List<int> grantAllocationAwardTravelLineItemTypeIDList)
        {
            if(grantAllocationAwardTravelLineItemTypeIDList.Any())
            {
                var grantAllocationAwardTravelLineItemTypesInSourceCollectionToDelete = grantAllocationAwardTravelLineItemTypes.Where(x => grantAllocationAwardTravelLineItemTypeIDList.Contains(x.GrantAllocationAwardTravelLineItemTypeID));
                foreach (var grantAllocationAwardTravelLineItemTypeToDelete in grantAllocationAwardTravelLineItemTypesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardTravelLineItemTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardTravelLineItemType(this IQueryable<GrantAllocationAwardTravelLineItemType> grantAllocationAwardTravelLineItemTypes, ICollection<GrantAllocationAwardTravelLineItemType> grantAllocationAwardTravelLineItemTypesToDelete)
        {
            if(grantAllocationAwardTravelLineItemTypesToDelete.Any())
            {
                var grantAllocationAwardTravelLineItemTypeIDList = grantAllocationAwardTravelLineItemTypesToDelete.Select(x => x.GrantAllocationAwardTravelLineItemTypeID).ToList();
                var grantAllocationAwardTravelLineItemTypesToDeleteFromSourceList = grantAllocationAwardTravelLineItemTypes.Where(x => grantAllocationAwardTravelLineItemTypeIDList.Contains(x.GrantAllocationAwardTravelLineItemTypeID)).ToList();

                foreach (var grantAllocationAwardTravelLineItemTypeToDelete in grantAllocationAwardTravelLineItemTypesToDeleteFromSourceList)
                {
                    grantAllocationAwardTravelLineItemTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardTravelLineItemType(this IQueryable<GrantAllocationAwardTravelLineItemType> grantAllocationAwardTravelLineItemTypes, int grantAllocationAwardTravelLineItemTypeID)
        {
            DeleteGrantAllocationAwardTravelLineItemType(grantAllocationAwardTravelLineItemTypes, new List<int> { grantAllocationAwardTravelLineItemTypeID });
        }

        public static void DeleteGrantAllocationAwardTravelLineItemType(this IQueryable<GrantAllocationAwardTravelLineItemType> grantAllocationAwardTravelLineItemTypes, GrantAllocationAwardTravelLineItemType grantAllocationAwardTravelLineItemTypeToDelete)
        {
            DeleteGrantAllocationAwardTravelLineItemType(grantAllocationAwardTravelLineItemTypes, new List<GrantAllocationAwardTravelLineItemType> { grantAllocationAwardTravelLineItemTypeToDelete });
        }
    }
}