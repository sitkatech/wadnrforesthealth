using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View A Grant Allocation Award Landowner Cost Share Line Item")]
    public class GrantAllocationAwardLandownerCostShareLineItemViewFeature : FirmaFeature
    {
        public GrantAllocationAwardLandownerCostShareLineItemViewFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward, Role.Normal, Role.CanManageGrantsAndAgreements }) { }
    }
}