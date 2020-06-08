//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectExemptReportingYearUpdate]
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
        public static ProjectExemptReportingYearUpdate GetProjectExemptReportingYearUpdate(this IQueryable<ProjectExemptReportingYearUpdate> projectExemptReportingYearUpdates, int projectExemptReportingYearUpdateID)
        {
            var projectExemptReportingYearUpdate = projectExemptReportingYearUpdates.SingleOrDefault(x => x.ProjectExemptReportingYearUpdateID == projectExemptReportingYearUpdateID);
            Check.RequireNotNullThrowNotFound(projectExemptReportingYearUpdate, "ProjectExemptReportingYearUpdate", projectExemptReportingYearUpdateID);
            return projectExemptReportingYearUpdate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectExemptReportingYearUpdate(this IQueryable<ProjectExemptReportingYearUpdate> projectExemptReportingYearUpdates, List<int> projectExemptReportingYearUpdateIDList)
        {
            if(projectExemptReportingYearUpdateIDList.Any())
            {
                var projectExemptReportingYearUpdatesInSourceCollectionToDelete = projectExemptReportingYearUpdates.Where(x => projectExemptReportingYearUpdateIDList.Contains(x.ProjectExemptReportingYearUpdateID));
                foreach (var projectExemptReportingYearUpdateToDelete in projectExemptReportingYearUpdatesInSourceCollectionToDelete.ToList())
                {
                    projectExemptReportingYearUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectExemptReportingYearUpdate(this IQueryable<ProjectExemptReportingYearUpdate> projectExemptReportingYearUpdates, ICollection<ProjectExemptReportingYearUpdate> projectExemptReportingYearUpdatesToDelete)
        {
            if(projectExemptReportingYearUpdatesToDelete.Any())
            {
                var projectExemptReportingYearUpdateIDList = projectExemptReportingYearUpdatesToDelete.Select(x => x.ProjectExemptReportingYearUpdateID).ToList();
                var projectExemptReportingYearUpdatesToDeleteFromSourceList = projectExemptReportingYearUpdates.Where(x => projectExemptReportingYearUpdateIDList.Contains(x.ProjectExemptReportingYearUpdateID)).ToList();

                foreach (var projectExemptReportingYearUpdateToDelete in projectExemptReportingYearUpdatesToDeleteFromSourceList)
                {
                    projectExemptReportingYearUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectExemptReportingYearUpdate(this IQueryable<ProjectExemptReportingYearUpdate> projectExemptReportingYearUpdates, int projectExemptReportingYearUpdateID)
        {
            DeleteProjectExemptReportingYearUpdate(projectExemptReportingYearUpdates, new List<int> { projectExemptReportingYearUpdateID });
        }

        public static void DeleteProjectExemptReportingYearUpdate(this IQueryable<ProjectExemptReportingYearUpdate> projectExemptReportingYearUpdates, ProjectExemptReportingYearUpdate projectExemptReportingYearUpdateToDelete)
        {
            DeleteProjectExemptReportingYearUpdate(projectExemptReportingYearUpdates, new List<ProjectExemptReportingYearUpdate> { projectExemptReportingYearUpdateToDelete });
        }
    }
}