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
        public static Grant GetGrant(this IQueryable<Grant> grants, int grantID)
        {
            var grant = grants.SingleOrDefault(x => x.GrantID == grantID);
            Check.RequireNotNullThrowNotFound(grant, "Grant", grantID);
            return grant;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrant(this IQueryable<Grant> grants, List<int> grantIDList)
        {
            if(grantIDList.Any())
            {
                var grantsInSourceCollectionToDelete = grants.Where(x => grantIDList.Contains(x.GrantID));
                foreach (var grantToDelete in grantsInSourceCollectionToDelete.ToList())
                {
                    grantToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrant(this IQueryable<Grant> grants, ICollection<Grant> grantsToDelete)
        {
            if(grantsToDelete.Any())
            {
                var grantIDList = grantsToDelete.Select(x => x.GrantID).ToList();
                var grantsToDeleteFromSourceList = grants.Where(x => grantIDList.Contains(x.GrantID)).ToList();

                foreach (var grantToDelete in grantsToDeleteFromSourceList)
                {
                    grantToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrant(this IQueryable<Grant> grants, int grantID)
        {
            DeleteGrant(grants, new List<int> { grantID });
        }

        public static void DeleteGrant(this IQueryable<Grant> grants, Grant grantToDelete)
        {
            DeleteGrant(grants, new List<Grant> { grantToDelete });
        }
    }
}