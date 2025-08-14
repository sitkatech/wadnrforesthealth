using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.InteractionEvent;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

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

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult NewForAProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var viewModel = new EditInteractionEventViewModel(projectPrimaryKey.EntityObject);
            return InteractionEventViewEdit(viewModel, EditInteractionEventEditType.NewInteractionEventEdit);
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult NewForAPerson(PersonPrimaryKey personPrimaryKey)
        {
            var viewModel = new EditInteractionEventViewModel(personPrimaryKey.EntityObject);
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

            var interactionEvent = new InteractionEvent(viewModel.InteractionEventTypeID, viewModel.Title, viewModel.Date);
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

            interactionEvent.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);

            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        #region FileResources

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult NewInteractionEventFiles(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            Check.EnsureNotNull(interactionEventPrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewInteractionEventFiles(viewModel);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewInteractionEventFiles(InteractionEventPrimaryKey interactionEventPrimaryKey, NewFileViewModel viewModel)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewInteractionEventFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(interactionEvent, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()} \"{interactionEvent.InteractionEventTitle}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewInteractionEventFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData(FieldDefinition.InteractionEvent.GetFieldDefinitionLabel());
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult EditInteractionEventFile(InteractionEventFileResourcePrimaryKey interactionEventFileResourcePrimaryKey)
        {
            var fileResource = interactionEventFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditInteractionFile(viewModel);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInteractionEventFile(InteractionEventFileResourcePrimaryKey interactionEventFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = interactionEventFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInteractionFile(viewModel);
            }

            viewModel.UpdateModel(fileResource);
            SetMessageForDisplay($"Successfully updated file \"{fileResource.DisplayName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInteractionFile(EditFileResourceViewModel viewModel)
        {
            var viewData = new EditFileResourceViewData();
            return RazorPartialView<EditFileResource, EditFileResourceViewData, EditFileResourceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [InteractionEventManageFeature]
        public PartialViewResult DeleteInteractionEventFile(InteractionEventFileResourcePrimaryKey interactionEventFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(interactionEventFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteInteractionEventFile(interactionEventFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [InteractionEventManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteInteractionEventFile(InteractionEventFileResourcePrimaryKey interactionEventFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var interactionEventFileResource = interactionEventFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteInteractionEventFile(interactionEventFileResource, viewModel);
            }

            var message = $"{FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()} file \"{interactionEventFileResource.DisplayName}\" created on '{interactionEventFileResource.FileResource.CreateDate}' by '{interactionEventFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            interactionEventFileResource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteInteractionEventFile(InteractionEventFileResource interactionEventFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{interactionEventFileResource.DisplayName}\" file created on '{interactionEventFileResource.FileResource.CreateDate}' by '{interactionEventFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion

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

            var interactionEventLocationMapInitJson = new MapInitJson($"interactionEvent_{interactionEvent.InteractionEventID}_mapID", 10, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), boundingBox);
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
            var gridSpec = new InteractionEventGridSpec(CurrentPerson);
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
            var mapInitJson = new MapInitJson($"interactionEvent_{interactionEvent.InteractionEventID}_EditMap", 10, layerGeoJsons, MapInitJson.GetExternalMapLayersForOtherMaps(), BoundingBox.MakeNewDefaultBoundingBox(), false) { AllowFullScreen = false, DisablePopups = true };

            var mapPostUrl = SitkaRoute<InteractionEventController>.BuildUrlFromExpression(c => c.EditInteractionEventLocation(interactionEvent.PrimaryKey, null));
            var mapFormID = GetMapLocationFormID(interactionEvent.PrimaryKey);
            var wmsLayerNames = FirmaWebConfiguration.GetWmsLayerNames();
            var mapServiceUrl = FirmaWebConfiguration.WebMapServiceUrl;
           
            var viewData = new EditInteractionEventLocationSimpleViewData(CurrentPerson, mapInitJson, wmsLayerNames, null, mapPostUrl,mapFormID, mapServiceUrl);
            return RazorPartialView<EditInteractionEventLocationSimple, EditInteractionEventLocationSimpleViewData, EditInteractionEventLocationSimpleViewModel>(viewData, viewModel);
        }

        [CrossAreaRoute]
        [InteractionEventViewFeature]
        public PartialViewResult InteractionEventMapPopup(InteractionEventPrimaryKey interactionEventPrimaryKey)
        {
            var interactionEvent = interactionEventPrimaryKey.EntityObject;
            return RazorPartialView<InteractionEventMapPopup, InteractionEventMapPopupViewData>(new InteractionEventMapPopupViewData(interactionEvent));
        }


        #region WADNR FundSource JSON API

        [InteractionEventJsonApiFeature]
        public JsonNetJArrayResult InteractionEventJsonApi()
        {
            var interactionEvents = HttpRequestStorage.DatabaseEntities.InteractionEvents.ToList();
            var jsonApiInteractionEvents = InteractionEventApiJson.MakeInteractionEventApiJsonsFromInteractionEvents(interactionEvents);
            return new JsonNetJArrayResult(jsonApiInteractionEvents);
        }

        #endregion


    }
}