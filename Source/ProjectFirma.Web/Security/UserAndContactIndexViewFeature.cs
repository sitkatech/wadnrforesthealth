using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View User and Contact Index page")]
    public class UserAndContactIndexViewFeature : FirmaBaseFeature
    {
        public UserAndContactIndexViewFeature() : base(new List<IRole> (Role.AllBaseRolesExceptUnassigned()))
        {
        }

    }
}