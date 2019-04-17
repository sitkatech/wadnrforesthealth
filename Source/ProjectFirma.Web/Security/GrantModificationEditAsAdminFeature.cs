using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Modification")]
    public class GrantModificationEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantModification>
    {
        private readonly FirmaFeatureWithContextImpl<GrantModification> _firmaFeatureWithContextImpl;

        public GrantModificationEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin})
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantModification>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantModification contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantModification contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to Grant Modification {contextModelObject.GrantModificationName}");
        }
    }
}