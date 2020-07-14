using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation")]
    public class GrantAllocationEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocation>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocation> _firmaFeatureWithContextImpl;

        public GrantAllocationEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
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

            if (person != null && person.PersonID != Person.AnonymousPersonID && GrantedRoles.Select(x => x.RoleID).Contains(person.RoleID))
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }
            return PermissionCheckResult.MakeFailurePermissionCheckResult("You are not allowed to add files to Grant Allocations");

        }
    }
}