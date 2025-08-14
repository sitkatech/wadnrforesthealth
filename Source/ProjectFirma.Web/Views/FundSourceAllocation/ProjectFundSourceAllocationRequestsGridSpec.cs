using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class ProjectFundSourceAllocationRequestsGridSpec : GridSpec<Models.ProjectFundSourceAllocationRequest>
    {
        public ProjectFundSourceAllocationRequestsGridSpec()
        {
            Add(Models.FieldDefinition.Project.ToGridHeaderString(),
                a => new HtmlLinkObject(a.Project.DisplayName,a.Project.GetDetailUrl()).ToJsonObjectForAgGrid(),
                350,
                AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectFundSourceAllocationRequestMatchAmount.ToGridHeaderString(), a => a.MatchAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectFundSourceAllocationRequestPayAmount.ToGridHeaderString(), a => a.PayAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectFundSourceAllocationRequestTotalAmount.ToGridHeaderString(), a => a.TotalAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
        }
    }
}