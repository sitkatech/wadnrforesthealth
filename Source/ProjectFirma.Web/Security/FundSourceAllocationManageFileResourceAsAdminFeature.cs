using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit or Delete Fund Source Allocation File")]
    public class FundSourceAllocationManageFileResourceAsAdminFeature : FirmaFeature
    {
        public FundSourceAllocationManageFileResourceAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {

        }
    }
}