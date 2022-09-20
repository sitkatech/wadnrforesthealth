//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomGridConfiguration]
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
        public static ProjectCustomGridConfiguration GetProjectCustomGridConfiguration(this IQueryable<ProjectCustomGridConfiguration> projectCustomGridConfigurations, int projectCustomGridConfigurationID)
        {
            var projectCustomGridConfiguration = projectCustomGridConfigurations.SingleOrDefault(x => x.ProjectCustomGridConfigurationID == projectCustomGridConfigurationID);
            Check.RequireNotNullThrowNotFound(projectCustomGridConfiguration, "ProjectCustomGridConfiguration", projectCustomGridConfigurationID);
            return projectCustomGridConfiguration;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectCustomGridConfiguration(this IQueryable<ProjectCustomGridConfiguration> projectCustomGridConfigurations, List<int> projectCustomGridConfigurationIDList)
        {
            if(projectCustomGridConfigurationIDList.Any())
            {
                var projectCustomGridConfigurationsInSourceCollectionToDelete = projectCustomGridConfigurations.Where(x => projectCustomGridConfigurationIDList.Contains(x.ProjectCustomGridConfigurationID));
                foreach (var projectCustomGridConfigurationToDelete in projectCustomGridConfigurationsInSourceCollectionToDelete.ToList())
                {
                    projectCustomGridConfigurationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectCustomGridConfiguration(this IQueryable<ProjectCustomGridConfiguration> projectCustomGridConfigurations, ICollection<ProjectCustomGridConfiguration> projectCustomGridConfigurationsToDelete)
        {
            if(projectCustomGridConfigurationsToDelete.Any())
            {
                var projectCustomGridConfigurationIDList = projectCustomGridConfigurationsToDelete.Select(x => x.ProjectCustomGridConfigurationID).ToList();
                var projectCustomGridConfigurationsToDeleteFromSourceList = projectCustomGridConfigurations.Where(x => projectCustomGridConfigurationIDList.Contains(x.ProjectCustomGridConfigurationID)).ToList();

                foreach (var projectCustomGridConfigurationToDelete in projectCustomGridConfigurationsToDeleteFromSourceList)
                {
                    projectCustomGridConfigurationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectCustomGridConfiguration(this IQueryable<ProjectCustomGridConfiguration> projectCustomGridConfigurations, int projectCustomGridConfigurationID)
        {
            DeleteProjectCustomGridConfiguration(projectCustomGridConfigurations, new List<int> { projectCustomGridConfigurationID });
        }

        public static void DeleteProjectCustomGridConfiguration(this IQueryable<ProjectCustomGridConfiguration> projectCustomGridConfigurations, ProjectCustomGridConfiguration projectCustomGridConfigurationToDelete)
        {
            DeleteProjectCustomGridConfiguration(projectCustomGridConfigurations, new List<ProjectCustomGridConfiguration> { projectCustomGridConfigurationToDelete });
        }
    }
}