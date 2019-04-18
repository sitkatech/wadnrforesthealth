using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Modification Note Internal")]
    public class GrantModificationNoteInternalEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantModificationNoteInternal>
    {
        private readonly FirmaFeatureWithContextImpl<GrantModificationNoteInternal> _firmaFeatureWithContextImpl;

        public GrantModificationNoteInternalEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin})
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantModificationNoteInternal>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GrantModificationNoteInternal contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GrantModificationNoteInternal contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to Internal Notes on Grant Modification {contextModelObject.GrantModification.GrantModificationName}");
        }
    }
}