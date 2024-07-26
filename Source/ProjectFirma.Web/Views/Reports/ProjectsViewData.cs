using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Project;
//using ProjectFirma.Web.Views.ProjectCustomGrid;
using ProjectFirmaModels.Models;

namespace ProjectFirma.Web.Views.Reports
{
    public class ProjectsViewData : FirmaViewData
    {
        public ProjectListGridSpec ProjectListGridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public HtmlString GenerateReportString { get; }

        public ProjectsViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            var projectDetails = HttpRequestStorage.DatabaseEntities.Projects;
            ProjectListGridSpec = new ProjectListGridSpec(currentPerson, new Dictionary<int, vTotalTreatedAcresByProject>(), new Dictionary<int, List<Models.Program>>())
            { ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", SaveFiltersInCookie = true };
            GridName = "ReportProjects";
            PageTitle = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}";
            GridDataUrl = SitkaRoute<ReportsController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData());

            var tagIconHtml = $"<span style=\"margin-right:5px\">{BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-file")}</span>";
            var getProjectIDFunctionString = $"function() {{ return {GridName}GetValuesFromCheckedGridRows('{AgGridHtmlHelpers.ProjectIdForModalColumnName}', 'ProjectIDList'); }}";
            var dialogUrl = SitkaRoute<ReportsController>.BuildUrlFromExpression(x => x.SelectReportToGenerateFromSelectedProjects());
            var dialogTitle = "Confirm Generate Reports";

            GenerateReportString =
               ModalDialogFormHelper.ModalDialogFormLink(null,
                   $"{tagIconHtml} Generate Reports",
                    dialogUrl,
                    dialogTitle,
                    ModalDialogFormHelper.DefaultDialogWidth,
                   ModalDialogFormHelper.SaveButtonID,
                    "Generate",
                    "Close",
                   new List<string> { "btn", "btn-firma" },
                    null,
                    getProjectIDFunctionString,
                    null,
                    null,
                   true);
        }
    }
}