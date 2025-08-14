using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Agreement")]
    public class AgreementDeleteFeature : FirmaFeature
    {
        public AgreementDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements }) { }
    }
}