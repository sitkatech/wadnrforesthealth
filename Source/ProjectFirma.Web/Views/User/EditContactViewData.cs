using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public bool FullUpUser { get; }
        public IEnumerable<SelectListItem> Organizations { get; }
        

        public EditContactViewData(IEnumerable<SelectListItem> organizations, bool fullUpUser)
        {
            Organizations = organizations;
            FullUpUser = fullUpUser;
        }

    }
}