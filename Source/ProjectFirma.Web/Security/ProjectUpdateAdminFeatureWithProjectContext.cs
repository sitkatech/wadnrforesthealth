using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("_Admin for {0} Updates with context to a specific {0}", FieldDefinitionEnum.Project)]
    public class ProjectUpdateAdminFeatureWithProjectContext : FirmaFeatureWithContext,
        IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureWithContextImpl<Project> _firmaFeatureWithContextImpl;

        public ProjectUpdateAdminFeatureWithProjectContext()
            : base(new List<Role> {Role.EsaAdmin, Role.Admin, Role.ProjectSteward})
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Project>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, Project contextModelObject)
        {

            var forbidAdmin = !HasPermissionByPerson(person) ||
                                       person.HasRole(Role.ProjectSteward) &&
                                       !person.CanStewardProject(contextModelObject);

            string possiblePermissionDeniedMessage = $"You don't have permission to make Administrative actions on {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName}";
            return PermissionCheckResult.MakeConditionalPermissionCheckResult(!forbidAdmin, possiblePermissionDeniedMessage);
        }

        public void DemandPermission(Person person, Project contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
