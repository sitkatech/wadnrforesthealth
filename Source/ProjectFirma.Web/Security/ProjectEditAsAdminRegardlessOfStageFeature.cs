﻿using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Project")]
    public class ProjectEditAsAdminRegardlessOfStageFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureWithContextImpl<Project> _firmaFeatureWithContextImpl;

        public ProjectEditAsAdminRegardlessOfStageFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward })
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
            var isProjectStewardButCannotStewardThisProject = person.HasRole(Role.ProjectSteward) && !person.CanStewardProject(contextModelObject);
            var forbidAdmin = !HasPermissionByPerson(person) || isProjectStewardButCannotStewardThisProject;
            if (forbidAdmin)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}