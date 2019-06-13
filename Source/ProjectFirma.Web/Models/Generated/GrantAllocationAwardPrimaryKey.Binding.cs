//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationAward
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationAwardPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationAward>
    {
        public GrantAllocationAwardPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationAwardPrimaryKey(GrantAllocationAward grantAllocationAward) : base(grantAllocationAward){}

        public static implicit operator GrantAllocationAwardPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationAwardPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationAwardPrimaryKey(GrantAllocationAward grantAllocationAward)
        {
            return new GrantAllocationAwardPrimaryKey(grantAllocationAward);
        }
    }
}