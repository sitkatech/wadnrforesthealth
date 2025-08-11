/*-----------------------------------------------------------------------
<copyright file="ExpectedFundingViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class ExpectedFundingViewData : ProjectCreateViewData
    {
        public string RequestFundSourceAllocationUrl { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public List<SelectListItem> FundingSources { get; }

        public ExpectedFundingViewData(Person currentPerson,
            Models.Project project,
            ProposalSectionsStatus proposalSectionsStatus,
            ViewDataForAngularClass viewDataForAngularClass) : base(currentPerson, project, ProjectCreateSection.ExpectedFunding.ProjectCreateSectionDisplayName, proposalSectionsStatus)
        {
            ViewDataForAngular = viewDataForAngularClass;
            RequestFundSourceAllocationUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.MissingFundSourceAllocation());
            FundingSources = FundingSource.All.ToSelectList(x => x.FundingSourceID.ToString(), y => y.FundingSourceDisplayName).ToList();
        }

        public class ViewDataForAngularClass
        {
            public readonly List<FundSourceAllocationSimple> AllFundSourceAllocations;
            // Actually a ProjectID
            public readonly int ProjectID;
            public readonly decimal EstimatedTotalCost;


            public ViewDataForAngularClass(Models.Project projectProposedBatch,
                                           List<FundSourceAllocationSimple> allFundSourceAllocations,
                                           decimal estimatedTotalCost)
            {
                AllFundSourceAllocations = allFundSourceAllocations;
                ProjectID = projectProposedBatch.ProjectID;
                EstimatedTotalCost = estimatedTotalCost;
            }
        }
    }
}
