﻿using System.Linq;
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

            Add("Lead Implementer", x => x.GetLeadImplementer().GetDisplayNameAsUrl(), 300, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.Program.ToGridHeaderString(), x => x.ProjectPrograms.ToProgramListDisplay(true), 300, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ProjectName), 300, DhtmlxGridColumnFilterType.Html);

            var landownerRelationshipType = ProjectPersonRelationshipType.PrivateLandowner;
            var userHasViewLandownerNamePermissions = landownerRelationshipType.IsViewableByUser(currentPerson);
            if (userHasViewLandownerNamePermissions)
            {
                Add(Models.FieldDefinition.Landowner.ToGridHeaderString(), x => string.Join(", ", x.GetPrivateLandowners().Select(y => y.FullNameFirstLast)), 150, DhtmlxGridColumnFilterType.Text);
            }

            Add(Models.FieldDefinition.County.ToGridHeaderString(), x => new HtmlString(string.Join(", ", x.GetProjectCounties().Select(y => y.GetCountyDisplayNameAsUrl()))), 150, DhtmlxGridColumnFilterType.Text);

            Add(Models.FieldDefinition.PrimaryContact.ToGridHeaderString(), x => x.GetPrimaryContact().GetFullNameFirstLastAsUrl(), 150, DhtmlxGridColumnFilterType.Text);


            Add($"Total {Models.FieldDefinition.TreatedAcres.GetFieldDefinitionLabelPluralized()}", x => x.TotalTreatedAcres, 90, DhtmlxGridColumnFormatType.Decimal);
            //Add($"Total {Models.FieldDefinition.FootprintAcres.GetFieldDefinitionLabelPluralized()}", x => x.TotalFootprintAcres, 90, DhtmlxGridColumnFormatType.Decimal);


            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType.DisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.ProjectStage.ProjectStageDisplayName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);


            Add(Models.FieldDefinition.ProjectApplicationDate.ToGridHeaderString(), x => x.GetApplicationDate(), 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectInitiationDate.ToGridHeaderString(), x => x.GetPlannedDate(), 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ExpirationDate.ToGridHeaderString(), x => x.GetExpirationDateFormatted(), 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.CompletionDate.ToGridHeaderString(), x => x.GetCompletionDateFormatted(), 90, DhtmlxGridColumnFilterType.Text);

            Add($"Total {Models.FieldDefinition.PaymentAmount.GetFieldDefinitionLabelPluralized()}", x => x.GetTotalPaymentAmount(), 90, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add($"Total {Models.FieldDefinition.MatchAmount.GetFieldDefinitionLabelPluralized()}", x => x.GetTotalMatchAmount(), 90, DhtmlxGridColumnFormatType.CurrencyWithCents);


            Add(Models.FieldDefinition.PercentageMatch.ToGridHeaderString(), x => x.PercentageMatch, 90, DhtmlxGridColumnFormatType.Percent);

            Add(Models.FieldDefinition.GrantAllocation.ToGridHeaderString(), x => x.GetExpectedFundingGrantAllocationsAsCommaDelimitedList(), 300, DhtmlxGridColumnFilterType.Html);



            if (userHasTagManagePermissions)
            {
                Add("Tags", x => new HtmlString(!x.ProjectTags.Any() ? string.Empty : string.Join(", ", x.ProjectTags.Select(pt => pt.Tag.DisplayNameAsUrl))), 100, DhtmlxGridColumnFilterType.Html);
            }




            //old columns not needed
            //Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetFactSheetUrl(), FirmaDhtmlxGridHtmlHelpers.FactSheetIcon.ToString()), 30, DhtmlxGridColumnFilterType.None);


            //Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.FhtProjectNumber), 100, DhtmlxGridColumnFilterType.Text);
            
            
            //if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            //{
            //    Add(Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.ToGridHeaderString(), x => x.GetCanStewardProjectsOrganization().GetShortNameAsUrl(), 150,
            //        DhtmlxGridColumnFilterType.Html);
            //}
            //Add(Models.FieldDefinition.PrimaryContactOrganization.ToGridHeaderString(), x => x.GetPrimaryContactOrganization().GetShortNameAsUrl(), 150, DhtmlxGridColumnFilterType.Html);
            
            
            //Add(Models.FieldDefinition.EstimatedTotalCost.ToGridHeaderString(), x => x.EstimatedTotalCost, 110, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            //Add(Models.FieldDefinition.ProjectGrantAllocationRequestTotalAmount.ToGridHeaderString(), x => x.GetTotalFunding(), 110, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            //Add(Models.FieldDefinition.ProjectDescription.ToGridHeaderString(), x => x.ProjectDescription, 300);
                      
        }
    }
}