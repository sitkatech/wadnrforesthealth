using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit or Delete Priority Landscape File")]
    public class PriorityLandscapeManageFileResourceAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<PriorityLandscapeFileResource>
    {
        private readonly FirmaFeatureWithContextImpl<PriorityLandscapeFileResource> _firmaFeatureWithContextImpl;

        public PriorityLandscapeManageFileResourceAsAdminFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<PriorityLandscapeFileResource>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, PriorityLandscapeFileResource contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, PriorityLandscapeFileResource contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to manage the {FieldDefinition.PriorityLandscape.FieldDefinitionDisplayName} file {contextModelObject.DisplayName}");
        }
    }
}