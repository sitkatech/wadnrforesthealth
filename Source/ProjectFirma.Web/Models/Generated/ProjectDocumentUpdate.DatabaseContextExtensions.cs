//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocumentUpdate]
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
        public static ProjectDocumentUpdate GetProjectDocumentUpdate(this IQueryable<ProjectDocumentUpdate> projectDocumentUpdates, int projectDocumentUpdateID)
        {
            var projectDocumentUpdate = projectDocumentUpdates.SingleOrDefault(x => x.ProjectDocumentUpdateID == projectDocumentUpdateID);
            Check.RequireNotNullThrowNotFound(projectDocumentUpdate, "ProjectDocumentUpdate", projectDocumentUpdateID);
            return projectDocumentUpdate;
        }

        public static void DeleteProjectDocumentUpdate(this IQueryable<ProjectDocumentUpdate> projectDocumentUpdates, List<int> projectDocumentUpdateIDList)
        {
            if(projectDocumentUpdateIDList.Any())
            {
                projectDocumentUpdates.Where(x => projectDocumentUpdateIDList.Contains(x.ProjectDocumentUpdateID)).Delete();
            }
        }

        public static void DeleteProjectDocumentUpdate(this IQueryable<ProjectDocumentUpdate> projectDocumentUpdates, ICollection<ProjectDocumentUpdate> projectDocumentUpdatesToDelete)
        {
            if(projectDocumentUpdatesToDelete.Any())
            {
                var projectDocumentUpdateIDList = projectDocumentUpdatesToDelete.Select(x => x.ProjectDocumentUpdateID).ToList();
                projectDocumentUpdates.Where(x => projectDocumentUpdateIDList.Contains(x.ProjectDocumentUpdateID)).Delete();
            }
        }

        public static void DeleteProjectDocumentUpdate(this IQueryable<ProjectDocumentUpdate> projectDocumentUpdates, int projectDocumentUpdateID)
        {
            DeleteProjectDocumentUpdate(projectDocumentUpdates, new List<int> { projectDocumentUpdateID });
        }

        public static void DeleteProjectDocumentUpdate(this IQueryable<ProjectDocumentUpdate> projectDocumentUpdates, ProjectDocumentUpdate projectDocumentUpdateToDelete)
        {
            DeleteProjectDocumentUpdate(projectDocumentUpdates, new List<ProjectDocumentUpdate> { projectDocumentUpdateToDelete });
        }
    }
}