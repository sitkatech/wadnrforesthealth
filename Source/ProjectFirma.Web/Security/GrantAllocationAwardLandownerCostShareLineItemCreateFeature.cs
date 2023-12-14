using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award Landowner Cost Share Line Item", FieldDefinitionEnum.GrantAllocationAwardLandownerCostShare)]
    public class GrantAllocationAwardLandownerCostShareLineItemCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardLandownerCostShareLineItemCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}