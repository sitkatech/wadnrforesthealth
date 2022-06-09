using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using LtInfo.Common.Email;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProgramNotificationSent
    {
        public static List<ProgramNotificationSent> SendMessageAndLogNotification(MailMessage mailMessage, IEnumerable<string> emailsToSendTo, IEnumerable<string> emailsToReplyTo, IEnumerable<string> emailsToCc, List<Person> notificationPeople, DateTime notificationDate, List<Project> notificationProjects, ProgramNotificationConfiguration programNotificationConfiguration)
        {
            SendMessage(mailMessage, emailsToSendTo, emailsToReplyTo, emailsToCc);
            var notifications = new List<ProgramNotificationSent>();
            foreach (var notificationPerson in notificationPeople)
            {
                var notification = new ProgramNotificationSent(programNotificationConfiguration, notificationPerson, notificationDate); 
                notification.ProgramNotificationSentProjects = notificationProjects.Select(p => new ProgramNotificationSentProject(notification, p)).ToList();
                notifications.Add(notification);
            }
            return notifications;
        }

        public static void SendMessage(MailMessage mailMessage, IEnumerable<string> emailsToSendTo, IEnumerable<string> emailsToReplyTo, IEnumerable<string> emailsToCc)
        {
            mailMessage.From = DoNotReplyMailAddress();
            foreach (var email in emailsToSendTo)
            {
                mailMessage.To.Add(email);
            }
            foreach (var email in emailsToReplyTo)
            {
                mailMessage.ReplyToList.Add(email);
            }
            foreach (var emailToCc in emailsToCc)
            {
                mailMessage.CC.Add(emailToCc);
            }            
            SitkaSmtpClient.Send(mailMessage);
        }

        public static MailAddress DoNotReplyMailAddress()
        {
            return new MailAddress(FirmaWebConfiguration.DoNotReplyEmail, MultiTenantHelpers.GetToolDisplayName());
        }
    }
}