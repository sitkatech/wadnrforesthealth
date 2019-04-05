/*-----------------------------------------------------------------------
<copyright file="ProjectFundingSourceExpenditureUpdate.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectGrantAllocationExpenditureUpdate : IGrantAllocationExpenditure, IAuditableEntity
    {
        public decimal? MonetaryAmount
        {
            get { return ExpenditureAmount; }
        }

        public string ExpenditureAmountDisplay
        {
            get { return ExpenditureAmount.ToStringCurrency(); }
        }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates = project.ProjectGrantAllocationExpenditures.Select(pfse => MakeNewProjectGrantAllocationExpenditureUpdate(projectUpdateBatch, pfse)).ToList();
        }

        private static ProjectGrantAllocationExpenditureUpdate MakeNewProjectGrantAllocationExpenditureUpdate(ProjectUpdateBatch projectUpdateBatch, ProjectGrantAllocationExpenditure projectGrantAllocationExpenditure)
        {
            return new ProjectGrantAllocationExpenditureUpdate(projectUpdateBatch,
                projectGrantAllocationExpenditure.CalendarYear,
                projectGrantAllocationExpenditure.ExpenditureAmount,
                projectGrantAllocationExpenditure.GrantAllocation);
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectGrantAllocationExpenditure> allProjectGrantAllocationExpenditures)
        {
            var project = projectUpdateBatch.Project;
            var projectGrantAllocationExpendituresFromProjectUpdate =
                projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.Select(
                    x => new ProjectGrantAllocationExpenditure(project.ProjectID, x.CalendarYear, x.ExpenditureAmount, x.GrantAllocationID)).ToList();
            project.ProjectGrantAllocationExpenditures.Merge(projectGrantAllocationExpendituresFromProjectUpdate,
                allProjectGrantAllocationExpenditures,
                (x, y) => x.ProjectID == y.ProjectID && x.CalendarYear == y.CalendarYear && x.GrantAllocationID == y.GrantAllocationID,
                (x, y) => x.ExpenditureAmount = y.ExpenditureAmount);
        }

        public string AuditDescriptionString
        {
            get
            {
                string grantAllocationID = this.GrantAllocation != null ? this.GrantAllocation.GrantAllocationID.ToString() : "none";
                string grantAllocationName = this.GrantAllocation != null ? this.GrantAllocation.GrantAllocationName : "none";
                return $"GrantAllocationID: {grantAllocationID}  Grant Allocation Name: {grantAllocationName} ExpenditureAmount: {this.ExpenditureAmountDisplay}";
            }
        }
    }
}
