﻿using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class TreatmentUpdateGridSpec : GridSpec<Models.TreatmentUpdate>
    {
        public TreatmentUpdateGridSpec(Person currentPerson, Models.ProjectUpdateBatch projectUpdateBatchForCreatingNewTreatments)
        {
            this.ObjectNameSingular = "Treatment";
            this.ObjectNamePlural = "Treatments";
            bool userHasEditPermissions = new GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            int buttonGridWidth = 30;

            if (userHasEditPermissions)
            {
                var createUrl = SitkaRoute<TreatmentUpdateController>.BuildUrlFromExpression(x => x.NewTreatmentUpdateFromProjectUpdateBatch(projectUpdateBatchForCreatingNewTreatments));
                this.CreateEntityModalDialogForm = new ModalDialogForm(createUrl, 950, $"Create new {ObjectNameSingular} for {projectUpdateBatchForCreatingNewTreatments.Project.DisplayName}");
            }

            Add("Edit", x => x.ImportedFromGis.HasValue && x.ImportedFromGis.Value ? new HtmlString(string.Empty) : AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTreatmentUpdateUrl(), $"Edit Treatment Update"), userHasEditPermissions), buttonGridWidth, AgGridColumnFilterType.None);

            Add("Treatment Area Name", a => a.ProjectLocationUpdate.ProjectLocationUpdateName, 200, AgGridColumnFilterType.SelectFilterStrict);
            Add("Treatment Update ID", a => a.TreatmentUpdateID.ToString(), 75, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.TreatmentType.GetFieldDefinitionLabel(), a => a.TreatmentType.TreatmentTypeDisplayName, 120, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TreatmentCode.GetFieldDefinitionLabel(), a => a.TreatmentCode?.TreatmentCodeDisplayName, 120, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TreatmentDetailedActivityType.GetFieldDefinitionLabel(), a => a.TreatmentDetailedActivityType.TreatmentDetailedActivityTypeDisplayName, 120, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareFootprintAcres.GetFieldDefinitionLabel(), a => a.TreatmentFootprintAcres, 60, AgGridColumnFormatType.Decimal);
            Add(Models.FieldDefinition.TreatedAcres.GetFieldDefinitionLabel(), a => a.TreatmentTreatedAcres ?? 0, 60, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.TreatmentCostPerAcre.GetFieldDefinitionLabel(), a => a.CostPerAcre, 60, AgGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.TreatmentTotalCost.GetFieldDefinitionLabel(), a => (a.TreatmentTreatedAcres ?? 0) * (a.CostPerAcre ?? 0), 60, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareStartDate.GetFieldDefinitionLabel(), a => a.TreatmentStartDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareEndDate.GetFieldDefinitionLabel(), a => a.TreatmentEndDate, 125, AgGridColumnFormatType.Date);
            Add("Treatment Notes", a => a.TreatmentNotes, 200, AgGridColumnFilterType.Text);
            Add("Imported From GIS", a => a.ImportedFromGis.ToYesNo("No"), 50, AgGridColumnFilterType.SelectFilterStrict );
        }
    }
}