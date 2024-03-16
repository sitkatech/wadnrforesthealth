using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hangfire;
using log4net;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    /// <summary>
    /// Base class for all jobs, handles the basic setup so jobs can focus on work to be done
    /// </summary>
    public abstract class ScheduledBackgroundJobBase
    {
        /// <summary>
        /// A safety guard to ensure only one job is running at a time, some jobs seem like they would collide if allowed to run concurrently or possibly drag the server down.
        /// </summary>
        public static readonly object ScheduledBackgroundGlobalJobLock = new object();
        public static readonly Dictionary<string, object> ScheduledBackgroundLockByJobName = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        public readonly string JobName;
        protected readonly ILog Logger;
        private readonly ConcurrencySetting _concurrencySetting;

        public enum ConcurrencySetting
        {
            RunJobByItself,
            OkToRunWhileOtherJobsAreRunning
        }

        /// <summary> 
        /// Jobs must have a proscribed environment to run in (for example, to prevent a job that makes a lot of calls to an external API
        /// from accidentally DOSing that API by running on all local boxes, QA, and Prod at the same time.
        /// </summary>
        public abstract List<FirmaEnvironmentType> RunEnvironments { get; }

        protected ScheduledBackgroundJobBase(string jobName, ConcurrencySetting concurrencySetting)
        {
            JobName = jobName;
            Logger = LogManager.GetLogger(GetType());
            if (!ScheduledBackgroundLockByJobName.ContainsKey(jobName))
            {
                ScheduledBackgroundLockByJobName.Add(jobName, new object());
            }
            _concurrencySetting = concurrencySetting;
            
        }

        /// <summary>
        /// This wraps the call to <see cref="RunJobImplementation"/> with all of the housekeeping for being a scheduled job.
        /// </summary>
        public void RunJob(IJobCancellationToken jobCancellationToken)
        {
            // ReSharper disable once StringLiteralTypo
            var jobRunTimestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            if (!HttpRequestStorage.PersonIsSet())
            {
                HttpRequestStorage.Person = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();
            }
            try
            {
                // No-Op if we're not running in an allowed environment
                if (!RunEnvironments.Contains(FirmaWebConfiguration.FirmaEnvironment.FirmaEnvironmentType))
                {
                    return;
                }

                switch (_concurrencySetting)
                {
                    case ConcurrencySetting.RunJobByItself:
                        Logger.Info($"Begin Batch Job Waiting For Global Lock for job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp}");
                        lock (ScheduledBackgroundGlobalJobLock)
                        {
                            Logger.Info($"End Batch Job Waiting For Global Lock for job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp}");
                            RunJobImpl(jobCancellationToken, jobRunTimestamp);
                        }
                        break;
                    case ConcurrencySetting.OkToRunWhileOtherJobsAreRunning:
                        RunJobImpl(jobCancellationToken, jobRunTimestamp);
                        break;
                    default:
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(jobCancellationToken), _concurrencySetting, "Value out of range");
                }
            }
            catch (OperationCanceledException)
            {
                // warn and swallow the exception - this just means that app pool was shutting down and that hangfire shut down our worker job
                Logger.Warn($"Job exited due to a hangfire cancellation - job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp}");
            }
            catch (Exception ex)
            {
                // Wrap and rethrow with the information about which job encountered the problem
                throw new ScheduledBackgroundJobException(JobName, ex);
            }
            finally
            {
                // Reset thread setting to default, this thread may be used to do other work
                SitkaHttpRequestStorage.DisposeItemsAndClearStore();
            }
        }

        private void RunJobImpl(IJobCancellationToken jobCancellationToken, string jobRunTimestamp)
        {
            Logger.Info($"Begin Batch Job Waiting For Job Specific Lock for job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp} ");
            lock (ScheduledBackgroundLockByJobName[JobName])
            {
                Logger.Info($"End Batch Job Waiting For Job Specific Lock for job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp} ");
                var stopWatch = Stopwatch.StartNew();
                Logger.Info($"Begin Batch RunJobImplementation for job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp}");
                RunJobImplementation(jobCancellationToken);
                stopWatch.Stop();
                Logger.Info($"End Batch RunJobImplementation for job: \"{JobName}\", jobRunTimestamp: {jobRunTimestamp}, elapsed time: {stopWatch.Elapsed}");
            }
        }

        /// <summary>
        /// Jobs can fill this in with whatever they need to run. This is called by <see cref="RunJob"/> which handles other miscellaneous stuff
        /// </summary>
        /// <param name="jobCancellationToken"></param>
        protected abstract void RunJobImplementation(IJobCancellationToken jobCancellationToken);
    }


}