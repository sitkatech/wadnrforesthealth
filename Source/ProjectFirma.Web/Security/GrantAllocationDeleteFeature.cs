using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation")]
    public class GrantAllocationDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocation>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocation> _firmaFeatureWithContextImpl;

        public GrantAllocationDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocation>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocation contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocation contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {contextModelObject.GrantAllocationName}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}