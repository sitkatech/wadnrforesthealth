﻿/*-----------------------------------------------------------------------
<copyright file="GrantModificationDeleteFileAsAdminFeature.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit or Delete Grant Modification File")]
    public class GrantModificationManageFileResourceAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantModificationFileResource>
    {
        private readonly FirmaFeatureWithContextImpl<GrantModificationFileResource> _firmaFeatureWithContextImpl;

        public GrantModificationManageFileResourceAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantModificationFileResource>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantModificationFileResource contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantModificationFileResource contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to manage the Grant Modification file {contextModelObject.DisplayName}");
        }
    }
}