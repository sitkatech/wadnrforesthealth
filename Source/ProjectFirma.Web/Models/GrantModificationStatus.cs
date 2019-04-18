namespace ProjectFirma.Web.Models
{
    public partial class GrantModificationStatus : IAuditableEntity
    {

        public string AuditDescriptionString => GrantModificationStatusName;

    }
}