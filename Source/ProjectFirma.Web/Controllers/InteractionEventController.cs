using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.InteractionEvent;
using ProjectFirma.Web.Views.Shared;

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
            viewModel.UpdateModel(interactionEvent, CurrentPerson, new List<InteractionEventProject>(), new List<InteractionEventContact>());
            HttpRequestStorage.DatabaseEntities.InteractionEvents.Add(interactionEvent);
            SetMessageForDisplay($"{FieldDefinition.InteractionEvent.FieldDefinitionDisplayName} \"{interactionEvent.InteractionEventTitle}\" successfully created.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult InteractionEventViewEdit(EditInteractionEventViewModel viewModel, EditInteractionEventEditType editInteractionEventEditType)
        {
            var interactionEventTypes = HttpRequestStorage.DatabaseEntities.InteractionEventTypes.ToList();
            var allPeople = HttpRequestStorage.DatabaseEntities.People.OrderBy(x => x.LastName).ToList();
            var allProjects = HttpRequestStorage.DatabaseEntities.Projects;

            var viewData = new EditInteractionEventViewData(CurrentPerson, editInteractionEventEditType, interactionEventTypes, allPeople, viewModel.InteractionEventID, allProjects);
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
            HttpRequestStorage.DatabaseEntities.InteractionEventProjects.Load();
            var interactionEventProjects = HttpRequestStorage.DatabaseEntities.InteractionEventProjects.Local;

            HttpRequestStorage.DatabaseEntities.InteractionEventContacts.Load();
            var interactionEventContacts = HttpRequestStorage.DatabaseEntities.InteractionEventContacts.Local;

            viewModel.UpdateModel(interactionEvent, CurrentPerson, interactionEventProjects, interactionEventContacts);

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult DeleteInteractionEvent(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(interactionEventPrimaryKey.PrimaryKeyValue);
            return ViewDeleteInteractionEvent(interactionEventPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteInteractionEvent(InteractionEvent interactionEvent, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()} '{interactionEvent.InteractionEventTitle}'?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteInteractionEvent(InteractionEventPrimaryKey interactionEventPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteInteractionEvent(interactionEvent, viewModel);
            }

            var message = $"{FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()} \"{interactionEvent.InteractionEventTitle}\" successfully deleted.";

            interactionEvent.DeleteFull(HttpRequestStorage.DatabaseEntities);

            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [InteractionEventViewFeature]
        public ViewResult InteractionEventDetail(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;

            var mapLocationFormID = GetMapLocationFormID(interactionEventPrimaryKey);

            //the following variables are plural because the MapInitJson constructor is expecting List<>s but InteractionEvents only have a single location point associated with them.
            var layers = new List<LayerGeoJson>();
            var locationFeatures = new List<Feature>();
            BoundingBox boundingBox = null;

            if (interactionEvent.InteractionEventLocationSimple != null)
            {
                locationFeatures.Add(DbGeometryToGeoJsonHelper.FromDbGeometry(interactionEvent.InteractionEventLocationSimple));
                boundingBox = new BoundingBox(new Point(interactionEvent.InteractionEventLocationSimple), 0.5m);
            }

            if (locationFeatures.Any())
            {
                layers.Add(new LayerGeoJson($"{FieldDefinition.InteractionEvent.FieldDefinitionDisplayName} Location",
                    new FeatureCollection(locationFeatures), "yellow", 1,
                    LayerInitialVisibility.Show));
            }

            
            var interactionEventLocationMapInitJson = new MapInitJson($"interactionEvent_{interactionEvent.InteractionEventID}_mapID", 10, layers, boundingBox);
            var viewData = new InteractionEventDetailViewData(CurrentPerson, interactionEvent, mapLocationFormID, interactionEventLocationMapInitJson);

            return RazorView<InteractionEventDetail, InteractionEventDetailViewData>(viewData);
        }

        private string GetMapLocationFormID(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            return $"editMapForInteractionEventLocation{interactionEventPrimaryKey}";
        }

        [InteractionEventViewFeature]
        public GridJsonNetJObjectResult<InteractionEvent> InteractionEventGridJsonData()
        {
            var gridSpec = new InteractionEventGridSpec(CurrentPerson, true, true);
            var interactionEvents = HttpRequestStorage.DatabaseEntities.InteractionEvents.OrderByDescending(x => x.InteractionEventDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InteractionEvent>(interactionEvents, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventLocation(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            var viewModel = new EditInteractionEventLocationSimpleViewModel(interactionEvent.InteractionEventLocationSimple);
            return ViewEditInteractionEventLocationSimple(viewModel, interactionEvent);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEventLocation(InteractionEventPrimaryKey interactionEventPrimaryKey, EditInteractionEventLocationSimpleViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInteractionEventLocationSimple(viewModel, interactionEvent);
            }

            viewModel.UpdateModel(interactionEvent);

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInteractionEventLocationSimple(EditInteractionEventLocationSimpleViewModel viewModel, InteractionEvent interactionEvent)
        {

            var layerGeoJsons = MapInitJson.GetAllGeospatialAreaMapLayers(LayerInitialVisibility.Hide);
            var mapInitJson = new MapInitJson($"interactionEvent_{interactionEvent.InteractionEventID}_EditMap", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox(), false) { AllowFullScreen = false, DisablePopups = true };

            var mapPostUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(c => c.EditInteractionEventLocation(interactionEvent.PrimaryKey, null));
            var mapFormID = GetMapLocationFormID(interactionEvent.PrimaryKey);
            var wmsLayerNames = FirmaWebConfiguration.GetWmsLayerNames();
            var mapServiceUrl = FirmaWebConfiguration.WebMapServiceUrl;
           
            var viewData = new EditInteractionEventLocationSimpleViewData(CurrentPerson, mapInitJson, wmsLayerNames, null, mapPostUrl,mapFormID, mapServiceUrl);
            return RazorPartialView<EditInteractionEventLocationSimple, EditInteractionEventLocationSimpleViewData, EditInteractionEventLocationSimpleViewModel>(viewData, viewModel);
        }
    }
}