//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantModificationPurpose
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantModificationPurposePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantModificationPurpose>
    {
        public GrantModificationPurposePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantModificationPurposePrimaryKey(GrantModificationPurpose grantModificationPurpose) : base(grantModificationPurpose){}

        public static implicit operator GrantModificationPurposePrimaryKey(int primaryKeyValue)
        {
            return new GrantModificationPurposePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantModificationPurposePrimaryKey(GrantModificationPurpose grantModificationPurpose)
        {
            return new GrantModificationPurposePrimaryKey(grantModificationPurpose);
        }
    }
}