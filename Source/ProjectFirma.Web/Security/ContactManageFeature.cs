using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Manage Contact")]
    public class ContactManageFeature : FirmaBaseFeature
    {
        public ContactManageFeature() : base(new List<IRole>{Role.Normal, Role.Admin, Role.ProjectSteward, Role.EsaAdmin, Role.CanAddEditUsersContactsOrganizations})
        {
        }
        
        public bool HasPermission(Person person)
        {
            if (person.HasRole(Role.Normal) && !person.HasRole(Role.CanAddEditUsersContactsOrganizations))
            {
                if (person.OrganizationID != OrganizationModelExtensions.WadnrID)
                {
                    return false;
                }
            }

            return base.HasPermissionByPerson(person);
        }
    }
}