//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureNote]
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
        public static PerformanceMeasureNote GetPerformanceMeasureNote(this IQueryable<PerformanceMeasureNote> performanceMeasureNotes, int performanceMeasureNoteID)
        {
            var performanceMeasureNote = performanceMeasureNotes.SingleOrDefault(x => x.PerformanceMeasureNoteID == performanceMeasureNoteID);
            Check.RequireNotNullThrowNotFound(performanceMeasureNote, "PerformanceMeasureNote", performanceMeasureNoteID);
            return performanceMeasureNote;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasureNote(this IQueryable<PerformanceMeasureNote> performanceMeasureNotes, List<int> performanceMeasureNoteIDList)
        {
            if(performanceMeasureNoteIDList.Any())
            {
                var performanceMeasureNotesInSourceCollectionToDelete = performanceMeasureNotes.Where(x => performanceMeasureNoteIDList.Contains(x.PerformanceMeasureNoteID));
                foreach (var performanceMeasureNoteToDelete in performanceMeasureNotesInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureNoteToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasureNote(this IQueryable<PerformanceMeasureNote> performanceMeasureNotes, ICollection<PerformanceMeasureNote> performanceMeasureNotesToDelete)
        {
            if(performanceMeasureNotesToDelete.Any())
            {
                var performanceMeasureNoteIDList = performanceMeasureNotesToDelete.Select(x => x.PerformanceMeasureNoteID).ToList();
                var performanceMeasureNotesToDeleteFromSourceList = performanceMeasureNotes.Where(x => performanceMeasureNoteIDList.Contains(x.PerformanceMeasureNoteID)).ToList();

                foreach (var performanceMeasureNoteToDelete in performanceMeasureNotesToDeleteFromSourceList)
                {
                    performanceMeasureNoteToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasureNote(this IQueryable<PerformanceMeasureNote> performanceMeasureNotes, int performanceMeasureNoteID)
        {
            DeletePerformanceMeasureNote(performanceMeasureNotes, new List<int> { performanceMeasureNoteID });
        }

        public static void DeletePerformanceMeasureNote(this IQueryable<PerformanceMeasureNote> performanceMeasureNotes, PerformanceMeasureNote performanceMeasureNoteToDelete)
        {
            DeletePerformanceMeasureNote(performanceMeasureNotes, new List<PerformanceMeasureNote> { performanceMeasureNoteToDelete });
        }
    }
}