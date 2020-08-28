//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantModificationFileResource
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantModificationFileResourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantModificationFileResource>
    {
        public GrantModificationFileResourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantModificationFileResourcePrimaryKey(GrantModificationFileResource grantModificationFileResource) : base(grantModificationFileResource){}

        public static implicit operator GrantModificationFileResourcePrimaryKey(int primaryKeyValue)
        {
            return new GrantModificationFileResourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantModificationFileResourcePrimaryKey(GrantModificationFileResource grantModificationFileResource)
        {
            return new GrantModificationFileResourcePrimaryKey(grantModificationFileResource);
        }
    }
}