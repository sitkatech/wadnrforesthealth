/*-----------------------------------------------------------------------
<copyright file="FundSourceController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.FundSource;
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
using ProjectFirma.Web.Views.FundSourceAllocation;

namespace ProjectFirma.Web.Controllers
{
    public class FundSourceController : FirmaBaseController
    {
        [HttpGet]
        [FundSourceDeleteFeature]
        public PartialViewResult DeleteFundSource(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSource(fundSourcePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteFundSource(FundSource fundSource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.FundSource.GetFieldDefinitionLabel()} '{fundSource.FundSourceTitle}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSource(FundSourcePrimaryKey fundSourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSource = fundSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSource(fundSource, viewModel);
            }

            var message = $"{FieldDefinition.FundSource.GetFieldDefinitionLabel()} \"{fundSource.FundSourceTitle}\" successfully deleted.";
            fundSource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceEditAsAdminFeature]
        public PartialViewResult Edit(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var fundSource = fundSourcePrimaryKey.EntityObject;
            var viewModel = new EditFundSourceViewModel(fundSource);
            return ViewEdit(viewModel,  EditFundSourceType.ExistingFundSource);
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FundSourcePrimaryKey fundSourcePrimaryKey, EditFundSourceViewModel viewModel)
        {
            var fundSource = fundSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel,  EditFundSourceType.ExistingFundSource);
            }
            viewModel.UpdateModel(fundSource, CurrentPerson);
            SetMessageForDisplay($"{FieldDefinition.FundSource.GetFieldDefinitionLabel()} \"{fundSource.FundSourceName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditFundSourceViewModel viewModel, EditFundSourceType editFundSourceType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var fundSourceStatuses = FundSourceStatus.All;
            var fundSourceTypes = HttpRequestStorage.DatabaseEntities.FundSourceTypes;
            
            var viewData = new EditFundSourceViewData(editFundSourceType,
                organizations, 
                fundSourceStatuses,
                fundSourceTypes
            );
            return RazorPartialView<EditFundSource, EditFundSourceViewData, EditFundSourceViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new NewFundSourceViewModel();
            return ViewNew(viewModel, EditFundSourceType.NewFundSource);
        }

        [HttpPost]
        [FundSourceCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(NewFundSourceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, EditFundSourceType.NewFundSource);
            }
            var fundSourceStatus = FundSourceStatus.All.Single(g => g.FundSourceStatusID == viewModel.FundSourceStatusID);
            var fundSourceOrganization = HttpRequestStorage.DatabaseEntities.Organizations.Single(g => g.OrganizationID == viewModel.OrganizationID);
            var fundSource = FundSource.CreateNewBlank(fundSourceStatus, fundSourceOrganization);
            viewModel.UpdateModel(fundSource, CurrentPerson);
            SetMessageForDisplay($"{FieldDefinition.FundSource.GetFieldDefinitionLabel()} \"{fundSource.FundSourceName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNew(NewFundSourceViewModel viewModel, EditFundSourceType editFundSourceType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var fundSourceStatuses = FundSourceStatus.All;
            var fundSourceTypes = HttpRequestStorage.DatabaseEntities.FundSourceTypes;

            var viewData = new NewFundSourceViewData(editFundSourceType,
                organizations,
                fundSourceStatuses,
                fundSourceTypes
            );
            return RazorPartialView<NewFundSource, NewFundSourceViewData, NewFundSourceViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceCreateFeature]
        public PartialViewResult Duplicate(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var fundSourceToDuplicate = fundSourcePrimaryKey.EntityObject;
            Check.EnsureNotNull(fundSourceToDuplicate);

            //get the fundSource allocations
            List<FundSourceAllocation> fundSourceAllocations = fundSourceToDuplicate.FundSourceAllocations.ToList();

            var viewModel = new DuplicateFundSourceViewModel(fundSourceToDuplicate);
            return DuplicateFundSourceViewEdit(viewModel, fundSourceToDuplicate, fundSourceAllocations);
        }

        [HttpPost]
        [FundSourceCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Duplicate(FundSourcePrimaryKey fundSourcePrimaryKey, DuplicateFundSourceViewModel viewModel)
        {
            var originalFundSource = fundSourcePrimaryKey.EntityObject;
            Check.EnsureNotNull(originalFundSource);

            if (!ModelState.IsValid)
            {
                return DuplicateFundSourceViewEdit(viewModel, originalFundSource, originalFundSource.FundSourceAllocations.ToList());
            }

            var fundSourceStatus = FundSourceStatus.All.Single(gs => gs.FundSourceStatusID == viewModel.FundSourceStatusID);
            var organization = originalFundSource.Organization;
            var newFundSource = FundSource.CreateNewBlank(fundSourceStatus, organization);
            viewModel.UpdateModel(newFundSource);
            newFundSource.CFDANumber = originalFundSource.CFDANumber;
            newFundSource.FundSourceTypeID = originalFundSource.FundSourceTypeID;

            if (viewModel.FundSourceAllocationsToDuplicate != null && viewModel.FundSourceAllocationsToDuplicate.Any())
            {
                foreach (var allocationID in viewModel.FundSourceAllocationsToDuplicate)
                {
                    var allocationToCopy =
                        HttpRequestStorage.DatabaseEntities.FundSourceAllocations.Single(ga =>
                            ga.FundSourceAllocationID == allocationID);
                    var newAllocation = FundSourceAllocation.CreateNewBlank(newFundSource);
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
                    newAllocation.CreateAllFundSourceAllocationBudgetLineItemsByCostType();
                }
            }

            //need to save changes here, because otherwise the MessageForDisplay will link to an item with a negative ID, causing errors
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"{FieldDefinition.FundSource.GetFieldDefinitionLabel()} \"{UrlTemplate.MakeHrefString(newFundSource.GetDetailUrl(), newFundSource.FundSourceName)}\" has been created.");
            return new ModalDialogFormJsonResult();
            //return RedirectToAction(new SitkaRoute<FundSourceController>(gc => gc.FundSourceDetail(newFundSource.FundSourceID)));
        }

        private PartialViewResult DuplicateFundSourceViewEdit(DuplicateFundSourceViewModel viewModel, FundSource fundSourceToDuplicate, List<FundSourceAllocation> fundSourceAllocations)
        {
            var fundSourceStatuses = FundSourceStatus.All;
            
            var viewData = new DuplicateFundSourceViewData(fundSourceStatuses, fundSourceToDuplicate, fundSourceAllocations);
            return RazorPartialView<DuplicateFundSource, DuplicateFundSourceViewData, DuplicateFundSourceViewModel>(viewData, viewModel);
        }


        #region FileResources

        [HttpGet]
        [FundSourceEditAsAdminFeature]
        public PartialViewResult NewFundSourceFiles(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            Check.EnsureNotNull(fundSourcePrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewFundSourceFiles(viewModel);
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundSourceFiles(FundSourcePrimaryKey fundSourcePrimaryKey, NewFileViewModel viewModel)
        {
            var fundSource = fundSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewFundSourceFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(fundSource, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.FundSource.GetFieldDefinitionLabel()} \"{fundSource.FundSourceName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewFundSourceFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData(FieldDefinition.FundSource.GetFieldDefinitionLabel());
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceManageFileResourceAsAdminFeature]
        public PartialViewResult EditFundSourceFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey)
        {
            var fileResource = fundSourceFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditFundSourceFile(viewModel);
        }

        [HttpPost]
        [FundSourceManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundSourceFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = fundSourceFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditFundSourceFile(viewModel);
            }

            viewModel.UpdateModel(fileResource);
            SetMessageForDisplay($"Successfully updated file \"{fileResource.DisplayName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundSourceFile(EditFileResourceViewModel viewModel)
        {
            var viewData = new EditFileResourceViewData();
            return RazorPartialView<EditFileResource, EditFileResourceViewData, EditFileResourceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceManageFileResourceAsAdminFeature]
        public PartialViewResult DeleteFundSourceFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceFile(fundSourceFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [FundSourceManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceFile(FundSourceFileResourcePrimaryKey fundSourceFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceFileResource = fundSourceFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceFile(fundSourceFileResource, viewModel);
            }

            var message = $"{FieldDefinition.FundSource.GetFieldDefinitionLabel()} file \"{fundSourceFileResource.DisplayName}\" created on '{fundSourceFileResource.FileResource.CreateDate}' by '{fundSourceFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            fundSourceFileResource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteFundSourceFile(FundSourceFileResource fundSourceFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{fundSourceFileResource.DisplayName}\" file created on '{fundSourceFileResource.FileResource.CreateDate}' by '{fundSourceFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion

        [FundSourceViewFeature]
        public ViewResult FundSourceDetail(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var fundSource = fundSourcePrimaryKey.EntityObject;
            var userHasEditFundSourcePermissions = new FundSourceEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var fundSourceNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(fundSource.FundSourceNotes)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewFundSourceNote(fundSourcePrimaryKey)),
                fundSource.FundSourceName,
                userHasEditFundSourcePermissions);

            var internalFundSourceNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(fundSource.FundSourceNoteInternals)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewFundSourceNoteInternal(fundSourcePrimaryKey)),
                fundSource.FundSourceName,
                userHasEditFundSourcePermissions);
            var viewData = new Views.FundSource.FundSourceDetailViewData(CurrentPerson, fundSource, fundSourceNotesViewData, internalFundSourceNotesViewData);
            return RazorView<FundSourceDetail, FundSourceDetailViewData>(viewData);
        }

        [FundSourceViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullFundSourceList);
            var viewData = new FundSourceIndexViewData(CurrentPerson, firmaPage);
            return RazorView<FundSourceIndex, FundSourceIndexViewData>(viewData);
        }

        [FundSourceViewFullListFeature]
        public ExcelResult FundSourcesExcelDownload()
        {
            var fundSources = HttpRequestStorage.DatabaseEntities.FundSources.ToList();
            var fundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.ToList();
            var workbookTitle = FieldDefinition.FundSource.GetFieldDefinitionLabelPluralized();
            return FundSourcesExcelDownloadImpl(fundSources, fundSourceAllocations, workbookTitle);
        }

        private ExcelResult FundSourcesExcelDownloadImpl(List<FundSource> fundSources, List<FundSourceAllocation> fundSourceAllocations, string workbookTitle)
        {
            var workSheets = new List<IExcelWorkbookSheetDescriptor>();

            // FundSources
            var fundSourceSpec = new FundSourceExcelSpec();
            var wsFundSources = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.FundSource.GetFieldDefinitionLabelPluralized()}", fundSourceSpec, fundSources);
            workSheets.Add(wsFundSources);

            // FundSource Allocations
            var fundSourceAllocationsSpec = new FundSourceAllocationExcelSpec();
            var wsFundSourceAllocations = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabelPluralized()}", fundSourceAllocationsSpec, fundSourceAllocations);
            workSheets.Add(wsFundSourceAllocations);

            // Overall excel file
            var wbm = new ExcelWorkbookMaker(workSheets);
            var excelWorkbook = wbm.ToXLWorkbook();

            return new ExcelResult(excelWorkbook, workbookTitle);
        }


        #region "Grid Json Object Functions"
        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSource> FundSourceGridJsonData()
        {
            var gridSpec = new FundSourceGridSpec(CurrentPerson);
            // They want the most current fundSources on top it seems, and sorting by FundSource Number might be good enough -- SLG 7/2/2020
            var fundSources = HttpRequestStorage.DatabaseEntities.FundSources.OrderByDescending(g => g.FundSourceNumber).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSource>(fundSources, gridSpec);
            return gridJsonNetJObjectResult;
        }

        // Move these to their relevant controllers instead??

        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> AllFundSourceAllocationGridJsonData()
        {
            // Create button is irrelevant to this data-only usage
            var gridSpec = new FundSourceAllocationGridSpec(CurrentPerson, FundSourceAllocationGridSpec.FundSourceAllocationGridCreateButtonType.Shown, null);
            var fundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(fundSourceAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> FundSourceAllocationGridJsonDataByFundSource(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var relevantFundSource = fundSourcePrimaryKey.EntityObject;
            // Create button is irrelevant to this data-only usage
            var gridSpec = new FundSourceAllocationGridSpec(CurrentPerson, FundSourceAllocationGridSpec.FundSourceAllocationGridCreateButtonType.Shown, relevantFundSource);
            var fundSourceAllocations = relevantFundSource.FundSourceAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(fundSourceAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }


        /// <summary>
        /// Used to display an empty fundSourceAllocation grid with "no results" when a row in the fundSource grid containing no current relationship to fundSourceAllocations is selected.
        /// Trying to make clear to user which fundSources don't have associated fundSourceAllocations yet.
        /// </summary>
        /// <returns>An empty dataset for grid population</returns>
        [FundSourceViewFullListFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> FundSourceAllocationGridWithoutAnyJsonData()
        {
            // Create button is irrelevant to this data-only usage
            var gridSpec = new FundSourceAllocationGridSpec(CurrentPerson, FundSourceAllocationGridSpec.FundSourceAllocationGridCreateButtonType.Shown, null);
            var fundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.Where(ga => ga.FundSource.FundSourceNumber == "").ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(fundSourceAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }


        #endregion


        #region WADNR FundSource JSON API

        [FundSourceViewJsonApiFeature]
        public JsonNetJArrayResult FundSourceJsonApi()
        {
            var fundSources = HttpRequestStorage.DatabaseEntities.FundSources.ToList();
            var jsonApiFundSources = FundSourceApiJson.MakeFundSourceApiJsonsFromFundSources(fundSources, false);
            return new JsonNetJArrayResult(jsonApiFundSources);
        }

        /// <summary>
        /// This is probably excessive, but I'm trying to overcompensate for every need Tammy Osborn has. -- SLG
        /// </summary>
        /// <returns></returns>
        [FundSourceViewJsonApiFeature]
        public JsonNetJArrayResult FundSourceStatusJsonApi()
        {
            var fundSourceStatuses = FundSourceStatus.All.ToList();
            var jsonApiFundSourceStatuses = FundSourceStatusApiJson.MakeFundSourceStatusApiJsonsFromFundSourceStatuses(fundSourceStatuses);
            return new JsonNetJArrayResult(jsonApiFundSourceStatuses);
        }


        #endregion


        #region "FundSource Note including internal"
        [HttpGet]
        [FundSourceEditAsAdminFeature]
        public PartialViewResult NewFundSourceNote(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var viewModel = new EditFundSourceNoteViewModel();
            return ViewEditNote(viewModel, EditFundSourceNoteType.NewNote);
        }

        [HttpGet]
        [FundSourceEditAsAdminFeature]
        public PartialViewResult NewFundSourceNoteInternal(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var viewModel = new EditFundSourceNoteInternalViewModel();
            return ViewEditNoteInternal(viewModel, EditFundSourceNoteInternalType.NewNote);
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundSourceNoteInternal(FundSourcePrimaryKey fundSourcePrimaryKey, EditFundSourceNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditFundSourceNoteInternalType.NewNote);
            }
            var fundSource = fundSourcePrimaryKey.EntityObject;
            var fundSourceNoteInternal = FundSourceNoteInternal.CreateNewBlank(fundSource, CurrentPerson);
            viewModel.UpdateModel(fundSourceNoteInternal, CurrentPerson, EditFundSourceNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNoteInternals.Add(fundSourceNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.FundSourceNoteInternal.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpPost]
        [FundSourceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundSourceNote(FundSourcePrimaryKey fundSourcePrimaryKey, EditFundSourceNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditFundSourceNoteType.NewNote);
            }
            var fundSource = fundSourcePrimaryKey.EntityObject;
            var fundSourceNote = FundSourceNote.CreateNewBlank(fundSource, CurrentPerson);
            viewModel.UpdateModel(fundSourceNote, CurrentPerson, EditFundSourceNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNotes.Add(fundSourceNote);
            SetMessageForDisplay($"{FieldDefinition.FundSourceNote.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceNoteEditAsAdminFeature]
        public PartialViewResult EditFundSourceNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey)
        {
            var fundSourceNote = fundSourceNotePrimaryKey.EntityObject;
            var viewModel = new EditFundSourceNoteViewModel(fundSourceNote);
            return ViewEditNote(viewModel, EditFundSourceNoteType.ExistingNote);
        }

        [HttpPost]
        [FundSourceNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundSourceNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey, EditFundSourceNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditFundSourceNoteType.ExistingNote);
            }

            var fundSourceNote = fundSourceNotePrimaryKey.EntityObject;
            viewModel.UpdateModel(fundSourceNote, CurrentPerson, EditFundSourceNoteType.ExistingNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNotes.AddOrUpdate(fundSourceNote);
            SetMessageForDisplay($"{FieldDefinition.FundSourceNote.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNoteInternal(EditFundSourceNoteInternalViewModel viewModel, EditFundSourceNoteInternalType editFundSourceNoteInternalType)
        {
            var viewData = new EditFundSourceNoteInternalViewData(editFundSourceNoteInternalType);
            return RazorPartialView<EditFundSourceNoteInternal, EditFundSourceNoteInternalViewData, EditFundSourceNoteInternalViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceNoteInternalEditAsAdminFeature]
        public PartialViewResult EditFundSourceNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey)
        {
            var fundSourceNoteInternal = fundSourceNoteInternalPrimaryKey.EntityObject;
            var viewModel = new EditFundSourceNoteInternalViewModel(fundSourceNoteInternal);
            return ViewEditNoteInternal(viewModel, EditFundSourceNoteInternalType.ExistingNote);
        }

        [HttpPost]
        [FundSourceNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundSourceNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey, EditFundSourceNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNoteInternal(viewModel, EditFundSourceNoteInternalType.ExistingNote);
            }

            var fundSourceNoteInternal = fundSourceNoteInternalPrimaryKey.EntityObject;
            viewModel.UpdateModel(fundSourceNoteInternal, CurrentPerson, EditFundSourceNoteType.ExistingNote);
            HttpRequestStorage.DatabaseEntities.FundSourceNoteInternals.AddOrUpdate(fundSourceNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.FundSourceNoteInternal.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNote(EditFundSourceNoteViewModel viewModel, EditFundSourceNoteType editFundSourceNoteType)
        {
            var viewData = new EditFundSourceNoteViewData(editFundSourceNoteType);
            return RazorPartialView<EditFundSourceNote, EditFundSourceNoteViewData, EditFundSourceNoteViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceNoteInternalEditAsAdminFeature]
        public PartialViewResult DeleteFundSourceNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceNoteInternalPrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceNoteInternal(fundSourceNoteInternalPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteFundSourceNoteInternal(FundSourceNoteInternal fundSourceNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.FundSourceNoteInternal.GetFieldDefinitionLabel()} created on '{fundSourceNoteInternal.CreatedDate}' by '{fundSourceNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceNoteInternal(FundSourceNoteInternalPrimaryKey fundSourceNoteInternalPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceNoteInternal = fundSourceNoteInternalPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceNoteInternal(fundSourceNoteInternal, viewModel);
            }

            var message = $"{FieldDefinition.FundSourceNoteInternal.GetFieldDefinitionLabel()} created on '{fundSourceNoteInternal.CreatedDate}' by '{fundSourceNoteInternal.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            fundSourceNoteInternal.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceNoteEditAsAdminFeature]
        public PartialViewResult DeleteFundSourceNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceNotePrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceNote(fundSourceNotePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteFundSourceNote(FundSourceNote fundSourceNote, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.FundSourceNote.GetFieldDefinitionLabel()} created on '{fundSourceNote.CreatedDate}' by '{fundSourceNote.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }



        [HttpPost]
        [FundSourceNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceNote(FundSourceNotePrimaryKey fundSourceNotePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceNote = fundSourceNotePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceNote(fundSourceNote, viewModel);
            }

            var message = $"{FieldDefinition.FundSourceNote.GetFieldDefinitionLabel()} created on '{fundSourceNote.CreatedDate}' by '{fundSourceNote.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            fundSourceNote.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
        #endregion


        [FundSourceViewFeature]
        public GridJsonNetJObjectResult<FundSourceAllocationBudgetLineItemForGrid> FundSourceAllocationBudgetLineItemGridJsonDataByFundSource(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var relevantFundSource = fundSourcePrimaryKey.EntityObject;
            // Create button is irrelevant to this data-only usage
            var gridSpec = new FundSourceAllocationBudgetLineItemGridSpec();
            var fundSourceAllocations = relevantFundSource.FundSourceAllocations.ToList();
            var budgetLineItems = fundSourceAllocations.Select(ga => new FundSourceAllocationBudgetLineItemForGrid(ga)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocationBudgetLineItemForGrid>(budgetLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FundSourceViewFeature]
        public GridJsonNetJObjectResult<Agreement> FundSourceAgreementGridJsonData(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            // 2/4/22 TK - need to walk "fundSource -> fundSource allocation -> AgreementFundSourceAllocation -> Agreement"
            var fundSource = fundSourcePrimaryKey.EntityObject;
            var fundSourceAllocations = fundSource.FundSourceAllocations;
            var agreementFundSourceAllocations = fundSourceAllocations.SelectMany(x => x.AgreementFundSourceAllocations);
            var agreements = agreementFundSourceAllocations.Select(x => x.Agreement).ToList();

            var gridSpec = new FundSourceAgreementGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Agreement>(agreements, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FundSourceViewFeature]
        public GridJsonNetJObjectResult<ProjectFundSourceAllocationRequest> ProjectFundSourceAllocationRequestsByFundSourceGridJsonData(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            var fundSource = fundSourcePrimaryKey.EntityObject;
            var fundSourceAllocations = fundSource.FundSourceAllocations;
            var projectFundSourceAllocationRequests = fundSourceAllocations.SelectMany(x => x.ProjectFundSourceAllocationRequests).ToList();
            var gridSpec = new ProjectFundSourceAllocationRequestsByFundSourceGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectFundSourceAllocationRequest>(projectFundSourceAllocationRequests, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
