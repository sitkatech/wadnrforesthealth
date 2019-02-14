//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantNote]
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
        public static GrantNote GetGrantNote(this IQueryable<GrantNote> grantNotes, int grantNoteID)
        {
            var grantNote = grantNotes.SingleOrDefault(x => x.GrantNoteID == grantNoteID);
            Check.RequireNotNullThrowNotFound(grantNote, "GrantNote", grantNoteID);
            return grantNote;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantNote(this IQueryable<GrantNote> grantNotes, List<int> grantNoteIDList)
        {
            if(grantNoteIDList.Any())
            {
                var grantNotesInSourceCollectionToDelete = grantNotes.Where(x => grantNoteIDList.Contains(x.GrantNoteID));
                foreach (var grantNoteToDelete in grantNotesInSourceCollectionToDelete.ToList())
                {
                    grantNoteToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantNote(this IQueryable<GrantNote> grantNotes, ICollection<GrantNote> grantNotesToDelete)
        {
            if(grantNotesToDelete.Any())
            {
                var grantNoteIDList = grantNotesToDelete.Select(x => x.GrantNoteID).ToList();
                var grantNotesToDeleteFromSourceList = grantNotes.Where(x => grantNoteIDList.Contains(x.GrantNoteID)).ToList();

                foreach (var grantNoteToDelete in grantNotesToDeleteFromSourceList)
                {
                    grantNoteToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantNote(this IQueryable<GrantNote> grantNotes, int grantNoteID)
        {
            DeleteGrantNote(grantNotes, new List<int> { grantNoteID });
        }

        public static void DeleteGrantNote(this IQueryable<GrantNote> grantNotes, GrantNote grantNoteToDelete)
        {
            DeleteGrantNote(grantNotes, new List<GrantNote> { grantNoteToDelete });
        }
    }
}