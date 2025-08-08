using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Fund Source")]
    public class FundSourceEditAsAdminFeature : FirmaFeature
    {
        public FundSourceEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {
        }
    }
}