//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantFileResource]
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
        public static FundSourceFileResource GetGrantFileResource(this IQueryable<FundSourceFileResource> grantFileResources, int grantFileResourceID)
        {
            var grantFileResource = grantFileResources.SingleOrDefault(x => x.GrantFileResourceID == grantFileResourceID);
            Check.RequireNotNullThrowNotFound(grantFileResource, "GrantFileResource", grantFileResourceID);
            return grantFileResource;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantFileResource(this IQueryable<FundSourceFileResource> grantFileResources, List<int> grantFileResourceIDList)
        {
            if(grantFileResourceIDList.Any())
            {
                var grantFileResourcesInSourceCollectionToDelete = grantFileResources.Where(x => grantFileResourceIDList.Contains(x.GrantFileResourceID));
                foreach (var grantFileResourceToDelete in grantFileResourcesInSourceCollectionToDelete.ToList())
                {
                    grantFileResourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantFileResource(this IQueryable<FundSourceFileResource> grantFileResources, ICollection<FundSourceFileResource> grantFileResourcesToDelete)
        {
            if(grantFileResourcesToDelete.Any())
            {
                var grantFileResourceIDList = grantFileResourcesToDelete.Select(x => x.GrantFileResourceID).ToList();
                var grantFileResourcesToDeleteFromSourceList = grantFileResources.Where(x => grantFileResourceIDList.Contains(x.GrantFileResourceID)).ToList();

                foreach (var grantFileResourceToDelete in grantFileResourcesToDeleteFromSourceList)
                {
                    grantFileResourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantFileResource(this IQueryable<FundSourceFileResource> grantFileResources, int grantFileResourceID)
        {
            DeleteGrantFileResource(grantFileResources, new List<int> { grantFileResourceID });
        }

        public static void DeleteGrantFileResource(this IQueryable<FundSourceFileResource> grantFileResources, FundSourceFileResource fundSourceFileResourceToDelete)
        {
            DeleteGrantFileResource(grantFileResources, new List<FundSourceFileResource> { fundSourceFileResourceToDelete });
        }
    }
}