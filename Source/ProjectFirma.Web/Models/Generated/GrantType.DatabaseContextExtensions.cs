//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantType]
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
        public static GrantType GetGrantType(this IQueryable<GrantType> grantTypes, int grantTypeID)
        {
            var grantType = grantTypes.SingleOrDefault(x => x.GrantTypeID == grantTypeID);
            Check.RequireNotNullThrowNotFound(grantType, "GrantType", grantTypeID);
            return grantType;
        }

        public static void DeleteGrantType(this IQueryable<GrantType> grantTypes, List<int> grantTypeIDList)
        {
            if(grantTypeIDList.Any())
            {
                grantTypes.Where(x => grantTypeIDList.Contains(x.GrantTypeID)).Delete();
            }
        }

        public static void DeleteGrantType(this IQueryable<GrantType> grantTypes, ICollection<GrantType> grantTypesToDelete)
        {
            if(grantTypesToDelete.Any())
            {
                var grantTypeIDList = grantTypesToDelete.Select(x => x.GrantTypeID).ToList();
                grantTypes.Where(x => grantTypeIDList.Contains(x.GrantTypeID)).Delete();
            }
        }

        public static void DeleteGrantType(this IQueryable<GrantType> grantTypes, int grantTypeID)
        {
            DeleteGrantType(grantTypes, new List<int> { grantTypeID });
        }

        public static void DeleteGrantType(this IQueryable<GrantType> grantTypes, GrantType grantTypeToDelete)
        {
            DeleteGrantType(grantTypes, new List<GrantType> { grantTypeToDelete });
        }
    }
}