using System;
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
        public PartialViewResult NewTreatmentsForAProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentsForAProjectViewModel()
            {
                ProjectID = project.ProjectID
            };
            return TreatmentsForAProjectViewEdit(viewModel, project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea && !x.Treatments.Any()));
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTreatmentsForAProject(ProjectPrimaryKey projectPrimaryKey, EditTreatmentsForAProjectViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentsForAProjectViewEdit(viewModel, project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea && !x.Treatments.Any()));
            }


            viewModel.UpdateModel(project.Treatments.ToList());
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TreatmentsForAProjectViewEdit(EditTreatmentsForAProjectViewModel viewModel, IEnumerable<ProjectLocation> projectLocationTreatmentAreas)
        {
            var treatmentTypesList = TreatmentType.All;
            var viewData = new EditTreatmentsForAProjectViewData(treatmentTypesList, projectLocationTreatmentAreas);
            return RazorPartialView<EditTreatmentsForAProject, EditTreatmentsForAProjectViewData, EditTreatmentsForAProjectViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult EditTreatmentsForProjectTreatmentArea(ProjectLocationPrimaryKey projectLocationPrimaryKey)
        {
            var projectLocation = projectLocationPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentsForAProjectViewModel(projectLocation.Treatments.ToList());
            return TreatmentsForProjectTreatmentAreaViewEdit(viewModel);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatmentsForProjectTreatmentArea(ProjectLocationPrimaryKey projectLocationPrimaryKey, EditTreatmentsForAProjectViewModel viewModel)
        {
            var projectLocation = projectLocationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentsForProjectTreatmentAreaViewEdit(viewModel);
            }
            viewModel.UpdateModel(projectLocation.Treatments.ToList());
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TreatmentsForProjectTreatmentAreaViewEdit(EditTreatmentsForAProjectViewModel viewModel)
        {
            var treatmentTypesList = TreatmentType.All;
            var viewData = new EditTreatmentsForAProjectViewData(treatmentTypesList, new List<ProjectLocation>());
            return RazorPartialView<EditTreatmentsForAProject, EditTreatmentsForAProjectViewData, EditTreatmentsForAProjectViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult EditTreatment(TreatmentPrimaryKey treatmentPrimaryKey)
        {
            var treatment = treatmentPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentViewModel(treatment);
            return TreatmentViewEdit(viewModel, treatment.Project);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatment(TreatmentPrimaryKey treatmentPrimaryKey, EditTreatmentViewModel viewModel)
        {
            var treatment = treatmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentViewEdit(viewModel, treatment.Project);
            }
            viewModel.UpdateModel(treatment);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult TreatmentViewEdit(EditTreatmentViewModel viewModel, Project project)
        {
            var treatmentTypesList = TreatmentType.All;
            var treatmentDetailedActivityTypesList = TreatmentDetailedActivityType.All;
            var treatmentAreas = project.ProjectLocations.Where(x => x.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea);
            var treatmentCodesList = HttpRequestStorage.DatabaseEntities.TreatmentCodes.ToList();
            var viewData = new EditTreatmentViewData(treatmentTypesList, treatmentDetailedActivityTypesList, treatmentAreas, treatmentCodesList);
            return RazorPartialView<EditTreatment, EditTreatmentViewData, EditTreatmentViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        public PartialViewResult NewTreatmentFromProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;



            var viewModel = new EditTreatmentViewModel();
            return TreatmentViewEdit(viewModel, project);
        }

        [HttpPost]
        [GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTreatmentFromProject(ProjectPrimaryKey projectPrimaryKey, EditTreatmentViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return TreatmentViewEdit(viewModel, project);
            }

            var newTreatment = Treatment.CreateNewBlank(project, TreatmentType.Other, TreatmentDetailedActivityType.Other);
            viewModel.UpdateModel(newTreatment);
            return new ModalDialogFormJsonResult();
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



    }
}