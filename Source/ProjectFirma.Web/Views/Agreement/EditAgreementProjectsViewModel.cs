using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System.Diagnostics;

namespace ProjectFirma.Web.Views.Agreement
{
    [DebuggerDisplay("ProjectID: {ProjectID} - ProjectNumber: {ProjectNumber} - ProjectName: {ProjectName}")]
    public class ProjectJson
    {
        public int ProjectID { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        
        // For use by model binder
        public ProjectJson() { }

        public ProjectJson(Models.Project project)
        {
            this.ProjectID = project.ProjectID;
            this.ProjectNumber = project.FhtProjectNumber;
            this.ProjectName = project.DisplayName;
        }

        public static List<ProjectJson> MakeProjectJsonsFromProjects(List<Models.Project> projects, bool doAlphaSort = true)
        {
            var outgoingProjects = projects;
            if (doAlphaSort)
            {
                outgoingProjects = projects.OrderBy(p => p.DisplayName).ThenBy(p => p.FhtProjectNumber).ToList();
            }
            return outgoingProjects.Select(p => new ProjectJson(p)).ToList();
        }
    }

    public class EditAgreementProjectsViewModel : FormViewModel
    {
        public int AgreementId { get; set; }
        public List<ProjectJson> ProjectJsons { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementProjectsViewModel()
        {
            ProjectJsons = new List<ProjectJson>();
        }

        public EditAgreementProjectsViewModel(Models.Agreement agreement)
        {
            AgreementId = agreement.AgreementID;
            var projects = agreement.AgreementProjects.Select(x => x.Project).ToList();
            ProjectJsons = ProjectJson.MakeProjectJsonsFromProjects(projects);
        }

        public void UpdateModel(Models.Agreement agreement)
        {
            // Clear out existing AgreementProjects
            agreement.AgreementProjects.ToList().ForEach(ap => ap.DeleteFull(HttpRequestStorage.DatabaseEntities));

            // Create all-new AgreementProjects
            var allPossibleProjects = HttpRequestStorage.DatabaseEntities.Projects.ToList();
            List<int> allSelectedProjectIds = ProjectJsons.Select(pj => pj.ProjectID).ToList();
            var allSelectedProjects = allPossibleProjects.Where(p => allSelectedProjectIds.Contains(p.ProjectID)).ToList();

            agreement.AgreementProjects = allSelectedProjects.Select(p => new AgreementProject(agreement, p)).ToList();
        }
    }
}