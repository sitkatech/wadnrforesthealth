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

using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Grant;
using ProjectFirma.Web.Views.Invoice;

namespace ProjectFirma.Web.Controllers
{
    public class InvoiceController : FirmaBaseController
    {
        [InvoicesViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullInvoiceList);
            var viewData = new InvoiceIndexViewData(CurrentPerson, firmaPage);
            return RazorView<InvoiceIndex, InvoiceIndexViewData>(viewData);
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
