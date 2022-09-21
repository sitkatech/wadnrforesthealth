using System.Collections.Generic;
using System.Linq;
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

        public ProjectsViewData(Person currentPerson, Models.FirmaPage firmaPage, List<ProjectCustomGridConfiguration> projectCustomFullGridConfigurations) : base(currentPerson, firmaPage)
        {
            var projectDetails = HttpRequestStorage.DatabaseEntities.Projects;;
            ProjectIndexGridSpec = new ProjectIndexGridSpec(currentPerson, true, true, new Dictionary<int, vTotalTreatedAcresByProject>(), new Dictionary<int, List<Models.Program>>())
                { ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", SaveFiltersInCookie = true };
            GridName = "ReportProjects";
            PageTitle = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}";
            GridDataUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}