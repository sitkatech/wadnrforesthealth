//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantStatus]
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
        public static GrantStatus GetGrantStatus(this IQueryable<GrantStatus> grantStatuses, int grantStatusID)
        {
            var grantStatus = grantStatuses.SingleOrDefault(x => x.GrantStatusID == grantStatusID);
            Check.RequireNotNullThrowNotFound(grantStatus, "GrantStatus", grantStatusID);
            return grantStatus;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantStatus(this IQueryable<GrantStatus> grantStatuses, List<int> grantStatusIDList)
        {
            if(grantStatusIDList.Any())
            {
                var grantStatusesInSourceCollectionToDelete = grantStatuses.Where(x => grantStatusIDList.Contains(x.GrantStatusID));
                foreach (var grantStatusToDelete in grantStatusesInSourceCollectionToDelete.ToList())
                {
                    grantStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantStatus(this IQueryable<GrantStatus> grantStatuses, ICollection<GrantStatus> grantStatusesToDelete)
        {
            if(grantStatusesToDelete.Any())
            {
                var grantStatusIDList = grantStatusesToDelete.Select(x => x.GrantStatusID).ToList();
                var grantStatusesToDeleteFromSourceList = grantStatuses.Where(x => grantStatusIDList.Contains(x.GrantStatusID)).ToList();

                foreach (var grantStatusToDelete in grantStatusesToDeleteFromSourceList)
                {
                    grantStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantStatus(this IQueryable<GrantStatus> grantStatuses, int grantStatusID)
        {
            DeleteGrantStatus(grantStatuses, new List<int> { grantStatusID });
        }

        public static void DeleteGrantStatus(this IQueryable<GrantStatus> grantStatuses, GrantStatus grantStatusToDelete)
        {
            DeleteGrantStatus(grantStatuses, new List<GrantStatus> { grantStatusToDelete });
        }
    }
}