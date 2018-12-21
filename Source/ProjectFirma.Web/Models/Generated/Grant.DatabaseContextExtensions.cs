//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Grant]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteGrant(this List<int> grantIDList)
        {
            if(grantIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllGrants.RemoveRange(HttpRequestStorage.DatabaseEntities.Grants.Where(x => grantIDList.Contains(x.GrantID)));
            }
        }

        public static void DeleteGrant(this ICollection<Grant> grantsToDelete)
        {
            if(grantsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllGrants.RemoveRange(grantsToDelete);
            }
        }

        public static void DeleteGrant(this int grantID)
        {
            DeleteGrant(new List<int> { grantID });
        }

        public static void DeleteGrant(this Grant grantToDelete)
        {
            DeleteGrant(new List<Grant> { grantToDelete });
        }
    }
}