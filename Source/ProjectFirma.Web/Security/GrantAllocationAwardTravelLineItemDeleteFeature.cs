using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Allocation Award Travel Line Item")]
    public class GrantAllocationAwardTravelLineItemDeleteFeature : FirmaFeature
    {
        public GrantAllocationAwardTravelLineItemDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}