namespace ProjectFirma.Web.Models
{
    public partial class GisUploadAttemptGisMetadataAttribute : IAuditableEntity
    {
        public string AuditDescriptionString => $"GisMetadataAttributeID: {GisMetadataAttributeID}; GisUploadAttemptID: {GisUploadAttemptID}";
    }
}