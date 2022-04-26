using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Note Internal")]
    public class GrantAllocationNoteInternalEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationNoteInternal>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationNoteInternal> _firmaFeatureWithContextImpl;
        private readonly GrantAllocationEditAsAdminFeature _grantAllocationEditAsAdminFeature;

        public GrantAllocationNoteInternalEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationNoteInternal>(this);
            _grantAllocationEditAsAdminFeature = new GrantAllocationEditAsAdminFeature();
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationNoteInternal contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return new PermissionCheckResult(false, "You do not have permission for this action");
            }
            return _grantAllocationEditAsAdminFeature.HasPermission(person, contextModelObject.GrantAllocation);
        }

        public void DemandPermission(Person person, GrantAllocationNoteInternal contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}