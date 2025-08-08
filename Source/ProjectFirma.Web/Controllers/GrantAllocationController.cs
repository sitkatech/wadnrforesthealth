/*-----------------------------------------------------------------------
<copyright file="GrantAllocationController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.GrantAllocation;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Controllers
{
    public class GrantAllocationController : FirmaBaseController
    {
        [HttpGet]
        [FundSourceAllocationDeleteFeature]
        public PartialViewResult DeleteGrantAllocation(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantAllocation(fundSourceAllocationPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantAllocation(FundSourceAllocation fundSourceAllocation, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} '{fundSourceAllocation.GrantAllocationName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantAllocation(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantAllocation(grantAllocation, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} \"{grantAllocation.GrantAllocationName}\" successfully deleted.";
            grantAllocation.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult Edit(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(grantAllocation);
            var relevantGrant = grantAllocation.FundSource;
            var viewModel = new EditGrantAllocationViewModel(grantAllocation);
            return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.ExistingGrantAllocation, grantAllocation, relevantGrant);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditGrantAllocationViewModel viewModel)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(grantAllocation);
            if (!ModelState.IsValid)
            {
                return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.ExistingGrantAllocation, grantAllocation, grantAllocation.FundSource);
            }
            viewModel.UpdateModel(grantAllocation, CurrentPerson);
            SetMessageForDisplay($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} \"{grantAllocation.GrantAllocationName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult GrantAllocationViewEdit(EditGrantAllocationViewModel viewModel,
                                                          EditGrantAllocationType editGrantAllocationType,
                                                          FundSourceAllocation fundSourceAllocationBeingEdited,
                                                          FundSource optionalRelevantFundSource)
        {
            if (editGrantAllocationType == EditGrantAllocationType.ExistingGrantAllocation)
            {
                // Sanity check; this should always agree for an existing one
                Check.Ensure(optionalRelevantFundSource.FundSourceID == fundSourceAllocationBeingEdited.FundSource.FundSourceID);
            }
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var grantTypes = HttpRequestStorage.DatabaseEntities.GrantTypes;
            var grants = HttpRequestStorage.DatabaseEntities.Grants.ToList();
            var divisions = Division.All;
            var regions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions;
            var federalFundCodes = HttpRequestStorage.DatabaseEntities.FederalFundCodes;
            var sources = HttpRequestStorage.DatabaseEntities.GrantAllocationSources;
            var priorities = HttpRequestStorage.DatabaseEntities.GrantAllocationPriorities;
            var people = HttpRequestStorage.DatabaseEntities.People.ToList();


            var viewData = new EditGrantAllocationViewData(editGrantAllocationType,
                                                            fundSourceAllocationBeingEdited,
                                                            organizations,
                                                            grantTypes,
                                                            grants,
                                                            divisions,
                                                            regions,
                                                            federalFundCodes,
                                                            sources,
                                                            priorities,
                                                            people
            );
            return RazorPartialView<EditGrantAllocation, EditGrantAllocationViewData, EditGrantAllocationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditGrantAllocationViewModel();
            // 6/29/20 TK (SLG EDIT) - Null is correct here. the Grant Allocation passed in is used to get any "Program Managers" assigned on
            // a Grant Allocation that may have lost their "program manager" permissions
            return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.NewGrantAllocation, null, null);
        }

        [HttpPost]
        [FundSourceAllocationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditGrantAllocationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // 6/29/20 TK (SLG EDIT) - Null is correct here. the Grant Allocation passed in is used to get any "Program Managers" assigned on
                // a Grant Allocation that may have lost their "program manager" permissions
                return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.NewGrantAllocation, null, null);
            }

            var relevantGrant = HttpRequestStorage.DatabaseEntities.Grants.GetGrant(viewModel.GrantID);
            var grantAllocation = FundSourceAllocation.CreateNewBlank(relevantGrant);
            viewModel.UpdateModel(grantAllocation, CurrentPerson);
            grantAllocation.CreateAllGrantAllocationBudgetLineItemsByCostType();
            SetMessageForDisplay($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} \"{grantAllocation.GrantAllocationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationCreateFeature]
        public PartialViewResult NewFromGrant(FundSourcePrimaryKey fundSourcePrimaryKey)
        {
            FundSource relevantFundSource = fundSourcePrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationViewModel();
            // Pre-populate allocation dates from the grant
            viewModel.StartDate = relevantFundSource.StartDate;
            viewModel.EndDate = relevantFundSource.EndDate;
            // 6/29/20 TK (SLG EDIT) - Null is correct here. the Grant Allocation passed in is used to get any "Program Managers" assigned on
            // a Grant Allocation that may have lost their "program manager" permissions
            return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.NewGrantAllocation, null, relevantFundSource);
        }

        [HttpPost]
        [FundSourceAllocationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewFromGrant(FundSourcePrimaryKey fundSourcePrimaryKey, EditGrantAllocationViewModel viewModel)
        {
            FundSource relevantFundSource = fundSourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                // 6/29/20 TK (SLG EDIT) - Null is correct here. the Grant Allocation passed in is used to get any "Program Managers" assigned on
                // a Grant Allocation that may have lost their "program manager" permissions
                return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.NewGrantAllocation, null, relevantFundSource);
            }

            var grantAllocation = FundSourceAllocation.CreateNewBlank(relevantFundSource);
            viewModel.UpdateModel(grantAllocation, CurrentPerson);
            grantAllocation.CreateAllGrantAllocationBudgetLineItemsByCostType();
            SetMessageForDisplay($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} \"{grantAllocation.GrantAllocationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationCreateFeature]
        public PartialViewResult Duplicate(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var originalGrantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(originalGrantAllocation);
            var relevantGrant = originalGrantAllocation.FundSource;
            
            // Copy original grant allocation to new view model, except for the grant mod and allocation amount
            var viewModel = new EditGrantAllocationViewModel(originalGrantAllocation);
            viewModel.GrantID = 0;
            viewModel.AllocationAmount = null;
            viewModel.GrantAllocationName = $"{viewModel.GrantAllocationName} - Copy";

            // 6/29/20 TK (SLG EDIT) - Null is correct here. the Grant Allocation passed in is used to get any "Program Managers" assigned on
            // a Grant Allocation that may have lost their "program manager" permissions
            return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.NewGrantAllocation, null, relevantGrant);
        }

        [HttpPost]
        [FundSourceAllocationCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Duplicate(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditGrantAllocationViewModel viewModel)
        {
            var originalGrantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            Check.EnsureNotNull(originalGrantAllocation);
            var relevantGrant = originalGrantAllocation.FundSource;
            if (!ModelState.IsValid)
            {
                // 6/29/20 TK (SLG EDIT) - Null is correct here. the Grant Allocation passed in is used to get any "Program Managers" assigned on
                // a Grant Allocation that may have lost their "program manager" permissions
                return GrantAllocationViewEdit(viewModel, EditGrantAllocationType.NewGrantAllocation, null, relevantGrant);
            }

            var grantAllocation = FundSourceAllocation.CreateNewBlank(relevantGrant);
            viewModel.UpdateModel(grantAllocation, CurrentPerson);
            grantAllocation.CreateAllGrantAllocationBudgetLineItemsByCostType();
            SetMessageForDisplay($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} \"{grantAllocation.GrantAllocationName}\" has been created.");
            return new ModalDialogFormJsonResult();
        }

        #region FileResources

        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult NewGrantAllocationFiles(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            Check.EnsureNotNull(fundSourceAllocationPrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewGrantAllocationFiles(viewModel);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantAllocationFiles(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, NewFileViewModel viewModel)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewGrantAllocationFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(grantAllocation, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} \"{grantAllocation.GrantAllocationName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewGrantAllocationFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData(FieldDefinition.GrantAllocation.GetFieldDefinitionLabel());
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        public PartialViewResult EditGrantAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey)
        {
            var fileResource = fundSourceAllocationFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditGrantAllocationFile(viewModel);
        }

        [HttpPost]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = fundSourceAllocationFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditGrantAllocationFile(viewModel);
            }

            viewModel.UpdateModel(fileResource);
            SetMessageForDisplay($"Successfully updated file \"{fileResource.DisplayName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditGrantAllocationFile(EditFileResourceViewModel viewModel)
        {
            var viewData = new EditFileResourceViewData();
            return RazorPartialView<EditFileResource, EditFileResourceViewData, EditFileResourceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        public PartialViewResult DeleteGrantAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantAllocationFile(fundSourceAllocationFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantAllocationFile(FundSourceAllocationFileResourcePrimaryKey fundSourceAllocationFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantAllocationFileResource = fundSourceAllocationFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantAllocationFile(grantAllocationFileResource, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} file \"{grantAllocationFileResource.DisplayName}\" deleted on '{grantAllocationFileResource.FileResource.CreateDate}' by '{grantAllocationFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            grantAllocationFileResource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteGrantAllocationFile(GrantAllocationFileResource grantAllocationFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{grantAllocationFileResource.DisplayName}\" file created on '{grantAllocationFileResource.FileResource.CreateDate}' by '{grantAllocationFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion

        [HttpGet]
        [FundSourceAllocationsViewFeature]
        public ViewResult GrantAllocationDetail(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            if (grantAllocation == null)
            {
                throw new Exception($"Could not find GrantAllocationID # {fundSourceAllocationPrimaryKey.PrimaryKeyValue}; has it been deleted?");
            }


            var grantAllocationBasicsViewData = new GrantAllocationBasicsViewData(grantAllocation, false, !CurrentPerson.IsAnonymousOrUnassigned);
            var userHasEditGrantAllocationPermissions = new FundSourceAllocationEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var grantAllocationNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grantAllocation.GrantAllocationNotes)),
                SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.NewGrantAllocationNote(fundSourceAllocationPrimaryKey)),
                grantAllocation.GrantAllocationName,
                userHasEditGrantAllocationPermissions);
            var grantAllocationNoteInternalsViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(grantAllocation.GrantAllocationNoteInternals)),
                SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.NewGrantAllocationNoteInternal(fundSourceAllocationPrimaryKey)),
                grantAllocation.GrantAllocationName,
                userHasEditGrantAllocationPermissions);

            var costTypes = CostType.All.Where(x => x.IsValidInvoiceLineItemCostType).OrderBy(x => x.CostTypeDisplayName).ToList();

            string chartTitle = $"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Expenditures";
            var chartContainerID = chartTitle.Replace(" ", "");

            // If ProjectGrantAllocationExpenditures is empty, ToGoogleChart returns null...
            var googleChart = grantAllocation.GrantAllocationExpenditures
                .ToGoogleChart(x => x.CostType?.CostTypeDisplayName,
                    costTypes.Select(x => x.CostTypeDisplayName).ToList(),
                    x => x.CostType?.CostTypeDisplayName,
                    chartContainerID,
                    grantAllocation.DisplayName);

            // Which makes this guy bork (bork bork bork)
            googleChart?.GoogleChartConfiguration.Legend.SetLegendPosition(GoogleChartLegendPosition.Top);
            var viewGoogleChartViewData = new ViewGoogleChartViewData(googleChart, chartTitle, 350, false);

            var projectGrantAllocationRequestsGridSpec = new ProjectGrantAllocationRequestsGridSpec()
            {
                ObjectNameSingular = "Project",
                ObjectNamePlural = "Projects",
                SaveFiltersInCookie = true
            };

            var grantAllocationExpendituresGridSpec = new GrantAllocationExpendituresGridSpec();

            var viewData = new Views.GrantAllocation.DetailViewData(CurrentPerson, grantAllocation, grantAllocationBasicsViewData, grantAllocationNotesViewData, grantAllocationNoteInternalsViewData, viewGoogleChartViewData, projectGrantAllocationRequestsGridSpec, grantAllocationExpendituresGridSpec);
            return RazorView<Views.GrantAllocation.Detail, Views.GrantAllocation.DetailViewData>(viewData);
        }


        [FundSourceAllocationsViewFeature]
        public GridJsonNetJObjectResult<ProjectGrantAllocationRequest> ProjectGrantAllocationRequestsGridJsonData(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var projectGrantAllocationRequests = grantAllocation.ProjectGrantAllocationRequests.ToList();
            var gridSpec = new ProjectGrantAllocationRequestsGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectGrantAllocationRequest>(projectGrantAllocationRequests, gridSpec);
            return gridJsonNetJObjectResult;
        }

        #region "Grant Allocation Budget Vs Actuals"
        [FundSourceAllocationsViewFeature]
        public GridJsonNetJObjectResult<BudgetVsActualLineItem> GrantAllocationBudgetVsActualsGridJsonData(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var gridSpec = new GrantAllocationBudgetVsActualsGridSpec(CurrentPerson);

            var grantAllocationBudgetVsActualLineItems = grantAllocation.GetAllBudgetVsActualLineItemsByCostType();

            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<BudgetVsActualLineItem>(grantAllocationBudgetVsActualLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }
        #endregion
        #region "Grant Allocation Budget Line Item"
        [HttpGet]
        [FundSourceAllocationBudgetLineItemEditAsAdminFeature]
        public JsonResult EditGrantAllocationBudgetLineItemAjax(FundSourceAllocationBudgetLineItemPrimaryKey fundSourceAllocationBudgetLineItemPrimaryKey)
        {
            return new JsonResult();
        }

        [HttpPost]
        [FundSourceAllocationBudgetLineItemEditAsAdminFeature]
        public JsonResult EditGrantAllocationBudgetLineItemAjax(FundSourceAllocationBudgetLineItemPrimaryKey fundSourceAllocationBudgetLineItemPrimaryKey, GrantAllocationBudgetLineItemAjaxModel grantAllocationBudgetLineItemAjaxModel)
        {
            var lineItem = HttpRequestStorage.DatabaseEntities.GrantAllocationBudgetLineItems.SingleOrDefault(x =>
                x.GrantAllocationBudgetLineItemID == grantAllocationBudgetLineItemAjaxModel.GrantAllocationBudgetLineItemID);

            var costType = CostType.All.Single(x => x.CostTypeID == grantAllocationBudgetLineItemAjaxModel.CostTypeID);

            if (lineItem == null)
            {
                return Json($"There was an issue saving the {FieldDefinition.GrantAllocationBudgetLineItem.FieldDefinitionDisplayName} for {FieldDefinition.CostType.FieldDefinitionDisplayName} \"{costType.CostTypeDisplayName}\".");
            }

            lineItem.GrantAllocationBudgetLineItemAmount = grantAllocationBudgetLineItemAjaxModel.LineItemAmount;
            lineItem.GrantAllocationBudgetLineItemNote = grantAllocationBudgetLineItemAjaxModel.LineItemNote;

            HttpRequestStorage.DatabaseEntities.SaveChanges(IsolationLevel.ReadUncommitted);

            return Json($"Saved {FieldDefinition.GrantAllocationBudgetLineItem.FieldDefinitionDisplayName} for {FieldDefinition.CostType.FieldDefinitionDisplayName} \"{costType.CostTypeDisplayName}\".");
        }

        

        #endregion


        #region "Grant Allocation Notes (Includes Internal)"
        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult NewGrantAllocationNote(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var viewModel = new EditGrantAllocationNoteViewModel();
            return ViewEditNote(viewModel, EditGrantAllocationNoteType.NewNote);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantAllocationNote(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditGrantAllocationNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditGrantAllocationNoteType.NewNote);
            }
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var grantAllocationNote = GrantAllocationNote.CreateNewBlank(grantAllocation, CurrentPerson);
            viewModel.UpdateModel(grantAllocationNote, CurrentPerson, EditGrantAllocationNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantAllocationNotes.Add(grantAllocationNote);
            SetMessageForDisplay($"New {FieldDefinition.GrantAllocationNote.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditNote(EditGrantAllocationNoteViewModel viewModel, EditGrantAllocationNoteType editGrantAllocationNoteType)
        {
            var viewData = new EditGrantAllocationNoteViewData(editGrantAllocationNoteType);
            return RazorPartialView<EditGrantAllocationNote, EditGrantAllocationNoteViewData, EditGrantAllocationNoteViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FundSourceAllocationEditAsAdminFeature]
        public PartialViewResult NewGrantAllocationNoteInternal(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var viewModel = new EditGrantAllocationNoteInternalViewModel();
            return ViewEditGrantAllocationNoteInternal(viewModel, EditGrantAllocationNoteInternalType.NewNote);
        }

        [HttpPost]
        [FundSourceAllocationEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewGrantAllocationNoteInternal(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey, EditGrantAllocationNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditGrantAllocationNoteInternal(viewModel, EditGrantAllocationNoteInternalType.NewNote);
            }
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var grantAllocationNoteInternal = GrantAllocationNoteInternal.CreateNewBlank(grantAllocation, CurrentPerson);
            viewModel.UpdateModel(grantAllocationNoteInternal, CurrentPerson, EditGrantAllocationNoteType.NewNote);
            HttpRequestStorage.DatabaseEntities.GrantAllocationNoteInternals.Add(grantAllocationNoteInternal);
            SetMessageForDisplay($"New {FieldDefinition.GrantAllocationNoteInternal.GetFieldDefinitionLabel()} has been created.");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewEditGrantAllocationNoteInternal(EditGrantAllocationNoteInternalViewModel viewModel, EditGrantAllocationNoteInternalType editGrantAllocationNoteInternalType)
        {
            var viewData = new EditGrantAllocationNoteInternalViewData(editGrantAllocationNoteInternalType);
            return RazorPartialView<EditGrantAllocationNoteInternal, EditGrantAllocationNoteInternalViewData, EditGrantAllocationNoteInternalViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        public PartialViewResult EditGrantAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey)
        {
            var grantAllocationNoteInternal = fundSourceAllocationNoteInternalPrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationNoteInternalViewModel(grantAllocationNoteInternal);
            return ViewEditGrantAllocationNoteInternal(viewModel, EditGrantAllocationNoteInternalType.ExistingGrantAllocationNoteInternal);
        }

        [HttpPost]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey, EditGrantAllocationNoteInternalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditGrantAllocationNoteInternal(viewModel, EditGrantAllocationNoteInternalType.ExistingGrantAllocationNoteInternal);
            }

            var grantAllocationNoteInternal = fundSourceAllocationNoteInternalPrimaryKey.EntityObject;
            viewModel.UpdateModel(grantAllocationNoteInternal, CurrentPerson, EditGrantAllocationNoteType.ExistingGrantAllocationNote);
            HttpRequestStorage.DatabaseEntities.GrantAllocationNoteInternals.AddOrUpdate(grantAllocationNoteInternal);
            SetMessageForDisplay($"{FieldDefinition.GrantAllocationNoteInternal.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        public PartialViewResult DeleteGrantAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationNoteInternalPrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantAllocationNoteInternal(fundSourceAllocationNoteInternalPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantAllocationNoteInternal(GrantAllocationNoteInternal grantAllocationNoteInternal, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationNoteInternal.GetFieldDefinitionLabel()} created on '{grantAllocationNoteInternal.CreatedDate}' by '{grantAllocationNoteInternal.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationNoteInternalEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantAllocationNoteInternal(FundSourceAllocationNoteInternalPrimaryKey fundSourceAllocationNoteInternalPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantAllocationNoteInternal = fundSourceAllocationNoteInternalPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantAllocationNoteInternal(grantAllocationNoteInternal, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationNoteInternal.GetFieldDefinitionLabel()} created on '{grantAllocationNoteInternal.CreatedDate}' by '{grantAllocationNoteInternal.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            grantAllocationNoteInternal.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FundSourceAllocationNoteEditAsAdminFeature]
        public PartialViewResult EditGrantAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey)
        {
            var grantAllocationNote = fundSourceAllocationNotePrimaryKey.EntityObject;
            var viewModel = new EditGrantAllocationNoteViewModel(grantAllocationNote);
            return ViewEditNote(viewModel, EditGrantAllocationNoteType.ExistingGrantAllocationNote);
        }

        [HttpPost]
        [FundSourceAllocationNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditGrantAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey, EditGrantAllocationNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel, EditGrantAllocationNoteType.ExistingGrantAllocationNote);
            }

            var grantAllocationNote = fundSourceAllocationNotePrimaryKey.EntityObject;
            viewModel.UpdateModel(grantAllocationNote, CurrentPerson, EditGrantAllocationNoteType.ExistingGrantAllocationNote);
            HttpRequestStorage.DatabaseEntities.GrantAllocationNotes.AddOrUpdate(grantAllocationNote);
            SetMessageForDisplay($"{FieldDefinition.GrantAllocationNote.GetFieldDefinitionLabel()} has been updated.");
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [FundSourceAllocationNoteEditAsAdminFeature]
        public PartialViewResult DeleteGrantAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fundSourceAllocationNotePrimaryKey.PrimaryKeyValue);
            return ViewDeleteGrantAllocationNote(fundSourceAllocationNotePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteGrantAllocationNote(GrantAllocationNote grantAllocationNote, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.GrantAllocationNote.GetFieldDefinitionLabel()} created on '{grantAllocationNote.CreatedDate}' by '{grantAllocationNote.CreatedByPerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FundSourceAllocationNoteEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteGrantAllocationNote(FundSourceAllocationNotePrimaryKey fundSourceAllocationNotePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var grantAllocationNote = fundSourceAllocationNotePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteGrantAllocationNote(grantAllocationNote, viewModel);
            }

            var message = $"{FieldDefinition.GrantAllocationNote.GetFieldDefinitionLabel()} created on '{grantAllocationNote.CreatedDate}' by '{grantAllocationNote.CreatedByPerson.FullNameFirstLast}' successfully deleted.";
            grantAllocationNote.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }



        #endregion

        [FundSourceAllocationsViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationExpenditure> GrantAllocationExpendituresGridJsonData(FundSourceAllocationPrimaryKey fundSourceAllocationPrimaryKey)
        {
            var grantAllocation = fundSourceAllocationPrimaryKey.EntityObject;
            var grantAllocationExpenditures = grantAllocation.GrantAllocationExpenditures.ToList();
            var gridSpec = new GrantAllocationExpendituresGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationExpenditure>(grantAllocationExpenditures, gridSpec);
            return gridJsonNetJObjectResult;
        }



        #region Grant Allocation JSON API

        [FundSourceViewJsonApiFeature]
        public JsonNetJArrayResult GrantAllocationJsonApi()
        {
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var jsonApiGrantAllocations = GrantAllocationApiJson.MakeGrantAllocationApiJsonsFromGrantAllocations(grantAllocations, false);
            return new JsonNetJArrayResult(jsonApiGrantAllocations);
        }

        #endregion

    }
}
