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
    public class TreatmentGridSpec : GridSpec<Models.Treatment>
    {
        public TreatmentGridSpec(Person currentPerson, Models.Project projectForCreatingNewTreatments)
        {
            this.ObjectNameSingular = "Treatment";
            this.ObjectNamePlural = "Treatments";
            bool userHasEditPermissions = new GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            int buttonGridWidth = 30;

            if (userHasEditPermissions)
            {
                var createUrl = SitkaRoute<TreatmentController>.BuildUrlFromExpression(x => x.NewTreatmentFromProject(projectForCreatingNewTreatments));
                this.CreateEntityModalDialogForm = new ModalDialogForm(createUrl, 950, $"Create new {ObjectNameSingular} for {projectForCreatingNewTreatments.DisplayName}");
            }

            Add(string.Empty, x => x.ImportedFromGis.HasValue && x.ImportedFromGis.Value ? new HtmlString(string.Empty) : DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTreatmentUrl(), $"Edit Treatment"), userHasEditPermissions), buttonGridWidth, DhtmlxGridColumnFilterType.None);

            Add("Treatment Area Name", a => a.ProjectLocation?.ProjectLocationName ?? "", 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Treatment ID", a => a.TreatmentID.ToString(), 75, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.TreatmentType.GetFieldDefinitionLabel(), a => a.TreatmentType.TreatmentTypeDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TreatmentCode.GetFieldDefinitionLabel(), a => a.TreatmentCode?.TreatmentCodeDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TreatmentDetailedActivityType.GetFieldDefinitionLabel(), a => a.TreatmentDetailedActivityType.TreatmentDetailedActivityTypeDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareFootprintAcres.GetFieldDefinitionLabel(), a => a.TreatmentFootprintAcres, 60, DhtmlxGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TreatedAcres.GetFieldDefinitionLabel(), a => a.TreatmentTreatedAcres ?? 0, 60, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.TreatmentCostPerAcre.GetFieldDefinitionLabel(), a => a.CostPerAcre, 60, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.TreatmentTotalCost.GetFieldDefinitionLabel(), a => (a.TreatmentTreatedAcres ?? 0) * (a.CostPerAcre ?? 0), 60, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.TreatmentStartDate, 125, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.TreatmentEndDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("Treatment Notes", a => a.TreatmentNotes, 200, DhtmlxGridColumnFilterType.Text);
            Add("Imported From GIS", a => a.ImportedFromGis.ToYesNo("No"), 50, DhtmlxGridColumnFilterType.SelectFilterStrict );
        }
    }    
}