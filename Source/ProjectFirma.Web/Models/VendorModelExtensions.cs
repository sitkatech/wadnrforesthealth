using System;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using System.Web;

namespace ProjectFirma.Web.Models
{
    public static class VendorModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<VendorController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Vendor vendor)
        {
            return vendor != null ? DetailUrlTemplate.ParameterReplace(vendor.VendorID) : string.Empty;
        }

        public static string GetVendorNameWithFullStatewideVendorNumber(this Vendor vendor)
        {
            return vendor != null ? $"{vendor.VendorName} ({vendor.StatewideVendorNumber}-{vendor.StatewideVendorNumberSuffix})" : string.Empty;
        }
        
        public static HtmlString GetVendorNameWithFullStatewideVendorNumberAsUrl(this Vendor vendor)
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(vendor), GetVendorNameWithFullStatewideVendorNumber(vendor));
        }

        public static string GetFullVendorAddress(this Vendor vendor)
        {
            return vendor != null
                ? String.Join("\r\n", vendor.VendorAddressLine1, vendor.VendorAddressLine2, vendor.VendorAddressLine3,
                    vendor.VendorCity, vendor.VendorState, vendor.VendorZip)
                : string.Empty;

        }
    }
}