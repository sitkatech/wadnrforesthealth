using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Contractor Invoice")]
    public class GrantAllocationAwardContractorInvoiceItemDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardContractorInvoice>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardContractorInvoice> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardContractorInvoiceItemDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
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
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {FieldDefinition.GrantAllocationAwardContractorInvoice} with ID: {contextModelObject.GrantAllocationAwardContractorInvoiceDescription}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}