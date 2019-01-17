//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGeospatialAreaTypeNoteUpdate]
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
        public static ProjectGeospatialAreaTypeNoteUpdate GetProjectGeospatialAreaTypeNoteUpdate(this IQueryable<ProjectGeospatialAreaTypeNoteUpdate> projectGeospatialAreaTypeNoteUpdates, int projectGeospatialAreaTypeNoteUpdateID)
        {
            var projectGeospatialAreaTypeNoteUpdate = projectGeospatialAreaTypeNoteUpdates.SingleOrDefault(x => x.ProjectGeospatialAreaTypeNoteUpdateID == projectGeospatialAreaTypeNoteUpdateID);
            Check.RequireNotNullThrowNotFound(projectGeospatialAreaTypeNoteUpdate, "ProjectGeospatialAreaTypeNoteUpdate", projectGeospatialAreaTypeNoteUpdateID);
            return projectGeospatialAreaTypeNoteUpdate;
        }

        public static void DeleteProjectGeospatialAreaTypeNoteUpdate(this IQueryable<ProjectGeospatialAreaTypeNoteUpdate> projectGeospatialAreaTypeNoteUpdates, List<int> projectGeospatialAreaTypeNoteUpdateIDList)
        {
            if(projectGeospatialAreaTypeNoteUpdateIDList.Any())
            {
                projectGeospatialAreaTypeNoteUpdates.Where(x => projectGeospatialAreaTypeNoteUpdateIDList.Contains(x.ProjectGeospatialAreaTypeNoteUpdateID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialAreaTypeNoteUpdate(this IQueryable<ProjectGeospatialAreaTypeNoteUpdate> projectGeospatialAreaTypeNoteUpdates, ICollection<ProjectGeospatialAreaTypeNoteUpdate> projectGeospatialAreaTypeNoteUpdatesToDelete)
        {
            if(projectGeospatialAreaTypeNoteUpdatesToDelete.Any())
            {
                var projectGeospatialAreaTypeNoteUpdateIDList = projectGeospatialAreaTypeNoteUpdatesToDelete.Select(x => x.ProjectGeospatialAreaTypeNoteUpdateID).ToList();
                projectGeospatialAreaTypeNoteUpdates.Where(x => projectGeospatialAreaTypeNoteUpdateIDList.Contains(x.ProjectGeospatialAreaTypeNoteUpdateID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialAreaTypeNoteUpdate(this IQueryable<ProjectGeospatialAreaTypeNoteUpdate> projectGeospatialAreaTypeNoteUpdates, int projectGeospatialAreaTypeNoteUpdateID)
        {
            DeleteProjectGeospatialAreaTypeNoteUpdate(projectGeospatialAreaTypeNoteUpdates, new List<int> { projectGeospatialAreaTypeNoteUpdateID });
        }

        public static void DeleteProjectGeospatialAreaTypeNoteUpdate(this IQueryable<ProjectGeospatialAreaTypeNoteUpdate> projectGeospatialAreaTypeNoteUpdates, ProjectGeospatialAreaTypeNoteUpdate projectGeospatialAreaTypeNoteUpdateToDelete)
        {
            DeleteProjectGeospatialAreaTypeNoteUpdate(projectGeospatialAreaTypeNoteUpdates, new List<ProjectGeospatialAreaTypeNoteUpdate> { projectGeospatialAreaTypeNoteUpdateToDelete });
        }
    }
}