//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPersonUpdate]
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
        public static ProjectPersonUpdate GetProjectPersonUpdate(this IQueryable<ProjectPersonUpdate> projectPersonUpdates, int projectPersonUpdateID)
        {
            var projectPersonUpdate = projectPersonUpdates.SingleOrDefault(x => x.ProjectPersonUpdateID == projectPersonUpdateID);
            Check.RequireNotNullThrowNotFound(projectPersonUpdate, "ProjectPersonUpdate", projectPersonUpdateID);
            return projectPersonUpdate;
        }

        public static void DeleteProjectPersonUpdate(this IQueryable<ProjectPersonUpdate> projectPersonUpdates, List<int> projectPersonUpdateIDList)
        {
            if(projectPersonUpdateIDList.Any())
            {
                projectPersonUpdates.Where(x => projectPersonUpdateIDList.Contains(x.ProjectPersonUpdateID)).Delete();
            }
        }

        public static void DeleteProjectPersonUpdate(this IQueryable<ProjectPersonUpdate> projectPersonUpdates, ICollection<ProjectPersonUpdate> projectPersonUpdatesToDelete)
        {
            if(projectPersonUpdatesToDelete.Any())
            {
                var projectPersonUpdateIDList = projectPersonUpdatesToDelete.Select(x => x.ProjectPersonUpdateID).ToList();
                projectPersonUpdates.Where(x => projectPersonUpdateIDList.Contains(x.ProjectPersonUpdateID)).Delete();
            }
        }

        public static void DeleteProjectPersonUpdate(this IQueryable<ProjectPersonUpdate> projectPersonUpdates, int projectPersonUpdateID)
        {
            DeleteProjectPersonUpdate(projectPersonUpdates, new List<int> { projectPersonUpdateID });
        }

        public static void DeleteProjectPersonUpdate(this IQueryable<ProjectPersonUpdate> projectPersonUpdates, ProjectPersonUpdate projectPersonUpdateToDelete)
        {
            DeleteProjectPersonUpdate(projectPersonUpdates, new List<ProjectPersonUpdate> { projectPersonUpdateToDelete });
        }
    }
}