using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class InteractionEventController : FirmaBaseController
    {
        [InteractionEventViewFeature]
        public ViewResult Index()
        {
            //var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            //return RazorView<Index, IndexViewData>(viewData);
            throw new NotImplementedException();
        }

        //[InteractionEventViewFeature]
        //public GridJsonNetJObjectResult<InteractionEvent> IndexGridJsonData()
        //{
        //    var gridSpec = new IndexGridSpec(CurrentPerson);
        //    var interactionEvents = HttpRequestStorage.DatabaseEntities.InteractionEvents.OrderBy(x => x.InteractionEventName).ToList();
        //    var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InteractionEvent>(interactionEvents, gridSpec);
        //    return gridJsonNetJObjectResult;
        //}

    }
}