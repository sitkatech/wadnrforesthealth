//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationFileResource
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationFileResourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationFileResource>
    {
        public GrantAllocationFileResourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationFileResourcePrimaryKey(GrantAllocationFileResource grantAllocationFileResource) : base(grantAllocationFileResource){}

        public static implicit operator GrantAllocationFileResourcePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationFileResourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationFileResourcePrimaryKey(GrantAllocationFileResource grantAllocationFileResource)
        {
            return new GrantAllocationFileResourcePrimaryKey(grantAllocationFileResource);
        }
    }
}