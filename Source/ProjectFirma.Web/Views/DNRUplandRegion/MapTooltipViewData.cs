using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.DNRUplandRegion
{
    public class MapTooltipViewData : FirmaViewData
    {
        public readonly Models.DNRUplandRegion DNRUplandRegion;
        public readonly HtmlString RegionDetailLink;
        public readonly string DNRUplandRegionLabel;

        public MapTooltipViewData(Person currentPerson, Models.DNRUplandRegion dnrUplandRegion) : base(currentPerson)
        {
            DNRUplandRegion = dnrUplandRegion;
            RegionDetailLink = SitkaRoute<DNRUplandRegionController>
                .BuildLinkFromExpression(c => c.Detail(DNRUplandRegion), DNRUplandRegion.DNRUplandRegionName).ToHTMLFormattedString();
            DNRUplandRegionLabel = Models.FieldDefinition.DNRUplandRegion.FieldDefinitionDisplayName;
        }
    }
}
