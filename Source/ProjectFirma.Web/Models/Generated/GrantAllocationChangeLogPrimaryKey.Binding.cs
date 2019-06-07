//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationChangeLog
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationChangeLogPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationChangeLog>
    {
        public GrantAllocationChangeLogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationChangeLogPrimaryKey(GrantAllocationChangeLog grantAllocationChangeLog) : base(grantAllocationChangeLog){}

        public static implicit operator GrantAllocationChangeLogPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationChangeLogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationChangeLogPrimaryKey(GrantAllocationChangeLog grantAllocationChangeLog)
        {
            return new GrantAllocationChangeLogPrimaryKey(grantAllocationChangeLog);
        }
    }
}