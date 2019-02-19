using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant")]
    public class GrantDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Grant>
    {
        private readonly FirmaFeatureWithContextImpl<Grant> _firmaFeatureWithContextImpl;

        public GrantDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Grant>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Grant contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Grant contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {contextModelObject.GrantTitle}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}