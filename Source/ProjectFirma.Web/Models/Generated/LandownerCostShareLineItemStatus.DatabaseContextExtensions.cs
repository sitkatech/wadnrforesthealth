//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandownerCostShareLineItemStatus]
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
        public static LandownerCostShareLineItemStatus GetLandownerCostShareLineItemStatus(this IQueryable<LandownerCostShareLineItemStatus> landownerCostShareLineItemStatuses, int landownerCostShareLineItemStatusID)
        {
            var landownerCostShareLineItemStatus = landownerCostShareLineItemStatuses.SingleOrDefault(x => x.LandownerCostShareLineItemStatusID == landownerCostShareLineItemStatusID);
            Check.RequireNotNullThrowNotFound(landownerCostShareLineItemStatus, "LandownerCostShareLineItemStatus", landownerCostShareLineItemStatusID);
            return landownerCostShareLineItemStatus;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteLandownerCostShareLineItemStatus(this IQueryable<LandownerCostShareLineItemStatus> landownerCostShareLineItemStatuses, List<int> landownerCostShareLineItemStatusIDList)
        {
            if(landownerCostShareLineItemStatusIDList.Any())
            {
                var landownerCostShareLineItemStatusesInSourceCollectionToDelete = landownerCostShareLineItemStatuses.Where(x => landownerCostShareLineItemStatusIDList.Contains(x.LandownerCostShareLineItemStatusID));
                foreach (var landownerCostShareLineItemStatusToDelete in landownerCostShareLineItemStatusesInSourceCollectionToDelete.ToList())
                {
                    landownerCostShareLineItemStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteLandownerCostShareLineItemStatus(this IQueryable<LandownerCostShareLineItemStatus> landownerCostShareLineItemStatuses, ICollection<LandownerCostShareLineItemStatus> landownerCostShareLineItemStatusesToDelete)
        {
            if(landownerCostShareLineItemStatusesToDelete.Any())
            {
                var landownerCostShareLineItemStatusIDList = landownerCostShareLineItemStatusesToDelete.Select(x => x.LandownerCostShareLineItemStatusID).ToList();
                var landownerCostShareLineItemStatusesToDeleteFromSourceList = landownerCostShareLineItemStatuses.Where(x => landownerCostShareLineItemStatusIDList.Contains(x.LandownerCostShareLineItemStatusID)).ToList();

                foreach (var landownerCostShareLineItemStatusToDelete in landownerCostShareLineItemStatusesToDeleteFromSourceList)
                {
                    landownerCostShareLineItemStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteLandownerCostShareLineItemStatus(this IQueryable<LandownerCostShareLineItemStatus> landownerCostShareLineItemStatuses, int landownerCostShareLineItemStatusID)
        {
            DeleteLandownerCostShareLineItemStatus(landownerCostShareLineItemStatuses, new List<int> { landownerCostShareLineItemStatusID });
        }

        public static void DeleteLandownerCostShareLineItemStatus(this IQueryable<LandownerCostShareLineItemStatus> landownerCostShareLineItemStatuses, LandownerCostShareLineItemStatus landownerCostShareLineItemStatusToDelete)
        {
            DeleteLandownerCostShareLineItemStatus(landownerCostShareLineItemStatuses, new List<LandownerCostShareLineItemStatus> { landownerCostShareLineItemStatusToDelete });
        }
    }
}