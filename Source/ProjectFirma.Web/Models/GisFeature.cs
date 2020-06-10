namespace ProjectFirma.Web.Models
{
    public partial class GisFeature : IAuditableEntity
    {
        public string AuditDescriptionString => $"GisUploadAttemptID: {GisUploadAttemptID}";
    }
}