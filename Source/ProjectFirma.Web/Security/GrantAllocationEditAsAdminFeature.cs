using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Note")]
    public class GrantAllocationNoteEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationNote>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationNote> _firmaFeatureWithContextImpl;
        private readonly GrantAllocationEditAsAdminFeature grantAllocationEditAsAdminFeature;

        public GrantAllocationNoteEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationNote>(this);
            grantAllocationEditAsAdminFeature = new GrantAllocationEditAsAdminFeature();
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationNote contextModelObject)
        {
            return grantAllocationEditAsAdminFeature.HasPermission(person, contextModelObject.GrantAllocation);
        }

        public void DemandPermission(Person person, GrantAllocationNote contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }

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
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}