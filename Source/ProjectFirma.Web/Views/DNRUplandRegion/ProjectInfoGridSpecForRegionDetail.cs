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
    public class ProjectInfoGridSpecForRegionDetail : GridSpec<Models.Project>
    {
        public ProjectInfoGridSpecForRegionDetail(Person currentPerson, bool allowTaggingFunctionality)
        {
            var userHasTagManagePermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);
            //if (userHasTagManagePermissions && allowTaggingFunctionality)
            //{
            //    BulkTagModalDialogForm = new BulkTagModalDialogForm(SitkaRoute<TagController>.BuildUrlFromExpression(x => x.BulkTagProjects(null)), $"Tag Checked {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", $"Tag {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}");
            //    AddCheckBoxColumn();
            //    Add("ProjectID", x => x.ProjectID, 0);
            //}

            Add("Lead Implementer", x => GetLeadImplementerHtmlLinkJson(x.GetLeadImplementer()) , 300, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.Program.ToGridHeaderString(), x => x.ProjectPrograms.ToProgramListDisplayForAgGrid(true), 300, AgGridColumnFilterType.HtmlLinkListJson);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => new HtmlLinkObject(x.ProjectName, x.GetDetailUrl()).ToJsonObjectForAgGrid(), 300, AgGridColumnFilterType.HtmlLinkJson);

            var landownerRelationshipType = ProjectPersonRelationshipType.PrivateLandowner;
            var userHasViewLandownerNamePermissions = landownerRelationshipType.IsViewableByUser(currentPerson);
            if (userHasViewLandownerNamePermissions)
            {
                Add(Models.FieldDefinition.Landowner.ToGridHeaderString(), x => string.Join(", ", x.GetPrivateLandowners().Select(y => y.FullNameFirstLast)), 150, AgGridColumnFilterType.Text);
            }

            Add(Models.FieldDefinition.County.ToGridHeaderString(), x => new HtmlString(string.Join(", ", x.GetProjectCounties().Select(y => y.DisplayName))), 150, AgGridColumnFilterType.Text);

            Add(Models.FieldDefinition.PrimaryContact.ToGridHeaderString(), x => GetPrimaryContactHtmlLinkJson(x.GetPrimaryContact()), 150, AgGridColumnFilterType.HtmlLinkJson);


            Add($"Total {Models.FieldDefinition.TreatedAcres.GetFieldDefinitionLabelPluralized()}", x => x.TotalTreatedAcres, 90, AgGridColumnFormatType.Decimal);
            //Add($"Total {Models.FieldDefinition.FootprintAcres.GetFieldDefinitionLabelPluralized()}", x => x.TotalFootprintAcres, 90, AgGridColumnFormatType.Decimal);


            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType.DisplayName, 120, AgGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);


            Add(Models.FieldDefinition.ProjectApplicationDate.ToGridHeaderString(), x => x.GetApplicationDate(), 90, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectInitiationDate.ToGridHeaderString(), x => x.GetPlannedDate(), 90, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ExpirationDate.ToGridHeaderString(), x => x.GetExpirationDateFormatted(), 90, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.CompletionDate.ToGridHeaderString(), x => x.GetCompletionDateFormatted(), 90, AgGridColumnFilterType.Text);

            Add($"Total {Models.FieldDefinition.PaymentAmount.GetFieldDefinitionLabelPluralized()}", x => x.GetTotalPaymentAmount(), 90, AgGridColumnFormatType.CurrencyWithCents);
            Add($"Total {Models.FieldDefinition.MatchAmount.GetFieldDefinitionLabelPluralized()}", x => x.GetTotalMatchAmount(), 90, AgGridColumnFormatType.CurrencyWithCents);


            Add(Models.FieldDefinition.PercentageMatch.ToGridHeaderString(), x => x.PercentageMatch, 90, AgGridColumnFormatType.Percent);

            Add(Models.FieldDefinition.FundSourceAllocation.ToGridHeaderString(), x => x.GetExpectedFundingFundSourceAllocationsAsCommaDelimitedListForAgGrid(), 300, AgGridColumnFilterType.HtmlLinkListJson);



            if (userHasTagManagePermissions)
            {
                Add("Tags", x => new HtmlString(!x.ProjectTags.Any() ? string.Empty : string.Join(", ", x.ProjectTags.Select(pt => pt.Tag.DisplayName))), 100, AgGridColumnFilterType.Html);
            }

                      
        }

        private string GetPrimaryContactHtmlLinkJson(Person person)
        {
            if (person == null)
            {
                return string.Empty;
            }

            return $"{{ \"link\":\"{person.GetDetailUrl()}\",\"displayText\":\"{person.FullNameFirstLast}\" }}";
        }
        
        
        private string GetLeadImplementerHtmlLinkJson(Models.Organization org)
        {
            if (org == null)
            {
                return string.Empty;
            }

            return $"{{ \"link\":\"{org.GetDetailUrl()}\",\"displayText\":\"{org.DisplayName}\" }}";
        }
    }
}