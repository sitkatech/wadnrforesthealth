using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class PersonTest : FirmaTestWithContext
    {
        [Test]
        public void EnsureAllUsersHaveASingleBaseRoleAssigned()
        {
            var allFullUsers = HttpRequestStorage.DatabaseEntities.People.ToList().Where(x => x.IsFullUser());

            var allFullUsersBaseRole = allFullUsers.Select(x => x.GetUsersBaseRole());//this line will throw an exception if the user does not have a base role assigned
            Assert.That(allFullUsers.Count() == allFullUsersBaseRole.Count(), "There is an issue with your users and their base roles.");
            
        }
    }
}