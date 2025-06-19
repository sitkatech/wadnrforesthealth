/*-----------------------------------------------------------------------
<copyright file="ProjectEditAsAdminFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    [SecurityFeatureDescription("Edit Project")]
    public class ProjectEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureWithContextImpl<Project> _firmaFeatureWithContextImpl;

        public ProjectEditAsAdminFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward, Role.CanEditProgram })
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
            bool isPending = contextModelObject.IsPendingProject();
            if (isPending)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You cannot edit {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName} because it is a Pending Project.");
            }

            bool isProjectStewardButCannotStewardThisProject = person != null && person.HasRole(Role.ProjectSteward) && !person.CanStewardProject(contextModelObject);
            bool forbidEdit = !HasPermissionByPerson(person) || isProjectStewardButCannotStewardThisProject;
            if (forbidEdit)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName}");
            }

            if (person != null && person.HasRole(Role.CanEditProgram) && !person.CanProgramEditorManageProject(contextModelObject))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName}");
            }

            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}
