using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a {0}", FieldDefinitionEnum.Grant)]
    public class GrantCreateFeature : FirmaFeature
    {
        public GrantCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}