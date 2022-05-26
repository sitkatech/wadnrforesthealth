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
        public new const string JobName = "Program Notifications";

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        public ProgramNotificationScheduledBackgroundJob(string jobName, ConcurrencySetting concurrencySetting) : base(jobName, concurrencySetting)
        {
        }

        protected  void RunJobImplementation()
        {
            //ProcessRemindersImpl();
        }
        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void ProcessRemindersImpl()
        {
            Logger.Info($"Processing '{JobName}' notifications.");

            var projectUpdateSettings = HttpRequestStorage.DatabaseEntities.ProgramNotificationConfigurations.ToList();

            /*
            foreach (var projectUpdateSetting in projectUpdateSettings)
            {

                var notifications = new List<Notification>();
                var tenantID = projectUpdateSetting.TenantID;
                var databaseEntities = new DatabaseEntities(tenantID);
                var projects = databaseEntities.Projects.ToList();

                var tenantAttribute = databaseEntities.TenantAttributes.Single();
                var reminderSubject = $"Time to update your {FieldDefinitionEnum.Project.ToType().GetFieldDefinitionLabelPluralizedForBackgroundJob(tenantAttribute.TenantID)}";
                if (projectUpdateSetting.EnableProjectUpdateReminders)
                {
                    var projectUpdateKickOffDate = FirmaDateUtilities.LastReportingPeriodStartDateForBackgroundJob(projectUpdateSetting.ProjectUpdateKickOffDate.GetValueOrDefault());
                    if (DateTime.Today == projectUpdateKickOffDate)
                    {
                        notifications.AddRange(RunNotifications(projects, reminderSubject,
                                                        projectUpdateSetting.ProjectUpdateKickOffIntroContent, tenantAttribute));
                    }
                }

                if (projectUpdateSetting.SendPeriodicReminders)
                {
                    if (TodayIsReminderDayForProjectUpdateConfiguration(projectUpdateSetting))
                    {
                        notifications.AddRange(RunNotifications(projects, reminderSubject, projectUpdateSetting.ProjectUpdateReminderIntroContent, tenantAttribute));
                        // notifyOnAll is false b/c we only send periodic reminders for projects whose updates haven't been submitted yet.
                    }
                }

                if (projectUpdateSetting.SendCloseOutNotification)
                {
                    var closeOutReminderDate = FirmaDateUtilities.LastReportingPeriodEndDateForBackgroundJob(projectUpdateSetting.ProjectUpdateKickOffDate.GetValueOrDefault(), projectUpdateSetting.ProjectUpdateCloseOutDate.GetValueOrDefault());
                    if (projectUpdateSetting.DaysBeforeCloseOutDateForReminder.HasValue)
                    {
                        closeOutReminderDate = closeOutReminderDate.AddDays(-projectUpdateSetting.DaysBeforeCloseOutDateForReminder.Value);
                    }
                    if (DateTime.Today == closeOutReminderDate)
                    {
                        notifications.AddRange(RunNotifications(projects, reminderSubject,
                            projectUpdateSetting.ProjectUpdateCloseOutIntroContent, tenantAttribute));
                    }
                }

                databaseEntities.AllNotifications.AddRange(notifications);
                databaseEntities.SaveChangesWithNoAuditing(tenantID);
            }
            */
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
