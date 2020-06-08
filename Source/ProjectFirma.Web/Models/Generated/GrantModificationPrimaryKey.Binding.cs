//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantModification
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantModificationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantModification>
    {
        public GrantModificationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantModificationPrimaryKey(GrantModification grantModification) : base(grantModification){}

        public static implicit operator GrantModificationPrimaryKey(int primaryKeyValue)
        {
            return new GrantModificationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantModificationPrimaryKey(GrantModification grantModification)
        {
            return new GrantModificationPrimaryKey(grantModification);
        }
    }
}