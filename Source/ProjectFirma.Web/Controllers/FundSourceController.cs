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
        [FundSourceDeleteFeature]
        public PartialViewResult DeleteGrant(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrant(fundSourcePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrant(FundSource fundSource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.Grant.GetFieldDefinitionLabel()} '{fundSource.GrantTitle}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrant(FundSourcePrimaryKey fundSourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grant = fundSourcePrimaryKey.EntityObject;
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
        [FundSourceEditAsAdminFeature]
        public PartialViewResult Edit(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var grant = fundSourcePrimaryKey.EntityObject;
            var viewModel = new EditGrantViewModel(grant);
            return ViewEdit(viewModel,  EditGrantType.ExistingGrant);
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FundSourcePrimaryKey fundSourcePrimaryKey, EditGrantViewModel viewModel)
        {
            var grant = fundSourcePrimaryKey.EntityObject;
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
            var grantTypes = HttpRequestStorage.DatabaseEntities.FundSourceTypes;
            
            var viewData = new EditGrantViewData(editGrantType,
                organizations, 
                grantStatuses,
                grantTypes
            );
            return RazorPartialView<EditGrant, EditGrantViewData, EditGrantViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new NewGrantViewModel();
            return ViewNew(viewModel, EditGrantType.NewGrant);
        }

        [HttpPost]
        [FundSourceCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(NewGrantViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, EditGrantType.NewGrant);
            }
            var grantStatus = FundSourceStatus.All.Single(g => g.FundSourceStatusID == viewModel.GrantStatusID);
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
            var grantTypes = HttpRequestStorage.DatabaseEntities.FundSourceTypes;

            var viewData = new NewGrantViewData(editGrantType,
                organizations,
                grantStatuses,
                grantTypes
            );
            return RazorPartialView<NewGrant, NewGrantViewData, NewGrantViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceCreateFeature]
        public PartialViewResult Duplicate(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var grantToDuplicate = fundSourcePrimaryKey.EntityObject;
            Check.EnsureNotNull(grantToDuplicate);

            //get the grant allocations
            List<FundSourceAllocation> grantAllocations = grantToDuplicate.FundSourceAllocations.ToList();

            var viewModel = new DuplicateGrantViewModel(grantToDuplicate);
            return DuplicateGrantViewEdit(viewModel, grantToDuplicate, grantAllocations);
        }

        [HttpPost]
        [FundSourceCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Duplicate(FundSourcePrimaryKey fundSourcePrimaryKey, DuplicateGrantViewModel viewModel)
        {
            var originalGrant = fundSourcePrimaryKey.EntityObject;
            Check.EnsureNotNull(originalGrant);

            if (!ModelState.IsValid)
            {
                return DuplicateGrantViewEdit(viewModel, originalGrant, originalGrant.FundSourceAllocations.ToList());
            }

            var grantStatus = FundSourceStatus.All.Single(gs => gs.FundSourceStatusID == viewModel.GrantStatusID);
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
                        HttpRequestStorage.DatabaseEntities.FundSourceAllocations.Single(ga =>
                            ga.FundSourceAllocationID == allocationID);
                    var newAllocation = FundSourceAllocation.CreateNewBlank(newGrant);
                    newAllocation.FundSourceAllocationName = allocationToCopy.FundSourceAllocationName;
                    newAllocation.StartDate = allocationToCopy.StartDate;
                    newAllocation.EndDate = allocationToCopy.EndDate;

                    // 10/7/20 TK - not sure we wanna copy these but going for it anyways
                    newAllocation.FederalFundCodeID = allocationToCopy.FederalFundCodeID;
                    newAllocation.OrganizationID = allocationToCopy.OrganizationID;
                    newAllocation.DNRUplandRegionID = allocationToCopy.DNRUplandRegionID;
                    newAllocation.DivisionID = allocationToCopy.DivisionID;
                    newAllocation.FundSourceManagerID = allocationToCopy.FundSourceManagerID;

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
        [FundSourceEditAsAdminFeature]
        public PartialViewResult NewGrantFiles(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            Check.EnsureNotNull(fundSourcePrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewGrantFiles(viewModel);
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantFiles(FundSourcePrimaryKey fundSourcePrimaryKey, NewFileViewModel viewModel)
        {
            var grant = fundSourcePrimaryKey.EntityObject;
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
        [FundSourceManageFileResourceAsAdminFeature]
        public PartialViewResult EditGrantFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey)
        {
            var fileResource = fundSourceFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditGrantFile(viewModel);
        }

        [HttpPost]
        [FundSourceManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = fundSourceFileResourcePrimaryKey.EntityObject;
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
        [FundSourceManageFileResourceAsAdminFeature]
        public PartialViewResult DeleteGrantFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantFile(fundSourceFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [FundSourceManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantFileResource = fundSourceFileResourcePrimaryKey.EntityObject;
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

        [FundSourceViewFeature]
        public ViewResult GrantDetail(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var grant = fundSourcePrimaryKey.EntityObject;
            var userHasEditGrantPermissions = new FundSourceEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var grantNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grant.GrantNotes)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewGrantNote(fundSourcePrimaryKey)),
                grant.FundSourceName,
                userHasEditGrantPermissions);

            var internalGrantNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grant.GrantNoteInternals)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewGrantNoteInternal(fundSourcePrimaryKey)),
                grant.FundSourceName,
                userHasEditGrantPermissions);
            var viewData = new Views.Grant.GrantDetailViewData(CurrentPerson, grant, grantNotesViewData, internalGrantNotesViewData);
            return RazorView<GrantDetail, GrantDetailViewData>(viewData);
        }

        [FundSourceViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullGrantList);
            var viewData = new GrantIndexViewData(CurrentPerson, firmaPage);
            return RazorView<GrantIndex, GrantIndexViewData>(viewData);
        }

        [FundSourceViewFullListFeature]
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
        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSource> GrantGridJsonData()
        {
            var gridSpec = new GrantGridSpec(CurrentPerson);
            // They want the most current grants on top it seems, and sorting by Grant Number might be good enough -- SLG 7/2/2020
            var grants = HttpRequestStorage.DatabaseEntities.Grants.OrderByDescending(g => g.FundSourceNumber).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSource>(grants, gridSpec);
            return gridJsonNetJObjectResult;
        }

        // Move these to their relevant controllers instead??

        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> AllGrantAllocationGridJsonData()
        {
            // Create button is irrelevant to this data-only usage
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, null);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> GrantAllocationGridJsonDataByGrant(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var relevantGrant = fundSourcePrimaryKey.EntityObject;
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
        [FundSourceViewFullListFeature]
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

        [FundSourceViewJsonApiFeature]
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
        [FundSourceViewJsonApiFeature]
        public JsonNetJArrayResult GrantStatusJsonApi()
        {
            var grantStatuses = FundSourceStatus.All.ToList();
            var jsonApiGrantStatuses = GrantStatusApiJson.MakeGrantStatusApiJsonsFromGrantStatuses(grantStatuses);
            return new JsonNetJArrayResult(jsonApiGrantStatuses);
        }


        #endregion


        #region "Grant Note including internal"
        [HttpGet]
        [FundSourceEditAsAdminFeature]
        public PartialViewResult NewGrantNote(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var viewModel = new EditGrantNoteViewModel();
            return ViewEditNote(viewModel, EditGrantNoteType.NewNote);
        }

        [HttpGet]
        [FundSourceEditAsAdminFeature]
        public PartialViewResult NewGrantNoteInternal(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var viewModel = new EditGrantNoteInternalViewModel();
            return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.NewNote);
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantNoteInternal(FundSourcePrimaryKey fundSourcePrimaryKey, EditGrantNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.NewNote);
            }
            var grant = fundSourcePrimaryKey.EntityObject;
            var grantNoteInternal = FundSourceNoteInternal.CreateNewBlank(grant, CurrentPerson);
            viewModel.UpdateModel(grantNoteInternal, CurrentPerson, EditGrantNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantNoteInternals.Add(grantNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantNote(FundSourcePrimaryKey fundSourcePrimaryKey, EditGrantNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditGrantNoteType.NewNote);
            }
            var grant = fundSourcePrimaryKey.EntityObject;
            var grantNote = FundSourceNote.CreateNewBlank(grant, CurrentPerson);
            viewModel.UpdateModel(grantNote, CurrentPerson, EditGrantNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNotes.Add(grantNote);
            SetMessageForDisplay($"{FieldDefinition.GrantNote.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceNoteEditAsAdminFeature]
        public PartialViewResult EditGrantNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey)
        {
            var grantNote = fundSourceNotePrimaryKey.EntityObject;
            var viewModel = new EditGrantNoteViewModel(grantNote);
            return ViewEditNote(viewModel, EditGrantNoteType.ExistingNote);
        }

        [HttpPost]
        [FundSourceNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey, EditGrantNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditGrantNoteType.ExistingNote);
            }

            var grantNote = fundSourceNotePrimaryKey.EntityObject;
            viewModel.UpdateModel(grantNote, CurrentPerson, EditGrantNoteType.ExistingNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNotes.AddOrUpdate(grantNote);
            SetMessageForDisplay($"{FieldDefinition.GrantNote.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNoteInternal(EditGrantNoteInternalViewModel viewModel, EditGrantNoteInternalType editGrantNoteInternalType)
        {
            var viewData = new EditGrantNoteInternalViewData(editGrantNoteInternalType);
            return RazorPartialView<EditGrantNoteInternal, EditGrantNoteInternalViewData, EditGrantNoteInternalViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceNoteInternalEditAsAdminFeature]
        public PartialViewResult EditGrantNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey)
        {
            var grantNoteInternal = fundSourceNoteInternalPrimaryKey.EntityObject;
            var viewModel = new EditGrantNoteInternalViewModel(grantNoteInternal);
            return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.ExistingNote);
        }

        [HttpPost]
        [FundSourceNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey, EditGrantNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditGrantNoteInternalType.ExistingNote);
            }

            var grantNoteInternal = fundSourceNoteInternalPrimaryKey.EntityObject;
            viewModel.UpdateModel(grantNoteInternal, CurrentPerson, EditGrantNoteType.ExistingNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNoteInternals.AddOrUpdate(grantNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNote(EditGrantNoteViewModel viewModel, EditGrantNoteType editGrantNoteType)
        {
            var viewData = new EditGrantNoteViewData(editGrantNoteType);
            return RazorPartialView<EditGrantNote, EditGrantNoteViewData, EditGrantNoteViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceNoteInternalEditAsAdminFeature]
        public PartialViewResult DeleteGrantNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceNoteInternalPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantNoteInternal(fundSourceNoteInternalPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantNoteInternal(FundSourceNoteInternal fundSourceNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()} created on '{fundSourceNoteInternal.CreatedDate}' by '{fundSourceNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantNoteInternal = fundSourceNoteInternalPrimaryKey.EntityObject;
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
        [FundSourceNoteEditAsAdminFeature]
        public PartialViewResult DeleteGrantNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceNotePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantNote(fundSourceNotePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantNote(FundSourceNote fundSourceNote, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantNote.GetFieldDefinitionLabel()} created on '{fundSourceNote.CreatedDate}' by '{fundSourceNote.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }



        [HttpPost]
        [FundSourceNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantNote = fundSourceNotePrimaryKey.EntityObject;
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


        [FundSourceViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationBudgetLineItemForGrid> GrantAllocationBudgetLineItemGridJsonDataByGrant(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var relevantGrant = fundSourcePrimaryKey.EntityObject;
            // Create button is irrelevant to this data-only usage
            var gridSpec = new GrantAllocationBudgetLineItemGridSpec();
            var grantAllocations = relevantGrant.FundSourceAllocations.ToList();
            var budgetLineItems = grantAllocations.Select(ga => new GrantAllocationBudgetLineItemForGrid(ga)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationBudgetLineItemForGrid>(budgetLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FundSourceViewFeature]
        public GridJsonNetJObjectResult<Agreement> GrantAgreementGridJsonData(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            // 2/4/22 TK - need to walk "grant -> grant allocation -> AgreementGrantAllocation -> Agreement"
            var grant = fundSourcePrimaryKey.EntityObject;
            var grantAllocations = grant.FundSourceAllocations;
            var agreementGrantAllocations = grantAllocations.SelectMany(x => x.AgreementFundSourceAllocations);
            var agreements = agreementGrantAllocations.Select(x => x.Agreement).ToList();

            var gridSpec = new GrantAgreementGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Agreement>(agreements, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FundSourceViewFeature]
        public GridJsonNetJObjectResult<ProjectFundSourceAllocationRequest> ProjectGrantAllocationRequestsByGrantGridJsonData(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var grant = fundSourcePrimaryKey.EntityObject;
            var grantAllocations = grant.FundSourceAllocations;
            var projectGrantAllocationRequests = grantAllocations.SelectMany(x => x.ProjectFundSourceAllocationRequests).ToList();
            var gridSpec = new ProjectGrantAllocationRequestsByGrantGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectFundSourceAllocationRequest>(projectGrantAllocationRequests, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
