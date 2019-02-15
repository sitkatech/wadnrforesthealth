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
            return grantEditAsAdminFeature.HasPermission(person, contextModelObject.Grant);
        }

        public void DemandPermission(Person person, GrantNote contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }


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
            bool userHasPermision = HasPermissionByPerson(person);
            if (userHasPermision)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to Grant {contextModelObject.GrantName}");
        }
    }
}