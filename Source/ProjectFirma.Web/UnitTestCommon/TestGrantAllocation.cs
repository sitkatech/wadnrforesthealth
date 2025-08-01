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
                var grant = TestFundSource.Create();
                var org = TestOrganization.Create();
                var grantAllocation = TestFundSourceAllocation.Create(grant, GetTestGrantAllocationName(org, "Test Grant Allocation"));
                //GrantAllocation.IsActive = true;
                return grantAllocation;
            }

            public static FundSourceAllocation Create(FundSource fundSource, string grantAllocationName)
            {
                var grantAllocation = new FundSourceAllocation(fundSource);
                grantAllocation.GrantAllocationName = grantAllocationName;
                return grantAllocation;
            }

            public static FundSourceAllocation CreateWithoutChangingName(string grantAllocationName)
            {
                var grant = TestFundSource.Create();
                var grantAllocation = new FundSourceAllocation(grant);
                grantAllocation.GrantAllocationName = grantAllocationName;
                return grantAllocation;
            }

            public static FundSourceAllocation CreateWithoutChangingName(string grantAllocationName, Organization organization)
            {
                var grant = TestFundSource.Create();
                var grantAllocation = new FundSourceAllocation(grant);
                grantAllocation.GrantAllocationName = grantAllocationName;
                grantAllocation.Organization = organization;
                grantAllocation.OrganizationID = organization.OrganizationID;
                return grantAllocation;
            }

            private static string GetTestGrantAllocationName(Organization organization, string grantAllocationName)
            {
                return string.Format("{0}{1}", organization.OrganizationName, grantAllocationName);
            }

            public static FundSourceAllocation Create(DatabaseEntities dbContext)
            {
                var grant = TestFramework.TestFundSource.Insert(dbContext);
                string testGrantAllocationName = TestFramework.MakeTestName("Test Grant Allocation Name");
                var grantAllocation = new FundSourceAllocation(grant);
                grantAllocation.GrantAllocationName = testGrantAllocationName;

                dbContext.GrantAllocations.Add(grantAllocation);
                return grantAllocation;
            }



            public static FundSourceAllocation Insert(DatabaseEntities dbContext)
            {
                var grantAllocation = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return grantAllocation;
            }
        }
    }
}