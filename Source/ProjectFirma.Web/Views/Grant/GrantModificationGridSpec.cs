﻿/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.DesignByContract;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Grant
{

    public class GrantModificationGridSpec : GridSpec<Models.GrantModification>
    {

        public GrantModificationGridSpec(Person currentPerson, Models.Grant grantToAssociate)
        {
            GrantModificationGridSpecConstructorImpl(currentPerson, grantToAssociate);
        }


        private void GrantModificationGridSpecConstructorImpl(Person currentPerson, Models.Grant grantToAssociate)
        {
            Check.Ensure(grantToAssociate != null, "Grant is null. Creating a New Grant Modification without a current Grant is currently not supported.");

            ObjectNameSingular = $"{Models.FieldDefinition.GrantModification.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantModification.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;

            var userHasCreatePermissions = new GrantModificationCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions && grantToAssociate != null)
            {
                var contentUrl = SitkaRoute<GrantModificationController>.BuildUrlFromExpression(gmc => gmc.NewGrantModificationForAGrant(grantToAssociate.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {Models.FieldDefinition.GrantModification.GetFieldDefinitionLabel()}");
            }
            var userHasEditPermissions = new GrantModificationEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(
                                                                    new ModalDialogForm(x.GetEditUrl(), $"Edit {ObjectNameSingular} - {x.GrantModificationName}"),
                                                                    userHasEditPermissions), 30, DhtmlxGridColumnFilterType.None);
            }

            var userHasDeletePermissions = new GrantModificationDeleteFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            Add(Models.FieldDefinition.GrantModificationName.ToGridHeaderString(), x => x.GrantModificationName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantModificationStartDate.ToGridHeaderString(), x => x.StartDateDisplay, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantModificationEndDate.ToGridHeaderString(), x => x.EndDateDisplay, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantModificationStatus.ToGridHeaderString(), x => x.GrantModificationStatus.GetDisplayName(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantModificationPurpose.ToGridHeaderString(), x => x.GrantModificationPurposeNamesAsCommaDelimitedString, 200, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantModificationAmount.ToGridHeaderString(), x => x.GrantModificationAmount, 125, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);

        }

    }
}