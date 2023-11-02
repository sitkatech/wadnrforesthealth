﻿using System;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
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
            }, 50, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            Add("Program Index",
                x => string.Join(", ",
                    x.GrantAllocationProgramIndexProjectCodes?.Where(y=>y.ProgramIndex != null).Select(y => y.ProgramIndex?.AuditDescriptionString) ??
                    Array.Empty<string>()), 200, DhtmlxGridColumnFilterType.Text);

            Add("Project Code",
                x => string.Join(", ",
                    x.GrantAllocationProgramIndexProjectCodes?.Where(y=> y.ProjectCode != null).Select(y =>  y.ProjectCode?.ProjectCodeName).Distinct() ??
                    Array.Empty<string>()), 200, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.GrantModification?.Grant.GrantNumber, 200, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDate, 75, DhtmlxGridColumnFormatType.Date);

            Add(Models.FieldDefinition.GrantAllocationFundFSPs.ToGridHeaderString(), x => x.HasFundFSPs.ToYesNo("N/A"), 50, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString(), x => x.DisplayNameAsUrl, 200, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.GrantAllocationSource.ToGridHeaderString(), x => x.GrantAllocationSource?.GrantAllocationSourceDisplayName, 200,
                DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationAllocation.ToGridHeaderString(), x => x.GetAllocationStringForDnrUplandRegionGrid(), 50, DhtmlxGridColumnFormatType.None, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.AllocationAmount.ToGridHeaderString(), x => x.AllocationAmount, 50, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationOverallBalance.ToGridHeaderString(), x => x.GetOverallBalance(), 50, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationContractualBalance.ToGridHeaderString(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Contractual)
                    ?.BudgetMinusExpendituresFromDatamart, 100, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationTravelBalance.ToGridHeaderString(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Travel)
                    ?.BudgetMinusExpendituresFromDatamart, 100, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationStaffBalance.ToGridHeaderString(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Personnel)
                    ?.BudgetMinusExpendituresFromDatamart, 100, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationLikelyToUse.ToGridHeaderString(),
                x => x.ToLikelyToUsePeopleListDisplay(),
                100, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.GrantAllocationCompleted.ToGridHeaderString(), x =>
            {
                var completed = (decimal)(x.GetTotalBudgetVsActualLineItem().ExpendituresFromDatamart /
                                          (x.GetOverallBalance() == 0 ? 1 : x.GetOverallBalance()));
                return new HtmlString(
                    $"<div style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{x.GetAllocationCssClass(completed)}\">{completed.ToStringPercent().HtmlEncode()}</div>");
            }, 100, DhtmlxGridColumnFormatType.Percent, DhtmlxGridColumnFilterType.Html);
        




        }
    }
}