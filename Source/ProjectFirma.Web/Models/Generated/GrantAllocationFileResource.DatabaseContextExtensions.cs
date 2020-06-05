//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationFileResource]
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
        public static GrantAllocationFileResource GetGrantAllocationFileResource(this IQueryable<GrantAllocationFileResource> grantAllocationFileResources, int grantAllocationFileResourceID)
        {
            var grantAllocationFileResource = grantAllocationFileResources.SingleOrDefault(x => x.GrantAllocationFileResourceID == grantAllocationFileResourceID);
            Check.RequireNotNullThrowNotFound(grantAllocationFileResource, "GrantAllocationFileResource", grantAllocationFileResourceID);
            return grantAllocationFileResource;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationFileResource(this IQueryable<GrantAllocationFileResource> grantAllocationFileResources, List<int> grantAllocationFileResourceIDList)
        {
            if(grantAllocationFileResourceIDList.Any())
            {
                var grantAllocationFileResourcesInSourceCollectionToDelete = grantAllocationFileResources.Where(x => grantAllocationFileResourceIDList.Contains(x.GrantAllocationFileResourceID));
                foreach (var grantAllocationFileResourceToDelete in grantAllocationFileResourcesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationFileResourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationFileResource(this IQueryable<GrantAllocationFileResource> grantAllocationFileResources, ICollection<GrantAllocationFileResource> grantAllocationFileResourcesToDelete)
        {
            if(grantAllocationFileResourcesToDelete.Any())
            {
                var grantAllocationFileResourceIDList = grantAllocationFileResourcesToDelete.Select(x => x.GrantAllocationFileResourceID).ToList();
                var grantAllocationFileResourcesToDeleteFromSourceList = grantAllocationFileResources.Where(x => grantAllocationFileResourceIDList.Contains(x.GrantAllocationFileResourceID)).ToList();

                foreach (var grantAllocationFileResourceToDelete in grantAllocationFileResourcesToDeleteFromSourceList)
                {
                    grantAllocationFileResourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationFileResource(this IQueryable<GrantAllocationFileResource> grantAllocationFileResources, int grantAllocationFileResourceID)
        {
            DeleteGrantAllocationFileResource(grantAllocationFileResources, new List<int> { grantAllocationFileResourceID });
        }

        public static void DeleteGrantAllocationFileResource(this IQueryable<GrantAllocationFileResource> grantAllocationFileResources, GrantAllocationFileResource grantAllocationFileResourceToDelete)
        {
            DeleteGrantAllocationFileResource(grantAllocationFileResources, new List<GrantAllocationFileResource> { grantAllocationFileResourceToDelete });
        }
    }
}