using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectImportBlacklistController : FirmaBaseController
    {
        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult BlacklistProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectPrimaryKey.PrimaryKeyValue);
            return ViewBlacklistProject(projectPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewBlacklistProject(Project project, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to blacklist this project '{project.ProjectName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BlacklistProject(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewBlacklistProject(project, viewModel);
            }

            foreach (var projectProgram in project.ProjectPrograms)
            {
                var projectImportBlacklist = new ProjectImportBlacklist(projectProgram.Program)
                {
                    ProjectGisIdentifier = project.ProjectGisIdentifier,
                    ProjectName = project.ProjectName
                };

                HttpRequestStorage.DatabaseEntities.ProjectImportBlacklists.Add(projectImportBlacklist);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

    }
}