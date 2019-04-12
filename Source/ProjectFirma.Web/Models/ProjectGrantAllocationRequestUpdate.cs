/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationRequestUpdate.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    public partial class ProjectGrantAllocationRequestUpdate : IGrantAllocationRequestAmount, IAuditableEntity
    {
        public decimal? TotalAmount { get; }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectGrantAllocationRequestUpdates = project.ProjectGrantAllocationRequests.Select(
                pgar =>
                    new ProjectGrantAllocationRequestUpdate(projectUpdateBatch, pgar.GrantAllocation)
                    {
                        SecuredAmount = pgar.SecuredAmount,
                        UnsecuredAmount = pgar.UnsecuredAmount
                    }
            ).ToList();
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectGrantAllocationRequest> allProjectGrantAllocationRequests)
        {
            var project = projectUpdateBatch.Project;
            var projectGrantAllocationExpectedFundingFromProjectUpdate = projectUpdateBatch
                .ProjectGrantAllocationRequestUpdates
                .Select(x => new ProjectGrantAllocationRequest(project.ProjectID, x.GrantAllocation.GrantAllocationID)
                    {
                        SecuredAmount = x.SecuredAmount,
                        UnsecuredAmount = x.UnsecuredAmount
                    }
                ).ToList();
            project.ProjectGrantAllocationRequests.Merge(projectGrantAllocationExpectedFundingFromProjectUpdate,
                allProjectGrantAllocationRequests,
                (x, y) => x.ProjectID == y.ProjectID && x.GrantAllocationID == y.GrantAllocationID,
                (x, y) =>
                {
                    x.SecuredAmount = y.SecuredAmount;
                    x.UnsecuredAmount = y.UnsecuredAmount;
                });
        }

        public string AuditDescriptionString
        {
            get
            {
                string grantAllocationID = this.GrantAllocation != null ? this.GrantAllocation.GrantAllocationID.ToString(): "none";
                string grantAllocationName = this.GrantAllocation != null ? this.GrantAllocation.GrantAllocationName : "none";
                return $"GrantAllocationID: {grantAllocationID}  Grant Allocation Name: {grantAllocationName} SecuredAmount: {this.SecuredAmount} UnsecuredAmount: {this.UnsecuredAmount}";
            }
        }
    }
}