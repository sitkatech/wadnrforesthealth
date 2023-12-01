//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationSource]
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
        public static GrantAllocationSource GetGrantAllocationSource(this IQueryable<GrantAllocationSource> grantAllocationSources, int grantAllocationSourceID)
        {
            var grantAllocationSource = grantAllocationSources.SingleOrDefault(x => x.GrantAllocationSourceID == grantAllocationSourceID);
            Check.RequireNotNullThrowNotFound(grantAllocationSource, "GrantAllocationSource", grantAllocationSourceID);
            return grantAllocationSource;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationSource(this IQueryable<GrantAllocationSource> grantAllocationSources, List<int> grantAllocationSourceIDList)
        {
            if(grantAllocationSourceIDList.Any())
            {
                var grantAllocationSourcesInSourceCollectionToDelete = grantAllocationSources.Where(x => grantAllocationSourceIDList.Contains(x.GrantAllocationSourceID));
                foreach (var grantAllocationSourceToDelete in grantAllocationSourcesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationSourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationSource(this IQueryable<GrantAllocationSource> grantAllocationSources, ICollection<GrantAllocationSource> grantAllocationSourcesToDelete)
        {
            if(grantAllocationSourcesToDelete.Any())
            {
                var grantAllocationSourceIDList = grantAllocationSourcesToDelete.Select(x => x.GrantAllocationSourceID).ToList();
                var grantAllocationSourcesToDeleteFromSourceList = grantAllocationSources.Where(x => grantAllocationSourceIDList.Contains(x.GrantAllocationSourceID)).ToList();

                foreach (var grantAllocationSourceToDelete in grantAllocationSourcesToDeleteFromSourceList)
                {
                    grantAllocationSourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationSource(this IQueryable<GrantAllocationSource> grantAllocationSources, int grantAllocationSourceID)
        {
            DeleteGrantAllocationSource(grantAllocationSources, new List<int> { grantAllocationSourceID });
        }

        public static void DeleteGrantAllocationSource(this IQueryable<GrantAllocationSource> grantAllocationSources, GrantAllocationSource grantAllocationSourceToDelete)
        {
            DeleteGrantAllocationSource(grantAllocationSources, new List<GrantAllocationSource> { grantAllocationSourceToDelete });
        }
    }
}