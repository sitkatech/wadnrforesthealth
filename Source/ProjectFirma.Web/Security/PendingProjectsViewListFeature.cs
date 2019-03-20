using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View Pending {0} List", FieldDefinitionEnum.Project)]
    public class PendingProjectsViewListFeature : FirmaFeature
    {
        public PendingProjectsViewListFeature() : base(Role.All.Except(new List<Role>{Role.Unassigned}))
        {
        }
    }
}