using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit or Delete Grant Allocation File")]
    public class GrantAllocationManageFileResourceAsAdminFeature : FirmaFeature
    {
        public GrantAllocationManageFileResourceAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {

        }
    }
}