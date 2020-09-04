//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationFileResource]
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
        public static GrantModificationFileResource GetGrantModificationFileResource(this IQueryable<GrantModificationFileResource> grantModificationFileResources, int grantModificationFileResourceID)
        {
            var grantModificationFileResource = grantModificationFileResources.SingleOrDefault(x => x.GrantModificationFileResourceID == grantModificationFileResourceID);
            Check.RequireNotNullThrowNotFound(grantModificationFileResource, "GrantModificationFileResource", grantModificationFileResourceID);
            return grantModificationFileResource;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantModificationFileResource(this IQueryable<GrantModificationFileResource> grantModificationFileResources, List<int> grantModificationFileResourceIDList)
        {
            if(grantModificationFileResourceIDList.Any())
            {
                var grantModificationFileResourcesInSourceCollectionToDelete = grantModificationFileResources.Where(x => grantModificationFileResourceIDList.Contains(x.GrantModificationFileResourceID));
                foreach (var grantModificationFileResourceToDelete in grantModificationFileResourcesInSourceCollectionToDelete.ToList())
                {
                    grantModificationFileResourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantModificationFileResource(this IQueryable<GrantModificationFileResource> grantModificationFileResources, ICollection<GrantModificationFileResource> grantModificationFileResourcesToDelete)
        {
            if(grantModificationFileResourcesToDelete.Any())
            {
                var grantModificationFileResourceIDList = grantModificationFileResourcesToDelete.Select(x => x.GrantModificationFileResourceID).ToList();
                var grantModificationFileResourcesToDeleteFromSourceList = grantModificationFileResources.Where(x => grantModificationFileResourceIDList.Contains(x.GrantModificationFileResourceID)).ToList();

                foreach (var grantModificationFileResourceToDelete in grantModificationFileResourcesToDeleteFromSourceList)
                {
                    grantModificationFileResourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantModificationFileResource(this IQueryable<GrantModificationFileResource> grantModificationFileResources, int grantModificationFileResourceID)
        {
            DeleteGrantModificationFileResource(grantModificationFileResources, new List<int> { grantModificationFileResourceID });
        }

        public static void DeleteGrantModificationFileResource(this IQueryable<GrantModificationFileResource> grantModificationFileResources, GrantModificationFileResource grantModificationFileResourceToDelete)
        {
            DeleteGrantModificationFileResource(grantModificationFileResources, new List<GrantModificationFileResource> { grantModificationFileResourceToDelete });
        }
    }
}