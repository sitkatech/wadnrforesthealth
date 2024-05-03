using System;
using Hangfire;

namespace ProjectFirma.Web.ScheduledJobs
{
    public static class JobCancellationTokenExtensions
    {
        /// <summary>
        /// Use of <see cref="JobCancellationToken.Null"/> can result in a literal null being passed into methods. This extension method allows calling the function without worrying about nulls.
        /// </summary>
        /// <param name="jobCancellationToken"></param>
        /// <param name="currentMethodName"></param>
        public static void ThrowIfCancellationRequestedDoNothingIfNull(this IJobCancellationToken jobCancellationToken)
        {
            if (jobCancellationToken == null)
            {
                return;
            }
            jobCancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// Has cancellation been requested?
        /// Use of <see cref="JobCancellationToken.Null"/> can result in a literal null being passed into methods. This extension method allows calling the function without worrying about nulls. 
        /// </summary>
        public static bool IsCancellationRequested(this IJobCancellationToken jobCancellationToken)
        {
            try
            {
                ThrowIfCancellationRequestedDoNothingIfNull(jobCancellationToken);
            }
            catch (Exception)
            {
                return true;
            }

            return false;
        }
    }
}