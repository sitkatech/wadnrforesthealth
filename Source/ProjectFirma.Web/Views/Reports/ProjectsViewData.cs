using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ProjectIndexGridSpec ProjectIndexGridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public HtmlString GenerateReportString { get; }

        public ProjectsViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            var projectDetails = HttpRequestStorage.DatabaseEntities.Projects;
            ProjectIndexGridSpec = new ProjectIndexGridSpec(currentPerson, true, true, new Dictionary<int, vTotalTreatedAcresByProject>(), new Dictionary<int, List<Models.Program>>())
            { ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", SaveFiltersInCookie = true };
            GridName = "ReportProjects";
            PageTitle = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}";
            GridDataUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());

            var tagIconHtml = $"<span style=\"margin-right:5px\">{BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-file")}</span>";
            var getProjectIDFunctionString = $"function() {{ return Sitka.{GridName}.getValuesFromCheckedGridRows(0, 'ProjectID', 'ProjectIDList'); }}";
            var dialogUrl = SitkaRoute<ReportsController>.BuildUrlFromExpression(x => x.SelectReportToGenerateFromSelectedProjects());
            var dialogTitle = "Confirm Generate Reports";

            GenerateReportString =
               ModalDialogFormHelper.ModalDialogFormLink(
                   $"{tagIconHtml} Generate Reports",
                    dialogUrl,
                    dialogTitle,
                    ModalDialogFormHelper.DefaultDialogWidth,
                    "Generate",
                    "Close",
                   new List<string> { "btn", "btn-firma" },
                    null,
                    getProjectIDFunctionString);
        }
    }
}