//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectImportBlacklist]
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
        public static ProjectImportBlacklist GetProjectImportBlacklist(this IQueryable<ProjectImportBlacklist> projectImportBlacklists, int projectImportBlacklistID)
        {
            var projectImportBlacklist = projectImportBlacklists.SingleOrDefault(x => x.ProjectImportBlacklistID == projectImportBlacklistID);
            Check.RequireNotNullThrowNotFound(projectImportBlacklist, "ProjectImportBlacklist", projectImportBlacklistID);
            return projectImportBlacklist;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectImportBlacklist(this IQueryable<ProjectImportBlacklist> projectImportBlacklists, List<int> projectImportBlacklistIDList)
        {
            if(projectImportBlacklistIDList.Any())
            {
                var projectImportBlacklistsInSourceCollectionToDelete = projectImportBlacklists.Where(x => projectImportBlacklistIDList.Contains(x.ProjectImportBlacklistID));
                foreach (var projectImportBlacklistToDelete in projectImportBlacklistsInSourceCollectionToDelete.ToList())
                {
                    projectImportBlacklistToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectImportBlacklist(this IQueryable<ProjectImportBlacklist> projectImportBlacklists, ICollection<ProjectImportBlacklist> projectImportBlacklistsToDelete)
        {
            if(projectImportBlacklistsToDelete.Any())
            {
                var projectImportBlacklistIDList = projectImportBlacklistsToDelete.Select(x => x.ProjectImportBlacklistID).ToList();
                var projectImportBlacklistsToDeleteFromSourceList = projectImportBlacklists.Where(x => projectImportBlacklistIDList.Contains(x.ProjectImportBlacklistID)).ToList();

                foreach (var projectImportBlacklistToDelete in projectImportBlacklistsToDeleteFromSourceList)
                {
                    projectImportBlacklistToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectImportBlacklist(this IQueryable<ProjectImportBlacklist> projectImportBlacklists, int projectImportBlacklistID)
        {
            DeleteProjectImportBlacklist(projectImportBlacklists, new List<int> { projectImportBlacklistID });
        }

        public static void DeleteProjectImportBlacklist(this IQueryable<ProjectImportBlacklist> projectImportBlacklists, ProjectImportBlacklist projectImportBlacklistToDelete)
        {
            DeleteProjectImportBlacklist(projectImportBlacklists, new List<ProjectImportBlacklist> { projectImportBlacklistToDelete });
        }
    }
}