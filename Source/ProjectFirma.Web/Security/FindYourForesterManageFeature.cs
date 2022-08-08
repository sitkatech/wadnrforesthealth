using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Manage Find Your Forester")]
    public class FindYourForesterManageFeature : FirmaBaseFeature
    {
        public FindYourForesterManageFeature() : base(new List<IRole>{ Role.ProjectSteward, Role.Admin, Role.SitkaAdmin })
        {
        }
        
        // 7/21/2022 TK - This could be a useful check to limit access, but not sure if needed yet.
        //public bool HasPermission(Person person)
        //{
        //    if (person.HasRole(Role.Normal))
        //    {
        //        if (person.OrganizationID != OrganizationModelExtensions.WadnrID)
        //        {
        //            return false;
        //        }
        //    }

        //    return base.HasPermissionByPerson(person);
        //}
    }
}