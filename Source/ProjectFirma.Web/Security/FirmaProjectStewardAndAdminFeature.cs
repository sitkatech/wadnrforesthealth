using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("_ProjectStewardAndAdmin for ProjectFirma")]
    public class FirmaProjectStewardAndAdminFeature : FirmaFeature
    {
        public FirmaProjectStewardAndAdminFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward }) { }
    }
}