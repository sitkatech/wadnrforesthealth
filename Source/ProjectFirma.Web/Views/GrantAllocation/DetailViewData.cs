/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.DhtmlWrappers;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class DetailViewData : GrantAllocationViewData
    {
        public GrantAllocationBasicsViewData GrantAllocationBasicsViewData { get; }
        public string NewGrantAllocationNoteUrl { get; set; }
        public EntityNotesViewData GrantAllocationNotesViewData { get; set; }
        public EntityNotesViewData GrantAllocationNoteInternalsViewData { get; set; }
        public ViewGoogleChartViewData ViewGoogleChartViewData { get; }
        public ProjectCalendarYearExpendituresGridSpec ProjectCalendarYearExpendituresGridSpec { get; }
        public string ProjectCalendarYearExpendituresGridName { get; }
        public string ProjectCalendarYearExpendituresGridDataUrl { get; }
        public List<int> CalendarYearsForProjectExpenditures { get; }

        public GridSpec<Models.ProjectGrantAllocationRequest> ProjectGrantAllocationRequestsGridSpec { get; }
        public string ProjectGrantAllocationRequestsGridName { get; }
        public string ProjectGrantAllocationRequestsGridDataUrl { get; }

        public GrantAllocationBudgetLineItemsViewData GrantAllocationBudgetLineItemsViewData { get; }

        public DetailViewData(Person currentPerson, Models.GrantAllocation grantAllocation
            , GrantAllocationBasicsViewData grantAllocationBasicsViewData
            , EntityNotesViewData grantAllocationNotesViewData
            , EntityNotesViewData grantAllocationNoteInternalsViewData
            , ViewGoogleChartViewData viewGoogleChartViewData
            , GridSpec<Models.ProjectGrantAllocationRequest> projectGrantAllocationRequestsGridSpec)
            : base(currentPerson, grantAllocation)
        {
            PageTitle = grantAllocation.GrantAllocationName.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Detail";

            GrantAllocationBasicsViewData = grantAllocationBasicsViewData;
            GrantAllocationNotesViewData = grantAllocationNotesViewData;

            NewGrantAllocationNoteUrl = grantAllocation.GetNewNoteUrl();
            GrantAllocationNoteInternalsViewData = grantAllocationNoteInternalsViewData;

            ViewGoogleChartViewData = viewGoogleChartViewData;

            var projectGrantAllocationExpenditures = GrantAllocation.ProjectGrantAllocationExpenditures.ToList();
            CalendarYearsForProjectExpenditures = projectGrantAllocationExpenditures.CalculateCalendarYearRangeForExpenditures(grantAllocation);

            ProjectCalendarYearExpendituresGridSpec = new ProjectCalendarYearExpendituresGridSpec(CalendarYearsForProjectExpenditures)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            ProjectCalendarYearExpendituresGridName = "projectsCalendarYearExpendituresFromGrantAllocationGrid";
            ProjectCalendarYearExpendituresGridDataUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(tc => tc.ProjectCalendarYearExpendituresGridJsonData(grantAllocation));

            ProjectGrantAllocationRequestsGridSpec = projectGrantAllocationRequestsGridSpec;
            ProjectGrantAllocationRequestsGridName = "projectsGrantAllocationRequestsFromGrantAllocationGrid";
            ProjectGrantAllocationRequestsGridDataUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(tc => tc.ProjectGrantAllocationRequestsGridJsonData(grantAllocation));

            GrantAllocationBudgetLineItemsViewData = new GrantAllocationBudgetLineItemsViewData(currentPerson, grantAllocation);
        }
    }
}
