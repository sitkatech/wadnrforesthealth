using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Note Internal")]
    public class GrantNoteInternalEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantNoteInternal>
    {
        private readonly FirmaFeatureWithContextImpl<GrantNoteInternal> _firmaFeatureWithContextImpl;
        private readonly GrantEditAsAdminFeature _grantEditAsAdminFeature;

        public GrantNoteInternalEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantNoteInternal>(this);
            _grantEditAsAdminFeature = new GrantEditAsAdminFeature();
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, GrantNoteInternal contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return new PermissionCheckResult(false, "You do not have permission for this action");
            }
            return _grantEditAsAdminFeature.HasPermission(person, contextModelObject.Grant);
        }

        public void DemandPermission(Person person, GrantNoteInternal contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}