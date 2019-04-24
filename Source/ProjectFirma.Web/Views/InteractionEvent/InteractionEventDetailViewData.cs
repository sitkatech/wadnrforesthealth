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

        public string EditInteractionEventBasicsUrl { get; set; }
        public string EditInteractionEventLocationSimpleUrl { get; set; }

        public List<InteractionEventContact> InteractionEventContacts { get; set; }

        public List<InteractionEventProject> InteractionEventProjects { get; set; }

        public bool UserHasInteractionEventManagePermissions { get; set; }

        public string LocationMapFormID { get; }

        public MapInitJson InteractionEventLocationSummaryMapInitJson { get; set; }



        public InteractionEventDetailViewData(Person currentPerson, Models.InteractionEvent interactionEvent, string locationMapFormID, MapInitJson interactionEventLocationSummaryMapInitJson) : base(currentPerson)
        {
            IndexUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.Index());
            EditInteractionEventBasicsUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.EditInteractionEvent(interactionEvent.PrimaryKey));
            EditInteractionEventLocationSimpleUrl =
                SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x =>
                    x.EditInteractionEventLocation(interactionEvent.PrimaryKey));
            UserHasInteractionEventManagePermissions = new InteractionEventManageFeature().HasPermissionByPerson(currentPerson);
            InteractionEvent = interactionEvent;
            InteractionEventContacts = interactionEvent.InteractionEventContacts.ToList();
            InteractionEventProjects = interactionEvent.InteractionEventProjects.ToList();
            LocationMapFormID = locationMapFormID;
            InteractionEventLocationSummaryMapInitJson = interactionEventLocationSummaryMapInitJson;
            PageTitle = interactionEvent.InteractionEventTitle;


        }

        
    }

    
}
