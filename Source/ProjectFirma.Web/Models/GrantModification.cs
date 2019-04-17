namespace ProjectFirma.Web.Models
{
    public partial class GrantModification : IAuditableEntity
    {
        public string StartDateDisplay => GrantModificationStartDate.ToShortDateString();
        public string EndDateDisplay => GrantModificationEndDate.ToShortDateString();
        public string AuditDescriptionString => GrantModificationName;
    }
}