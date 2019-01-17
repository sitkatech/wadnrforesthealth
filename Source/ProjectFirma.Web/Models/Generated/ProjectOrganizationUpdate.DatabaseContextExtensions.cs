//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectOrganizationUpdate]
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
        public static ProjectOrganizationUpdate GetProjectOrganizationUpdate(this IQueryable<ProjectOrganizationUpdate> projectOrganizationUpdates, int projectOrganizationUpdateID)
        {
            var projectOrganizationUpdate = projectOrganizationUpdates.SingleOrDefault(x => x.ProjectOrganizationUpdateID == projectOrganizationUpdateID);
            Check.RequireNotNullThrowNotFound(projectOrganizationUpdate, "ProjectOrganizationUpdate", projectOrganizationUpdateID);
            return projectOrganizationUpdate;
        }

        public static void DeleteProjectOrganizationUpdate(this IQueryable<ProjectOrganizationUpdate> projectOrganizationUpdates, List<int> projectOrganizationUpdateIDList)
        {
            if(projectOrganizationUpdateIDList.Any())
            {
                projectOrganizationUpdates.Where(x => projectOrganizationUpdateIDList.Contains(x.ProjectOrganizationUpdateID)).Delete();
            }
        }

        public static void DeleteProjectOrganizationUpdate(this IQueryable<ProjectOrganizationUpdate> projectOrganizationUpdates, ICollection<ProjectOrganizationUpdate> projectOrganizationUpdatesToDelete)
        {
            if(projectOrganizationUpdatesToDelete.Any())
            {
                var projectOrganizationUpdateIDList = projectOrganizationUpdatesToDelete.Select(x => x.ProjectOrganizationUpdateID).ToList();
                projectOrganizationUpdates.Where(x => projectOrganizationUpdateIDList.Contains(x.ProjectOrganizationUpdateID)).Delete();
            }
        }

        public static void DeleteProjectOrganizationUpdate(this IQueryable<ProjectOrganizationUpdate> projectOrganizationUpdates, int projectOrganizationUpdateID)
        {
            DeleteProjectOrganizationUpdate(projectOrganizationUpdates, new List<int> { projectOrganizationUpdateID });
        }

        public static void DeleteProjectOrganizationUpdate(this IQueryable<ProjectOrganizationUpdate> projectOrganizationUpdates, ProjectOrganizationUpdate projectOrganizationUpdateToDelete)
        {
            DeleteProjectOrganizationUpdate(projectOrganizationUpdates, new List<ProjectOrganizationUpdate> { projectOrganizationUpdateToDelete });
        }
    }
}