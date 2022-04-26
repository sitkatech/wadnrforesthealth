using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award")]
    public class GrantAllocationAwardEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAward>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAward> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
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
                return new PermissionCheckResult(false, "You do not have permission for this action");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}