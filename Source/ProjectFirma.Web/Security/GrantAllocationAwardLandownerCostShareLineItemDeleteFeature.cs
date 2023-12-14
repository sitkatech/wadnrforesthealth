using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Landowner Cost Share Line Item")]
    public class GrantAllocationAwardLandownerCostShareLineItemDeleteFeature : FirmaFeature
    {
        public GrantAllocationAwardLandownerCostShareLineItemDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}