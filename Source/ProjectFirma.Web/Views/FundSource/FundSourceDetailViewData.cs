/*-----------------------------------------------------------------------
<copyright file="FundSourceDetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FundSourceAllocation;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceDetailViewData : FundSourceViewData
    {
        public string NewFundSourceNoteUrl { get; set; }
        public EntityNotesViewData FundSourceNotesViewData { get; set; }
        public EntityNotesViewData InternalFundSourceNotesViewData { get; set; }

        public FileDetailsViewData FundSourceDetailsFileDetailsViewData { get; }

        //public FundSourceAllocationGridSpec FundSourceAllocationGridSpec { get; }
        //public string FundSourceAllocationGridName { get; }
        //public string FundSourceAllocationGridDataUrlTemplate { get; }

        public FundSourceAllocationBudgetLineItemGridSpec FundSourceAllocationBudgetLineItemGridSpec { get; }
        public string FundSourceAllocationBudgetLineItemGridName { get; }
        public string FundSourceAllocationBudgetLineItemGridDataUrl { get; }

        public FundSourceAgreementGridSpec FundSourceAgreementGridSpec { get; }
        public string FundSourceAgreementGridName { get; }
        public string FundSourceAgreementGridDataUrl { get; }
        public ProjectFundSourceAllocationRequestsByFundSourceGridSpec ProjectFundSourceAllocationRequestsByFundSourceGridSpec { get; }
        public string ProjectFundSourceAllocationRequestsGridName { get; }
        public string ProjectFundSourceAllocationRequestsGridDataUrl { get; }

        public bool isUserLoggedIn { get; }

        public FundSourceDetailViewData(Person currentPerson,
                                    Models.FundSource fundSource,
                                    EntityNotesViewData fundSourceNotesViewData,
                                    EntityNotesViewData internalNotesViewData)
            : base(currentPerson, fundSource)
        {
            PageTitle = fundSource.FundSourceTitle.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()} Detail";
            NewFundSourceNoteUrl = fundSource.GetNewNoteUrl();
            FundSourceNotesViewData = fundSourceNotesViewData;
            InternalFundSourceNotesViewData = internalNotesViewData;

            //FundSourceAllocationGridSpec = new FundSourceAllocationGridSpec(currentPerson, FundSourceAllocationGridSpec.FundSourceAllocationGridCreateButtonType.Shown, fundSource);
            //FundSourceAllocationGridName = "fundSourceAllocationsGridName";
            //FundSourceAllocationGridDataUrlTemplate = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourceAllocationGridJsonDataByFundSourceModification(UrlTemplate.Parameter1Int));

            FundSourceAllocationBudgetLineItemGridSpec = new FundSourceAllocationBudgetLineItemGridSpec();
            FundSourceAllocationBudgetLineItemGridName = "fundSourceAllocationBudgetLineItemsGridName";
            FundSourceAllocationBudgetLineItemGridDataUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourceAllocationBudgetLineItemGridJsonDataByFundSource(fundSource));

            FundSourceAgreementGridSpec = new FundSourceAgreementGridSpec();
            FundSourceAgreementGridName = "fundSourceAgreementGridName";
            FundSourceAgreementGridDataUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourceAgreementGridJsonData(fundSource));

            FundSourceDetailsFileDetailsViewData = new FileDetailsViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(fundSource.FundSourceFileResources)),
                SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.NewFundSourceFiles(fundSource.PrimaryKey)),
                new FundSourceEditAsAdminFeature().HasPermissionByPerson(currentPerson),
                Models.FieldDefinition.FundSource
            );

            ProjectFundSourceAllocationRequestsByFundSourceGridSpec = new ProjectFundSourceAllocationRequestsByFundSourceGridSpec()
            {
                ObjectNameSingular = "Project",
                ObjectNamePlural = "Projects",
                SaveFiltersInCookie = true
            };
            ProjectFundSourceAllocationRequestsGridName = "projectsFundSourceAllocationRequestsFromFundSourceAllocationGrid";
            ProjectFundSourceAllocationRequestsGridDataUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.ProjectFundSourceAllocationRequestsByFundSourceGridJsonData(fundSource));

            isUserLoggedIn = !currentPerson.IsAnonymousOrUnassigned;
        }
    }
}
