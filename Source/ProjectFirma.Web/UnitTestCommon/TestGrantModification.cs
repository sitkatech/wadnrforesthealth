using System;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public class TestGrantModification
        {
            public static GrantModification Create(string grantModificationName)
            {
                var grant = TestGrant.Create();
                var testGrantModificationStatus = GetDefaultGrantModificationStatus();
                var testGrantModificationName = GetTestGrantModificationName(grantModificationName);
                var grantModification = new GrantModification(testGrantModificationName, grant, DateTime.Now, DateTime.Now.AddYears(2), testGrantModificationStatus, 0);
                return grantModification;
                
            }

            public static GrantModification Create(Organization organization)
            {
                var grant = TestGrant.Create(organization);
                var testGrantModificationStatus = GetDefaultGrantModificationStatus();
                var testGrantModificationName = GetTestGrantModificationName("Test Grant Modification");
                var grantModification = new GrantModification(testGrantModificationName, grant, DateTime.Now, DateTime.Now.AddYears(2), testGrantModificationStatus, 0);
                return grantModification;

            }

            public static GrantModification Create()
            {
                var testGrantModificationName = GetTestGrantModificationName("Test Grant Modification");
                return TestGrantModification.Create(testGrantModificationName);
            }


            private static string GetTestGrantModificationName(string grantModificationName)
            {
                return TestFramework.MakeTestName(grantModificationName, GrantModification.FieldLengths.GrantModificationName);
            }


            public static GrantModification Create(DatabaseEntities dbContext)
            {
                string testGrantModificationName = GetTestGrantModificationName("Test Grant Modification");
                var grant = TestGrant.Create(dbContext);
                var testGrantModificationStatus = GetDefaultGrantModificationStatus();
                var grantModification = new GrantModification(testGrantModificationName, grant, DateTime.Now, DateTime.Now.AddYears(2), testGrantModificationStatus, 0);

                dbContext.GrantModifications.Add(grantModification);
                return grantModification;
            }

            private static GrantModificationStatus GetDefaultGrantModificationStatus()
            {
                return HttpRequestStorage.DatabaseEntities.GrantModificationStatuses.FirstOrDefault();
            }

            public static GrantModification Insert(DatabaseEntities dbContext)
            {
                var grantModification = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return grantModification;
            }
        }
    }
}