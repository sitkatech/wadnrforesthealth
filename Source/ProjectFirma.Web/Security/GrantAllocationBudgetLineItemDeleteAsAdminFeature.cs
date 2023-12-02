using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Budget Line Item")]
    public class GrantAllocationBudgetLineItemDeleteAsAdminFeature : FirmaFeature
    {
        public GrantAllocationBudgetLineItemDeleteAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements }) { }
    }
}