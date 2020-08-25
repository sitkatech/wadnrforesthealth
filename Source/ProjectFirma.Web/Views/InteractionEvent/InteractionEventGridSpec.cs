/*-----------------------------------------------------------------------
<copyright file="InteractionEventGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
using LtInfo.Common.DesignByContract;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class InteractionEventGridSpec : GridSpec<Models.InteractionEvent>
    {

        public InteractionEventGridSpec(Models.Person currentPerson)
        {
            InteractionEventGridSpecConstructorImpl(currentPerson, null, null);
        }

        public InteractionEventGridSpec(Models.Person currentPerson, Models.Project projectToAssociate)
        {
            InteractionEventGridSpecConstructorImpl(currentPerson, projectToAssociate, null);
        }

        public InteractionEventGridSpec(Models.Person currentPerson, Models.Person personToAssociate)
        {
            InteractionEventGridSpecConstructorImpl(currentPerson, null, personToAssociate);
        }

        private void InteractionEventGridSpecConstructorImpl(Person currentPerson, Models.Project optionalProjectToAssociate, Person optionalPersonToAssociate)
        {
            // ReSharper disable twice ConditionIsAlwaysTrueOrFalse
            Check.Ensure( (optionalProjectToAssociate == null && optionalPersonToAssociate == null) ||
                                   (optionalProjectToAssociate == null && optionalPersonToAssociate != null) ||
                                   (optionalProjectToAssociate != null && optionalPersonToAssociate == null));

            ObjectNameSingular = $"{Models.FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.InteractionEvent.GetFieldDefinitionLabelPluralized()}";

            SaveFiltersInCookie = true;
            var userHasManagePermissions = new InteractionEventManageFeature().HasPermissionByPerson(currentPerson);
            if (userHasManagePermissions)
            {

                var contentUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.New());
                if (optionalPersonToAssociate != null)
                {
                   contentUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.NewForAPerson(optionalPersonToAssociate));
                }
                else if (optionalProjectToAssociate != null)
                {
                    contentUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.NewForAProject(optionalProjectToAssociate));
                }
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {ObjectNameSingular}");
            }

            if (userHasManagePermissions)
            {
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), userHasManagePermissions, true),
                    30, DhtmlxGridColumnFilterType.None);
            }

            if (userHasManagePermissions)
            {
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(
                        new ModalDialogForm(x.GetEditUrl(), $"Edit {ObjectNameSingular} - {x.InteractionEventTitle}"),
                        userHasManagePermissions), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Title", x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.InteractionEventTitle), 150, DhtmlxGridColumnFilterType.Html);
            Add("Description", x => x.InteractionEventDescription, 200, DhtmlxGridColumnFilterType.Text);
            Add("Date", x => x.InteractionEventDate, 80, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.InteractionEventType.ToGridHeaderString(), x => x.InteractionEventType?.InteractionEventTypeDisplayName, 180, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DNRStaffPerson.ToGridHeaderString(), x => x.StaffPerson?.FullNameFirstLast, 180, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
