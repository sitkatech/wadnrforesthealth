using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FocusArea;
using ProjectFirma.Web.Views.GrantAllocation;
using ProjectFirma.Web.Views.GrantAllocationAward;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;
using DetailViewData = ProjectFirma.Web.Views.GrantAllocationAward.DetailViewData;

namespace ProjectFirma.Web.Controllers
{
    public class GrantAllocationAwardController : FirmaBaseController
    {

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
                // Null is likely wrong here!!!
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

            var viewData = new EditGrantAllocationAwardViewData(grantAllocations);
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

            var viewData = new DetailViewData(CurrentPerson, 
                                              grantAllocationAward, 
                                              backButtonUrl, 
                                              backButtonText, 
                                              suppliesLineItemGridSpec, 
                                              personnelAndBenefitsLineItemGridSpec,
                                              travelLineItemGridSpec,
                                              landownerCostShareLineItemGridSpec);
            return RazorView<Views.GrantAllocationAward.Detail, DetailViewData>(viewData);
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
        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult EditIndirectCost(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditIndirectCostViewModel(grantAllocationAward);
            return IndirectCostViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditIndirectCost(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditIndirectCostViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return IndirectCostViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult IndirectCostViewEdit(EditIndirectCostViewModel viewModel)
        {

            var viewData = new EditIndirectCostViewData();
            return RazorPartialView<EditIndirectCost, EditIndirectCostViewData, EditIndirectCostViewModel>(viewData, viewModel);
        }
        #endregion "Indirect Costs"

        #region "Supplies"
        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult EditSupplies(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditSuppliesViewModel(grantAllocationAward);
            return SuppliesViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditSupplies(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditSuppliesViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return SuppliesViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult SuppliesViewEdit(EditSuppliesViewModel viewModel)
        {
            var viewData = new EditSuppliesViewData();
            return RazorPartialView<EditSupplies, EditSuppliesViewData, EditSuppliesViewModel>(viewData, viewModel);
        }


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
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardSupplies.GetFieldDefinitionLabel()} '{grantAllocationSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDescription}'?";
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

            var message = $"{FieldDefinition.GrantAllocationAwardSupplies.GetFieldDefinitionLabel()} \"{grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDescription}\" successfully deleted.";
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
        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult EditPersonnelAndBenefits(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditPersonnelAndBenefitsViewModel(grantAllocationAward);
            return PersonnelAndBenefitsViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPersonnelAndBenefits(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditPersonnelAndBenefitsViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return PersonnelAndBenefitsViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult PersonnelAndBenefitsViewEdit(EditPersonnelAndBenefitsViewModel viewModel)
        {
            var viewData = new EditPersonnelAndBenefitsViewData();
            return RazorPartialView<EditPersonnelAndBenefits, EditPersonnelAndBenefitsViewData, EditPersonnelAndBenefitsViewModel>(viewData, viewModel);
        }


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
            var viewData = new EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewData();
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
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardPersonnelAndBenefits.GetFieldDefinitionLabel()} '{grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemDescription}'?";
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

            var message = $"{FieldDefinition.GrantAllocationAwardPersonnelAndBenefits.GetFieldDefinitionLabel()} \"{personnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemDescription}\" successfully deleted.";
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
        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult EditTravel(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditTravelViewModel(grantAllocationAward);
            return TravelViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTravel(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditTravelViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TravelViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TravelViewEdit(EditTravelViewModel viewModel)
        {
            var viewData = new EditTravelViewData();
            return RazorPartialView<EditTravel, EditTravelViewData, EditTravelViewModel>(viewData, viewModel);
        }




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
            var viewData = new EditGrantAllocationAwardTravelLineItemViewData(travelTypes);
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
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabel()} '{grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemDescription}'?";
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

            var message = $"{FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabel()} \"{travelLineItem.GrantAllocationAwardTravelLineItemDescription}\" successfully deleted.";
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
        [HttpGet]
        [GrantAllocationAwardEditAsAdminFeature]
        public PartialViewResult EditLandownerCostShare(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditLandownerCostShareViewModel(grantAllocationAward);
            return LandownerCostShareViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLandownerCostShare(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditLandownerCostShareViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return LandownerCostShareViewEdit(viewModel);
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult LandownerCostShareViewEdit(EditLandownerCostShareViewModel viewModel)
        {
            var viewData = new EditLandownerCostShareViewData();
            return RazorPartialView<EditLandownerCostShare, EditLandownerCostShareViewData, EditLandownerCostShareViewModel>(viewData, viewModel);
        }


        [GrantAllocationAwardLandownerCostShareLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem> LandownerCostShareLineItemGridJsonData(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var landownerCostShareLineItems = grantAllocationAward.GrantAllocationAwardLandownerCostShareLineItems;
            var gridSpec = new LandownerCostShareLineItemGridSpec(CurrentPerson, grantAllocationAward);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem>(landownerCostShareLineItems.ToList(), gridSpec);
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
            var landownerCostShareLineItem = GrantAllocationAwardLandownerCostShareLineItem.CreateNewBlank(grantAllocationAward, null, LandownerCostShareLineItemStatus.Planned);
            viewModel.UpdateModel(landownerCostShareLineItem);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationAwardLandownerCostShareLineItemViewEdit(EditGrantAllocationAwardLandownerCostShareLineItemViewModel viewModel)
        {
            var statusList = LandownerCostShareLineItemStatus.All;
            var projectList = HttpRequestStorage.DatabaseEntities.Projects.ToList();
            var viewData = new EditGrantAllocationAwardLandownerCostShareLineItemViewData(statusList, projectList);
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
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel()} \"{grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemID}\"?";
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

            var message = $"{FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel()} \"{landownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemID}\" successfully deleted.";
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
        #endregion "Contractor Invoice"


        
    }
}