﻿using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Program;
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
            return ConfirmBlockListProjectAction(viewModel, $"Are you sure you want to add the project '{project.ProjectName}' to the import block list?");
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BlockListProject(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ConfirmBlockListProjectAction(viewModel, $"ModelState is invalid. Refresh the page and try again.");
            }

            foreach (var projectProgram in project.ProjectPrograms)
            {
                var projectImportBlockList = new ProjectImportBlockList(projectProgram.Program)
                {
                    ProgramID = projectProgram.ProgramID,
                    ProjectGisIdentifier = project.ProjectGisIdentifier,
                    ProjectName = project.ProjectName,
                    ProjectID = project.ProjectID,
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