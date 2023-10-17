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
        public AssociatedGrantAllocationsGridSpec(Person currentPerson, bool allowTaggingFunctionality)
        {
            var userHasTagManagePermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);


            Add("Priority", x => x.Priority.DisplayValue(), 50, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add("Program Index",
                x => string.Join(",",
                    x.GrantAllocationProgramIndexProjectCodes?.Select(y => y.ProgramIndex?.ProgramIndexTitle) ??
                    Array.Empty<string>()), 200, DhtmlxGridColumnFilterType.Text);

            Add("Program Code",
                x => string.Join(",",
                    x.GrantAllocationProgramIndexProjectCodes?.Select(y => y.ProjectCode?.ProjectCodeTitle) ??
                    Array.Empty<string>()), 200, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantNumber.GetFieldDefinitionLabel(), x => x.GrantModification?.Grant.GrantNumber, 200, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.GrantEndDate.GetFieldDefinitionLabel(), x => x.EndDate, 75, DhtmlxGridColumnFormatType.Date);

            Add(Models.FieldDefinition.GrantAllocationFundFSPs.GetFieldDefinitionLabel(), x => x.HasFundFSPs.ToYesNo("N/A"), 50, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.GrantAllocationName.GetFieldDefinitionLabel(), x => x.DisplayNameAsUrl, 200, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.GrantAllocationSource.GetFieldDefinitionLabel(), x => x.GrantAllocationSource?.GrantAllocationSourceDisplayName, 200,
                DhtmlxGridColumnFilterType.SelectFilterStrict);

            //Tested, good to go
            Add(Models.FieldDefinition.GrantAllocationAllocation.GetFieldDefinitionLabel(), x =>
            {
                var allocation = x.GetAllocation();
                return new HtmlString(
                    $"<div class=\"blah\"; style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{x.GetAllocationCssClass(allocation)}\">{allocation.ToStringPercent().HtmlEncode()}</div>");
            }, 50, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnFilterType.Numeric);

            Add(Models.FieldDefinition.GrantAllocationAmount.GetFieldDefinitionLabel(), x => x.AllocationAmount, 50, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);
            //Tested, good to go
            Add(Models.FieldDefinition.GrantAllocationOverallBalance.GetFieldDefinitionLabel(), x => x.GetOverallBalance(), 50, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);

            //Tested
            Add(Models.FieldDefinition.GrantAllocationContractualBalance.GetFieldDefinitionLabel(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Contractual)
                    ?.BudgetMinusExpendituresFromDatamart, 100, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);
            //Tested

            Add(Models.FieldDefinition.GrantAllocationTravelBalance.GetFieldDefinitionLabel(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Travel)
                    ?.BudgetMinusExpendituresFromDatamart, 100, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);
            //Tested

            Add(Models.FieldDefinition.GrantAllocationStaffBalance.GetFieldDefinitionLabel(),
                x => x.GetAllBudgetVsActualLineItemsByCostType().FirstOrDefault(y => y.CostType == CostType.Personnel)
                    ?.BudgetMinusExpendituresFromDatamart, 100, DhtmlxGridColumnFormatType.CurrencyWithCents,
                DhtmlxGridColumnAggregationType.Total);


            Add(Models.FieldDefinition.GrantAllocationLikelyToUse.GetFieldDefinitionLabel(),
                x => string.Join(",", x.GrantAllocationLikelyPeople.Select(y => y.Person.GetFullNameFirstLastAsUrl())),
                100, DhtmlxGridColumnFilterType.Html);

            Add(Models.FieldDefinition.GrantAllocationCompleted.GetFieldDefinitionLabel(), x =>
            {
                var completed = (decimal)(x.GetTotalBudgetVsActualLineItem().ExpendituresFromDatamart /
                                          (x.GetOverallBalance() == 0 ? 1 : x.GetOverallBalance()));
                return new HtmlString(
                    $"<div class=\"blah\"; style=\"padding-right:30%;height: 94%;margin-left: -5px; width:130%;padding-top: 7px; background-color:{x.GetAllocationCssClass(completed)}\">{completed.ToStringPercent().HtmlEncode()}</div>");
            }, 100, DhtmlxGridColumnFormatType.Percent, DhtmlxGridColumnFilterType.Numeric);
        




    }
    }
}