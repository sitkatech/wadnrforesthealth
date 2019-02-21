/*-----------------------------------------------------------------------
<copyright file="ProjectController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
        public PartialViewResult NewGrantNote(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new EditGrantNoteViewModel();
            return ViewEditNote(viewModel, EditGrantNoteType.NewNote);
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


        private PartialViewResult ViewEditNote(EditGrantNoteViewModel viewModel, EditGrantNoteType editGrantNoteType)
        {
            var viewData = new EditGrantNoteViewData(editGrantNoteType);
            return RazorPartialView<EditGrantNote, EditGrantNoteViewData, EditGrantNoteViewModel>(viewData, viewModel);
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
            
            var viewModel = new EditGrantViewModel();
            return ViewEdit(viewModel,EditGrantType.NewGrant);
        }

        [HttpPost]
        [GrantCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditGrantViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, EditGrantType.NewGrant);
            }
            var grantStatus = HttpRequestStorage.DatabaseEntities.GrantStatuses.Single(g => g.GrantStatusID == viewModel.GrantStatusID);
            var grantOrganization = HttpRequestStorage.DatabaseEntities.Organizations.Single(g => g.OrganizationID == viewModel.OrganizationID);
            var grant = Grant.CreateNewBlank(grantStatus, grantOrganization);
            viewModel.UpdateModel(grant, CurrentPerson);
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
            var viewData = new Views.Grant.DetailViewData(CurrentPerson, grant, grantNotesViewData);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [GrantsViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullProjectList);
            var viewData = new GrantIndexViewData(CurrentPerson, firmaPage);
            return RazorView<GrantIndex, GrantIndexViewData>(viewData);
        }

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
        /// <summary>
        /// Used to display an empty grantAllocation grid with "no results" when a row in the grant grid containing no current relationship to grantAllocations is selected.
        /// Trying to make clear to user which grants don't have associated grantAllocations yet.
        /// </summary>
        /// <returns>An empty dataset for grid population</returns>
        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<GrantAllocation> GrantAllocationGridWithoutAnyJsonData()
        {
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.Where(ga => ga.Grant.GrantNumber == "").ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
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















    }
}
