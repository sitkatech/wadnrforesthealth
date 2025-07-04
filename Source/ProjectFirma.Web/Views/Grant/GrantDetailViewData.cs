/*-----------------------------------------------------------------------
<copyright file="GrantDetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.GrantAllocation;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantDetailViewData : GrantViewData
    {
        public string NewGrantNoteUrl { get; set; }
        public EntityNotesViewData GrantNotesViewData { get; set; }
        public EntityNotesViewData InternalGrantNotesViewData { get; set; }

        public FileDetailsViewData GrantDetailsFileDetailsViewData { get; }

        //public GrantAllocationGridSpec GrantAllocationGridSpec { get; }
        //public string GrantAllocationGridName { get; }
        //public string GrantAllocationGridDataUrlTemplate { get; }

        public GrantAllocationBudgetLineItemGridSpec GrantAllocationBudgetLineItemGridSpec { get; }
        public string GrantAllocationBudgetLineItemGridName { get; }
        public string GrantAllocationBudgetLineItemGridDataUrl { get; }

        public GrantAgreementGridSpec GrantAgreementGridSpec { get; }
        public string GrantAgreementGridName { get; }
        public string GrantAgreementGridDataUrl { get; }
        public ProjectGrantAllocationRequestsByGrantGridSpec ProjectGrantAllocationRequestsByGrantGridSpec { get; }
        public string ProjectGrantAllocationRequestsGridName { get; }
        public string ProjectGrantAllocationRequestsGridDataUrl { get; }

        public bool isUserLoggedIn { get; }

        public GrantDetailViewData(Person currentPerson,
                                    Models.Grant grant,
                                    EntityNotesViewData grantNotesViewData,
                                    EntityNotesViewData internalNotesViewData)
            : base(currentPerson, grant)
        {
            PageTitle = grant.GrantTitle.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()} Detail";
            NewGrantNoteUrl = grant.GetNewNoteUrl();
            GrantNotesViewData = grantNotesViewData;
            InternalGrantNotesViewData = internalNotesViewData;

            //GrantAllocationGridSpec = new GrantAllocationGridSpec(currentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, grant);
            //GrantAllocationGridName = "grantAllocationsGridName";
            //GrantAllocationGridDataUrlTemplate = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationGridJsonDataByGrantModification(UrlTemplate.Parameter1Int));

            GrantAllocationBudgetLineItemGridSpec = new GrantAllocationBudgetLineItemGridSpec();
            GrantAllocationBudgetLineItemGridName = "grantAllocationBudgetLineItemsGridName";
            GrantAllocationBudgetLineItemGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationBudgetLineItemGridJsonDataByGrant(grant));

            GrantAgreementGridSpec = new GrantAgreementGridSpec();
            GrantAgreementGridName = "grantAgreementGridName";
            GrantAgreementGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAgreementGridJsonData(grant));

            GrantDetailsFileDetailsViewData = new FileDetailsViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(grant.GrantFileResources)),
                SitkaRoute<GrantController>.BuildUrlFromExpression(x => x.NewGrantFiles(grant.PrimaryKey)),
                new GrantEditAsAdminFeature().HasPermissionByPerson(currentPerson),
                Models.FieldDefinition.Grant
            );

            ProjectGrantAllocationRequestsByGrantGridSpec = new ProjectGrantAllocationRequestsByGrantGridSpec()
            {
                ObjectNameSingular = "Project",
                ObjectNamePlural = "Projects",
                SaveFiltersInCookie = true
            };
            ProjectGrantAllocationRequestsGridName = "projectsGrantAllocationRequestsFromGrantAllocationGrid";
            ProjectGrantAllocationRequestsGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.ProjectGrantAllocationRequestsByGrantGridJsonData(grant));

            isUserLoggedIn = !currentPerson.IsAnonymousOrUnassigned;
        }
    }
}
