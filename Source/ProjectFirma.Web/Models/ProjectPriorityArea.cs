using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectPriorityArea : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var priorityArea = PriorityArea != null ? PriorityArea.DisplayName : ViewUtilities.NotFoundString;
                var project= Project != null ? Project.DisplayName : ViewUtilities.NotFoundString;
                return $"Priority Area: {priorityArea}, {FieldDefinition.Project.GetFieldDefinitionLabel()}: {project}";
            }
        }
    }
}