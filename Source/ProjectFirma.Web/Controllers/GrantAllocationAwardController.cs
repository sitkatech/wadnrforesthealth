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

            var viewData = new DetailViewData(CurrentPerson, grantAllocationAward, backButtonUrl, backButtonText, suppliesLineItemGridSpec);
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
        [GrantAllocationAwardSuppliesLineItemEditAsAdminFeature]
        public PartialViewResult EditSupplies(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            var viewModel = new EditSuppliesViewModel(grantAllocationAward);
            return SuppliesViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardSuppliesLineItemEditAsAdminFeature]
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