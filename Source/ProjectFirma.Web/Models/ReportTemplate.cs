using System.Data.Entity.Core;
using System.Linq;
using ProjectFirma.Web.Common;

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

        public static ReportTemplate GetApprovalLetterTemplate()
        {
            var approvalLetterTemplate = HttpRequestStorage.DatabaseEntities.ReportTemplates.Single(x => x.IsSystemTemplate && x.DisplayName == "Financial Assistance Approval Letter");
            return approvalLetterTemplate;
        }

        public static ReportTemplate GetInvoicePaymentRequestTemplate()
        {
            var invoicePaymentRequestTemplate = HttpRequestStorage.DatabaseEntities.ReportTemplates.Single(x => x.IsSystemTemplate && x.DisplayName == "Invoice Payment Request");
            return invoicePaymentRequestTemplate;
        }
    }
}