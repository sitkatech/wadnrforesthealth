using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Invoice Line Item")]
    public class InvoiceLineItemDeleteFeature : FirmaFeature
    {
        public InvoiceLineItemDeleteFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin }) { }
    }
}