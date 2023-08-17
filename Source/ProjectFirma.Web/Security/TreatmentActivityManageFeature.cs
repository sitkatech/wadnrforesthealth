using System.Collections.Generic;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Manage Treatment Activity")]
    public class TreatmentActivityManageFeature : FirmaFeature
    {
        public TreatmentActivityManageFeature()
            : base(new List<Role> {Role.EsaAdmin, Role.Admin, Role.ProjectSteward})
        {
        }
    }
}