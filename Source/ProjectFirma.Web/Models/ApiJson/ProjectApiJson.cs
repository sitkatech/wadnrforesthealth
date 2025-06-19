using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("ProjectID: {ProjectID} - ProjectName: {ProjectName}")]
    public class ProjectApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (ProjectTypeName, ReviewedByPersonName, etc.)
        public int ProjectID { get; set; }
        public int ProjectTypeID { get; set; }
        public string ProjectTypeName { get; set; }
        public int ProjectStageID { get; set; }
        public string ProjectStageName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal? EstimatedTotalCost { get; set; }
        public DbGeometry ProjectLocationPoint { get; set; }
        public bool IsFeatured { get; set; }
        public string ProjectLocationNotes { get; set; }
        public DateTime? PlannedDate { get; set; }
        public int ProjectLocationSimpleTypeID { get; set; }
        public string ProjectLocationSimpleTypeName { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public string PrimaryContactPersonName { get; set; }
        public int ProjectApprovalStatusID { get; set; }
        public string ProjectApprovalStatusName { get; set; }
        public int? ProposingPersonID { get; set; }
        public string ProposingPersonName { get; set; }
        public DateTime? ProposingDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ReviewedByPersonID { get; set; }
        public string ReviewedByPersonName { get; set; }
        public int? FocusAreaID { get; set; }
        public string NoExpendituresToReportExplanation { get; set; }
        public string NoRegionsExplanation { get; set; }
        public string NoPriorityLandscapesExplanation { get; set; }
        public DateTime? ExpirationDate { get; set; }

        // For use by model binder
        public ProjectApiJson()
        {
        }

        public ProjectApiJson(Project project)
        {
            ProjectID = project.ProjectID;
            ProjectTypeID = project.ProjectTypeID;
            ProjectTypeName = project.ProjectType.ProjectTypeName;
            ProjectStageID = project.ProjectStageID;
            ProjectStageName = project.ProjectStage.ProjectStageName;
            ProjectName = project.ProjectName;
            ProjectDescription = project.ProjectDescription;
            CompletionDate = project.CompletionDate;
            EstimatedTotalCost = project.EstimatedTotalCost;
            ProjectLocationPoint = project.ProjectLocationPoint;

            IsFeatured = project.IsFeatured;
            ProjectLocationNotes = project.ProjectLocationNotes;
            PlannedDate = project.PlannedDate;
            ProjectLocationSimpleTypeID = project.ProjectLocationSimpleTypeID;
            ProjectLocationSimpleTypeName = project.ProjectLocationSimpleType.ProjectLocationSimpleTypeName;

            var primaryContact = project.GetPrimaryContact();
            PrimaryContactPersonID = primaryContact?.PersonID;
            PrimaryContactPersonName = primaryContact?.FullNameFirstLastAndOrgShortName;

            ProjectApprovalStatusID = project.ProjectApprovalStatusID;
            ProjectApprovalStatusName = project.ProjectApprovalStatus.ProjectApprovalStatusName;
            ProposingPersonID = project.ProposingPersonID;
            ProposingPersonName = project.ProposingPerson?.FullNameFirstLastAndOrgShortName;
            ProposingDate = project.ProposingDate;

            SubmissionDate = project.SubmissionDate;
            ApprovalDate = project.ApprovalDate;
            ReviewedByPersonID = project.ReviewedByPersonID;
            ReviewedByPersonName = project.ReviewedByPerson?.FullNameFirstLastAndOrgShortName;
            FocusAreaID = project.FocusAreaID;
            NoExpendituresToReportExplanation = project.NoExpendituresToReportExplanation;
            NoRegionsExplanation = project.NoRegionsExplanation;
            NoPriorityLandscapesExplanation = project.NoPriorityLandscapesExplanation;
            ExpirationDate = project.ExpirationDate;
        }

        public static List<ProjectApiJson> MakeProjectApiJsonsFromProjects(List<Project> projects, bool doAlphaSort = true)
        {
            var outgoingProjects = projects;
            if (doAlphaSort)
            {
                outgoingProjects = outgoingProjects.OrderBy(p => p.ProjectName).ToList();
            }
            return outgoingProjects.Select(proj => new ProjectApiJson(proj)).ToList();
        }
    }
}