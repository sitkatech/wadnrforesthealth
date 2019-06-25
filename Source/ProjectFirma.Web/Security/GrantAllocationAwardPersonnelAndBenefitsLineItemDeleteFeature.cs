using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Personnel And Benefits Line Item")]
    public class GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantAllocationAwardPersonnelAndBenefitsLineItem>
    {
        private readonly FirmaFeatureWithContextImpl<GrantAllocationAwardPersonnelAndBenefitsLineItem> _firmaFeatureWithContextImpl;

        public GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
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
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete this {FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsLineItem.GetFieldDefinitionLabel()}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}