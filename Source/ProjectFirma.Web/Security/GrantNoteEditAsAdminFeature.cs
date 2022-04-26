using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Note")]
    public class GrantNoteEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GrantNote>
    {
        private readonly FirmaFeatureWithContextImpl<GrantNote> _firmaFeatureWithContextImpl;
        private readonly GrantEditAsAdminFeature grantEditAsAdminFeature;

        public GrantNoteEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GrantNote>(this);
            grantEditAsAdminFeature = new GrantEditAsAdminFeature();
            ActionFilter = _firmaFeatureWithContextImpl;
        }


        public PermissionCheckResult HasPermission(Person person, GrantNote contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                return new PermissionCheckResult(false, "You do not have permission for this action");
            }
            return grantEditAsAdminFeature.HasPermission(person, contextModelObject.Grant);
        }

        public void DemandPermission(Person person, GrantNote contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}