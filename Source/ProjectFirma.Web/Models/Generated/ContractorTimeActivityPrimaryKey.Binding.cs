//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ContractorTimeActivity
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ContractorTimeActivityPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ContractorTimeActivity>
    {
        public ContractorTimeActivityPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ContractorTimeActivityPrimaryKey(ContractorTimeActivity contractorTimeActivity) : base(contractorTimeActivity){}

        public static implicit operator ContractorTimeActivityPrimaryKey(int primaryKeyValue)
        {
            return new ContractorTimeActivityPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ContractorTimeActivityPrimaryKey(ContractorTimeActivity contractorTimeActivity)
        {
            return new ContractorTimeActivityPrimaryKey(contractorTimeActivity);
        }
    }
}