using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Travel Line Item")]
    public class GrantAllocationAwardTravelLineItemEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardTravelLineItem>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardTravelLineItem> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardTravelLineItemEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationAwardTravelLineItem>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationAwardTravelLineItem contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        //JJV 4/26/22 TODO
        public PermissionCheckResult HasPermission(Person person, GrantAllocationAwardTravelLineItem contextModelObject)
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