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
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Grant;
using ProjectFirma.Web.Views.ProjectFunding;
using ProjectFirma.Web.Views.ProjectUpdate;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.ExpenditureAndBudgetControls;
using ProjectFirma.Web.Views.Shared.PerformanceMeasureControls;
using ProjectFirma.Web.Views.Shared.ProjectControls;
using ProjectFirma.Web.Views.Shared.ProjectDocument;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using ProjectFirma.Web.Views.Shared.ProjectOrganization;
using ProjectFirma.Web.Views.Shared.ProjectPerson;
using ProjectFirma.Web.Views.Shared.TextControls;
using ProjectFirma.Web.Views.TreatmentActivity;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class DetailViewData : GrantAllocationViewData
    {
        public GrantAllocationBasicsViewData GrantAllocationBasicsViewData { get; }
        public string NewGrantAllocationNoteUrl { get; set; }

        public DetailViewData(Person currentPerson, Models.GrantAllocation grantAllocation, GrantAllocationBasicsViewData grantAllocationBasicsViewData)
            : base(currentPerson, grantAllocation)
        {
            PageTitle = grantAllocation.ProjectName.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Detail";

            GrantAllocationBasicsViewData = grantAllocationBasicsViewData;

            NewGrantAllocationNoteUrl = grantAllocation.GetNewNoteUrl();
        }
    }
}
