using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.FocusArea;
using ProjectFirma.Web.Views.GrantAllocationAward;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class GrantAllocationAwardController : FirmaBaseController
    {

        [HttpPost]
        [AnonymousUnclassifiedFeature]
        public JsonResult GetGrantAllocationDates(int grantAllocationID)
        {
            var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.FirstOrDefault(x => x.GrantAllocationID == grantAllocationID);

            if (grantAllocation != null)
            {
                var grantAllocationStartYear = grantAllocation.StartDate.HasValue ? grantAllocation.StartDate.Value.Year : DateTime.Now.Year;
                var grantAllocationEndDateJsonStructure =  new {endDate = grantAllocation.EndDateDisplay, startYear = grantAllocationStartYear, name = grantAllocation.DisplayName, id = grantAllocation.GrantAllocationID};
                //use JSON structure for jquery's autocomplete functionality
                return Json(grantAllocationEndDateJsonStructure, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [GrantAllocationAwardDeleteFeature]
        public PartialViewResult DeleteGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantAllocationAward(grantAllocationAwardPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantAllocationAward(GrantAllocationAward grantAllocationAward, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !grantAllocationAward.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} '{grantAllocationAward.GrantAllocationAwardName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage(FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel(), SitkaRoute<GrantAllocationAwardController>.BuildLinkFromExpression(x => x.GrantAllocationAwardDetail(grantAllocationAward), "here"));
            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantAllocationAward(grantAllocationAward, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} \"{grantAllocationAward.GrantAllocationAwardName}\" successfully deleted.";
            grantAllocationAward.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [GrantAllocationAwardCreateFeature]
        public PartialViewResult NewForAFocusArea(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardViewModel()
            {
                FocusAreaID = focusArea.FocusAreaID
            };
            return GrantAllocationAwardViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewForAFocusArea(FocusAreaPrimaryKey focusAreaPrimaryKey, EditGrantAllocationAwardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardViewEdit(viewModel);
            }
            var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(ga => ga.GrantAllocationID == viewModel.GrantAllocationID);
            var focusArea = HttpRequestStorage.DatabaseEntities.FocusAreas.Single(fa => fa.FocusAreaID == viewModel.FocusAreaID);
            var grantAllocationAward = GrantAllocationAward.CreateNewBlank(grantAllocation, focusArea);
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardCreateFeature]
        public PartialViewResult NewForAGrantAllocation(GrantAllocationPrimaryKey grantAllocationPrimaryKey)
        {
            var grantAllocation = grantAllocationPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardViewModel()
            {
                GrantAllocationID = grantAllocation.GrantAllocationID
            };
            return GrantAllocationAwardViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewForAGrantAllocation(GrantAllocationPrimaryKey grantAllocationPrimaryKey, EditGrantAllocationAwardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardViewEdit(viewModel);
            }
            var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(ga => ga.GrantAllocationID == viewModel.GrantAllocationID);
            var focusArea = HttpRequestStorage.DatabaseEntities.FocusAreas.Single(fa => fa.FocusAreaID == viewModel.FocusAreaID);
            var grantAllocationAward = GrantAllocationAward.CreateNewBlank(grantAllocation, focusArea);
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult Edit(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardViewModel(grantAllocationAward);
            return GrantAllocationAwardViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult GrantAllocationAwardViewEdit(EditGrantAllocationAwardViewModel viewModel)
        {
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations;
            var focusAreas = HttpRequestStorage.DatabaseEntities.FocusAreas;

            var viewData = new EditGrantAllocationAwardViewData(grantAllocations, focusAreas);
            return RazorPartialView<EditGrantAllocationAward, EditGrantAllocationAwardViewData, EditGrantAllocationAwardViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardViewFeature]
        public ViewResult GrantAllocationAwardDetail(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (grantAllocationAward == null)
            {
                throw new Exception($"Could not find GrantAllocationAwardID # {grantAllocationAwardPrimaryKey.PrimaryKeyValue}; has it been deleted?");
            }

            var backButtonUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(x => x.Detail(grantAllocationAward.FocusAreaID));
            var backButtonText = $"Back to {FieldDefinition.FocusArea.GetFieldDefinitionLabel()}: {grantAllocationAward.FocusArea.FocusAreaName}";

            var suppliesLineItemGridSpec = new SuppliesLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var personnelAndBenefitsLineItemGridSpec = new PersonnelAndBenefitsLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var travelLineItemGridSpec = new TravelLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var landownerCostShareLineItemGridSpec = new LandownerCostShareLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var contractorInvoiceItemGridSpec = new ContractorInvoiceItemGridSpec(CurrentPerson, grantAllocationAward);

            var viewData = new Views.GrantAllocationAward.DetailViewData(CurrentPerson, 
                                              grantAllocationAward, 
                                              backButtonUrl, 
                                              backButtonText, 
                                              suppliesLineItemGridSpec, 
                                              personnelAndBenefitsLineItemGridSpec,
                                              travelLineItemGridSpec,
                                              landownerCostShareLineItemGridSpec,
                                              contractorInvoiceItemGridSpec);
            return RazorView<Views.GrantAllocationAward.Detail, Views.GrantAllocationAward.DetailViewData>(viewData);
        }

        [GrantAllocationAwardViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAward> GrantAllocationAwardByFocusAreaGridJsonData(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var grantAllocationAwards = focusArea.GrantAllocationAwards.ToList();
            var gridSpec = new GrantAllocationAwardGridSpec(CurrentPerson, focusArea);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAward>(grantAllocationAwards, gridSpec);
            return gridJsonNetJObjectResult;
        }


        #region "Indirect Costs"

        #endregion "Indirect Costs"

        #region "Supplies"

        [GrantAllocationAwardSuppliesLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardSuppliesLineItem> SuppliesLineItemGridJsonData(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var suppliesLineItems = grantAllocationAward.GrantAllocationAwardSuppliesLineItems;
            var gridSpec = new SuppliesLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardSuppliesLineItem>(suppliesLineItems.ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [GrantAllocationAwardSuppliesLineItemCreateFeature]
        public PartialViewResult NewSuppliesLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardSuppliesLineItemViewModel()
            {
                GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID
            };
            return GrantAllocationAwardSuppliesLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardSuppliesLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewSuppliesLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardSuppliesLineItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardSuppliesLineItemViewEdit(viewModel);
            }

            var grantAllocationAward = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.Single(ga => ga.GrantAllocationAwardID == viewModel.GrantAllocationAwardID);
            var grantAllocationAwardSuppliesLineItem = GrantAllocationAwardSuppliesLineItem.CreateNewBlank(grantAllocationAward);
            viewModel.UpdateModel(grantAllocationAwardSuppliesLineItem);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationAwardSuppliesLineItemViewEdit(EditGrantAllocationAwardSuppliesLineItemViewModel viewModel)
        {
            var viewData = new EditGrantAllocationAwardSuppliesLineItemViewData();
            return RazorPartialView<EditGrantAllocationAwardSuppliesLineItem, EditGrantAllocationAwardSuppliesLineItemViewData, EditGrantAllocationAwardSuppliesLineItemViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardSuppliesLineItemDeleteFeature]
        public PartialViewResult DeleteSuppliesLineItem(GrantAllocationAwardSuppliesLineItemPrimaryKey grantAllocationAwardSuppliesLineItemPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardSuppliesLineItemPrimaryKey.PrimaryKeyValue);
            return ViewDeleteSuppliesLineItem(grantAllocationAwardSuppliesLineItemPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteSuppliesLineItem(GrantAllocationAwardSuppliesLineItem grantAllocationSuppliesLineItem, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardSuppliesLineItem.GetFieldDefinitionLabel()} '{grantAllocationSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDescription}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardSuppliesLineItemDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteSuppliesLineItem(GrantAllocationAwardSuppliesLineItemPrimaryKey grantAllocationAwardSuppliesLineItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantAllocationAwardSuppliesLineItem = grantAllocationAwardSuppliesLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteSuppliesLineItem(grantAllocationAwardSuppliesLineItem, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationAwardSuppliesLineItem.GetFieldDefinitionLabel()} \"{grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDescription}\" successfully deleted.";
            grantAllocationAwardSuppliesLineItem.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardSuppliesLineItemEditAsAdminFeature]
        public PartialViewResult EditSuppliesLineItem(GrantAllocationAwardSuppliesLineItemPrimaryKey grantAllocationAwardSuppliesLineItemPrimaryKey)
        {
            var grantAllocationAwardSuppliesLineItem = grantAllocationAwardSuppliesLineItemPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardSuppliesLineItemViewModel(grantAllocationAwardSuppliesLineItem);
            return GrantAllocationAwardSuppliesLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardSuppliesLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditSuppliesLineItem(GrantAllocationAwardSuppliesLineItemPrimaryKey grantAllocationAwardSuppliesLineItemPrimaryKey, EditGrantAllocationAwardSuppliesLineItemViewModel viewModel)
        {
            var grantAllocationAwardSuppliesLineItem = grantAllocationAwardSuppliesLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardSuppliesLineItemViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAwardSuppliesLineItem);
            return new ModalDialogFormJsonResult();
        }


        #endregion "Supplies"

        #region "Personnel & Benefits"

        [GrantAllocationAwardPersonnelAndBenefitsLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardPersonnelAndBenefitsLineItem> PersonnelAndBenefitsLineItemGridJsonData(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var personnelAndBenefitsLineItems = grantAllocationAward.GrantAllocationAwardPersonnelAndBenefitsLineItems;
            var gridSpec = new PersonnelAndBenefitsLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardPersonnelAndBenefitsLineItem>(personnelAndBenefitsLineItems.ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [GrantAllocationAwardPersonnelAndBenefitsLineItemCreateFeature]
        public PartialViewResult NewPersonnelAndBenefitsLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel()
            {
                GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID
            };
            return GrantAllocationAwardPersonnelAndBenefitsLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardPersonnelAndBenefitsLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewPersonnelAndBenefitsLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardPersonnelAndBenefitsLineItemViewEdit(viewModel);
            }

            var grantAllocationAward = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.Single(ga => ga.GrantAllocationAwardID == viewModel.GrantAllocationAwardID);
            var personnelAndBenefitsLineItem = GrantAllocationAwardPersonnelAndBenefitsLineItem.CreateNewBlank(grantAllocationAward);
            viewModel.UpdateModel(personnelAndBenefitsLineItem);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationAwardPersonnelAndBenefitsLineItemViewEdit(EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel viewModel)
        {
            var peopleList = HttpRequestStorage.DatabaseEntities.People.ToList().OrderBy(x => x.FullNameLastFirst);
            var viewData = new EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewData(peopleList);
            return RazorPartialView<EditGrantAllocationAwardPersonnelAndBenefitsLineItem, EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewData, EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature]
        public PartialViewResult DeletePersonnelAndBenefitsLineItem(GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey.PrimaryKeyValue);
            return ViewDeletePersonnelAndBenefitsLineItem(grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeletePersonnelAndBenefitsLineItem(GrantAllocationAwardPersonnelAndBenefitsLineItem grantAllocationAwardPersonnelAndBenefitsLineItem, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsLineItem.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeletePersonnelAndBenefitsLineItem(GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var personnelAndBenefitsLineItem = grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeletePersonnelAndBenefitsLineItem(personnelAndBenefitsLineItem, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsLineItem.GetFieldDefinitionLabel()} successfully deleted.";
            personnelAndBenefitsLineItem.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature]
        public PartialViewResult EditPersonnelAndBenefitsLineItem(GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey)
        {
            var personnelAndBenefitsLineItem = grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel(personnelAndBenefitsLineItem);
            return GrantAllocationAwardPersonnelAndBenefitsLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPersonnelAndBenefitsLineItem(GrantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey, EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel viewModel)
        {
            var personnelAndBenefitsLineItem = grantAllocationAwardPersonnelAndBenefitsLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardPersonnelAndBenefitsLineItemViewEdit(viewModel);
            }
            viewModel.UpdateModel(personnelAndBenefitsLineItem);
            return new ModalDialogFormJsonResult();
        }


        #endregion "Personnel & Benefits"

        #region "Travel"

        [GrantAllocationAwardTravelLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardTravelLineItem> TravelLineItemGridJsonData(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var travelLineItems = grantAllocationAward.GrantAllocationAwardTravelLineItems;
            var gridSpec = new TravelLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardTravelLineItem>(travelLineItems.ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [GrantAllocationAwardTravelLineItemCreateFeature]
        public PartialViewResult NewTravelLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardTravelLineItemViewModel()
            {
                GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID
            };
            return GrantAllocationAwardTravelLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardTravelLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTravelLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardTravelLineItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardTravelLineItemViewEdit(viewModel);
            }

            var grantAllocationAward = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.Single(ga => ga.GrantAllocationAwardID == viewModel.GrantAllocationAwardID);
            var travelLineItem = GrantAllocationAwardTravelLineItem.CreateNewBlank(grantAllocationAward, GrantAllocationAwardTravelLineItemType.Transportation);
            viewModel.UpdateModel(travelLineItem);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationAwardTravelLineItemViewEdit(EditGrantAllocationAwardTravelLineItemViewModel viewModel)
        {
            var travelTypes = GrantAllocationAwardTravelLineItemType.All;
            var peopleList = HttpRequestStorage.DatabaseEntities.People.ToList().OrderBy(x => x.FullNameLastFirst);
            var viewData = new EditGrantAllocationAwardTravelLineItemViewData(travelTypes, peopleList);
            return RazorPartialView<EditGrantAllocationAwardTravelLineItem, EditGrantAllocationAwardTravelLineItemViewData, EditGrantAllocationAwardTravelLineItemViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardTravelLineItemDeleteFeature]
        public PartialViewResult DeleteTravelLineItem(GrantAllocationAwardTravelLineItemPrimaryKey grantAllocationAwardTravelLineItemPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardTravelLineItemPrimaryKey.PrimaryKeyValue);
            return ViewDeleteTravelLineItem(grantAllocationAwardTravelLineItemPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteTravelLineItem(GrantAllocationAwardTravelLineItem grantAllocationAwardTravelLineItem, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardTravelLineItem.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardTravelLineItemDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteTravelLineItem(GrantAllocationAwardTravelLineItemPrimaryKey grantAllocationAwardTravelLineItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var travelLineItem = grantAllocationAwardTravelLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTravelLineItem(travelLineItem, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationAwardTravelLineItem.GetFieldDefinitionLabel()} successfully deleted.";
            travelLineItem.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardTravelLineItemEditAsAdminFeature]
        public PartialViewResult EditTravelLineItem(GrantAllocationAwardTravelLineItemPrimaryKey grantAllocationAwardTravelLineItemPrimaryKey)
        {
            var travelLineItem = grantAllocationAwardTravelLineItemPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardTravelLineItemViewModel(travelLineItem);
            return GrantAllocationAwardTravelLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardTravelLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTravelLineItem(GrantAllocationAwardTravelLineItemPrimaryKey grantAllocationAwardTravelLineItemPrimaryKey, EditGrantAllocationAwardTravelLineItemViewModel viewModel)
        {
            var travelLineItem = grantAllocationAwardTravelLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardTravelLineItemViewEdit(viewModel);
            }
            viewModel.UpdateModel(travelLineItem);
            return new ModalDialogFormJsonResult();
        }


        #endregion "Travel"

        #region "Landowner Cost Share"
        [GrantAllocationAwardLandownerCostShareLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem> LandownerCostShareLineItemGridJsonData(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var landownerCostShareLineItems = grantAllocationAward.GrantAllocationAwardLandownerCostShareLineItems;
            var gridSpec = new LandownerCostShareLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem>(landownerCostShareLineItems.ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantAllocationAwardLandownerCostShareLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem> LandownerCostShareLineItemProjectDetailGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var landownerCostShareLineItems = project.GrantAllocationAwardLandownerCostShareLineItems;
            var gridSpec = new LandownerCostShareLineItemProjectDetailGridSpec(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem>(landownerCostShareLineItems.ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProjectActivitiesSectionViewFeature]
        public GridJsonNetJObjectResult<TreatmentGroup> TreatmentAreaProjectDetailGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var treatmentAreas = project.Treatments.Where(x => x.ProjectLocationID.HasValue).Select(x => x.ProjectLocation).GroupBy(x => x.ProjectLocationID).Select(x => x.ToList().First()).ToList();
            var grantAllocationAwardLandownerCostShareLineItems = project.Treatments.Where(x => x.GrantAllocationAwardLandownerCostShareLineItemID.HasValue).Select(x => x.GrantAllocationAwardLandownerCostShareLineItem).GroupBy(x => x.GrantAllocationAwardLandownerCostShareLineItemID).Select(x => x.ToList().First());
            var treatmentGroups = new List<TreatmentGroup>();
            var treatmentAreaToTreatmentGroups = treatmentAreas.Select(x => new TreatmentGroup(x)).ToList();
            var grantAllocationAwardLandownerCostShareLineItemsToTreatmentGroups = grantAllocationAwardLandownerCostShareLineItems.Select(x => new TreatmentGroup(x)).ToList();
            treatmentGroups.AddRange(treatmentAreaToTreatmentGroups);
            treatmentGroups.AddRange(grantAllocationAwardLandownerCostShareLineItemsToTreatmentGroups);
            var gridSpec = new TreatmentGroupGridSpec(CurrentPerson, project);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentGroup>(treatmentGroups, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProjectActivitiesSectionViewFeature]
        public GridJsonNetJObjectResult<Treatment> TreatmentProjectDetailGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var treatments = project.Treatments.ToList();
            var gridSpec = new TreatmentGridSpec(CurrentPerson, project);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Treatment>(treatments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProjectActivitiesSectionViewFeature]
        public GridJsonNetJObjectResult<TreatmentUpdate> TreatmentUpdateProjectDetailGridJsonData(ProjectUpdateBatchPrimaryKey projectUpdateBatchPrimaryKey)
        {
            var projectUpdateBatch = projectUpdateBatchPrimaryKey.EntityObject;
            var treatmentUpdates = projectUpdateBatch.TreatmentUpdates.ToList();
            var gridSpec = new TreatmentUpdateGridSpec(CurrentPerson, projectUpdateBatch);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentUpdate>(treatmentUpdates, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemCreateFeature]
        public PartialViewResult NewLandownerCostShareLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardLandownerCostShareLineItemViewModel()
            {
                GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID
            };
            return GrantAllocationAwardLandownerCostShareLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewLandownerCostShareLineItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardLandownerCostShareLineItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardLandownerCostShareLineItemViewEdit(viewModel);
            }

            var grantAllocationAward = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.Single(ga => ga.GrantAllocationAwardID == viewModel.GrantAllocationAwardID);
            var project = HttpRequestStorage.DatabaseEntities.Projects.Single(x => x.ProjectID == viewModel.ProjectID);
            var landownerCostShareLineItemStatus = LandownerCostShareLineItemStatus.All.Single(x => x.LandownerCostShareLineItemStatusID == viewModel.StatusID);
            var landownerCostShareLineItem = GrantAllocationAwardLandownerCostShareLineItem.CreateNewBlank(project, landownerCostShareLineItemStatus);
            landownerCostShareLineItem.GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            viewModel.UpdateModel(landownerCostShareLineItem);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationAwardLandownerCostShareLineItemViewEdit(EditGrantAllocationAwardLandownerCostShareLineItemViewModel viewModel)
        {
            var statusList = LandownerCostShareLineItemStatus.All;
            var projectList = HttpRequestStorage.DatabaseEntities.Projects.ToList().OrderBy(x => x.DisplayName);
            var grantAllocationAwardList = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.OrderBy(x => x.GrantAllocationAwardName);
            var viewData = new EditGrantAllocationAwardLandownerCostShareLineItemViewData(statusList, projectList, grantAllocationAwardList);
            return RazorPartialView<EditGrantAllocationAwardLandownerCostShareLineItem, EditGrantAllocationAwardLandownerCostShareLineItemViewData, EditGrantAllocationAwardLandownerCostShareLineItemViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemDeleteFeature]
        public PartialViewResult DeleteLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItemPrimaryKey grantAllocationAwardLandownerCostShareLineItemPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardLandownerCostShareLineItemPrimaryKey.PrimaryKeyValue);
            return ViewDeleteLandownerCostShareLineItem(grantAllocationAwardLandownerCostShareLineItemPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItemPrimaryKey grantAllocationAwardLandownerCostShareLineItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var landownerCostShareLineItem = grantAllocationAwardLandownerCostShareLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteLandownerCostShareLineItem(landownerCostShareLineItem, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()} successfully deleted.";
            landownerCostShareLineItem.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult EditLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItemPrimaryKey grantAllocationAwardLandownerCostShareLineItemPrimaryKey)
        {
            var landownerCostShareLineItem = grantAllocationAwardLandownerCostShareLineItemPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardLandownerCostShareLineItemViewModel(landownerCostShareLineItem);
            return GrantAllocationAwardLandownerCostShareLineItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItemPrimaryKey grantAllocationAwardLandownerCostShareLineItemPrimaryKey, EditGrantAllocationAwardLandownerCostShareLineItemViewModel viewModel)
        {
            var landownerCostShareLineItem = grantAllocationAwardLandownerCostShareLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardLandownerCostShareLineItemViewEdit(viewModel);
            }
            viewModel.UpdateModel(landownerCostShareLineItem);
            return new ModalDialogFormJsonResult();
        }


        #endregion "Landowner Cost Share"

        #region "Contractor Invoice"
        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult EditContractorInvoice(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditContractorInvoiceViewModel(grantAllocationAward);
            return ContractorInvoiceViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditContractorInvoice(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditContractorInvoiceViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ContractorInvoiceViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ContractorInvoiceViewEdit(EditContractorInvoiceViewModel viewModel)
        {
            var viewData = new EditContractorInvoiceViewData();
            return RazorPartialView<EditContractorInvoice, EditContractorInvoiceViewData, EditContractorInvoiceViewModel>(viewData, viewModel);
        }


        [GrantAllocationAwardContractorInvoiceItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardContractorInvoice> ContractorInvoiceItemGridJsonData(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var contractorInvoices = grantAllocationAward.GrantAllocationAwardContractorInvoices;
            var gridSpec = new ContractorInvoiceItemGridSpec(CurrentPerson, grantAllocationAward);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardContractorInvoice>(contractorInvoices.ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [GrantAllocationAwardContractorInvoiceItemCreateFeature]
        public PartialViewResult NewContractorInvoiceItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardContractorInvoiceItemViewModel()
            {
                GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID
            };
            return GrantAllocationAwardContractorInvoiceItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardContractorInvoiceItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewContractorInvoiceItemFromGrantAllocationAward(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardContractorInvoiceItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardContractorInvoiceItemViewEdit(viewModel);
            }

            var grantAllocationAward = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.Single(ga => ga.GrantAllocationAwardID == viewModel.GrantAllocationAwardID);
            var contractorInvoiceType = GrantAllocationAwardContractorInvoiceType.All.Single(x => x.GrantAllocationAwardContractorInvoiceTypeID == viewModel.TypeID);
            var contractorInvoice = GrantAllocationAwardContractorInvoice.CreateNewBlank(grantAllocationAward, contractorInvoiceType);
            viewModel.UpdateModel(contractorInvoice, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationAwardContractorInvoiceItemViewEdit(EditGrantAllocationAwardContractorInvoiceItemViewModel viewModel)
        {
            var invoiceTypes = GrantAllocationAwardContractorInvoiceType.All;
            var viewData = new EditGrantAllocationAwardContractorInvoiceItemViewData(invoiceTypes);
            return RazorPartialView<EditGrantAllocationAwardContractorInvoiceItem, EditGrantAllocationAwardContractorInvoiceItemViewData, EditGrantAllocationAwardContractorInvoiceItemViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardContractorInvoiceItemDeleteFeature]
        public PartialViewResult DeleteContractorInvoiceItem(GrantAllocationAwardContractorInvoicePrimaryKey grantAllocationAwardContractorInvoiceItemPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardContractorInvoiceItemPrimaryKey.PrimaryKeyValue);
            return ViewDeleteContractorInvoiceItem(grantAllocationAwardContractorInvoiceItemPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteContractorInvoiceItem(GrantAllocationAwardContractorInvoice grantAllocationAwardContractorInvoiceItem, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardContractorInvoiceLineItem.GetFieldDefinitionLabel()} \"{grantAllocationAwardContractorInvoiceItem.GrantAllocationAwardContractorInvoiceDescription}\"?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardContractorInvoiceItemDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteContractorInvoiceItem(GrantAllocationAwardContractorInvoicePrimaryKey grantAllocationAwardContractorInvoiceItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var contractorInvoiceItem = grantAllocationAwardContractorInvoiceItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteContractorInvoiceItem(contractorInvoiceItem, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationAwardContractorInvoiceLineItem.GetFieldDefinitionLabel()} \"{contractorInvoiceItem.GrantAllocationAwardContractorInvoiceDescription}\" successfully deleted.";
            contractorInvoiceItem.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantAllocationAwardContractorInvoiceItemEditAsAdminFeature]
        public PartialViewResult EditContractorInvoiceItem(GrantAllocationAwardContractorInvoicePrimaryKey grantAllocationAwardContractorInvoiceItemPrimaryKey)
        {
            var contractorInvoice = grantAllocationAwardContractorInvoiceItemPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationAwardContractorInvoiceItemViewModel(contractorInvoice);
            return GrantAllocationAwardContractorInvoiceItemViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardContractorInvoiceItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditContractorInvoiceItem(GrantAllocationAwardContractorInvoicePrimaryKey grantAllocationAwardContractorInvoiceItemPrimaryKey, EditGrantAllocationAwardContractorInvoiceItemViewModel viewModel)
        {
            var contractorInvoice = grantAllocationAwardContractorInvoiceItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardContractorInvoiceItemViewEdit(viewModel);
            }
            viewModel.UpdateModel(contractorInvoice, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }


        #endregion "Contractor Invoice"



    }
}