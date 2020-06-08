//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationRequestUpdate]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ProjectGrantAllocationRequestUpdate GetProjectGrantAllocationRequestUpdate(this IQueryable<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates, int projectGrantAllocationRequestUpdateID)
        {
            var projectGrantAllocationRequestUpdate = projectGrantAllocationRequestUpdates.SingleOrDefault(x => x.ProjectGrantAllocationRequestUpdateID == projectGrantAllocationRequestUpdateID);
            Check.RequireNotNullThrowNotFound(projectGrantAllocationRequestUpdate, "ProjectGrantAllocationRequestUpdate", projectGrantAllocationRequestUpdateID);
            return projectGrantAllocationRequestUpdate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectGrantAllocationRequestUpdate(this IQueryable<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates, List<int> projectGrantAllocationRequestUpdateIDList)
        {
            if(projectGrantAllocationRequestUpdateIDList.Any())
            {
                var projectGrantAllocationRequestUpdatesInSourceCollectionToDelete = projectGrantAllocationRequestUpdates.Where(x => projectGrantAllocationRequestUpdateIDList.Contains(x.ProjectGrantAllocationRequestUpdateID));
                foreach (var projectGrantAllocationRequestUpdateToDelete in projectGrantAllocationRequestUpdatesInSourceCollectionToDelete.ToList())
                {
                    projectGrantAllocationRequestUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectGrantAllocationRequestUpdate(this IQueryable<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates, ICollection<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdatesToDelete)
        {
            if(projectGrantAllocationRequestUpdatesToDelete.Any())
            {
                var projectGrantAllocationRequestUpdateIDList = projectGrantAllocationRequestUpdatesToDelete.Select(x => x.ProjectGrantAllocationRequestUpdateID).ToList();
                var projectGrantAllocationRequestUpdatesToDeleteFromSourceList = projectGrantAllocationRequestUpdates.Where(x => projectGrantAllocationRequestUpdateIDList.Contains(x.ProjectGrantAllocationRequestUpdateID)).ToList();

                foreach (var projectGrantAllocationRequestUpdateToDelete in projectGrantAllocationRequestUpdatesToDeleteFromSourceList)
                {
                    projectGrantAllocationRequestUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectGrantAllocationRequestUpdate(this IQueryable<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates, int projectGrantAllocationRequestUpdateID)
        {
            DeleteProjectGrantAllocationRequestUpdate(projectGrantAllocationRequestUpdates, new List<int> { projectGrantAllocationRequestUpdateID });
        }

        public static void DeleteProjectGrantAllocationRequestUpdate(this IQueryable<ProjectGrantAllocationRequestUpdate> projectGrantAllocationRequestUpdates, ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdateToDelete)
        {
            DeleteProjectGrantAllocationRequestUpdate(projectGrantAllocationRequestUpdates, new List<ProjectGrantAllocationRequestUpdate> { projectGrantAllocationRequestUpdateToDelete });
        }
    }
}