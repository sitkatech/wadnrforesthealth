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
using LtInfo.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class DetailViewData : GrantAllocationViewData
    {
        public GrantAllocationBasicsViewData GrantAllocationBasicsViewData { get; }
        public string NewGrantAllocationNoteUrl { get; set; }
        public EntityNotesViewData GrantAllocationNotesViewData { get; set; }

        public DetailViewData(Person currentPerson, Models.GrantAllocation grantAllocation, GrantAllocationBasicsViewData grantAllocationBasicsViewData, EntityNotesViewData grantAllocationNotesViewData)
            : base(currentPerson, grantAllocation)
        {
            PageTitle = grantAllocation.ProjectName.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Detail";

            GrantAllocationBasicsViewData = grantAllocationBasicsViewData;
            GrantAllocationNotesViewData = grantAllocationNotesViewData;

            NewGrantAllocationNoteUrl = grantAllocation.GetNewNoteUrl();
        }
    }
}
