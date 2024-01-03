﻿/*-----------------------------------------------------------------------
<copyright file="EditRolesViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.User
{
    public class EditRolesViewData : FirmaUserControlViewData
    {
        public List<RoleSimple> AllSupplementalRoles { get; }
        public List<SelectListItem> AllBaseRoles { get; }
        public bool CanEditBaseRole { get; }

        public EditRolesViewData(List<RoleSimple> supplementalRoles, List<RoleSimple> baseRoles, bool canEditBaseRole)
        {
            AllSupplementalRoles = supplementalRoles;
            AllBaseRoles = baseRoles.ToSelectList(x => x.RoleID.ToString(), y => y.RoleDisplayName).ToList();
            CanEditBaseRole = canEditBaseRole;
        }
    }
}
