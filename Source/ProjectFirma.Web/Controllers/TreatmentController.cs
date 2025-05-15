using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.ProjectUpdate;
using ProjectFirma.Web.Views.Treatment;

namespace ProjectFirma.Web.Controllers
{
    public class TreatmentController : FirmaBaseController
    {

        [HttpGet]
        [TreatmentEditAsAdminFeature]
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
        [TreatmentEditAsAdminFeature]
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
        [TreatmentEditAsAdminFeature]
        public PartialViewResult EditTreatmentsForProjectTreatmentArea(ProjectLocationPrimaryKey projectLocationPrimaryKey)
        {
            var projectLocation = projectLocationPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentsForAProjectViewModel(projectLocation.Treatments.ToList());
            return TreatmentsForProjectTreatmentAreaViewEdit(viewModel);
        }

        [HttpPost]
        [TreatmentEditAsAdminFeature]
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
        [TreatmentEditAsAdminFeature]
        public PartialViewResult EditTreatment(TreatmentPrimaryKey treatmentPrimaryKey)
        {
            var treatment = treatmentPrimaryKey.EntityObject;
            var viewModel = new EditTreatmentViewModel(treatment);
            return TreatmentViewEdit(viewModel, treatment.Project);
        }

        [HttpPost]
        [TreatmentEditAsAdminFeature]
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
        [TreatmentEditAsAdminFeature]
        public PartialViewResult NewTreatmentFromProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;



            var viewModel = new EditTreatmentViewModel();
            return TreatmentViewEdit(viewModel, project);
        }

        [HttpPost]
        [TreatmentEditAsAdminFeature]
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


        [ProjectActivitiesSectionViewFeature]
        public GridJsonNetJObjectResult<TreatmentGroup> TreatmentAreaProjectDetailGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var treatmentAreas = project.Treatments.Where(x => x.ProjectLocationID.HasValue).Select(x => x.ProjectLocation).GroupBy(x => x.ProjectLocationID).Select(x => x.ToList().First()).ToList();
            var treatmentGroups = new List<TreatmentGroup>();
            var treatmentAreaToTreatmentGroups = treatmentAreas.Select(x => new TreatmentGroup(x)).ToList();
            treatmentGroups.AddRange(treatmentAreaToTreatmentGroups);
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


    }
}