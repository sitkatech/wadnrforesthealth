//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.tmpAgreementContact
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class tmpAgreementContactPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<tmpAgreementContact>
    {
        public tmpAgreementContactPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public tmpAgreementContactPrimaryKey(tmpAgreementContact tmpAgreementContact) : base(tmpAgreementContact){}

        public static implicit operator tmpAgreementContactPrimaryKey(int primaryKeyValue)
        {
            return new tmpAgreementContactPrimaryKey(primaryKeyValue);
        }

        public static implicit operator tmpAgreementContactPrimaryKey(tmpAgreementContact tmpAgreementContact)
        {
            return new tmpAgreementContactPrimaryKey(tmpAgreementContact);
        }
    }
}