using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award Travel Line Item", FieldDefinitionEnum.GrantAllocationAwardTravel)]
    public class GrantAllocationAwardTravelLineItemCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardTravelLineItemCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}