/*-----------------------------------------------------------------------
<copyright file="ProjectMapPopupViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class InteractionEventMapPopupViewData : FirmaUserControlViewData
    {
        public Models.InteractionEvent InteractionEvent { get; }
        public string InteractionEventContactsAsCommaDelimitedString { get; }
        public string InteractionEventProjectsAsCommaDelimitedString { get; }

        

        public InteractionEventMapPopupViewData(Models.InteractionEvent interactionEvent)
        {
            InteractionEvent = interactionEvent;
            InteractionEventContactsAsCommaDelimitedString = string.Join(", ", interactionEvent.InteractionEventContacts.Select(iec => iec.Person.FullNameFirstLastAndOrgShortName));
            InteractionEventProjectsAsCommaDelimitedString = string.Join(", ", interactionEvent.InteractionEventProjects.Select(iep => iep.Project.DisplayName));
        }

    }
}
