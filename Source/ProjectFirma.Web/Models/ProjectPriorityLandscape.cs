using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectPriorityLandscape : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var priorityLandscape = PriorityLandscape != null ? PriorityLandscape.DisplayName : ViewUtilities.NotFoundString;
                var project= Project != null ? Project.DisplayName : ViewUtilities.NotFoundString;
                return $"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()}: {priorityLandscape}, {FieldDefinition.Project.GetFieldDefinitionLabel()}: {project}";
            }
        }
    }
}