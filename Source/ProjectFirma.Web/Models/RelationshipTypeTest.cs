using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class RelationshipTypeTest
    {


        [Test]
        public void VerifyLeadImplementerRelationshipTypeExistsTest()
        {
            var leadImplementerRelationshipType = HttpRequestStorage.DatabaseEntities.RelationshipTypes.Single(x => x.RelationshipTypeID == RelationshipType.LeadImplementerID);
            Assert.NotNull(leadImplementerRelationshipType, "The Lead Implementer relationship type needs to exist.");
            Assert.That(leadImplementerRelationshipType.RelationshipTypeID == 33, "Need to make sure the Lead Implementer RelationshipTypeID matches the id in the constant variable in the class.");
        }

    }
}