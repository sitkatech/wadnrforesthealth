using Hangfire;
using NUnit.Framework;

namespace ProjectFirma.Web.ScheduledJobs
{
    [TestFixture]
    public class LoaDataImportBackgroundJobTest
    {
        [Ignore("9/1/23 TK - This test is erratic. It has been failing regularly on the build server but I cannot get it to fail on my local machine. Ignoring for now")]
        [Test, Description("Simple test to at least get some coverage of LOA. However test can take over 7 minutes. Can be useful for performance profiling.")]
        public void WholeJobShouldRunWithoutErrors()
        {
            var jobCancellationToken = new JobCancellationToken(false);
            Assert.DoesNotThrow(() => LoaDataImportBackgroundJob.Instance.RunJob(jobCancellationToken), $"Job {nameof(LoaDataImportBackgroundJob)} should complete without exceptions");
        }
    }
}