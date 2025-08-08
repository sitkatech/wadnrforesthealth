using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceAgreementGridSpec : GridSpec<Models.Agreement>
    {

        public FundSourceAgreementGridSpec()
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()}";


            Add(Models.FieldDefinition.AgreementType.ToGridHeaderString("Type"), x => x.AgreementType?.AgreementTypeAbbrev, 70, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.AgreementNumber.ToGridHeaderString("Number"), x => x.AgreementNumber, 100, AgGridColumnFilterType.Html);
            //Add(Models.FieldDefinition.FundSource.ToGridHeaderString(), x => x.GetListOfFundSourceHrefs(), 130, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => new HtmlLinkObject(x.Organization.DisplayName,x.GetOrganizationDetailUrl()).ToJsonObjectForAgGrid(), 130, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.AgreementTitle.ToGridHeaderString(), x => new HtmlLinkObject(x.AgreementTitle, x.GetDetailUrl()).ToJsonObjectForAgGrid(), 180, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.AgreementStartDate.ToGridHeaderString("Start Date"), x => x.StartDate, 120, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.AgreementEndDate.ToGridHeaderString("End Date"), x => x.EndDate, 120, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.AgreementAmount.ToGridHeaderString("Amount"), x => x.AgreementAmount, 70, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProgramIndex.ToGridHeaderString(), x => x.ProgramIndices.ToDistinctOrderedCsvList(), 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectCode.ToGridHeaderString(), x => x.ProjectCodes.ToDistinctOrderedCsvList(), 90, AgGridColumnFilterType.Text);

        }
    }
}