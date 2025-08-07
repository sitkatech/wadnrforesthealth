using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant")]
    public class FundSourceDeleteFeature : FirmaFeature
    {
        public FundSourceDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}