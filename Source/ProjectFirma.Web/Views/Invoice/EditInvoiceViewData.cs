/*-----------------------------------------------------------------------
<copyright file="EditInvoiceViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    //public int? GrantID { get; set; }
    //public int? ProgramIndexID { get; set; }
    //public int? ProjectCodeID { get; set; }
    //public int OrganizationCodeID { get; set; }
    //public string InvoiceNumber { get; set; }
    //public string Fund { get; set; }
    //public string Appn { get; set; }
    //public string SubObject { get; set; }
    public class EditInvoiceViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> InvoiceApprovalStatuses { get; set; }
        public IEnumerable<SelectListItem> InvoiceStatuses { get; set; }
        public IEnumerable<SelectListItem> People { get; set; }
        public IEnumerable<SelectListItem> PurchaseAuthorityType { get; set; }
        public IEnumerable<SelectListItem> Grants { get; set; }
        public IEnumerable<SelectListItem> ProgramIndices { get; set; }
        public IEnumerable<SelectListItem> ProjectCodes { get; set; }
        public IEnumerable<SelectListItem> OrganizationCodes { get; set; }

        public EditInvoiceType EditInvoiceType { get; set; }

        public EditInvoiceViewData(EditInvoiceType editInvoiceType,
            IEnumerable<InvoiceApprovalStatus> invoiceApprovalStatuses, IEnumerable<InvoiceStatus> invoiceStatuses,
            IEnumerable<Person> people, IEnumerable<Models.Grant> grants, IEnumerable<Models.ProgramIndex> programIndices,
            IEnumerable<Models.ProjectCode> projectCodes, IEnumerable<OrganizationCode> organizationCodes)
        {
            Grants = grants.ToSelectListWithEmptyFirstRow(x => x.GrantID.ToString(CultureInfo.InvariantCulture), y => y.GrantNumber);
            ProgramIndices = programIndices.ToSelectListWithEmptyFirstRow(x => x.ProgramIndexID.ToString(CultureInfo.InvariantCulture), y => y.ProgramIndexCode);
            ProjectCodes = projectCodes.ToSelectListWithEmptyFirstRow(x => x.ProjectCodeID.ToString(CultureInfo.InvariantCulture), y => y.ProjectCodeTitle);
            OrganizationCodes = organizationCodes.ToSelectListWithEmptyFirstRow(x => x.OrganizationCodeID.ToString(CultureInfo.InvariantCulture), y => $"{y.OrganizationCodeName} {y.OrganizationCodeValue}");

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
