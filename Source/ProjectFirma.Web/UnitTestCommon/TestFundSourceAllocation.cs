using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public class TestFundSourceAllocation
        {
            public static FundSourceAllocation Create()
            {
                var fundSource = TestFundSource.Create();
                var org = TestOrganization.Create();
                var fundSourceAllocation = TestFundSourceAllocation.Create(fundSource, GetTestFundSourceAllocationName(org, "Test FundSource Allocation"));
                //FundSourceAllocation.IsActive = true;
                return fundSourceAllocation;
            }

            public static FundSourceAllocation Create(FundSource fundSource, string fundSourceAllocationName)
            {
                var fundSourceAllocation = new FundSourceAllocation(fundSource);
                fundSourceAllocation.FundSourceAllocationName = fundSourceAllocationName;
                return fundSourceAllocation;
            }

            public static FundSourceAllocation CreateWithoutChangingName(string fundSourceAllocationName)
            {
                var fundSource = TestFundSource.Create();
                var fundSourceAllocation = new FundSourceAllocation(fundSource);
                fundSourceAllocation.FundSourceAllocationName = fundSourceAllocationName;
                return fundSourceAllocation;
            }

            public static FundSourceAllocation CreateWithoutChangingName(string fundSourceAllocationName, Organization organization)
            {
                var fundSource = TestFundSource.Create();
                var fundSourceAllocation = new FundSourceAllocation(fundSource);
                fundSourceAllocation.FundSourceAllocationName = fundSourceAllocationName;
                fundSourceAllocation.Organization = organization;
                fundSourceAllocation.OrganizationID = organization.OrganizationID;
                return fundSourceAllocation;
            }

            private static string GetTestFundSourceAllocationName(Organization organization, string fundSourceAllocationName)
            {
                return string.Format("{0}{1}", organization.OrganizationName, fundSourceAllocationName);
            }

            public static FundSourceAllocation Create(DatabaseEntities dbContext)
            {
                var fundSource = TestFramework.TestFundSource.Insert(dbContext);
                string testFundSourceAllocationName = TestFramework.MakeTestName("Test FundSource Allocation Name");
                var fundSourceAllocation = new FundSourceAllocation(fundSource);
                fundSourceAllocation.FundSourceAllocationName = testFundSourceAllocationName;

                dbContext.FundSourceAllocations.Add(fundSourceAllocation);
                return fundSourceAllocation;
            }



            public static FundSourceAllocation Insert(DatabaseEntities dbContext)
            {
                var fundSourceAllocation = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return fundSourceAllocation;
            }
        }
    }
}