using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Personnel And Benefits Line Item")]
    public class GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature : FirmaFeature
    {
        public GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}