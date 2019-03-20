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

            var interactionEvent = new InteractionEvent(viewModel.InteractionEventTypeID, viewModel.DNRStaffPersonID,
                viewModel.Title, viewModel.Date);
            viewModel.UpdateModel(interactionEvent, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.InteractionEvents.Add(interactionEvent);
            SetMessageForDisplay($"{FieldDefinition.InteractionEvent.FieldDefinitionDisplayName} \"{interactionEvent.InteractionEventTitle}\" successfully created.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult InteractionEventViewEdit(EditInteractionEventViewModel viewModel, EditInteractionEventEditType editInteractionEventEditType)
        {
            var interactionEventTypes = HttpRequestStorage.DatabaseEntities.InteractionEventTypes.ToList();
            var dnrStaffPeople = HttpRequestStorage.DatabaseEntities.People.Where(p =>
                p.Organization.OrganizationName == Organization.OrganizationWADNR).OrderBy(p => p.LastName).ToList();
            

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
        public ViewResult InteractionEventDetail(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            var allPeople = HttpRequestStorage.DatabaseEntities.People;

            var viewData = new InteractionEventDetailViewData(CurrentPerson, interactionEvent, allPeople);
            var viewModel = new InteractionEventDetailViewModel(interactionEvent.InteractionEventContacts.ToList());
            return RazorView<InteractionEventDetail, InteractionEventDetailViewData, InteractionEventDetailViewModel>(viewData, viewModel);
        }



        [InteractionEventViewFeature]
        public GridJsonNetJObjectResult<InteractionEvent> InteractionEventGridJsonData()
        {
            var gridSpec = new InteractionEventGridSpec(CurrentPerson);
            var interactionEvents = HttpRequestStorage.DatabaseEntities.InteractionEvents.OrderByDescending(x => x.InteractionEventDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InteractionEvent>(interactionEvents, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventContacts(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEventContacts(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventContactsViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            //if (!ModelState.IsValid)
            //{
            //    return InteractionEventContactsViewEdit(viewModel, EditInteractionEventContactsEditType.ExistingInteractionEventContactsEdit);
            //}
            //viewModel.UpdateModel(interactionEvent, CurrentPerson);
            //return new ModalDialogFormJsonResult();
            throw new NotImplementedException();
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventProjects(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEventProjects(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventProjectsViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            //if (!ModelState.IsValid)
            //{
            //    return InteractionEventProjectsViewEdit(viewModel, EditInteractionEventEditType.ExistingInteractionEventContactsEdit);
            //}
            //viewModel.UpdateModel(interactionEvent, CurrentPerson);
            //return new ModalDialogFormJsonResult();
            throw new NotImplementedException();
        }

    }
}