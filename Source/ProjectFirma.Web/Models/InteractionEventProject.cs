namespace ProjectFirma.Web.Models
{
    public partial class InteractionEventProject : IAuditableEntity
    {
        public string AuditDescriptionString => $"Interaction Event {InteractionEvent?.InteractionEventTitle ?? "(Interaction Event not found)"} connected to Project {Project?.DisplayName ?? "(Project not found)"}";
    }
}