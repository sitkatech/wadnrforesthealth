using System.Collections.Generic;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public bool FullUpUser { get; }
        public IEnumerable<SelectListItem> Organizations { get; }
        public string VendorFindUrlTemplate { get; }

        public EditContactViewData(IEnumerable<SelectListItem> organizations, bool fullUpUser)
        {
            Organizations = organizations;
            FullUpUser = fullUpUser;
            VendorFindUrlTemplate =
                SitkaRoute<VendorController>.BuildUrlFromExpression(x => x.FindVendor(string.Empty));
        }

    }
}