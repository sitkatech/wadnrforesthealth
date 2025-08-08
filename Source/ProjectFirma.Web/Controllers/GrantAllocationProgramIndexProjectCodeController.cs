using System.Linq;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Controllers
{
    public class FundSourceAllocationProgramIndexProjectCodeController : FirmaBaseController
    {
        #region WADNR FundSource JSON API

        [ProjectCodeViewJsonApiFeature]
        public JsonNetJArrayResult FundSourceAllocationProgramIndexProjectCodeJsonApi()
        {
            var fundSourceAllocationProgramIndexProjectCodes = HttpRequestStorage.DatabaseEntities.FundSourceAllocationProgramIndexProjectCodes.ToList();
            var jsonProjectCodes = FundSourceAllocationProgramIndexProjectCodeApiJson.MakeFundSourceAllocationProgramIndexProjectCodeApiJsonsFromFundSourceAllocationProgramIndexProjectCodes(fundSourceAllocationProgramIndexProjectCodes, false);
            return new JsonNetJArrayResult(jsonProjectCodes);
        }

        #endregion
    }
}