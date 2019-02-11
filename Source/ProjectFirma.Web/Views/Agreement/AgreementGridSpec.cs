/*-----------------------------------------------------------------------
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
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Agreement
{
    public class AgreementGridSpec : GridSpec<Models.Agreement>
    {
        public static string AgreementIDHiddenColumn = "AgreementIDHiddenColumnName";

        public AgreementGridSpec(Models.Person currentPerson, bool agreementFileExistsOnAtLeastOne)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;
            var userHasDeletePermissions = new AgreementDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new AgreementCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.New());
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, "Create a new Agreement");
            }

            CustomExcelDownloadUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(tc => tc.AgreementsExcelDownload());

            // hidden column for agreement id for use by JavaScript
            Add(AgreementIDHiddenColumn, x => x.PrimaryKey, 0);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true), 30, DhtmlxGridColumnFilterType.None);
            }

            if (agreementFileExistsOnAtLeastOne)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeFileDownloadIconAsHyperlinkBootstrap(x.GetFileDownloadUrl()), 30, DhtmlxGridColumnFilterType.None);
            }
            Add(Models.FieldDefinition.AgreementType.ToGridHeaderString("Type"), x => x.AgreementType?.AgreementTypeAbbrev, 70, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.AgreementNumber.ToGridHeaderString("Number"), x => x.AgreementNumber, 100, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.FundingSource.ToGridHeaderString(), x => x.Grant.GrantNumber, 130, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => x.Organization.DisplayName, 130, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.AgreementTitle.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl() , x.AgreementTitle), 130, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.AgreementStartDate.ToGridHeaderString("Start Date"), x => x.StartDateDisplay, 70, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.AgreementEndDate.ToGridHeaderString("End Date"), x => x.EndDateDisplay, 70, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.AgreementAmount.ToGridHeaderString("Amount"), x => x.AgreementAmount.ToStringCurrency(), 70, DhtmlxGridColumnFilterType.Text);
           
        }
    }
}
