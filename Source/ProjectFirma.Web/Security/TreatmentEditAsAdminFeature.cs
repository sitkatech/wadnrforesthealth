using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit FundSource Allocation Award Landowner Cost Share Line Item")]
    public class TreatmentEditAsAdminFeature : FirmaFeature
    {
        public TreatmentEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {
        }
    }
}