using System;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static partial class TestStaffTimeActivity
        {
            public static StaffTimeActivity Create(Project project, FundingSource fundingSource, decimal staffTimeActivityHours,
                decimal staffTimeActivityRate, decimal staffTimeActivityTotalAmount,
                DateTime staffTimeActivityStartDate)
            {
                return new StaffTimeActivity(project, fundingSource, staffTimeActivityHours, staffTimeActivityRate,
                    staffTimeActivityTotalAmount, staffTimeActivityStartDate);
            }
        }
    }
}