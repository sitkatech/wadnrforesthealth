namespace ProjectFirma.Web.Models
{
    public partial class ReportTemplateModelType : IAuditableEntity
    {
        public string AuditDescriptionString => GetAuditDescriptionString();

        public string GetAuditDescriptionString()
        {
            return $"ReportTemplateModelTypeID: {ReportTemplateModelTypeID}, ReportTemplateModelTypeName: {ReportTemplateModelTypeName}";
        }
    }
}