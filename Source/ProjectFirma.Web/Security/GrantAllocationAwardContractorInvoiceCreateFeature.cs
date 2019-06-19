using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award Contractor Invoice", FieldDefinitionEnum.GrantAllocationAwardContractorInvoice)]
    public class GrantAllocationAwardContractorInvoiceCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardContractorInvoiceCreateFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
        }
    }
}