//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardTravelLineItemType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardTravelLineItemTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardTravelLineItemType>
    {
        public GrantAllocationAwardTravelLineItemTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardTravelLineItemTypePrimaryKey(GrantAllocationAwardTravelLineItemType grantAllocationAwardTravelLineItemType) : base(grantAllocationAwardTravelLineItemType){}

        public static implicit operator GrantAllocationAwardTravelLineItemTypePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardTravelLineItemTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardTravelLineItemTypePrimaryKey(GrantAllocationAwardTravelLineItemType grantAllocationAwardTravelLineItemType)
        {
            return new GrantAllocationAwardTravelLineItemTypePrimaryKey(grantAllocationAwardTravelLineItemType);
        }
    }
}