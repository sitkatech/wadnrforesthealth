using System.Linq;
using System.Web;
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
    public class TreatmentGroupGridSpec : GridSpec<Models.TreatmentGroup>
    {
        public TreatmentGroupGridSpec(Person currentPerson, Models.Project projectToCreateNewTreatments)
        {
            this.ObjectNameSingular = "Treatment";
            this.ObjectNamePlural = "Treatments";

            bool userHasEditPermissions = new GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardLandownerCostShareLineItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newTreatmentsUrl = SitkaRoute<TreatmentController>.BuildUrlFromExpression(tc => tc.NewTreatmentFromProject(projectToCreateNewTreatments));
                CreateEntityModalDialogForm = new ModalDialogForm(newTreatmentsUrl, 950, $"Create new Treatments for {projectToCreateNewTreatments.DisplayName}");                
            }
            //int buttonGridWidth = 30;


            //Add(string.Empty, x => x.Treatments.First().GrantAllocationAwardLandownerCostShareLineItemID.HasValue ? AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GetEditLandownerCostShareLineItemUrl(), $"Edit {Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()}"), userHasEditPermissions) : new HtmlString(string.Empty), buttonGridWidth, AgGridColumnFilterType.None);

            Add("Treatment Area Name", a => a.ProjectLocation?.ProjectLocationName ?? "", 200, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAward.ToGridHeaderString(), a => a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardID != null ? UrlTemplate.MakeHrefString(a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAward.GetDetailUrl(), a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAward.GrantAllocationAwardName) : new HtmlString($"No {Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} set"), 150, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(),
                a => a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardID != null ? a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAward.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsCommaDelimited() : "Not Available",
                75, AgGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStatus.GetFieldDefinitionLabel(), a => a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GetStatusDisplayName(), 125, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentStartDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentEndDate, 125, AgGridColumnFormatType.Date);

            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareFootprintAcres.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentFootprintAcres, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareChippingAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Chipping).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostSharePruningAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Pruning).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareThinningAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Thinning).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareSlashAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Slash).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMasticationAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Mastication).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareGrazingAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Grazing).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLopAndScatterAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.LopAndScatter).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareBiomassRemovalAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BiomassRemoval).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareHandPileAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPile).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareBroadcastBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BroadcastBurn).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareHandPileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPileBurn).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMachinePileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.MachinePileBurn).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareOtherTreatmentAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Other).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);

            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareAllocatedAmount.ToGridHeaderString(), a => a.Treatments.First()?.GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareTotalCost.ToGridHeaderString(), s => s.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardLandownerCostShareLineItemTotalCost, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareActualMatch.ToGridHeaderString(), s => s.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardLandownerCostShareLineItemActualMatch, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareGrantCost.ToGridHeaderString(), s => s.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardLandownerCostShareLineItemGrantCost, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareNotes.ToGridHeaderString(), a => a.Treatments.First().GrantAllocationAwardLandownerCostShareLineItem?.GrantAllocationAwardLandownerCostShareLineItemNotes, 250, AgGridColumnFilterType.Text);

        }
    }
}