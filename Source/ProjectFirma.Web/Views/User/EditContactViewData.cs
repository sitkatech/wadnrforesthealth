using System.Collections.Generic;
using System.Web.Mvc;
using ProjectFirma.Web.Views;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public bool FullUpUser { get; }

        public EditContactViewData(IEnumerable<SelectListItem> organizations, bool fullUpUser)
        {
            Organizations = organizations;
            FullUpUser = fullUpUser;
        }

        public IEnumerable<SelectListItem> Organizations { get; }
    }
}