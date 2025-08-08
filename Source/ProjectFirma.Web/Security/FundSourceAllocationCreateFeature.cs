using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a {0}", FieldDefinitionEnum.FundSourceAllocation)]
    public class FundSourceAllocationCreateFeature : FirmaFeature
    {
        public FundSourceAllocationCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {
            
        }
    }
}