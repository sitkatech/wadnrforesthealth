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
        public class TestGrantAllocation
        {
            public static GrantAllocation Create()
            {
                var grantModification = TestGrantModification.Create();
                var org = TestOrganization.Create();
                var grantAllocation = TestGrantAllocation.Create(grantModification, GetTestGrantAllocationName(org, "Test Grant Allocation"));
                //GrantAllocation.IsActive = true;
                return grantAllocation;
            }

            public static GrantAllocation Create(GrantModification grantModification, string grantAllocationName)
            {
                var grantAllocation = new GrantAllocation(grantModification);
                grantAllocation.GrantAllocationName = grantAllocationName;
                return grantAllocation;
            }

            public static GrantAllocation CreateWithoutChangingName(string grantAllocationName)
            {
                var grantModification = TestGrantModification.Create();
                var grantAllocation = new GrantAllocation(grantModification);
                grantAllocation.GrantAllocationName = grantAllocationName;
                return grantAllocation;
            }

            public static GrantAllocation CreateWithoutChangingName(string grantAllocationName, Organization organization)
            {
                var grantModification = TestGrantModification.Create();
                var grantAllocation = new GrantAllocation(grantModification);
                grantAllocation.GrantAllocationName = grantAllocationName;
                grantAllocation.Organization = organization;
                grantAllocation.OrganizationID = organization.OrganizationID;
                return grantAllocation;
            }

            private static string GetTestGrantAllocationName(Organization organization, string grantAllocationName)
            {
                return string.Format("{0}{1}", organization.OrganizationName, grantAllocationName);
            }

            public static GrantAllocation Create(DatabaseEntities dbContext)
            {
                var grantModification = TestFramework.TestGrantModification.Insert(dbContext);
                string testGrantAllocationName = TestFramework.MakeTestName("Test Grant Allocation Name");
                var grantAllocation = new GrantAllocation(grantModification);
                grantAllocation.GrantAllocationName = testGrantAllocationName;

                dbContext.GrantAllocations.Add(grantAllocation);
                return grantAllocation;
            }



            public static GrantAllocation Insert(DatabaseEntities dbContext)
            {
                var grantAllocation = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return grantAllocation;
            }
        }
    }
}