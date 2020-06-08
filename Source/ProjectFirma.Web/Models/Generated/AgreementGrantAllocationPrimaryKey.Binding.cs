//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.AgreementGrantAllocation
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class AgreementGrantAllocationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<AgreementGrantAllocation>
    {
        public AgreementGrantAllocationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public AgreementGrantAllocationPrimaryKey(AgreementGrantAllocation agreementGrantAllocation) : base(agreementGrantAllocation){}

        public static implicit operator AgreementGrantAllocationPrimaryKey(int primaryKeyValue)
        {
            return new AgreementGrantAllocationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator AgreementGrantAllocationPrimaryKey(AgreementGrantAllocation agreementGrantAllocation)
        {
            return new AgreementGrantAllocationPrimaryKey(agreementGrantAllocation);
        }
    }
}