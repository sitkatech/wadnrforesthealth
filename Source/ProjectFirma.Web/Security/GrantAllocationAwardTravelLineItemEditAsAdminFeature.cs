using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award Travel Line Item")]
    public class GrantAllocationAwardTravelLineItemEditAsAdminFeature : FirmaFeature
    {
        public GrantAllocationAwardTravelLineItemEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}