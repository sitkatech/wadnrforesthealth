using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class ProgramIndexController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public JsonResult FindProgramIndex(string term)
        {
            var tmpProgramIndices = HttpRequestStorage.DatabaseEntities.ProgramIndices.ToList();
            var programIndicesFound = tmpProgramIndices.GetProgramIndicesInTheCurrentBiennium().GetProgramIndexFindResults(term).Take(20);
            var programIndicesAsListItems = programIndicesFound.Select(p => new ListItem(p.ProgramIndexCode, p.ProgramIndexID.ToString(CultureInfo.InvariantCulture))).ToList();
            var programIndicesAsAnonymousJsonStructure = programIndicesAsListItems.Select(v => new { label = v.Text, value = v.Text, actualValue = v.Value });
            //use JSON structure for jquery's autocomplete functionality
            return Json(programIndicesAsAnonymousJsonStructure, JsonRequestBehavior.AllowGet);
        }
    }
}