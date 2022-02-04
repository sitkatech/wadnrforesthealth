using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantAgreementGridSpec : GridSpec<Models.Agreement>
    {
        public static string GrantNumberHiddenColumnName = "GrantNumberAsText";

        public GrantGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;
            var userHasDeletePermissions = new GrantDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new GrantCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.New());
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, "Create a new Grant");
            }

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            // hidden column for grant number for use by JavaScript
            Add(GrantNumberHiddenColumnName, x => x.GrantNumber, 0);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }

            if (userHasCreatePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDuplicateIconAndLinkBootstrap(x.GetDuplicateUrl(), 950, $"Duplicate {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{x.GrantName}\" to New {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}"), 30, DhtmlxGridColumnFilterType.None);
            }

            //Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), FirmaDhtmlxGridHtmlHelpers.FactSheetIcon.ToString()), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantNumber), GrantAllocationGridSpec.GrantNumberColumnWidth, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.CFDA.ToGridHeaderString(), x => x.CFDANumber, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantTitle), 250, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.TotalAwardAmount.ToGridHeaderString(), x => x.GetTotalAwardAmount(), 90, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.GrantCurrentBalance.ToGridHeaderString(), x => x.GetCurrentBalanceOfGrantBasedOnAllGrantAllocationExpenditures(), 90, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.GrantStartDate.ToGridHeaderString(), x => x.StartDate, 90, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDate, 90, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantStatus.ToGridHeaderString(), x => x.GrantStatus.GrantStatusName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantType.ToGridHeaderString(), x => x.GrantTypeDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            
        }
    }
}