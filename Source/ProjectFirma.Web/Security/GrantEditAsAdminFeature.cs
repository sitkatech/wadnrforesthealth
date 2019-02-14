using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant")]
    public class GrantEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Grant>
    {
        private readonly FirmaFeatureWithContextImpl<Grant> _firmaFeatureWithContextImpl;

        public GrantEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin})
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
            return new PermissionCheckResult();
        }
    }
}