using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.TreatmentUpdate;

namespace ProjectFirma.Web.Controllers
{
    public class TreatmentUpdateController : FirmaBaseController
    {
        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult EditTreatmentUpdate(TreatmentUpdatePrimaryKey treatmentUpdatePrimaryKey)
        {
            var treatmentUpdate = treatmentUpdatePrimaryKey.EntityObject;
            var viewModel = new EditTreatmentUpdateViewModel(treatmentUpdate);
            return TreatmentUpdateViewEdit(viewModel, treatmentUpdate.ProjectUpdateBatch);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatmentUpdate(TreatmentUpdatePrimaryKey treatmentUpdatePrimaryKey, EditTreatmentUpdateViewModel viewModel)
        {
            var treatmentUpdate = treatmentUpdatePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentUpdateViewEdit(viewModel, treatmentUpdate.ProjectUpdateBatch);
            }
            viewModel.UpdateModel(treatmentUpdate);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TreatmentUpdateViewEdit(EditTreatmentUpdateViewModel viewModel, ProjectUpdateBatch projectUpdateBatch)
        {
            var treatmentTypesList = TreatmentType.All;
            var treatmentDetailedActivityTypesList = TreatmentDetailedActivityType.All;
            var treatmentAreas = projectUpdateBatch.ProjectLocationUpdates.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea);
            var viewData = new EditTreatmentUpdateViewData(treatmentTypesList, treatmentDetailedActivityTypesList, treatmentAreas);
            return RazorPartialView<EditTreatmentUpdate, EditTreatmentUpdateViewData, EditTreatmentUpdateViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult NewTreatmentUpdateFromProjectUpdateBatch(ProjectUpdateBatchPrimaryKey projectUpdateBatchPrimaryKey)
        {
            var projectUpdateBatch = projectUpdateBatchPrimaryKey.EntityObject;

            var viewModel = new EditTreatmentUpdateViewModel();
            return TreatmentUpdateViewEdit(viewModel, projectUpdateBatch);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTreatmentUpdateFromProjectUpdateBatch(ProjectUpdateBatchPrimaryKey projectUpdateBatchPrimaryKey, EditTreatmentUpdateViewModel viewModel)
        {
            var projectUpdateBatch = projectUpdateBatchPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentUpdateViewEdit(viewModel, projectUpdateBatch);
            }

            var newTreatmentUpdate = TreatmentUpdate.CreateNewBlank(projectUpdateBatch, TreatmentType.Other, TreatmentDetailedActivityType.Other);
            viewModel.UpdateModel(newTreatmentUpdate);
            return new ModalDialogFormJsonResult();
        }

    }
}