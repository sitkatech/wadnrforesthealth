using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a {0}", FieldDefinitionEnum.FundSource)]
    public class FundSourceCreateFeature : FirmaFeature
    {
        public FundSourceCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageFundSourcesAndAgreements })
        {
        }
    }
}