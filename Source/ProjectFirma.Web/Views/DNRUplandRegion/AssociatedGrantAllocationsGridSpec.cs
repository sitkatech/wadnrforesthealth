using System;
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
                Add(Models.FieldDefinition.GrantAllocationPriority.ToGridHeaderString(), x => x.GrantAllocationPriority?.GrantAllocationPriorityNumber.ToString(), 50, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            Add("Program Index",
                x => string.Join(",",
                    x.GrantAllocationProgramIndexProjectCodes?.Select(y => y.ProgramIndex?.ProgramIndexTitle) ??
                    Array.Empty<string>()), 200, DhtmlxGridColumnFilterType.Text);

            Add("Project Code",
                x => string.Join(",",
                    x.GrantAllocationProgramIndexProjectCodes?.Select(y => y.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsCommaDelimited()) ??
                    Array.Empty<string>()), 200, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.GrantModification?.Grant.GrantNumber, 200, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDate, 75, DhtmlxGridColumnFormatType.Date);

            Add(Models.FieldDefinition.GrantAllocationFundFSPs.ToGridHeaderString(), x => x.HasFundFSPs.ToYesNo("N/A"), 50, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString(), x => x.DisplayNameAsUrl, 200, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.GrantAllocationSource.ToGridHeaderString(), x => x.GrantAllocationSource?.GrantAllocationSourceDisplayName, 200,
                DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationAllocation.ToGridHeaderString(), x => x.GetAllocation().ToStringPercent(), 50, DhtmlxGridColumnFilterType.Text);

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

            Add(Models.FieldDefinition.GrantAllocationCompleted.ToGridHeaderString(), x => ((decimal)x.GetTotalBudgetVsActualLineItem().ExpendituresFromDatamart / (x.GetOverallBalance() == 0 ? 1 : x.GetOverallBalance())).ToStringPercent(), 100, DhtmlxGridColumnFilterType.Text);

        }
    }
}