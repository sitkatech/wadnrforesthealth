using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Region
{
    public class MapTooltipViewData : FirmaViewData
    {
        public readonly Models.Region Region;
        public readonly HtmlString RegionDetailLink;

        public MapTooltipViewData(Person currentPerson, Models.Region geospatialArea) : base(currentPerson)
        {
            Region = geospatialArea;
            RegionDetailLink = SitkaRoute<RegionController>
                .BuildLinkFromExpression(c => c.Detail(Region), Region.RegionName).ToHTMLFormattedString();
        }
    }
}
