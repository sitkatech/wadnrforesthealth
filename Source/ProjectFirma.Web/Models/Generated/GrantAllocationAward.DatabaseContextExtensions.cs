//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAward]
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
        public static GrantAllocationAward GetGrantAllocationAward(this IQueryable<GrantAllocationAward> grantAllocationAwards, int grantAllocationAwardID)
        {
            var grantAllocationAward = grantAllocationAwards.SingleOrDefault(x => x.GrantAllocationAwardID == grantAllocationAwardID);
            Check.RequireNotNullThrowNotFound(grantAllocationAward, "GrantAllocationAward", grantAllocationAwardID);
            return grantAllocationAward;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAward(this IQueryable<GrantAllocationAward> grantAllocationAwards, List<int> grantAllocationAwardIDList)
        {
            if(grantAllocationAwardIDList.Any())
            {
                var grantAllocationAwardsInSourceCollectionToDelete = grantAllocationAwards.Where(x => grantAllocationAwardIDList.Contains(x.GrantAllocationAwardID));
                foreach (var grantAllocationAwardToDelete in grantAllocationAwardsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAward(this IQueryable<GrantAllocationAward> grantAllocationAwards, ICollection<GrantAllocationAward> grantAllocationAwardsToDelete)
        {
            if(grantAllocationAwardsToDelete.Any())
            {
                var grantAllocationAwardIDList = grantAllocationAwardsToDelete.Select(x => x.GrantAllocationAwardID).ToList();
                var grantAllocationAwardsToDeleteFromSourceList = grantAllocationAwards.Where(x => grantAllocationAwardIDList.Contains(x.GrantAllocationAwardID)).ToList();

                foreach (var grantAllocationAwardToDelete in grantAllocationAwardsToDeleteFromSourceList)
                {
                    grantAllocationAwardToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAward(this IQueryable<GrantAllocationAward> grantAllocationAwards, int grantAllocationAwardID)
        {
            DeleteGrantAllocationAward(grantAllocationAwards, new List<int> { grantAllocationAwardID });
        }

        public static void DeleteGrantAllocationAward(this IQueryable<GrantAllocationAward> grantAllocationAwards, GrantAllocationAward grantAllocationAwardToDelete)
        {
            DeleteGrantAllocationAward(grantAllocationAwards, new List<GrantAllocationAward> { grantAllocationAwardToDelete });
        }
    }
}