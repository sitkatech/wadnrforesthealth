/*-----------------------------------------------------------------------
<copyright file="SuppliesLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class PersonnelAndBenefitsLineItemGridSpec : GridSpec<GrantAllocationAwardPersonnelAndBenefitsLineItem>
    {
        public PersonnelAndBenefitsLineItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsLineItem.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsLineItem.GetFieldDefinitionLabelPluralized();

            bool hasDeletePermission = new GrantAllocationAwardPersonnelAndBenefitsLineItemDeleteFeature().HasPermissionByPerson(currentPerson);
            bool hasEditPermission = new GrantAllocationAwardPersonnelAndBenefitsLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardPersonnelAndBenefitsLineItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newSuppliesLineItemUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewPersonnelAndBenefitsLineItemFromGrantAllocationAward(grantAllocationAward.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(newSuppliesLineItemUrl, $"Create a new {Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefits.GetFieldDefinitionLabel()} Line Item");
            }

            //delete column
            if (hasDeletePermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeletePersonnelAndBenefitsLineItemUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditPersonnelAndBenefitsLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefits.GetFieldDefinitionLabel()} Line Item")), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsName.ToGridHeaderString(), s => s.Person != null ? s.Person.GetFullNameFirstLastAndOrgShortNameAsUrl() : new HtmlString(string.Empty), 200, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsTarOrMonth.ToGridHeaderString(), s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth , 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsDate.ToGridHeaderString(), s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemDate, 125, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsTarHours.ToGridHeaderString(), s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours, 125, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsHourlyRate.ToGridHeaderString(), s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate, 125, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add("Hourly Total", s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsFringeRate.ToGridHeaderString(), s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate, 125, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add("Fringe Total", s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add("TAR Total", s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemTarTotal, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardPersonnelAndBenefitsNotes.ToGridHeaderString(), s => s.GrantAllocationAwardPersonnelAndBenefitsLineItemNotes, 250, DhtmlxGridColumnFilterType.Text);
        }
    }
}
