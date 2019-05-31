//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationExpenditure
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationExpenditurePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationExpenditure>
    {
        public GrantAllocationExpenditurePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationExpenditurePrimaryKey(GrantAllocationExpenditure grantAllocationExpenditure) : base(grantAllocationExpenditure){}

        public static implicit operator GrantAllocationExpenditurePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationExpenditurePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationExpenditurePrimaryKey(GrantAllocationExpenditure grantAllocationExpenditure)
        {
            return new GrantAllocationExpenditurePrimaryKey(grantAllocationExpenditure);
        }
    }
}