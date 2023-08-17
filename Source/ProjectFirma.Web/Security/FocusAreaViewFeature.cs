using System.Collections.Generic;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View DNR LOA Focus Area")]
    public class FocusAreaViewFeature : FirmaFeature
    {
        public FocusAreaViewFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward, Role.Normal }) { }
    }
}