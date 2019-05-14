using System;
using System.Collections.Generic;
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
        // ProjectLocationPoint
        public string PerformanceMeasureActualYearsExemptionExplanation { get; set; }
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
        public string PerformanceMeasureNotes { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ReviewedByPersonID { get; set; }
        public string ReviewedByPersonName { get; set; }
        public int? FocusAreaID { get; set; }
        public string NoExpendituresToReportExplanation { get; set; }
        public string NoRegionsExplanation { get; set; }
        public string NoPriorityAreasExplanation { get; set; }
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
            // ProjectLocationPoint
            PerformanceMeasureActualYearsExemptionExplanation  = project.PerformanceMeasureActualYearsExemptionExplanation;
            IsFeatured = project.IsFeatured;
            ProjectLocationNotes = project.ProjectLocationNotes;
            PlannedDate = project.PlannedDate;
            ProjectLocationSimpleTypeID = project.ProjectLocationSimpleTypeID;
            ProjectLocationSimpleTypeName = project.ProjectLocationSimpleType.ProjectLocationSimpleTypeName;
            PrimaryContactPersonID = project.PrimaryContactPersonID;
            PrimaryContactPersonName = project.PrimaryContactPerson.FullNameFirstLastAndOrgShortName;
            ProjectApprovalStatusID = project.ProjectApprovalStatusID;
            ProjectApprovalStatusName = project.ProjectApprovalStatus.ProjectApprovalStatusName;
            ProposingPersonID = project.ProposingPersonID;
            ProposingPersonName = project.ProposingPerson?.FullNameFirstLastAndOrgShortName;
            ProposingDate = project.ProposingDate;
            PerformanceMeasureNotes = project.PerformanceMeasureNotes;
            SubmissionDate = project.SubmissionDate;
            ApprovalDate = project.ApprovalDate;
            ReviewedByPersonID = project.ReviewedByPersonID;
            ReviewedByPersonName = project.ReviewedByPerson?.FullNameFirstLastAndOrgShortName;
            FocusAreaID = project.FocusAreaID;
            NoExpendituresToReportExplanation = project.NoExpendituresToReportExplanation;
            NoRegionsExplanation = project.NoRegionsExplanation;
            NoPriorityAreasExplanation = project.NoPriorityAreasExplanation;
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