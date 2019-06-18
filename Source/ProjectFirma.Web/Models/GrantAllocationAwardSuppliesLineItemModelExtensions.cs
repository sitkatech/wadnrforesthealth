using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardSuppliesLineItemModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.DeleteSuppliesLineItem(UrlTemplate.Parameter1Int)));
        public static string GetDeleteSuppliesLineItemUrl(this GrantAllocationAwardSuppliesLineItem suppliesLineItem)
        {
            return DeleteUrlTemplate.ParameterReplace(suppliesLineItem.GrantAllocationAwardSuppliesLineItemID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.EditSuppliesLineItem(UrlTemplate.Parameter1Int)));
        public static string GetEditSuppliesLineItemUrl(this GrantAllocationAwardSuppliesLineItem suppliesLineItem)
        {
            return EditUrlTemplate.ParameterReplace(suppliesLineItem.GrantAllocationAwardSuppliesLineItemID);
        }

    }
}