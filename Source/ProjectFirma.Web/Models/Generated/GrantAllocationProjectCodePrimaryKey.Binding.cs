//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationProjectCode
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationProjectCodePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationProjectCode>
    {
        public GrantAllocationProjectCodePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationProjectCodePrimaryKey(GrantAllocationProjectCode grantAllocationProjectCode) : base(grantAllocationProjectCode){}

        public static implicit operator GrantAllocationProjectCodePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationProjectCodePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationProjectCodePrimaryKey(GrantAllocationProjectCode grantAllocationProjectCode)
        {
            return new GrantAllocationProjectCodePrimaryKey(grantAllocationProjectCode);
        }
    }
}