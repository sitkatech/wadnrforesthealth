//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAwardContractorInvoice
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardContractorInvoicePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAwardContractorInvoice>
    {
        public GrantAllocationAwardContractorInvoicePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardContractorInvoicePrimaryKey(GrantAllocationAwardContractorInvoice grantAllocationAwardContractorInvoice) : base(grantAllocationAwardContractorInvoice){}

        public static implicit operator GrantAllocationAwardContractorInvoicePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardContractorInvoicePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardContractorInvoicePrimaryKey(GrantAllocationAwardContractorInvoice grantAllocationAwardContractorInvoice)
        {
            return new GrantAllocationAwardContractorInvoicePrimaryKey(grantAllocationAwardContractorInvoice);
        }
    }
}