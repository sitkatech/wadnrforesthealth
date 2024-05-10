using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
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
            Add("Fact Sheet", x => UrlTemplate.MakeHrefString(x.Project.GetFactSheetUrl(), AgGridHtmlHelpers.FactSheetIcon.ToString()), 30, AgGridColumnFilterType.None);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.Project.GetDetailUrl(), x.Project.ProjectName), 300, AgGridColumnFilterType.Html);
            if (contactPerson != null && !contactPerson.IsFullUser())
            {
                Add(Models.FieldDefinition.ContactRelationshipType.ToGridHeaderString(),
                    x => x.ProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName, 150, AgGridColumnFilterType.SelectFilterStrict);
            }
            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                Add(Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.ToGridHeaderString(), x => x.Project.GetCanStewardProjectsOrganization().GetShortNameAsUrl(), 150,
                    AgGridColumnFilterType.Html);
            }
            Add(Models.FieldDefinition.PrimaryContactOrganization.ToGridHeaderString(), x => x.Project.GetPrimaryContactOrganization().GetShortNameAsUrl(), 150, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectInitiationDate.ToGridHeaderString(), x => x.Project.GetPlannedDate(), 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ExpirationDate.ToGridHeaderString(), x => x.Project.GetExpirationDateFormatted(), 115, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.CompletionDate.ToGridHeaderString(), x => x.Project.GetCompletionDateFormatted(), 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.EstimatedTotalCost.ToGridHeaderString(), x => x.Project.EstimatedTotalCost, 110, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectGrantAllocationRequestTotalAmount.ToGridHeaderString(), x => x.Project.GetTotalFunding(), 110, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectDescription.ToGridHeaderString(), x => x.Project.ProjectDescription, 300);
        }
    }
}