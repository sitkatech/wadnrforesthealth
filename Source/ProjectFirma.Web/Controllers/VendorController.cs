using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class VendorController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public JsonResult FindVendor(string term)
        {

            var vendorsFound = HttpRequestStorage.DatabaseEntities.Vendors
                .GetVendorFindResultsForVendorNameAndStatewideVendorNumber(term).Take(20);
            var vendorsFound2 = vendorsFound.Select(p => new ListItem(p.VendorNameAndStatewideVendorNumberWithSuffix, p.VendorID.ToString(CultureInfo.InvariantCulture))).ToList();

            return Json(vendorsFound2.Select(v => new { label = v.Text, value = v.Value }), JsonRequestBehavior.AllowGet);//use JSON structure for jquerys autocomplete functionality

        }

    }
}