using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.PriorityArea
{
    public class MapTooltipViewData : FirmaViewData
    {
        public readonly Models.PriorityArea PriorityArea;
        public readonly HtmlString PriorityAreaDetailLink;

        public MapTooltipViewData(Person currentPerson, Models.PriorityArea priorityArea) : base(currentPerson)
        {
            PriorityArea = priorityArea;
            PriorityAreaDetailLink = SitkaRoute<PriorityAreaController>
                .BuildLinkFromExpression(c => c.Detail(PriorityArea), PriorityArea.PriorityAreaName).ToHTMLFormattedString();
        }
    }
}
