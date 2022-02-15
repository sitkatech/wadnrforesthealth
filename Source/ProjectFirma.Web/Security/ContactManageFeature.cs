using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Manage Contact")]
    public class ContactManageFeature : FirmaBaseFeature
    {
        public ContactManageFeature() : base(new List<IRole>{Role.Normal, Role.Admin, Role.ProjectSteward, Role.SitkaAdmin})
        {
        }

        public override bool HasPermissionByPerson(Person person)
        {
            if (person.HasRole(Role.Normal))
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