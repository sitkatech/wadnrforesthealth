using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Invoice Line Item")]
    public class InvoiceLineItemEditAsAdminFeature : FirmaFeature
    {
        public InvoiceLineItemEditAsAdminFeature() : base(new List<Role> { Role.SitkaAdmin, Role.Admin }) { }
    }
}