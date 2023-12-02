using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award Contractor Invoice", FieldDefinitionEnum.GrantAllocationAwardContractorInvoice)]
    public class GrantAllocationAwardContractorInvoiceItemCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardContractorInvoiceItemCreateFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}