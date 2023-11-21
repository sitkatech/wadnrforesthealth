﻿/*-----------------------------------------------------------------------
<copyright file="CustomPageManageFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    [SecurityFeatureDescription("View Custom Page Content")]
    public class CustomPageViewFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<CustomPage>
    {
        private readonly FirmaFeatureWithContextImpl<CustomPage> _firmaFeatureWithContextImpl;

        public CustomPageViewFeature()
            : base(Role.AllBaseRoles())
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<CustomPage>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, CustomPage contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, CustomPage contextModelObject)
        {
            if (contextModelObject == null)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult("custom page is null/invalid");
            }

            var isVisible = contextModelObject.CustomPageDisplayType == CustomPageDisplayType.Public ||
                            (!person.IsAnonymousUser &&
                             contextModelObject.CustomPageDisplayType == CustomPageDisplayType.Protected);
            if (isVisible)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult("Does not have custom page view privileges");
        }
    }
}
