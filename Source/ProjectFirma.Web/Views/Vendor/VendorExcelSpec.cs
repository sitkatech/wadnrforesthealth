/*-----------------------------------------------------------------------
<copyright file="VendorExcelSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Vendor
{
    public class VendorExcelSpec : ExcelWorksheetSpec<Models.Vendor>
    {
        public VendorExcelSpec()
        {
            AddColumn("Vendor ID", a => a.VendorID.ToString());
            AddColumn("Vendor Type", a => a.VendorType);
            AddColumn("Billing Agency", a => a.BillingAgency);
            AddColumn("Billing Sub Agency", a => a.BillingSubAgency);
            AddColumn("Billing Fund", a => a.BillingFund);
            AddColumn("Billing Fund Breakout", a => a.BillingFundBreakout);
            AddColumn("Vendor Address Line 1", a => a.VendorAddressLine1);
            AddColumn("Vendor Address Line 2", a => a.VendorAddressLine2);
            AddColumn("Vendor Address Line 3", a => a.VendorAddressLine3);
            AddColumn("Vendor City", a => a.VendorCity);
            AddColumn("Vendor State", a => a.VendorState);
            AddColumn("Vendor Zip", a => a.VendorZip);
            AddColumn("Remarks", a => a.Remarks);
            AddColumn("Vendor Phone", a => a.VendorPhone.ToPhoneNumberString());
            AddColumn("Vendor Status", a => a.VendorStatus);
            AddColumn("Taxpayer ID Number", a => a.TaxpayerIdNumber);
            AddColumn("Email", a => a.Email);
        }
    }
}
