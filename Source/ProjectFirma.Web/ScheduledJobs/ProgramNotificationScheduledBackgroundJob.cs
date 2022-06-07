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

                if (programNotificationConfiguration.ProgramNotificationTypeID == (int)ProgramNotificationTypeEnum.CompletedProjectsMaintenanceReminder)
                {
                    notificationSents.AddRange(ProcessCompletedProjectsMaintenanceReminder(programNotificationConfiguration));

                }

                databaseEntities.ProgramNotificationSents.AddRange(notificationSents);
                databaseEntities.SaveChangesWithNoAuditing();

            }

        }

        private List<ProgramNotificationSent> ProcessCompletedProjectsMaintenanceReminder(ProgramNotificationConfiguration programNotificationConfiguration)
        {
            var allProjectsInProgram = programNotificationConfiguration.Program.ProjectPrograms.Select(x => x.Project);
            var recurrenceIntervalInYears = programNotificationConfiguration.RecurrenceInterval.RecurrenceIntervalInYears;

            var completedProjects = allProjectsInProgram.Where(ap => ap.CompletionDate.HasValue && ap.CompletionDate.Value.AddYears(recurrenceIntervalInYears) > DateTime.Now);

            var projectsNeedingNotification = new List<Project>();
            
            //check that notifications have not been sent for current interval
            foreach (var project in completedProjects)
            {
                if (!project.ProgramNotificationSentProjects.Any())
                {
                    //send notification!
                    projectsNeedingNotification.Add(project);
                    continue;
                }

                var lastNotificationSentDate = project.ProgramNotificationSentProjects.Select(x => x.ProgramNotificationSent).OrderByDescending(pns => pns.ProgramNotificationSentDate).First().ProgramNotificationSentDate;
                if (lastNotificationSentDate.AddYears(recurrenceIntervalInYears) > DateTime.Now)
                {
                    //send notification!
                    projectsNeedingNotification.Add(project);
                    continue;
                }

                //no need to send notification!
            }

            return RunNotifications(projectsNeedingNotification, programNotificationConfiguration);

        }


        private List<ProgramNotificationSent> RunNotifications(List<Project> projectsNeedingNotification, ProgramNotificationConfiguration programNotificationConfiguration)
        {
            string contactSupportEmail = FirmaWebConfiguration.SitkaSupportEmail;
            string reminderSubject = "Forest Health Tracker - Time to update your Project";
            FileResource toolLogo = null;
            string toolDisplayName = string.Empty;

            var programNotificationHelper = new ProgramNotificationHelper(contactSupportEmail, programNotificationConfiguration.NotificationEmailText, reminderSubject, toolLogo, toolDisplayName);


            var projectsGroupedByPrimaryContact = projectsNeedingNotification.Where(x => x.GetPrimaryContact() != null).GroupBy(x => x.GetPrimaryContact()).ToList();
            var notifications = projectsGroupedByPrimaryContact.SelectMany(x => programNotificationHelper.SendProgramNotificationMessage(x)).ToList();

            var message = $"Reminder emails sent to {projectsGroupedByPrimaryContact.Count} Primary Contacts for {projectsGroupedByPrimaryContact.Count} projects requiring an update.";
            Logger.Info(message);

            var projectsGroupedByPrivateLandowner = projectsNeedingNotification.Where(x => x.GetPrivateLandowner() != null).GroupBy(x => x.GetPrivateLandowner()).ToList();
            notifications.AddRange(projectsGroupedByPrivateLandowner.SelectMany(x => programNotificationHelper.SendProgramNotificationMessage(x)).ToList());

            message = $"Reminder emails sent to {projectsGroupedByPrivateLandowner.Count} Private Landowner for {projectsGroupedByPrivateLandowner.Count} projects requiring an update.";
            Logger.Info(message);

            return notifications;
            
        }


        
    }
}
