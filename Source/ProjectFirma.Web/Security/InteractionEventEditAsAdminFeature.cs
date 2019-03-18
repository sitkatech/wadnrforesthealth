using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Interaction/Event")]
    public class InteractionEventEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<InteractionEvent>
    {
        private readonly FirmaFeatureWithContextImpl<InteractionEvent> _firmaFeatureWithContextImpl;

        public InteractionEventEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<InteractionEvent>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, InteractionEvent contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, InteractionEvent contextModelObject)
        {
            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}