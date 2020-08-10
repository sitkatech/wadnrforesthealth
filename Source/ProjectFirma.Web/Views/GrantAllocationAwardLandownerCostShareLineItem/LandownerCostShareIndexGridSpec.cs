using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
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

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditLandownerCostShareLineItemUrl(), $"Edit {Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()}"), userHasEditPermissions), buttonGridWidth, DhtmlxGridColumnFilterType.None);

            Add(Models.FieldDefinition.GrantAllocationAward.ToGridHeaderString(), a => a.GrantAllocationAwardID != null ? UrlTemplate.MakeHrefString(a.GrantAllocationAward.GetDetailUrl(), a.GrantAllocationAward.GrantAllocationAwardName) : new HtmlString($"No {Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} set"), 150, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.Project.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.Project.GetDetailUrl(), a.Project.ProjectName), 150, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(),
                a => a.GrantAllocationAwardID != null ? a.GrantAllocationAward.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsCommaDelimited() : "Not Available",
                75, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStatus.GetFieldDefinitionLabel(), a => a.GetStatusDisplayName(), 125, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault()?.TreatmentStartDate, 125, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault()?.TreatmentEndDate , 125, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareFootprintAcres.GetFieldDefinitionLabel(), a => a.Treatments.FirstOrDefault()?.TreatmentFootprintAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareChippingAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Chipping)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostSharePruningAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Pruning)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareThinningAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Thinning)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareSlashAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Slash)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMasticationAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Mastication)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareGrazingAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Grazing)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLopAndScatterAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.LopAndScatter)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareBiomassRemovalAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.BiomassRemoval)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareHandPileAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.HandPile)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareBroadcastBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.BroadcastBurn)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareHandPileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.HandPileBurn)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMachinePileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.MachinePileBurn)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareOtherTreatmentAcres.GetFieldDefinitionLabel(), a => a.Treatments.SingleOrDefault(x => x.TreatmentType == TreatmentType.Other)?.TreatmentTreatedAcres ?? 0, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareAllocatedAmount.ToGridHeaderString(), a => a.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareTotalCost.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemTotalCost, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareActualMatch.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemActualMatch, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareGrantCost.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemGrantCost, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareNotes.ToGridHeaderString(), a => a.GrantAllocationAwardLandownerCostShareLineItemNotes, 250, DhtmlxGridColumnFilterType.Text);

        }


    }
}