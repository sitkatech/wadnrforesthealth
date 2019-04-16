using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationBudgetLineItemModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.DeleteGrantAllocationBudgetLineItem(UrlTemplate.Parameter1Int)));
        public static string GetDeleteGrantAllocationBudgetLineItemUrl(this GrantAllocationBudgetLineItem grantAllocationBudgetLineItem)
        {
            return DeleteUrlTemplate.ParameterReplace(grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.EditGrantAllocationBudgetLineItem(UrlTemplate.Parameter1Int)));
        public static string GetEditGrantAllocationBudgetLineItemUrl(this GrantAllocationBudgetLineItem grantAllocationBudgetLineItem)
        {
            return EditUrlTemplate.ParameterReplace(grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemID);
        }
    }
}