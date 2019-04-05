using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class ProjectFundingSourceRequestsGridSpec : GridSpec<Models.ProjectGrantAllocationRequest>
    {
        public ProjectFundingSourceRequestsGridSpec()
        {
            Add(Models.FieldDefinition.Project.ToGridHeaderString(),
                a => UrlTemplate.MakeHrefString(a.Project.GetDetailUrl(), a.Project.DisplayName),
                350,
                DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.SecuredFunding.ToGridHeaderString(), a => a.SecuredAmount, 100, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.UnsecuredFunding.ToGridHeaderString(), a => a.UnsecuredAmount, 100, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
        }
    }
}