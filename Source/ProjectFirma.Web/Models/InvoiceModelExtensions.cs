/*-----------------------------------------------------------------------
<copyright file="ProjectModelExtensions.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.Models;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class InvoiceModelExtensions
    {
        //public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.DeleteInvoice(UrlTemplate.Parameter1Int)));
        //public static string GetDeleteUrl(this Invoice invoice)
        //{
        //    return DeleteUrlTemplate.ParameterReplace(invoice.InvoiceID);
        //}

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.InvoiceDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Invoice invoice)
        {
            return DetailUrlTemplate.ParameterReplace(invoice.InvoiceID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this Invoice invoice)
        {
            return EditUrlTemplate.ParameterReplace(invoice.InvoiceID);
        }

        //public static readonly UrlTemplate<int> NewNoteUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(t => t.NewFundSourceAllocationNote(UrlTemplate.Parameter1Int)));
        //public static string GetNewNoteUrl(this FundSourceAllocation fundSourceAllocation)
        //{
        //    return NewNoteUrlTemplate.ParameterReplace(fundSourceAllocation.FundSourceAllocationID);
        //}
        public static string GetFileDownloadUrl(this Invoice invoice)
        {
            if (invoice.InvoiceFileResource != null)
            {
                var url = FileResource.FileResourceByGuidUrlTemplate;
                return url.ParameterReplace(invoice.InvoiceFileResource.FileResourceGUIDAsString);
            }

            return string.Empty;
        }

    }
}
