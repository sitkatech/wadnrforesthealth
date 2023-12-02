using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Budget Line Item")]
    public class GrantAllocationBudgetLineItemEditAsAdminFeature : FirmaFeature
    {
        public GrantAllocationBudgetLineItemEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements }) { }
    }

}