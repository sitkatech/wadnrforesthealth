using System.Linq;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Project
{
    public class TreatmentGroupGridSpec : GridSpec<Models.TreatmentGroup>
    {
        public TreatmentGroupGridSpec(Person currentPerson, Models.Project projectToCreateNewTreatments)
        {
            this.ObjectNameSingular = "Treatment";
            this.ObjectNamePlural = "Treatments";

            bool hasCreatePermission = new TreatmentEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newTreatmentsUrl = SitkaRoute<TreatmentController>.BuildUrlFromExpression(tc => tc.NewTreatmentFromProject(projectToCreateNewTreatments));
                CreateEntityModalDialogForm = new ModalDialogForm(newTreatmentsUrl, 950, $"Create new Treatments for {projectToCreateNewTreatments.DisplayName}");                
            }
;
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentStartDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentEndDate, 125, AgGridColumnFormatType.Date);

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


        }
    }
}