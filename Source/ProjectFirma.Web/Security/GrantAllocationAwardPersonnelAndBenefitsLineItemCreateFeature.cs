using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award Personnel And Benefits Line Item", FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefits)]
    public class GrantAllocationAwardPersonnelAndBenefitsLineItemCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardPersonnelAndBenefitsLineItemCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}