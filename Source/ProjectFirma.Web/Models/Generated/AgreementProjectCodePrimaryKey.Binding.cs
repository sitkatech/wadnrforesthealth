//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.AgreementProjectCode
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class AgreementProjectCodePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<AgreementProjectCode>
    {
        public AgreementProjectCodePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public AgreementProjectCodePrimaryKey(AgreementProjectCode agreementProjectCode) : base(agreementProjectCode){}

        public static implicit operator AgreementProjectCodePrimaryKey(int primaryKeyValue)
        {
            return new AgreementProjectCodePrimaryKey(primaryKeyValue);
        }

        public static implicit operator AgreementProjectCodePrimaryKey(AgreementProjectCode agreementProjectCode)
        {
            return new AgreementProjectCodePrimaryKey(agreementProjectCode);
        }
    }
}