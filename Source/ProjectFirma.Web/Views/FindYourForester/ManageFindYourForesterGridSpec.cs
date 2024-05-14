/*-----------------------------------------------------------------------
<copyright file="ManageFindYourForesterGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Web;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FindYourForester
{
    public class ManageFindYourForesterGridSpec : GridSpec<ForesterWorkUnit>
    {
        public const string ForesterWorkUnitIDColumnName = "ForesterWorkUnitID";

        public ManageFindYourForesterGridSpec(Person currentPerson)
        {
            DisableSmartRendering = true;
            ObjectNameSingular = "Forester Work Unit";
            ObjectNamePlural = "Forester Work Units";
            

            AddMasterCheckBoxColumn();
            Add(ForesterWorkUnitIDColumnName, x => x.ForesterWorkUnitID, 0);
            Add($"Role", a => a.ForesterRole.ForesterRoleDisplayName, 225, AgGridColumnFilterType.None);
            Add($"Forester Work Unit Name", a => a.ForesterWorkUnitName, 165);
            Add($"Assigned to Person", a => a.PersonID.HasValue ? a.Person.GetFullNameFirstLastAsUrl() : new HtmlString("unassigned") , 200, AgGridColumnFilterType.Html);
        }
    }
}
