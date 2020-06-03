using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectPriorityLandscapeUpdate : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var priorityLandscape = PriorityLandscape != null ? PriorityLandscape.DisplayName : ViewUtilities.NotFoundString;
                var projectUpdate = ProjectUpdateBatch != null ? ProjectUpdateBatch.ProjectUpdate.DisplayName : ViewUtilities.NotFoundString;
                return $"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()}: {priorityLandscape}, {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {projectUpdate}";
            }
        }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            var projectPriorityLandscapeUpdates = project.ProjectPriorityLandscapes.Select(
                projectPriorityLandscapeToClone => new ProjectPriorityLandscapeUpdate(projectUpdateBatch, projectPriorityLandscapeToClone.PriorityLandscape)).ToList();
            projectUpdateBatch.ProjectPriorityLandscapeUpdates = projectPriorityLandscapeUpdates;
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectPriorityLandscape> allProjectPriorityLandscapes)
        {
            var project = projectUpdateBatch.Project;
            var currentProjectPriorityLandscapes = project.ProjectPriorityLandscapes.ToList();
            currentProjectPriorityLandscapes.ForEach(projectPriorityLandscape =>
            {
                allProjectPriorityLandscapes.Remove(projectPriorityLandscape);
            });
            currentProjectPriorityLandscapes.Clear();

            if (projectUpdateBatch.ProjectPriorityLandscapeUpdates.Any())
            {
                // Completely rebuild the list
                projectUpdateBatch.ProjectPriorityLandscapeUpdates.ToList().ForEach(x =>
                {
                    var projectPriorityLandscape = new ProjectPriorityLandscape(project, x.PriorityLandscape);
                    allProjectPriorityLandscapes.Add(projectPriorityLandscape);
                });
            }

            project.NoPriorityLandscapesExplanation = projectUpdateBatch.NoPriorityLandscapesExplanation;
        }
    }
}