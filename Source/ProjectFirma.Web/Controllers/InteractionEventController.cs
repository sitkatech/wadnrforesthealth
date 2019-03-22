using System;
using System.Data.Entity;
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

            var viewData = new InteractionEventDetailViewData(CurrentPerson, interactionEvent);

            return RazorView<InteractionEventDetail, InteractionEventDetailViewData>(viewData);
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
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            var interactionEventContacts = HttpRequestStorage.DatabaseEntities.InteractionEventContacts.Where(x => x.InteractionEventID == interactionEventPrimaryKey.PrimaryKeyValue);
            var viewModel = new EditInteractionEventContactsViewModel(interactionEventContacts);
            return ViewEditInteractionEventContacts(viewModel, interactionEvent);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEventContacts(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventContactsViewModel viewModel)
        {

            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInteractionEventContacts(viewModel, interactionEvent);
            }
            HttpRequestStorage.DatabaseEntities.InteractionEventContacts.Load();
            var interactionEventContacts = HttpRequestStorage.DatabaseEntities.InteractionEventContacts.Local;

            viewModel.UpdateModel(interactionEvent, interactionEventContacts);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInteractionEventContacts(EditInteractionEventContactsViewModel viewModel, InteractionEvent interactionEvent)
        {
            var allPeople = HttpRequestStorage.DatabaseEntities.People;


            var viewData = new EditInteractionEventContactsViewData(CurrentPerson, interactionEvent.PrimaryKey, allPeople);
            return RazorPartialView<EditInteractionEventContacts, EditInteractionEventContactsViewData, EditInteractionEventContactsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventProjects(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            var interactionEventProjects = HttpRequestStorage.DatabaseEntities.InteractionEventProjects.Where(x => x.InteractionEventID == interactionEventPrimaryKey.PrimaryKeyValue);
            var viewModel = new EditInteractionEventProjectsViewModel(interactionEventProjects);
            return ViewEditInteractionEventProjects(viewModel, interactionEvent);
        }

        private PartialViewResult ViewEditInteractionEventProjects(EditInteractionEventProjectsViewModel viewModel, InteractionEvent interactionEvent)
        {
            var allProjects = HttpRequestStorage.DatabaseEntities.Projects;


            var viewData = new EditInteractionEventProjectsViewData(CurrentPerson, interactionEvent.PrimaryKey, allProjects);
            return RazorPartialView<EditInteractionEventProjects, EditInteractionEventProjectsViewData, EditInteractionEventProjectsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEventProjects(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventProjectsViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInteractionEventProjects(viewModel, interactionEvent);
            }
            HttpRequestStorage.DatabaseEntities.InteractionEventProjects.Load();
            var interactionEventProjects = HttpRequestStorage.DatabaseEntities.InteractionEventProjects.Local;

            viewModel.UpdateModel(interactionEvent, interactionEventProjects);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventLocation(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            //var interactionEventProje = HttpRequestStorage.DatabaseEntities.InteractionEventContacts.Where(x => x.InteractionEventID == interactionEventPrimaryKey.PrimaryKeyValue);
            var viewModel = new EditInteractionEventLocationSimpleViewModel(interactionEvent.InteractionEventLocationSimple);
            return ViewEditInteractionEventLocationSimple(viewModel, interactionEvent);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventLocation(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventLocationSimpleViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        private PartialViewResult ViewEditInteractionEventLocationSimple(EditInteractionEventLocationSimpleViewModel viewModel, InteractionEvent interactionEvent)
        {

            var layerGeoJsons = MapInitJson.GetAllGeospatialAreaMapLayers(LayerInitialVisibility.Hide);
            var mapInitJson = new MapInitJson($"interactionEvent_{interactionEvent.InteractionEventID}_EditMap", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox(), false) { AllowFullScreen = false, DisablePopups = true };

            var mapPostUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(c => c.EditInteractionEventLocation(interactionEvent.PrimaryKey, null));
            var mapFormID = $"editMapForInteractionEventLocation{interactionEvent.InteractionEventID}";
            var wmsLayerNames = FirmaWebConfiguration.GetWmsLayerNames();
            var mapServiceUrl = FirmaWebConfiguration.WebMapServiceUrl;
           
            var viewData = new EditInteractionEventLocationSimpleViewData(CurrentPerson, mapInitJson, wmsLayerNames, null, mapPostUrl,mapFormID, mapServiceUrl);
            return RazorPartialView<EditInteractionEventLocationSimple, EditInteractionEventLocationSimpleViewData, EditInteractionEventLocationSimpleViewModel>(viewData, viewModel);
        }
    }
}