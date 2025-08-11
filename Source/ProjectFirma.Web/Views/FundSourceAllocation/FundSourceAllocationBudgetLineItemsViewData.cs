using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class FundSourceAllocationBudgetLineItemsViewData : FirmaUserControlViewData
    {
        public List<CostType> CostTypes { get; }
        public string FormPostUrl { get; }
        public int FundSourceAllocationID { get; }
        public List<FundSourceAllocationBudgetLineItem> FundSourceAllocationBudgetLineItems { get; }
        public bool PersonHasPermissionToEditBudgetLineItems { get; }

        public FundSourceAllocationBudgetLineItemsViewData(Person currentPerson, Models.FundSourceAllocation fundSourceAllocationBeingEdited, List<FundSourceAllocationBudgetLineItem> fundSourceAllocationBudgetLineItems)
        {
            CostTypes = CostType.GetLineItemCostTypes();
            FundSourceAllocationID = fundSourceAllocationBeingEdited.FundSourceAllocationID;
            FundSourceAllocationBudgetLineItems = fundSourceAllocationBudgetLineItems.OrderBy(x => x.CostType.SortOrder).ToList();

            PersonHasPermissionToEditBudgetLineItems = new FundSourceAllocationBudgetLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            //This will prevent the JS from posting back if the user doesn't have permission to edit the budget line items
            if (PersonHasPermissionToEditBudgetLineItems)
            {
                FormPostUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x =>
                    x.EditFundSourceAllocationBudgetLineItemAjax(fundSourceAllocationBeingEdited.PrimaryKey));
            }
        }
    }
}