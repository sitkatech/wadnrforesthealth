using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.GrantAllocationAward;
using ProjectFirma.Web.Views.GrantAllocationAwardLandownerCostShareLineItem;
using ProjectFirma.Web.Views.Treatment;

namespace ProjectFirma.Web.Controllers
{
    public class TreatmentController : FirmaBaseController
    {



        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemCreateFeature]
        public PartialViewResult NewTreatmentFromProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentViewModel()
            {
                ProjectID = project.ProjectID
            };
            return TreatmentViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTreatmentFromProject(ProjectPrimaryKey projectPrimaryKey, EditTreatmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return TreatmentViewEdit(viewModel);
            }

            //var grantAllocationAward = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.Single(ga => ga.GrantAllocationAwardID == viewModel.GrantAllocationAwardID);
            //var project = HttpRequestStorage.DatabaseEntities.Projects.Single(x => x.ProjectID == viewModel.ProjectID);
            //var treatmentType = TreatmentType.All.Single(x => x.TreatmentTypeID == viewModel.TreatmentTypeID);
            //var treatmentDetailedActivity = TreatmentDetailedActivityType.
            //var treatment = Treatment.CreateNewBlank(project, treatmentType);
            ////landownerCostShareLineItem.GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            //viewModel.UpdateModel(treatment);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TreatmentViewEdit(EditTreatmentViewModel viewModel)
        {
            var statusList = LandownerCostShareLineItemStatus.All;
            var projectList = HttpRequestStorage.DatabaseEntities.Projects.ToList().OrderBy(x => x.DisplayName);
            var grantAllocationAwardList = HttpRequestStorage.DatabaseEntities.GrantAllocationAwards.OrderBy(x => x.GrantAllocationAwardName);
            var viewData = new EditTreatmentViewData(statusList, projectList, grantAllocationAwardList);
            return RazorPartialView<EditTreatment, EditTreatmentViewData, EditTreatmentViewModel>(viewData, viewModel);
        }



        //[HttpGet]
        //[GrantAllocationAwardLandownerCostShareLineItemDeleteFeature]
        //public PartialViewResult DeleteLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItemPrimaryKey grantAllocationAwardLandownerCostShareLineItemPrimaryKey)
        //{
        //    var viewModel = new ConfirmDialogFormViewModel(grantAllocationAwardLandownerCostShareLineItemPrimaryKey.PrimaryKeyValue);
        //    return ViewDeleteLandownerCostShareLineItem(grantAllocationAwardLandownerCostShareLineItemPrimaryKey.EntityObject, viewModel);
        //}

        //private PartialViewResult ViewDeleteLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem, ConfirmDialogFormViewModel viewModel)
        //{
        //    var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()}?";
        //    var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
        //    return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        //}

        //[HttpPost]
        //[GrantAllocationAwardLandownerCostShareLineItemDeleteFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult DeleteLandownerCostShareLineItem(GrantAllocationAwardLandownerCostShareLineItemPrimaryKey grantAllocationAwardLandownerCostShareLineItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        //{
        //    var landownerCostShareLineItem = grantAllocationAwardLandownerCostShareLineItemPrimaryKey.EntityObject;
        //    if (!ModelState.IsValid)
        //    {
        //        return ViewDeleteLandownerCostShareLineItem(landownerCostShareLineItem, viewModel);
        //    }

        //    var message = $"{FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()} successfully deleted.";
        //    landownerCostShareLineItem.DeleteFull(HttpRequestStorage.DatabaseEntities);
        //    SetMessageForDisplay(message);
        //    return new ModalDialogFormJsonResult();
        //}

        //[HttpGet]
        //[GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        //public PartialViewResult EditTreatment(TreatmentPrimaryKey treatmentPrimaryKey)
        //{
        //    var treatment = treatmentPrimaryKey.EntityObject;
        //    var viewModel = new EditTreatmentViewModel(treatment);
        //    return TreatmentViewEdit(viewModel);
        //}

        //[HttpPost]
        //[GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult EditTreatment(TreatmentPrimaryKey treatmentPrimaryKey, EditTreatmentViewModel viewModel)
        //{
        //    var treatment = treatmentPrimaryKey.EntityObject;
        //    if (!ModelState.IsValid)
        //    {
        //        return TreatmentViewEdit(viewModel);
        //    }
        //    viewModel.UpdateModel(treatment);
        //    return new ModalDialogFormJsonResult();
        //}

    }
}