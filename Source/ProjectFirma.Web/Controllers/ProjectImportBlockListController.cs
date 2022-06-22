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
    public class ProjectImportBlockListController : FirmaBaseController
    {
        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult BlockListProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectPrimaryKey.PrimaryKeyValue);
            return ViewBlockListProject(projectPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewBlockListProject(Project project, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to add this project '{project.ProjectName}' to the block list?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BlockListProject(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewBlockListProject(project, viewModel);
            }

            foreach (var projectProgram in project.ProjectPrograms)
            {
                var projectImportBlockList = new ProjectImportBlockList(projectProgram.Program)
                {
                    ProjectGisIdentifier = project.ProjectGisIdentifier,
                    ProjectName = project.ProjectName
                };

                HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists.Add(projectImportBlockList);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

    }
}