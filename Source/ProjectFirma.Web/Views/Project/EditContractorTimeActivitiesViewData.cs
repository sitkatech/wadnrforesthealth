using System.Collections.Generic;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class EditContractorTimeActivitiesViewData : FirmaViewData
    {
        public List<FundingSourceSimple> AllFundingSources { get; }
        public int ProjectID { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }
        public string ProjectUrl { get; }

        private EditContractorTimeActivitiesViewData(List<FundingSourceSimple> allFundingSources, Models.Project project, Person currentPerson) : base(currentPerson)
        {
            AllFundingSources = allFundingSources;
            ProjectID = project.ProjectID;
            ViewDataForAngular = new ViewDataForAngularClass(AllFundingSources, ProjectID);
            ProjectUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Detail(ProjectID));

            EntityName = Models.FieldDefinition.Project.GetFieldDefinitionLabel();
            PageTitle = $"Edit {project.DisplayName} Contractor Time";
        }

        public EditContractorTimeActivitiesViewData(Models.Project project,
            List<FundingSourceSimple> allFundingSources, Person currentPerson)
            : this(allFundingSources, project, currentPerson)
        {
        }

        public class ViewDataForAngularClass
        {
            public List<FundingSourceSimple> AllFundingSources { get; }
            public int ProjectID { get; }

            public ViewDataForAngularClass(List<FundingSourceSimple> allFundingSources, int projectID)
            {
                AllFundingSources = allFundingSources;
                ProjectID = projectID;
            }
        }
    }
}