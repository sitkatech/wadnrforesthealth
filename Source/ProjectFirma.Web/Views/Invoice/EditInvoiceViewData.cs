/*-----------------------------------------------------------------------
<copyright file="EditProjectViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
        public IEnumerable<SelectListItem> People { get; set; }
        public IEnumerable<SelectListItem> PurchaseAuthorityType { get; set; }
        public EditInvoiceType EditInvoiceType { get; set; }

        public EditInvoiceViewData(EditInvoiceType editInvoiceType, IEnumerable<Models.InvoiceApprovalStatus> invoiceApprovalStatuses, IEnumerable<Models.InvoiceStatus> invoiceStatuses, IEnumerable<Models.Person> people)
        {
            InvoiceApprovalStatuses = invoiceApprovalStatuses.ToSelectList(x => x.InvoiceApprovalStatusID.ToString(CultureInfo.InvariantCulture), y => y.InvoiceApprovalStatusName);
            EditInvoiceType = editInvoiceType;
            var selectListItemLandOwnerCostShareAgreement = new SelectListItem(){Text = Models.Invoice.LandOwnerPurchaseAuthority, Value = true.ToString()};
            var selectListItemOther = new SelectListItem(){Text = "Other (Enter Agreement Number in textbox below)" , Value = false.ToString()};
            var selectListChooseOne = new SelectListItem(){Text = "<Choose one>" , Value = ""};
           
            if (editInvoiceType == EditInvoiceType.ExistingInvoice)
            {
                InvoiceStatuses = invoiceStatuses.ToSelectList(x => x.InvoiceStatusID.ToString(), y => y.InvoiceStatusDisplayName);
                PurchaseAuthorityType =
                    new List<SelectListItem> { selectListItemLandOwnerCostShareAgreement, selectListItemOther };
            }
            else
            {
                InvoiceStatuses = invoiceStatuses.ToSelectListWithEmptyFirstRow(x => x.InvoiceStatusID.ToString(), y => y.InvoiceStatusDisplayName);
                PurchaseAuthorityType =
                    new List<SelectListItem> { selectListChooseOne, selectListItemLandOwnerCostShareAgreement, selectListItemOther };
            }
            
            People = people.OrderBy(x => x.LastName)
                .ToSelectListWithEmptyFirstRow(k => k.PersonID.ToString(), v => v.FullNameFirstLastAndOrg);
        }

    }
}
