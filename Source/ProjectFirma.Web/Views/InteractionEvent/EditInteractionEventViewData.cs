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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventViewData : FirmaUserControlViewData
    {

        public EditInteractionEventEditType EditInteractionEventEditType { get; }
        public IEnumerable<SelectListItem> InteractionEventTypes { get; }
        public IEnumerable<SelectListItem> StaffPeople { get; }

        public EditInteractionEventAngularViewData AngularViewData { get; set; }
        public bool UserCanManageContacts { get; set; }
        public string AddContactUrl { get; set; }
        public string AddProjectUrl { get; set; }


        public EditInteractionEventViewData(EditInteractionEventEditType editInteractionEventEditType, IEnumerable<Models.InteractionEventType> interactionEventTypes, IEnumerable<Models.Person> staffPeople, int interactionEventPrimaryKey, IEnumerable<Models.Project> allProjects)
        {
            InteractionEventTypes = interactionEventTypes.ToSelectListWithEmptyFirstRow(x => x.InteractionEventTypeID.ToString(CultureInfo.InvariantCulture), y => y.InteractionEventTypeDisplayName);//sorted in the controller
            // Sorted and filtered on controller
            StaffPeople =
                staffPeople.ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLast);

            EditInteractionEventEditType = editInteractionEventEditType;

            var allProjectSimples = allProjects.OrderBy(x => x.DisplayName).Select(x => new ProjectSimple(x)).ToList();
            AngularViewData = new EditInteractionEventAngularViewData(interactionEventPrimaryKey, new List<PersonSimple>(), allProjectSimples);
            AddProjectUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index());
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
