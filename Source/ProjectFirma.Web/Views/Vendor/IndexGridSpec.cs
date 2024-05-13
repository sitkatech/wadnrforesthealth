/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Linq;
using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Vendor
{
    public class IndexGridSpec : GridSpec<Models.Vendor>
    {
        public IndexGridSpec(Person currentPerson)
        {
            CustomExcelDownloadUrl = SitkaRoute<VendorController>.BuildUrlFromExpression(vc => vc.VendorsExcelDownloadImpl());
            Add("Vendor Name", a => $"{{ \"link\":\"{a.GetDetailUrl()}\",\"displayText\":\"{a.VendorName}\" }}", 200, AgGridColumnFilterType.HtmlLinkJson);
            Add("Statewide Vendor Number", a => a.StatewideVendorNumber.ToString(), 100, AgGridColumnFilterType.Html);
            Add("Statewide Vendor Number Suffix", a => a.StatewideVendorNumberSuffix.ToString(), 75, AgGridColumnFilterType.Html);
            Add("Billing Agency", a => a.BillingAgency, 75, AgGridColumnFilterType.Html);
            Add("Billing Sub Agency", a => a.BillingSubAgency, 75);
            Add("Billing Fund", a => a.BillingFund, 75, AgGridColumnFilterType.Html);
            Add("Billing Fund Breakout", a => a.BillingFundBreakout, 75, AgGridColumnFilterType.SelectFilterStrict);
            Add("Vendor Address Line 1", a => a.VendorAddressLine1, 150, AgGridColumnFilterType.Html);
            Add("Vendor Address Line 2", a => a.VendorAddressLine2, 100, AgGridColumnFilterType.Html);
            Add("Vendor Address Line 3", a => a.VendorAddressLine3, 100, AgGridColumnFilterType.Html);
            Add("Vendor City", a => a.VendorCity, 75, AgGridColumnFilterType.Html);
            Add("Vendor State", a => a.VendorState, 75, AgGridColumnFilterType.Html);
            Add("Vendor Zip", a => a.VendorZip, 75, AgGridColumnFilterType.Html);
            Add("Remarks", a => a.Remarks, 100, AgGridColumnFilterType.Html);
            Add("Vendor Phone", a => a.VendorPhone.ToPhoneNumberString(), 100, AgGridColumnFilterType.Html);
            Add("Vendor Status", a => a.VendorStatus, 75, AgGridColumnFilterType.SelectFilterStrict);
            Add("Taxpayer ID Number", a => a.TaxpayerIdNumber, 75, AgGridColumnFilterType.Html);
            Add("Email", a => a.Email, 200, AgGridColumnFilterType.Html);
        }
    }
}
