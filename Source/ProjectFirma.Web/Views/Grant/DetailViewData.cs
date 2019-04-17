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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.Grant
{
    public class DetailViewData : GrantViewData
    {
        public string NewGrantNoteUrl { get; set; }
        public EntityNotesViewData GrantNotesViewData { get; set; }
        public EntityNotesViewData InternalGrantNotesViewData { get; set; }
        public bool ShowDownload { get; set; }

        public GrantAllocationGridSpec GrantAllocationGridSpec { get; }
        public string GrantAllocationGridName { get; }
        public string GrantAllocationGridDataUrl { get; }

        public DetailViewData(Person currentPerson, Models.Grant grant, EntityNotesViewData grantNotesViewData, EntityNotesViewData internalNotesViewData)
            : base(currentPerson, grant)
        {
            PageTitle = grant.GrantTitle.ToEllipsifiedStringClean(110);
            BreadCrumbTitle = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()} Detail";
            NewGrantNoteUrl = grant.GetNewNoteUrl();
            GrantNotesViewData = grantNotesViewData;
            InternalGrantNotesViewData = internalNotesViewData;
            // Used for creating file download link, if file available
            ShowDownload = grant.GrantFileResource != null;

            GrantAllocationGridSpec = new GrantAllocationGridSpec(currentPerson);
            GrantAllocationGridName = "grantAllocationsGridName";
            GrantAllocationGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationGridJsonDataByGrant(grant.PrimaryKey));

        }
    }
}
