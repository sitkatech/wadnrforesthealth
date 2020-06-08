//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardSuppliesLineItem
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardSuppliesLineItemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardSuppliesLineItem>
    {
        public GrantAllocationAwardSuppliesLineItemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardSuppliesLineItemPrimaryKey(GrantAllocationAwardSuppliesLineItem grantAllocationAwardSuppliesLineItem) : base(grantAllocationAwardSuppliesLineItem){}

        public static implicit operator GrantAllocationAwardSuppliesLineItemPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardSuppliesLineItemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardSuppliesLineItemPrimaryKey(GrantAllocationAwardSuppliesLineItem grantAllocationAwardSuppliesLineItem)
        {
            return new GrantAllocationAwardSuppliesLineItemPrimaryKey(grantAllocationAwardSuppliesLineItem);
        }
    }
}