using Hangfire;
using NUnit.Framework;

namespace ProjectFirma.Web.ScheduledJobs
{
    [TestFixture]
    public class LoaDataImportBackgroundJobTest
    {
        [Test, Description("Simple test to at least get some coverage of LOA. However test can take over 7 minutes. Can be useful for performance profiling.")]
        public void WholeJobShouldRunWithoutErrors()
        {
            var jobCancellationToken = new JobCancellationToken(false);
            LoaDataImportBackgroundJob.Instance.RunJob(jobCancellationToken);
        }
    }
}