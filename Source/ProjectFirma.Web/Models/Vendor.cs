namespace ProjectFirma.Web.Models
{
    public partial class Vendor : IAuditableEntity
    {
        public string AuditDescriptionString => $"{VendorName} ({StatewideVendorNumberWithSuffix})";

        public string StatewideVendorNumberWithSuffix => $"{StatewideVendorNumber}-{VendorSuffix}";
    }
}