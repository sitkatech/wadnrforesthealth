using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Normal User and Above for ProjectFirma")]
    public class FirmaNormalUserAndAboveFeature : FirmaFeature
    {
        public FirmaNormalUserAndAboveFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward, Role.Normal }) { }
    }
}