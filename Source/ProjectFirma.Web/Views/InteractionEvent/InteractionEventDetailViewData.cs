/*-----------------------------------------------------------------------
<copyright file="InteractionEventDetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class InteractionEventDetailViewData : FirmaViewData
    {
        public string IndexUrl { get; set; }

        public Models.InteractionEvent InteractionEvent { get; set; }

        public string EditInteractionEventUrl { get; set; }
        public string EditInteractionEventContactsUrl { get; set; }
        public string EditInteractionEventProjectsUrl { get; set; }

        public bool UserHasInteractionEventManagePermissions { get; set; }

        public InteractionEventDetailAngularViewData AngularViewData { get; set; }

        public InteractionEventDetailViewData(Person currentPerson, Models.InteractionEvent interactionEvent, IEnumerable<Person> allPeople) : base(currentPerson)
        {
            IndexUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.Index());
            EditInteractionEventUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.EditInteractionEvent(interactionEvent.PrimaryKey));
            EditInteractionEventContactsUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.EditInteractionEventContacts(interactionEvent.PrimaryKey));
            EditInteractionEventProjectsUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.EditInteractionEventProjects(interactionEvent.PrimaryKey));
            UserHasInteractionEventManagePermissions = new InteractionEventManageFeature().HasPermissionByPerson(currentPerson);
            InteractionEvent = interactionEvent;
            var allContacts = allPeople.OrderBy(x => x.LastName).Select(x => new PersonSimple(x)).ToList();
            AngularViewData = new InteractionEventDetailAngularViewData(interactionEvent, allContacts);
        }

        
    }

    public class InteractionEventDetailAngularViewData
    {
        public List<PersonSimple> AllContacts { get; set; }
        public string SaveInteractionEventContactUrl { get; }
        public int InteractionEventID { get; }

        public InteractionEventDetailAngularViewData(Models.InteractionEvent interactionEvent, List<PersonSimple> allContacts)
        {
            AllContacts = allContacts;
            SaveInteractionEventContactUrl =
                SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.SaveInteractionEventContact(interactionEvent.InteractionEventID));
            InteractionEventID = interactionEvent.InteractionEventID;
        }

    }
}
