using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Supplies Line Item")]
    public class GrantAllocationAwardSuppliesLineItemDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardSuppliesLineItem>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardSuppliesLineItem> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardSuppliesLineItemDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationAwardSuppliesLineItem>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationAwardSuppliesLineItem contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationAwardSuppliesLineItem contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {contextModelObject.GrantAllocationAwardSuppliesLineItemDescription}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}