//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGeospatialAreaUpdate]
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
        public static ProjectGeospatialAreaUpdate GetProjectGeospatialAreaUpdate(this IQueryable<ProjectGeospatialAreaUpdate> projectGeospatialAreaUpdates, int projectGeospatialAreaUpdateID)
        {
            var projectGeospatialAreaUpdate = projectGeospatialAreaUpdates.SingleOrDefault(x => x.ProjectGeospatialAreaUpdateID == projectGeospatialAreaUpdateID);
            Check.RequireNotNullThrowNotFound(projectGeospatialAreaUpdate, "ProjectGeospatialAreaUpdate", projectGeospatialAreaUpdateID);
            return projectGeospatialAreaUpdate;
        }

        public static void DeleteProjectGeospatialAreaUpdate(this IQueryable<ProjectGeospatialAreaUpdate> projectGeospatialAreaUpdates, List<int> projectGeospatialAreaUpdateIDList)
        {
            if(projectGeospatialAreaUpdateIDList.Any())
            {
                projectGeospatialAreaUpdates.Where(x => projectGeospatialAreaUpdateIDList.Contains(x.ProjectGeospatialAreaUpdateID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialAreaUpdate(this IQueryable<ProjectGeospatialAreaUpdate> projectGeospatialAreaUpdates, ICollection<ProjectGeospatialAreaUpdate> projectGeospatialAreaUpdatesToDelete)
        {
            if(projectGeospatialAreaUpdatesToDelete.Any())
            {
                var projectGeospatialAreaUpdateIDList = projectGeospatialAreaUpdatesToDelete.Select(x => x.ProjectGeospatialAreaUpdateID).ToList();
                projectGeospatialAreaUpdates.Where(x => projectGeospatialAreaUpdateIDList.Contains(x.ProjectGeospatialAreaUpdateID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialAreaUpdate(this IQueryable<ProjectGeospatialAreaUpdate> projectGeospatialAreaUpdates, int projectGeospatialAreaUpdateID)
        {
            DeleteProjectGeospatialAreaUpdate(projectGeospatialAreaUpdates, new List<int> { projectGeospatialAreaUpdateID });
        }

        public static void DeleteProjectGeospatialAreaUpdate(this IQueryable<ProjectGeospatialAreaUpdate> projectGeospatialAreaUpdates, ProjectGeospatialAreaUpdate projectGeospatialAreaUpdateToDelete)
        {
            DeleteProjectGeospatialAreaUpdate(projectGeospatialAreaUpdates, new List<ProjectGeospatialAreaUpdate> { projectGeospatialAreaUpdateToDelete });
        }
    }
}