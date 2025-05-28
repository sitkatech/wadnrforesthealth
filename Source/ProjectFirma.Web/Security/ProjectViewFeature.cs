/*-----------------------------------------------------------------------
<copyright file="ProjectViewFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

            // "should only be visible and accessible to the following roles: Project Steward, Program Editor, Admin, Sitka Admin."
            bool projectCanOnlyBeSeenByAdmins = contextModelObject.ProjectType.LimitVisibilityToAdmin;
            bool personHasLimitedVisibilityRole = person.HasRole(Role.Admin) || person.HasRole(Role.EsaAdmin) || person.HasRole(Role.ProjectSteward) || person.HasRole(Role.CanEditProgram);
            if (projectCanOnlyBeSeenByAdmins)
            {
                if (!personHasLimitedVisibilityRole)
                {
                    return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not visible to you.");
                }

                if (person.HasRole(Role.CanEditProgram) && !person.CanProgramEditorManageProject(contextModelObject))
                {
                    return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not visible to you.");
                }
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
