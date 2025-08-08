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
        public class TestFundSource
        {
            public static FundSource Create()
            {
                var fundSourceStatus = GetDefaultFundSourceStatus();
                var organization = TestFramework.TestOrganization.Create();
                var fundSource = new FundSource(TestFramework.MakeTestName("FundSource", FundSource.FieldLengths.FundSourceName), fundSourceStatus, organization, 0);
                //FundSource.IsActive = true;
                return fundSource;
            }

            private static FundSourceStatus GetDefaultFundSourceStatus()
            {
                return FundSourceStatus.All.First();
            }

            public static FundSource Create(Organization organization)
            {
                var fundSourceStatus = GetDefaultFundSourceStatus();
                var testFundSourceName = GetTestFundSourceName(organization, GetTestFundSourceName(organization, "Test FundSource Name"));
                var fundSource = new FundSource(testFundSourceName, fundSourceStatus, organization, 0);
                return fundSource;
            }

            public static FundSource Create(Organization organization, string fundSourceName)
            {
                var fundSourceStatus = GetDefaultFundSourceStatus();
                var testFundSourceName = GetTestFundSourceName(organization, fundSourceName);
                var fundSource = new FundSource(testFundSourceName, fundSourceStatus, organization, 0);
                return fundSource;
            }


            private static string GetTestFundSourceName(Organization organization, string fundSourceName)
            {
                return $"{organization.OrganizationName}{fundSourceName}";
            }


            public static FundSource Create(DatabaseEntities dbContext)
            {
                var organization = TestFramework.TestOrganization.Insert(dbContext);
                string testFundSourceName = TestFramework.MakeTestName("Test FundSource Name");
                var testFundSourceStatus = GetDefaultFundSourceStatus();
                var fundSource = new FundSource(testFundSourceName, testFundSourceStatus, organization, 0);

                dbContext.FundSources.Add(fundSource);
                return fundSource;
            }

            public static FundSource Insert(DatabaseEntities dbContext)
            {
                var fundSource = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return fundSource;
            }
        }
    }
}