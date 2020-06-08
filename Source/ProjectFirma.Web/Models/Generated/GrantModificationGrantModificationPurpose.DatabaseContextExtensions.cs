//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationGrantModificationPurpose]
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
        public static GrantModificationGrantModificationPurpose GetGrantModificationGrantModificationPurpose(this IQueryable<GrantModificationGrantModificationPurpose> grantModificationGrantModificationPurposes, int grantModificationGrantModificationPurposeID)
        {
            var grantModificationGrantModificationPurpose = grantModificationGrantModificationPurposes.SingleOrDefault(x => x.GrantModificationGrantModificationPurposeID == grantModificationGrantModificationPurposeID);
            Check.RequireNotNullThrowNotFound(grantModificationGrantModificationPurpose, "GrantModificationGrantModificationPurpose", grantModificationGrantModificationPurposeID);
            return grantModificationGrantModificationPurpose;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantModificationGrantModificationPurpose(this IQueryable<GrantModificationGrantModificationPurpose> grantModificationGrantModificationPurposes, List<int> grantModificationGrantModificationPurposeIDList)
        {
            if(grantModificationGrantModificationPurposeIDList.Any())
            {
                var grantModificationGrantModificationPurposesInSourceCollectionToDelete = grantModificationGrantModificationPurposes.Where(x => grantModificationGrantModificationPurposeIDList.Contains(x.GrantModificationGrantModificationPurposeID));
                foreach (var grantModificationGrantModificationPurposeToDelete in grantModificationGrantModificationPurposesInSourceCollectionToDelete.ToList())
                {
                    grantModificationGrantModificationPurposeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantModificationGrantModificationPurpose(this IQueryable<GrantModificationGrantModificationPurpose> grantModificationGrantModificationPurposes, ICollection<GrantModificationGrantModificationPurpose> grantModificationGrantModificationPurposesToDelete)
        {
            if(grantModificationGrantModificationPurposesToDelete.Any())
            {
                var grantModificationGrantModificationPurposeIDList = grantModificationGrantModificationPurposesToDelete.Select(x => x.GrantModificationGrantModificationPurposeID).ToList();
                var grantModificationGrantModificationPurposesToDeleteFromSourceList = grantModificationGrantModificationPurposes.Where(x => grantModificationGrantModificationPurposeIDList.Contains(x.GrantModificationGrantModificationPurposeID)).ToList();

                foreach (var grantModificationGrantModificationPurposeToDelete in grantModificationGrantModificationPurposesToDeleteFromSourceList)
                {
                    grantModificationGrantModificationPurposeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantModificationGrantModificationPurpose(this IQueryable<GrantModificationGrantModificationPurpose> grantModificationGrantModificationPurposes, int grantModificationGrantModificationPurposeID)
        {
            DeleteGrantModificationGrantModificationPurpose(grantModificationGrantModificationPurposes, new List<int> { grantModificationGrantModificationPurposeID });
        }

        public static void DeleteGrantModificationGrantModificationPurpose(this IQueryable<GrantModificationGrantModificationPurpose> grantModificationGrantModificationPurposes, GrantModificationGrantModificationPurpose grantModificationGrantModificationPurposeToDelete)
        {
            DeleteGrantModificationGrantModificationPurpose(grantModificationGrantModificationPurposes, new List<GrantModificationGrantModificationPurpose> { grantModificationGrantModificationPurposeToDelete });
        }
    }
}