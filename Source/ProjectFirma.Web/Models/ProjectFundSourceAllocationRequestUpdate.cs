/*-----------------------------------------------------------------------
<copyright file="ProjectFundSourceAllocationRequestUpdate.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectFundSourceAllocationRequestUpdate : IFundSourceAllocationRequestAmount, IAuditableEntity
    {

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectFundSourceAllocationRequestUpdates = project.ProjectFundSourceAllocationRequests.Select(
                pgar =>
                    new ProjectFundSourceAllocationRequestUpdate(projectUpdateBatch, pgar.FundSourceAllocation, pgar.CreateDate, pgar.ImportedFromTabularData)
                    {
                        TotalAmount = pgar.TotalAmount,
                        MatchAmount = pgar.MatchAmount,
                        PayAmount = pgar.PayAmount,
                        UpdateDate = pgar.UpdateDate
                    }
            ).ToList();

            projectUpdateBatch.ProjectFundingSourceUpdates = project.ProjectFundingSources.Select(
                pfs => new ProjectFundingSourceUpdate(projectUpdateBatch, pfs.FundingSource)
            ).ToList();

        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectFundSourceAllocationRequest> allProjectFundSourceAllocationRequests)
        {
            var project = projectUpdateBatch.Project;
            var projectFundSourceAllocationExpectedFundingFromProjectUpdate = projectUpdateBatch
                .ProjectFundSourceAllocationRequestUpdates
                .Select(x => new ProjectFundSourceAllocationRequest(project.ProjectID, x.FundSourceAllocation.FundSourceAllocationID, x.CreateDate, x.ImportedFromTabularData)
                    {
                        TotalAmount = x.TotalAmount,
                        PayAmount = x.PayAmount,
                        MatchAmount = x.MatchAmount,
                        UpdateDate = x.UpdateDate
                    }
                ).ToList();
            project.ProjectFundSourceAllocationRequests.Merge(projectFundSourceAllocationExpectedFundingFromProjectUpdate,
                allProjectFundSourceAllocationRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.FundSourceAllocationID == y.FundSourceAllocationID,
                (x, y) =>
                {
                    x.TotalAmount = y.TotalAmount;
                    x.MatchAmount = y.MatchAmount;
                    x.PayAmount = y.PayAmount;
                    x.CreateDate = y.CreateDate;
                    x.UpdateDate = y.UpdateDate;
                    x.ImportedFromTabularData = y.ImportedFromTabularData;
                });
        }

        public string AuditDescriptionString
        {
            get
            {
                string fundSourceAllocationID = this.FundSourceAllocation != null ? this.FundSourceAllocation.FundSourceAllocationID.ToString(): "none";
                string fundSourceAllocationName = this.FundSourceAllocation != null ? this.FundSourceAllocation.FundSourceAllocationName : "none";
                return $"FundSourceAllocationID: {fundSourceAllocationID}  FundSource Allocation Name: {fundSourceAllocationName} TotalAmount: {this.TotalAmount}";
            }
        }

        public bool IsMatchAndPayRelevant
        {
            get { return true; }
        }
    }
}