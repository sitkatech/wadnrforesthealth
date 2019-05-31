//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationExpenditureJsonStage
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationExpenditureJsonStagePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationExpenditureJsonStage>
    {
        public GrantAllocationExpenditureJsonStagePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationExpenditureJsonStagePrimaryKey(GrantAllocationExpenditureJsonStage grantAllocationExpenditureJsonStage) : base(grantAllocationExpenditureJsonStage){}

        public static implicit operator GrantAllocationExpenditureJsonStagePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationExpenditureJsonStagePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationExpenditureJsonStagePrimaryKey(GrantAllocationExpenditureJsonStage grantAllocationExpenditureJsonStage)
        {
            return new GrantAllocationExpenditureJsonStagePrimaryKey(grantAllocationExpenditureJsonStage);
        }
    }
}