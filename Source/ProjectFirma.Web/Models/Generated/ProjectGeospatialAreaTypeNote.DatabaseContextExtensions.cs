//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGeospatialAreaTypeNote]
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
        public static ProjectGeospatialAreaTypeNote GetProjectGeospatialAreaTypeNote(this IQueryable<ProjectGeospatialAreaTypeNote> projectGeospatialAreaTypeNotes, int projectGeospatialAreaTypeNoteID)
        {
            var projectGeospatialAreaTypeNote = projectGeospatialAreaTypeNotes.SingleOrDefault(x => x.ProjectGeospatialAreaTypeNoteID == projectGeospatialAreaTypeNoteID);
            Check.RequireNotNullThrowNotFound(projectGeospatialAreaTypeNote, "ProjectGeospatialAreaTypeNote", projectGeospatialAreaTypeNoteID);
            return projectGeospatialAreaTypeNote;
        }

        public static void DeleteProjectGeospatialAreaTypeNote(this IQueryable<ProjectGeospatialAreaTypeNote> projectGeospatialAreaTypeNotes, List<int> projectGeospatialAreaTypeNoteIDList)
        {
            if(projectGeospatialAreaTypeNoteIDList.Any())
            {
                projectGeospatialAreaTypeNotes.Where(x => projectGeospatialAreaTypeNoteIDList.Contains(x.ProjectGeospatialAreaTypeNoteID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialAreaTypeNote(this IQueryable<ProjectGeospatialAreaTypeNote> projectGeospatialAreaTypeNotes, ICollection<ProjectGeospatialAreaTypeNote> projectGeospatialAreaTypeNotesToDelete)
        {
            if(projectGeospatialAreaTypeNotesToDelete.Any())
            {
                var projectGeospatialAreaTypeNoteIDList = projectGeospatialAreaTypeNotesToDelete.Select(x => x.ProjectGeospatialAreaTypeNoteID).ToList();
                projectGeospatialAreaTypeNotes.Where(x => projectGeospatialAreaTypeNoteIDList.Contains(x.ProjectGeospatialAreaTypeNoteID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialAreaTypeNote(this IQueryable<ProjectGeospatialAreaTypeNote> projectGeospatialAreaTypeNotes, int projectGeospatialAreaTypeNoteID)
        {
            DeleteProjectGeospatialAreaTypeNote(projectGeospatialAreaTypeNotes, new List<int> { projectGeospatialAreaTypeNoteID });
        }

        public static void DeleteProjectGeospatialAreaTypeNote(this IQueryable<ProjectGeospatialAreaTypeNote> projectGeospatialAreaTypeNotes, ProjectGeospatialAreaTypeNote projectGeospatialAreaTypeNoteToDelete)
        {
            DeleteProjectGeospatialAreaTypeNote(projectGeospatialAreaTypeNotes, new List<ProjectGeospatialAreaTypeNote> { projectGeospatialAreaTypeNoteToDelete });
        }
    }
}