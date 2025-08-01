//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Grant]
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
        public static FundSource GetGrant(this IQueryable<FundSource> grants, int grantID)
        {
            var grant = grants.SingleOrDefault(x => x.FundSourceID == grantID);
            Check.RequireNotNullThrowNotFound(grant, "Grant", grantID);
            return grant;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrant(this IQueryable<FundSource> grants, List<int> grantIDList)
        {
            if(grantIDList.Any())
            {
                var grantsInSourceCollectionToDelete = grants.Where(x => grantIDList.Contains(x.FundSourceID));
                foreach (var grantToDelete in grantsInSourceCollectionToDelete.ToList())
                {
                    grantToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrant(this IQueryable<FundSource> grants, ICollection<FundSource> grantsToDelete)
        {
            if(grantsToDelete.Any())
            {
                var grantIDList = grantsToDelete.Select(x => x.FundSourceID).ToList();
                var grantsToDeleteFromSourceList = grants.Where(x => grantIDList.Contains(x.FundSourceID)).ToList();

                foreach (var grantToDelete in grantsToDeleteFromSourceList)
                {
                    grantToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrant(this IQueryable<FundSource> grants, int grantID)
        {
            DeleteGrant(grants, new List<int> { grantID });
        }

        public static void DeleteGrant(this IQueryable<FundSource> grants, FundSource fundSourceToDelete)
        {
            DeleteGrant(grants, new List<FundSource> { fundSourceToDelete });
        }
    }
}