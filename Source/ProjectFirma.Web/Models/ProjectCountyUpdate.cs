using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectCountyUpdate : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var county = County != null ? County.DisplayName : ViewUtilities.NotFoundString;
                var projectUpdate = ProjectUpdateBatch != null ? ProjectUpdateBatch.ProjectUpdate.DisplayName : ViewUtilities.NotFoundString;
                return $"County: {county}, {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {projectUpdate}";
            }
        }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            var projectCountyUpdates = project.ProjectCounties.Select(
                projectRegionToClone => new ProjectCountyUpdate(projectUpdateBatch, projectRegionToClone.County)).ToList();
            projectUpdateBatch.ProjectCountyUpdates = projectCountyUpdates;
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectCounty> allProjectCounties)
        {
            var project = projectUpdateBatch.Project;
            var currentProjectCounties = project.ProjectCounties.ToList();
            currentProjectCounties.ForEach(projectCounty=>
            {
                allProjectCounties.Remove(projectCounty);
            });
            currentProjectCounties.Clear();

            if (projectUpdateBatch.ProjectCountyUpdates.Any())
            {
                // Completely rebuild the list
                projectUpdateBatch.ProjectCountyUpdates.ToList().ForEach(x =>
                {
                    var projectCounty = new ProjectCounty(project, x.County);
                    allProjectCounties.Add(projectCounty);
                });
            }

            project.NoCountiesExplanation = projectUpdateBatch.NoCountiesExplanation;
        }
    }
}