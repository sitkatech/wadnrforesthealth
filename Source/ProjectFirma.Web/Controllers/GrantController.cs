/*-----------------------------------------------------------------------
<copyright file="GrantController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Views.Grant;
using ProjectFirma.Web.Views.Shared;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Controllers
{
    public class GrantController : FirmaBaseController
    {
        [HttpGet]
        [GrantDeleteFeature]
        public PartialViewResult DeleteGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrant(grantPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrant(Grant grant, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.Grant.GetFieldDefinitionLabel()} '{grant.GrantTitle}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrant(GrantPrimaryKey grantPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grant = grantPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrant(grant, viewModel);
            }

            var message = $"{FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{grant.GrantTitle}\" successfully deleted.";
            grant.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
        

        [HttpGet]
        [GrantEditAsAdminFeature]
        public PartialViewResult Edit(GrantPrimaryKey grantPrimaryKey)
        {
            var grant = grantPrimaryKey.EntityObject;
            var viewModel = new EditGrantViewModel(grant);
            return ViewEdit(viewModel,  EditGrantType.ExistingGrant);
        }

        [HttpPost]
        [GrantEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(GrantPrimaryKey grantPrimaryKey, EditGrantViewModel viewModel)
        {
            var grant = grantPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel,  EditGrantType.ExistingGrant);
            }
            viewModel.UpdateModel(grant, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditGrantViewModel viewModel, EditGrantType editGrantType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var grantStatuses = HttpRequestStorage.DatabaseEntities.GrantStatuses;
            var grantTypes = HttpRequestStorage.DatabaseEntities.GrantTypes;
            
            var viewData = new EditGrantViewData(editGrantType,
                organizations, 
                grantStatuses,
                grantTypes
            );
            return RazorPartialView<EditGrant, EditGrantViewData, EditGrantViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantCreateFeature]
        public PartialViewResult New()
        {
            
            var viewModel = new NewGrantViewModel();
            return ViewNew(viewModel,EditGrantType.NewGrant);
        }

        [HttpPost]
        [GrantCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(NewGrantViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, EditGrantType.NewGrant);
            }
            var grantStatus = HttpRequestStorage.DatabaseEntities.GrantStatuses.Single(g => g.GrantStatusID == viewModel.GrantStatusID);
            var grantOrganization = HttpRequestStorage.DatabaseEntities.Organizations.Single(g => g.OrganizationID == viewModel.OrganizationID);
            var grant = Grant.CreateNewBlank(grantStatus, grantOrganization);
            viewModel.UpdateModel(grant, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(NewGrantViewModel viewModel, EditGrantType editGrantType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var grantStatuses = HttpRequestStorage.DatabaseEntities.GrantStatuses;
            var grantTypes = HttpRequestStorage.DatabaseEntities.GrantTypes;

            var viewData = new NewGrantViewData(editGrantType,
                organizations,
                grantStatuses,
                grantTypes
            );
            return RazorPartialView<NewGrant, NewGrantViewData, NewGrantViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantEditAsAdminFeature]
        public PartialViewResult NewGrantFiles(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new NewGrantFileViewModel(grantPrimaryKey.EntityObject);
            return ViewNewGrantFiles(viewModel);
        }

        [HttpPost]
        [GrantEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantFiles(GrantPrimaryKey grantPrimaryKey, NewGrantFileViewModel viewModel)
        {
            var grant = grantPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewGrantFiles(new NewGrantFileViewModel());
            }

            viewModel.UpdateModel(grant, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.GrantFileResourceDatas.Count} new files(s) for {FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{grant.GrantName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewGrantFiles(NewGrantFileViewModel viewModel)
        {
            var viewData = new NewGrantFileViewData();
            return RazorPartialView<NewGrantFile, NewGrantFileViewData, NewGrantFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantDeleteFileAsAdminFeature]
        public PartialViewResult DeleteGrantFile(GrantFileResourcePrimaryKey grantFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantFile(grantFileResourcePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantFile(GrantFileResource grantFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{grantFileResource.DisplayName}\" file created on '{grantFileResource.FileResource.CreateDate}' by '{grantFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantDeleteFileAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantFile(GrantFileResourcePrimaryKey grantFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantFileResource = grantFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantFile(grantFileResource, viewModel);
            }

            var message = $"{FieldDefinition.Grant.GetFieldDefinitionLabel()} file \"{grantFileResource.DisplayName}\" created on '{grantFileResource.FileResource.CreateDate}' by '{grantFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            grantFileResource.FileResource.Delete(HttpRequestStorage.DatabaseEntities);
            grantFileResource.Delete(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }


        [GrantsViewFeature]
        public ViewResult GrantDetail(GrantPrimaryKey grantPrimaryKey)
        {
            var grant = grantPrimaryKey.EntityObject;
            var userHasEditGrantPermissions = new GrantEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var grantNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grant.GrantNotes)),
                SitkaRoute<GrantController>.BuildUrlFromExpression(x => x.NewGrantNote(grantPrimaryKey)),
                grant.GrantName,
                userHasEditGrantPermissions);

            var internalGrantNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grant.GrantNoteInternals)),
                SitkaRoute<GrantController>.BuildUrlFromExpression(x => x.NewGrantNoteInternal(grantPrimaryKey)),
                grant.GrantName,
                userHasEditGrantPermissions);
            var viewData = new Views.Grant.DetailViewData(CurrentPerson, grant, grantNotesViewData, internalGrantNotesViewData);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [GrantsViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullGrantList);
            var viewData = new GrantIndexViewData(CurrentPerson, firmaPage);
            return RazorView<GrantIndex, GrantIndexViewData>(viewData);
        }

        [GrantsViewFullListFeature]
        public ExcelResult GrantsExcelDownload()
        {
            var grants = HttpRequestStorage.DatabaseEntities.Grants.ToList();
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var workbookTitle = FieldDefinition.Grant.GetFieldDefinitionLabelPluralized();
            return GrantsExcelDownloadImpl(grants, grantAllocations, workbookTitle);
        }

        private ExcelResult GrantsExcelDownloadImpl(List<Grant> grants, List<GrantAllocation> grantAllocations, string workbookTitle)
        {
            var workSheets = new List<IExcelWorkbookSheetDescriptor>();

            // Grants
            var grantSpec = new GrantExcelSpec();
            var wsGrants = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()}", grantSpec, grants);
            workSheets.Add(wsGrants);

            // Grant Allocations
            var grantAllocationsSpec = new GrantAllocationExcelSpec();
            var wsGrantAllocations = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized()}", grantAllocationsSpec, grantAllocations);
            workSheets.Add(wsGrantAllocations);

            // Overall excel file
            var wbm = new ExcelWorkbookMaker(workSheets);
            var excelWorkbook = wbm.ToXLWorkbook();

            return new ExcelResult(excelWorkbook, workbookTitle);
        }


        #region "Grid Json Object Functions"
        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<Grant> GrantGridJsonData()
        {
            var gridSpec = new GrantGridSpec(CurrentPerson);
            var grants = HttpRequestStorage.DatabaseEntities.Grants.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Grant>(grants, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<GrantAllocation> GrantAllocationGridJsonData()
        {
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<GrantAllocation> GrantAllocationGridJsonDataByGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson);
            var grant = grantPrimaryKey.EntityObject;
            var grantAllocations = grant.GrantModifications.SelectMany(x => x.GrantAllocations).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        /// <summary>
        /// Used to display an empty grantAllocation grid with "no results" when a row in the grant grid containing no current relationship to grantAllocations is selected.
        /// Trying to make clear to user which grants don't have associated grantAllocations yet.
        /// </summary>
        /// <returns>An empty dataset for grid population</returns>
        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<GrantAllocation> GrantAllocationGridWithoutAnyJsonData()
        {
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.Where(ga => ga.GrantModification.Grant.GrantNumber == "").ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<GrantModification> GrantModificationGridJsonDataByGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var grant = grantPrimaryKey.EntityObject;
            var gridSpec = new GrantModificationGridSpec(CurrentPerson, grant, grant.GrantModifications.Any(x => x.GrantModificationFileResourceID.HasValue));
            var grantModifications = grant.GrantModifications.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantModification>(grantModifications, gridSpec);
            return gridJsonNetJObjectResult;
        }
        #endregion


        #region WADNR Grant JSON API

        [GrantsViewJsonApiFeature]
        public JsonNetJArrayResult GrantJsonApi()
        {
            var grants = HttpRequestStorage.DatabaseEntities.Grants.ToList();
            var jsonApiGrants = GrantApiJson.MakeGrantApiJsonsFromGrants(grants, false);
            return new JsonNetJArrayResult(jsonApiGrants);
        }

        /// <summary>
        /// This is probably excessive, but I'm trying to overcompensate for every need Tammy Osborn has. -- SLG
        /// </summary>
        /// <returns></returns>
        [GrantsViewJsonApiFeature]
        public JsonNetJArrayResult GrantStatusJsonApi()
        {
            var grantStatuses = HttpRequestStorage.DatabaseEntities.GrantStatuses.ToList();
            var jsonApiGrantStatuses = GrantStatusApiJson.MakeGrantStatusApiJsonsFromGrantStatuses(grantStatuses);
            return new JsonNetJArrayResult(jsonApiGrantStatuses);
        }


        #endregion


        #region "Grant Note including internal"
        [HttpGet]
        [GrantEditAsAdminFeature]
        public PartialViewResult NewGrantNote(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new EditGrantNoteViewModel();
            return ViewEditNote(viewModel, EditGrantNoteType.NewNote);
        }

        [HttpGet]
        [GrantEditAsAdminFeature]
        public PartialViewResult NewGrantNoteInternal(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new EditGrantNoteInternalViewModel();
            return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.NewNote);
        }

        [HttpPost]
        [GrantEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantNoteInternal(GrantPrimaryKey grantPrimaryKey, EditGrantNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.NewNote);
            }
            var grant = grantPrimaryKey.EntityObject;
            var grantNoteInternal = GrantNoteInternal.CreateNewBlank(grant, CurrentPerson);
            viewModel.UpdateModel(grantNoteInternal, CurrentPerson, EditGrantNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantNoteInternals.Add(grantNoteInternal);
            return new ModalDialogFormJsonResult();
        }

        [HttpPost]
        [GrantEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantNote(GrantPrimaryKey grantPrimaryKey, EditGrantNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditGrantNoteType.NewNote);
            }
            var grant = grantPrimaryKey.EntityObject;
            var grantNote = GrantNote.CreateNewBlank(grant, CurrentPerson);
            viewModel.UpdateModel(grantNote, CurrentPerson, EditGrantNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantNotes.Add(grantNote);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantNoteEditAsAdminFeature]
        public PartialViewResult EditGrantNote(GrantNotePrimaryKey grantNotePrimaryKey)
        {
            var grantNote = grantNotePrimaryKey.EntityObject;
            var viewModel = new EditGrantNoteViewModel(grantNote);
            return ViewEditNote(viewModel, EditGrantNoteType.ExistingNote);
        }

        [HttpPost]
        [GrantNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantNote(GrantNotePrimaryKey grantNotePrimaryKey, EditGrantNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditGrantNoteType.ExistingNote);
            }

            var grantNote = grantNotePrimaryKey.EntityObject;
            viewModel.UpdateModel(grantNote, CurrentPerson, EditGrantNoteType.ExistingNote);
            HttpRequestStorage.DatabaseEntities.GrantNotes.AddOrUpdate(grantNote);
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNoteInternal(EditGrantNoteInternalViewModel viewModel, EditGrantNoteInternalType editGrantNoteInternalType)
        {
            var viewData = new EditGrantNoteInternalViewData(editGrantNoteInternalType);
            return RazorPartialView<EditGrantNoteInternal, EditGrantNoteInternalViewData, EditGrantNoteInternalViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantNoteInternalEditAsAdminFeature]
        public PartialViewResult EditGrantNoteInternal(GrantNoteInternalPrimaryKey grantNoteInternalPrimaryKey)
        {
            var grantNoteInternal = grantNoteInternalPrimaryKey.EntityObject;
            var viewModel = new EditGrantNoteInternalViewModel(grantNoteInternal);
            return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.ExistingNote);
        }

        [HttpPost]
        [GrantNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantNoteInternal(GrantNoteInternalPrimaryKey grantNoteInternalPrimaryKey, EditGrantNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.ExistingNote);
            }

            var grantNoteInternal = grantNoteInternalPrimaryKey.EntityObject;
            viewModel.UpdateModel(grantNoteInternal, CurrentPerson, EditGrantNoteType.ExistingNote);
            HttpRequestStorage.DatabaseEntities.GrantNoteInternals.AddOrUpdate(grantNoteInternal);
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNote(EditGrantNoteViewModel viewModel, EditGrantNoteType editGrantNoteType)
        {
            var viewData = new EditGrantNoteViewData(editGrantNoteType);
            return RazorPartialView<EditGrantNote, EditGrantNoteViewData, EditGrantNoteViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantNoteInternalEditAsAdminFeature]
        public PartialViewResult DeleteGrantNoteInternal(GrantNoteInternalPrimaryKey grantNoteInternalPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantNoteInternalPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantNoteInternal(grantNoteInternalPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantNoteInternal(GrantNoteInternal grantNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} created on '{grantNoteInternal.CreatedDate}' by '{grantNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GrantNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantNoteInternal(GrantNoteInternalPrimaryKey grantNoteInternalPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantNoteInternal = grantNoteInternalPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantNoteInternal(grantNoteInternal, viewModel);
            }

            var message = $"{FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} created on '{grantNoteInternal.CreatedDate}' by '{grantNoteInternal.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            grantNoteInternal.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [GrantNoteEditAsAdminFeature]
        public PartialViewResult DeleteGrantNote(GrantNotePrimaryKey grantNotePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantNotePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantNote(grantNotePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantNote(GrantNote grantNote, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantNote.GetFieldDefinitionLabel()} created on '{grantNote.CreatedDate}' by '{grantNote.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }



        [HttpPost]
        [GrantNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantNote(GrantNotePrimaryKey grantNotePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantNote = grantNotePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantNote(grantNote, viewModel);
            }

            var message = $"{FieldDefinition.GrantNote.GetFieldDefinitionLabel()} created on '{grantNote.CreatedDate}' by '{grantNote.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            grantNote.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
        #endregion














    }
}
