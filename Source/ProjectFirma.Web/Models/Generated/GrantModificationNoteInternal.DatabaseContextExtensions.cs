//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationNoteInternal]
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
        public static GrantModificationNoteInternal GetGrantModificationNoteInternal(this IQueryable<GrantModificationNoteInternal> grantModificationNoteInternals, int grantModificationNoteInternalID)
        {
            var grantModificationNoteInternal = grantModificationNoteInternals.SingleOrDefault(x => x.GrantModificationNoteInternalID == grantModificationNoteInternalID);
            Check.RequireNotNullThrowNotFound(grantModificationNoteInternal, "GrantModificationNoteInternal", grantModificationNoteInternalID);
            return grantModificationNoteInternal;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantModificationNoteInternal(this IQueryable<GrantModificationNoteInternal> grantModificationNoteInternals, List<int> grantModificationNoteInternalIDList)
        {
            if(grantModificationNoteInternalIDList.Any())
            {
                var grantModificationNoteInternalsInSourceCollectionToDelete = grantModificationNoteInternals.Where(x => grantModificationNoteInternalIDList.Contains(x.GrantModificationNoteInternalID));
                foreach (var grantModificationNoteInternalToDelete in grantModificationNoteInternalsInSourceCollectionToDelete.ToList())
                {
                    grantModificationNoteInternalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantModificationNoteInternal(this IQueryable<GrantModificationNoteInternal> grantModificationNoteInternals, ICollection<GrantModificationNoteInternal> grantModificationNoteInternalsToDelete)
        {
            if(grantModificationNoteInternalsToDelete.Any())
            {
                var grantModificationNoteInternalIDList = grantModificationNoteInternalsToDelete.Select(x => x.GrantModificationNoteInternalID).ToList();
                var grantModificationNoteInternalsToDeleteFromSourceList = grantModificationNoteInternals.Where(x => grantModificationNoteInternalIDList.Contains(x.GrantModificationNoteInternalID)).ToList();

                foreach (var grantModificationNoteInternalToDelete in grantModificationNoteInternalsToDeleteFromSourceList)
                {
                    grantModificationNoteInternalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantModificationNoteInternal(this IQueryable<GrantModificationNoteInternal> grantModificationNoteInternals, int grantModificationNoteInternalID)
        {
            DeleteGrantModificationNoteInternal(grantModificationNoteInternals, new List<int> { grantModificationNoteInternalID });
        }

        public static void DeleteGrantModificationNoteInternal(this IQueryable<GrantModificationNoteInternal> grantModificationNoteInternals, GrantModificationNoteInternal grantModificationNoteInternalToDelete)
        {
            DeleteGrantModificationNoteInternal(grantModificationNoteInternals, new List<GrantModificationNoteInternal> { grantModificationNoteInternalToDelete });
        }
    }
}