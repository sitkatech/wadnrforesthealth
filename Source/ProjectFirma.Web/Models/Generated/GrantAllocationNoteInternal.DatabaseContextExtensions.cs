//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationNoteInternal]
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
        public static GrantAllocationNoteInternal GetGrantAllocationNoteInternal(this IQueryable<GrantAllocationNoteInternal> grantAllocationNoteInternals, int grantAllocationNoteInternalID)
        {
            var grantAllocationNoteInternal = grantAllocationNoteInternals.SingleOrDefault(x => x.GrantAllocationNoteInternalID == grantAllocationNoteInternalID);
            Check.RequireNotNullThrowNotFound(grantAllocationNoteInternal, "GrantAllocationNoteInternal", grantAllocationNoteInternalID);
            return grantAllocationNoteInternal;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationNoteInternal(this IQueryable<GrantAllocationNoteInternal> grantAllocationNoteInternals, List<int> grantAllocationNoteInternalIDList)
        {
            if(grantAllocationNoteInternalIDList.Any())
            {
                var grantAllocationNoteInternalsInSourceCollectionToDelete = grantAllocationNoteInternals.Where(x => grantAllocationNoteInternalIDList.Contains(x.GrantAllocationNoteInternalID));
                foreach (var grantAllocationNoteInternalToDelete in grantAllocationNoteInternalsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationNoteInternalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationNoteInternal(this IQueryable<GrantAllocationNoteInternal> grantAllocationNoteInternals, ICollection<GrantAllocationNoteInternal> grantAllocationNoteInternalsToDelete)
        {
            if(grantAllocationNoteInternalsToDelete.Any())
            {
                var grantAllocationNoteInternalIDList = grantAllocationNoteInternalsToDelete.Select(x => x.GrantAllocationNoteInternalID).ToList();
                var grantAllocationNoteInternalsToDeleteFromSourceList = grantAllocationNoteInternals.Where(x => grantAllocationNoteInternalIDList.Contains(x.GrantAllocationNoteInternalID)).ToList();

                foreach (var grantAllocationNoteInternalToDelete in grantAllocationNoteInternalsToDeleteFromSourceList)
                {
                    grantAllocationNoteInternalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationNoteInternal(this IQueryable<GrantAllocationNoteInternal> grantAllocationNoteInternals, int grantAllocationNoteInternalID)
        {
            DeleteGrantAllocationNoteInternal(grantAllocationNoteInternals, new List<int> { grantAllocationNoteInternalID });
        }

        public static void DeleteGrantAllocationNoteInternal(this IQueryable<GrantAllocationNoteInternal> grantAllocationNoteInternals, GrantAllocationNoteInternal grantAllocationNoteInternalToDelete)
        {
            DeleteGrantAllocationNoteInternal(grantAllocationNoteInternals, new List<GrantAllocationNoteInternal> { grantAllocationNoteInternalToDelete });
        }
    }
}