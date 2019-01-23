//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectInternalNote]
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
        public static ProjectInternalNote GetProjectInternalNote(this IQueryable<ProjectInternalNote> projectInternalNotes, int projectInternalNoteID)
        {
            var projectInternalNote = projectInternalNotes.SingleOrDefault(x => x.ProjectInternalNoteID == projectInternalNoteID);
            Check.RequireNotNullThrowNotFound(projectInternalNote, "ProjectInternalNote", projectInternalNoteID);
            return projectInternalNote;
        }

        public static void DeleteProjectInternalNote(this IQueryable<ProjectInternalNote> projectInternalNotes, List<int> projectInternalNoteIDList)
        {
            if(projectInternalNoteIDList.Any())
            {
                projectInternalNotes.Where(x => projectInternalNoteIDList.Contains(x.ProjectInternalNoteID)).Delete();
            }
        }

        public static void DeleteProjectInternalNote(this IQueryable<ProjectInternalNote> projectInternalNotes, ICollection<ProjectInternalNote> projectInternalNotesToDelete)
        {
            if(projectInternalNotesToDelete.Any())
            {
                var projectInternalNoteIDList = projectInternalNotesToDelete.Select(x => x.ProjectInternalNoteID).ToList();
                projectInternalNotes.Where(x => projectInternalNoteIDList.Contains(x.ProjectInternalNoteID)).Delete();
            }
        }

        public static void DeleteProjectInternalNote(this IQueryable<ProjectInternalNote> projectInternalNotes, int projectInternalNoteID)
        {
            DeleteProjectInternalNote(projectInternalNotes, new List<int> { projectInternalNoteID });
        }

        public static void DeleteProjectInternalNote(this IQueryable<ProjectInternalNote> projectInternalNotes, ProjectInternalNote projectInternalNoteToDelete)
        {
            DeleteProjectInternalNote(projectInternalNotes, new List<ProjectInternalNote> { projectInternalNoteToDelete });
        }
    }
}