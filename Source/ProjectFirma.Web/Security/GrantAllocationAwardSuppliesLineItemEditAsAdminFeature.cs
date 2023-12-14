using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Supplies Line Item")]
    public class GrantAllocationAwardSuppliesLineItemEditAsAdminFeature : FirmaFeature
    {
        public GrantAllocationAwardSuppliesLineItemEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}