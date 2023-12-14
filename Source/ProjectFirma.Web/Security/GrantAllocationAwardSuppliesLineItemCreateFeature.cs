using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award Supplies Line Item", FieldDefinitionEnum.GrantAllocationAwardSupplies)]
    public class GrantAllocationAwardSuppliesLineItemCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardSuppliesLineItemCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}