using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.ProjectImportBlockList;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectImportBlockListController : FirmaBaseController
    {
        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult BlockListProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            //var viewModel = new ConfirmDialogFormViewModel(projectPrimaryKey.PrimaryKeyValue);
            //return ConfirmBlockListProjectAction(viewModel, $"Are you sure you want to add the project '{project.ProjectName}' to the import block list?");

            var viewModel = new EditProjectImportBlockListViewModel()
            {
                ProgramID = -1,
                ProjectID = project.ProjectID,
                ProjectGisIdentifier = project.ProjectGisIdentifier,
                ProjectName = project.ProjectName
            };
            var viewData = new EditProjectImportBlockListViewData(CurrentPerson, null);
            return RazorPartialView<EditProjectImportBlockList, EditProjectImportBlockListViewData, EditProjectImportBlockListViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BlockListProject(ProjectPrimaryKey projectPrimaryKey, EditProjectImportBlockListViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;

            if (string.IsNullOrEmpty(viewModel.ProjectName) && string.IsNullOrEmpty(viewModel.ProjectGisIdentifier))
            {
                var validationMessage = "You must provide Project Name and/or Project GIS Identifier.";
                this.ModelState.AddModelError("Required", validationMessage);
            }

            if (!ModelState.IsValid)
            {
                return BlockListProject(project);
            }

            foreach (var projectProgram in project.ProjectPrograms)
            {
                var projectImportBlockList = new ProjectImportBlockList(projectProgram.Program)
                {
                    ProgramID = projectProgram.ProgramID,
                    ProjectGisIdentifier = viewModel.ProjectGisIdentifier,
                    ProjectName = viewModel.ProjectName,
                    ProjectID = viewModel.ProjectID,
                    Notes = viewModel.Notes
                };

                HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists.Add(projectImportBlockList);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult RemoveBlockListProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectPrimaryKey.PrimaryKeyValue);
            return ConfirmBlockListProjectAction(viewModel, $"Are you sure you want to remove the project '{project.ProjectName}' from the import block list?");
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RemoveBlockListProject(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ConfirmBlockListProjectAction(viewModel, $"ModelState is invalid. Refresh the page and try again.");
            }

            foreach (var projectProgram in project.ProjectPrograms)
            {
                var existing = HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists
                    .FirstOrDefault(p => p.ProjectGisIdentifier == project.ProjectGisIdentifier &&
                                         p.ProjectName == project.ProjectName &&
                                         p.ProgramID == projectProgram.Program.ProgramID);

                if (existing != null)
                    HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists.Remove(existing);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult RemoveBlockListEntry(ProjectImportBlockListPrimaryKey blockListPrimaryKey)
        {
            var blockListEntry = blockListPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(blockListPrimaryKey.PrimaryKeyValue);
            return ConfirmBlockListProjectAction(viewModel, $"Are you sure you want to remove the project '{blockListEntry.ProjectName}' ({blockListEntry.ProjectGisIdentifier}) from the import block list?");
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RemoveBlockListEntry(ProjectImportBlockListPrimaryKey blockListPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var blockListEntry = blockListPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ConfirmBlockListProjectAction(viewModel, $"ModelState is invalid. Refresh the page and try again.");
            }

            var existing = HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists
                .FirstOrDefault(p => p.ProjectImportBlockListID == blockListEntry.ProjectImportBlockListID);

            if (existing != null)
                HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists.Remove(existing);

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ConfirmBlockListProjectAction(ConfirmDialogFormViewModel viewModel, string message)
        {
            var viewData = new ConfirmDialogFormViewData(message, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}