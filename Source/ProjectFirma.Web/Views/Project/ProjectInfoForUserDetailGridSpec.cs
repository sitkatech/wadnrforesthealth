using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class ProjectInfoForUserDetailGridSpec : GridSpec<Models.ProjectPersonRelationship>
    {
        public ProjectInfoForUserDetailGridSpec(Person currentPerson, Person contactPerson = null)
        {
            Add(string.Empty, x => UrlTemplate.MakeHrefString(x.Project.GetFactSheetUrl(), FirmaDhtmlxGridHtmlHelpers.FactSheetIcon.ToString()), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.Project.GetDetailUrl(), x.Project.ProjectName), 300, DhtmlxGridColumnFilterType.Html);
            if (contactPerson != null && !contactPerson.IsFullUser())
            {
                Add(Models.FieldDefinition.ContactRelationshipType.ToGridHeaderString(),
                    x => x.ProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            }
            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                Add(Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.ToGridHeaderString(), x => x.Project.GetCanStewardProjectsOrganization().GetShortNameAsUrl(), 150,
                    DhtmlxGridColumnFilterType.Html);
            }
            Add(Models.FieldDefinition.PrimaryContactOrganization.ToGridHeaderString(), x => x.Project.GetPrimaryContactOrganization().GetShortNameAsUrl(), 150, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectInitiationDate.ToGridHeaderString(), x => x.Project.GetPlannedDate(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ExpirationDate.ToGridHeaderString(), x => x.Project.GetExpirationDateFormatted(), 115, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.CompletionDate.ToGridHeaderString(), x => x.Project.GetCompletionDateFormatted(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.EstimatedTotalCost.ToGridHeaderString(), x => x.Project.EstimatedTotalCost, 110, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestTotalAmount.ToGridHeaderString(), x => x.Project.GetTotalFunding(), 110, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectDescription.ToGridHeaderString(), x => x.Project.ProjectDescription, 300);
        }
    }
}