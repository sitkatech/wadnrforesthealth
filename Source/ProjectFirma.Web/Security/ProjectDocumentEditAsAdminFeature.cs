using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Project Document")]
    public class ProjectDocumentEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<ProjectDocument>
    {
        private readonly FirmaFeatureWithContextImpl<ProjectDocument> _firmaFeatureWithContextImpl;

        public ProjectDocumentEditAsAdminFeature()
            : base(new List<Role> {Role.Normal, Role.EsaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<ProjectDocument>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, ProjectDocument contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, ProjectDocument contextModelObject)
        {
            var isProjectDocumentStewardButCannotStewardThisProjectDocument = person.HasRole(Role.ProjectSteward) && !person.CanStewardProject(contextModelObject.Project);
            var forbidAdmin = !HasPermissionByPerson(person) || isProjectDocumentStewardButCannotStewardThisProjectDocument;
            if (forbidAdmin)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit documents for {FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName}");
            }

            if (contextModelObject.Project.IsPendingProject())
            {
                return new ProjectCreateFeature().HasPermission(person, contextModelObject.Project);
            }

            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}
