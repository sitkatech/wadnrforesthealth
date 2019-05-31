//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationProgramIndexProjectCode
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationProgramIndexProjectCodePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationProgramIndexProjectCode>
    {
        public GrantAllocationProgramIndexProjectCodePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationProgramIndexProjectCodePrimaryKey(GrantAllocationProgramIndexProjectCode grantAllocationProgramIndexProjectCode) : base(grantAllocationProgramIndexProjectCode){}

        public static implicit operator GrantAllocationProgramIndexProjectCodePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationProgramIndexProjectCodePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationProgramIndexProjectCodePrimaryKey(GrantAllocationProgramIndexProjectCode grantAllocationProgramIndexProjectCode)
        {
            return new GrantAllocationProgramIndexProjectCodePrimaryKey(grantAllocationProgramIndexProjectCode);
        }
    }
}