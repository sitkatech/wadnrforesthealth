namespace ProjectFirma.Web.Models
{
    public partial class InteractionEventContact : IAuditableEntity
    {
        public string AuditDescriptionString => $"Interaction Event {InteractionEvent?.InteractionEventTitle ?? "(Interaction Event not found)"} connected to Contact {Person?.FirstName ?? "(Contact not found)"}";
    }
}