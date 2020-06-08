//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantModificationGrantModificationPurpose
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantModificationGrantModificationPurposePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantModificationGrantModificationPurpose>
    {
        public GrantModificationGrantModificationPurposePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantModificationGrantModificationPurposePrimaryKey(GrantModificationGrantModificationPurpose grantModificationGrantModificationPurpose) : base(grantModificationGrantModificationPurpose){}

        public static implicit operator GrantModificationGrantModificationPurposePrimaryKey(int primaryKeyValue)
        {
            return new GrantModificationGrantModificationPurposePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantModificationGrantModificationPurposePrimaryKey(GrantModificationGrantModificationPurpose grantModificationGrantModificationPurpose)
        {
            return new GrantModificationGrantModificationPurposePrimaryKey(grantModificationGrantModificationPurpose);
        }
    }
}