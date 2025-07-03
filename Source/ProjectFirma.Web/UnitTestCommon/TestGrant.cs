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
        public class TestGrant
        {
            public static Grant Create()
            {
                var grantStatus = GetDefaultGrantStatus();
                var organization = TestFramework.TestOrganization.Create();
                var grant = new Grant(TestFramework.MakeTestName("Grant", Grant.FieldLengths.GrantName), grantStatus, organization, 0);
                //Grant.IsActive = true;
                return grant;
            }

            private static GrantStatus GetDefaultGrantStatus()
            {
                return GrantStatus.All.First();
            }

            public static Grant Create(Organization organization)
            {
                var grantStatus = GetDefaultGrantStatus();
                var testGrantName = GetTestGrantName(organization, GetTestGrantName(organization, "Test Grant Name"));
                var grant = new Grant(testGrantName, grantStatus, organization, 0);
                return grant;
            }

            public static Grant Create(Organization organization, string grantName)
            {
                var grantStatus = GetDefaultGrantStatus();
                var testGrantName = GetTestGrantName(organization, grantName);
                var grant = new Grant(testGrantName, grantStatus, organization, 0);
                return grant;
            }


            private static string GetTestGrantName(Organization organization, string grantName)
            {
                return $"{organization.OrganizationName}{grantName}";
            }


            public static Grant Create(DatabaseEntities dbContext)
            {
                var organization = TestFramework.TestOrganization.Insert(dbContext);
                string testGrantName = TestFramework.MakeTestName("Test Grant Name");
                var testGrantStatus = GetDefaultGrantStatus();
                var grant = new Grant(testGrantName, testGrantStatus, organization, 0);

                dbContext.Grants.Add(grant);
                return grant;
            }

            public static Grant Insert(DatabaseEntities dbContext)
            {
                var grant = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return grant;
            }
        }
    }
}