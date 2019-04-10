//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationExpenditureUpdate]
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
        public static ProjectGrantAllocationExpenditureUpdate GetProjectGrantAllocationExpenditureUpdate(this IQueryable<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates, int projectGrantAllocationExpenditureUpdateID)
        {
            var projectGrantAllocationExpenditureUpdate = projectGrantAllocationExpenditureUpdates.SingleOrDefault(x => x.ProjectGrantAllocationExpenditureUpdateID == projectGrantAllocationExpenditureUpdateID);
            Check.RequireNotNullThrowNotFound(projectGrantAllocationExpenditureUpdate, "ProjectGrantAllocationExpenditureUpdate", projectGrantAllocationExpenditureUpdateID);
            return projectGrantAllocationExpenditureUpdate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectGrantAllocationExpenditureUpdate(this IQueryable<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates, List<int> projectGrantAllocationExpenditureUpdateIDList)
        {
            if(projectGrantAllocationExpenditureUpdateIDList.Any())
            {
                var projectGrantAllocationExpenditureUpdatesInSourceCollectionToDelete = projectGrantAllocationExpenditureUpdates.Where(x => projectGrantAllocationExpenditureUpdateIDList.Contains(x.ProjectGrantAllocationExpenditureUpdateID));
                foreach (var projectGrantAllocationExpenditureUpdateToDelete in projectGrantAllocationExpenditureUpdatesInSourceCollectionToDelete.ToList())
                {
                    projectGrantAllocationExpenditureUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectGrantAllocationExpenditureUpdate(this IQueryable<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates, ICollection<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdatesToDelete)
        {
            if(projectGrantAllocationExpenditureUpdatesToDelete.Any())
            {
                var projectGrantAllocationExpenditureUpdateIDList = projectGrantAllocationExpenditureUpdatesToDelete.Select(x => x.ProjectGrantAllocationExpenditureUpdateID).ToList();
                var projectGrantAllocationExpenditureUpdatesToDeleteFromSourceList = projectGrantAllocationExpenditureUpdates.Where(x => projectGrantAllocationExpenditureUpdateIDList.Contains(x.ProjectGrantAllocationExpenditureUpdateID)).ToList();

                foreach (var projectGrantAllocationExpenditureUpdateToDelete in projectGrantAllocationExpenditureUpdatesToDeleteFromSourceList)
                {
                    projectGrantAllocationExpenditureUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectGrantAllocationExpenditureUpdate(this IQueryable<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates, int projectGrantAllocationExpenditureUpdateID)
        {
            DeleteProjectGrantAllocationExpenditureUpdate(projectGrantAllocationExpenditureUpdates, new List<int> { projectGrantAllocationExpenditureUpdateID });
        }

        public static void DeleteProjectGrantAllocationExpenditureUpdate(this IQueryable<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates, ProjectGrantAllocationExpenditureUpdate projectGrantAllocationExpenditureUpdateToDelete)
        {
            DeleteProjectGrantAllocationExpenditureUpdate(projectGrantAllocationExpenditureUpdates, new List<ProjectGrantAllocationExpenditureUpdate> { projectGrantAllocationExpenditureUpdateToDelete });
        }
    }
}