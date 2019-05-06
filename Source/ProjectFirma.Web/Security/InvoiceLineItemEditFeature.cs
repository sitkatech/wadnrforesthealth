using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Invoice Line Item")]
    public class InvoiceLineItemEditFeature : FirmaFeature
    {
        public InvoiceLineItemEditFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward }) { }
    }
}