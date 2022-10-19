namespace ProjectFirma.Web.Models
{
    public partial class InvoicePaymentRequest : IAuditableEntity
    {
        public string AuditDescriptionString => $"Invoice Payment Request: {InvoicePaymentRequestID} - {InvoicePaymentRequestDate}";
    }
}