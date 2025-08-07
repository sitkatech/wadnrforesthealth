using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View Fund Source Note Internal")]
    public class FundSourceNoteInternalViewFeature : FirmaFeature
    {
        public FundSourceNoteInternalViewFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}