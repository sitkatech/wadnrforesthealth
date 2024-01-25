using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Contact")]
    public class ContactCreateAndViewFeature : FirmaBaseFeature
    {
        public ContactCreateAndViewFeature() : base(new List<IRole> {Role.Admin, Role.EsaAdmin, Role.CanAddEditUsersContactsOrganizations })
        {
        }

    }
}