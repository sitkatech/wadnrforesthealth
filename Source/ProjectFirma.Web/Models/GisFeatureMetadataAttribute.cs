namespace ProjectFirma.Web.Models
{
    public partial class GisFeatureMetadataAttribute : IAuditableEntity
    {
        public string AuditDescriptionString => $"GisFeatureID: {GisFeatureID}; GisMetadataAttributeID: {GisMetadataAttributeID}";
    }
}