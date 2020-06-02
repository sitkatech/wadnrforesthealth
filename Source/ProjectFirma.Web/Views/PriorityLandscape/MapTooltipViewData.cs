using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.PriorityLandscape
{
    public class MapTooltipViewData : FirmaViewData
    {
        public readonly Models.PriorityLandscape PriorityLandscape;
        public readonly HtmlString PriorityLandscapeDetailLink;

        public MapTooltipViewData(Person currentPerson, Models.PriorityLandscape priorityLandscape) : base(currentPerson)
        {
            PriorityLandscape = priorityLandscape;
            PriorityLandscapeDetailLink = SitkaRoute<PriorityLandscapeController>
                .BuildLinkFromExpression(c => c.Detail(PriorityLandscape), PriorityLandscape.PriorityLandscapeName).ToHTMLFormattedString();
        }
    }
}
