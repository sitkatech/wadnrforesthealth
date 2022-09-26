/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Vendor
{
    public class IndexGridSpec : GridSpec<Models.Vendor>
    {
        public IndexGridSpec(Person currentPerson)
        {
            var hasDeletePermission = new PersonDeleteFeature().HasPermissionByPerson(currentPerson);
            Add("Vendor Name", a => a.VendorName, 100, DhtmlxGridColumnFilterType.Html);
            Add($"{Models.FieldDefinition.StatewideVendorNumber.GetFieldDefinitionLabel()}", a => a.StatewideVendorNumber, 100, DhtmlxGridColumnFilterType.Html);
            Add("Statewide Vendor Number Suffix", a => a.StatewideVendorNumberSuffix, 200, DhtmlxGridColumnFilterType.Html);
            Add("Vendor Type", a => a.VendorType, 200, DhtmlxGridColumnFilterType.Html);
            Add("Billing Agency", a => a.BillingAgency, 100, DhtmlxGridColumnFilterType.Html);
            Add("Billing Sub Agency", a => a.BillingSubAgency, 120);
            Add("Billing Fund", a => a.BillingFund, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Billing Fund Breakout", a => a.BillingFundBreakout, 75, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Vendor Address Line 1", a => a.VendorAddressLine1, 100, DhtmlxGridColumnFilterType.Html);
            Add("Vendor Address Line 2", a => a.VendorAddressLine2, 120, DhtmlxGridColumnFilterType.Html);
            Add("Vendor Address Line 3", a => a.VendorAddressLine3, 100, DhtmlxGridColumnFilterType.Html);
            Add("Vendor City", a => a.VendorCity, 75, DhtmlxGridColumnFilterType.Html);
            Add("Vendor State", a => a.VendorState, 75, DhtmlxGridColumnFilterType.Html);
            Add("Vendor Zip", a => a.VendorZip, 75, DhtmlxGridColumnFilterType.Html);
            Add("Remarks", a => a.Remarks, 75, DhtmlxGridColumnFilterType.Html);
            Add("Vendor Phone", a => a.VendorPhone.ToPhoneNumberString(), 100, DhtmlxGridColumnFilterType.Html);
            Add("Vendor Status", a => a.VendorStatus, 75, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Taxpayer ID Number", a => a.TaxpayerIdNumber, 75, DhtmlxGridColumnFilterType.Html);
            Add("Email", a => a.Email, 200, DhtmlxGridColumnFilterType.Html);
        }

        private static HtmlString GetOrganizationShortNameUrl(Person person)
        {
            if (person == null)
            {
                return new HtmlString(string.Empty);
            }

            if (person.Organization == null)
            {
                return new HtmlString(string.Empty);
            }

            return person.Organization.GetShortNameAsUrl();
        }
    }
}
