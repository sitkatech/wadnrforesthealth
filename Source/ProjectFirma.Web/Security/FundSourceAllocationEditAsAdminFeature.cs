using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Fund Source Allocation")]
    public class FundSourceAllocationEditAsAdminFeature : FirmaFeature
    {
        public FundSourceAllocationEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {

        }
    }
}