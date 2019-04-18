using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Grant Modification")]
    public class GrantModificationDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantModification>
    {
        private readonly FirmaFeatureWithContextImpl<GrantModification> _firmaFeatureWithContextImpl;

        public GrantModificationDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
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
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to delete {contextModelObject.GrantModificationName}");
            }
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}