//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[Project] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Project]")]
    public partial class Project : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Project()
        {
            this.AgreementProjects = new HashSet<AgreementProject>();
            this.GrantAllocationAwardLandownerCostShareLineItems = new HashSet<GrantAllocationAwardLandownerCostShareLineItem>();
            this.InteractionEventProjects = new HashSet<InteractionEventProject>();
            this.InvoicePaymentRequests = new HashSet<InvoicePaymentRequest>();
            this.NotificationProjects = new HashSet<NotificationProject>();
            this.PerformanceMeasureActuals = new HashSet<PerformanceMeasureActual>();
            this.PerformanceMeasureExpecteds = new HashSet<PerformanceMeasureExpected>();
            this.ProgramNotificationSentProjects = new HashSet<ProgramNotificationSentProject>();
            this.ProjectClassifications = new HashSet<ProjectClassification>();
            this.ProjectCounties = new HashSet<ProjectCounty>();
            this.ProjectDocuments = new HashSet<ProjectDocument>();
            this.ProjectExemptReportingYears = new HashSet<ProjectExemptReportingYear>();
            this.ProjectExternalLinks = new HashSet<ProjectExternalLink>();
            this.ProjectFundingSources = new HashSet<ProjectFundingSource>();
            this.ProjectGrantAllocationExpenditures = new HashSet<ProjectGrantAllocationExpenditure>();
            this.ProjectGrantAllocationRequests = new HashSet<ProjectGrantAllocationRequest>();
            this.ProjectImages = new HashSet<ProjectImage>();
            this.ProjectImportBlockLists = new HashSet<ProjectImportBlockList>();
            this.ProjectInternalNotes = new HashSet<ProjectInternalNote>();
            this.ProjectLocations = new HashSet<ProjectLocation>();
            this.ProjectLocationStagings = new HashSet<ProjectLocationStaging>();
            this.ProjectNotes = new HashSet<ProjectNote>();
            this.ProjectOrganizations = new HashSet<ProjectOrganization>();
            this.ProjectPeople = new HashSet<ProjectPerson>();
            this.ProjectPriorityLandscapes = new HashSet<ProjectPriorityLandscape>();
            this.ProjectPrograms = new HashSet<ProjectProgram>();
            this.ProjectRegions = new HashSet<ProjectRegion>();
            this.ProjectTags = new HashSet<ProjectTag>();
            this.ProjectUpdateBatches = new HashSet<ProjectUpdateBatch>();
            this.Treatments = new HashSet<Treatment>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Project(int projectID, int projectTypeID, int projectStageID, string projectName, string projectDescription, DateTime? completionDate, decimal? estimatedTotalCost, DbGeometry projectLocationPoint, string performanceMeasureActualYearsExemptionExplanation, bool isFeatured, string projectLocationNotes, DateTime? plannedDate, int projectLocationSimpleTypeID, int projectApprovalStatusID, int? proposingPersonID, DateTime? proposingDate, string performanceMeasureNotes, DateTime? submissionDate, DateTime? approvalDate, int? reviewedByPersonID, DbGeometry defaultBoundingBox, string noExpendituresToReportExplanation, int? focusAreaID, string noRegionsExplanation, DateTime? expirationDate, string fhtProjectNumber, string noPriorityLandscapesExplanation, int? createGisUploadAttemptID, int? lastUpdateGisUploadAttemptID, string projectGisIdentifier, string projectFundingSourceNotes, string noCountiesExplanation, int? percentageMatch) : this()
        {
            this.ProjectID = projectID;
            this.ProjectTypeID = projectTypeID;
            this.ProjectStageID = projectStageID;
            this.ProjectName = projectName;
            this.ProjectDescription = projectDescription;
            this.CompletionDate = completionDate;
            this.EstimatedTotalCost = estimatedTotalCost;
            this.ProjectLocationPoint = projectLocationPoint;
            this.PerformanceMeasureActualYearsExemptionExplanation = performanceMeasureActualYearsExemptionExplanation;
            this.IsFeatured = isFeatured;
            this.ProjectLocationNotes = projectLocationNotes;
            this.PlannedDate = plannedDate;
            this.ProjectLocationSimpleTypeID = projectLocationSimpleTypeID;
            this.ProjectApprovalStatusID = projectApprovalStatusID;
            this.ProposingPersonID = proposingPersonID;
            this.ProposingDate = proposingDate;
            this.PerformanceMeasureNotes = performanceMeasureNotes;
            this.SubmissionDate = submissionDate;
            this.ApprovalDate = approvalDate;
            this.ReviewedByPersonID = reviewedByPersonID;
            this.DefaultBoundingBox = defaultBoundingBox;
            this.NoExpendituresToReportExplanation = noExpendituresToReportExplanation;
            this.FocusAreaID = focusAreaID;
            this.NoRegionsExplanation = noRegionsExplanation;
            this.ExpirationDate = expirationDate;
            this.FhtProjectNumber = fhtProjectNumber;
            this.NoPriorityLandscapesExplanation = noPriorityLandscapesExplanation;
            this.CreateGisUploadAttemptID = createGisUploadAttemptID;
            this.LastUpdateGisUploadAttemptID = lastUpdateGisUploadAttemptID;
            this.ProjectGisIdentifier = projectGisIdentifier;
            this.ProjectFundingSourceNotes = projectFundingSourceNotes;
            this.NoCountiesExplanation = noCountiesExplanation;
            this.PercentageMatch = percentageMatch;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Project(int projectTypeID, int projectStageID, string projectName, bool isFeatured, int projectLocationSimpleTypeID, int projectApprovalStatusID, string fhtProjectNumber) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectTypeID = projectTypeID;
            this.ProjectStageID = projectStageID;
            this.ProjectName = projectName;
            this.IsFeatured = isFeatured;
            this.ProjectLocationSimpleTypeID = projectLocationSimpleTypeID;
            this.ProjectApprovalStatusID = projectApprovalStatusID;
            this.FhtProjectNumber = fhtProjectNumber;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Project(ProjectType projectType, ProjectStage projectStage, string projectName, bool isFeatured, ProjectLocationSimpleType projectLocationSimpleType, ProjectApprovalStatus projectApprovalStatus, string fhtProjectNumber) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectTypeID = projectType.ProjectTypeID;
            this.ProjectType = projectType;
            projectType.Projects.Add(this);
            this.ProjectStageID = projectStage.ProjectStageID;
            this.ProjectName = projectName;
            this.IsFeatured = isFeatured;
            this.ProjectLocationSimpleTypeID = projectLocationSimpleType.ProjectLocationSimpleTypeID;
            this.ProjectApprovalStatusID = projectApprovalStatus.ProjectApprovalStatusID;
            this.FhtProjectNumber = fhtProjectNumber;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Project CreateNewBlank(ProjectType projectType, ProjectStage projectStage, ProjectLocationSimpleType projectLocationSimpleType, ProjectApprovalStatus projectApprovalStatus)
        {
            return new Project(projectType, projectStage, default(string), default(bool), projectLocationSimpleType, projectApprovalStatus, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return AgreementProjects.Any() || GrantAllocationAwardLandownerCostShareLineItems.Any() || InteractionEventProjects.Any() || InvoicePaymentRequests.Any() || NotificationProjects.Any() || PerformanceMeasureActuals.Any() || PerformanceMeasureExpecteds.Any() || ProgramNotificationSentProjects.Any() || ProjectClassifications.Any() || ProjectCounties.Any() || ProjectDocuments.Any() || ProjectExemptReportingYears.Any() || ProjectExternalLinks.Any() || ProjectFundingSources.Any() || ProjectGrantAllocationExpenditures.Any() || ProjectGrantAllocationRequests.Any() || ProjectImages.Any() || ProjectImportBlockLists.Any() || ProjectInternalNotes.Any() || ProjectLocations.Any() || ProjectLocationStagings.Any() || ProjectNotes.Any() || ProjectOrganizations.Any() || ProjectPeople.Any() || ProjectPriorityLandscapes.Any() || ProjectPrograms.Any() || ProjectRegions.Any() || ProjectTags.Any() || ProjectUpdateBatches.Any() || Treatments.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(AgreementProjects.Any())
            {
                dependentObjects.Add(typeof(AgreementProject).Name);
            }

            if(GrantAllocationAwardLandownerCostShareLineItems.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationAwardLandownerCostShareLineItem).Name);
            }

            if(InteractionEventProjects.Any())
            {
                dependentObjects.Add(typeof(InteractionEventProject).Name);
            }

            if(InvoicePaymentRequests.Any())
            {
                dependentObjects.Add(typeof(InvoicePaymentRequest).Name);
            }

            if(NotificationProjects.Any())
            {
                dependentObjects.Add(typeof(NotificationProject).Name);
            }

            if(PerformanceMeasureActuals.Any())
            {
                dependentObjects.Add(typeof(PerformanceMeasureActual).Name);
            }

            if(PerformanceMeasureExpecteds.Any())
            {
                dependentObjects.Add(typeof(PerformanceMeasureExpected).Name);
            }

            if(ProgramNotificationSentProjects.Any())
            {
                dependentObjects.Add(typeof(ProgramNotificationSentProject).Name);
            }

            if(ProjectClassifications.Any())
            {
                dependentObjects.Add(typeof(ProjectClassification).Name);
            }

            if(ProjectCounties.Any())
            {
                dependentObjects.Add(typeof(ProjectCounty).Name);
            }

            if(ProjectDocuments.Any())
            {
                dependentObjects.Add(typeof(ProjectDocument).Name);
            }

            if(ProjectExemptReportingYears.Any())
            {
                dependentObjects.Add(typeof(ProjectExemptReportingYear).Name);
            }

            if(ProjectExternalLinks.Any())
            {
                dependentObjects.Add(typeof(ProjectExternalLink).Name);
            }

            if(ProjectFundingSources.Any())
            {
                dependentObjects.Add(typeof(ProjectFundingSource).Name);
            }

            if(ProjectGrantAllocationExpenditures.Any())
            {
                dependentObjects.Add(typeof(ProjectGrantAllocationExpenditure).Name);
            }

            if(ProjectGrantAllocationRequests.Any())
            {
                dependentObjects.Add(typeof(ProjectGrantAllocationRequest).Name);
            }

            if(ProjectImages.Any())
            {
                dependentObjects.Add(typeof(ProjectImage).Name);
            }

            if(ProjectImportBlockLists.Any())
            {
                dependentObjects.Add(typeof(ProjectImportBlockList).Name);
            }

            if(ProjectInternalNotes.Any())
            {
                dependentObjects.Add(typeof(ProjectInternalNote).Name);
            }

            if(ProjectLocations.Any())
            {
                dependentObjects.Add(typeof(ProjectLocation).Name);
            }

            if(ProjectLocationStagings.Any())
            {
                dependentObjects.Add(typeof(ProjectLocationStaging).Name);
            }

            if(ProjectNotes.Any())
            {
                dependentObjects.Add(typeof(ProjectNote).Name);
            }

            if(ProjectOrganizations.Any())
            {
                dependentObjects.Add(typeof(ProjectOrganization).Name);
            }

            if(ProjectPeople.Any())
            {
                dependentObjects.Add(typeof(ProjectPerson).Name);
            }

            if(ProjectPriorityLandscapes.Any())
            {
                dependentObjects.Add(typeof(ProjectPriorityLandscape).Name);
            }

            if(ProjectPrograms.Any())
            {
                dependentObjects.Add(typeof(ProjectProgram).Name);
            }

            if(ProjectRegions.Any())
            {
                dependentObjects.Add(typeof(ProjectRegion).Name);
            }

            if(ProjectTags.Any())
            {
                dependentObjects.Add(typeof(ProjectTag).Name);
            }

            if(ProjectUpdateBatches.Any())
            {
                dependentObjects.Add(typeof(ProjectUpdateBatch).Name);
            }

            if(Treatments.Any())
            {
                dependentObjects.Add(typeof(Treatment).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Project).Name, typeof(AgreementProject).Name, typeof(GrantAllocationAwardLandownerCostShareLineItem).Name, typeof(InteractionEventProject).Name, typeof(InvoicePaymentRequest).Name, typeof(NotificationProject).Name, typeof(PerformanceMeasureActual).Name, typeof(PerformanceMeasureExpected).Name, typeof(ProgramNotificationSentProject).Name, typeof(ProjectClassification).Name, typeof(ProjectCounty).Name, typeof(ProjectDocument).Name, typeof(ProjectExemptReportingYear).Name, typeof(ProjectExternalLink).Name, typeof(ProjectFundingSource).Name, typeof(ProjectGrantAllocationExpenditure).Name, typeof(ProjectGrantAllocationRequest).Name, typeof(ProjectImage).Name, typeof(ProjectImportBlockList).Name, typeof(ProjectInternalNote).Name, typeof(ProjectLocation).Name, typeof(ProjectLocationStaging).Name, typeof(ProjectNote).Name, typeof(ProjectOrganization).Name, typeof(ProjectPerson).Name, typeof(ProjectPriorityLandscape).Name, typeof(ProjectProgram).Name, typeof(ProjectRegion).Name, typeof(ProjectTag).Name, typeof(ProjectUpdateBatch).Name, typeof(Treatment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Projects.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in AgreementProjects.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationAwardLandownerCostShareLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in InteractionEventProjects.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in InvoicePaymentRequests.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in NotificationProjects.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PerformanceMeasureActuals.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PerformanceMeasureExpecteds.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProgramNotificationSentProjects.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectClassifications.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectCounties.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectDocuments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectExemptReportingYears.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectExternalLinks.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectFundingSources.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectGrantAllocationExpenditures.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectGrantAllocationRequests.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectImages.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectImportBlockLists.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectInternalNotes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectLocations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectLocationStagings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNotes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectOrganizations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectPeople.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectPriorityLandscapes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectPrograms.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectRegions.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectTags.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectUpdateBatches.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in Treatments.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectID { get; set; }
        public int ProjectTypeID { get; set; }
        public int ProjectStageID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal? EstimatedTotalCost { get; set; }
        public DbGeometry ProjectLocationPoint { get; set; }
        public string PerformanceMeasureActualYearsExemptionExplanation { get; set; }
        public bool IsFeatured { get; set; }
        public string ProjectLocationNotes { get; set; }
        public DateTime? PlannedDate { get; set; }
        public int ProjectLocationSimpleTypeID { get; set; }
        public int ProjectApprovalStatusID { get; set; }
        public int? ProposingPersonID { get; set; }
        public DateTime? ProposingDate { get; set; }
        public string PerformanceMeasureNotes { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ReviewedByPersonID { get; set; }
        public DbGeometry DefaultBoundingBox { get; set; }
        public string NoExpendituresToReportExplanation { get; set; }
        public int? FocusAreaID { get; set; }
        public string NoRegionsExplanation { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string FhtProjectNumber { get; set; }
        public string NoPriorityLandscapesExplanation { get; set; }
        public int? CreateGisUploadAttemptID { get; set; }
        public int? LastUpdateGisUploadAttemptID { get; set; }
        public string ProjectGisIdentifier { get; set; }
        public string ProjectFundingSourceNotes { get; set; }
        public string NoCountiesExplanation { get; set; }
        public int? PercentageMatch { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectID; } set { ProjectID = value; } }

        public virtual ICollection<AgreementProject> AgreementProjects { get; set; }
        public virtual ICollection<GrantAllocationAwardLandownerCostShareLineItem> GrantAllocationAwardLandownerCostShareLineItems { get; set; }
        public virtual ICollection<InteractionEventProject> InteractionEventProjects { get; set; }
        public virtual ICollection<InvoicePaymentRequest> InvoicePaymentRequests { get; set; }
        public virtual ICollection<NotificationProject> NotificationProjects { get; set; }
        public virtual ICollection<PerformanceMeasureActual> PerformanceMeasureActuals { get; set; }
        public virtual ICollection<PerformanceMeasureExpected> PerformanceMeasureExpecteds { get; set; }
        public virtual ICollection<ProgramNotificationSentProject> ProgramNotificationSentProjects { get; set; }
        public virtual ICollection<ProjectClassification> ProjectClassifications { get; set; }
        public virtual ICollection<ProjectCounty> ProjectCounties { get; set; }
        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        public virtual ICollection<ProjectExemptReportingYear> ProjectExemptReportingYears { get; set; }
        public virtual ICollection<ProjectExternalLink> ProjectExternalLinks { get; set; }
        public virtual ICollection<ProjectFundingSource> ProjectFundingSources { get; set; }
        public virtual ICollection<ProjectGrantAllocationExpenditure> ProjectGrantAllocationExpenditures { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequest> ProjectGrantAllocationRequests { get; set; }
        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
        public virtual ICollection<ProjectImportBlockList> ProjectImportBlockLists { get; set; }
        public virtual ICollection<ProjectInternalNote> ProjectInternalNotes { get; set; }
        public virtual ICollection<ProjectLocation> ProjectLocations { get; set; }
        public virtual ICollection<ProjectLocationStaging> ProjectLocationStagings { get; set; }
        public virtual ICollection<ProjectNote> ProjectNotes { get; set; }
        public virtual ICollection<ProjectOrganization> ProjectOrganizations { get; set; }
        public virtual ICollection<ProjectPerson> ProjectPeople { get; set; }
        public virtual ICollection<ProjectPriorityLandscape> ProjectPriorityLandscapes { get; set; }
        public virtual ICollection<ProjectProgram> ProjectPrograms { get; set; }
        public virtual ICollection<ProjectRegion> ProjectRegions { get; set; }
        public virtual ICollection<ProjectTag> ProjectTags { get; set; }
        public virtual ICollection<ProjectUpdateBatch> ProjectUpdateBatches { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
        public virtual ProjectType ProjectType { get; set; }
        public ProjectStage ProjectStage { get { return ProjectStage.AllLookupDictionary[ProjectStageID]; } }
        public ProjectLocationSimpleType ProjectLocationSimpleType { get { return ProjectLocationSimpleType.AllLookupDictionary[ProjectLocationSimpleTypeID]; } }
        public ProjectApprovalStatus ProjectApprovalStatus { get { return ProjectApprovalStatus.AllLookupDictionary[ProjectApprovalStatusID]; } }
        public virtual Person ProposingPerson { get; set; }
        public virtual Person ReviewedByPerson { get; set; }
        public virtual FocusArea FocusArea { get; set; }
        public virtual GisUploadAttempt CreateGisUploadAttempt { get; set; }
        public virtual GisUploadAttempt LastUpdateGisUploadAttempt { get; set; }

        public static class FieldLengths
        {
            public const int ProjectName = 140;
            public const int ProjectDescription = 4000;
            public const int PerformanceMeasureActualYearsExemptionExplanation = 4000;
            public const int ProjectLocationNotes = 4000;
            public const int PerformanceMeasureNotes = 500;
            public const int NoRegionsExplanation = 4000;
            public const int FhtProjectNumber = 20;
            public const int NoPriorityLandscapesExplanation = 4000;
            public const int ProjectGisIdentifier = 140;
            public const int ProjectFundingSourceNotes = 4000;
            public const int NoCountiesExplanation = 4000;
        }
    }
}