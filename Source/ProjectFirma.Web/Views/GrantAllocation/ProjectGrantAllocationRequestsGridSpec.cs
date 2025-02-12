﻿using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class ProjectGrantAllocationRequestsGridSpec : GridSpec<Models.ProjectGrantAllocationRequest>
    {
        public ProjectGrantAllocationRequestsGridSpec()
        {
            Add(Models.FieldDefinition.Project.ToGridHeaderString(),
                a => new HtmlLinkObject(a.Project.DisplayName,a.Project.GetDetailUrl()).ToJsonObjectForAgGrid(),
                350,
                AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestMatchAmount.ToGridHeaderString(), a => a.MatchAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestPayAmount.ToGridHeaderString(), a => a.PayAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestTotalAmount.ToGridHeaderString(), a => a.TotalAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
        }
    }
}