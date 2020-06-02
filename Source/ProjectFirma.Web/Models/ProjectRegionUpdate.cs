using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectRegionUpdate : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var region = DNRUplandRegion != null ? DNRUplandRegion.DisplayName : ViewUtilities.NotFoundString;
                var projectUpdate = ProjectUpdateBatch != null ? ProjectUpdateBatch.ProjectUpdate.DisplayName : ViewUtilities.NotFoundString;
                return $"Region: {region}, {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {projectUpdate}";
            }
        }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            var projectRegionUpdates = project.ProjectRegions.Select(
                projectRegionToClone => new ProjectRegionUpdate(projectUpdateBatch, projectRegionToClone.DNRUplandRegion)).ToList();
            projectUpdateBatch.ProjectRegionUpdates = projectRegionUpdates;
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectRegion> allProjectRegions)
        {
            var project = projectUpdateBatch.Project;
            var currentProjectRegions = project.ProjectRegions.ToList();
            currentProjectRegions.ForEach(projectRegion =>
            {
                allProjectRegions.Remove(projectRegion);
            });
            currentProjectRegions.Clear();

            if (projectUpdateBatch.ProjectRegionUpdates.Any())
            {
                // Completely rebuild the list
                projectUpdateBatch.ProjectRegionUpdates.ToList().ForEach(x =>
                {
                    var projectRegion = new ProjectRegion(project, x.DNRUplandRegion);
                    allProjectRegions.Add(projectRegion);
                });
            }
        }
    }
}