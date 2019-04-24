//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationBudgetLineItem
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationBudgetLineItemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationBudgetLineItem>
    {
        public GrantAllocationBudgetLineItemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationBudgetLineItemPrimaryKey(GrantAllocationBudgetLineItem grantAllocationBudgetLineItem) : base(grantAllocationBudgetLineItem){}

        public static implicit operator GrantAllocationBudgetLineItemPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationBudgetLineItemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationBudgetLineItemPrimaryKey(GrantAllocationBudgetLineItem grantAllocationBudgetLineItem)
        {
            return new GrantAllocationBudgetLineItemPrimaryKey(grantAllocationBudgetLineItem);
        }
    }
}