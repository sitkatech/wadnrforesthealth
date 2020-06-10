namespace ProjectFirma.Web.Models
{
    public partial class GisMetadataAttribute : IAuditableEntity
    {
        public string AuditDescriptionString => GisMetadataAttributeName;
    }
}