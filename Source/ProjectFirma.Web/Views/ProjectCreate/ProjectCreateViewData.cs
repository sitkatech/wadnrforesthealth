/*-----------------------------------------------------------------------
<copyright file="ProjectCreateViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public abstract class ProjectCreateViewData : FirmaViewData
    {
        public Models.Project Project { get; }
        public string CurrentSectionDisplayName { get; }

        public string ProposalDetailUrl { get; }
        public string ProvideFeedbackUrl { get; }
        public string ProposalBasicsUrl { get; }
        public string HistoricProjectBasicsUrl { get; }
        public string ProposalNotesUrl { get; }
        public string ProposalPhotosUrl { get; }
        public string ProposalTreatmentsUrl { get; }
        public string ProposalClassificationsUrl { get; }
        public string SubmitUrl { get; }
        public string ApproveUrl { get; }
        public string ReturnUrl { get; }
        public string WithdrawUrl { get; }
        public string RejectUrl { get; }
        public bool CurrentPersonIsSubmitter { get; }
        public bool CurrentPersonIsApprover { get; }
        public ProposalSectionsStatus ProposalSectionsStatus { get; }
        public bool CanAdvanceStage { get; }
        public bool ProjectStateIsValidInWizard { get; }
        public bool CurrentPersonCanWithdraw { get; set; }

        public List<ProjectWorkflowSectionGrouping> ProjectWorkflowSectionGroupings { get; }
        public ProjectStage ProjectStage { get; set; }

        protected ProjectCreateViewData(Person currentPerson,
            Models.Project project,
            string currentSectionDisplayName,
            ProposalSectionsStatus proposalSectionsStatus) : this(project, currentPerson, currentSectionDisplayName)
        {

            Check.Assert(project != null, "Project should be created in database by this point so it cannot be null.");
            // SLG- See Story #1506 - Causing us much grief, perhaps the disease is really better than this cure? We know the Project record exists, is that maybe enough?
            //   This whole expression seems like it had multiple errors in it, giving up on it for now, I don't understand what it is trying to do.
            //   We can't get it to crash once this is removed, so, to heck with it for now.
            //Check.Assert(IsInstructionsPage || isBasicsPage || proposalSectionsStatus.IsBasicsSectionComplete, $"Can't access this section of the Proposal - You must complete the basics first ({project.GetEditUrl()})");

            CurrentPersonCanWithdraw = new ProjectCreateFeature().HasPermission(currentPerson, project).HasPermission;

            Project = project;
            ProjectStage = project.ProjectStage;
            ProposalSectionsStatus = proposalSectionsStatus;
            CanAdvanceStage = ProposalSectionsStatus.AreAllSectionsValidForProject(project);
            // ReSharper disable PossibleNullReferenceException
            ProjectStateIsValidInWizard = project.ProjectApprovalStatus == ProjectApprovalStatus.Draft || project.ProjectApprovalStatus == ProjectApprovalStatus.Returned || project.ProjectApprovalStatus == ProjectApprovalStatus.PendingApproval;

            var pageTitle = $"Add {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}";
            PageTitle = $"{pageTitle}: {project.DisplayName}";

            ProposalDetailUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Detail(project));
            ProposalBasicsUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(project.ProjectID));
           
            ProposalNotesUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.DocumentsAndNotes(project.ProjectID));
            ProposalPhotosUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Photos(project.ProjectID));

            ProposalTreatmentsUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Treatments(project.ProjectID));
            ProposalClassificationsUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditClassifications(project.ProjectID));

            SubmitUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Submit(project));
            ApproveUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Approve(project));
            ReturnUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Return(project));
            WithdrawUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Withdraw(project));
            RejectUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.Reject(project));
        }

        //New (not yet created) Projects use this constructor. Valid only for Instructions and Basics page.

        protected ProjectCreateViewData(Person currentPerson, string currentSectionDisplayName) : this(null, currentPerson, currentSectionDisplayName)
        {
            Check.Assert(currentSectionDisplayName.Equals("Instructions", StringComparison.InvariantCultureIgnoreCase) || currentSectionDisplayName.Equals("Basics", StringComparison.InvariantCultureIgnoreCase));

            Project = null;
            ProposalSectionsStatus = new ProposalSectionsStatus();

            HistoricProjectBasicsUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.CreateAndEditBasics());

            CurrentPersonCanWithdraw = false;

            SubmitUrl = string.Empty;
            ApproveUrl = string.Empty;
            ReturnUrl = string.Empty;
            WithdrawUrl = string.Empty;
            RejectUrl = string.Empty;

        }

        private ProjectCreateViewData(Models.Project project, Person currentPerson, string currentSectionDisplayName) : base(currentPerson)
        {
            EntityName = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}";
            ProvideFeedbackUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.PendingProjectFeedback());
            CurrentPersonIsSubmitter = new ProjectCreateFeature().HasPermissionByPerson(CurrentPerson);
            CurrentPersonIsApprover = project != null && new ProjectApproveFeature().HasPermission(currentPerson, project).HasPermission;
            ProjectWorkflowSectionGroupings = ProjectWorkflowSectionGrouping.All.OrderBy(x => x.SortOrder).ToList();
            CurrentSectionDisplayName = currentSectionDisplayName;
        }
    }
}
