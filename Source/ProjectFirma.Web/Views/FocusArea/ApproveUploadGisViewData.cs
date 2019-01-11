using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class ApproveUploadGisViewData : FirmaViewData

    {
        public readonly MapInitJson MapInitJson;
        public readonly string FocusAreaDetailUrl;

        public ApproveUploadGisViewData(Person currentPerson, Models.FocusArea focusArea,
            MapInitJson mapInitJson) : base(currentPerson)
        {
            MapInitJson = mapInitJson;
            FocusAreaDetailUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Detail(focusArea));
        }
    }
}
