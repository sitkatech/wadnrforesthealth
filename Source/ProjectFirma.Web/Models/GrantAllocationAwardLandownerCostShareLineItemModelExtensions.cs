using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardLandownerCostShareLineItemModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.DeleteLandownerCostShareLineItem(UrlTemplate.Parameter1Int)));
        public static string GetDeleteLandownerCostShareLineItemUrl(this GrantAllocationAwardLandownerCostShareLineItem travelLineItem)
        {
            return DeleteUrlTemplate.ParameterReplace(travelLineItem.GrantAllocationAwardLandownerCostShareLineItemID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.EditLandownerCostShareLineItem(UrlTemplate.Parameter1Int)));
        public static string GetEditLandownerCostShareLineItemUrl(this GrantAllocationAwardLandownerCostShareLineItem travelLineItem)
        {
            return EditUrlTemplate.ParameterReplace(travelLineItem.GrantAllocationAwardLandownerCostShareLineItemID);
        }

    }
}