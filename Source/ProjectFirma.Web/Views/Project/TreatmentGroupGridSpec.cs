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
            Add(Models.FieldDefinition.TreatmentStartDate.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentStartDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.TreatmentEndDate.GetFieldDefinitionLabel(), a => a.Treatments.First().TreatmentEndDate, 125, AgGridColumnFormatType.Date);

            Add(Models.FieldDefinition.ChippingAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Chipping).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.PruningAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Pruning).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ThinningAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Thinning).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.SlashAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Slash).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.MasticationAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Mastication).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrazingAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Grazing).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.LopAndScatterAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.LopAndScatter).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.BiomassRemovalAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BiomassRemoval).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.HandPileAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPile).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.BroadcastBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BroadcastBurn).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.HandPileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPileBurn).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.MachinePileBurnAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.MachinePileBurn).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.OtherTreatmentAcres.GetFieldDefinitionLabel(), a => a.Treatments.Where(x => x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Other).Sum(x => x.TreatmentTreatedAcres) ?? 0, 75, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);


        }
    }
}