/*-----------------------------------------------------------------------
<copyright file="EditProjectFundingSourceRequestsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using DocumentFormat.OpenXml.Presentation;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectFundingSourceRequest
{
    public class EditProjectFundingSourceRequestsViewData : FirmaUserControlViewData
    {
        public readonly List<FundingSourceSimple> AllGrantAllocations;
        public readonly List<ProjectSimple> AllProjects;
        public readonly int? ProjectID;
        public readonly int? GrantAllocationID;
        public readonly bool FromGrantAllocation;

        private EditProjectFundingSourceRequestsViewData(List<ProjectSimple> allProjects,
            List<FundingSourceSimple> allGrantAllocations,
            int? projectID,
            int? grantAllocationID)
        {
            AllGrantAllocations = allGrantAllocations;
            ProjectID = projectID;
            GrantAllocationID = grantAllocationID;
            AllProjects = allProjects;

            
            var displayMode = GrantAllocationID.HasValue ? EditorDisplayMode.FromGrantAllocation : EditorDisplayMode.FromProject;
            FromGrantAllocation = displayMode == EditorDisplayMode.FromGrantAllocation;
        }

        public EditProjectFundingSourceRequestsViewData(ProjectSimple project,
            List<FundingSourceSimple> allGrantAllocations)
            : this(new List<ProjectSimple> { project }, allGrantAllocations, project.ProjectID, null)
        {
        }

        public EditProjectFundingSourceRequestsViewData(FundingSourceSimple fundingSource, List<ProjectSimple> allProjects)
            : this(allProjects, new List<FundingSourceSimple> {fundingSource}, null, fundingSource.GrantAllocationID)
        {
        }

        public enum EditorDisplayMode
        {
            FromProject,
            FromGrantAllocation
        }
    }
}
