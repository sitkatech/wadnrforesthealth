/*-----------------------------------------------------------------------
<copyright file="UserEditFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit User")]
    public class UserEditBasicsFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Person>
    {
        private readonly FirmaFeatureWithContextImpl<Person> _firmaFeatureWithContextImpl;

        public UserEditBasicsFeature()
            : base(new List<Role> {Role.EsaAdmin, Role.Admin, Role.Normal, Role.ProjectSteward, Role.CanAddEditUsersContactsOrganizations})
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Person>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Person contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Person contextModelObject)
        {
            var hasContactManagePermissions = new ContactManageFeature().HasPermission(person);
            var hasAdminPermissions = new FirmaAdminFeature().HasPermissionByPerson(person);

            if (contextModelObject.PersonID == person.PersonID)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            if (!person.IsFullUser())
            {
                if (hasContactManagePermissions)
                {
                    return PermissionCheckResult.MakeSuccessPermissionCheckResult();
                }
            }
            else
            {
                if (hasAdminPermissions || person.HasRole(Role.CanAddEditUsersContactsOrganizations))
                {
                    return PermissionCheckResult.MakeSuccessPermissionCheckResult();
                }
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have permission to edit {contextModelObject.FullNameFirstLast}");
        }
    }
}
