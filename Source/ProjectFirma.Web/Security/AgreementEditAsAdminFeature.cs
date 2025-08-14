using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Agreement")]
    public class AgreementEditAsAdminFeature : FirmaFeature
    {
        public AgreementEditAsAdminFeature() : base(new List<Role> {Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements }) { }
    }
}