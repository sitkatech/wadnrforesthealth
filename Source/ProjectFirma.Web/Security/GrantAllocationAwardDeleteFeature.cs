using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award")]
    public class GrantAllocationAwardDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAward>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAward> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationAward>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationAward contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationAward contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {contextModelObject.GrantAllocationAwardName}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}