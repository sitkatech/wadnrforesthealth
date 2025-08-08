using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Fund Source Allocation Budget Line Item")]
    public class FundSourceAllocationBudgetLineItemEditAsAdminFeature : FirmaFeature
    {
        public FundSourceAllocationBudgetLineItemEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements }) { }
    }

}