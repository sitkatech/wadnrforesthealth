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

        public GrantAgreementGridSpec()
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()}";


            Add(Models.FieldDefinition.AgreementType.ToGridHeaderString("Type"), x => x.AgreementType?.AgreementTypeAbbrev, 70, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.AgreementNumber.ToGridHeaderString("Number"), x => x.AgreementNumber, 100, DhtmlxGridColumnFilterType.Html);
            //Add(Models.FieldDefinition.Grant.ToGridHeaderString(), x => x.GetListOfGrantHrefs(), 130, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetOrganizationDetailUrl(), x.Organization.DisplayName), 130, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.AgreementTitle.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.AgreementTitle), 180, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.AgreementStartDate.ToGridHeaderString("Start Date"), x => x.StartDate, 120, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.AgreementEndDate.ToGridHeaderString("End Date"), x => x.EndDate, 120, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.AgreementAmount.ToGridHeaderString("Amount"), x => x.AgreementAmount, 70, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProgramIndex.ToGridHeaderString(), x => x.ProgramIndices.ToDistinctOrderedCsvList(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectCode.ToGridHeaderString(), x => x.ProjectCodes.ToDistinctOrderedCsvList(), 90, DhtmlxGridColumnFilterType.Text);

        }
    }
}