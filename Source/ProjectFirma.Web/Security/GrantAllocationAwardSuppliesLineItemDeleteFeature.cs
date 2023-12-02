using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Supplies Line Item")]
    public class GrantAllocationAwardSuppliesLineItemDeleteFeature : FirmaFeature
    {
        public GrantAllocationAwardSuppliesLineItemDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}