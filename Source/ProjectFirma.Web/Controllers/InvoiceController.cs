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
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Grant;
using ProjectFirma.Web.Views.Invoice;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.InvoiceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Controllers
{
    public class InvoiceController : FirmaBaseController
    {

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
            var agreement = Invoice.CreateNewBlank(preparedByPerson, invoiceApprovalStatus, invoiceMatchAmountType, invoiceStatus);
            viewModel.UpdateModel(agreement, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        [InvoicesViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullInvoiceList);
            var viewData = new InvoiceIndexViewData(CurrentPerson, firmaPage);
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
            var gridSpec = new InvoiceGridSpec(CurrentPerson);
            var invoices = HttpRequestStorage.DatabaseEntities.Invoices.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Invoice>(invoices, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
