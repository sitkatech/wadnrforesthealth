using System;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.DNRUplandRegion
{
    public class AssociatedGrantAllocationsGridSpec : GridSpec<Models.GrantAllocation>
    {
        public AssociatedGrantAllocationsGridSpec()
            { 
            Add(Models.FieldDefinition.GrantAllocationPriority.ToGridHeaderString(), x =>
            {
                var priorityColor = x.GrantAllocationPriority?.GrantAllocationPriorityColor ?? "default";
                
                return new HtmlString(
                    $"<div style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{priorityColor}\">{x.GrantAllocationPriority?.GrantAllocationPriorityNumber.ToString()}</div>");
            }, 50, AgGridColumnFormatType.Integer, AgGridColumnFilterType.SelectFilterHtmlStrict);

            Add("Program Index",
                x => string.Join(", ",
                    x.GrantAllocationProgramIndexProjectCodes?.Where(y=>y.ProgramIndex != null).Select(y => y.ProgramIndex?.AuditDescriptionString) ??
                    Array.Empty<string>()), 80, AgGridColumnFilterType.Text);

            Add("Project Code",
                x => string.Join(", ",
                    x.GrantAllocationProgramIndexProjectCodes?.Where(y=> y.ProjectCode != null).Select(y =>  y.ProjectCode?.ProjectCodeName).Distinct() ??
                    Array.Empty<string>()), 80, AgGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.GrantModification?.Grant.GetGrantNumberAsUrl(), 150, AgGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDate, 75, AgGridColumnFormatType.Date);

            Add(Models.FieldDefinition.GrantAllocationFundFSPs.ToGridHeaderString(), x => x.HasFundFSPs.ToYesNo("N/A"), 80, AgGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString(), x => new HtmlLinkObject(x.DisplayName, x.SummaryUrl).ToJsonObjectForAgGrid(), 175, AgGridColumnFilterType.HtmlLinkJson);

            Add(Models.FieldDefinition.GrantAllocationSource.ToGridHeaderString(), x => x.GrantAllocationSource?.GrantAllocationSourceDisplayName, 175,
                AgGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationAllocation.ToGridHeaderString(), x => x.GetAllocationStringForDnrUplandRegionGrid(), 75, AgGridColumnFormatType.None, AgGridColumnFilterType.Html);

            Add(Models.FieldDefinition.AllocationAmount.ToGridHeaderString(), x => x.AllocationAmount, 100, AgGridColumnFormatType.CurrencyWithCents,
                AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationOverallBalance.ToGridHeaderString(), x => x.GetOverallBalance(), 75, AgGridColumnFormatType.CurrencyWithCents,
                AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationContractualBalance.ToGridHeaderString(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Contractual)
                    ?.BudgetMinusExpendituresFromDatamart, 100, AgGridColumnFormatType.CurrencyWithCents,
                AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationTravelBalance.ToGridHeaderString(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Travel)
                    ?.BudgetMinusExpendituresFromDatamart, 75, AgGridColumnFormatType.CurrencyWithCents,
                AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationStaffBalance.ToGridHeaderString(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Personnel)
                    ?.BudgetMinusExpendituresFromDatamart, 75, AgGridColumnFormatType.CurrencyWithCents,
                AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationLikelyToUse.ToGridHeaderString(),
                x => x.ToLikelyToUsePeopleListDisplayForAgGrid(),
                175, AgGridColumnFilterType.HtmlLinkListJson);

            Add(Models.FieldDefinition.GrantAllocationCompleted.ToGridHeaderString(), x =>
            {
                var completed = (decimal)(x.GetTotalBudgetVsActualLineItem().ExpendituresFromDatamart /
                                          (x.GetOverallBalance() == 0 ? 1 : x.GetOverallBalance()));
                return new HtmlString(
                    $"<div style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{x.GetAllocationCssClass(completed)}\">{completed.ToStringPercent().HtmlEncode()}</div>");
            }, 100, AgGridColumnFormatType.Percent, AgGridColumnFilterType.Html);
        




        }
    }
}