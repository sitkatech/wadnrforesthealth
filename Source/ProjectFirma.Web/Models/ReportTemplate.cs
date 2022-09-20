namespace ProjectFirma.Web.Models
{
    public partial class ReportTemplate : IAuditableEntity
    {
        public string AuditDescriptionString => GetAuditDescriptionString();

        public string GetAuditDescriptionString()
        {
            return $"ReportTemplateID: {ReportTemplateID}, DisplayName: {DisplayName}";
        }

        public void DeleteFullWithFileResource(DatabaseEntities dbContext)
        {
            // Looks kind of weird but it will cascade delete the ReportTemplate
            FileResource.DeleteFull(dbContext);
        }
    }
}