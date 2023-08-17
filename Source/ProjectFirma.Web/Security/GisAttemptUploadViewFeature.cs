using System.Collections.Generic;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View GIS Feature")]
    public class GisAttemptUploadViewFeature : FirmaFeature
    {
        public GisAttemptUploadViewFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward }) { }
    }
}