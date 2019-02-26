namespace ProjectFirma.Web.Models
{
    public static class VendorModelExtensions
    {

        public static string GetVendorNameWithFullStatewideVendorNumber(this Vendor vendor)
        {
            return vendor != null ? $"{vendor.VendorName} ({vendor.StatewideVendorNumber}-{vendor.StatewideVendorNumberSuffix})" : string.Empty;
        }
    }
}