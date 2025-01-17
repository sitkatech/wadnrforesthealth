﻿/*-----------------------------------------------------------------------
<copyright file="ExpendituresViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.ExpenditureAndBudgetControls;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class ExpendituresViewData : ProjectCreateViewData
    {
        public readonly string RefreshUrl;
        public readonly string DiffUrl;
        public readonly ProjectExpendituresDetailViewData ProjectExpendituresDetailViewData;
        public readonly string RequestGrantAllocationUrl;
        public readonly ViewDataForAngularClass ViewDataForAngular;
        
        public ExpendituresViewData(Person currentPerson, Models.Project project, ViewDataForAngularClass viewDataForAngularClass, ProjectExpendituresDetailViewData projectExpendituresDetailViewData, ProposalSectionsStatus proposalSectionsStatus)
            : base(currentPerson, project, "THIS IS BROKEN!" /* 5/15/2019 TK-  ProjectCreateSection.ReportedExpenditures.ProjectCreateSectionDisplayName*/, proposalSectionsStatus)
        {
            ViewDataForAngular = viewDataForAngularClass;
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshExpenditures(project));
            DiffUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.DiffExpenditures(project));
            RequestGrantAllocationUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.MissingGrantAllocation());
            ProjectExpendituresDetailViewData = projectExpendituresDetailViewData;
        }

        public class ViewDataForAngularClass
        {
            public readonly List<int> CalendarYearRange;
            public readonly List<GrantAllocationSimple> AllGrantAllocations;
            public readonly int ProjectID;
            public readonly int MaxYear;
            public readonly bool UseFiscalYears;
            public readonly bool ShowNoExpendituresExplanation;

            public ViewDataForAngularClass(Models.Project project,
                List<GrantAllocationSimple> allGrantAllocations,
                List<int> calendarYearRange, bool showNoExpendituresExplanation)
            {
                CalendarYearRange = calendarYearRange;
                AllGrantAllocations = allGrantAllocations;
                ProjectID = project.ProjectID;
                
                MaxYear = FirmaDateUtilities.CalculateCurrentYearToUseForUpToAllowableInputInReporting();
                UseFiscalYears = MultiTenantHelpers.UseFiscalYears();
                ShowNoExpendituresExplanation = showNoExpendituresExplanation;
            }
        }
    }
}
