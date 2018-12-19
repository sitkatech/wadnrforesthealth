//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPersonUpdate]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ProjectPersonUpdate GetProjectPersonUpdate(this IQueryable<ProjectPersonUpdate> projectPersonUpdates, int projectPersonUpdateID)
        {
            var projectPersonUpdate = projectPersonUpdates.SingleOrDefault(x => x.ProjectPersonUpdateID == projectPersonUpdateID);
            Check.RequireNotNullThrowNotFound(projectPersonUpdate, "ProjectPersonUpdate", projectPersonUpdateID);
            return projectPersonUpdate;
        }

        public static void DeleteProjectPersonUpdate(this List<int> projectPersonUpdateIDList)
        {
            if(projectPersonUpdateIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllProjectPersonUpdates.RemoveRange(HttpRequestStorage.DatabaseEntities.ProjectPersonUpdates.Where(x => projectPersonUpdateIDList.Contains(x.ProjectPersonUpdateID)));
            }
        }

        public static void DeleteProjectPersonUpdate(this ICollection<ProjectPersonUpdate> projectPersonUpdatesToDelete)
        {
            if(projectPersonUpdatesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllProjectPersonUpdates.RemoveRange(projectPersonUpdatesToDelete);
            }
        }

        public static void DeleteProjectPersonUpdate(this int projectPersonUpdateID)
        {
            DeleteProjectPersonUpdate(new List<int> { projectPersonUpdateID });
        }

        public static void DeleteProjectPersonUpdate(this ProjectPersonUpdate projectPersonUpdateToDelete)
        {
            DeleteProjectPersonUpdate(new List<ProjectPersonUpdate> { projectPersonUpdateToDelete });
        }
    }
}