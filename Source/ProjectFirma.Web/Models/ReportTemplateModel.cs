namespace ProjectFirma.Web.Models
{
    public partial class ReportTemplateModel : IAuditableEntity
    {
        public string AuditDescriptionString => GetAuditDescriptionString();

        public string GetAuditDescriptionString()
        {
            return $"ReportTemplateModelID: {ReportTemplateModelID}, ReportTemplateModelName: {ReportTemplateModelName}";
        }
    }
}