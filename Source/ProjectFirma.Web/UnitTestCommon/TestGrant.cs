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
                var grantStatus = GetDefaultGrantStatus();
                var organization = TestFramework.TestOrganization.Create();
                var grant = new FundSource(TestFramework.MakeTestName("Grant", FundSource.FieldLengths.FundSourceName), grantStatus, organization, 0);
                //Grant.IsActive = true;
                return grant;
            }

            private static FundSourceStatus GetDefaultGrantStatus()
            {
                return FundSourceStatus.All.First();
            }

            public static FundSource Create(Organization organization)
            {
                var grantStatus = GetDefaultGrantStatus();
                var testGrantName = GetTestGrantName(organization, GetTestGrantName(organization, "Test Grant Name"));
                var grant = new FundSource(testGrantName, grantStatus, organization, 0);
                return grant;
            }

            public static FundSource Create(Organization organization, string grantName)
            {
                var grantStatus = GetDefaultGrantStatus();
                var testGrantName = GetTestGrantName(organization, grantName);
                var grant = new FundSource(testGrantName, grantStatus, organization, 0);
                return grant;
            }


            private static string GetTestGrantName(Organization organization, string grantName)
            {
                return $"{organization.OrganizationName}{grantName}";
            }


            public static FundSource Create(DatabaseEntities dbContext)
            {
                var organization = TestFramework.TestOrganization.Insert(dbContext);
                string testGrantName = TestFramework.MakeTestName("Test Grant Name");
                var testGrantStatus = GetDefaultGrantStatus();
                var grant = new FundSource(testGrantName, testGrantStatus, organization, 0);

                dbContext.Grants.Add(grant);
                return grant;
            }

            public static FundSource Insert(DatabaseEntities dbContext)
            {
                var grant = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return grant;
            }
        }
    }
}