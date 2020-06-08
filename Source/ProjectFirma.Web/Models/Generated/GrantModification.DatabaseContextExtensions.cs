//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModification]
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
        public static GrantModification GetGrantModification(this IQueryable<GrantModification> grantModifications, int grantModificationID)
        {
            var grantModification = grantModifications.SingleOrDefault(x => x.GrantModificationID == grantModificationID);
            Check.RequireNotNullThrowNotFound(grantModification, "GrantModification", grantModificationID);
            return grantModification;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantModification(this IQueryable<GrantModification> grantModifications, List<int> grantModificationIDList)
        {
            if(grantModificationIDList.Any())
            {
                var grantModificationsInSourceCollectionToDelete = grantModifications.Where(x => grantModificationIDList.Contains(x.GrantModificationID));
                foreach (var grantModificationToDelete in grantModificationsInSourceCollectionToDelete.ToList())
                {
                    grantModificationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantModification(this IQueryable<GrantModification> grantModifications, ICollection<GrantModification> grantModificationsToDelete)
        {
            if(grantModificationsToDelete.Any())
            {
                var grantModificationIDList = grantModificationsToDelete.Select(x => x.GrantModificationID).ToList();
                var grantModificationsToDeleteFromSourceList = grantModifications.Where(x => grantModificationIDList.Contains(x.GrantModificationID)).ToList();

                foreach (var grantModificationToDelete in grantModificationsToDeleteFromSourceList)
                {
                    grantModificationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantModification(this IQueryable<GrantModification> grantModifications, int grantModificationID)
        {
            DeleteGrantModification(grantModifications, new List<int> { grantModificationID });
        }

        public static void DeleteGrantModification(this IQueryable<GrantModification> grantModifications, GrantModification grantModificationToDelete)
        {
            DeleteGrantModification(grantModifications, new List<GrantModification> { grantModificationToDelete });
        }
    }
}