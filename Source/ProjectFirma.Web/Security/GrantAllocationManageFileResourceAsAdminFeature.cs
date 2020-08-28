using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit or Delete Grant Allocation File")]
    public class GrantAllocationManageFileResourceAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationFileResource>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationFileResource> _firmaFeatureWithContextImpl;

        public GrantAllocationManageFileResourceAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationFileResource>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationFileResource contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationFileResource contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to manage the Grant Allocation file {contextModelObject.DisplayName}");
        }
    }
}