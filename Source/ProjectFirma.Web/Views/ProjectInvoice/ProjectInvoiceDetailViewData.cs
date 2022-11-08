/*-----------------------------------------------------------------------
<copyright file="ProjectInvoiceDetailViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Invoice;

namespace ProjectFirma.Web.Views.ProjectInvoice
{
    public class ProjectInvoiceDetailViewData : FirmaViewData
    {
        public List<InvoicePaymentRequest> InvoicePaymentRequests { get; }
        public bool CanEditInvoicePaymentRequests { get; }
        public string AddInvoicePaymentRequestUrl { get; }

        public Models.Project Project { get; }

        public string InvoiceGridNamePrefix;


        public ProjectInvoiceDetailViewData(Person currentPerson, Models.Project project) : base(currentPerson)
        {
            InvoicePaymentRequests = project.InvoicePaymentRequests.ToList();
            AddInvoicePaymentRequestUrl = SitkaRoute<InvoiceController>.BuildUrlFromExpression(c => c.NewInvoicePaymentRequest(project));
            CanEditInvoicePaymentRequests = new InvoiceCreateFeature().HasPermissionByPerson(currentPerson);
            Project = project;
            InvoiceGridNamePrefix = "invoiceGridName";
        }

        public string GetDownloadInvoicePaymentRequestUrl(InvoicePaymentRequest invoicePaymentRequest)
        {
            return SitkaRoute<ReportsController>.BuildUrlFromExpression(t => t.DownloadInvoicePaymentRequest(this.Project.PrimaryKey, invoicePaymentRequest));
        }
    }
}
