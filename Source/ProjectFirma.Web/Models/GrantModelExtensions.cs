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
using System.Linq;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantModelExtensions
    {

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.DeleteGrant(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Grant grant)
        {
            return DeleteUrlTemplate.ParameterReplace(grant.GrantID);
        }


        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.GrantDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Grant grant)
        {
            return DetailUrlTemplate.ParameterReplace(grant.GrantID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this Grant grant)
        {
            return EditUrlTemplate.ParameterReplace(grant.GrantID);
        }

        public static readonly UrlTemplate<int> DuplicateUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.Duplicate(UrlTemplate.Parameter1Int)));
        public static string GetDuplicateUrl(this Grant grant)
        {
            return DuplicateUrlTemplate.ParameterReplace(grant.GrantID);
        }

        public static readonly UrlTemplate<int> NewNoteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.NewGrantNote(UrlTemplate.Parameter1Int)));
        public static string GetNewNoteUrl(this Grant grant)
        {
            return NewNoteUrlTemplate.ParameterReplace(grant.GrantID);
        }

        public static HtmlString GetGrantNumberAsUrl(this Grant grant)
        {
            return grant != null ? UrlTemplate.MakeHrefString(grant.GetDetailUrl(), grant.GrantNumber) : new HtmlString(null);
        }

        public static Money GetCurrentBalanceOfGrantBasedOnAllGrantAllocationExpenditures(this Grant grant)
        {
            var grantAllocations = grant.GrantAllocations.ToList();
            var allBudgetLineItemVsActualItems = grantAllocations.Select(ga => ga.GetTotalBudgetVsActualLineItem());
            var currentBalanceOfAllGrantAllocations = allBudgetLineItemVsActualItems.Select(blai => blai.BudgetMinusExpendituresFromDatamart).ToList();
            Money total = 0;
            foreach (var grantAllocationTotal in currentBalanceOfAllGrantAllocations)
            {
                total += grantAllocationTotal;
            }

            return total;
        }
    }
}
