using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Contractor Invoice")]
    public class GrantAllocationAwardContractorInvoiceEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardContractorInvoice>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardContractorInvoice> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardContractorInvoiceEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationAwardContractorInvoice>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationAwardContractorInvoice contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationAwardContractorInvoice contextModelObject)
        {
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}