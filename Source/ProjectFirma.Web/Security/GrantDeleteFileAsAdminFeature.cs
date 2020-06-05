using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant File")]
    public class GrantDeleteFileAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantFileResource>
    {
        private readonly FirmaFeatureWithContextImpl<GrantFileResource> _firmaFeatureWithContextImpl;

        public GrantDeleteFileAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantFileResource>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantFileResource contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantFileResource contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to delete the Grant File {contextModelObject.DisplayName}");
        }
    }
}