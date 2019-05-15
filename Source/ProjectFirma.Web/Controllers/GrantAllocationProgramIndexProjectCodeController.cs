using System.Linq;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Controllers
{
    public class GrantAllocationProgramIndexProjectCodeController : FirmaBaseController
    {
        #region WADNR Grant JSON API

        [ProjectCodeViewJsonApiFeature]
        public JsonNetJArrayResult GrantAllocationProgramIndexProjectCodeJsonApi()
        {
            var grantAllocationProgramIndexProjectCodes = HttpRequestStorage.DatabaseEntities.GrantAllocationProgramIndexProjectCodes.ToList();
            var jsonProjectCodes = GrantAllocationProgramIndexProjectCodeApiJson.MakeGrantAllocationProgramIndexProjectCodeApiJsonsFromGrantAllocationProgramIndexProjectCodes(grantAllocationProgramIndexProjectCodes, false);
            return new JsonNetJArrayResult(jsonProjectCodes);
        }

        #endregion
    }
}