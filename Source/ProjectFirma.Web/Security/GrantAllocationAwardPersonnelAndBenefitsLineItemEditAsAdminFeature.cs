using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Personnel And Benefits Line Item")]
    public class GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardPersonnelAndBenefitsLineItem>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardPersonnelAndBenefitsLineItem> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantAllocationAwardPersonnelAndBenefitsLineItem>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantAllocationAwardPersonnelAndBenefitsLineItem contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }


        public PermissionCheckResult HasPermission(Person person, GrantAllocationAwardPersonnelAndBenefitsLineItem contextModelObject)
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