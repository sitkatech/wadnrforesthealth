namespace ProjectFirma.Web.Models
{
    public static class VendorModelExtensions
    {
        public static string GetStatewideVendorNumberWithSuffix(this Vendor vendor)
        {
            return vendor != null ? $"{vendor.StatewideVendorNumber}-{vendor.VendorSuffix}" : string.Empty;
        }
    }
}