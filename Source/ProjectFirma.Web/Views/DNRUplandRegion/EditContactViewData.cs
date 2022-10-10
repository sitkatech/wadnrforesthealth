
namespace ProjectFirma.Web.Views.DNRUplandRegion
{
    public class EditContactViewData : FirmaUserControlViewData
    {
        public Models.DNRUplandRegion Region { get; }

        public EditContactViewData(Models.DNRUplandRegion region)
        {
            Region = region;
        }

    }
}