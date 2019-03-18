using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.InteractionEvent;

namespace ProjectFirma.Web.Controllers
{
    public class InteractionEventController : FirmaBaseController
    {
        [InteractionEventViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            //return RazorView<Index, IndexViewData>(viewData);
            throw new NotImplementedException();
        }


        [HttpGet]
        [InteractionEventCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditInteractionEventViewModel();
            return InteractionEventViewEdit(viewModel, EditInteractionEventType.NewInteractionEvent);
        }

        [HttpPost]
        [InteractionEventCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditInteractionEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return InteractionEventViewEdit(viewModel, EditInteractionEventType.NewInteractionEvent);
            }
            var interactionEvent = HttpRequestStorage.DatabaseEntities.InteractionEvents.Single(g => g.InteractionEventID == viewModel.InteractionEventID);
            viewModel.UpdateModel(interactionEvent, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult InteractionEventViewEdit(EditInteractionEventViewModel viewModel, EditInteractionEventType editInteractionEventType)
        {
            var interactionEventTypes = HttpRequestStorage.DatabaseEntities.InteractionEventTypes;
            

            var viewData = new EditInteractionEventViewData(editInteractionEventType,
                interactionEventTypes
            );
            return RazorPartialView<EditInteractionEvent, EditInteractionEventViewData, EditInteractionEventViewModel>(viewData, viewModel);
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