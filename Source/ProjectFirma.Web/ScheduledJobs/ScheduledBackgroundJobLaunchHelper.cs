using Hangfire;

namespace ProjectFirma.Web.ScheduledJobs
{
    /// <summary>
    /// When running jobs via Hangfire, the job execution is serialized.  Much better to have a static void function for that to avoid problems. This class provides the functions that can be wired to Hangire.
    /// </summary>
    public static class ScheduledBackgroundJobLaunchHelper
    {
        public static void RunLoaDataImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            LoaDataImportBackgroundJob.Instance.RunJob(jobCancellationToken);
        }

        public static void RunProgramNotificationScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            ProgramNotificationScheduledBackgroundJob.Instance.RunJob(jobCancellationToken);
        }

        public static void RunVendorImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            VendorImportHangfireBackgroundJob.Instance.RunJob(jobCancellationToken);
        }

        public static void RunProjectCodeImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            ProjectCodeImportHangfireBackgroundJob.Instance.RunJob(jobCancellationToken);
        }

        public static void RunProgramIndexImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            ProgramIndexImportHangfireBackgroundJob.Instance.RunJob(jobCancellationToken);
        }

        public static void RunGrantExpenditureImportScheduledBackgroundJob(IJobCancellationToken jobCancellationToken)
        {
            GrantExpenditureImportHangfireBackgroundJob.Instance.RunJob(jobCancellationToken);
        }


    }
}
