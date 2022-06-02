using ProjectFirma.Web.Common;
using ProjectFirmaModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Models;
using Hangfire;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProgramNotificationScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public static ProgramNotificationScheduledBackgroundJob Instance;

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static ProgramNotificationScheduledBackgroundJob()
        {
            Instance = new ProgramNotificationScheduledBackgroundJob();
        }

        public ProgramNotificationScheduledBackgroundJob() : base("Program Notifications", ConcurrencySetting.RunJobByItself)
        {
        }


        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            ProcessRemindersImpl();
        }

        protected virtual void ProcessRemindersImpl()
        {
            Logger.Info($"Processing '{JobName}'.");

            var programNotificationConfigurations = HttpRequestStorage.DatabaseEntities.ProgramNotificationConfigurations.ToList();


            foreach (var programNotificationConfiguration in programNotificationConfigurations)
            {

                var notificationSents = new List<ProgramNotificationSent>();
                var databaseEntities = new DatabaseEntities();
                var projects = databaseEntities.Projects.ToList();


                var reminderSubject = $"Time to update your Project";

                if (programNotificationConfiguration.ProgramNotificationTypeID == (int)ProgramNotificationTypeEnum.CompletedProjectsMaintenanceReminder)
                {
                    ProcessCompletedProjectsMaintenanceReminder(programNotificationConfiguration);

                }

                //if (programNotificationConfiguration.EnableProjectUpdateReminders)
                //{
                //    var projectUpdateKickOffDate = FirmaDateUtilities.LastReportingPeriodStartDateForBackgroundJob(programNotificationConfiguration.ProjectUpdateKickOffDate.GetValueOrDefault());
                //    if (DateTime.Today == projectUpdateKickOffDate)
                //    {
                //        notificationSents.AddRange(RunNotifications(projects, reminderSubject,
                //                                        programNotificationConfiguration.ProjectUpdateKickOffIntroContent, tenantAttribute));
                //    }
                //}

                //if (programNotificationConfiguration.SendPeriodicReminders)
                //{
                //    if (TodayIsReminderDayForProjectUpdateConfiguration(programNotificationConfiguration))
                //    {
                //        notificationSents.AddRange(RunNotifications(projects, reminderSubject, programNotificationConfiguration.ProjectUpdateReminderIntroContent, tenantAttribute));
                //        // notifyOnAll is false b/c we only send periodic reminders for projects whose updates haven't been submitted yet.
                //    }
                //}

                //if (programNotificationConfiguration.SendCloseOutNotification)
                //{
                //    var closeOutReminderDate = FirmaDateUtilities.LastReportingPeriodEndDateForBackgroundJob(programNotificationConfiguration.ProjectUpdateKickOffDate.GetValueOrDefault(), programNotificationConfiguration.ProjectUpdateCloseOutDate.GetValueOrDefault());
                //    if (programNotificationConfiguration.DaysBeforeCloseOutDateForReminder.HasValue)
                //    {
                //        closeOutReminderDate = closeOutReminderDate.AddDays(-programNotificationConfiguration.DaysBeforeCloseOutDateForReminder.Value);
                //    }
                //    if (DateTime.Today == closeOutReminderDate)
                //    {
                //        notificationSents.AddRange(RunNotifications(projects, reminderSubject,
                //            programNotificationConfiguration.ProjectUpdateCloseOutIntroContent, tenantAttribute));
                //    }
                //}

                databaseEntities.ProgramNotificationSents.AddRange(notificationSents);
                databaseEntities.SaveChangesWithNoAuditing();
            }

        }

        private void ProcessCompletedProjectsMaintenanceReminder(ProgramNotificationConfiguration programNotificationConfiguration)
        {
            var allProjectsInProgram = programNotificationConfiguration.Program.ProjectPrograms.Select(x => x.Project);
            var recurrenceIntervalInYears = programNotificationConfiguration.RecurrenceInterval.RecurrenceIntervalInYears;

            var projectsThatNeedNotifications = allProjectsInProgram.Where();


        }

        private static bool TodayIsReminderDayForProjectUpdateConfiguration()//ProjectUpdateSetting projectUpdateSetting)
        {
            /*
            var projectUpdateKickOffDate = FirmaDateUtilities.LastReportingPeriodStartDateForBackgroundJob(projectUpdateSetting.ProjectUpdateKickOffDate.GetValueOrDefault());
            var projectUpdateCloseOutDate = FirmaDateUtilities.LastReportingPeriodEndDateForBackgroundJob(projectUpdateSetting.ProjectUpdateKickOffDate.GetValueOrDefault(), projectUpdateSetting.ProjectUpdateCloseOutDate.GetValueOrDefault());
            var isReminderDay = DateTime.Today != projectUpdateKickOffDate && (DateTime.Today - projectUpdateKickOffDate).Days % projectUpdateSetting.ProjectUpdateReminderInterval == 0;
            var isAfterCloseOut = DateTime.Today.IsDateAfter(projectUpdateCloseOutDate);
            return isReminderDay && !isAfterCloseOut;
            */
            return false;
        }

        /// <summary>
        /// Sends a notification to all the primary contacts for the given tenant's projects.
        /// </summary>
        /// <param name="projectsForTenant"></param>
        /// <param name="reminderSubject"></param>
        /// <param name="introContent"></param>
        /// <param name="notifyOnAll"></param>
        /// <param name="attribute"></param>
        private List<Notification> RunNotifications(IEnumerable<Project> projectsForTenant, string reminderSubject, string introContent)
        {
            /*
            // Constrain to tenant boundaries.
            var toolDisplayName = attribute.ToolDisplayName;
            var contactSupportEmail = attribute.PrimaryContactPerson.Email;
            var toolLogo = attribute.TenantSquareLogoFileResourceInfo;
            var tenantID = attribute.TenantID;

            var projectUpdateNotificationHelper = new ProjectUpdateNotificationHelper(contactSupportEmail, introContent, reminderSubject, toolLogo, toolDisplayName, tenantID);

            var projectsToNotifyOn = projectsForTenant.AsQueryable().GetUpdatableProjectsThatHaveNotBeenSubmittedForBackgroundJob(tenantID);

            var projectsGroupedByPrimaryContact =
                projectsToNotifyOn.Where(x => x.GetPrimaryContact() != null).GroupBy(x => x.GetPrimaryContact())
                    .ToList();

            var notifications = projectsGroupedByPrimaryContact
                .SelectMany(x => projectUpdateNotificationHelper.SendProjectUpdateReminderMessage(x)).ToList();

            var message =
                $"Reminder emails sent to {projectsGroupedByPrimaryContact.Count} {FieldDefinitionEnum.ProjectPrimaryContact.ToType().GetFieldDefinitionLabelPluralized()} for {projectsGroupedByPrimaryContact.Count} projects requiring an update.";
            Logger.Info(message);

            return notifications;
            */
            return new List<Notification>();
        }


        
    }
}
