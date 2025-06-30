/*-----------------------------------------------------------------------
<copyright file="EditprojectGrantAllocationRequestsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectGrantAllocationRequest
{
    public class EditProjectGrantAllocationRequestsViewData : FirmaUserControlViewData
    {
        public List<GrantAllocationSimple> AllGrantAllocations { get; }
        public List<ProjectSimple> AllProjects { get; }
        public int? ProjectID { get; }
        public int? GrantAllocationID { get; }
        public bool FromGrantAllocation { get; }
        public List<SelectListItem> FundingSources { get; }
        public bool IsMatchAndPayRelevant { get; }

        public bool IsLoaProject { get; }

        private EditProjectGrantAllocationRequestsViewData(List<ProjectSimple> allProjects,
            List<GrantAllocationSimple> allGrantAllocations,
            int? projectID,
            bool isLoa,
            int? grantAllocationID)
        {
            AllGrantAllocations = allGrantAllocations;
            ProjectID = projectID;
            GrantAllocationID = grantAllocationID;
            AllProjects = allProjects;
            FundingSources = FundingSource.All.ToSelectList(x => x.FundingSourceID.ToString(), y => y.FundingSourceDisplayName).ToList();

            var displayMode = GrantAllocationID.HasValue ? EditorDisplayMode.FromGrantAllocation : EditorDisplayMode.FromProject;
            FromGrantAllocation = displayMode == EditorDisplayMode.FromGrantAllocation;
            IsMatchAndPayRelevant = false;
            if (displayMode == EditorDisplayMode.FromProject)
            {
                if (isLoa)
                {
                    IsMatchAndPayRelevant = true;
                }
            }

            IsLoaProject = isLoa;
        }

        public EditProjectGrantAllocationRequestsViewData(ProjectSimple project,
            List<GrantAllocationSimple> allGrantAllocations)
            : this(new List<ProjectSimple> { project }, allGrantAllocations, project.ProjectID, project.IsLoa, null)
        {
        }


        public enum EditorDisplayMode
        {
            FromProject,
            FromGrantAllocation
        }
    }
}
