using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.TreatmentActivity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectFirma.Web.Controllers
{
    public class TreatmentActivityController : FirmaBaseController
    {
        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult EditTreatmentActivity(TreatmentActivityPrimaryKey treatmentActivityPrimaryKey)
        {
            var treatmentActivity = treatmentActivityPrimaryKey.EntityObject;

            var viewModel = new EditTreatmentActivityViewModel(treatmentActivity);
            return ViewEditTreatmentActivity(treatmentActivity, viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditTreatmentActivity(TreatmentActivityPrimaryKey treatmentActivityPrimaryKey, EditTreatmentActivityViewModel viewModel)
        {
            var treatmentActivity = treatmentActivityPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditTreatmentActivity(treatmentActivity, viewModel);
            }
            return UpdateTreatmentActivity(viewModel, treatmentActivity);
        }

        private static ActionResult UpdateTreatmentActivity(
            EditTreatmentActivityViewModel viewModel, TreatmentActivity treatmentActivity)
        {
            viewModel.UpdateModel(treatmentActivity, treatmentActivity.Project);

            return new ModalDialogFormJsonResult();

        }

        private PartialViewResult ViewEditTreatmentActivity(TreatmentActivity treatmentActivity, EditTreatmentActivityViewModel viewModel)
        {

            var treatmentActivityStatusAsSelectListItems =
                TreatmentActivityStatus.All.ToSelectListWithEmptyFirstRow(v => v.TreatmentActivityStatusID.ToString(),
                    m => m.TreatmentActivityStatusDisplayName);

            var contactsAsSelectListItems =
                treatmentActivity.Project.ProjectPeople.ToSelectListWithEmptyFirstRow(v => v.PersonID.ToString(),
                    d => d.Person.FullNameFirstLastAndOrg);

            var viewData = new EditTreatmentActivityViewData(treatmentActivityStatusAsSelectListItems, contactsAsSelectListItems, CurrentPerson);
            return RazorPartialView<EditTreatmentActivity, EditTreatmentActivityViewData, EditTreatmentActivityViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult NewTreatmentActivity(ProjectPrimaryKey projectPrimaryKey)
        {
            var treatmentActivity = TreatmentActivity.CreateNewBlank(projectPrimaryKey.EntityObject, TreatmentActivityStatus.Planned);

            var viewModel = new EditTreatmentActivityViewModel(treatmentActivity);
            return ViewEditTreatmentActivity(treatmentActivity, viewModel);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewTreatmentActivity(ProjectPrimaryKey projectPrimaryKey, EditTreatmentActivityViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            var treatmentActivity = TreatmentActivity.CreateNewBlank(project, TreatmentActivityStatus.Planned);

            if (!ModelState.IsValid)
            {
                return ViewEditTreatmentActivity(treatmentActivity, viewModel);
            }
            return UpdateTreatmentActivity(viewModel, treatmentActivity);
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult DeleteTreatmentActivity(TreatmentActivityPrimaryKey treatmentActivityPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(treatmentActivityPrimaryKey.PrimaryKeyValue);
            return ViewDeleteTreatmentActivity(treatmentActivityPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteTreatmentActivity(TreatmentActivity treatmentActivity, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this treatment activity?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteTreatmentActivity(TreatmentActivityPrimaryKey treatmentActivityPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentActivity = treatmentActivityPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentActivity(treatmentActivity, viewModel);
            }

            var message = "This treatment activity was successfully deleted.";
            treatmentActivity.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FirmaAdminFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<TreatmentActivity> TreatmentActivityGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var gridSpec = new TreatmentActivityProjectDetailGridSpec(CurrentPerson);
            var treatmentActivities = HttpRequestStorage.DatabaseEntities.TreatmentActivities
                .GetTreatmentActivitiesForProject(projectPrimaryKey.EntityObject).OrderBy(x => x.TreatmentActivityStartDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentActivity>(treatmentActivities, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<TreatmentActivity> IndexGridJsonData()
        {
            var gridSpec = new TreatmentActivityIndexGridSpec(CurrentPerson);
            var treatmentActivities = HttpRequestStorage.DatabaseEntities.TreatmentActivities.OrderBy(x => x.TreatmentActivityStartDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentActivity>(treatmentActivities, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}