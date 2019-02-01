using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class AgreementPersonModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.DeleteAgreementPerson(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this AgreementPerson agreementPerson)
        {
            return DeleteUrlTemplate.ParameterReplace(agreementPerson.PrimaryKey);
        }

    }
}