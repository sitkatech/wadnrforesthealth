/*-----------------------------------------------------------------------
<copyright file="EditInvoiceViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;
using MoreLinq;

namespace ProjectFirma.Web.Views.Invoice
{
    public class EditInvoiceViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> InvoiceApprovalStatuses { get; set; }
        public IEnumerable<SelectListItem> InvoiceStatuses { get; set; }

        public IEnumerable<SelectListItem> Grants { get; set; }
        public IEnumerable<SelectListItem> ProgramIndices { get; set; }
        public IEnumerable<SelectListItem> ProjectCodes { get; set; }
        public IEnumerable<SelectListItem> OrganizationCodes { get; set; }

        public EditInvoiceType EditInvoiceType { get; set; }

        public EditInvoiceViewData(EditInvoiceType editInvoiceType,
            IEnumerable<InvoiceApprovalStatus> invoiceApprovalStatuses, IEnumerable<InvoiceStatus> invoiceStatuses,
            IEnumerable<Person> people, IEnumerable<Models.FundSource> grants, IEnumerable<Models.ProgramIndex> programIndices,
            IEnumerable<Models.ProjectCode> projectCodes, IEnumerable<OrganizationCode> organizationCodes)
        {
            Grants = grants.ToSelectListWithEmptyFirstRow(x => x.FundSourceID.ToString(CultureInfo.InvariantCulture), y => $"{y.FundSourceNumber} - {y.FundSourceName}");
            ProgramIndices = programIndices.ToSelectListWithEmptyFirstRow(x => x.ProgramIndexID.ToString(CultureInfo.InvariantCulture), y => $"{y.ProgramIndexCode} - {y.ProgramIndexTitle} ({y.Biennium})");
            ProjectCodes = projectCodes.ToSelectListWithEmptyFirstRow(x => x.ProjectCodeID.ToString(CultureInfo.InvariantCulture), y => $"{y.ProjectCodeName} - {y.ProjectCodeTitle}");
            OrganizationCodes = organizationCodes.ToSelectListWithEmptyFirstRow(x => x.OrganizationCodeID.ToString(CultureInfo.InvariantCulture), y => $"{y.OrganizationCodeValue} - {y.OrganizationCodeName} ");

            InvoiceApprovalStatuses = invoiceApprovalStatuses.ToSelectList(x => x.InvoiceApprovalStatusID.ToString(CultureInfo.InvariantCulture), y => y.InvoiceApprovalStatusName);
            EditInvoiceType = editInvoiceType;

            if (editInvoiceType == EditInvoiceType.ExistingInvoice)
            {
                InvoiceStatuses = invoiceStatuses.ToSelectList(x => x.InvoiceStatusID.ToString(), y => y.InvoiceStatusDisplayName);
            }
            else
            {
                InvoiceStatuses = invoiceStatuses.ToSelectListWithEmptyFirstRow(x => x.InvoiceStatusID.ToString(), y => y.InvoiceStatusDisplayName);
            }
;
        }

    }
}
