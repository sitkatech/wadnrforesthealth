//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationProgramManager
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationProgramManagerPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationProgramManager>
    {
        public GrantAllocationProgramManagerPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationProgramManagerPrimaryKey(GrantAllocationProgramManager grantAllocationProgramManager) : base(grantAllocationProgramManager){}

        public static implicit operator GrantAllocationProgramManagerPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationProgramManagerPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationProgramManagerPrimaryKey(GrantAllocationProgramManager grantAllocationProgramManager)
        {
            return new GrantAllocationProgramManagerPrimaryKey(grantAllocationProgramManager);
        }
    }
}