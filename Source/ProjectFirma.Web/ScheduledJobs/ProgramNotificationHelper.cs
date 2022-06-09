using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProgramNotificationHelper
    {
        public string ToolName { get; set; }
        public HtmlString IntroContent { get; set; }
        public FileResource ToolLogo { get; set; }
        public string ReminderEmailSubject { get; set; }
        public string ContactSupportEmail { get; set; }

        private const string ReminderMessageTemplate = @"Hello {0},<br/><br/>
{1}
<div style=""font-weight:bold"">Your projects that require an update are:</div>
<div style=""margin-left: 15px"">
    {2}
</div>";


        public ProgramNotificationHelper(string contactSupportEmail, HtmlString introContent, string reminderSubject,
            FileResource toolLogo, string toolDisplayName)
        {
            ContactSupportEmail = contactSupportEmail;
            IntroContent = introContent;
            ReminderEmailSubject = reminderSubject;
            ToolLogo = toolLogo;
            ToolName = toolDisplayName;
        }

        public List<ProgramNotificationSent> SendProgramNotificationMessage(IGrouping<Person, Project> contactProjectsGrouping, ProgramNotificationConfiguration programNotificationConfiguration)
        {
            var contactPerson = contactProjectsGrouping.Key;

            if (string.IsNullOrEmpty(contactPerson.Email))
            {
                return new List<ProgramNotificationSent>();
            }

            var projects = contactProjectsGrouping.ToList();

            if (projects.Count <= 0) return new List<ProgramNotificationSent>();

            var mailMessage = GenerateReminderForPerson(contactPerson, projects);
            var sendProjectUpdateReminderMessage = ProgramNotificationSent.SendMessageAndLogNotification(mailMessage,
                new List<string> {contactPerson.Email},
                new List<string>(),
                new List<string>(),
                new List<Person> {contactPerson},
                DateTime.Now, projects,
                programNotificationConfiguration);
            return sendProjectUpdateReminderMessage;
        }

        private MailMessage GenerateReminderForPerson(Person person, List<Project> projects)
        {
            var projectListAsHtmlStrings =
                GenerateProjectListAsHtmlStrings(
                    projects);


            var emailContent = GetEmailContentWithGeneratedSignature(person.FullNameFirstLast, String.Join("<br/>", projectListAsHtmlStrings));

            var htmlView = AlternateView.CreateAlternateViewFromString(emailContent, null, "text/html");
            //htmlView.LinkedResources.Add(new LinkedResource(new MemoryStream(ToolLogo.FileResourceData), "img/jpeg") {ContentId = "tool-logo"});
            var mailMessage = new MailMessage {Subject = ReminderEmailSubject, IsBodyHtml = true, Body = emailContent };
            mailMessage.AlternateViews.Add(htmlView);

            return mailMessage;
        }

        private string GetEmailContent(string fullNameFirstLast, string projectListConcatenated, string signature)
        {
            var body = string.Format(ReminderMessageTemplate,
                fullNameFirstLast,
                IntroContent,
                projectListConcatenated);
            var emailContent = $"{body}<br/>{signature}";
            return emailContent;
        }

        private string GetEmailContentWithGeneratedSignature(string fullNameFirstLast, string projectListConcatenated)
        {
            return GetEmailContent(fullNameFirstLast,
                projectListConcatenated, GetReminderMessageSignature(false));
        }

        public string GetEmailContentPreview()
        {
            var signature = GetReminderMessageSignature(true);

            return GetEmailContent(
                $"<em>{FieldDefinition.Organization.GetFieldDefinitionLabel()} Primary Contact</em>",
                "<p><em>A list of the recipient’s projects that require an update and do not have an update submitted yet will appear here.&nbsp;</em></p>",
                signature
            );
        }

        private static IEnumerable<HtmlString> GenerateProjectListAsHtmlStrings(
            IReadOnlyCollection<Project> projects)
        {
            var projectsRemaining = projects;
            var projectListAsHtmlStrings = projectsRemaining.OrderBy(project => project.DisplayName).Select(project =>
            {
                var projectUrl =
                    SitkaRoute<ProjectController>.BuildAbsoluteUrlHttpsFromExpression(controller =>
                        controller.Detail(project));
                return new HtmlString($@"<div style=""font-size:smaller""><a href=""{projectUrl}"">{project.ProjectName}</a></div>");
            }).ToList();

            return projectListAsHtmlStrings;
        }

        private string GetReminderMessageSignature(bool isPreview)
        {
            var logoUrl = isPreview ? ToolLogo.FileResourceUrl : "cid:tool-logo";

            return $@"
Thank you,<br />
{ToolName} team<br/><br/><img src=""{logoUrl}"" width=""160"" />
<p>
P.S. - You received this email because you are listed as the Primary Contact for these projects. If you feel that you should not be the Primary Contact for one or more of these projects, please <a href=""mailto:{ContactSupportEmail}"">contact support</a>.
</p>";
        }
    }
}