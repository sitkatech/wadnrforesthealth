using System.Data.Entity;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CheckForUncreatedGisUploadSourceOrganizationForProgram()
        {
            var allPrograms = HttpRequestStorage.DatabaseEntities.Programs.ToList();

            Assert.IsTrue(allPrograms.All(x => x.GisUploadSourceOrganization != null), $"You have a program missing a GisUploadSourceOrganization. This will cause issues on the Program Detail page and for the GIS import process.");
        }
    }
}