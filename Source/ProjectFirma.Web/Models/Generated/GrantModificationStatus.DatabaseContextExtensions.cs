//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationStatus]
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
        public static GrantModificationStatus GetGrantModificationStatus(this IQueryable<GrantModificationStatus> grantModificationStatuses, int grantModificationStatusID)
        {
            var grantModificationStatus = grantModificationStatuses.SingleOrDefault(x => x.GrantModificationStatusID == grantModificationStatusID);
            Check.RequireNotNullThrowNotFound(grantModificationStatus, "GrantModificationStatus", grantModificationStatusID);
            return grantModificationStatus;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantModificationStatus(this IQueryable<GrantModificationStatus> grantModificationStatuses, List<int> grantModificationStatusIDList)
        {
            if(grantModificationStatusIDList.Any())
            {
                var grantModificationStatusesInSourceCollectionToDelete = grantModificationStatuses.Where(x => grantModificationStatusIDList.Contains(x.GrantModificationStatusID));
                foreach (var grantModificationStatusToDelete in grantModificationStatusesInSourceCollectionToDelete.ToList())
                {
                    grantModificationStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantModificationStatus(this IQueryable<GrantModificationStatus> grantModificationStatuses, ICollection<GrantModificationStatus> grantModificationStatusesToDelete)
        {
            if(grantModificationStatusesToDelete.Any())
            {
                var grantModificationStatusIDList = grantModificationStatusesToDelete.Select(x => x.GrantModificationStatusID).ToList();
                var grantModificationStatusesToDeleteFromSourceList = grantModificationStatuses.Where(x => grantModificationStatusIDList.Contains(x.GrantModificationStatusID)).ToList();

                foreach (var grantModificationStatusToDelete in grantModificationStatusesToDeleteFromSourceList)
                {
                    grantModificationStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantModificationStatus(this IQueryable<GrantModificationStatus> grantModificationStatuses, int grantModificationStatusID)
        {
            DeleteGrantModificationStatus(grantModificationStatuses, new List<int> { grantModificationStatusID });
        }

        public static void DeleteGrantModificationStatus(this IQueryable<GrantModificationStatus> grantModificationStatuses, GrantModificationStatus grantModificationStatusToDelete)
        {
            DeleteGrantModificationStatus(grantModificationStatuses, new List<GrantModificationStatus> { grantModificationStatusToDelete });
        }
    }
}