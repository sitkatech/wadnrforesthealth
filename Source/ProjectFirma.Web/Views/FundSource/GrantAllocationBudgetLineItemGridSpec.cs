using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
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

            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.FundSourceAllocation.GetDetailUrl(), x.FundSourceAllocation.GrantAllocationName), 225,AgGridColumnFilterType.Text);
            Add(CostType.Personnel.CostTypeDisplayName, x => x.PersonnelAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(CostType.Benefits.CostTypeDisplayName, x => x.BenefitsAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(CostType.Travel.CostTypeDisplayName, x => x.TravelAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(CostType.Supplies.CostTypeDisplayName, x => x.SuppliesAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(CostType.Contractual.CostTypeDisplayName, x => x.ContractualAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(CostType.IndirectCosts.CostTypeDisplayName, x => x.IndirectCostsAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            // 9/22/20 TK - If showing these in the grid, I recommend adding them back to the TotalAmount in the class GrantAllocationBudgetLineItemForGrid
            //Add(CostType.Other.CostTypeDisplayName, x => x.OtherAmount, 70, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            //Add(CostType.Equipment.CostTypeDisplayName, x => x.EquipmentAmount, 70, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add($"Total", x => x.TotalAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
        }
    }
}