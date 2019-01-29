//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomAttributeUpdate]
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
        public static ProjectCustomAttributeUpdate GetProjectCustomAttributeUpdate(this IQueryable<ProjectCustomAttributeUpdate> projectCustomAttributeUpdates, int projectCustomAttributeUpdateID)
        {
            var projectCustomAttributeUpdate = projectCustomAttributeUpdates.SingleOrDefault(x => x.ProjectCustomAttributeUpdateID == projectCustomAttributeUpdateID);
            Check.RequireNotNullThrowNotFound(projectCustomAttributeUpdate, "ProjectCustomAttributeUpdate", projectCustomAttributeUpdateID);
            return projectCustomAttributeUpdate;
        }

        public static void DeleteProjectCustomAttributeUpdate(this IQueryable<ProjectCustomAttributeUpdate> projectCustomAttributeUpdates, List<int> projectCustomAttributeUpdateIDList)
        {
            if(projectCustomAttributeUpdateIDList.Any())
            {
                projectCustomAttributeUpdates.Where(x => projectCustomAttributeUpdateIDList.Contains(x.ProjectCustomAttributeUpdateID)).Delete();
            }
        }

        public static void DeleteProjectCustomAttributeUpdate(this IQueryable<ProjectCustomAttributeUpdate> projectCustomAttributeUpdates, ICollection<ProjectCustomAttributeUpdate> projectCustomAttributeUpdatesToDelete)
        {
            if(projectCustomAttributeUpdatesToDelete.Any())
            {
                
                var valuesToDelete = projectCustomAttributeUpdatesToDelete.SelectMany(x => x.ProjectCustomAttributeUpdateValues).ToList();
                HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeUpdateValues.DeleteProjectCustomAttributeUpdateValue(valuesToDelete);

                var projectCustomAttributeUpdateIDList = projectCustomAttributeUpdatesToDelete.Select(x => x.ProjectCustomAttributeUpdateID).ToList();
                projectCustomAttributeUpdates.Where(x => projectCustomAttributeUpdateIDList.Contains(x.ProjectCustomAttributeUpdateID)).Delete();
            }
        }

        public static void DeleteProjectCustomAttributeUpdate(this IQueryable<ProjectCustomAttributeUpdate> projectCustomAttributeUpdates, int projectCustomAttributeUpdateID)
        {
            DeleteProjectCustomAttributeUpdate(projectCustomAttributeUpdates, new List<int> { projectCustomAttributeUpdateID });
        }

        public static void DeleteProjectCustomAttributeUpdate(this IQueryable<ProjectCustomAttributeUpdate> projectCustomAttributeUpdates, ProjectCustomAttributeUpdate projectCustomAttributeUpdateToDelete)
        {
            DeleteProjectCustomAttributeUpdate(projectCustomAttributeUpdates, new List<ProjectCustomAttributeUpdate> { projectCustomAttributeUpdateToDelete });
        }
    }
}