using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class RoleTest
    {
        [Test]
        public void VerifyUsersHaveOnlyOneBaseRoleAssigned()
        {
            var baseRoleIDs = Models.Role.GetBaseRoleIDs();
            var peopleWithMultipleBaseRoles = new List<Person>();

            foreach (var person in HttpRequestStorage.DatabaseEntities.PersonRoles.Select(x => x.Person).Distinct())
            {
                if (person.PersonRoles.Count > 1)
                {
                    var currentPersonsRoleIDs = person.PersonRoles.Select(x => x.RoleID);
                    if (currentPersonsRoleIDs.Intersect(baseRoleIDs).Count() > 1)
                    {
                        peopleWithMultipleBaseRoles.Add(person);
                    }
                }
            }

            Assert.That(!peopleWithMultipleBaseRoles.Any(), $"The following users have multiple base roles assigned to them: {string.Join(", ", peopleWithMultipleBaseRoles.Select(x => x.PersonID))}");

        }
    }
}