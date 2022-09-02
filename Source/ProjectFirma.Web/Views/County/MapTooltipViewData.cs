using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.County
{
    public class MapTooltipViewData : FirmaViewData
    {
        public readonly Models.County County;
        public readonly HtmlString CountyDetailLink;
        public readonly string CountyLabel;

        public MapTooltipViewData(Person currentPerson, Models.County county) : base(currentPerson)
        {
            County = county;
            CountyDetailLink = SitkaRoute<CountyController>
                .BuildLinkFromExpression(c => c.Detail(County), County.CountyName).ToHTMLFormattedString();
            CountyLabel = Models.FieldDefinition.County.FieldDefinitionDisplayName;
        }
    }
}
