using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class VendorModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<VendorController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Vendor vendor)
        {
            return DetailUrlTemplate.ParameterReplace(vendor.VendorID);
        }

        public static string GetVendorNameWithFullStatewideVendorNumber(this Vendor vendor)
        {
            return vendor != null ? $"{vendor.VendorName} ({vendor.StatewideVendorNumber}-{vendor.StatewideVendorNumberSuffix})" : string.Empty;
        }
    }
}