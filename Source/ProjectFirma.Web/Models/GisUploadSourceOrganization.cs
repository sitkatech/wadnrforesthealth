namespace ProjectFirma.Web.Models
{
    public partial class GisUploadSourceOrganization : IAuditableEntity
    {
        public string AuditDescriptionString => GisUploadSourceOrganizationName;
    }
}