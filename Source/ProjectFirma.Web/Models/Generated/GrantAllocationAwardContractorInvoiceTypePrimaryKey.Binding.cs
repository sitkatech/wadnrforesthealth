//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardContractorInvoiceType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardContractorInvoiceTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardContractorInvoiceType>
    {
        public GrantAllocationAwardContractorInvoiceTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardContractorInvoiceTypePrimaryKey(GrantAllocationAwardContractorInvoiceType grantAllocationAwardContractorInvoiceType) : base(grantAllocationAwardContractorInvoiceType){}

        public static implicit operator GrantAllocationAwardContractorInvoiceTypePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardContractorInvoiceTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardContractorInvoiceTypePrimaryKey(GrantAllocationAwardContractorInvoiceType grantAllocationAwardContractorInvoiceType)
        {
            return new GrantAllocationAwardContractorInvoiceTypePrimaryKey(grantAllocationAwardContractorInvoiceType);
        }
    }
}