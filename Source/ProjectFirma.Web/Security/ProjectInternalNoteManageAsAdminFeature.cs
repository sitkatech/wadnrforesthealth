using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Manage {0}", FieldDefinitionEnum.ProjectInternalNote)]
    public class ProjectInternalNoteManageAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<ProjectInternalNote>
    {
        private readonly FirmaFeatureWithContextImpl<ProjectInternalNote> _firmaFeatureWithContextImpl;

        public ProjectInternalNoteManageAsAdminFeature()
            : base(new List<Role> { Role.Normal, Role.EsaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<ProjectInternalNote>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, ProjectInternalNote contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
        
        public PermissionCheckResult HasPermission(Person person, ProjectInternalNote contextModelObject)
        {

            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return new PermissionCheckResult(false, $"You don't have permission to Edit {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.Project.DisplayName}");
            }

            if (contextModelObject.Project.IsPendingProject())
            {
                return new ProjectCreateFeature().HasPermission(person, contextModelObject.Project);
            }

            return new ProjectEditAsAdminFeature().HasPermission(person, contextModelObject.Project);
        }
    }
}