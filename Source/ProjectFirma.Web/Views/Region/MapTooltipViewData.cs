using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Region
{
    public class MapTooltipViewData : FirmaViewData
    {
        public readonly Models.DNRUplandRegion DnrUplandRegion;
        public readonly HtmlString RegionDetailLink;

        public MapTooltipViewData(Person currentPerson, Models.DNRUplandRegion dnrUplandRegion) : base(currentPerson)
        {
            DnrUplandRegion = dnrUplandRegion;
            RegionDetailLink = SitkaRoute<RegionController>
                .BuildLinkFromExpression(c => c.Detail(DnrUplandRegion), DnrUplandRegion.DNRUplandRegionName).ToHTMLFormattedString();
        }
    }
}
