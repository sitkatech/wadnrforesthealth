using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Personnel And Benefits Line Item")]
    public class GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature : FirmaFeature
    {
        public GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}