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

        public GridSpec<Models.ProjectGrantAllocationRequest> ProjectGrantAllocationRequestsGridSpec { get; }
        public string ProjectGrantAllocationRequestsGridName { get; }
        public string ProjectGrantAllocationRequestsGridDataUrl { get; }

        public GrantAllocationBudgetLineItemsViewData GrantAllocationBudgetLineItemsViewData { get; }

        public GrantAllocationBudgetVsActualsViewData GrantAllocationBudgetVsActualsViewData { get; }

        public GrantAllocationExpendituresGridSpec GrantAllocationExpendituresGridSpec { get; }
        
        public string GrantAllocationExpendituresGridName { get; }
        public string GrantAllocationExpendituresGridDataUrl { get; }

        public FileDetailsViewData GrantAllocationDetailsFileDetailsViewData { get; set; }
        public List<AgreementGrantAllocation> CurrentAgreementGrantAllocationsInSortedOrder { get; }

        public DetailViewData(Person currentPerson, Models.GrantAllocation grantAllocation
            , GrantAllocationBasicsViewData grantAllocationBasicsViewData
            , EntityNotesViewData grantAllocationNotesViewData
            , EntityNotesViewData grantAllocationNoteInternalsViewData
            , ViewGoogleChartViewData viewGoogleChartViewData
            , GridSpec<Models.ProjectGrantAllocationRequest> projectGrantAllocationRequestsGridSpec
            , GrantAllocationExpendituresGridSpec grantAllocationExpendituresGridSpec)
            : base(currentPerson, grantAllocation)
        {
            PageTitle = grantAllocation.GrantAllocationName.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Detail";

            GrantAllocationBasicsViewData = grantAllocationBasicsViewData;
            GrantAllocationNotesViewData = grantAllocationNotesViewData;

            NewGrantAllocationNoteUrl = grantAllocation.GetNewNoteUrl();
            GrantAllocationNoteInternalsViewData = grantAllocationNoteInternalsViewData;

            ViewGoogleChartViewData = viewGoogleChartViewData;

            ProjectGrantAllocationRequestsGridSpec = projectGrantAllocationRequestsGridSpec;
            ProjectGrantAllocationRequestsGridName = "projectsGrantAllocationRequestsFromGrantAllocationGrid";
            ProjectGrantAllocationRequestsGridDataUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(tc => tc.ProjectGrantAllocationRequestsGridJsonData(grantAllocation));

            GrantAllocationExpendituresGridSpec = grantAllocationExpendituresGridSpec;
            GrantAllocationExpendituresGridName = "grantAllocationExpendituresGrid";
            GrantAllocationExpendituresGridDataUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(gac => gac.GrantAllocationExpendituresGridJsonData(grantAllocation));

            GrantAllocationBudgetLineItemsViewData = new GrantAllocationBudgetLineItemsViewData(currentPerson, grantAllocation, grantAllocation.GrantAllocationBudgetLineItems.ToList());
            GrantAllocationBudgetVsActualsViewData = new GrantAllocationBudgetVsActualsViewData(currentPerson, grantAllocation);

            var canEditDocuments = new GrantAllocationEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            GrantAllocationDetailsFileDetailsViewData = new FileDetailsViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(grantAllocation.GrantAllocationFileResources)),
                SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x => x.NewGrantAllocationFiles(grantAllocation.PrimaryKey)),
                canEditDocuments,
                Models.FieldDefinition.GrantAllocation
            );

            List<AgreementGrantAllocation> agreementGrantAllocationsList = grantAllocation.AgreementGrantAllocations.ToList();
            CurrentAgreementGrantAllocationsInSortedOrder = agreementGrantAllocationsList;



        }
    }
}
