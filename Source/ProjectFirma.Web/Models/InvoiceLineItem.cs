namespace ProjectFirma.Web.Models
{
    public partial class InvoiceLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => $"Invoice Line Item";
    }
}