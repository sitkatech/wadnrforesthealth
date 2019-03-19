using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.InteractionEvent;

namespace ProjectFirma.Web.Controllers
{
    public class InteractionEventController : FirmaBaseController
    {
        [InteractionEventViewFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.InteractionEventList);
            var viewData = new InteractionEventIndexViewData(CurrentPerson, firmaPage);
            return RazorView<InteractionEventIndex, InteractionEventIndexViewData>(viewData);
        }


        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditInteractionEventViewModel();
            return InteractionEventViewEdit(viewModel, EditInteractionEventEditType.NewInteractionEventEdit);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditInteractionEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return InteractionEventViewEdit(viewModel, EditInteractionEventEditType.NewInteractionEventEdit);
            }

            var interactionEventType =
                HttpRequestStorage.DatabaseEntities.InteractionEventTypes.Single(x =>
                    x.InteractionEventTypeID == viewModel.InteractionEventTypeID);
            var dnrStaffPerson =
                HttpRequestStorage.DatabaseEntities.People.Single(p => p.PersonID == viewModel.DNRStaffPersonID);
            var interactionEvent = InteractionEvent.CreateNewBlank(interactionEventType, dnrStaffPerson);
            viewModel.UpdateModel(interactionEvent, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult InteractionEventViewEdit(EditInteractionEventViewModel viewModel, EditInteractionEventEditType editInteractionEventEditType)
        {
            var interactionEventTypes = HttpRequestStorage.DatabaseEntities.InteractionEventTypes.ToList();
            var dnrStaffPeople = HttpRequestStorage.DatabaseEntities.People.OrderBy(p => p.LastName).Where(p =>
                p.Organization.OrganizationName == "Washington State Department of Natural Resources").ToList();
            

            var viewData = new EditInteractionEventViewData(editInteractionEventEditType,
                interactionEventTypes,
                dnrStaffPeople
            );
            return RazorPartialView<EditInteractionEvent, EditInteractionEventViewData, EditInteractionEventViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEvent(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var viewModel = new EditInteractionEventViewModel(interactionEventPrimaryKey.EntityObject);
            return InteractionEventViewEdit(viewModel, EditInteractionEventEditType.ExistingInteractionEventEdit);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEvent(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return InteractionEventViewEdit(viewModel, EditInteractionEventEditType.ExistingInteractionEventEdit);
            }
            viewModel.UpdateModel(interactionEvent, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult DeleteInteractionEvent(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteInteractionEvent(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [InteractionEventViewFeature]
        public PartialViewResult InteractionEventDetail(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            throw new NotImplementedException();
        }


        [InteractionEventViewFeature]
        public GridJsonNetJObjectResult<InteractionEvent> InteractionEventGridJsonData()
        {
            var gridSpec = new InteractionEventGridSpec(CurrentPerson);
            var interactionEvents = HttpRequestStorage.DatabaseEntities.InteractionEvents.OrderByDescending(x => x.InteractionEventDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InteractionEvent>(interactionEvents, gridSpec);
            return gridJsonNetJObjectResult;
        }

    }
}