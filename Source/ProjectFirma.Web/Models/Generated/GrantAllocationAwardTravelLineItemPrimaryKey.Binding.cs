//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardTravelLineItem
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardTravelLineItemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardTravelLineItem>
    {
        public GrantAllocationAwardTravelLineItemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardTravelLineItemPrimaryKey(GrantAllocationAwardTravelLineItem grantAllocationAwardTravelLineItem) : base(grantAllocationAwardTravelLineItem){}

        public static implicit operator GrantAllocationAwardTravelLineItemPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardTravelLineItemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardTravelLineItemPrimaryKey(GrantAllocationAwardTravelLineItem grantAllocationAwardTravelLineItem)
        {
            return new GrantAllocationAwardTravelLineItemPrimaryKey(grantAllocationAwardTravelLineItem);
        }
    }
}