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
    public class ProjectCodeController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public JsonResult FindProjectCode(string term)
        {

            var projectCodesFound = HttpRequestStorage.DatabaseEntities.ProjectCodes.GetProjectCodeFindResults(term).Take(20);
            var projectCodesFound2 = projectCodesFound.Select(p => new ListItem(p.ProjectCodeAbbrev, p.ProjectCodeID.ToString(CultureInfo.InvariantCulture))).ToList();

            return Json(projectCodesFound2.Select(v => new { label = v.Text, value = v.Text, actualValue = v.Value }), JsonRequestBehavior.AllowGet);//use JSON structure for jquerys autocomplete functionality

        }

    }
}