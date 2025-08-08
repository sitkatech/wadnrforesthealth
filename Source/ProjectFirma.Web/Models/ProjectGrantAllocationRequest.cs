/*-----------------------------------------------------------------------
<copyright file="ProjectFundSourceAllocationRequest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Common;
using LtInfo.Common;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectFundSourceAllocationRequest : IAuditableEntity, IFundSourceAllocationRequestAmount
    {
        public string AuditDescriptionString
        {
            get
            {
                var project = HttpRequestStorage.DatabaseEntities.Projects.Find(ProjectID);
                var fundSourceAllocation = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.Find(FundSourceAllocationID);
                var projectName = project != null ? project.AuditDescriptionString : ViewUtilities.NotFoundString;
                var fundSourceAllocationName = fundSourceAllocation != null ? fundSourceAllocation.AuditDescriptionString : ViewUtilities.NotFoundString;
                var totalRequestAmount = TotalAmount.ToStringCurrency();
                return $"Project: {projectName}, FundSource Allocation: {fundSourceAllocationName}, Total Request Amount: {totalRequestAmount}";
            }
        }

        public bool IsMatchAndPayRelevant
        {
            get { return true; }
        }
    }
}