//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocation
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FundSourceAllocation>
    {
        public GrantAllocationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationPrimaryKey(FundSourceAllocation fundSourceAllocation) : base(fundSourceAllocation){}

        public static implicit operator GrantAllocationPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationPrimaryKey(FundSourceAllocation fundSourceAllocation)
        {
            return new GrantAllocationPrimaryKey(fundSourceAllocation);
        }
    }
}