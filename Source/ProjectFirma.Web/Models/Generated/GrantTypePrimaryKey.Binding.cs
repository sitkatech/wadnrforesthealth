//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantType>
    {
        public GrantTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantTypePrimaryKey(GrantType grantType) : base(grantType){}

        public static implicit operator GrantTypePrimaryKey(int primaryKeyValue)
        {
            return new GrantTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantTypePrimaryKey(GrantType grantType)
        {
            return new GrantTypePrimaryKey(grantType);
        }
    }
}