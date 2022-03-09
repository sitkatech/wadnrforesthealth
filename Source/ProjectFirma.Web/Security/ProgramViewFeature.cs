using System.Collections.Generic;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View Program")]
    public class ProgramViewFeature : FirmaFeature
    {
        public ProgramViewFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward, Role.ProgramEditor })
        {
        }
    }
}