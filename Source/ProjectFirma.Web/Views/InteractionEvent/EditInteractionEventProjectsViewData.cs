/*-----------------------------------------------------------------------
<copyright file="EditInteractionEventProjectsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventProjectsViewData : FirmaViewData
    {

        public EditInteractionEventProjectsAngularViewData AngularViewData { get; set; }
        public string AddProjectUrl { get; set; }

        public EditInteractionEventProjectsViewData(Person currentPerson, int interactionEventPrimaryKey, IEnumerable<Models.Project> allProjects) : base(currentPerson)
        {

            var allProjectSimples = allProjects.OrderBy(x => x.DisplayName).Select(x => new ProjectSimple(x)).ToList();
            AngularViewData = new EditInteractionEventProjectsAngularViewData(interactionEventPrimaryKey, allProjectSimples);
            AddProjectUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index());
        }

        public EditInteractionEventProjectsViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
        }
    }

    public class EditInteractionEventProjectsAngularViewData
    {
        public List<ProjectSimple> AllProjects { get; set; }
        public int InteractionEventID { get; }

        public EditInteractionEventProjectsAngularViewData(int interactionEventPrimaryKey, List<ProjectSimple> allProjects)
        {
            AllProjects = allProjects;
            InteractionEventID = interactionEventPrimaryKey;
        }

    }
}