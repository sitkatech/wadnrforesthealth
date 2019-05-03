using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectCodeController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public JsonResult FindProjectCode(string term)
        {
            var projectCodesFound = HttpRequestStorage.DatabaseEntities.ProjectCodes.GetProjectCodeFindResults(term).Take(20);
            var projectCodesFoundAsListItems = projectCodesFound.Select(p => new ListItem(p.ProjectCodeName, p.ProjectCodeID.ToString(CultureInfo.InvariantCulture))).ToList();
            var projectCodesFoundAsAnonymousJsonStructure = projectCodesFoundAsListItems.Select(v => new { label = v.Text, value = v.Text, actualValue = v.Value });
            // use JSON structure for jquerys autocomplete functionality
            return Json(projectCodesFoundAsAnonymousJsonStructure, JsonRequestBehavior.AllowGet);
        }
    }
}