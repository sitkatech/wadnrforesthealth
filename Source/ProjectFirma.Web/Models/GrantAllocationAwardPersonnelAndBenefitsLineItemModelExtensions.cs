using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardPersonnelAndBenefitsLineItemModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.DeletePersonnelAndBenefitsLineItem(UrlTemplate.Parameter1Int)));
        public static string GetDeletePersonnelAndBenefitsLineItemUrl(this GrantAllocationAwardPersonnelAndBenefitsLineItem personnelAndBenefitsLineItem)
        {
            return DeleteUrlTemplate.ParameterReplace(personnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.EditPersonnelAndBenefitsLineItem(UrlTemplate.Parameter1Int)));
        public static string GetEditPersonnelAndBenefitsLineItemUrl(this GrantAllocationAwardPersonnelAndBenefitsLineItem personnelAndBenefitsLineItem)
        {
            return EditUrlTemplate.ParameterReplace(personnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemID);
        }

    }
}