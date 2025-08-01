/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationRequestUpdate.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public partial class ProjectGrantAllocationRequestUpdate : IGrantAllocationRequestAmount, IAuditableEntity
    {

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectGrantAllocationRequestUpdates = project.ProjectGrantAllocationRequests.Select(
                pgar =>
                    new ProjectGrantAllocationRequestUpdate(projectUpdateBatch, pgar.FundSourceAllocation, pgar.CreateDate, pgar.ImportedFromTabularData)
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

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectGrantAllocationRequest> allProjectGrantAllocationRequests)
        {
            var project = projectUpdateBatch.Project;
            var projectGrantAllocationExpectedFundingFromProjectUpdate = projectUpdateBatch
                .ProjectGrantAllocationRequestUpdates
                .Select(x => new ProjectGrantAllocationRequest(project.ProjectID, x.FundSourceAllocation.GrantAllocationID, x.CreateDate, x.ImportedFromTabularData)
                    {
                        TotalAmount = x.TotalAmount,
                        PayAmount = x.PayAmount,
                        MatchAmount = x.MatchAmount,
                        UpdateDate = x.UpdateDate
                    }
                ).ToList();
            project.ProjectGrantAllocationRequests.Merge(projectGrantAllocationExpectedFundingFromProjectUpdate,
                allProjectGrantAllocationRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.GrantAllocationID == y.GrantAllocationID,
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
                string grantAllocationID = this.FundSourceAllocation != null ? this.FundSourceAllocation.GrantAllocationID.ToString(): "none";
                string grantAllocationName = this.FundSourceAllocation != null ? this.FundSourceAllocation.GrantAllocationName : "none";
                return $"GrantAllocationID: {grantAllocationID}  Grant Allocation Name: {grantAllocationName} TotalAmount: {this.TotalAmount}";
            }
        }

        public bool IsMatchAndPayRelevant
        {
            get { return true; }
        }
    }
}