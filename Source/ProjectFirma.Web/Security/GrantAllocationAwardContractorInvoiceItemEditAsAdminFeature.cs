using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Contractor Invoice")]
    public class GrantAllocationAwardContractorInvoiceItemEditAsAdminFeature : FirmaFeature
    {
        public GrantAllocationAwardContractorInvoiceItemEditAsAdminFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {

        }

    }
}