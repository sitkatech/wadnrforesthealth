using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectPriorityLandscape : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var priorityLandscapeDisplayString = ViewUtilities.NotFoundString;
                if (PriorityLandscape is null)
                {
                    var priorityLandscape = HttpRequestStorage.DatabaseEntities.PriorityLandscapes.Find(PriorityLandscapeID);
                    if (priorityLandscape != null)
                    {
                        priorityLandscapeDisplayString = priorityLandscape.DisplayName;
                    }
                }
                else
                {
                    priorityLandscapeDisplayString = PriorityLandscape.DisplayName;
                }

                var projectDisplayString = ViewUtilities.NotFoundString;
                if (Project is null)
                {
                    var project = HttpRequestStorage.DatabaseEntities.Projects.Find(ProjectID);
                    if (project != null)
                    {
                        projectDisplayString = project.DisplayName;
                    }
                }
                else
                {
                    projectDisplayString = Project.DisplayName;
                }

                return $"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()}: {priorityLandscapeDisplayString}, {FieldDefinition.Project.GetFieldDefinitionLabel()}: {projectDisplayString}";
            }
        }
    }
}