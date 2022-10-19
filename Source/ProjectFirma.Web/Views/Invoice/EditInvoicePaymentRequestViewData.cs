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
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.Invoice
{
    public class EditInvoicePaymentRequestViewData : FirmaUserControlViewData
    {

        public IEnumerable<SelectListItem> People { get; set; }
        public IEnumerable<SelectListItem> PurchaseAuthorityType { get; set; }
        public EditInvoicePaymentRequestType EditInvoicePaymentRequestType { get; set; }

        public string VendorFindUrl { get; }

        public EditInvoicePaymentRequestViewData(EditInvoicePaymentRequestType editInvoicePaymentRequestType, IEnumerable<Models.Person> people)
        {

            EditInvoicePaymentRequestType = editInvoicePaymentRequestType;
            var selectListItemLandOwnerCostShareAgreement = new SelectListItem(){Text = Models.Invoice.LandOwnerPurchaseAuthority, Value = true.ToString()};
            var selectListItemOther = new SelectListItem(){Text = "Other (Enter Agreement Number in textbox below)" , Value = false.ToString()};
            var selectListChooseOne = new SelectListItem(){Text = "<Choose one>" , Value = ""};
           
            if (EditInvoicePaymentRequestType == EditInvoicePaymentRequestType.ExistingIpr)
            {
                PurchaseAuthorityType =
                    new List<SelectListItem> { selectListItemLandOwnerCostShareAgreement, selectListItemOther };
            }
            else
            {
                PurchaseAuthorityType =
                    new List<SelectListItem> { selectListChooseOne, selectListItemLandOwnerCostShareAgreement, selectListItemOther };
            }
            VendorFindUrl = SitkaRoute<InvoiceController>.BuildUrlFromExpression(c => c.Find(string.Empty));

            People = people.OrderBy(x => x.LastName)
                .ToSelectListWithEmptyFirstRow(k => k.PersonID.ToString(), v => v.FullNameFirstLastAndOrg);
        }

    }
}
