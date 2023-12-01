//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationPriority
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationPriorityPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationPriority>
    {
        public GrantAllocationPriorityPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationPriorityPrimaryKey(GrantAllocationPriority grantAllocationPriority) : base(grantAllocationPriority){}

        public static implicit operator GrantAllocationPriorityPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationPriorityPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationPriorityPrimaryKey(GrantAllocationPriority grantAllocationPriority)
        {
            return new GrantAllocationPriorityPrimaryKey(grantAllocationPriority);
        }
    }
}