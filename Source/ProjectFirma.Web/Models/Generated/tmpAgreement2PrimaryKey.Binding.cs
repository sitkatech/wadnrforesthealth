//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.tmpAgreement2
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class tmpAgreement2PrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<tmpAgreement2>
    {
        public tmpAgreement2PrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public tmpAgreement2PrimaryKey(tmpAgreement2 tmpAgreement2) : base(tmpAgreement2){}

        public static implicit operator tmpAgreement2PrimaryKey(int primaryKeyValue)
        {
            return new tmpAgreement2PrimaryKey(primaryKeyValue);
        }

        public static implicit operator tmpAgreement2PrimaryKey(tmpAgreement2 tmpAgreement2)
        {
            return new tmpAgreement2PrimaryKey(tmpAgreement2);
        }
    }
}