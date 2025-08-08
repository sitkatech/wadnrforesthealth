/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FocusArea;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.FundSourceAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class DetailViewData : FundSourceAllocationViewData
    {
        public FundSourceAllocationBasicsViewData FundSourceAllocationBasicsViewData { get; }
        public string NewFundSourceAllocationNoteUrl { get; set; }
        public EntityNotesViewData FundSourceAllocationNotesViewData { get; set; }
        public EntityNotesViewData FundSourceAllocationNoteInternalsViewData { get; set; }
        public ViewGoogleChartViewData ViewGoogleChartViewData { get; }

        public GridSpec<Models.ProjectFundSourceAllocationRequest> ProjectFundSourceAllocationRequestsGridSpec { get; }
        public string ProjectFundSourceAllocationRequestsGridName { get; }
        public string ProjectFundSourceAllocationRequestsGridDataUrl { get; }

        public FundSourceAllocationBudgetLineItemsViewData FundSourceAllocationBudgetLineItemsViewData { get; }

        public FundSourceAllocationBudgetVsActualsViewData FundSourceAllocationBudgetVsActualsViewData { get; }

        public FundSourceAllocationExpendituresGridSpec FundSourceAllocationExpendituresGridSpec { get; }
        
        public string FundSourceAllocationExpendituresGridName { get; }
        public string FundSourceAllocationExpendituresGridDataUrl { get; }

        public FileDetailsViewData FundSourceAllocationDetailsFileDetailsViewData { get; set; }
        public List<AgreementFundSourceAllocation> CurrentAgreementFundSourceAllocationsInSortedOrder { get; }
        public bool IsUserLoggedIn { get; }

        public DetailViewData(Person currentPerson, Models.FundSourceAllocation fundSourceAllocation
            , FundSourceAllocationBasicsViewData fundSourceAllocationBasicsViewData
            , EntityNotesViewData fundSourceAllocationNotesViewData
            , EntityNotesViewData fundSourceAllocationNoteInternalsViewData
            , ViewGoogleChartViewData viewGoogleChartViewData
            , GridSpec<Models.ProjectFundSourceAllocationRequest> projectFundSourceAllocationRequestsGridSpec
            , FundSourceAllocationExpendituresGridSpec fundSourceAllocationExpendituresGridSpec)
            : base(currentPerson, fundSourceAllocation)
        {
            PageTitle = fundSourceAllocation.FundSourceAllocationName.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} Detail";

            FundSourceAllocationBasicsViewData = fundSourceAllocationBasicsViewData;
            FundSourceAllocationNotesViewData = fundSourceAllocationNotesViewData;

            NewFundSourceAllocationNoteUrl = fundSourceAllocation.GetNewNoteUrl();
            FundSourceAllocationNoteInternalsViewData = fundSourceAllocationNoteInternalsViewData;

            ViewGoogleChartViewData = viewGoogleChartViewData;

            ProjectFundSourceAllocationRequestsGridSpec = projectFundSourceAllocationRequestsGridSpec;
            ProjectFundSourceAllocationRequestsGridName = "projectsFundSourceAllocationRequestsFromFundSourceAllocationGrid";
            ProjectFundSourceAllocationRequestsGridDataUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(tc => tc.ProjectFundSourceAllocationRequestsGridJsonData(fundSourceAllocation));

            FundSourceAllocationExpendituresGridSpec = fundSourceAllocationExpendituresGridSpec;
            FundSourceAllocationExpendituresGridName = "fundSourceAllocationExpendituresGrid";
            FundSourceAllocationExpendituresGridDataUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(gac => gac.FundSourceAllocationExpendituresGridJsonData(fundSourceAllocation));

            FundSourceAllocationBudgetLineItemsViewData = new FundSourceAllocationBudgetLineItemsViewData(currentPerson, fundSourceAllocation, fundSourceAllocation.FundSourceAllocationBudgetLineItems.ToList());
            FundSourceAllocationBudgetVsActualsViewData = new FundSourceAllocationBudgetVsActualsViewData(currentPerson, fundSourceAllocation);

            var canEditDocuments = new FundSourceAllocationEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            FundSourceAllocationDetailsFileDetailsViewData = new FileDetailsViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(fundSourceAllocation.FundSourceAllocationFileResources)),
                SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(x => x.NewFundSourceAllocationFiles(fundSourceAllocation.PrimaryKey)),
                canEditDocuments,
                Models.FieldDefinition.FundSourceAllocation
            );

            List<AgreementFundSourceAllocation> agreementFundSourceAllocationsList = fundSourceAllocation.AgreementFundSourceAllocations.ToList();
            CurrentAgreementFundSourceAllocationsInSortedOrder = agreementFundSourceAllocationsList;


            IsUserLoggedIn = !currentPerson.IsAnonymousOrUnassigned;
        }
    }
}
