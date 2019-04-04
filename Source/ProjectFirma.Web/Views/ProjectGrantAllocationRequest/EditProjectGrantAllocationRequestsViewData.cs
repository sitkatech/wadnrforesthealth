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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectGrantAllocationRequest
{
    public class EditProjectGrantAllocationRequestsViewData : FirmaUserControlViewData
    {
        public readonly List<GrantAllocationSimple> AllGrantAllocations;
        public readonly List<ProjectSimple> AllProjects;
        public readonly int? ProjectID;
        public readonly int? GrantAllocationID;
        public readonly bool FromGrantAllocation;

        private EditProjectGrantAllocationRequestsViewData(List<ProjectSimple> allProjects,
            List<GrantAllocationSimple> allGrantAllocations,
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

        public EditProjectGrantAllocationRequestsViewData(ProjectSimple project,
            List<GrantAllocationSimple> allGrantAllocations)
            : this(new List<ProjectSimple> { project }, allGrantAllocations, project.ProjectID, null)
        {
        }

        public EditProjectGrantAllocationRequestsViewData(GrantAllocationSimple grantAllocation, List<ProjectSimple> allProjects)
            : this(allProjects, new List<GrantAllocationSimple> {grantAllocation}, null, grantAllocation.GrantAllocationID)
        {
        }

        public enum EditorDisplayMode
        {
            FromProject,
            FromGrantAllocation
        }
    }
}
