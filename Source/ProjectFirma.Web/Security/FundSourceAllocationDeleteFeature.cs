using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Fund Source Allocation")]
    public class FundSourceAllocationDeleteFeature : FirmaFeature
    {
        public FundSourceAllocationDeleteFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {

        }
    }
}