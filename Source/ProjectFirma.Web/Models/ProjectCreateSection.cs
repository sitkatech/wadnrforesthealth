using System.Linq;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.ProjectCreate;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectCreateSection
    {
        public abstract bool IsComplete(Project project);
        public abstract string GetSectionUrl(Project project);
    }

    public partial class ProjectCreateSectionBasics
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            var basicsResults = new BasicsViewModel(project).GetValidationResults();
            return !basicsResults.Any();
        }

        public override string GetSectionUrl(Project project)
        {
            return project != null ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(project.ProjectID)) : null;
        }
    }


    public partial class ProjectCreateSectionLocationSimple
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }

            var locationSimpleValidationResults = new LocationSimpleViewModel(project).GetValidationResults();
            return !locationSimpleValidationResults.Any() && project.ProjectLocationPoint != null;            
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditLocationSimple(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionLocationDetailed
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            return true;
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditLocationDetailed(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionDNRUplandRegions
    {
        public override bool IsComplete(Project project)
        {
            return project != null && project.IsProjectRegionValid();
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Regions(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionPriorityLandscapes
    {
        public override bool IsComplete(Project project)
        {
            return project != null && project.IsProjectPriorityLandscapeValid();
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.PriorityLandscapes(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionClassifications
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }

            var projectClassificationSimples = ProjectCreateController.GetProjectClassificationSimples(project);

            var classificationValidationResults = new EditProposalClassificationsViewModel(projectClassificationSimples).GetValidationResults();
            return !classificationValidationResults.Any();
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditClassifications(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionPhotos
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            return Basics.IsComplete(project);
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Photos(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionNotesAndDocuments
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            return Basics.IsComplete(project);
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.DocumentsAndNotes(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionExpectedFunding
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            // todo: more complicated than that.
            return Basics.IsComplete(project);
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.ExpectedFunding(project.ProjectID)) : null;
        }
    }

    // 5/15/2019 TK - commenting this out because the section is removed from the DB. WADNR may need this section in phase 2
    //public partial class ProjectCreateSectionReportedExpenditures
    //{
    //    public override bool IsComplete(Project project)
    //    {
    //        if (project == null)
    //        {
    //            return false;
    //        }
    //        var projectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
    //        var validationResults = new ExpendituresViewModel(projectGrantAllocationExpenditures,
    //                projectGrantAllocationExpenditures.CalculateCalendarYearRangeForExpenditures(project), project,
    //                project.GetExpendituresExemptReportingYears().Select(x => new ProjectExemptReportingYearSimple(x)).ToList()) {ProjectID = project.ProjectID}
    //            .GetValidationResults();
    //        return !validationResults.Any();
    //    }

    //    public override string GetSectionUrl(Project project)
    //    {
    //        return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Expenditures(project.ProjectID)) : null;
    //    }
    //}

    public partial class ProjectCreateSectionOrganizations
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            var validationResults = new OrganizationsViewModel(project, null).GetValidationResults().ToList();
            return !validationResults.Any();
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Organizations(project.ProjectID)) : null;
        }
    }
    public partial class ProjectCreateSectionContacts
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            var validationResults = new ContactsViewModel(project, null).GetValidationResults().ToList();
            return !validationResults.Any();
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Contacts(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionTreatments
    {
        public override bool IsComplete(Project project)
        {
            if (project == null)
            {
                return false;
            }
            return true;
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Treatments(project.ProjectID)) : null;
        }
    }

    public partial class ProjectCreateSectionCounties
    {
        public override bool IsComplete(Project project)
        {
            return project != null && project.IsProjectCountiesValid();
        }

        public override string GetSectionUrl(Project project)
        {
            return Basics.IsComplete(project) ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Counties(project.ProjectID)) : null;
        }
    }
}
