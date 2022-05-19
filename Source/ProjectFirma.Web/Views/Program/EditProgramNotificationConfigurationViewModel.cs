/*-----------------------------------------------------------------------
<copyright file = "EditProgramNotificationConfigurationViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright(c) Tahoe Regional Planning Agency and Sitka Technology Group.All rights reserved.
<author>Sitka Technology Group</author>
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
using ProjectFirma.Web.Views.ProjectUpdate;

namespace ProjectFirma.Web.Views.Program
{
    public class EditProgramNotificationConfigurationViewModel : FormViewModel, IValidatableObject
    {

        public int ProgramID { get; set; }

        public int ProgramNotificationConfigurationID { get; set; }

        [Required]
        [DisplayName("Recurrence Interval")]
        public int RecurrenceIntervalID { get; set; }

        [Required]
        [DisplayName("Notification Type")]
        public int ProgramNotificationTypeID { get; set; }

        [Required]
        [DisplayName("Notification Email Text")]
        public HtmlString NotificationEmailText { get; set; }


        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditProgramNotificationConfigurationViewModel()
        {
        }

        public EditProgramNotificationConfigurationViewModel(ProgramNotificationConfiguration programNotificationConfiguration)
        {

            RecurrenceIntervalID = programNotificationConfiguration.RecurrenceIntervalID;
            ProgramNotificationTypeID = programNotificationConfiguration.ProgramNotificationTypeID;
            NotificationEmailText = programNotificationConfiguration.NotificationEmailTextHtmlString;

        }

        public void UpdateModel(ProgramNotificationConfiguration programNotificationConfiguration)
        {
            programNotificationConfiguration.RecurrenceIntervalID = RecurrenceIntervalID;
            programNotificationConfiguration.ProgramNotificationTypeID = ProgramNotificationTypeID;
            programNotificationConfiguration.NotificationEmailTextHtmlString = NotificationEmailText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            return new List<ValidationResult>();
        }
    }
}