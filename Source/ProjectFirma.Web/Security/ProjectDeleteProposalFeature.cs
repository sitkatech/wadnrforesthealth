using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete proposed projects")]
    public class ProjectDeleteProposalFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Project>
    {
        private readonly FirmaFeatureWithContextImpl<Project> _lakeTahoeInfoFeatureWithContextImpl;

        public ProjectDeleteProposalFeature() : base(FirmaBaseFeatureHelpers.AllBaseRolesExceptUnassigned)
        {
            _lakeTahoeInfoFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Project>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Project contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Project contextModelObject)
        {
            var possiblePermissionDeniedMessage = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} {contextModelObject.DisplayName} is not deletable by you";
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return new PermissionCheckResult(false, possiblePermissionDeniedMessage);
            }

            if (new ProjectDeleteFeature().HasPermissionByPerson(person))
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            bool userHasPermission = contextModelObject.IsMyProject(person) 
                                     && (contextModelObject.ProjectApprovalStatus == ProjectApprovalStatus.Draft || contextModelObject.ProjectApprovalStatus == ProjectApprovalStatus.Rejected);
           
            return PermissionCheckResult.MakeConditionalPermissionCheckResult(userHasPermission, possiblePermissionDeniedMessage);
        }
    }
}