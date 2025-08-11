using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit or Delete FundSource File")]
    public class FundSourceManageFileResourceAsAdminFeature : FirmaFeature
    {
        public FundSourceManageFileResourceAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {
        }
    }
}