using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantAllocationBudgetLineItemGridSpec : GridSpec<Models.GrantAllocationBudgetLineItemForGrid>
    {
        public GrantAllocationBudgetLineItemGridSpec()
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;

            //CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            Add(Models.FieldDefinition.GrantModificationName.ToGridHeaderString(), x => x.GrantModification.GetGrantModificationNameAsUrl(), 225, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GrantAllocation.GetDetailUrl(), x.GrantAllocation.GrantAllocationName), 225,DhtmlxGridColumnFilterType.Text);
            Add(CostType.Personnel.CostTypeDisplayName, x => x.PersonnelAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.Benefits.CostTypeDisplayName, x => x.BenefitsAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.Travel.CostTypeDisplayName, x => x.TravelAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.Supplies.CostTypeDisplayName, x => x.SuppliesAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.Contractual.CostTypeDisplayName, x => x.ContractualAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.IndirectCosts.CostTypeDisplayName, x => x.IndirectCostsAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.Other.CostTypeDisplayName, x => x.OtherAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(CostType.Equipment.CostTypeDisplayName, x => x.EquipmentAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);

            Add($"Total", x => x.TotalAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
        }
    }
}