﻿/*-----------------------------------------------------------------------
<copyright file="ProjectViewFeature.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View {0}", FieldDefinitionEnum.Project)]
    public class ProjectViewFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureWithContextImpl<Project> _firmaFeatureWithContextImpl;

        public ProjectViewFeature() : base(new List<Role>())
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Project>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Project contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Project contextModelObject)
        {
            if (!HasPermissionByPerson(person))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to view {contextModelObject.DisplayName}");
            }

            if (contextModelObject.IsProposal() && person.IsAnonymousUser)
            {
                // do not allow if user is anonymous and do not show proposals to public
                if (!MultiTenantHelpers.ShowApplicationsToThePublic())
                {
                    return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not visible to you.");
                }
                // do not allow if user is anonymous and show proposals to public and stage a stage other than pending 
                if (MultiTenantHelpers.ShowApplicationsToThePublic() && contextModelObject.ProjectApprovalStatus != ProjectApprovalStatus.PendingApproval)
                {
                    return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not visible to you.");
                }
            }

            // "should only be visible and accessible to the following roles: Program Manager, Admin, Sitka Admin."
            var limitedVisibilityRoleIDs = new List<int> {Role.Admin.RoleID, Role.SitkaAdmin.RoleID, Role.ProjectSteward.RoleID};
            bool projectCanOnlyBeSeenByAdmins = contextModelObject.ProjectType.LimitVisibilityToAdmin;
            bool personHasAdminRole = limitedVisibilityRoleIDs.Contains(person.Role.RoleID);
            if (projectCanOnlyBeSeenByAdmins && !personHasAdminRole)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not visible to you.");
            }

            // Allowed
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }

        public List<Project> FilterProjectListToThoseVisibleToUser(List<Project> projects, Person person)
        {
            return projects.Where(p => this.HasPermission(person, p).HasPermission).ToList();
        }
    }
}
