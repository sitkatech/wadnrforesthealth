//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPriorityAreaUpdate]
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
        public static ProjectPriorityAreaUpdate GetProjectPriorityAreaUpdate(this IQueryable<ProjectPriorityAreaUpdate> projectPriorityAreaUpdates, int projectPriorityAreaUpdateID)
        {
            var projectPriorityAreaUpdate = projectPriorityAreaUpdates.SingleOrDefault(x => x.ProjectPriorityAreaUpdateID == projectPriorityAreaUpdateID);
            Check.RequireNotNullThrowNotFound(projectPriorityAreaUpdate, "ProjectPriorityAreaUpdate", projectPriorityAreaUpdateID);
            return projectPriorityAreaUpdate;
        }

        public static void DeleteProjectPriorityAreaUpdate(this IQueryable<ProjectPriorityAreaUpdate> projectPriorityAreaUpdates, List<int> projectPriorityAreaUpdateIDList)
        {
            if(projectPriorityAreaUpdateIDList.Any())
            {
                projectPriorityAreaUpdates.Where(x => projectPriorityAreaUpdateIDList.Contains(x.ProjectPriorityAreaUpdateID)).Delete();
            }
        }

        public static void DeleteProjectPriorityAreaUpdate(this IQueryable<ProjectPriorityAreaUpdate> projectPriorityAreaUpdates, ICollection<ProjectPriorityAreaUpdate> projectPriorityAreaUpdatesToDelete)
        {
            if(projectPriorityAreaUpdatesToDelete.Any())
            {
                var projectPriorityAreaUpdateIDList = projectPriorityAreaUpdatesToDelete.Select(x => x.ProjectPriorityAreaUpdateID).ToList();
                projectPriorityAreaUpdates.Where(x => projectPriorityAreaUpdateIDList.Contains(x.ProjectPriorityAreaUpdateID)).Delete();
            }
        }

        public static void DeleteProjectPriorityAreaUpdate(this IQueryable<ProjectPriorityAreaUpdate> projectPriorityAreaUpdates, int projectPriorityAreaUpdateID)
        {
            DeleteProjectPriorityAreaUpdate(projectPriorityAreaUpdates, new List<int> { projectPriorityAreaUpdateID });
        }

        public static void DeleteProjectPriorityAreaUpdate(this IQueryable<ProjectPriorityAreaUpdate> projectPriorityAreaUpdates, ProjectPriorityAreaUpdate projectPriorityAreaUpdateToDelete)
        {
            DeleteProjectPriorityAreaUpdate(projectPriorityAreaUpdates, new List<ProjectPriorityAreaUpdate> { projectPriorityAreaUpdateToDelete });
        }
    }
}