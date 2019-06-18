using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardTravelLineItemModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.DeleteTravelLineItem(UrlTemplate.Parameter1Int)));
        public static string GetDeleteTravelLineItemUrl(this GrantAllocationAwardTravelLineItem travelLineItem)
        {
            return DeleteUrlTemplate.ParameterReplace(travelLineItem.GrantAllocationAwardTravelLineItemID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.EditTravelLineItem(UrlTemplate.Parameter1Int)));
        public static string GetEditTravelLineItemUrl(this GrantAllocationAwardTravelLineItem travelLineItem)
        {
            return EditUrlTemplate.ParameterReplace(travelLineItem.GrantAllocationAwardTravelLineItemID);
        }

    }
}