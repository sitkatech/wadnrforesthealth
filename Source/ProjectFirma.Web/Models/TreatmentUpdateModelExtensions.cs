using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class TreatmentUpdateModelExtensions
    {
        public static readonly UrlTemplate<int> EditTreatmentUpdateUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentUpdateController>.BuildUrlFromExpression(t => t.EditTreatmentUpdate(UrlTemplate.Parameter1Int)));
        public static string GetEditTreatmentUpdateUrl(this TreatmentUpdate treatmentUpdate)
        {
            return EditTreatmentUpdateUrlTemplate.ParameterReplace(treatmentUpdate.TreatmentUpdateID);
        }
    }
}