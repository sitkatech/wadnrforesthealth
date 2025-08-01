//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Grant
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FundSource>
    {
        public GrantPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantPrimaryKey(FundSource fundSource) : base(fundSource){}

        public static implicit operator GrantPrimaryKey(int primaryKeyValue)
        {
            return new GrantPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantPrimaryKey(FundSource fundSource)
        {
            return new GrantPrimaryKey(fundSource);
        }
    }
}