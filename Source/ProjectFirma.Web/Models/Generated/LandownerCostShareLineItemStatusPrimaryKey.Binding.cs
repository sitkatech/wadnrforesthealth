//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandownerCostShareLineItemStatus
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class LandownerCostShareLineItemStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LandownerCostShareLineItemStatus>
    {
        public LandownerCostShareLineItemStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandownerCostShareLineItemStatusPrimaryKey(LandownerCostShareLineItemStatus landownerCostShareLineItemStatus) : base(landownerCostShareLineItemStatus){}

        public static implicit operator LandownerCostShareLineItemStatusPrimaryKey(int primaryKeyValue)
        {
            return new LandownerCostShareLineItemStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandownerCostShareLineItemStatusPrimaryKey(LandownerCostShareLineItemStatus landownerCostShareLineItemStatus)
        {
            return new LandownerCostShareLineItemStatusPrimaryKey(landownerCostShareLineItemStatus);
        }
    }
}