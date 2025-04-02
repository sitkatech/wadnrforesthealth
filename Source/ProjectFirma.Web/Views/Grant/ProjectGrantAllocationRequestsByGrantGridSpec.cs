using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Grant
{
    public class ProjectGrantAllocationRequestsByGrantGridSpec : GridSpec<Models.ProjectGrantAllocationRequest>
    {
        public ProjectGrantAllocationRequestsByGrantGridSpec()
        {
            Add(Models.FieldDefinition.GrantAllocation.ToGridHeaderString(),
                a => new HtmlLinkObject(a.GrantAllocation.DisplayName, a.GrantAllocation.GetDetailUrl()).ToJsonObjectForAgGrid(),
                200,
                AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.Project.ToGridHeaderString(),
                a => new HtmlLinkObject(a.Project.DisplayName,a.Project.GetDetailUrl()).ToJsonObjectForAgGrid(),
                200,
                AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestMatchAmount.ToGridHeaderString(), a => a.MatchAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestPayAmount.ToGridHeaderString(), a => a.PayAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestTotalAmount.ToGridHeaderString(), a => a.TotalAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
        }
    }
}