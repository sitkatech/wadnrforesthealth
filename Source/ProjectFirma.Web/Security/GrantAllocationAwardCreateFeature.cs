using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation Award", FieldDefinitionEnum.GrantAllocationAward)]
    public class GrantAllocationAwardCreateFeature : FirmaFeature
    {
        public GrantAllocationAwardCreateFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}