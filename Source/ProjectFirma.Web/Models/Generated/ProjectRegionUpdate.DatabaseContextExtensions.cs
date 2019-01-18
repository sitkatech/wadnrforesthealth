//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectRegionUpdate]
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
        public static ProjectRegionUpdate GetProjectRegionUpdate(this IQueryable<ProjectRegionUpdate> projectRegionUpdates, int projectRegionUpdateID)
        {
            var projectRegionUpdate = projectRegionUpdates.SingleOrDefault(x => x.ProjectRegionUpdateID == projectRegionUpdateID);
            Check.RequireNotNullThrowNotFound(projectRegionUpdate, "ProjectRegionUpdate", projectRegionUpdateID);
            return projectRegionUpdate;
        }

        public static void DeleteProjectRegionUpdate(this IQueryable<ProjectRegionUpdate> projectRegionUpdates, List<int> projectRegionUpdateIDList)
        {
            if(projectRegionUpdateIDList.Any())
            {
                projectRegionUpdates.Where(x => projectRegionUpdateIDList.Contains(x.ProjectRegionUpdateID)).Delete();
            }
        }

        public static void DeleteProjectRegionUpdate(this IQueryable<ProjectRegionUpdate> projectRegionUpdates, ICollection<ProjectRegionUpdate> projectRegionUpdatesToDelete)
        {
            if(projectRegionUpdatesToDelete.Any())
            {
                var projectRegionUpdateIDList = projectRegionUpdatesToDelete.Select(x => x.ProjectRegionUpdateID).ToList();
                projectRegionUpdates.Where(x => projectRegionUpdateIDList.Contains(x.ProjectRegionUpdateID)).Delete();
            }
        }

        public static void DeleteProjectRegionUpdate(this IQueryable<ProjectRegionUpdate> projectRegionUpdates, int projectRegionUpdateID)
        {
            DeleteProjectRegionUpdate(projectRegionUpdates, new List<int> { projectRegionUpdateID });
        }

        public static void DeleteProjectRegionUpdate(this IQueryable<ProjectRegionUpdate> projectRegionUpdates, ProjectRegionUpdate projectRegionUpdateToDelete)
        {
            DeleteProjectRegionUpdate(projectRegionUpdates, new List<ProjectRegionUpdate> { projectRegionUpdateToDelete });
        }
    }
}