//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantNoteInternal]
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
        public static FundSourceNoteInternal GetGrantNoteInternal(this IQueryable<FundSourceNoteInternal> grantNoteInternals, int grantNoteInternalID)
        {
            var grantNoteInternal = grantNoteInternals.SingleOrDefault(x => x.GrantNoteInternalID == grantNoteInternalID);
            Check.RequireNotNullThrowNotFound(grantNoteInternal, "GrantNoteInternal", grantNoteInternalID);
            return grantNoteInternal;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantNoteInternal(this IQueryable<FundSourceNoteInternal> grantNoteInternals, List<int> grantNoteInternalIDList)
        {
            if(grantNoteInternalIDList.Any())
            {
                var grantNoteInternalsInSourceCollectionToDelete = grantNoteInternals.Where(x => grantNoteInternalIDList.Contains(x.GrantNoteInternalID));
                foreach (var grantNoteInternalToDelete in grantNoteInternalsInSourceCollectionToDelete.ToList())
                {
                    grantNoteInternalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantNoteInternal(this IQueryable<FundSourceNoteInternal> grantNoteInternals, ICollection<FundSourceNoteInternal> grantNoteInternalsToDelete)
        {
            if(grantNoteInternalsToDelete.Any())
            {
                var grantNoteInternalIDList = grantNoteInternalsToDelete.Select(x => x.GrantNoteInternalID).ToList();
                var grantNoteInternalsToDeleteFromSourceList = grantNoteInternals.Where(x => grantNoteInternalIDList.Contains(x.GrantNoteInternalID)).ToList();

                foreach (var grantNoteInternalToDelete in grantNoteInternalsToDeleteFromSourceList)
                {
                    grantNoteInternalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantNoteInternal(this IQueryable<FundSourceNoteInternal> grantNoteInternals, int grantNoteInternalID)
        {
            DeleteGrantNoteInternal(grantNoteInternals, new List<int> { grantNoteInternalID });
        }

        public static void DeleteGrantNoteInternal(this IQueryable<FundSourceNoteInternal> grantNoteInternals, FundSourceNoteInternal fundSourceNoteInternalToDelete)
        {
            DeleteGrantNoteInternal(grantNoteInternals, new List<FundSourceNoteInternal> { fundSourceNoteInternalToDelete });
        }
    }
}