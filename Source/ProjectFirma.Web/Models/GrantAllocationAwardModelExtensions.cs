using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.GrantAllocationAwardDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this GrantAllocationAward grantAllocationAward)
        {
            return DetailUrlTemplate.ParameterReplace(grantAllocationAward.GrantAllocationAwardID);
        }
    }
}