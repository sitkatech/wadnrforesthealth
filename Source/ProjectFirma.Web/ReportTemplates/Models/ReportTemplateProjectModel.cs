﻿using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateProjectModel : ReportTemplateBaseModel
    {
        private Project Project { get; set; }
        private List<ProjectPerson> ProjectPersons { get; set; }
        private List<ProjectOrganization> ProjectOrganizations { get; set; }
        private List<ProjectImage> ProjectImages { get; set; }

        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public ReportTemplateOrganizationModel PrimaryContactOrganization { get; set; }
        public string ProjectStage { get; set; }
        public int NumberOfReportedPerformanceMeasures { get; set; }
        public ReportTemplatePersonModel ProjectPrimaryContact { get; set; }
        public string PlanningDesignStartYear { get; set; }
        public string ImplementationStartYear { get; set; }
        public string CompletionYear { get; set; }
        public string PrimaryTaxonomyLeaf { get; set; }
        public int NumberOfReportedExpenditures { get; set; }
        public string FundingType { get; set; }
        public string EstimatedTotalCost { get; set; }
        //public string SecuredFunding { get; set; }
        //public string TargetedFunding { get; set; }
        //public string NoFundingSourceIdentified { get; set; }
        public string TotalFunding { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectLastUpdated { get; set; }
        //public string CurrentProjectStatus { get; set; }
        //public string CurrentProjectStatusColor { get; set; }
        //public string FinalStatusUpdateStatus { get; set; }
       
        
        public ReportTemplateProjectModel(Project project)
        {
            // Private properties
            Project = project;
            ProjectPersons = project.ProjectPeople.ToList();
            ProjectOrganizations = project.ProjectOrganizations.ToList();
            ProjectImages = project.ProjectImages.ToList();

            // Public properties
            ProjectID = Project.ProjectID;
            ProjectName = Project.ProjectName;
            ProjectUrl = Project.GetDetailUrlAbsolute();
            PrimaryContactOrganization = Project.GetPrimaryContactOrganization() != null ? new ReportTemplateOrganizationModel(Project.GetPrimaryContactOrganization()) : null;
            ProjectStage = Project.ProjectStage.ProjectStageDisplayName;
            NumberOfReportedPerformanceMeasures = Project.PerformanceMeasureActuals.Count;
            ProjectPrimaryContact = Project.GetPrimaryContact() != null ? new ReportTemplatePersonModel(Project.GetPrimaryContact()) : null;
            PlanningDesignStartYear = Project.PlannedDate.HasValue ? Project.PlannedDate.Value.Year.ToString() : string.Empty;
            ImplementationStartYear = Project.GetImplementationStartYear().HasValue ? Project.GetImplementationStartYear().ToString() : string.Empty;
            CompletionYear = Project.CompletionDate.HasValue ? Project.CompletionDate.Value.Year.ToString() : string.Empty;
            PrimaryTaxonomyLeaf = Project.GetCanStewardProjectsTaxonomyBranch()?.DisplayName;
            NumberOfReportedExpenditures = Project.ProjectGrantAllocationExpenditures.Count();
            FundingType = string.Join(", ", Project.ProjectFundingSources.Select(x => x.FundingSource.FundingSourceDisplayName));
            EstimatedTotalCost = Project.EstimatedTotalCost?.ToStringCurrency();
            //SecuredFunding = Project.GetSecuredFunding().ToStringCurrency();
            //TargetedFunding = Project.GetTargetedFunding().ToStringCurrency();
            //NoFundingSourceIdentified = Project.GetNoFundingSourceIdentifiedAmount()?.ToStringCurrency();
            TotalFunding = Project.GetTotalFunding()?.ToStringCurrency();
            ProjectDescription = Project.ProjectDescription;
            ProjectLastUpdated = Project.LastUpdateDate;

            //var projectStatus = project.GetCurrentProjectStatus();
            //if (projectStatus != null)
            //{
            //    CurrentProjectStatusColor = projectStatus.ProjectStatusColor;
            //    CurrentProjectStatus = projectStatus.ProjectStatusDisplayName;
            //}

            //var finalProjectStatus = Project.FinalStatusReportStatusDescription;
            //if (finalProjectStatus != null)
            //{
            //    FinalStatusUpdateStatus = finalProjectStatus;
            //}

        }

        public List<ReportTemplateProjectContactModel> GetProjectContacts()
        {
            return ProjectPersons.Select(x => new ReportTemplateProjectContactModel(x)).ToList();
        }

        public List<ReportTemplateProjectContactModel> GetProjectContactsByType(string contactTypeName)
        {
            var projectContactsInType = ProjectPersons.Where(x => x.ProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName == contactTypeName).ToList();
            return projectContactsInType.Select(x => new ReportTemplateProjectContactModel(x)).ToList();
        }

        public List<ReportTemplateProjectOrganizationModel> GetProjectOrganizations()
        {
            return ProjectOrganizations.Select(x => new ReportTemplateProjectOrganizationModel(x)).ToList();
        }

        public List<ReportTemplateProjectOrganizationModel> GetProjectOrganizationsByType(string organizationTypeName)
        {
            var organizationsInType = ProjectOrganizations.Where(x => x.RelationshipType.RelationshipTypeName == organizationTypeName).ToList();
            return organizationsInType.Select(x => new ReportTemplateProjectOrganizationModel(x)).ToList();
        }

        public string GetProjectContactNamesByType(string contactTypeName)
        {
            var projectContactsInType = ProjectPersons.Where(x => x.ProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName == contactTypeName).ToList();
            var projectContactNames = projectContactsInType.Select(x => x.Person.FullNameFirstLast).ToList();
            return $"{string.Join(", ", projectContactNames)}";
        }

        public string GetProjectOrganizationNamesByType(string organizationTypeName)
        {
            var organizationsInType = ProjectOrganizations.Where(x => x.RelationshipType.RelationshipTypeName == organizationTypeName).ToList();
            var organizationNames = organizationsInType.Select(x => x.Organization.DisplayName).ToList();
            return $"{string.Join(", ", organizationNames)}";
        }

        public List<ReportTemplateProjectImageModel> GetProjectImages()
        {
            return ProjectImages.Select(x => new ReportTemplateProjectImageModel(x)).ToList();
        }

        public List<ReportTemplateProjectImageModel> GetProjectImagesByTiming(string timingName)
        {
            return ProjectImages.Where(x => x.ProjectImageTiming.ProjectImageTimingName == timingName).Select(x => new ReportTemplateProjectImageModel(x)).ToList();
        }

        public ReportTemplateProjectImageModel GetProjectKeyPhoto()
        {
            var projectKeyPhoto = ProjectImages.FirstOrDefault(x => x.IsKeyPhoto == true);
            return projectKeyPhoto != null ? new ReportTemplateProjectImageModel(projectKeyPhoto) : null;
        }

        //public List<ReportTemplateProjectStatusModel> GetAllProjectStatusesFromTheLastWeek()
        //{
        //    var lastMonday = GetStartOfWeek(DateTime.Now, DayOfWeek.Monday).AddDays(-7);
        //    var allProjectStatuses = Project.ProjectProjectStatuses.ToList();
        //    var filteredProjectStatuses = allProjectStatuses.Where(x => x.ProjectProjectStatusUpdateDate >= lastMonday);
        //    return filteredProjectStatuses.OrderByDescending(x => x.ProjectProjectStatusUpdateDate).Select(x => new ReportTemplateProjectStatusModel(x)).ToList();
        //}

        public List<ReportTemplateProjectReportedPerformanceMeasureModel> GetProjectReportedPerformanceMeasures()
        {
            return Project.PerformanceMeasureActuals.Select(x => new ReportTemplateProjectReportedPerformanceMeasureModel(x))
                .OrderBy(x => x.PerformanceMeasureName)
                .ThenBy(x => x.PerformanceMeasureSubcategoryName)
                .ThenBy(x => x.PerformanceMeasureSubcategoryOptionName)
                .ThenByDescending(x => x.Year)
                .ToList();
        }

        public List<ReportTemplateProjectExpectedPerformanceMeasureModel> GetProjectExpectedPerformanceMeasures()
        {
            return Project.PerformanceMeasureExpecteds.Select(x => new ReportTemplateProjectExpectedPerformanceMeasureModel(x))
                .OrderBy(x => x.PerformanceMeasureName)
                .ThenBy(x => x.PerformanceMeasureSubcategoryName)
                .ThenBy(x => x.PerformanceMeasureSubcategoryOptionName)
                .ToList();
        }

        private DateTime GetStartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}