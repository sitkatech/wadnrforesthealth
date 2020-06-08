//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationPurpose]
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
        public static GrantModificationPurpose GetGrantModificationPurpose(this IQueryable<GrantModificationPurpose> grantModificationPurposes, int grantModificationPurposeID)
        {
            var grantModificationPurpose = grantModificationPurposes.SingleOrDefault(x => x.GrantModificationPurposeID == grantModificationPurposeID);
            Check.RequireNotNullThrowNotFound(grantModificationPurpose, "GrantModificationPurpose", grantModificationPurposeID);
            return grantModificationPurpose;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantModificationPurpose(this IQueryable<GrantModificationPurpose> grantModificationPurposes, List<int> grantModificationPurposeIDList)
        {
            if(grantModificationPurposeIDList.Any())
            {
                var grantModificationPurposesInSourceCollectionToDelete = grantModificationPurposes.Where(x => grantModificationPurposeIDList.Contains(x.GrantModificationPurposeID));
                foreach (var grantModificationPurposeToDelete in grantModificationPurposesInSourceCollectionToDelete.ToList())
                {
                    grantModificationPurposeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantModificationPurpose(this IQueryable<GrantModificationPurpose> grantModificationPurposes, ICollection<GrantModificationPurpose> grantModificationPurposesToDelete)
        {
            if(grantModificationPurposesToDelete.Any())
            {
                var grantModificationPurposeIDList = grantModificationPurposesToDelete.Select(x => x.GrantModificationPurposeID).ToList();
                var grantModificationPurposesToDeleteFromSourceList = grantModificationPurposes.Where(x => grantModificationPurposeIDList.Contains(x.GrantModificationPurposeID)).ToList();

                foreach (var grantModificationPurposeToDelete in grantModificationPurposesToDeleteFromSourceList)
                {
                    grantModificationPurposeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantModificationPurpose(this IQueryable<GrantModificationPurpose> grantModificationPurposes, int grantModificationPurposeID)
        {
            DeleteGrantModificationPurpose(grantModificationPurposes, new List<int> { grantModificationPurposeID });
        }

        public static void DeleteGrantModificationPurpose(this IQueryable<GrantModificationPurpose> grantModificationPurposes, GrantModificationPurpose grantModificationPurposeToDelete)
        {
            DeleteGrantModificationPurpose(grantModificationPurposes, new List<GrantModificationPurpose> { grantModificationPurposeToDelete });
        }
    }
}