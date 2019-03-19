using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create, Edit, or Delete an Interaction/Event", FieldDefinitionEnum.InteractionEvent)]
    public class InteractionEventManageFeature : FirmaFeature
    {
        public InteractionEventManageFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
        }
    }
}