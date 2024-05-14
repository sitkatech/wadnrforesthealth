/*-----------------------------------------------------------------------
<copyright file="ProgramNotificationGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Program
{
    public class ProgramNotificationGridSpec : GridSpec<Models.ProgramNotificationConfiguration>
    {
        public ProgramNotificationGridSpec(Person currentPerson, Models.Program programToAssociate)
        {
            var hasProgramManagePermissions = new ProgramManageFeature().HasPermissionByPerson(currentPerson);

            if (hasProgramManagePermissions)
            {
                var contentUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(x => x.NewProgramNotificationConfiguration(programToAssociate.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new Program Notification");

                Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), $"Edit {ObjectNameSingular}"),
                    hasProgramManagePermissions), 30, AgGridColumnFilterType.None, true);
            }
            Add("Program Notification Type", a => a.ProgramNotificationType.ProgramNotificationTypeDisplayName, 300, AgGridColumnFilterType.SelectFilterStrict);
            Add("Recurrence Interval in Years", a => a.RecurrenceInterval.RecurrenceIntervalInYears.ToString(), 100, AgGridColumnFilterType.Numeric);
            Add("Notification Email Text", a => a.NotificationEmailTextHtmlString, 600, AgGridColumnFilterType.Html);


        }
    }
}
