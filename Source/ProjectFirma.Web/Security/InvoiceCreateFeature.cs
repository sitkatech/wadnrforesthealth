using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create an Invoice", FieldDefinitionEnum.Application)]
    public class InvoiceCreateFeature : FirmaFeature
    {
        public InvoiceCreateFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
        }
    }
}