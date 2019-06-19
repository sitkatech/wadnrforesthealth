using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Landowner Cost Share Line Item")]
    public class GrantAllocationAwardLandownerCostShareLineItemDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardLandownerCostShareLineItem>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardLandownerCostShareLineItem> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardLandownerCostShareLineItemDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationAwardLandownerCostShareLineItem>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationAwardLandownerCostShareLineItem contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantAllocationAwardLandownerCostShareLineItem contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {FieldDefinition.GrantAllocationAwardLandownerCostShare} with ID: {contextModelObject.GrantAllocationAwardLandownerCostShareLineItemID}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}