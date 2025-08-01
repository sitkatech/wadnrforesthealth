//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantType]
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
        public static FundSourceType GetGrantType(this IQueryable<FundSourceType> grantTypes, int grantTypeID)
        {
            var grantType = grantTypes.SingleOrDefault(x => x.GrantTypeID == grantTypeID);
            Check.RequireNotNullThrowNotFound(grantType, "GrantType", grantTypeID);
            return grantType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantType(this IQueryable<FundSourceType> grantTypes, List<int> grantTypeIDList)
        {
            if(grantTypeIDList.Any())
            {
                var grantTypesInSourceCollectionToDelete = grantTypes.Where(x => grantTypeIDList.Contains(x.GrantTypeID));
                foreach (var grantTypeToDelete in grantTypesInSourceCollectionToDelete.ToList())
                {
                    grantTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantType(this IQueryable<FundSourceType> grantTypes, ICollection<FundSourceType> grantTypesToDelete)
        {
            if(grantTypesToDelete.Any())
            {
                var grantTypeIDList = grantTypesToDelete.Select(x => x.GrantTypeID).ToList();
                var grantTypesToDeleteFromSourceList = grantTypes.Where(x => grantTypeIDList.Contains(x.GrantTypeID)).ToList();

                foreach (var grantTypeToDelete in grantTypesToDeleteFromSourceList)
                {
                    grantTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantType(this IQueryable<FundSourceType> grantTypes, int grantTypeID)
        {
            DeleteGrantType(grantTypes, new List<int> { grantTypeID });
        }

        public static void DeleteGrantType(this IQueryable<FundSourceType> grantTypes, FundSourceType fundSourceTypeToDelete)
        {
            DeleteGrantType(grantTypes, new List<FundSourceType> { fundSourceTypeToDelete });
        }
    }
}