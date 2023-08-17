/*-----------------------------------------------------------------------
<copyright file="ProjectCreateFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    [SecurityFeatureDescription("Edit {0}", FieldDefinitionEnum.Project)]
    public class ProjectCreateFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureForProject _firmaFeatureWithContextImpl;

        public ProjectCreateFeature()
            : base(new List<Role> { Role.Normal, Role.EsaAdmin, Role.Admin, Role.ProjectSteward, Role.ProgramEditor })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureForProject(this);
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
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit {contextModelObject.DisplayName}");
            }

            if (contextModelObject.ProjectApprovalStatus == ProjectApprovalStatus.Approved)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"This {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} has been approved and can no longer be edited through this wizard.");
            }

            if (contextModelObject.ProjectApprovalStatus == ProjectApprovalStatus.Rejected)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"This {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} has been rejected and can no longer be edited through this wizard.");
            }

            var projectIsEditableByUser = contextModelObject.IsEditableToThisPerson(person);
            if (!projectIsEditableByUser)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not editable by you.");
            }

            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}
