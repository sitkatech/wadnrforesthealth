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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.FundingSource;
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
        public readonly List<int> CalendarYearsForProjectExpenditures;

        public DetailViewData(Person currentPerson, Models.GrantAllocation grantAllocation
            , GrantAllocationBasicsViewData grantAllocationBasicsViewData
            , EntityNotesViewData grantAllocationNotesViewData
            , EntityNotesViewData grantAllocationNoteInternalsViewData
            , ViewGoogleChartViewData viewGoogleChartViewData)
            : base(currentPerson, grantAllocation)
        {
            PageTitle = grantAllocation.GrantAllocationName.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Detail";

            GrantAllocationBasicsViewData = grantAllocationBasicsViewData;
            GrantAllocationNotesViewData = grantAllocationNotesViewData;

            NewGrantAllocationNoteUrl = grantAllocation.GetNewNoteUrl();
            GrantAllocationNoteInternalsViewData = grantAllocationNoteInternalsViewData;

            ViewGoogleChartViewData = viewGoogleChartViewData;


            var projectFundingSourceExpenditures = GrantAllocation.ProjectFundingSourceExpenditures.ToList();
            CalendarYearsForProjectExpenditures = projectFundingSourceExpenditures.CalculateCalendarYearRangeForExpenditures(grantAllocation);

            ProjectCalendarYearExpendituresGridSpec = new ProjectCalendarYearExpendituresGridSpec(CalendarYearsForProjectExpenditures)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            ProjectCalendarYearExpendituresGridName = "projectsCalendarYearExpendituresFromFundingSourceGrid";
            ProjectCalendarYearExpendituresGridDataUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(tc => tc.ProjectCalendarYearExpendituresGridJsonData(grantAllocation));
        }
    }
}
