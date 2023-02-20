
using DocumentFormat.OpenXml.Office2013.Word;
using ProjectFirma.Web.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;
using Person = ProjectFirma.Web.Models.Person;

namespace ProjectFirma.Web.Views.DNRUplandRegion
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public Models.DNRUplandRegion Region { get; }
        public IEnumerable<SelectListItem> activePersonsList { get; }

        public EditContactViewData(Models.DNRUplandRegion region)
        {
            Region = region;
            var allPeople = HttpRequestStorage.DatabaseEntities.People.GetAllWadnrPeople().OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
            activePersonsList = allPeople.ToSelectList(x => x.PersonID.ToString(CultureInfo.InvariantCulture), y => y.FullNameFirstLastAndOrgShortName, "Unassigned");

        }

    }
}