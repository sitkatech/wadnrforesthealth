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
            ProcessRemindersImpl(jobCancellationToken);
        }

        protected virtual void ProcessRemindersImpl(IJobCancellationToken jobCancellationToken)
        {
            Logger.Info($"Processing '{JobName}'.");

            var programNotificationConfigurations = HttpRequestStorage.DatabaseEntities.ProgramNotificationConfigurations.ToList();


            foreach (var programNotificationConfiguration in programNotificationConfigurations)
            {
                var notificationSents = new List<ProgramNotificationSent>();

                if (programNotificationConfiguration.ProgramNotificationTypeID == (int)ProgramNotificationTypeEnum.CompletedProjectsMaintenanceReminder)
                {
                    notificationSents.AddRange(ProcessCompletedProjectsMaintenanceReminder(programNotificationConfiguration));

                }

                HttpRequestStorage.DatabaseEntities.ProgramNotificationSents.AddRange(notificationSents);
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

                jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
            }

        }

        private List<ProgramNotificationSent> ProcessCompletedProjectsMaintenanceReminder(ProgramNotificationConfiguration programNotificationConfiguration)
        {
            var recurrenceIntervalInYears = programNotificationConfiguration.RecurrenceInterval.RecurrenceIntervalInYears;

            var allProjectsInProgram = programNotificationConfiguration.Program.ProjectPrograms.Select(x => x.Project);
            var allCompletedProjectsInProgram = allProjectsInProgram.Where(x => x.ProjectStageID == (int)ProjectStageEnum.Completed);
            var completedProjectsThatMightNeedNotification = allCompletedProjectsInProgram.Where(ap => ap.CompletionDate.HasValue && ap.CompletionDate.Value.AddYears(recurrenceIntervalInYears).CompareTo(DateTime.Now) < 0);

            var projectsNeedingNotification = new List<Project>();
            
            //check that notifications have not been sent for current interval
            foreach (var project in completedProjectsThatMightNeedNotification)
            {
                if (!project.ProgramNotificationSentProjects.Any())
                {
                    //send notification!
                    projectsNeedingNotification.Add(project);
                    continue;
                }

                var lastNotificationSentDate = project.ProgramNotificationSentProjects.Select(x => x.ProgramNotificationSent).OrderByDescending(pns => pns.ProgramNotificationSentDate).First().ProgramNotificationSentDate;
                if (lastNotificationSentDate.AddYears(recurrenceIntervalInYears).CompareTo(DateTime.Now) < 0)
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
            string contactSupportEmail = FirmaWebConfiguration.WadnrSupportEmail;
            string reminderSubject = "Forest Health Tracker - Time to update your Project";
            string toolDisplayName = FirmaWebConfiguration.WebsiteDisplayName;

            var programNotificationHelper = new ProgramNotificationHelper(contactSupportEmail, programNotificationConfiguration.NotificationEmailTextHtmlString, reminderSubject, toolDisplayName);


            var projectsWithPrimaryContactWithEmail = projectsNeedingNotification.Where(x => x.GetPrimaryContact() != null && !string.IsNullOrEmpty(x.GetPrimaryContact().Email)).ToList();
            var projectsGroupedByPrimaryContact = projectsWithPrimaryContactWithEmail.GroupBy(x => x.GetPrimaryContact()).ToList();
            var notifications = projectsGroupedByPrimaryContact.SelectMany(x => programNotificationHelper.SendProgramNotificationMessage(x, programNotificationConfiguration)).ToList();

            var totalProjectsCapturedInThePrimaryContactGroupingEmails = projectsGroupedByPrimaryContact.SelectMany(group => group.ToList().Select(x => x.ProjectID)).Distinct().Count();
            var message = $"Reminder emails sent to {projectsGroupedByPrimaryContact.Count} Primary Contacts for {totalProjectsCapturedInThePrimaryContactGroupingEmails} projects requiring an update.";
            Logger.Info(message);

            var projectsWithPrivateLandowners = projectsNeedingNotification.Where(x => x.GetPrivateLandowners().Any()).ToList();
            var projectPersonsWithPrivateLandownersWithEmails = projectsWithPrivateLandowners.SelectMany(x => x.ProjectPeople).Where(pp => pp.ProjectPersonRelationshipTypeID == ProjectPersonRelationshipType.PrivateLandowner.ProjectPersonRelationshipTypeID && !string.IsNullOrEmpty(pp.Person.Email)).ToList();
            var projectsGroupedByPrivateLandowner = projectPersonsWithPrivateLandownersWithEmails.GroupBy(x => x.Person, y => y.Project).ToList();
            notifications.AddRange(projectsGroupedByPrivateLandowner.SelectMany(x => programNotificationHelper.SendProgramNotificationMessage(x, programNotificationConfiguration)).ToList());

            var totalProjectsCapturedInThePrivateLandownerGroupingEmails = projectsGroupedByPrivateLandowner.SelectMany(group => group.ToList().Select(x => x.ProjectID)).Distinct().Count();
            message = $"Reminder emails sent to {projectsGroupedByPrivateLandowner.Count} Private Landowner for {totalProjectsCapturedInThePrivateLandownerGroupingEmails} projects requiring an update.";
            Logger.Info(message);

            return notifications;
            
        }


        
    }
}
