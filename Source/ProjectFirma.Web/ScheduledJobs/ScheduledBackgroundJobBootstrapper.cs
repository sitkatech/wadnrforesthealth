using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Owin;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.ScheduledJobs
{
    /// <summary>
    /// All the logic to get the Hangfire Scheduled Jobs wired up in <see cref="Hangfire"/>
    /// </summary>
    public class ScheduledBackgroundJobBootstrapper
    {
        /// <summary>
        /// Configuration entry point for <see cref="FirmaOwinStartup"/> via the <see cref="Microsoft.Owin.OwinStartupAttribute"/>
        /// </summary>
        public static void ConfigureHangfireAndScheduledBackgroundJobs(IAppBuilder app)
        {
            ConfigureHangfire(app);
            ConfigureScheduledBackgroundJobs();
        }

        /// <summary>
        /// Setup the basics for <see cref="Hangfire"/>, database connectivity and security on urls
        /// </summary>
        private static void ConfigureHangfire(IAppBuilder app)
        {
            Thread.Sleep(1000);
            var sqlServerStorageOptions = new SqlServerStorageOptions
            {
                // We have scripted out the Hangfire tables, so we tell Hangfire not to insert them. 
                // This might be an issue when Hangfire does a change to its schema, but this should work for now.
                PrepareSchemaIfNecessary = false
            };
            GlobalConfiguration.Configuration.UseSqlServerStorage(FirmaWebConfiguration.DatabaseConnectionString, sqlServerStorageOptions);
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1,
                Queues = new[] {"critical","default"},
                ServerName = "ProjectFirma:1337"
            }); // 11/03/2015 MF - limit the number of worker threads, we really don't need that many - in fact we try to have each job run serially for the time being because we're not sure how concurrent the different jobs could really be.

            // Hangfire defaults to 10 retries for failed jobs; this disables that behavior by doing no automatic retries.
            // http://hangfire.readthedocs.org/en/latest/background-processing/dealing-with-exceptions.html
            // Note that specific jobs may override this; look for uses of the AutomaticRetry symbol on specific job start functions.
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireFirmaWebAuthorizationFilter() }
            });
        }

        /*
        /// <summary>
        /// Set up the jobs particular to this application
        /// </summary>
        private static void ConfigureScheduledBackgroundJobs()
        {
            var recurringJobIds = new List<string>();

            // WADNR has the luxury of not worrying about tenant.
            // because the reminder configurations are tenant-specific and user-configurable, just schedule the job to run nightly and have it check whether it's time to send a remind for each tenant.

            // Right now this job crashes, and we don't need it. Also, I can't get to Hangfire dashboard with /hangfire.
            // Ray suggests re-porting our Hangfire work from Gemini which doesn't auto-restore database and fire off jobs, so we may want to 
            // just re-port all of Gemini Hangfire rather than debug what's here in Wadnr/Firma already. -- SLG 2/19/2019
            
            AddRecurringJob(ProjectUpdateReminderScheduledBackgroundJob.JobName,
                () => ScheduledBackgroundJobLaunchHelper.RunProjectUpdateKickoffReminderScheduledBackgroundJob(),
                MakeDailyUtcCronJobStringFromLocalTime(1,23),
                recurringJobIds);
            
            // Remove any jobs we haven't explicitly scheduled
            RemoveExtraneousJobs(recurringJobIds);
        }
        */

        /// <summary>
        /// Set up the jobs particular to this application
        /// </summary>
        private static void ConfigureScheduledBackgroundJobs()
        {
            // Note that tasks here all have staggered start times - for example, all the hourly jobs start at 2, 4, 6, etc. minutes after the hour. This is to give the schedule a more 
            // predictable flow, rather than starting random, less predictable traffic jams.
            var recurringJobIds = new List<string>();

            // 1:30 AM tasks
            var oneThirtyAmCronString = MakeDailyCronJobStringFromLocalTime(1, 36); AddRecurringJob(VendorImportHangfireBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunVendorImportScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(oneThirtyAmCronString), recurringJobIds);
            AddRecurringJob(ProjectCodeImportHangfireBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunProjectCodeImportScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(oneThirtyAmCronString), recurringJobIds);
            AddRecurringJob(ProgramIndexImportHangfireBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunProgramIndexImportScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(oneThirtyAmCronString), recurringJobIds);
            AddRecurringJob(GrantExpenditureImportHangfireBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunGrantExpenditureImportScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(oneThirtyAmCronString), recurringJobIds);

            // See ConfigureScheduledBackgroundJobs in Gemini for further examples of how to schedule things at various time intervals. 
            // Commented out examples below.

            /*
            AddRecurringJob(WorkflowNotificationsScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunWorkflowNotificationsScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(Cron.Hourly(2)), recurringJobIds);
            AddRecurringJob(ChangeRequestMeetingScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunChangeRequestMeetingScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(Cron.Hourly(4)), recurringJobIds);
            AddRecurringJob(TaurusSessionDeletedExpiredScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunTaurusSessionDeletedExpiredScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(Cron.Hourly(6)), recurringJobIds);
            AddRecurringJob(SnapshotPortfolioReviewBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunSnapshotPortfolioReviewBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(Cron.Hourly(8)), recurringJobIds);

            // 11:00~ish PM tasks 
            AddRecurringJob(BudgetStartOfYearSendDigestNotificationBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunBudgetStartOfYearSendDigestNotificationBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(23, 00)), recurringJobIds);
            AddRecurringJob(ProjectSendDigestNotificationTaurusBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunProjectSendDigestNotificationTaurusBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(23, 05)), recurringJobIds);
            */


            //// 12:05 am tasks
            //// This job takes a while ideally it will be done before the Aries ETL processes.
            //// Also we don't want it to run during the 11:30pm time because that's when there's a nightly web site reset.
            //AddRecurringJob(IdentifyXYScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunIdentifyXYScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(00, 05)), recurringJobIds);

            //// 1:30~ish AM tasks
            //AddRecurringJob(SessionCleanupScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunSessionCleanupScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(1, 36)), recurringJobIds);

            //// 2:00~ish AM tasks
            //AddRecurringJob(UpdateMonitoringResourcesWebReferencePaths.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunUpdateMonitoringResourcesWebReferencePaths(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(2, 03)), recurringJobIds);
            //AddRecurringJob(CleanUpOgr2OgrFilesAndDatabaseTables.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunCleanUpOgr2OgrFilesAndDatabaseTables(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(2, 10)), recurringJobIds);

            //// 4:30~ish AM tasks
            //AddRecurringJob(FireRemindersScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunFireRemindersScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(4, 30)), recurringJobIds);
            //AddRecurringJob(AccrualObligationsScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunAccrualObligationsScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(4, 35)), recurringJobIds);
            //AddRecurringJob(StatusReportStatisticsScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunStatusReportStatisticsScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(4, 40)), recurringJobIds);
            //AddRecurringJob(AutoSetTaskStatusScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunAutoSetTaskStatusScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(4, 45)), recurringJobIds);

            //// 6:00~ish AM tasks
            //AddRecurringJob(DeleteOldPortfolioCacheRecordsScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunDeleteOldPortfolioCacheRecordsScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(5, 55)), recurringJobIds);
            //AddRecurringJob(MailDataQualityAdminScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunMailDataQualityAdminScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(6, 00)), recurringJobIds);
            //AddRecurringJob(UpdatePortfolioCacheTaurusBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunUpdatePortfolioCacheTaurusBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(6, 05)), recurringJobIds);
            //AddRecurringJob(CostSharePlanReminderEmailBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunCostSharePlanReminderEmailBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringFromLocalTime(6, 30)), recurringJobIds);

            //// every 5 minutes jobs
            //AddRecurringJob(UpdateDocumentIndexScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunUpdateDocumentIndexScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled("*/5 * * * *"), recurringJobIds);
            //AddRecurringJob(NotifyAboutNewUserAgentsThatMightBeUnwantedBots.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunNotifyAboutNewUserAgentsThatMightBeUnwantedBotsBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled("*/5 * * * *"), recurringJobIds);

            //// every 15 minutes jobs
            //AddRecurringJob(VirusScanScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunVirusScanScheduledBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled($"*/{VirusScanScheduledBackgroundJob.RunJobEveryNMinutes} * * * *"), recurringJobIds);
            //AddRecurringJob(GenerateFilesForDownloadScheduledBackgroundJob.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunGenerateFilesForDownloadScheduledBackgroundJobBackgroundJob(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled("*/15 * * * *"), recurringJobIds);


            //// every 2 hour jobs
            //AddRecurringJob(UpdateMonitoringResourcesCache.Instance.JobName, () => ScheduledBackgroundJobLaunchHelper.RunUpdateMonitoringResourcesCache(JobCancellationToken.Null), CronValueOrNeverIfJobsDisabled(MakeDailyCronJobStringForMinuteWithOffset(11, 120)), recurringJobIds);

            // Remove any jobs we haven't explicitly scheduled above
            RemoveExtraneousJobs(recurringJobIds);
        }


        private static string CronValueOrNeverIfJobsDisabled(string cronStringIfActive)
        {
            // If this is enabled, we want NO background jobs at all.
            //if (TaurusWebConfiguration.FeatureFlagDisableAllBackgroundJobs)
            //{
            //    // Some time in the far, far future (2044?)
            //    // (NO, Hangfire DOES NOT support a "never" concept for this kind of job, at least not yet. So you are stuck with setting SOME time.)
            //    // https://stackoverflow.com/questions/37587167/set-an-on-demand-only-job-in-hangfire/43255963#43255963
            //    return "0 0 29 2/12000 WED";
            //}

            // For now, always use Cron value. We could consider disabling jobs with a feature flag or other setting like we did with Gemini, if needed.
            return cronStringIfActive;
        }


        /// <summary>
        /// Convert hour/minute into cron time string for Hangfire
        /// </summary>
        private static string MakeDailyCronJobStringFromLocalTime(int hour, int minute)
        {
            var now = DateTime.Now;
            var localCrontTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0, DateTimeKind.Local);
            return Cron.Daily(localCrontTime.Hour, localCrontTime.Minute);
        }

        private static string MakeDailyCronJobStringForMinuteWithOffset(int minuteOffset, int runJobEveryNMinutes)
        {
            return $"{minuteOffset}-59/{runJobEveryNMinutes} * * * *";
        }


        private static void AddRecurringJob(string jobName, 
                                            Expression<Action> methodCallExpression,
                                            string cronExpression, List<string> recurringJobIds)
        {
            RecurringJob.AddOrUpdate(jobName, methodCallExpression, cronExpression);
            recurringJobIds.Add(jobName);
        }

        private static void RemoveExtraneousJobs(List<string> recurringJobIds)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                var recurringJobs = connection.GetRecurringJobs();
                var jobsToRemove = recurringJobs.Where(x => !recurringJobIds.Contains(x.Id)).ToList();
                foreach (var job in jobsToRemove)
                {
                    RecurringJob.RemoveIfExists(job.Id);
                }
            }
        }

        /// <summary>
        /// Hangfire defaults to a UTC time, so here convert from local time to UTC, then use the equivalent
        /// UTC time to create a cron string.
        /// 
        /// Since SetUpBackgroundHangfireJobs should be re-run when the webserver restarts, this should get
        /// updated enough to handle the problems associated with DST/UTC/TimeZone conversions. At the least,
        /// problems won't hang around for too long since AddOrUpdate will adjust the time to be the correct one
        /// after a DST change. -- SLG 03/16/2015
        /// </summary>
        private static string MakeDailyUtcCronJobStringFromLocalTime(int hour, int minute)
        {
            var utcCronTime = MakeUtcCronTime(hour, minute);
            return Cron.Daily(utcCronTime.Hour, utcCronTime.Minute);
        }

        private static string MakeYearlyUtcCronJobStringFromLocalTime(int month, int day, int hour, int minute)
        {
            var utcCronTime = MakeUtcCronTime(month, day, hour, minute);
            return Cron.Yearly(utcCronTime.Month, utcCronTime.Day, utcCronTime.Hour, utcCronTime.Minute);
        }

        private static DateTime MakeUtcCronTime(int hour, int minute)
        {
            var now = DateTime.Now;
            return MakeUtcCronTime(now.Year, now.Month, now.Day, hour, minute);
        }

        private static DateTime MakeUtcCronTime(int month, int day, int hour, int minute)
        {
            var now = DateTime.Now;
            return MakeUtcCronTime(now.Year, month, day, hour, minute);
        }

        private static DateTime MakeUtcCronTime(int year, int month, int day, int hour, int minute)
        {
            var localCrontTime = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Local);
            var utcCronTime = TimeZoneInfo.ConvertTimeToUtc(localCrontTime);
            return utcCronTime;
        }
    }
}
