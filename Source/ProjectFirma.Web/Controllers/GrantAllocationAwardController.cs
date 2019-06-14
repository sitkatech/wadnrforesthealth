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
        public PartialViewResult New()
        {
            var viewModel = new EditGrantAllocationAwardViewModel();
            return GrantAllocationAwardViewEdit(viewModel, "New");
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
            return GrantAllocationAwardViewEdit(viewModel, "New");
        }

        [HttpPost]
        [GrantAllocationAwardCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditGrantAllocationAwardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Null is likely wrong here!!!
                return GrantAllocationAwardViewEdit(viewModel, "New");
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
            return GrantAllocationAwardViewEdit(viewModel, "Edit");
        }

        [HttpPost]
        [GrantAllocationAwardEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(GrantAllocationAwardPrimaryKey grantAllocationAwardPrimaryKey, EditGrantAllocationAwardViewModel viewModel)
        {
            var grantAllocationAward = grantAllocationAwardPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return GrantAllocationAwardViewEdit(viewModel, "Edit");
            }
            viewModel.UpdateModel(grantAllocationAward);
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult GrantAllocationAwardViewEdit(EditGrantAllocationAwardViewModel viewModel, string controllerAction)
        {
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations;

            var viewData = new EditGrantAllocationAwardViewData(grantAllocations, controllerAction);
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
            var viewData = new DetailViewData(CurrentPerson, grantAllocationAward, backButtonUrl, backButtonText);
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
        #endregion "Travel"

        #region "Landowner Cost Share"
        #endregion "Landowner Cost Share"

        #region "Contractor Invoice"
        #endregion "Contractor Invoice"
    }
}