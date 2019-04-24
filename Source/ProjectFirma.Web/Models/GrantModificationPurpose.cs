namespace ProjectFirma.Web.Models
{
    public partial class GrantModificationPurpose : IAuditableEntity
    {

        public string AuditDescriptionString => GrantModificationPurposeName;

    }
}