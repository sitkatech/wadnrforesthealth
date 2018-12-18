using System.Collections.Generic;
using System.Web.Mvc;
using ProjectFirma.Web.Views;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public EditContactViewData(IEnumerable<SelectListItem> organizations)
        {
            Organizations = organizations;
        }

        public IEnumerable<SelectListItem> Organizations { get; }
    }
}