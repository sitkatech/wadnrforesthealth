using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
//using ProjectFirma.Web.Views.ProjectCustomGrid;
using ProjectFirmaModels.Models;

namespace ProjectFirma.Web.Views.Reports
{
    public class ProjectsViewData : FirmaViewData
    {
        //public ProjectCustomGridSpec ProjectCustomFullGridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public ProjectsViewData(Person currentPerson, Models.FirmaPage firmaPage, List<ProjectCustomGridConfiguration> projectCustomFullGridConfigurations) : base(currentPerson, firmaPage)
        {
            var projectDetails = HttpRequestStorage.DatabaseEntities.Projects;;
            //ProjectCustomFullGridSpec = new ProjectCustomGridSpec(currentPerson, projectCustomFullGridConfigurations, ProjectCustomGridType.Reports.ToEnum, projectDetails, currentPerson);
            GridName = "ReportProjects";
            PageTitle = $"{FieldDefinitionEnum.Project.ToType().GetFieldDefinitionLabelPluralized()}";
            //GridDataUrl = SitkaRoute<ProjectCustomGridController>.BuildUrlFromExpression(tc => tc.AllActiveProjectsAndProposalsCustomGridReportsJsonData());
        }
    }
}