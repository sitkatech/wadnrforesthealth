using System;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static partial class TestContractorTimeActivity
        {
            public static ContractorTimeActivity Create(Project project, FundingSource fundingSource, decimal contractorTimeActivityHours,
                decimal contractorTimeActivityRate, decimal contractorTimeActivityTotalAmount,
                DateTime contractorTimeActivityStartDate)
            {
                return new ContractorTimeActivity(project, fundingSource, contractorTimeActivityHours, contractorTimeActivityRate,
                     contractorTimeActivityStartDate);
            }
        }
    }
}