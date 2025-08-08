/*-----------------------------------------------------------------------
<copyright file="FundSourceAllocationController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Views.FocusArea;
using ProjectFirma.Web.Views.FundSourceAllocation;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.FundSourceAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Controllers
{
    public class FundSourceAllocationController : FirmaBaseController
    {
        [HttpGet]
        [FundSourceAllocationDeleteFeature]
        public PartialViewResult DeleteFundSourceAllocation(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationPrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceAllocation(fundSourceAllocationPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteFundSourceAllocation(FundSourceAllocation fundSourceAllocation, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} '{fundSourceAllocation.FundSourceAllocationName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceAllocation(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceAllocation(fundSourceAllocation, viewModel);
            }

            var message = $"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} \"{fundSourceAllocation.FundSourceAllocationName}\" successfully deleted.";
            fundSourceAllocation.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult Edit(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(fundSourceAllocation);
            var relevantFundSource = fundSourceAllocation.FundSource;
            var viewModel = new EditFundSourceAllocationViewModel(fundSourceAllocation);
            return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.ExistingFundSourceAllocation, fundSourceAllocation, relevantFundSource);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditFundSourceAllocationViewModel viewModel)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(fundSourceAllocation);
            if (!ModelState.IsValid)
            {
                return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.ExistingFundSourceAllocation, fundSourceAllocation, fundSourceAllocation.FundSource);
            }
            viewModel.UpdateModel(fundSourceAllocation, CurrentPerson);
            SetMessageForDisplay($"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} \"{fundSourceAllocation.FundSourceAllocationName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult FundSourceAllocationViewEdit(EditFundSourceAllocationViewModel viewModel,
                                                          EditFundSourceAllocationType editFundSourceAllocationType,
                                                          FundSourceAllocation fundSourceAllocationBeingEdited,
                                                          FundSource optionalRelevantFundSource)
        {
            if (editFundSourceAllocationType == EditFundSourceAllocationType.ExistingFundSourceAllocation)
            {
                // Sanity check; this should always agree for an existing one
                Check.Ensure(optionalRelevantFundSource.FundSourceID == fundSourceAllocationBeingEdited.FundSource.FundSourceID);
            }
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var fundSourceTypes = HttpRequestStorage.DatabaseEntities.FundSourceTypes;
            var fundSources = HttpRequestStorage.DatabaseEntities.FundSources.ToList();
            var divisions = Division.All;
            var regions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions;
            var federalFundCodes = HttpRequestStorage.DatabaseEntities.FederalFundCodes;
            var sources = HttpRequestStorage.DatabaseEntities.FundSourceAllocationSources;
            var priorities = HttpRequestStorage.DatabaseEntities.FundSourceAllocationPriorities;
            var people = HttpRequestStorage.DatabaseEntities.People.ToList();


            var viewData = new EditFundSourceAllocationViewData(editFundSourceAllocationType,
                                                            fundSourceAllocationBeingEdited,
                                                            organizations,
                                                            fundSourceTypes,
                                                            fundSources,
                                                            divisions,
                                                            regions,
                                                            federalFundCodes,
                                                            sources,
                                                            priorities,
                                                            people
            );
            return RazorPartialView<EditFundSourceAllocation, EditFundSourceAllocationViewData, EditFundSourceAllocationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditFundSourceAllocationViewModel();
            // 6/29/20 TK (SLG EDIT) - Null is correct here. the FundSource Allocation passed in is used to get any "Program Managers" assigned on
            // a FundSource Allocation that may have lost their "program manager" permissions
            return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.NewFundSourceAllocation, null, null);
        }

        [HttpPost]
        [FundSourceAllocationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditFundSourceAllocationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // 6/29/20 TK (SLG EDIT) - Null is correct here. the FundSource Allocation passed in is used to get any "Program Managers" assigned on
                // a FundSource Allocation that may have lost their "program manager" permissions
                return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.NewFundSourceAllocation, null, null);
            }

            var relevantFundSource = HttpRequestStorage.DatabaseEntities.FundSources.GetFundSource(viewModel.FundSourceID);
            var fundSourceAllocation = FundSourceAllocation.CreateNewBlank(relevantFundSource);
            viewModel.UpdateModel(fundSourceAllocation, CurrentPerson);
            fundSourceAllocation.CreateAllFundSourceAllocationBudgetLineItemsByCostType();
            SetMessageForDisplay($"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} \"{fundSourceAllocation.FundSourceAllocationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationCreateFeature]
        public PartialViewResult NewFromFundSource(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            FundSource relevantFundSource = fundSourcePrimaryKey.EntityObject;
            var viewModel = new EditFundSourceAllocationViewModel();
            // Pre-populate allocation dates from the fundSource
            viewModel.StartDate = relevantFundSource.StartDate;
            viewModel.EndDate = relevantFundSource.EndDate;
            // 6/29/20 TK (SLG EDIT) - Null is correct here. the FundSource Allocation passed in is used to get any "Program Managers" assigned on
            // a FundSource Allocation that may have lost their "program manager" permissions
            return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.NewFundSourceAllocation, null, relevantFundSource);
        }

        [HttpPost]
        [FundSourceAllocationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFromFundSource(FundSourcePrimaryKey fundSourcePrimaryKey, EditFundSourceAllocationViewModel viewModel)
        {
            FundSource relevantFundSource = fundSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                // 6/29/20 TK (SLG EDIT) - Null is correct here. the FundSource Allocation passed in is used to get any "Program Managers" assigned on
                // a FundSource Allocation that may have lost their "program manager" permissions
                return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.NewFundSourceAllocation, null, relevantFundSource);
            }

            var fundSourceAllocation = FundSourceAllocation.CreateNewBlank(relevantFundSource);
            viewModel.UpdateModel(fundSourceAllocation, CurrentPerson);
            fundSourceAllocation.CreateAllFundSourceAllocationBudgetLineItemsByCostType();
            SetMessageForDisplay($"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} \"{fundSourceAllocation.FundSourceAllocationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationCreateFeature]
        public PartialViewResult Duplicate(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var originalFundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(originalFundSourceAllocation);
            var relevantFundSource = originalFundSourceAllocation.FundSource;
            
            // Copy original fundSource allocation to new view model, except for the fundSource mod and allocation amount
            var viewModel = new EditFundSourceAllocationViewModel(originalFundSourceAllocation);
            viewModel.FundSourceID = 0;
            viewModel.AllocationAmount = null;
            viewModel.FundSourceAllocationName = $"{viewModel.FundSourceAllocationName} - Copy";

            // 6/29/20 TK (SLG EDIT) - Null is correct here. the FundSource Allocation passed in is used to get any "Program Managers" assigned on
            // a FundSource Allocation that may have lost their "program manager" permissions
            return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.NewFundSourceAllocation, null, relevantFundSource);
        }

        [HttpPost]
        [FundSourceAllocationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Duplicate(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditFundSourceAllocationViewModel viewModel)
        {
            var originalFundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(originalFundSourceAllocation);
            var relevantFundSource = originalFundSourceAllocation.FundSource;
            if (!ModelState.IsValid)
            {
                // 6/29/20 TK (SLG EDIT) - Null is correct here. the FundSource Allocation passed in is used to get any "Program Managers" assigned on
                // a FundSource Allocation that may have lost their "program manager" permissions
                return FundSourceAllocationViewEdit(viewModel, EditFundSourceAllocationType.NewFundSourceAllocation, null, relevantFundSource);
            }

            var fundSourceAllocation = FundSourceAllocation.CreateNewBlank(relevantFundSource);
            viewModel.UpdateModel(fundSourceAllocation, CurrentPerson);
            fundSourceAllocation.CreateAllFundSourceAllocationBudgetLineItemsByCostType();
            SetMessageForDisplay($"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} \"{fundSourceAllocation.FundSourceAllocationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        #region FileResources

        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult NewFundSourceAllocationFiles(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            Check.EnsureNotNull(fundSourceAllocationPrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewFundSourceAllocationFiles(viewModel);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundSourceAllocationFiles(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, NewFileViewModel viewModel)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewFundSourceAllocationFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(fundSourceAllocation, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} \"{fundSourceAllocation.FundSourceAllocationName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewFundSourceAllocationFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData(FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel());
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        public PartialViewResult EditFundSourceAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey)
        {
            var fileResource = fundSourceAllocationFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditFundSourceAllocationFile(viewModel);
        }

        [HttpPost]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundSourceAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = fundSourceAllocationFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditFundSourceAllocationFile(viewModel);
            }

            viewModel.UpdateModel(fileResource);
            SetMessageForDisplay($"Successfully updated file \"{fileResource.DisplayName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFundSourceAllocationFile(EditFileResourceViewModel viewModel)
        {
            var viewData = new EditFileResourceViewData();
            return RazorPartialView<EditFileResource, EditFileResourceViewData, EditFileResourceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        public PartialViewResult DeleteFundSourceAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceAllocationFile(fundSourceAllocationFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceAllocationFileResource = fundSourceAllocationFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceAllocationFile(fundSourceAllocationFileResource, viewModel);
            }

            var message = $"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} file \"{fundSourceAllocationFileResource.DisplayName}\" deleted on '{fundSourceAllocationFileResource.FileResource.CreateDate}' by '{fundSourceAllocationFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            fundSourceAllocationFileResource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteFundSourceAllocationFile(FundSourceAllocationFileResource fundSourceAllocationFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{fundSourceAllocationFileResource.DisplayName}\" file created on '{fundSourceAllocationFileResource.FileResource.CreateDate}' by '{fundSourceAllocationFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion

        [HttpGet]
        [FundSourceAllocationsViewFeature]
        public ViewResult FundSourceAllocationDetail(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            if (fundSourceAllocation == null)
            {
                throw new Exception($"Could not find FundSourceAllocationID # {fundSourceAllocationPrimaryKey.PrimaryKeyValue}; has it been deleted?");
            }


            var fundSourceAllocationBasicsViewData = new FundSourceAllocationBasicsViewData(fundSourceAllocation, false, !CurrentPerson.IsAnonymousOrUnassigned);
            var userHasEditFundSourceAllocationPermissions = new FundSourceAllocationEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var fundSourceAllocationNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(fundSourceAllocation.FundSourceAllocationNotes)),
                SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x => x.NewFundSourceAllocationNote(fundSourceAllocationPrimaryKey)),
                fundSourceAllocation.FundSourceAllocationName,
                userHasEditFundSourceAllocationPermissions);
            var fundSourceAllocationNoteInternalsViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(fundSourceAllocation.FundSourceAllocationNoteInternals)),
                SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x => x.NewFundSourceAllocationNoteInternal(fundSourceAllocationPrimaryKey)),
                fundSourceAllocation.FundSourceAllocationName,
                userHasEditFundSourceAllocationPermissions);

            var costTypes = CostType.All.Where(x => x.IsValidInvoiceLineItemCostType).OrderBy(x => x.CostTypeDisplayName).ToList();

            string chartTitle = $"{FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} Expenditures";
            var chartContainerID = chartTitle.Replace(" ", "");

            // If ProjectFundSourceAllocationExpenditures is empty, ToGoogleChart returns null...
            var googleChart = fundSourceAllocation.FundSourceAllocationExpenditures
                .ToGoogleChart(x => x.CostType?.CostTypeDisplayName,
                    costTypes.Select(x => x.CostTypeDisplayName).ToList(),
                    x => x.CostType?.CostTypeDisplayName,
                    chartContainerID,
                    fundSourceAllocation.DisplayName);

            // Which makes this guy bork (bork bork bork)
            googleChart?.GoogleChartConfiguration.Legend.SetLegendPosition(GoogleChartLegendPosition.Top);
            var viewGoogleChartViewData = new ViewGoogleChartViewData(googleChart, chartTitle, 350, false);

            var projectFundSourceAllocationRequestsGridSpec = new ProjectFundSourceAllocationRequestsGridSpec()
            {
                ObjectNameSingular = "Project",
                ObjectNamePlural = "Projects",
                SaveFiltersInCookie = true
            };

            var fundSourceAllocationExpendituresGridSpec = new FundSourceAllocationExpendituresGridSpec();

            var viewData = new Views.FundSourceAllocation.DetailViewData(CurrentPerson, fundSourceAllocation, fundSourceAllocationBasicsViewData, fundSourceAllocationNotesViewData, fundSourceAllocationNoteInternalsViewData, viewGoogleChartViewData, projectFundSourceAllocationRequestsGridSpec, fundSourceAllocationExpendituresGridSpec);
            return RazorView<Views.FundSourceAllocation.Detail, Views.FundSourceAllocation.DetailViewData>(viewData);
        }


        [FundSourceAllocationsViewFeature]
        public GridJsonNetJObjectResult<ProjectFundSourceAllocationRequest> ProjectFundSourceAllocationRequestsGridJsonData(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var projectFundSourceAllocationRequests = fundSourceAllocation.ProjectFundSourceAllocationRequests.ToList();
            var gridSpec = new ProjectFundSourceAllocationRequestsGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectFundSourceAllocationRequest>(projectFundSourceAllocationRequests, gridSpec);
            return gridJsonNetJObjectResult;
        }

        #region "FundSource Allocation Budget Vs Actuals"
        [FundSourceAllocationsViewFeature]
        public GridJsonNetJObjectResult<BudgetVsActualLineItem> FundSourceAllocationBudgetVsActualsGridJsonData(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var gridSpec = new FundSourceAllocationBudgetVsActualsGridSpec(CurrentPerson);

            var fundSourceAllocationBudgetVsActualLineItems = fundSourceAllocation.GetAllBudgetVsActualLineItemsByCostType();

            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<BudgetVsActualLineItem>(fundSourceAllocationBudgetVsActualLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }
        #endregion
        #region "FundSource Allocation Budget Line Item"
        [HttpGet]
        [FundSourceAllocationBudgetLineItemEditAsAdminFeature]
        public JsonResult EditFundSourceAllocationBudgetLineItemAjax(FundSourceAllocationBudgetLineItemPrimaryKey fundSourceAllocationBudgetLineItemPrimaryKey)
        {
            return new JsonResult();
        }

        [HttpPost]
        [FundSourceAllocationBudgetLineItemEditAsAdminFeature]
        public JsonResult EditFundSourceAllocationBudgetLineItemAjax(FundSourceAllocationBudgetLineItemPrimaryKey fundSourceAllocationBudgetLineItemPrimaryKey, FundSourceAllocationBudgetLineItemAjaxModel fundSourceAllocationBudgetLineItemAjaxModel)
        {
            var lineItem = HttpRequestStorage.DatabaseEntities.FundSourceAllocationBudgetLineItems.SingleOrDefault(x =>
                x.FundSourceAllocationBudgetLineItemID == fundSourceAllocationBudgetLineItemAjaxModel.FundSourceAllocationBudgetLineItemID);

            var costType = CostType.All.Single(x => x.CostTypeID == fundSourceAllocationBudgetLineItemAjaxModel.CostTypeID);

            if (lineItem == null)
            {
                return Json($"There was an issue saving the {FieldDefinition.FundSourceAllocationBudgetLineItem.FieldDefinitionDisplayName} for {FieldDefinition.CostType.FieldDefinitionDisplayName} \"{costType.CostTypeDisplayName}\".");
            }

            lineItem.FundSourceAllocationBudgetLineItemAmount = fundSourceAllocationBudgetLineItemAjaxModel.LineItemAmount;
            lineItem.FundSourceAllocationBudgetLineItemNote = fundSourceAllocationBudgetLineItemAjaxModel.LineItemNote;

            HttpRequestStorage.DatabaseEntities.SaveChanges(IsolationLevel.ReadUncommitted);

            return Json($"Saved {FieldDefinition.FundSourceAllocationBudgetLineItem.FieldDefinitionDisplayName} for {FieldDefinition.CostType.FieldDefinitionDisplayName} \"{costType.CostTypeDisplayName}\".");
        }

        

        #endregion


        #region "FundSource Allocation Notes (Includes Internal)"
        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult NewFundSourceAllocationNote(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var viewModel = new EditFundSourceAllocationNoteViewModel();
            return ViewEditNote(viewModel, EditFundSourceAllocationNoteType.NewNote);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundSourceAllocationNote(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditFundSourceAllocationNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditFundSourceAllocationNoteType.NewNote);
            }
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var fundSourceAllocationNote = FundSourceAllocationNote.CreateNewBlank(fundSourceAllocation, CurrentPerson);
            viewModel.UpdateModel(fundSourceAllocationNote, CurrentPerson, EditFundSourceAllocationNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.FundSourceAllocationNotes.Add(fundSourceAllocationNote);
            SetMessageForDisplay($"New {FieldDefinition.FundSourceAllocationNote.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNote(EditFundSourceAllocationNoteViewModel viewModel, EditFundSourceAllocationNoteType editFundSourceAllocationNoteType)
        {
            var viewData = new EditFundSourceAllocationNoteViewData(editFundSourceAllocationNoteType);
            return RazorPartialView<EditFundSourceAllocationNote, EditFundSourceAllocationNoteViewData, EditFundSourceAllocationNoteViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult NewFundSourceAllocationNoteInternal(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var viewModel = new EditFundSourceAllocationNoteInternalViewModel();
            return ViewEditFundSourceAllocationNoteInternal(viewModel, EditFundSourceAllocationNoteInternalType.NewNote);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFundSourceAllocationNoteInternal(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditFundSourceAllocationNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditFundSourceAllocationNoteInternal(viewModel, EditFundSourceAllocationNoteInternalType.NewNote);
            }
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var fundSourceAllocationNoteInternal = FundSourceAllocationNoteInternal.CreateNewBlank(fundSourceAllocation, CurrentPerson);
            viewModel.UpdateModel(fundSourceAllocationNoteInternal, CurrentPerson, EditFundSourceAllocationNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.FundSourceAllocationNoteInternals.Add(fundSourceAllocationNoteInternal);
            SetMessageForDisplay($"New {FieldDefinition.FundSourceAllocationNoteInternal.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditFundSourceAllocationNoteInternal(EditFundSourceAllocationNoteInternalViewModel viewModel, EditFundSourceAllocationNoteInternalType editFundSourceAllocationNoteInternalType)
        {
            var viewData = new EditFundSourceAllocationNoteInternalViewData(editFundSourceAllocationNoteInternalType);
            return RazorPartialView<EditFundSourceAllocationNoteInternal, EditFundSourceAllocationNoteInternalViewData, EditFundSourceAllocationNoteInternalViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        public PartialViewResult EditFundSourceAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey)
        {
            var fundSourceAllocationNoteInternal = fundSourceAllocationNoteInternalPrimaryKey.EntityObject;
            var viewModel = new EditFundSourceAllocationNoteInternalViewModel(fundSourceAllocationNoteInternal);
            return ViewEditFundSourceAllocationNoteInternal(viewModel, EditFundSourceAllocationNoteInternalType.ExistingFundSourceAllocationNoteInternal);
        }

        [HttpPost]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundSourceAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey, EditFundSourceAllocationNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditFundSourceAllocationNoteInternal(viewModel, EditFundSourceAllocationNoteInternalType.ExistingFundSourceAllocationNoteInternal);
            }

            var fundSourceAllocationNoteInternal = fundSourceAllocationNoteInternalPrimaryKey.EntityObject;
            viewModel.UpdateModel(fundSourceAllocationNoteInternal, CurrentPerson, EditFundSourceAllocationNoteType.ExistingFundSourceAllocationNote);
            HttpRequestStorage.DatabaseEntities.FundSourceAllocationNoteInternals.AddOrUpdate(fundSourceAllocationNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.FundSourceAllocationNoteInternal.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        public PartialViewResult DeleteFundSourceAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationNoteInternalPrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceAllocationNoteInternal(fundSourceAllocationNoteInternalPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteFundSourceAllocationNoteInternal(FundSourceAllocationNoteInternal fundSourceAllocationNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.FundSourceAllocationNoteInternal.GetFieldDefinitionLabel()} created on '{fundSourceAllocationNoteInternal.CreatedDate}' by '{fundSourceAllocationNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceAllocationNoteInternal = fundSourceAllocationNoteInternalPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceAllocationNoteInternal(fundSourceAllocationNoteInternal, viewModel);
            }

            var message = $"{FieldDefinition.FundSourceAllocationNoteInternal.GetFieldDefinitionLabel()} created on '{fundSourceAllocationNoteInternal.CreatedDate}' by '{fundSourceAllocationNoteInternal.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            fundSourceAllocationNoteInternal.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationNoteEditAsAdminFeature]
        public PartialViewResult EditFundSourceAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey)
        {
            var fundSourceAllocationNote = fundSourceAllocationNotePrimaryKey.EntityObject;
            var viewModel = new EditFundSourceAllocationNoteViewModel(fundSourceAllocationNote);
            return ViewEditNote(viewModel, EditFundSourceAllocationNoteType.ExistingFundSourceAllocationNote);
        }

        [HttpPost]
        [FundSourceAllocationNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFundSourceAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey, EditFundSourceAllocationNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditFundSourceAllocationNoteType.ExistingFundSourceAllocationNote);
            }

            var fundSourceAllocationNote = fundSourceAllocationNotePrimaryKey.EntityObject;
            viewModel.UpdateModel(fundSourceAllocationNote, CurrentPerson, EditFundSourceAllocationNoteType.ExistingFundSourceAllocationNote);
            HttpRequestStorage.DatabaseEntities.FundSourceAllocationNotes.AddOrUpdate(fundSourceAllocationNote);
            SetMessageForDisplay($"{FieldDefinition.FundSourceAllocationNote.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [FundSourceAllocationNoteEditAsAdminFeature]
        public PartialViewResult DeleteFundSourceAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationNotePrimaryKey.PrimaryKeyValue);
            return ViewDeleteFundSourceAllocationNote(fundSourceAllocationNotePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteFundSourceAllocationNote(FundSourceAllocationNote fundSourceAllocationNote, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.FundSourceAllocationNote.GetFieldDefinitionLabel()} created on '{fundSourceAllocationNote.CreatedDate}' by '{fundSourceAllocationNote.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFundSourceAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fundSourceAllocationNote = fundSourceAllocationNotePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFundSourceAllocationNote(fundSourceAllocationNote, viewModel);
            }

            var message = $"{FieldDefinition.FundSourceAllocationNote.GetFieldDefinitionLabel()} created on '{fundSourceAllocationNote.CreatedDate}' by '{fundSourceAllocationNote.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            fundSourceAllocationNote.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }



        #endregion

        [FundSourceAllocationsViewFeature]
        public GridJsonNetJObjectResult<FundSourceAllocationExpenditure> FundSourceAllocationExpendituresGridJsonData(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var fundSourceAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var fundSourceAllocationExpenditures = fundSourceAllocation.FundSourceAllocationExpenditures.ToList();
            var gridSpec = new FundSourceAllocationExpendituresGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocationExpenditure>(fundSourceAllocationExpenditures, gridSpec);
            return gridJsonNetJObjectResult;
        }



        #region FundSource Allocation JSON API

        [FundSourceViewJsonApiFeature]
        public JsonNetJArrayResult FundSourceAllocationJsonApi()
        {
            var fundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.ToList();
            var jsonApiFundSourceAllocations = FundSourceAllocationApiJson.MakeFundSourceAllocationApiJsonsFromFundSourceAllocations(fundSourceAllocations, false);
            return new JsonNetJArrayResult(jsonApiFundSourceAllocations);
        }

        #endregion

    }
}
