using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Allocation Award")]
    public class GrantAllocationAwardEditAsAdminFeature : FirmaFeature
    {

        public GrantAllocationAwardEditAsAdminFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
            
        }

    }
}