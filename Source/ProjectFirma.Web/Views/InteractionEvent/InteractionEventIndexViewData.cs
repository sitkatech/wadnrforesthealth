/*-----------------------------------------------------------------------
<copyright file="InteractionEventIndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class InteractionEventIndexViewData : FirmaViewData
    {
        public readonly InteractionEventGridSpec InteractionEventGridSpec;
        public readonly string InteractionEventGridName;
        public readonly string InteractionEventGridDataUrl;


        public InteractionEventIndexViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Full Interaction/Event List";

            InteractionEventGridSpec = new InteractionEventGridSpec(currentPerson, true, true);
            InteractionEventGridName = "interactionEventsGridName";
            InteractionEventGridDataUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(tc => tc.InteractionEventGridJsonData());

        }
    }
}
