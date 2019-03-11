using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View Grant Allocation Note Internal")]
    public class GrantAllocationNoteInternalViewFeature : FirmaFeature
    {
        public GrantAllocationNoteInternalViewFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
        }
    }
}