namespace ProjectFirma.Web.Models
{
    public partial class InvoicePaymentRequest : IAuditableEntity
    {
        public const string LandOwnerPurchaseAuthority = "Landowner Cost-Share Agreement";

        public string AuditDescriptionString => $"Invoice Payment Request: {InvoicePaymentRequestID} - {InvoicePaymentRequestDate}";

        public string PurchaseAuthorityDisplay => PurchaseAuthorityIsLandownerCostShareAgreement
            ? LandOwnerPurchaseAuthority
            : PurchaseAuthority;
    }
}