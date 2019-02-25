using System.Collections.Generic;
using System.Web.Mvc;
using ProjectFirma.Web.Views;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public bool FullUpUser { get; }

        public EditContactViewData(IEnumerable<SelectListItem> organizations, IEnumerable<SelectListItem> vendors, bool fullUpUser)
        {
            Organizations = organizations;
            Vendors = vendors;
            FullUpUser = fullUpUser;
        }

        public IEnumerable<SelectListItem> Organizations { get; }

        public IEnumerable<SelectListItem> Vendors { get; }


    }
}