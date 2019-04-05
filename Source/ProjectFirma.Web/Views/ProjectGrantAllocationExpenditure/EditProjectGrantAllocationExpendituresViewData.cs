/*-----------------------------------------------------------------------
<copyright file="EditProjectFundingSourceExpendituresViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectGrantAllocationExpenditure
{
    public class EditProjectGrantAllocationExpendituresViewData : FirmaUserControlViewData
    {
        public List<int> CalendarYearRange { get; }
        public List<GrantAllocationSimple> AllGrantAllocationSimples { get; }
        public List<ProjectSimple> AllProjects { get; }
        public int? ProjectID { get; }
        public int? GrantAllocationID { get; }
        public bool FromGrantAllocation { get; }
        public bool UseFiscalYears { get; }
        public bool ShowNoExpendituresExplanation { get; }

        private EditProjectGrantAllocationExpendituresViewData(List<ProjectSimple> allProjects,
            List<GrantAllocationSimple> allGrantAllocationSimples, int? projectID, int? grantAllocationID,
            List<int> calendarYearRange, bool showNoExpendituresExplanation)
        {
            CalendarYearRange = calendarYearRange;
            AllGrantAllocationSimples = allGrantAllocationSimples;
            ProjectID = projectID;
            GrantAllocationID = grantAllocationID;
            AllProjects = allProjects;
            FromGrantAllocation = false;
            UseFiscalYears = MultiTenantHelpers.UseFiscalYears();
            ShowNoExpendituresExplanation = showNoExpendituresExplanation;
        }

        public EditProjectGrantAllocationExpendituresViewData(ProjectSimple project,
            List<GrantAllocationSimple> allGrantAllocationSimples, List<int> calendarYearRangeForExpenditures,
            bool showNoExpendituresExplanation)
            : this(new List<ProjectSimple> { project }, allGrantAllocationSimples, project.ProjectID, null, calendarYearRangeForExpenditures, showNoExpendituresExplanation)
        {
        }
    }
}
