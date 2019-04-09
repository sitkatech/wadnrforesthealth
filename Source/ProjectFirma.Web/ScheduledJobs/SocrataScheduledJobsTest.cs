using NUnit.Framework;

namespace ProjectFirma.Web.ScheduledJobs
{
    [TestFixture]
    public class SocrataScheduledJobsTest
    {
        [Test]
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
