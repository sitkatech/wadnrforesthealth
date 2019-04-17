//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantModificationStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantModificationStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantModificationStatus>
    {
        public GrantModificationStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantModificationStatusPrimaryKey(GrantModificationStatus grantModificationStatus) : base(grantModificationStatus){}

        public static implicit operator GrantModificationStatusPrimaryKey(int primaryKeyValue)
        {
            return new GrantModificationStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantModificationStatusPrimaryKey(GrantModificationStatus grantModificationStatus)
        {
            return new GrantModificationStatusPrimaryKey(grantModificationStatus);
        }
    }
}