using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Agreement")]
    public class AgreementEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Agreement>
    {
        private readonly FirmaFeatureWithContextImpl<Agreement> _firmaFeatureWithContextImpl;

        public AgreementEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Agreement>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Agreement contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Agreement contextModelObject)
        {
            return new PermissionCheckResult();
        }
    }
}