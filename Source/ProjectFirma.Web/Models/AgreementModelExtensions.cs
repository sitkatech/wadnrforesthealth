using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class AgreementModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.DeleteAgreement(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Agreement agreement)
        {
            return DeleteUrlTemplate.ParameterReplace(agreement.PrimaryKey);
        }

    }
}