using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create an {0}", FieldDefinitionEnum.Invoice)]
    public class InvoiceCreateFeature : FirmaFeature
    {
        public InvoiceCreateFeature()
            : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward })
        {
        }
    }
}