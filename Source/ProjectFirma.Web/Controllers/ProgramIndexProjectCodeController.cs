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
    public class ProgramIndexProjectCodeController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public JsonResult FindProgramIndexProjectCode(string term)
        {
            var programIndexProjectCodesFound = HttpRequestStorage.DatabaseEntities.ProgramIndexProjectCodes.GetProgramIndexProjectCodeFindResults(term).Take(20);
            var programIndexProjectCodesFoundAsListItems = programIndexProjectCodesFound.Select(p => new ListItem(p.ProgramIndexProjectCodeDisplayString, p.ProgramIndexProjectCodeID.ToString(CultureInfo.InvariantCulture))).ToList();
            var programIndexProjectCodesFoundAsAnonymousJsonStructure = programIndexProjectCodesFoundAsListItems.Select(v => new { label = v.Text, value = v.Text, actualValue = v.Value });
            // use JSON structure for jquerys autocomplete functionality
            return Json(programIndexProjectCodesFoundAsAnonymousJsonStructure, JsonRequestBehavior.AllowGet);
        }

    }
}