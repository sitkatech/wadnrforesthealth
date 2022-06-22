using System.Collections.Generic;
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
            return TreatmentViewEdit(viewModel, project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea && !x.Treatments.Any()));
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTreatmentFromProject(ProjectPrimaryKey projectPrimaryKey, EditTreatmentViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentViewEdit(viewModel, project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea && !x.Treatments.Any()));
            }


            viewModel.UpdateModel(project.Treatments.ToList());
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TreatmentViewEdit(EditTreatmentViewModel viewModel, IEnumerable<ProjectLocation> projectLocationTreatmentAreas)
        {
            var treatmentTypesList = TreatmentType.All;
            var viewData = new EditTreatmentViewData(treatmentTypesList, projectLocationTreatmentAreas);
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

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult EditTreatmentsForProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentViewModel(project.Treatments.ToList());
            var projectLocationTreatmentAreas = project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea);
            return TreatmentViewEdit(viewModel, projectLocationTreatmentAreas);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatmentsForProject(ProjectPrimaryKey projectPrimaryKey, EditTreatmentViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentViewEdit(viewModel, project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea && !x.Treatments.Any()));
            }
            viewModel.UpdateModel(project.Treatments.ToList());
            return new ModalDialogFormJsonResult();
        }

    }
}