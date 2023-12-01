using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Landowner Cost Share Line Item")]
    public class GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature : FirmaFeature
    {
        public GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}