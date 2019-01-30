using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View an Agreement")]
    // Admin for now, until someone tells us differently
    //public class GrantsViewFullListFeature : FirmaAdminFeature
    public class AgreementsViewFeature : AnonymousUnclassifiedFeature
    {
    }
}