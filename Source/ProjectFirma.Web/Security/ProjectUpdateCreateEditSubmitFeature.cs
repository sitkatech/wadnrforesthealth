﻿/*-----------------------------------------------------------------------
<copyright file="ProjectUpdateManageFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    [SecurityFeatureDescription("Create, Edit, and Submit {0} Updates", FieldDefinitionEnum.Project)]
    public class ProjectUpdateCreateEditSubmitFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureForProject _firmaFeatureWithContextImpl;

        public ProjectUpdateCreateEditSubmitFeature()
            : base(new List<Role> { Role.Normal, Role.EsaAdmin, Role.Admin, Role.ProjectSteward })
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
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to Edit {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName}");
            }

            if (!contextModelObject.IsUpdatableViaProjectUpdateProcess)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName} is not updatable via the {FieldDefinition.Project.GetFieldDefinitionLabel()} Update process");
            }

            var projectIsEditableByUser = new ProjectUpdateAdminFeatureWithProjectContext().HasPermission(person, contextModelObject).HasPermission || contextModelObject.IsMyProject(person);
            if (!projectIsEditableByUser)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.ProjectID} is not editable by you.");
            }

            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}
