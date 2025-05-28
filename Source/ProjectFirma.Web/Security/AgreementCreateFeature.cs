using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create an {0}", FieldDefinitionEnum.Agreement)]
    public class AgreementCreateFeature : FirmaFeature
    {
        public AgreementCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}