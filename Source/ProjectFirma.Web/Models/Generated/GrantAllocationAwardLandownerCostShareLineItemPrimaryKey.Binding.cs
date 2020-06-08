//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardLandownerCostShareLineItem
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardLandownerCostShareLineItemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardLandownerCostShareLineItem>
    {
        public GrantAllocationAwardLandownerCostShareLineItemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardLandownerCostShareLineItemPrimaryKey(GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem) : base(grantAllocationAwardLandownerCostShareLineItem){}

        public static implicit operator GrantAllocationAwardLandownerCostShareLineItemPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardLandownerCostShareLineItemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardLandownerCostShareLineItemPrimaryKey(GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem)
        {
            return new GrantAllocationAwardLandownerCostShareLineItemPrimaryKey(grantAllocationAwardLandownerCostShareLineItem);
        }
    }
}