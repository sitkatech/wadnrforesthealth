using Hangfire;

namespace ProjectFirma.Web.ScheduledJobs
{
    /// <summary>
    /// When running jobs via Hangfire, the job execution is serialized.  Much better to have a static void function for that to avoid problems. This class provides the functions that can be wired to Hangire.
    /// </summary>
    public static class ScheduledBackgroundJobLaunchHelper
    {
        public static void RunProjectUpdateKickoffReminderScheduledBackgroundJob()
        {
            throw new System.NotImplementedException();

            //var projectUpdateReminderScheduledBackgroundJob = new ProjectUpdateReminderScheduledBackgroundJob("Project Update Reminders");
            //projectUpdateReminderScheduledBackgroundJob.RunJob();
        }

        public static void RunProjectCodeImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            ProjectCodeImportHangfireBackgroundJob.Instance.RunJob(jobCancellationToken);
        }

        public static void RunVendorImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            VendorImportHangfireBackgroundJob.Instance.RunJob(jobCancellationToken);
        }
    }
}
