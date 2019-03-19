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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Agreement;
using ProjectFirma.Web.Views.Grant;
using ProjectFirma.Web.Views.Invoice;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.InvoiceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Controllers
{
    public class InvoiceController : FirmaBaseController
    {

        [HttpGet]
        [InvoiceLineItemDeleteFeature]
        public PartialViewResult DeleteInvoiceLineItem(InvoiceLineItemPrimaryKey invoiceLineItemPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(invoiceLineItemPrimaryKey.PrimaryKeyValue);
            return ViewDeleteInvoiceLineItem(invoiceLineItemPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteInvoiceLineItem(InvoiceLineItem invoiceLineItem, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to remove this {FieldDefinition.Invoice.GetFieldDefinitionLabel()} Line Item from this {FieldDefinition.Invoice.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [InvoiceLineItemDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteInvoiceLineItem(InvoiceLineItemPrimaryKey invoiceLineItemPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var invoiceLineItem = invoiceLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteInvoiceLineItem(invoiceLineItem, viewModel);
            }

            var message = $"{FieldDefinition.Invoice.GetFieldDefinitionLabel()} Line Item successfully removed from this {FieldDefinition.Invoice.GetFieldDefinitionLabel()}.";

            invoiceLineItem.DeleteFull(HttpRequestStorage.DatabaseEntities);

            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [InvoiceEditAsAdminFeature]
        public PartialViewResult NewInvoiceLineItem(InvoicePrimaryKey invoicePrimaryKey)
        {
            var invoiceID = invoicePrimaryKey.EntityObject.InvoiceID;
            var viewModel = new EditInvoiceLineItemViewModel(invoiceID);
            return ViewEditInvoiceLineItem(viewModel);
        }

        [HttpPost]
        [InvoiceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewInvoiceLineItem(InvoicePrimaryKey invoicePrimaryKey, EditInvoiceLineItemViewModel viewModel)
        {
            var invoiceID = invoicePrimaryKey.EntityObject.InvoiceID;
            if (!ModelState.IsValid)
            {
                return ViewEditInvoiceLineItem(viewModel);
            }

            var invoiceLineItem = new InvoiceLineItem(invoiceID, viewModel.GrantAllocationID, viewModel.CostTypeID,
                viewModel.InvoiceLineItemAmount);
            viewModel.UpdateModel(invoiceLineItem);
            HttpRequestStorage.DatabaseEntities.InvoiceLineItems.Add(invoiceLineItem);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Invoice Line Item successfully added to this {FieldDefinition.Invoice.GetFieldDefinitionLabel()}.");

            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [InvoiceLineItemEditAsAdminFeature]
        public PartialViewResult EditInvoiceLineItem(InvoiceLineItemPrimaryKey invoiceLineItemPrimaryKey)
        {
            var invoiceLineItem = invoiceLineItemPrimaryKey.EntityObject;
            var viewModel = new EditInvoiceLineItemViewModel(invoiceLineItem);
            return ViewEditInvoiceLineItem(viewModel);
        }

        [HttpPost]
        [InvoiceLineItemEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInvoiceLineItem(InvoiceLineItemPrimaryKey invoiceLineItemPrimaryKey, EditInvoiceLineItemViewModel viewModel)
        {
            var invoiceLineItem = invoiceLineItemPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInvoiceLineItem(viewModel);
            }
            viewModel.UpdateModel(invoiceLineItem);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInvoiceLineItem(EditInvoiceLineItemViewModel viewModel)
        {
            var costTypes = HttpRequestStorage.DatabaseEntities.CostTypes.Where(x => x.CostTypeID < 5).ToList();
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var viewData = new EditInvoiceLineItemViewData(grantAllocations, costTypes);
            return RazorPartialView<EditInvoiceLineItem, EditInvoiceLineItemViewData, EditInvoiceLineItemViewModel>(viewData, viewModel);
        }

        [InvoiceViewFeature]
        public GridJsonNetJObjectResult<InvoiceLineItem> InvoiceLineItemGridJsonData(InvoicePrimaryKey invoicePrimaryKey)
        {
            var invoiceID = invoicePrimaryKey.EntityObject.InvoiceID;
            var gridSpec = new InvoiceLineItemGridSpec(CurrentPerson);
            var invoice = HttpRequestStorage.DatabaseEntities.Invoices.FirstOrDefault(x => x.InvoiceID == invoiceID);
            var invoiceLineItems = invoice != null
                ? invoice.InvoiceLineItems.OrderBy(i => i.CostType.CostTypeDescription).ToList()
                : new List<InvoiceLineItem>();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InvoiceLineItem>(invoiceLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [HttpGet]
        [AgreementCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditInvoiceViewModel();
            return InvoiceViewEdit(viewModel, EditInvoiceType.NewInvoice);
        }

        [HttpPost]
        [AgreementCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditInvoiceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return InvoiceViewEdit(viewModel, EditInvoiceType.NewInvoice);
            }

            var preparedByPerson = HttpRequestStorage.DatabaseEntities.People.Single(g => g.PersonID == viewModel.PreparedByPersonID);
            var invoiceApprovalStatus = InvoiceApprovalStatus.All.Single(g => g.InvoiceApprovalStatusID == viewModel.InvoiceApprovalStatusID);
            var invoiceMatchAmountType = InvoiceMatchAmountType.AllLookupDictionary[viewModel.InvoiceMatchAmountTypeID];
            var invoiceStatus = InvoiceStatus.AllLookupDictionary[viewModel.InvoiceStatusID];
            var invoice = Invoice.CreateNewBlank(preparedByPerson, invoiceApprovalStatus, invoiceMatchAmountType, invoiceStatus);
            viewModel.UpdateModel(invoice, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        [InvoicesViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullInvoiceList);
            var invoices = HttpRequestStorage.DatabaseEntities.Invoices.ToList();
            var viewData = new InvoiceIndexViewData(CurrentPerson, firmaPage, invoices.Any(x => x.InvoiceFileResourceID.HasValue));
            return RazorView<InvoiceIndex, InvoiceIndexViewData>(viewData);
        }

        [HttpGet]
        [InvoiceEditAsAdminFeature]
        public PartialViewResult Edit(InvoicePrimaryKey invoicePrimaryKey)
        {
            var invoice = invoicePrimaryKey.EntityObject;
            var viewModel = new EditInvoiceViewModel(invoice);
            return InvoiceViewEdit(viewModel, EditInvoiceType.ExistingInvoice);
        }

        [HttpPost]
        [InvoiceEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(InvoicePrimaryKey invoicePrimaryKey, EditInvoiceViewModel viewModel)
        {
            var invoice = invoicePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return InvoiceViewEdit(viewModel, EditInvoiceType.ExistingInvoice);
            }

            viewModel.UpdateModel(invoice, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult InvoiceViewEdit(EditInvoiceViewModel viewModel, EditInvoiceType editInvoiceType)
        {
            var invoiceApprovalStatuses = InvoiceApprovalStatus.All;
            var invoiceStatuses = InvoiceStatus.All.OrderBy(x => x.InvoiceStatusID).ToList();
            var people =  HttpRequestStorage.DatabaseEntities.People.GetActivePeople();
            var viewData = new EditInvoiceViewData(editInvoiceType, invoiceApprovalStatuses, invoiceStatuses, people);
            return RazorPartialView<EditInvoice, EditInvoiceViewData, EditInvoiceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [InvoiceViewFeature]
        public ViewResult InvoiceDetail(InvoicePrimaryKey invoicePrimaryKey)
        {
            var invoice =
                HttpRequestStorage.DatabaseEntities.Invoices.SingleOrDefault(i =>
                    i.InvoiceID == invoicePrimaryKey.PrimaryKeyValue);
            if (invoice == null)
            {
                throw new Exception(
                    $"Could not find InvoiceID # {invoicePrimaryKey.PrimaryKeyValue}; has it been deleted?");
            }

            var taxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();
            var invoiceBasicsViewData = new InvoiceBasicsViewData(invoice, false, taxonomyLevel);
            var viewData = new Views.Invoice.DetailViewData(CurrentPerson, invoice, invoiceBasicsViewData);
            return RazorView<Views.Invoice.Detail, Views.Invoice.DetailViewData>(viewData);
        }

        [InvoicesViewFullListFeature]
        public GridJsonNetJObjectResult<Invoice> InvoiceGridJsonData()
        {
            var invoices = HttpRequestStorage.DatabaseEntities.Invoices.ToList();
            var gridSpec = new InvoiceGridSpec(CurrentPerson, invoices.Any(x => x.InvoiceFileResourceID.HasValue));
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Invoice>(invoices, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
