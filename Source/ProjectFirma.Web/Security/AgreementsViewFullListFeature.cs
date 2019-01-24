using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("View Full Agreement List")]
    // Admin for now, until someone tells us differently
    //public class GrantsViewFullListFeature : FirmaAdminFeature
    public class AgreementsViewFullListFeature : AnonymousUnclassifiedFeature
    {
    }
}