/*-----------------------------------------------------------------------
<copyright file="LandownerCostShareLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Linq;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class LandownerCostShareLineItemGridSpec : GridSpec<Models.GrantAllocationAwardLandownerCostShareLineItem>
    {
        public LandownerCostShareLineItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabelPluralized();

            bool hasDeletePermission = new GrantAllocationAwardLandownerCostShareLineItemDeleteFeature().HasPermissionByPerson(currentPerson);
            bool hasEditPermission = new GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardLandownerCostShareLineItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newLandownerCostShareLineItemUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewLandownerCostShareLineItemFromGrantAllocationAward(grantAllocationAward.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(newLandownerCostShareLineItemUrl, $"Create a new {Models.FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel()} Line Item");
            }

            //delete column
            if (hasDeletePermission)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteLandownerCostShareLineItemUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditLandownerCostShareLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel()} Line Item")), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.Project.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.Project.GetDetailUrl(), a.Project.ProjectName), 150, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(),
                a => a.GrantAllocationAwardID != null ? a.GrantAllocationAward.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsCommaDelimited() : "Not Available",
                75, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStatus.GetFieldDefinitionLabel(), a => a.GetStatusDisplayName(), 125, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.Treatments.FirstOrDefault()?.TreatmentStartDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.Treatments.FirstOrDefault()?.TreatmentEndDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareFootprintAcres.GetFieldDefinitionLabel(), a => a.Treatments.FirstOrDefault()?.TreatmentFootprintAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareChippingAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Chipping)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostSharePruningAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Pruning)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareThinningAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Thinning)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareSlashAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Slash)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMasticationAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Mastication)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareGrazingAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Grazing)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLopAndScatterAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.LopAndScatter)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareBiomassRemovalAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BiomassRemoval)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareHandPileAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPile)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareBroadcastBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BroadcastBurn)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareHandPileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPileBurn)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMachinePileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.MachinePileBurn)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareOtherTreatmentAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Other)?.TreatmentTreatedAcres ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareAllocatedAmount.ToGridHeaderString(), a => a.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareTotalCost.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemTotalCost, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareActualMatch.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemActualMatch, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareGrantCost.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemGrantCost, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareNotes.ToGridHeaderString(), a => a.GrantAllocationAwardLandownerCostShareLineItemNotes, 250, AgGridColumnFilterType.Text);
        }
    }
}
