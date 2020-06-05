using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation File")]
    public class GrantAllocationDeleteFileAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationFileResource>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationFileResource> _firmaFeatureWithContextImpl;

        public GrantAllocationDeleteFileAsAdminFeature()
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

            // todo: change to DisplayName
            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to Grant {contextModelObject.FileResource.OriginalCompleteFileName}");
        }
    }
}