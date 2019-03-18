using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create an Interaction/Event", FieldDefinitionEnum.Application)]
    public class InteractionEventCreateFeature : FirmaFeature
    {
        public InteractionEventCreateFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
        }
    }
}