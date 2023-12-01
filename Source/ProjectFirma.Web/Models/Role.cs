﻿/*-----------------------------------------------------------------------
<copyright file="Role.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class Role : IRole
    {
        public List<FeaturePermission> GetFeaturePermissions()
        {
            var featurePermissions = this.GetFeaturePermissions(typeof(FirmaFeature));
            featurePermissions.AddRange(this.GetFeaturePermissions(typeof(FirmaFeatureWithContext)));
            return featurePermissions;
        }

        public string GetRoleDisplayName()
        {
            if (RoleName == "ProjectSteward")
            {
                return FieldDefinition.ProjectSteward.GetFieldDefinitionLabel();
            }

            if (RoleName == "Normal")
            {
                return FieldDefinition.NormalUser.GetFieldDefinitionLabel();
            }

            return RoleDisplayName;
        }

        public List<Person> GetPeopleWithRole()
        {
            return HttpRequestStorage.DatabaseEntities.People.Where(x => x.IsActive).ToList().Where(x => x.HasRoleID(RoleID)).ToList();
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Detail(RoleID)), GetRoleDisplayName());
        }

        public static string GetAnonymousRoleUrl()
        {
            return SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Anonymous());
        }

        public static List<int> GetBaseRoleIDs()
        {
            var roleIDs = AllBaseRoles().Select(x => x.RoleID).ToList();
            return roleIDs;
        }

        public static List<Role> AllSupplementalRoles()
        {
            return Role.All.Where(x => !x.IsBaseRole).ToList();
        }

        public static List<Role> AllBaseRoles()
        {
            return Role.All.Where(x => x.IsBaseRole).ToList();
        }

        public static List<Role> AllBaseRolesExceptUnassigned()
        {
            return Role.AllBaseRoles().Except(new List<Role> { Role.Unassigned }).ToList();
        }

    }
}
