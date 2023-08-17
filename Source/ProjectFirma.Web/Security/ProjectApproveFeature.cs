/*-----------------------------------------------------------------------
<copyright file="ProjectApproveFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Approve a {0}", FieldDefinitionEnum.Project)]
    public class ProjectApproveFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureWithContextImpl<Project> _firmaFeatureWithContextImpl;

        public ProjectApproveFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward, Role.ProgramEditor })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Project>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, Project contextModelObject)
        {
            var projectLabel = FieldDefinition.Project.GetFieldDefinitionLabel();
            if (!HasPermissionByPerson(person))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have permission to approve this {projectLabel}.");
            }

            if (person.HasRole(Role.ProjectSteward) &&
                !person.CanStewardProject(contextModelObject))
            {
                var organizationLabel = FieldDefinition.Organization.GetFieldDefinitionLabel();
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have permission to approve this {projectLabel} based on your relationship to the {projectLabel}'s {organizationLabel}.");
            }

            if (person.HasRole(Role.ProgramEditor) && !person.CanProgramEditorManageProject(contextModelObject))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have permission to approve this {projectLabel} based on your relationship to the {projectLabel}'s Program.");
            }

            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }

        public void DemandPermission(Person person, Project contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}