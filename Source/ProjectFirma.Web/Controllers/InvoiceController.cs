/*-----------------------------------------------------------------------
<copyright file="ProjectController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Views.Invoice;
using ProjectFirma.Web.Views.Shared.InvoiceControls;
using ProjectFirma.Web.Security.Shared;
using System.Web.UI.WebControls;


namespace ProjectFirma.Web.Controllers
{
    public class InvoiceController : FirmaBaseController
    {

        [HttpGet]
        [InvoiceCreateFeature]
        public PartialViewResult New(InvoicePaymentRequestPrimaryKey invoicePaymentRequestPrimaryKey)
        {
            var viewModel = new EditInvoiceViewModel();
            return InvoiceViewEdit(viewModel, EditInvoiceType.NewInvoice);
        }

        [HttpPost]
        [InvoiceCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(InvoicePaymentRequestPrimaryKey invoicePaymentRequestPrimaryKey, EditInvoiceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return InvoiceViewEdit(viewModel, EditInvoiceType.NewInvoice);
            }

            var ipr = invoicePaymentRequestPrimaryKey.EntityObject;
            var invoiceApprovalStatus = InvoiceApprovalStatus.All.Single(g => g.InvoiceApprovalStatusID == viewModel.InvoiceApprovalStatusID);
            var invoiceMatchAmountType = InvoiceMatchAmountType.AllLookupDictionary[viewModel.InvoiceMatchAmountTypeID];
            var invoiceStatus = InvoiceStatus.AllLookupDictionary[viewModel.InvoiceStatusID];
            var invoice = Invoice.CreateNewBlank(invoiceApprovalStatus, invoiceMatchAmountType, invoiceStatus, ipr);
            viewModel.UpdateModel(invoice, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [InvoiceEditFeature]
        public PartialViewResult Edit(InvoicePrimaryKey invoicePrimaryKey)
        {
            var invoice = invoicePrimaryKey.EntityObject;
            var viewModel = new EditInvoiceViewModel(invoice);
            return InvoiceViewEdit(viewModel, EditInvoiceType.ExistingInvoice);
        }

        [HttpPost]
        [InvoiceEditFeature]
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
            var people = HttpRequestStorage.DatabaseEntities.People.GetActivePeople();
            var grants = HttpRequestStorage.DatabaseEntities.Grants.OrderBy(x => x.FundSourceNumber);
            var programIndices = HttpRequestStorage.DatabaseEntities.ProgramIndices.OrderBy(x => x.ProgramIndexCode);
            var projectCodes = HttpRequestStorage.DatabaseEntities.ProjectCodes.OrderBy(x => x.ProjectCodeName);
            var organizationCodes = HttpRequestStorage.DatabaseEntities.OrganizationCodes;
            
            var viewData = new EditInvoiceViewData(editInvoiceType, invoiceApprovalStatuses, invoiceStatuses, people, grants, programIndices, projectCodes, organizationCodes);
            return RazorPartialView<EditInvoice, EditInvoiceViewData, EditInvoiceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [InvoiceViewFeature]
        public ViewResult InvoiceDetail(InvoicePrimaryKey invoicePrimaryKey)
        {
            var invoice = invoicePrimaryKey.EntityObject;

            var taxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();
            var invoiceBasicsViewData = new InvoiceBasicsViewData(invoice, false, taxonomyLevel);
            var viewData = new Views.Invoice.DetailViewData(CurrentPerson, invoice, invoiceBasicsViewData);
            return RazorView<Views.Invoice.Detail, Views.Invoice.DetailViewData>(viewData);
        }

        [InvoicesViewFullListFeature]
        public GridJsonNetJObjectResult<Invoice> InvoiceGridJsonData(InvoicePaymentRequestPrimaryKey invoicePaymentRequestPrimaryKey)
        {
            var ipr = invoicePaymentRequestPrimaryKey.EntityObject;
            var invoices = ipr.Invoices.ToList();
            var gridSpec = new InvoiceGridSpec(CurrentPerson, invoices.Any(x => x.InvoiceFileResourceID.HasValue), ipr);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Invoice>(invoices, gridSpec);
            return gridJsonNetJObjectResult;
        }




        [HttpGet]
        [InvoiceCreateFeature]
        public PartialViewResult NewInvoicePaymentRequest(ProjectPrimaryKey projectPrimaryKey)
        {
            var viewModel = new EditInvoicePaymentRequestViewModel();
            return InvoicePaymentRequestViewEdit(viewModel, EditInvoicePaymentRequestType.NewIpr);
        }

        [HttpPost]
        [InvoiceCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewInvoicePaymentRequest(ProjectPrimaryKey projectPrimaryKey, EditInvoicePaymentRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return InvoicePaymentRequestViewEdit(viewModel, EditInvoicePaymentRequestType.NewIpr);
            }

            var invoicePaymentRequest = InvoicePaymentRequest.CreateNewBlank(projectPrimaryKey.EntityObject);
            viewModel.UpdateModel(invoicePaymentRequest, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult Find(string term)
        {
            var vendorFindResults = GetViewableVendorsFromSearchCriteria(term.Trim());
            var results = vendorFindResults.Take(VendorsCountLimit).Select(p => new ListItem(p.GetVendorNameWithFullStatewideVendorNumber(), p.VendorID.ToString(CultureInfo.InvariantCulture))).ToList();
            if (vendorFindResults.Count > VendorsCountLimit)
            {
                results.Add(
                    new ListItem(
                        $"<span style='font-weight:bold'>Displaying {VendorsCountLimit} of {vendorFindResults.Count}</span>"));
            }
            return Json(results.Select(pfr => new { label = pfr.Text, value = pfr.Value }), JsonRequestBehavior.AllowGet);
        }

        private List<Vendor> GetViewableVendorsFromSearchCriteria(string searchCriteria)
        {
            var vendorsFound = HttpRequestStorage.DatabaseEntities.Vendors.GetVendorFindResultsForVendorNameAndStatewideVendorNumber(searchCriteria).ToList();
            return vendorsFound;
        }
        
        private const int VendorsCountLimit = 20;
        
        [HttpGet]
        [InvoiceEditFeature]
        public PartialViewResult EditInvoicePaymentRequest(InvoicePaymentRequestPrimaryKey invoicePaymentRequestPrimaryKey)
        {
            var invoicePaymentRequest = invoicePaymentRequestPrimaryKey.EntityObject;
            var viewModel = new EditInvoicePaymentRequestViewModel(invoicePaymentRequest);
            return InvoicePaymentRequestViewEdit(viewModel, EditInvoicePaymentRequestType.ExistingIpr);
        }

        [HttpPost]
        [InvoiceEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInvoicePaymentRequest(InvoicePaymentRequestPrimaryKey invoicePaymentRequestPrimaryKey, EditInvoicePaymentRequestViewModel viewModel)
        {
            var invoicePaymentRequest = invoicePaymentRequestPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return InvoicePaymentRequestViewEdit(viewModel, EditInvoicePaymentRequestType.ExistingIpr);
            }

            viewModel.UpdateModel(invoicePaymentRequest, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult InvoicePaymentRequestViewEdit(EditInvoicePaymentRequestViewModel viewModel, EditInvoicePaymentRequestType editInvoiceType)
        {
            var people = HttpRequestStorage.DatabaseEntities.People.GetActiveWadnrPeople();
            var viewData = new EditInvoicePaymentRequestViewData(editInvoiceType, people);
            return RazorPartialView<EditInvoicePaymentRequest, EditInvoicePaymentRequestViewData, EditInvoicePaymentRequestViewModel>(viewData, viewModel);
        }



        #region WADNR Grant JSON API

        [InvoicesViewJsonApiFeature]
        public JsonNetJArrayResult InvoiceJsonApi()
        {
            var invoices = HttpRequestStorage.DatabaseEntities.Invoices.ToList();
            var jsonApiInvoices = InvoiceApiJson.MakeInvoiceApiJsonsFromAgreements(invoices, false);
            return new JsonNetJArrayResult(jsonApiInvoices);
        }

        #endregion


    }
}
