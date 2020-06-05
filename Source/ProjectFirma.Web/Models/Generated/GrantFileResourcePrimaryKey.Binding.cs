//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantFileResource
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantFileResourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantFileResource>
    {
        public GrantFileResourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantFileResourcePrimaryKey(GrantFileResource grantFileResource) : base(grantFileResource){}

        public static implicit operator GrantFileResourcePrimaryKey(int primaryKeyValue)
        {
            return new GrantFileResourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantFileResourcePrimaryKey(GrantFileResource grantFileResource)
        {
            return new GrantFileResourcePrimaryKey(grantFileResource);
        }
    }
}