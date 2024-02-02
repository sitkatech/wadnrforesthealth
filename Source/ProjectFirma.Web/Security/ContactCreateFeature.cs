using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Contact")]
    public class ContactCreateFeature : FirmaBaseFeature
    {
        public ContactCreateFeature() : base(new List<IRole> {Role.Admin, Role.EsaAdmin, Role.CanAddEditUsersContactsOrganizations })
        {
        }

    }
}