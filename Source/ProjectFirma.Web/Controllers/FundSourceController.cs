/*-----------------------------------------------------------------------
<copyright file="GrantController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.TextControls;
using ProjectFirma.Web.Views.GrantAllocation;

namespace ProjectFirma.Web.Controllers
{
    public class FundSourceController : FirmaBaseController
    {
        [HttpGet]
        [GrantDeleteFeature]
        public PartialViewResult DeleteGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrant(grantPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrant(FundSource fundSource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.Grant.GetFieldDefinitionLabel()} '{fundSource.GrantTitle}'?";
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
            grant.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
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
            SetMessageForDisplay($"{FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{grant.FundSourceName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditGrantViewModel viewModel, EditGrantType editGrantType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var grantStatuses = FundSourceStatus.All;
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
            return ViewNew(viewModel, EditGrantType.NewGrant);
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
            var grantStatus = FundSourceStatus.All.Single(g => g.GrantStatusID == viewModel.GrantStatusID);
            var grantOrganization = HttpRequestStorage.DatabaseEntities.Organizations.Single(g => g.OrganizationID == viewModel.OrganizationID);
            var grant = FundSource.CreateNewBlank(grantStatus, grantOrganization);
            viewModel.UpdateModel(grant, CurrentPerson);
            SetMessageForDisplay($"{FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{grant.FundSourceName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(NewGrantViewModel viewModel, EditGrantType editGrantType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var grantStatuses = FundSourceStatus.All;
            var grantTypes = HttpRequestStorage.DatabaseEntities.GrantTypes;

            var viewData = new NewGrantViewData(editGrantType,
                organizations,
                grantStatuses,
                grantTypes
            );
            return RazorPartialView<NewGrant, NewGrantViewData, NewGrantViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [GrantCreateFeature]
        public PartialViewResult Duplicate(GrantPrimaryKey grantPrimaryKey)
        {
            var grantToDuplicate = grantPrimaryKey.EntityObject;
            Check.EnsureNotNull(grantToDuplicate);

            //get the grant allocations
            List<FundSourceAllocation> grantAllocations = grantToDuplicate.GrantAllocations.ToList();

            var viewModel = new DuplicateGrantViewModel(grantToDuplicate);
            return DuplicateGrantViewEdit(viewModel, grantToDuplicate, grantAllocations);
        }

        [HttpPost]
        [GrantCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Duplicate(GrantPrimaryKey grantPrimaryKey, DuplicateGrantViewModel viewModel)
        {
            var originalGrant = grantPrimaryKey.EntityObject;
            Check.EnsureNotNull(originalGrant);

            if (!ModelState.IsValid)
            {
                return DuplicateGrantViewEdit(viewModel, originalGrant, originalGrant.GrantAllocations.ToList());
            }

            var grantStatus = FundSourceStatus.All.Single(gs => gs.GrantStatusID == viewModel.GrantStatusID);
            var organization = originalGrant.Organization;
            var newGrant = FundSource.CreateNewBlank(grantStatus, organization);
            viewModel.UpdateModel(newGrant);
            newGrant.CFDANumber = originalGrant.CFDANumber;
            newGrant.FundSourceTypeID = originalGrant.FundSourceTypeID;

            if (viewModel.GrantAllocationsToDuplicate != null && viewModel.GrantAllocationsToDuplicate.Any())
            {
                foreach (var allocationID in viewModel.GrantAllocationsToDuplicate)
                {
                    var allocationToCopy =
                        HttpRequestStorage.DatabaseEntities.GrantAllocations.Single(ga =>
                            ga.GrantAllocationID == allocationID);
                    var newAllocation = FundSourceAllocation.CreateNewBlank(newGrant);
                    newAllocation.GrantAllocationName = allocationToCopy.GrantAllocationName;
                    newAllocation.StartDate = allocationToCopy.StartDate;
                    newAllocation.EndDate = allocationToCopy.EndDate;

                    // 10/7/20 TK - not sure we wanna copy these but going for it anyways
                    newAllocation.FederalFundCodeID = allocationToCopy.FederalFundCodeID;
                    newAllocation.OrganizationID = allocationToCopy.OrganizationID;
                    newAllocation.DNRUplandRegionID = allocationToCopy.DNRUplandRegionID;
                    newAllocation.DivisionID = allocationToCopy.DivisionID;
                    newAllocation.GrantManagerID = allocationToCopy.GrantManagerID;

                    // 10/7/20 TK - make sure we setup the budgetLineItems for the new allocation
                    newAllocation.CreateAllGrantAllocationBudgetLineItemsByCostType();
                }
            }

            //need to save changes here, because otherwise the MessageForDisplay will link to an item with a negative ID, causing errors
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"{FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{UrlTemplate.MakeHrefString(newGrant.GetDetailUrl(), newGrant.FundSourceName)}\" has been created.");
            return new ModalDialogFormJsonResult();
            //return RedirectToAction(new SitkaRoute<GrantController>(gc => gc.GrantDetail(newGrant.GrantID)));
        }

        private PartialViewResult DuplicateGrantViewEdit(DuplicateGrantViewModel viewModel, FundSource fundSourceToDuplicate, List<FundSourceAllocation> grantAllocations)
        {
            var grantStatuses = FundSourceStatus.All;
            
            var viewData = new DuplicateGrantViewData(grantStatuses, fundSourceToDuplicate, grantAllocations);
            return RazorPartialView<DuplicateGrant, DuplicateGrantViewData, DuplicateGrantViewModel>(viewData, viewModel);
        }


        #region FileResources

        [HttpGet]
        [GrantEditAsAdminFeature]
        public PartialViewResult NewGrantFiles(GrantPrimaryKey grantPrimaryKey)
        {
            Check.EnsureNotNull(grantPrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewGrantFiles(viewModel);
        }

        [HttpPost]
        [GrantEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantFiles(GrantPrimaryKey grantPrimaryKey, NewFileViewModel viewModel)
        {
            var grant = grantPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewGrantFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(grant, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{grant.FundSourceName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewGrantFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData(FieldDefinition.Grant.GetFieldDefinitionLabel());
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantManageFileResourceAsAdminFeature]
        public PartialViewResult EditGrantFile(GrantFileResourcePrimaryKey grantFileResourcePrimaryKey)
        {
            var fileResource = grantFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditGrantFile(viewModel);
        }

        [HttpPost]
        [GrantManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantFile(GrantFileResourcePrimaryKey grantFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = grantFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditGrantFile(viewModel);
            }

            viewModel.UpdateModel(fileResource);
            SetMessageForDisplay($"Successfully updated file \"{fileResource.DisplayName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditGrantFile(EditFileResourceViewModel viewModel)
        {
            var viewData = new EditFileResourceViewData();
            return RazorPartialView<EditFileResource, EditFileResourceViewData, EditFileResourceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [GrantManageFileResourceAsAdminFeature]
        public PartialViewResult DeleteGrantFile(GrantFileResourcePrimaryKey grantFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(grantFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantFile(grantFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [GrantManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantFile(GrantFileResourcePrimaryKey grantFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantFileResource = grantFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantFile(grantFileResource, viewModel);
            }

            var message = $"{FieldDefinition.Grant.GetFieldDefinitionLabel()} file \"{grantFileResource.DisplayName}\" created on '{grantFileResource.FileResource.CreateDate}' by '{grantFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            grantFileResource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteGrantFile(FundSourceFileResource fundSourceFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{fundSourceFileResource.DisplayName}\" file created on '{fundSourceFileResource.FileResource.CreateDate}' by '{fundSourceFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion

        [GrantsViewFeature]
        public ViewResult GrantDetail(GrantPrimaryKey grantPrimaryKey)
        {
            var grant = grantPrimaryKey.EntityObject;
            var userHasEditGrantPermissions = new GrantEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var grantNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grant.GrantNotes)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewGrantNote(grantPrimaryKey)),
                grant.FundSourceName,
                userHasEditGrantPermissions);

            var internalGrantNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grant.GrantNoteInternals)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewGrantNoteInternal(grantPrimaryKey)),
                grant.FundSourceName,
                userHasEditGrantPermissions);
            var viewData = new Views.Grant.GrantDetailViewData(CurrentPerson, grant, grantNotesViewData, internalGrantNotesViewData);
            return RazorView<GrantDetail, GrantDetailViewData>(viewData);
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

        private ExcelResult GrantsExcelDownloadImpl(List<FundSource> grants, List<FundSourceAllocation> grantAllocations, string workbookTitle)
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
        public GridJsonNetJObjectResult<FundSource> GrantGridJsonData()
        {
            var gridSpec = new GrantGridSpec(CurrentPerson);
            // They want the most current grants on top it seems, and sorting by Grant Number might be good enough -- SLG 7/2/2020
            var grants = HttpRequestStorage.DatabaseEntities.Grants.OrderByDescending(g => g.FundSourceNumber).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSource>(grants, gridSpec);
            return gridJsonNetJObjectResult;
        }

        // Move these to their relevant controllers instead??

        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> AllGrantAllocationGridJsonData()
        {
            // Create button is irrelevant to this data-only usage
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, null);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> GrantAllocationGridJsonDataByGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var relevantGrant = grantPrimaryKey.EntityObject;
            // Create button is irrelevant to this data-only usage
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, relevantGrant);
            var grantAllocations = relevantGrant.GrantAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }


        /// <summary>
        /// Used to display an empty grantAllocation grid with "no results" when a row in the grant grid containing no current relationship to grantAllocations is selected.
        /// Trying to make clear to user which grants don't have associated grantAllocations yet.
        /// </summary>
        /// <returns>An empty dataset for grid population</returns>
        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> GrantAllocationGridWithoutAnyJsonData()
        {
            // Create button is irrelevant to this data-only usage
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, null);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.Where(ga => ga.FundSource.FundSourceNumber == "").ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(grantAllocations, gridSpec);
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
            var grantStatuses = FundSourceStatus.All.ToList();
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
            var grantNoteInternal = FundSourceNoteInternal.CreateNewBlank(grant, CurrentPerson);
            viewModel.UpdateModel(grantNoteInternal, CurrentPerson, EditGrantNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantNoteInternals.Add(grantNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} has been created.");
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
            var grantNote = FundSourceNote.CreateNewBlank(grant, CurrentPerson);
            viewModel.UpdateModel(grantNote, CurrentPerson, EditGrantNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantNotes.Add(grantNote);
            SetMessageForDisplay($"{FieldDefinition.GrantNote.GetFieldDefinitionLabel()} has been created.");
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
            SetMessageForDisplay($"{FieldDefinition.GrantNote.GetFieldDefinitionLabel()} has been updated.");
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
            SetMessageForDisplay($"{FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} has been updated.");
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

        private PartialViewResult ViewDeleteGrantNoteInternal(FundSourceNoteInternal fundSourceNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} created on '{fundSourceNoteInternal.CreatedDate}' by '{fundSourceNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
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

        private PartialViewResult ViewDeleteGrantNote(FundSourceNote fundSourceNote, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantNote.GetFieldDefinitionLabel()} created on '{fundSourceNote.CreatedDate}' by '{fundSourceNote.CreatedByPerson.FullNameFirstLast}'?";
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


        [GrantsViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationBudgetLineItemForGrid> GrantAllocationBudgetLineItemGridJsonDataByGrant(GrantPrimaryKey grantPrimaryKey)
        {
            var relevantGrant = grantPrimaryKey.EntityObject;
            // Create button is irrelevant to this data-only usage
            var gridSpec = new GrantAllocationBudgetLineItemGridSpec();
            var grantAllocations = relevantGrant.GrantAllocations.ToList();
            var budgetLineItems = grantAllocations.Select(ga => new GrantAllocationBudgetLineItemForGrid(ga)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationBudgetLineItemForGrid>(budgetLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFeature]
        public GridJsonNetJObjectResult<Agreement> GrantAgreementGridJsonData(GrantPrimaryKey grantPrimaryKey)
        {
            // 2/4/22 TK - need to walk "grant -> grant allocation -> AgreementGrantAllocation -> Agreement"
            var grant = grantPrimaryKey.EntityObject;
            var grantAllocations = grant.GrantAllocations;
            var agreementGrantAllocations = grantAllocations.SelectMany(x => x.AgreementGrantAllocations);
            var agreements = agreementGrantAllocations.Select(x => x.Agreement).ToList();

            var gridSpec = new GrantAgreementGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Agreement>(agreements, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFeature]
        public GridJsonNetJObjectResult<ProjectGrantAllocationRequest> ProjectGrantAllocationRequestsByGrantGridJsonData(GrantPrimaryKey grantPrimaryKey)
        {
            var grant = grantPrimaryKey.EntityObject;
            var grantAllocations = grant.GrantAllocations;
            var projectGrantAllocationRequests = grantAllocations.SelectMany(x => x.ProjectGrantAllocationRequests).ToList();
            var gridSpec = new ProjectGrantAllocationRequestsByGrantGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectGrantAllocationRequest>(projectGrantAllocationRequests, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
