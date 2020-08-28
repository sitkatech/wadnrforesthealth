/*-----------------------------------------------------------------------
<copyright file="GrantModificationDetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Grant;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.GrantModification
{
    public class GrantModificationDetailViewData : FirmaViewData
    {
        public Models.GrantModification GrantModification { get; }
        public string EditGrantModificationBasicsUrl { get; }
        public EntityNotesViewData InternalGrantModificationNotesViewData { get; }
        public bool UserHasEditGrantModificationPermissions { get; }
        public string ParentGrantUrl { get; }
        public string BackToParentGrantUrlText { get; }
        public FileDetailsViewData GrantModificationDetailFileDetailsViewData { get; set; }

        public GrantAllocationGridSpec GrantAllocationGridSpec { get; }
        public string GrantAllocationGridName { get; }
        public string GrantAllocationGridDataUrl { get; }

        public GrantModificationDetailViewData(Person currentPerson,
                                               Models.GrantModification grantModification,
                                               EntityNotesViewData internalGrantModificationNotesViewData) : base(currentPerson)
        {
            Check.EnsureNotNull(currentPerson);
            Check.EnsureNotNull(grantModification);

            GrantModification = grantModification;
            PageTitle = grantModification.GrantModificationName;
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantModification.GetFieldDefinitionLabel()} Detail";
            InternalGrantModificationNotesViewData = internalGrantModificationNotesViewData;
            ParentGrantUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(gc => gc.GrantDetail(grantModification.GrantID));
            BackToParentGrantUrlText = $"Back to {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}: {grantModification.Grant.GrantName}";
            EditGrantModificationBasicsUrl = SitkaRoute<GrantModificationController>.BuildUrlFromExpression(gmc => gmc.EditGrantModification(grantModification.PrimaryKey));
            UserHasEditGrantModificationPermissions = new GrantModificationEditAsAdminFeature().HasPermissionByPerson(currentPerson);

            var canEditDocuments = new GrantModificationEditAsAdminFeature().HasPermission(currentPerson, grantModification).HasPermission;
            GrantModificationDetailFileDetailsViewData = new FileDetailsViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(grantModification.GrantModificationFileResources)),
                SitkaRoute<GrantModificationController>.BuildUrlFromExpression(x => x.NewGrantModificationFiles(grantModification.PrimaryKey)),
                canEditDocuments,
                Models.FieldDefinition.GrantModification
            );

            var relevantGrant = grantModification.Grant;
            GrantAllocationGridSpec = new GrantAllocationGridSpec(currentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Shown, relevantGrant);
            GrantAllocationGridName = "grantAllocationsGridName";
            GrantAllocationGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationGridJsonDataByGrantModification(grantModification));
        }
    }
}
