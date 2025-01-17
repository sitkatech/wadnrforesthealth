﻿/*-----------------------------------------------------------------------
<copyright file = "EditProjectUpdateConfigurationViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright(c) Tahoe Regional Planning Agency and Environmental Science Associates.All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU Affero General Public License<http://www.gnu.org/licenses/> for more details.

Source code is available upon request via<support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class EditProjectUpdateConfigurationViewModel : FormViewModel, IValidatableObject
    {
        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectUpdateKickOffDate)]
        public DateTime? ProjectUpdateKickOffDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectUpdateCloseOutDate)]
        public DateTime? ProjectUpdateCloseOutDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectUpdateReminderInterval)]
        public int? ProjectUpdateReminderInterval { get; set; }

        [Required]
        [DisplayName("Enable Project Update Reminders?")]
        public bool? EnableProjectUpdateReminders { get; set; }

        [Required]
        [DisplayName("Send Periodic Reminders?")]
        public bool? SendPeriodicReminders { get; set; }

        [Required]
        [DisplayName("Send Close-Out Notification?")]
        public bool? SendCloseOutNotification { get; set; }

        [DisplayName("Project Update Kick-Off Email Content")]
        public HtmlString ProjectUpdateKickOffIntroContent { get; set; }

        [DisplayName("Project Update Reminder Email Content")]
        public HtmlString ProjectUpdateReminderIntroContent { get; set; }

        [DisplayName("Project Update Close-Out Email Content")]
        public HtmlString ProjectUpdateCloseOutIntroContent { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditProjectUpdateConfigurationViewModel()
        {
        }

        public EditProjectUpdateConfigurationViewModel(ProjectUpdateConfiguration projectUpdateConfiguration)
        {
            ProjectUpdateKickOffDate = projectUpdateConfiguration?.ProjectUpdateKickOffDate;
            ProjectUpdateCloseOutDate = projectUpdateConfiguration?.ProjectUpdateCloseOutDate;
            ProjectUpdateReminderInterval = projectUpdateConfiguration?.ProjectUpdateReminderInterval;
            EnableProjectUpdateReminders = projectUpdateConfiguration?.EnableProjectUpdateReminders;
            SendPeriodicReminders = projectUpdateConfiguration?.SendPeriodicReminders;
            SendCloseOutNotification = projectUpdateConfiguration?.SendCloseOutNotification;
            ProjectUpdateKickOffIntroContent = projectUpdateConfiguration?.ProjectUpdateKickOffIntroContentHtmlString;
            ProjectUpdateReminderIntroContent = projectUpdateConfiguration?.ProjectUpdateReminderIntroContentHtmlString;
            ProjectUpdateCloseOutIntroContent = projectUpdateConfiguration?.ProjectUpdateCloseOutIntroContentHtmlString;
        }

        public void UpdateModel(ProjectUpdateConfiguration projectUpdateConfiguration)
        {
            projectUpdateConfiguration.ProjectUpdateKickOffDate = ProjectUpdateKickOffDate;
            projectUpdateConfiguration.ProjectUpdateCloseOutDate = ProjectUpdateCloseOutDate;
            projectUpdateConfiguration.ProjectUpdateReminderInterval = ProjectUpdateReminderInterval;
            projectUpdateConfiguration.EnableProjectUpdateReminders = EnableProjectUpdateReminders.GetValueOrDefault(); // will never be null
            projectUpdateConfiguration.SendPeriodicReminders = SendPeriodicReminders.GetValueOrDefault(); // will never be null
            projectUpdateConfiguration.SendCloseOutNotification = SendCloseOutNotification.GetValueOrDefault(); // will never be null
            projectUpdateConfiguration.ProjectUpdateKickOffIntroContent = ProjectUpdateKickOffIntroContent?.ToString();
            projectUpdateConfiguration.ProjectUpdateReminderIntroContent =
                ProjectUpdateReminderIntroContent?.ToString();
            projectUpdateConfiguration.ProjectUpdateCloseOutIntroContent =
                ProjectUpdateCloseOutIntroContent?.ToString();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // these bools will never be null due to RequiredAttribute
            if (EnableProjectUpdateReminders ?? false)
            {
                if (string.IsNullOrWhiteSpace(ProjectUpdateKickOffIntroContent?.ToString()))
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, HtmlString>(
                        $"You must provide {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Kick-Off Email Content if {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Reminders are enabled.",
                        m => m.ProjectUpdateKickOffIntroContent);
                if (!ProjectUpdateKickOffDate.HasValue)
                {
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                        $"You must provide a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Kick-Off Date if {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Reminders are enabled.",
                        m => m.ProjectUpdateKickOffDate);
                }
                else if (ProjectUpdateKickOffDate.Value < DateTime.Today)
                {
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                        $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Kick-Off Date cannot be in the past.", m => m.ProjectUpdateKickOffDate);
                }
            }

            if (SendPeriodicReminders ?? false)
            {
                if (string.IsNullOrWhiteSpace(ProjectUpdateReminderIntroContent?.ToString()))
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, HtmlString>(
                        $"You must provide {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Reminder Email Content if Periodic Reminders are enabled.",
                        m => m.ProjectUpdateReminderIntroContent);

                if (!ProjectUpdateReminderInterval.HasValue)
                {
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, int?>(
                        $"You must provide a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Reminder Interval if Periodic Reminders are enabled.",
                        m => m.ProjectUpdateReminderInterval);
                }
                else if (ProjectUpdateReminderInterval.Value < 7)
                {
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, int?>(
                        $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Reminder Interval must be at least 7 days.",
                        m => m.ProjectUpdateReminderInterval);
                }
                else if (ProjectUpdateReminderInterval > 365)
                {
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, int?>(
                        $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Reminder Interval cannot be greater than 365 days.",
                        m => m.ProjectUpdateReminderInterval);
                }
            }

            if (SendCloseOutNotification ?? false)
            {
                if (string.IsNullOrWhiteSpace(ProjectUpdateCloseOutIntroContent?.ToString()))
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, HtmlString>(
                        $"You must provide {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Email Content if {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Notifications are enabled.",
                        m => m.ProjectUpdateCloseOutIntroContent);
                if (!ProjectUpdateCloseOutDate.HasValue)
                {
                    yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                        $"You must provide a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Date if {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Notifications are enabled.",
                        m => m.ProjectUpdateCloseOutDate);
                }
                else if (ProjectUpdateKickOffDate.HasValue)
                {
                    if (!EnableProjectUpdateReminders ?? false)
                        yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                            $"You cannot set a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Date without also setting a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Kick-Off Date",
                            m => m.ProjectUpdateCloseOutDate);
                    if (ProjectUpdateKickOffDate.Value.AddYears(1) < ProjectUpdateCloseOutDate.Value)
                        yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                            $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Date cannot be more than 1 year later than {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Kick-Off Date.",
                            m => m.ProjectUpdateCloseOutDate);
                    if (ProjectUpdateKickOffDate.Value >= ProjectUpdateCloseOutDate.Value)
                        yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                            $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Date must be later than {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Kick-Off Date.",
                            m => m.ProjectUpdateCloseOutDate);
                    if (ProjectUpdateCloseOutDate.Value < DateTime.Today)
                        yield return new SitkaValidationResult<EditProjectUpdateConfigurationViewModel, DateTime?>(
                            $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update Close-Out Date cannot be in the past.", m => m.ProjectUpdateKickOffDate);
                }
            }
        }
    }
}