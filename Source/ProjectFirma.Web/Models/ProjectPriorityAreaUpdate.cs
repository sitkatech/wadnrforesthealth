using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectPriorityAreaUpdate : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var priorityArea = PriorityArea != null ? PriorityArea.DisplayName : ViewUtilities.NotFoundString;
                var projectUpdate = ProjectUpdateBatch != null ? ProjectUpdateBatch.ProjectUpdate.DisplayName : ViewUtilities.NotFoundString;
                return $"Priority Area: {priorityArea}, {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {projectUpdate}";
            }
        }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            var projectPriorityAreaUpdates = project.ProjectPriorityAreas.Select(
                projectPriorityAreaToClone => new ProjectPriorityAreaUpdate(projectUpdateBatch, projectPriorityAreaToClone.PriorityArea)).ToList();
            projectUpdateBatch.ProjectPriorityAreaUpdates = projectPriorityAreaUpdates;
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectPriorityArea> allProjectPriorityAreas)
        {
            var project = projectUpdateBatch.Project;
            var currentProjectPriorityAreas = project.ProjectPriorityAreas.ToList();
            currentProjectPriorityAreas.ForEach(projectPriorityArea =>
            {
                allProjectPriorityAreas.Remove(projectPriorityArea);
            });
            currentProjectPriorityAreas.Clear();

            if (projectUpdateBatch.ProjectPriorityAreaUpdates.Any())
            {
                // Completely rebuild the list
                projectUpdateBatch.ProjectPriorityAreaUpdates.ToList().ForEach(x =>
                {
                    var projectPriorityArea = new ProjectPriorityArea(project, x.PriorityArea);
                    allProjectPriorityAreas.Add(projectPriorityArea);
                });
            }

            project.NoPriorityAreasExplanation = projectUpdateBatch.NoPriorityAreasExplanation;
        }
    }
}