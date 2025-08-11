/*-----------------------------------------------------------------------
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
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.ProjectFunding;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ExpectedFundingViewData : ProjectUpdateViewData
    {
        public string RefreshUrl { get; }
        public string DiffUrl { get; }
        public ProjectFundingDetailViewData ProjectFundingDetailViewData { get; set; }
        public List<SelectListItem> FundingSources { get; }

        public string RequestFundSourceAllocationUrl { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public SectionCommentsViewData SectionCommentsViewData { get; }

        public ExpectedFundingViewData(Person currentPerson, ProjectUpdateBatch projectUpdateBatch, ViewDataForAngularClass viewDataForAngularClass, ProjectFundingDetailViewData projectFundingDetailViewData, UpdateStatus updateStatus, ExpectedFundingValidationResult expectedFundingValidationResult)
            : base(currentPerson, projectUpdateBatch, updateStatus, expectedFundingValidationResult.GetWarningMessages(), ProjectUpdateSection.ExpectedFunding.ProjectUpdateSectionDisplayName)
        {
            ViewDataForAngular = viewDataForAngularClass;
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshExpectedFunding(projectUpdateBatch.Project));
            DiffUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.DiffExpectedFunding(projectUpdateBatch.Project));
            RequestFundSourceAllocationUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.MissingFundSourceAllocation());
            ProjectFundingDetailViewData = projectFundingDetailViewData;
            SectionCommentsViewData = new SectionCommentsViewData(projectUpdateBatch.ExpectedFundingComment, projectUpdateBatch.IsReturned);
            ValidationWarnings = expectedFundingValidationResult.GetWarningMessages();
            FundingSources = FundingSource.All.ToSelectList(x => x.FundingSourceID.ToString(), y => y.FundingSourceDisplayName).ToList();

        }

        public class ViewDataForAngularClass
        {
            public readonly List<FundSourceAllocationSimple> AllFundSourceAllocationSimples;
            // Actually a ProjectUpdateBatchID
            public readonly int ProjectID;
            public readonly decimal EstimatedTotalCost;

            public ViewDataForAngularClass(ProjectUpdateBatch projectUpdateBatch,
                List<FundSourceAllocationSimple> allFundSourceAllocationSimples,
                decimal estimatedTotalCost)
            {
                AllFundSourceAllocationSimples = allFundSourceAllocationSimples;
                ProjectID = projectUpdateBatch.ProjectUpdateBatchID;
                EstimatedTotalCost = estimatedTotalCost;
            }
        }
    }
}
