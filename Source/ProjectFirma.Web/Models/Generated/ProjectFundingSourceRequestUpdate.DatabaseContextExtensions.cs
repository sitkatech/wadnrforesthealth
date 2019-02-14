//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectFundingSourceRequestUpdate]
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
        public static ProjectFundingSourceRequestUpdate GetProjectFundingSourceRequestUpdate(this IQueryable<ProjectFundingSourceRequestUpdate> projectFundingSourceRequestUpdates, int projectFundingSourceRequestUpdateID)
        {
            var projectFundingSourceRequestUpdate = projectFundingSourceRequestUpdates.SingleOrDefault(x => x.ProjectFundingSourceRequestUpdateID == projectFundingSourceRequestUpdateID);
            Check.RequireNotNullThrowNotFound(projectFundingSourceRequestUpdate, "ProjectFundingSourceRequestUpdate", projectFundingSourceRequestUpdateID);
            return projectFundingSourceRequestUpdate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectFundingSourceRequestUpdate(this IQueryable<ProjectFundingSourceRequestUpdate> projectFundingSourceRequestUpdates, List<int> projectFundingSourceRequestUpdateIDList)
        {
            if(projectFundingSourceRequestUpdateIDList.Any())
            {
                var projectFundingSourceRequestUpdatesInSourceCollectionToDelete = projectFundingSourceRequestUpdates.Where(x => projectFundingSourceRequestUpdateIDList.Contains(x.ProjectFundingSourceRequestUpdateID));
                foreach (var projectFundingSourceRequestUpdateToDelete in projectFundingSourceRequestUpdatesInSourceCollectionToDelete.ToList())
                {
                    projectFundingSourceRequestUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectFundingSourceRequestUpdate(this IQueryable<ProjectFundingSourceRequestUpdate> projectFundingSourceRequestUpdates, ICollection<ProjectFundingSourceRequestUpdate> projectFundingSourceRequestUpdatesToDelete)
        {
            if(projectFundingSourceRequestUpdatesToDelete.Any())
            {
                var projectFundingSourceRequestUpdateIDList = projectFundingSourceRequestUpdatesToDelete.Select(x => x.ProjectFundingSourceRequestUpdateID).ToList();
                var projectFundingSourceRequestUpdatesToDeleteFromSourceList = projectFundingSourceRequestUpdates.Where(x => projectFundingSourceRequestUpdateIDList.Contains(x.ProjectFundingSourceRequestUpdateID)).ToList();

                foreach (var projectFundingSourceRequestUpdateToDelete in projectFundingSourceRequestUpdatesToDeleteFromSourceList)
                {
                    projectFundingSourceRequestUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectFundingSourceRequestUpdate(this IQueryable<ProjectFundingSourceRequestUpdate> projectFundingSourceRequestUpdates, int projectFundingSourceRequestUpdateID)
        {
            DeleteProjectFundingSourceRequestUpdate(projectFundingSourceRequestUpdates, new List<int> { projectFundingSourceRequestUpdateID });
        }

        public static void DeleteProjectFundingSourceRequestUpdate(this IQueryable<ProjectFundingSourceRequestUpdate> projectFundingSourceRequestUpdates, ProjectFundingSourceRequestUpdate projectFundingSourceRequestUpdateToDelete)
        {
            DeleteProjectFundingSourceRequestUpdate(projectFundingSourceRequestUpdates, new List<ProjectFundingSourceRequestUpdate> { projectFundingSourceRequestUpdateToDelete });
        }
    }
}