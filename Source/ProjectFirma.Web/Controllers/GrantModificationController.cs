/*-----------------------------------------------------------------------
<copyright file="GrantModificationController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.GrantModification;
using ProjectFirma.Web.Views.Shared;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Controllers
{
    public class GrantModificationController : FirmaBaseController
    {
        [HttpGet]
        [GrantModificationDeleteFeature]
        public PartialViewResult DeleteGrantModification(GrantModificationPrimaryKey grantModificationPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantModificationPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantModification(grantModificationPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantModification(GrantModification grantModification, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantModification.GetFieldDefinitionLabel()} '{grantModification.GrantModificationName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantModificationDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantModification(GrantModificationPrimaryKey grantModificationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantModification = grantModificationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantModification(grantModification, viewModel);
            }

            var message = $"{FieldDefinition.GrantModification.GetFieldDefinitionLabel()} \"{grantModification.GrantModificationName}\" successfully deleted.";
            grantModification.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
        

        [HttpGet]
        [GrantModificationEditAsAdminFeature]
        public PartialViewResult EditGrantModification(GrantModificationPrimaryKey grantModificationPrimaryKey)
        {
            var grantModification = grantModificationPrimaryKey.EntityObject;
            var viewModel = new EditGrantModificationViewModel(grantModification);
            return ViewEditGrantModification(viewModel, EditGrantModificationType.Existing);
        }

        [HttpPost]
        [GrantModificationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantModification(GrantModificationPrimaryKey grantModificationPrimaryKey, EditGrantModificationViewModel viewModel)
        {
            var grantModification = grantModificationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditGrantModification(viewModel, EditGrantModificationType.Existing);
            }

            var allGrantModificationGrantModificationPurposes = HttpRequestStorage.DatabaseEntities.GrantModificationGrantModificationPurposes.ToList();
            viewModel.UpdateModel(grantModification, CurrentPerson, allGrantModificationGrantModificationPurposes);
            SetMessageForDisplay($"{FieldDefinition.GrantModification.GetFieldDefinitionLabel()} \"{grantModification.GrantModificationName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditGrantModification(EditGrantModificationViewModel viewModel, EditGrantModificationType editGrantModificationType)
        {
            var grantModificationStatuses = HttpRequestStorage.DatabaseEntities.GrantModificationStatuses;
            var grantModificationPurposes = HttpRequestStorage.DatabaseEntities.GrantModificationPurposes;
            
            var viewData = new EditGrantModificationViewData(grantModificationStatuses, grantModificationPurposes, editGrantModificationType);
            return RazorPartialView<EditGrantModification, EditGrantModificationViewData, EditGrantModificationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantModificationCreateFeature]
        public PartialViewResult NewGrantModificationForAGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new EditGrantModificationViewModel(grantPrimaryKey.EntityObject);
            return ViewEditGrantModification(viewModel, EditGrantModificationType.New);
        }

        [HttpPost]
        [GrantModificationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantModificationForAGrant(GrantPrimaryKey grantPrimaryKey, EditGrantModificationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditGrantModification(viewModel, EditGrantModificationType.New);
            }
            var grantModificationStatus = HttpRequestStorage.DatabaseEntities.GrantModificationStatuses.Single(g => g.GrantModificationStatusID == viewModel.GrantModificationStatusID);
            var grant = HttpRequestStorage.DatabaseEntities.Grants.FirstOrDefault(x => x.GrantID == viewModel.GrantID);
            var grantModification = GrantModification.CreateNewBlank(grant, grantModificationStatus);
            var allGrantModificationGrantModificationPurposes = HttpRequestStorage.DatabaseEntities.GrantModificationGrantModificationPurposes.ToList();
            viewModel.UpdateModel(grantModification, CurrentPerson, allGrantModificationGrantModificationPurposes);
            SetMessageForDisplay($"{FieldDefinition.GrantModification.GetFieldDefinitionLabel()} \"{grantModification.GrantModificationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantModificationEditAsAdminFeature]
        public PartialViewResult NewGrantModificationFiles(GrantModificationPrimaryKey grantModificationPrimaryKey)
        {
            Check.EnsureNotNull(grantModificationPrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewGrantModificationFiles(viewModel);
        }

        [HttpPost]
        [GrantModificationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantModificationFiles(GrantModificationPrimaryKey grantModificationPrimaryKey, NewFileViewModel viewModel)
        {
            var grantModification = grantModificationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewGrantModificationFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(grantModification, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.GrantModification.GetFieldDefinitionLabel()} \"{grantModification.GrantModificationName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewGrantModificationFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData();
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantModificationDeleteFileAsAdminFeature]
        public PartialViewResult DeleteGrantModificationFile(GrantModificationFileResourcePrimaryKey grantModificationFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantModificationFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantModificationFile(grantModificationFileResourcePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantModificationFile(GrantModificationFileResource grantModificationFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{grantModificationFileResource.DisplayName}\" file created on '{grantModificationFileResource.FileResource.CreateDate}' by '{grantModificationFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantModificationDeleteFileAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantModificationFile(GrantModificationFileResourcePrimaryKey grantModificationFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantModificationFileResource = grantModificationFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantModificationFile(grantModificationFileResource, viewModel);
            }

            var message = $"{FieldDefinition.GrantModification.GetFieldDefinitionLabel()} file \"{grantModificationFileResource.DisplayName}\" created on '{grantModificationFileResource.FileResource.CreateDate}' by '{grantModificationFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            grantModificationFileResource.Delete(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [GrantModificationViewFeature]
        public ViewResult GrantModificationDetail(GrantModificationPrimaryKey grantModificationPrimaryKey)
        {
            var grantModification = grantModificationPrimaryKey.EntityObject;
            var userHasEditGrantModificationPermissions = new GrantModificationEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);


            var internalGrantModificationNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grantModification.GrantModificationNoteInternals)),
                SitkaRoute<GrantModificationController>.BuildUrlFromExpression(x => x.NewGrantModificationNoteInternal(grantModificationPrimaryKey)),
                grantModification.GrantModificationName,
                userHasEditGrantModificationPermissions);

            var viewData = new GrantModificationDetailViewData(CurrentPerson, grantModification, internalGrantModificationNotesViewData);
            return RazorView<GrantModificationDetail, GrantModificationDetailViewData>(viewData);
        }





        #region "GrantModification Modification Note Internal"


        [HttpGet]
        [GrantModificationEditAsAdminFeature]
        public PartialViewResult NewGrantModificationNoteInternal(GrantModificationPrimaryKey grantModificationPrimaryKey)
        {
            var viewModel = new EditGrantModificationNoteInternalViewModel();
            return ViewEditNoteInternal(viewModel, EditGrantModificationNoteInternalType.NewNote);
        }

        [HttpPost]
        [GrantModificationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantModificationNoteInternal(GrantModificationPrimaryKey grantModificationPrimaryKey, EditGrantModificationNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditGrantModificationNoteInternalType.NewNote);
            }
            var grantModification = grantModificationPrimaryKey.EntityObject;
            var grantModificationNoteInternal = GrantModificationNoteInternal.CreateNewBlank(grantModification, CurrentPerson);
            viewModel.UpdateModel(grantModificationNoteInternal, CurrentPerson, EditGrantModificationNoteInternalType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantModificationNoteInternals.Add(grantModificationNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.GrantModificationNoteInternal.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditNoteInternal(EditGrantModificationNoteInternalViewModel viewModel, EditGrantModificationNoteInternalType editGrantModificationNoteInternalType)
        {
            var viewData = new EditGrantModificationNoteInternalViewData(editGrantModificationNoteInternalType);
            return RazorPartialView<EditGrantModificationNoteInternal, EditGrantModificationNoteInternalViewData, EditGrantModificationNoteInternalViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantModificationNoteInternalEditAsAdminFeature]
        public PartialViewResult EditGrantModificationNoteInternal(GrantModificationNoteInternalPrimaryKey grantModificationNoteInternalPrimaryKey)
        {
            var grantModificationNoteInternal = grantModificationNoteInternalPrimaryKey.EntityObject;
            var viewModel = new EditGrantModificationNoteInternalViewModel(grantModificationNoteInternal);
            return ViewEditNoteInternal(viewModel, EditGrantModificationNoteInternalType.ExistingGrantModificationNoteInternal);
        }

        [HttpPost]
        [GrantModificationNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantModificationNoteInternal(GrantModificationNoteInternalPrimaryKey grantModificationNoteInternalPrimaryKey, EditGrantModificationNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditGrantModificationNoteInternalType.ExistingGrantModificationNoteInternal);
            }

            var grantModificationNoteInternal = grantModificationNoteInternalPrimaryKey.EntityObject;
            viewModel.UpdateModel(grantModificationNoteInternal, CurrentPerson, EditGrantModificationNoteInternalType.ExistingGrantModificationNoteInternal);
            HttpRequestStorage.DatabaseEntities.GrantModificationNoteInternals.AddOrUpdate(grantModificationNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.GrantModificationNoteInternal.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }



        [HttpGet]
        [GrantModificationNoteInternalEditAsAdminFeature]
        public PartialViewResult DeleteGrantModificationNoteInternal(GrantModificationNoteInternalPrimaryKey grantModificationNoteInternalPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantModificationNoteInternalPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantModificationNoteInternal(grantModificationNoteInternalPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantModificationNoteInternal(GrantModificationNoteInternal grantModificationNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantModificationNoteInternal.GetFieldDefinitionLabel()} created on '{grantModificationNoteInternal.CreatedDate}' by '{grantModificationNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantModificationNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantModificationNoteInternal(GrantModificationNoteInternalPrimaryKey grantModificationNoteInternalPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantModificationNoteInternal = grantModificationNoteInternalPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantModificationNoteInternal(grantModificationNoteInternal, viewModel);
            }

            var message = $"{FieldDefinition.GrantModificationNoteInternal.GetFieldDefinitionLabel()} created on '{grantModificationNoteInternal.CreatedDate}' by '{grantModificationNoteInternal.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            grantModificationNoteInternal.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        #endregion














    }
}
