using ProjectFirma.Web.Models;
using System.Collections.Generic;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Allows editing roles etc for users.")]
    public class UserEditAsAdminFeature : FirmaFeature
    {
        public UserEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanAddEditUsersContactsOrganizations })
        {
        }
    }
}