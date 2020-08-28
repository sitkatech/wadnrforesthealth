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
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared.Grant;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantDetailViewData : GrantViewData
    {
        public string NewGrantNoteUrl { get; set; }
        public EntityNotesViewData GrantNotesViewData { get; set; }
        public EntityNotesViewData InternalGrantNotesViewData { get; set; }
        public bool ShowDownload { get; set; }

        public GrantModificationGridSpec GrantModificationGridSpec { get; }
        public string GrantModificationGridName { get; }
        public string GrantModificationGridDataUrl { get; }
        public GrantDetailsFileDetailsViewData GrantDetailsFileDetailsViewData { get; }

        public readonly string GrantAllocationPanelTitleID = "GrantAllocationPanelTitle";

        public GrantAllocationGridSpec GrantAllocationGridSpec { get; }
        public string GrantAllocationGridName { get; }
        public string GrantAllocationGridDataUrlTemplate { get; }

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
            // Used for creating file download link, if file available
            ShowDownload = grant.GrantFileResources.Any(); ;

            bool grantModificationFileExistsOnAtLeastOne = false;//grant.GrantModifications.Any(x => x.GrantModificationFileResourceID.HasValue);
            GrantModificationGridSpec = new GrantModificationGridSpec(currentPerson, grant, grantModificationFileExistsOnAtLeastOne);
            GrantModificationGridName = "grantModificationsGridName";
            GrantModificationGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantModificationGridJsonDataByGrant(grant.PrimaryKey));

            GrantAllocationGridSpec = new GrantAllocationGridSpec(currentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, grant);
            GrantAllocationGridName = "grantAllocationsGridName";
            GrantAllocationGridDataUrlTemplate = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationGridJsonDataByGrantModification(UrlTemplate.Parameter1Int));

            GrantDetailsFileDetailsViewData = new GrantDetailsFileDetailsViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(grant.GrantFileResources)),
                SitkaRoute<GrantController>.BuildUrlFromExpression(x => x.NewGrantFiles(grant.PrimaryKey)),
                grant.GrantName,
                new GrantEditAsAdminFeature().HasPermission(currentPerson, grant).HasPermission,
                Models.FieldDefinition.Grant
            );
        }
    }
}
