/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Mvc;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.User
{
    public class IndexViewData : FirmaViewData
    {
        public IndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public bool UserIsSitkaAdmin { get; }
        public string AddContactUrl { get; set; }
        public bool UserCanAddContact { get; set; }

        public List<SelectListItem> ActiveOnlyOrAllUsersSelectListItems { get; }
        public string UserGridDropdownFilterId { get; }

        public IndexViewData(Person currentPerson, List<SelectListItem> activeOnlyOrAllUsersSelectListItems, IndexGridSpec.UsersStatusFilterTypeEnum selectedEnum) : base(currentPerson)
        {
            PageTitle = "Users and Contacts";
            GridSpec = new IndexGridSpec(currentPerson)
                { ObjectNameSingular = "Person", ObjectNamePlural = "People", SaveFiltersInCookie = true };
            GridName = "UserGrid";
            GridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData(selectedEnum));

            UserIsSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(currentPerson);
            AddContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.AddContact());
            UserCanAddContact = new ContactCreateFeature().HasPermissionByPerson(currentPerson);

            ActiveOnlyOrAllUsersSelectListItems = activeOnlyOrAllUsersSelectListItems;
            UserGridDropdownFilterId = "userGridDropdownFilterId";

        }

    }
}
