using NUnit.Framework;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    [TestFixture]
    public class SocrataScheduledJobsTest : FirmaTestWithContext
    {
        [Test]
        [Ignore("The test is valid, but we want to avoid pounding on the web service when we don't have to, as we could get throttled and this one in particular burns bandwidth")]
        [Description("A partial test of Socrata interface. This is just a test to see if we can successfully download the vendor JSON file")]
        public void DownloadVendorTableData()
        {
            var socrataJob = new SocrataDataMartUpdateBackgroundJob("SampleJobName");
            socrataJob.DownloadSocrataVendorTable();

            //Assert.That(info.Where(x => x.FeatureCount == 0).ToList(), Is.Empty, $"All should have at least one {_typeOfFirmaBaseFeature.Name}");
            //Assert.That(info.Where(x => x.FeatureCount > 1).ToList(), Is.Empty, $"Should have no more than one{_typeOfFirmaBaseFeature.Name}");
        }

    }
}
