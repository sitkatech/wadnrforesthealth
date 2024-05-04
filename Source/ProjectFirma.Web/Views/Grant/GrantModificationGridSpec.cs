/*-----------------------------------------------------------------------
<copyright file="GrantModificationGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.DesignByContract;
using LtInfo.Common.AgGridWrappers;
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
        public static string GrantModificationIDHiddenColumnName = "GrantModificationID_HiddenColumnName";
        public readonly string GrantModificationNameHiddenColumnName = "GrantModificationName_HiddenColumnName";

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

            // hidden columns for use by JavaScript
            Add(GrantModificationIDHiddenColumnName, x => x.GrantModificationID, 0);
            Add(GrantModificationNameHiddenColumnName, x => x.GrantModificationName, 0);

            var userHasDeletePermissions = new GrantModificationDeleteFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }

            var userHasEditPermissions = new GrantModificationEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(
                                                                    new ModalDialogForm(x.GetEditUrl(), $"Edit {ObjectNameSingular} - {x.GrantModificationName}"),
                                                                    userHasEditPermissions), 30, AgGridColumnFilterType.None);
            }

            if (userHasCreatePermissions && grantToAssociate != null)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDuplicateIconAndLinkBootstrap(x.GetDuplicateUrl(), 950, $"Duplicate {Models.FieldDefinition.GrantModification.GetFieldDefinitionLabel()} \"{x.GrantModificationName}\" to New {Models.FieldDefinition.GrantModification.GetFieldDefinitionLabel()}"), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantModificationName.ToGridHeaderString(), x => x.GetGrantModificationNameAsUrl(), 125, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.GrantModificationStartDate.ToGridHeaderString(), x => x.GrantModificationStartDate, 100, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantModificationEndDate.ToGridHeaderString(), x => x.GrantModificationEndDate, 100, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantModificationStatus.ToGridHeaderString(), x => x.GrantModificationStatus.GetDisplayName(), 100, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantModificationPurpose.ToGridHeaderString(), x => x.GrantModificationPurposeNamesAsCommaDelimitedString, 200, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantModificationAmount.ToGridHeaderString(), x => x.GrantModificationAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantModificationDescription.ToGridHeaderString(), x => x.GrantModificationDescription, 125, AgGridColumnFilterType.Text);
        }
    }
}
