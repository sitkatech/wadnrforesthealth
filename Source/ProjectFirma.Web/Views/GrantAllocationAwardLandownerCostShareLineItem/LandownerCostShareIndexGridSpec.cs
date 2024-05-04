using System;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocationAwardLandownerCostShareLineItem
{
    public class LandownerCostShareIndexGridSpec : GridSpec<Models.GrantAllocationAwardLandownerCostShareLineItem>
    {
        public LandownerCostShareIndexGridSpec(Person currentPerson)
        {

            bool userHasEditPermissions = new GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            int buttonGridWidth = 30;

            Add(string.Empty, x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditLandownerCostShareLineItemUrl(), $"Edit {Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()}"), userHasEditPermissions), buttonGridWidth, AgGridColumnFilterType.None);

            Add(Models.FieldDefinition.GrantAllocationAward.ToGridHeaderString(), a => a.GrantAllocationAwardID != null ? UrlTemplate.MakeHrefString(a.GrantAllocationAward.GetDetailUrl(), a.GrantAllocationAward.GrantAllocationAwardName) : new HtmlString($"No {Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} set"), 150, AgGridColumnFilterType.Html);

            Add(Models.FieldDefinition.Project.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.Project.GetDetailUrl(), a.Project.ProjectName), 150, AgGridColumnFilterType.Html);

            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(),
                a => a.GrantAllocationAwardID != null ? a.GrantAllocationAward.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsCommaDelimited() : "Not Available",
                75, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStatus.GetFieldDefinitionLabel(), a => a.GetStatusDisplayName(), 125, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.Treatments.FirstOrDefault()?.TreatmentStartDate ?? DateTime.MinValue, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.Treatments.FirstOrDefault()?.TreatmentEndDate ?? DateTime.MinValue, 125, AgGridColumnFormatType.Date);
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