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
            var vendorJob = new VendorImportHangfireBackgroundJob();
            vendorJob.DownloadSocrataVendorTable();
        }

        [Test]
        [Ignore("The test is valid, but we want to avoid pounding on the web service when we don't have to, as we could get throttled.")]
        public void DownloadProgramIndexData()
        {
            var socrataJob = new SocrataDataMartUpdateBackgroundJob("ProgramIndexTestJob");
            socrataJob.DownloadSocrataProgramIndexTable();
        }

        [Test]
        [Ignore("The test is valid, but we want to avoid pounding on the web service when we don't have to, as we could get throttled.")]
        public void DownloadSocrataProjectCodeTable()
     {
            var projectCodeImportJob = new ProjectCodeImportHangfireBackgroundJob();
            projectCodeImportJob.DownloadSocrataProjectCodeTable();
        }

    }
}
