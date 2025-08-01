//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FundSourceType>
    {
        public GrantTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantTypePrimaryKey(FundSourceType fundSourceType) : base(fundSourceType){}

        public static implicit operator GrantTypePrimaryKey(int primaryKeyValue)
        {
            return new GrantTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantTypePrimaryKey(FundSourceType fundSourceType)
        {
            return new GrantTypePrimaryKey(fundSourceType);
        }
    }
}