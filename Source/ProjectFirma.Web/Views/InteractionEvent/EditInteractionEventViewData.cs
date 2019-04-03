/*-----------------------------------------------------------------------
<copyright file="EditProjectViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventViewData : FirmaUserControlViewData
    {
        public EditInteractionEventEditType EditInteractionEventEditType { get; }
        public IEnumerable<SelectListItem> InteractionEventTypesSelectListItems { get; }
        public IEnumerable<SelectListItem> StaffPeopleSelectListItems { get; }

        public EditInteractionEventAngularViewData AngularViewData { get; set; }
        public bool UserCanManageContacts { get; set; }
        public string AddContactUrl { get; set; }

        public string PostUrl { get; set; }


        private string GetPostUrl(int interactionEventPrimaryKey)
        {
            if (this.EditInteractionEventEditType == EditInteractionEventEditType.NewInteractionEventEdit)
            {
                return SitkaRoute<InteractionEventController>.BuildUrlFromExpression(c => c.New());
            }

            if (this.EditInteractionEventEditType == EditInteractionEventEditType.ExistingInteractionEventEdit)
            {
                return SitkaRoute<InteractionEventController>.BuildUrlFromExpression(c => c.EditInteractionEvent(interactionEventPrimaryKey));
            }

            throw new Exception($"Unhandled EditInteractionEventEditType: {this.EditInteractionEventEditType.ToString()}");
        }


        public EditInteractionEventViewData(Person currentPerson, EditInteractionEventEditType editInteractionEventEditType, IEnumerable<Models.InteractionEventType> interactionEventTypes, List<Models.Person> allPeople, int interactionEventID, IEnumerable<Models.Project> allProjects)
        {
            InteractionEventTypesSelectListItems = interactionEventTypes.ToSelectListWithEmptyFirstRow(x => x.InteractionEventTypeID.ToString(CultureInfo.InvariantCulture), y => y.InteractionEventTypeDisplayName);//sorted in the controller

            // Sorted and filtered on controller
            var allPeopleInWadnrOrganization = allPeople.Where(p => p?.Organization != null && p.Organization.OrganizationName == Models.Organization.OrganizationWADNR).ToList();
            StaffPeopleSelectListItems = allPeopleInWadnrOrganization.ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture), y => y.FullNameFirstLast);

            EditInteractionEventEditType = editInteractionEventEditType;

            var allProjectSimples = allProjects.OrderBy(x => x.DisplayName).Select(x => new ProjectSimple(x)).ToList();

            UserCanManageContacts = new ContactManageFeature().HasPermissionByPerson(currentPerson);
            AddContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index());
            var allContactSimples = allPeople.Select(x => new PersonSimple(x)).ToList();

            AngularViewData = new EditInteractionEventAngularViewData(interactionEventID, allContactSimples, allProjectSimples);

            PostUrl = GetPostUrl(interactionEventID);
        }


        public class EditInteractionEventAngularViewData
        {
            public List<PersonSimple> AllContacts { get; }
            public int InteractionEventID { get; }
            public List<ProjectSimple> AllProjects { get; }

            public EditInteractionEventAngularViewData(int interactionEventPrimaryKey, List<PersonSimple> allContacts, List<ProjectSimple> allProjects)
            {
                AllContacts = allContacts;
                InteractionEventID = interactionEventPrimaryKey;
                AllProjects = allProjects;
            }

        }
    }
}
