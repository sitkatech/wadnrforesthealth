//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardPersonnelAndBenefitsLineItem
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardPersonnelAndBenefitsLineItem>
    {
        public GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey(GrantAllocationAwardPersonnelAndBenefitsLineItem grantAllocationAwardPersonnelAndBenefitsLineItem) : base(grantAllocationAwardPersonnelAndBenefitsLineItem){}

        public static implicit operator GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey(GrantAllocationAwardPersonnelAndBenefitsLineItem grantAllocationAwardPersonnelAndBenefitsLineItem)
        {
            return new GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey(grantAllocationAwardPersonnelAndBenefitsLineItem);
        }
    }
}