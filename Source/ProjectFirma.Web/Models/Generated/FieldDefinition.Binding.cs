//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinition]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public abstract partial class FieldDefinition : IHavePrimaryKey
    {
        public static readonly FieldDefinitionProjectType ProjectType = FieldDefinitionProjectType.Instance;
        public static readonly FieldDefinitionExpectedValue ExpectedValue = FieldDefinitionExpectedValue.Instance;
        public static readonly FieldDefinitionTaxonomyTrunk TaxonomyTrunk = FieldDefinitionTaxonomyTrunk.Instance;
        public static readonly FieldDefinitionPrimaryContactOrganization PrimaryContactOrganization = FieldDefinitionPrimaryContactOrganization.Instance;
        public static readonly FieldDefinitionProjectsStewardOrganizationRelationshipToProject ProjectsStewardOrganizationRelationshipToProject = FieldDefinitionProjectsStewardOrganizationRelationshipToProject.Instance;
        public static readonly FieldDefinitionOrganization Organization = FieldDefinitionOrganization.Instance;
        public static readonly FieldDefinitionPassword Password = FieldDefinitionPassword.Instance;
        public static readonly FieldDefinitionPerformanceMeasure PerformanceMeasure = FieldDefinitionPerformanceMeasure.Instance;
        public static readonly FieldDefinitionPerformanceMeasureType PerformanceMeasureType = FieldDefinitionPerformanceMeasureType.Instance;
        public static readonly FieldDefinitionMeasurementUnit MeasurementUnit = FieldDefinitionMeasurementUnit.Instance;
        public static readonly FieldDefinitionPhotoCaption PhotoCaption = FieldDefinitionPhotoCaption.Instance;
        public static readonly FieldDefinitionPhotoCredit PhotoCredit = FieldDefinitionPhotoCredit.Instance;
        public static readonly FieldDefinitionPhotoTiming PhotoTiming = FieldDefinitionPhotoTiming.Instance;
        public static readonly FieldDefinitionOrganizationPrimaryContact OrganizationPrimaryContact = FieldDefinitionOrganizationPrimaryContact.Instance;
        public static readonly FieldDefinitionTaxonomyBranch TaxonomyBranch = FieldDefinitionTaxonomyBranch.Instance;
        public static readonly FieldDefinitionCompletionDate CompletionDate = FieldDefinitionCompletionDate.Instance;
        public static readonly FieldDefinitionProjectDescription ProjectDescription = FieldDefinitionProjectDescription.Instance;
        public static readonly FieldDefinitionProjectName ProjectName = FieldDefinitionProjectName.Instance;
        public static readonly FieldDefinitionProjectNote ProjectNote = FieldDefinitionProjectNote.Instance;
        public static readonly FieldDefinitionExpirationDate ExpirationDate = FieldDefinitionExpirationDate.Instance;
        public static readonly FieldDefinitionReportedValue ReportedValue = FieldDefinitionReportedValue.Instance;
        public static readonly FieldDefinitionOrganizationType OrganizationType = FieldDefinitionOrganizationType.Instance;
        public static readonly FieldDefinitionProjectGrantAllocationRequestTotalAmount ProjectGrantAllocationRequestTotalAmount = FieldDefinitionProjectGrantAllocationRequestTotalAmount.Instance;
        public static readonly FieldDefinitionProjectStage ProjectStage = FieldDefinitionProjectStage.Instance;
        public static readonly FieldDefinitionClassificationName ClassificationName = FieldDefinitionClassificationName.Instance;
        public static readonly FieldDefinitionEstimatedTotalCost EstimatedTotalCost = FieldDefinitionEstimatedTotalCost.Instance;
        public static readonly FieldDefinitionUnfundedNeed UnfundedNeed = FieldDefinitionUnfundedNeed.Instance;
        public static readonly FieldDefinitionUsername Username = FieldDefinitionUsername.Instance;
        public static readonly FieldDefinitionProject Project = FieldDefinitionProject.Instance;
        public static readonly FieldDefinitionClassification Classification = FieldDefinitionClassification.Instance;
        public static readonly FieldDefinitionPerformanceMeasureSubcategory PerformanceMeasureSubcategory = FieldDefinitionPerformanceMeasureSubcategory.Instance;
        public static readonly FieldDefinitionPerformanceMeasureSubcategoryOption PerformanceMeasureSubcategoryOption = FieldDefinitionPerformanceMeasureSubcategoryOption.Instance;
        public static readonly FieldDefinitionIsPrimaryTaxonomyBranch IsPrimaryTaxonomyBranch = FieldDefinitionIsPrimaryTaxonomyBranch.Instance;
        public static readonly FieldDefinitionFundedAmount FundedAmount = FieldDefinitionFundedAmount.Instance;
        public static readonly FieldDefinitionProjectLocation ProjectLocation = FieldDefinitionProjectLocation.Instance;
        public static readonly FieldDefinitionExcludeFromFactSheet ExcludeFromFactSheet = FieldDefinitionExcludeFromFactSheet.Instance;
        public static readonly FieldDefinitionProjectCostInYearOfExpenditure ProjectCostInYearOfExpenditure = FieldDefinitionProjectCostInYearOfExpenditure.Instance;
        public static readonly FieldDefinitionGlobalInflationRate GlobalInflationRate = FieldDefinitionGlobalInflationRate.Instance;
        public static readonly FieldDefinitionReportingYear ReportingYear = FieldDefinitionReportingYear.Instance;
        public static readonly FieldDefinitionTagName TagName = FieldDefinitionTagName.Instance;
        public static readonly FieldDefinitionTagDescription TagDescription = FieldDefinitionTagDescription.Instance;
        public static readonly FieldDefinitionReportedExpenditure ReportedExpenditure = FieldDefinitionReportedExpenditure.Instance;
        public static readonly FieldDefinitionApplication Application = FieldDefinitionApplication.Instance;
        public static readonly FieldDefinitionSpendingAssociatedWithPM SpendingAssociatedWithPM = FieldDefinitionSpendingAssociatedWithPM.Instance;
        public static readonly FieldDefinitionStartApprovalDate StartApprovalDate = FieldDefinitionStartApprovalDate.Instance;
        public static readonly FieldDefinitionAssociatedTaxonomyBranches AssociatedTaxonomyBranches = FieldDefinitionAssociatedTaxonomyBranches.Instance;
        public static readonly FieldDefinitionExternalLinks ExternalLinks = FieldDefinitionExternalLinks.Instance;
        public static readonly FieldDefinitionCurrentYearForPVCalculations CurrentYearForPVCalculations = FieldDefinitionCurrentYearForPVCalculations.Instance;
        public static readonly FieldDefinitionLifecycleOperatingCost LifecycleOperatingCost = FieldDefinitionLifecycleOperatingCost.Instance;
        public static readonly FieldDefinitionPerformanceMeasureChartTitle PerformanceMeasureChartTitle = FieldDefinitionPerformanceMeasureChartTitle.Instance;
        public static readonly FieldDefinitionRoleName RoleName = FieldDefinitionRoleName.Instance;
        public static readonly FieldDefinitionRegion Region = FieldDefinitionRegion.Instance;
        public static readonly FieldDefinitionPerformanceMeasureChartCaption PerformanceMeasureChartCaption = FieldDefinitionPerformanceMeasureChartCaption.Instance;
        public static readonly FieldDefinitionMonitoringProgram MonitoringProgram = FieldDefinitionMonitoringProgram.Instance;
        public static readonly FieldDefinitionMonitoringApproach MonitoringApproach = FieldDefinitionMonitoringApproach.Instance;
        public static readonly FieldDefinitionMonitoringProgramPartner MonitoringProgramPartner = FieldDefinitionMonitoringProgramPartner.Instance;
        public static readonly FieldDefinitionMonitoringProgramUrl MonitoringProgramUrl = FieldDefinitionMonitoringProgramUrl.Instance;
        public static readonly FieldDefinitionClassificationDescription ClassificationDescription = FieldDefinitionClassificationDescription.Instance;
        public static readonly FieldDefinitionClassificationGoalStatement ClassificationGoalStatement = FieldDefinitionClassificationGoalStatement.Instance;
        public static readonly FieldDefinitionClassificationNarrative ClassificationNarrative = FieldDefinitionClassificationNarrative.Instance;
        public static readonly FieldDefinitionTaxonomySystemName TaxonomySystemName = FieldDefinitionTaxonomySystemName.Instance;
        public static readonly FieldDefinitionProjectTypeDisplayNameForProject ProjectTypeDisplayNameForProject = FieldDefinitionProjectTypeDisplayNameForProject.Instance;
        public static readonly FieldDefinitionProjectRelationshipType ProjectRelationshipType = FieldDefinitionProjectRelationshipType.Instance;
        public static readonly FieldDefinitionProjectSteward ProjectSteward = FieldDefinitionProjectSteward.Instance;
        public static readonly FieldDefinitionChartLastUpdatedDate ChartLastUpdatedDate = FieldDefinitionChartLastUpdatedDate.Instance;
        public static readonly FieldDefinitionUnsecuredFunding UnsecuredFunding = FieldDefinitionUnsecuredFunding.Instance;
        public static readonly FieldDefinitionProjectStewardOrganizationDisplayName ProjectStewardOrganizationDisplayName = FieldDefinitionProjectStewardOrganizationDisplayName.Instance;
        public static readonly FieldDefinitionClassificationSystem ClassificationSystem = FieldDefinitionClassificationSystem.Instance;
        public static readonly FieldDefinitionClassificationSystemName ClassificationSystemName = FieldDefinitionClassificationSystemName.Instance;
        public static readonly FieldDefinitionProjectPrimaryContact ProjectPrimaryContact = FieldDefinitionProjectPrimaryContact.Instance;
        public static readonly FieldDefinitionCustomPageDisplayType CustomPageDisplayType = FieldDefinitionCustomPageDisplayType.Instance;
        public static readonly FieldDefinitionTaxonomyTrunkDescription TaxonomyTrunkDescription = FieldDefinitionTaxonomyTrunkDescription.Instance;
        public static readonly FieldDefinitionTaxonomyBranchDescription TaxonomyBranchDescription = FieldDefinitionTaxonomyBranchDescription.Instance;
        public static readonly FieldDefinitionProjectTypeDescription ProjectTypeDescription = FieldDefinitionProjectTypeDescription.Instance;
        public static readonly FieldDefinitionShowApplicationsToThePublic ShowApplicationsToThePublic = FieldDefinitionShowApplicationsToThePublic.Instance;
        public static readonly FieldDefinitionShowLeadImplementerLogoOnFactSheet ShowLeadImplementerLogoOnFactSheet = FieldDefinitionShowLeadImplementerLogoOnFactSheet.Instance;
        public static readonly FieldDefinitionProjectCustomAttribute ProjectCustomAttribute = FieldDefinitionProjectCustomAttribute.Instance;
        public static readonly FieldDefinitionProjectCustomAttributeDataType ProjectCustomAttributeDataType = FieldDefinitionProjectCustomAttributeDataType.Instance;
        public static readonly FieldDefinitionProjectUpdateKickOffDate ProjectUpdateKickOffDate = FieldDefinitionProjectUpdateKickOffDate.Instance;
        public static readonly FieldDefinitionProjectUpdateReminderInterval ProjectUpdateReminderInterval = FieldDefinitionProjectUpdateReminderInterval.Instance;
        public static readonly FieldDefinitionProjectUpdateCloseOutDate ProjectUpdateCloseOutDate = FieldDefinitionProjectUpdateCloseOutDate.Instance;
        public static readonly FieldDefinitionPerformanceMeasureIsAggregatable PerformanceMeasureIsAggregatable = FieldDefinitionPerformanceMeasureIsAggregatable.Instance;
        public static readonly FieldDefinitionGrantAllocationAmount GrantAllocationAmount = FieldDefinitionGrantAllocationAmount.Instance;
        public static readonly FieldDefinitionNormalUser NormalUser = FieldDefinitionNormalUser.Instance;
        public static readonly FieldDefinitionProjectStewardshipArea ProjectStewardshipArea = FieldDefinitionProjectStewardshipArea.Instance;
        public static readonly FieldDefinitionProjectInternalNote ProjectInternalNote = FieldDefinitionProjectInternalNote.Instance;
        public static readonly FieldDefinitionStatewideVendorNumber StatewideVendorNumber = FieldDefinitionStatewideVendorNumber.Instance;
        public static readonly FieldDefinitionContact Contact = FieldDefinitionContact.Instance;
        public static readonly FieldDefinitionContactRelationshipType ContactRelationshipType = FieldDefinitionContactRelationshipType.Instance;
        public static readonly FieldDefinitionContractor Contractor = FieldDefinitionContractor.Instance;
        public static readonly FieldDefinitionLandowner Landowner = FieldDefinitionLandowner.Instance;
        public static readonly FieldDefinitionPartner Partner = FieldDefinitionPartner.Instance;
        public static readonly FieldDefinitionPrimaryContact PrimaryContact = FieldDefinitionPrimaryContact.Instance;
        public static readonly FieldDefinitionFocusArea FocusArea = FieldDefinitionFocusArea.Instance;
        public static readonly FieldDefinitionGrant Grant = FieldDefinitionGrant.Instance;
        public static readonly FieldDefinitionGrantAllocation GrantAllocation = FieldDefinitionGrantAllocation.Instance;
        public static readonly FieldDefinitionCostType CostType = FieldDefinitionCostType.Instance;
        public static readonly FieldDefinitionProjectCode ProjectCode = FieldDefinitionProjectCode.Instance;
        public static readonly FieldDefinitionGrantAllocationProjectCode GrantAllocationProjectCode = FieldDefinitionGrantAllocationProjectCode.Instance;
        public static readonly FieldDefinitionProgramIndex ProgramIndex = FieldDefinitionProgramIndex.Instance;
        public static readonly FieldDefinitionGrantName GrantName = FieldDefinitionGrantName.Instance;
        public static readonly FieldDefinitionGrantShortName GrantShortName = FieldDefinitionGrantShortName.Instance;
        public static readonly FieldDefinitionGrantStatus GrantStatus = FieldDefinitionGrantStatus.Instance;
        public static readonly FieldDefinitionGrantType GrantType = FieldDefinitionGrantType.Instance;
        public static readonly FieldDefinitionGrantNumber GrantNumber = FieldDefinitionGrantNumber.Instance;
        public static readonly FieldDefinitionCFDA CFDA = FieldDefinitionCFDA.Instance;
        public static readonly FieldDefinitionTotalAwardAmount TotalAwardAmount = FieldDefinitionTotalAwardAmount.Instance;
        public static readonly FieldDefinitionGrantStartDate GrantStartDate = FieldDefinitionGrantStartDate.Instance;
        public static readonly FieldDefinitionGrantEndDate GrantEndDate = FieldDefinitionGrantEndDate.Instance;
        public static readonly FieldDefinitionGrantNote GrantNote = FieldDefinitionGrantNote.Instance;
        public static readonly FieldDefinitionPriorityArea PriorityArea = FieldDefinitionPriorityArea.Instance;
        public static readonly FieldDefinitionInvoice Invoice = FieldDefinitionInvoice.Instance;
        public static readonly FieldDefinitionAgreement Agreement = FieldDefinitionAgreement.Instance;
        public static readonly FieldDefinitionFederalFundCode FederalFundCode = FieldDefinitionFederalFundCode.Instance;
        public static readonly FieldDefinitionAllocationAmount AllocationAmount = FieldDefinitionAllocationAmount.Instance;
        public static readonly FieldDefinitionAgreementType AgreementType = FieldDefinitionAgreementType.Instance;
        public static readonly FieldDefinitionAgreementNumber AgreementNumber = FieldDefinitionAgreementNumber.Instance;
        public static readonly FieldDefinitionAgreementTitle AgreementTitle = FieldDefinitionAgreementTitle.Instance;
        public static readonly FieldDefinitionAgreementStartDate AgreementStartDate = FieldDefinitionAgreementStartDate.Instance;
        public static readonly FieldDefinitionAgreementEndDate AgreementEndDate = FieldDefinitionAgreementEndDate.Instance;
        public static readonly FieldDefinitionAgreementAmount AgreementAmount = FieldDefinitionAgreementAmount.Instance;
        public static readonly FieldDefinitionProgramManager ProgramManager = FieldDefinitionProgramManager.Instance;
        public static readonly FieldDefinitionAgreementNotes AgreementNotes = FieldDefinitionAgreementNotes.Instance;
        public static readonly FieldDefinitionAgreementStatus AgreementStatus = FieldDefinitionAgreementStatus.Instance;
        public static readonly FieldDefinitionGrantAllocationNote GrantAllocationNote = FieldDefinitionGrantAllocationNote.Instance;
        public static readonly FieldDefinitionFileResource FileResource = FieldDefinitionFileResource.Instance;
        public static readonly FieldDefinitionProjectTotalCompletedFootprintAcres ProjectTotalCompletedFootprintAcres = FieldDefinitionProjectTotalCompletedFootprintAcres.Instance;
        public static readonly FieldDefinitionFocusAreaTotalProjectReportedExpendiures FocusAreaTotalProjectReportedExpendiures = FieldDefinitionFocusAreaTotalProjectReportedExpendiures.Instance;
        public static readonly FieldDefinitionFocusAreaTotalProjectEstimatedTotalCosts FocusAreaTotalProjectEstimatedTotalCosts = FieldDefinitionFocusAreaTotalProjectEstimatedTotalCosts.Instance;
        public static readonly FieldDefinitionFocusAreaTotalCompletedFootprintAcres FocusAreaTotalCompletedFootprintAcres = FieldDefinitionFocusAreaTotalCompletedFootprintAcres.Instance;
        public static readonly FieldDefinitionFocusAreaTotalPlannedFootprintAcres FocusAreaTotalPlannedFootprintAcres = FieldDefinitionFocusAreaTotalPlannedFootprintAcres.Instance;
        public static readonly FieldDefinitionFocusAreaCloseoutReportProjectList FocusAreaCloseoutReportProjectList = FieldDefinitionFocusAreaCloseoutReportProjectList.Instance;
        public static readonly FieldDefinitionRequestorName RequestorName = FieldDefinitionRequestorName.Instance;
        public static readonly FieldDefinitionInvoiceDate InvoiceDate = FieldDefinitionInvoiceDate.Instance;
        public static readonly FieldDefinitionPurchaseAuthority PurchaseAuthority = FieldDefinitionPurchaseAuthority.Instance;
        public static readonly FieldDefinitionTotalRequestedInvoicePaymentAmount TotalRequestedInvoicePaymentAmount = FieldDefinitionTotalRequestedInvoicePaymentAmount.Instance;
        public static readonly FieldDefinitionPreparedByPerson PreparedByPerson = FieldDefinitionPreparedByPerson.Instance;
        public static readonly FieldDefinitionInvoiceIdentifyingName InvoiceIdentifyingName = FieldDefinitionInvoiceIdentifyingName.Instance;
        public static readonly FieldDefinitionGrantNoteInternal GrantNoteInternal = FieldDefinitionGrantNoteInternal.Instance;
        public static readonly FieldDefinitionGrantAllocationNoteInternal GrantAllocationNoteInternal = FieldDefinitionGrantAllocationNoteInternal.Instance;
        public static readonly FieldDefinitionInvoiceStatus InvoiceStatus = FieldDefinitionInvoiceStatus.Instance;
        public static readonly FieldDefinitionInvoiceApprovalStatus InvoiceApprovalStatus = FieldDefinitionInvoiceApprovalStatus.Instance;
        public static readonly FieldDefinitionInvoiceApprovalComment InvoiceApprovalComment = FieldDefinitionInvoiceApprovalComment.Instance;
        public static readonly FieldDefinitionMatchAmount MatchAmount = FieldDefinitionMatchAmount.Instance;
        public static readonly FieldDefinitionVendor Vendor = FieldDefinitionVendor.Instance;
        public static readonly FieldDefinitionEstimatedIndirectCost EstimatedIndirectCost = FieldDefinitionEstimatedIndirectCost.Instance;
        public static readonly FieldDefinitionEstimatedPersonnelAndBenefitsCost EstimatedPersonnelAndBenefitsCost = FieldDefinitionEstimatedPersonnelAndBenefitsCost.Instance;
        public static readonly FieldDefinitionEstimatedSuppliesCost EstimatedSuppliesCost = FieldDefinitionEstimatedSuppliesCost.Instance;
        public static readonly FieldDefinitionEstimatedTravelCost EstimatedTravelCost = FieldDefinitionEstimatedTravelCost.Instance;
        public static readonly FieldDefinitionInvoiceID InvoiceID = FieldDefinitionInvoiceID.Instance;
        public static readonly FieldDefinitionInvoiceLineItem InvoiceLineItem = FieldDefinitionInvoiceLineItem.Instance;
        public static readonly FieldDefinitionInteractionEvent InteractionEvent = FieldDefinitionInteractionEvent.Instance;
        public static readonly FieldDefinitionInteractionEventType InteractionEventType = FieldDefinitionInteractionEventType.Instance;
        public static readonly FieldDefinitionDNRStaffPerson DNRStaffPerson = FieldDefinitionDNRStaffPerson.Instance;
        public static readonly FieldDefinitionInteractionEventContact InteractionEventContact = FieldDefinitionInteractionEventContact.Instance;
        public static readonly FieldDefinitionInteractionEventProject InteractionEventProject = FieldDefinitionInteractionEventProject.Instance;
        public static readonly FieldDefinitionInteractionEventLocation InteractionEventLocation = FieldDefinitionInteractionEventLocation.Instance;
        public static readonly FieldDefinitionGrantAllocationName GrantAllocationName = FieldDefinitionGrantAllocationName.Instance;
        public static readonly FieldDefinitionDivision Division = FieldDefinitionDivision.Instance;
        public static readonly FieldDefinitionGrantManager GrantManager = FieldDefinitionGrantManager.Instance;
        public static readonly FieldDefinitionJob Job = FieldDefinitionJob.Instance;
        public static readonly FieldDefinitionJobImportTableType JobImportTableType = FieldDefinitionJobImportTableType.Instance;
        public static readonly FieldDefinitionGrantAllocationBudgetLineItem GrantAllocationBudgetLineItem = FieldDefinitionGrantAllocationBudgetLineItem.Instance;

        public static readonly List<FieldDefinition> All;
        public static readonly ReadOnlyDictionary<int, FieldDefinition> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FieldDefinition()
        {
            All = new List<FieldDefinition> { ProjectType, ExpectedValue, TaxonomyTrunk, PrimaryContactOrganization, ProjectsStewardOrganizationRelationshipToProject, Organization, Password, PerformanceMeasure, PerformanceMeasureType, MeasurementUnit, PhotoCaption, PhotoCredit, PhotoTiming, OrganizationPrimaryContact, TaxonomyBranch, CompletionDate, ProjectDescription, ProjectName, ProjectNote, ExpirationDate, ReportedValue, OrganizationType, ProjectGrantAllocationRequestTotalAmount, ProjectStage, ClassificationName, EstimatedTotalCost, UnfundedNeed, Username, Project, Classification, PerformanceMeasureSubcategory, PerformanceMeasureSubcategoryOption, IsPrimaryTaxonomyBranch, FundedAmount, ProjectLocation, ExcludeFromFactSheet, ProjectCostInYearOfExpenditure, GlobalInflationRate, ReportingYear, TagName, TagDescription, ReportedExpenditure, Application, SpendingAssociatedWithPM, StartApprovalDate, AssociatedTaxonomyBranches, ExternalLinks, CurrentYearForPVCalculations, LifecycleOperatingCost, PerformanceMeasureChartTitle, RoleName, Region, PerformanceMeasureChartCaption, MonitoringProgram, MonitoringApproach, MonitoringProgramPartner, MonitoringProgramUrl, ClassificationDescription, ClassificationGoalStatement, ClassificationNarrative, TaxonomySystemName, ProjectTypeDisplayNameForProject, ProjectRelationshipType, ProjectSteward, ChartLastUpdatedDate, UnsecuredFunding, ProjectStewardOrganizationDisplayName, ClassificationSystem, ClassificationSystemName, ProjectPrimaryContact, CustomPageDisplayType, TaxonomyTrunkDescription, TaxonomyBranchDescription, ProjectTypeDescription, ShowApplicationsToThePublic, ShowLeadImplementerLogoOnFactSheet, ProjectCustomAttribute, ProjectCustomAttributeDataType, ProjectUpdateKickOffDate, ProjectUpdateReminderInterval, ProjectUpdateCloseOutDate, PerformanceMeasureIsAggregatable, GrantAllocationAmount, NormalUser, ProjectStewardshipArea, ProjectInternalNote, StatewideVendorNumber, Contact, ContactRelationshipType, Contractor, Landowner, Partner, PrimaryContact, FocusArea, Grant, GrantAllocation, CostType, ProjectCode, GrantAllocationProjectCode, ProgramIndex, GrantName, GrantShortName, GrantStatus, GrantType, GrantNumber, CFDA, TotalAwardAmount, GrantStartDate, GrantEndDate, GrantNote, PriorityArea, Invoice, Agreement, FederalFundCode, AllocationAmount, AgreementType, AgreementNumber, AgreementTitle, AgreementStartDate, AgreementEndDate, AgreementAmount, ProgramManager, AgreementNotes, AgreementStatus, GrantAllocationNote, FileResource, ProjectTotalCompletedFootprintAcres, FocusAreaTotalProjectReportedExpendiures, FocusAreaTotalProjectEstimatedTotalCosts, FocusAreaTotalCompletedFootprintAcres, FocusAreaTotalPlannedFootprintAcres, FocusAreaCloseoutReportProjectList, RequestorName, InvoiceDate, PurchaseAuthority, TotalRequestedInvoicePaymentAmount, PreparedByPerson, InvoiceIdentifyingName, GrantNoteInternal, GrantAllocationNoteInternal, InvoiceStatus, InvoiceApprovalStatus, InvoiceApprovalComment, MatchAmount, Vendor, EstimatedIndirectCost, EstimatedPersonnelAndBenefitsCost, EstimatedSuppliesCost, EstimatedTravelCost, InvoiceID, InvoiceLineItem, InteractionEvent, InteractionEventType, DNRStaffPerson, InteractionEventContact, InteractionEventProject, InteractionEventLocation, GrantAllocationName, Division, GrantManager, Job, JobImportTableType, GrantAllocationBudgetLineItem };
            AllLookupDictionary = new ReadOnlyDictionary<int, FieldDefinition>(All.ToDictionary(x => x.FieldDefinitionID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected FieldDefinition(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition)
        {
            FieldDefinitionID = fieldDefinitionID;
            FieldDefinitionName = fieldDefinitionName;
            FieldDefinitionDisplayName = fieldDefinitionDisplayName;
            DefaultDefinition = defaultDefinition;
        }
        public List<ProjectPersonRelationshipType> ProjectPersonRelationshipTypes { get { return ProjectPersonRelationshipType.All.Where(x => x.FieldDefinitionID == FieldDefinitionID).ToList(); } }
        [Key]
        public int FieldDefinitionID { get; private set; }
        public string FieldDefinitionName { get; private set; }
        public string FieldDefinitionDisplayName { get; private set; }
        public string DefaultDefinition { get; set; }
        [NotMapped]
        public HtmlString DefaultDefinitionHtmlString
        { 
            get { return DefaultDefinition == null ? null : new HtmlString(DefaultDefinition); }
            set { DefaultDefinition = value?.ToString(); }
        }
        [NotMapped]
        public int PrimaryKey { get { return FieldDefinitionID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(FieldDefinition other)
        {
            if (other == null)
            {
                return false;
            }
            return other.FieldDefinitionID == FieldDefinitionID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as FieldDefinition);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return FieldDefinitionID;
        }

        public static bool operator ==(FieldDefinition left, FieldDefinition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FieldDefinition left, FieldDefinition right)
        {
            return !Equals(left, right);
        }

        public FieldDefinitionEnum ToEnum { get { return (FieldDefinitionEnum)GetHashCode(); } }

        public static FieldDefinition ToType(int enumValue)
        {
            return ToType((FieldDefinitionEnum)enumValue);
        }

        public static FieldDefinition ToType(FieldDefinitionEnum enumValue)
        {
            switch (enumValue)
            {
                case FieldDefinitionEnum.Agreement:
                    return Agreement;
                case FieldDefinitionEnum.AgreementAmount:
                    return AgreementAmount;
                case FieldDefinitionEnum.AgreementEndDate:
                    return AgreementEndDate;
                case FieldDefinitionEnum.AgreementNotes:
                    return AgreementNotes;
                case FieldDefinitionEnum.AgreementNumber:
                    return AgreementNumber;
                case FieldDefinitionEnum.AgreementStartDate:
                    return AgreementStartDate;
                case FieldDefinitionEnum.AgreementStatus:
                    return AgreementStatus;
                case FieldDefinitionEnum.AgreementTitle:
                    return AgreementTitle;
                case FieldDefinitionEnum.AgreementType:
                    return AgreementType;
                case FieldDefinitionEnum.AllocationAmount:
                    return AllocationAmount;
                case FieldDefinitionEnum.Application:
                    return Application;
                case FieldDefinitionEnum.AssociatedTaxonomyBranches:
                    return AssociatedTaxonomyBranches;
                case FieldDefinitionEnum.CFDA:
                    return CFDA;
                case FieldDefinitionEnum.ChartLastUpdatedDate:
                    return ChartLastUpdatedDate;
                case FieldDefinitionEnum.Classification:
                    return Classification;
                case FieldDefinitionEnum.ClassificationDescription:
                    return ClassificationDescription;
                case FieldDefinitionEnum.ClassificationGoalStatement:
                    return ClassificationGoalStatement;
                case FieldDefinitionEnum.ClassificationName:
                    return ClassificationName;
                case FieldDefinitionEnum.ClassificationNarrative:
                    return ClassificationNarrative;
                case FieldDefinitionEnum.ClassificationSystem:
                    return ClassificationSystem;
                case FieldDefinitionEnum.ClassificationSystemName:
                    return ClassificationSystemName;
                case FieldDefinitionEnum.CompletionDate:
                    return CompletionDate;
                case FieldDefinitionEnum.Contact:
                    return Contact;
                case FieldDefinitionEnum.ContactRelationshipType:
                    return ContactRelationshipType;
                case FieldDefinitionEnum.Contractor:
                    return Contractor;
                case FieldDefinitionEnum.CostType:
                    return CostType;
                case FieldDefinitionEnum.CurrentYearForPVCalculations:
                    return CurrentYearForPVCalculations;
                case FieldDefinitionEnum.CustomPageDisplayType:
                    return CustomPageDisplayType;
                case FieldDefinitionEnum.Division:
                    return Division;
                case FieldDefinitionEnum.DNRStaffPerson:
                    return DNRStaffPerson;
                case FieldDefinitionEnum.EstimatedIndirectCost:
                    return EstimatedIndirectCost;
                case FieldDefinitionEnum.EstimatedPersonnelAndBenefitsCost:
                    return EstimatedPersonnelAndBenefitsCost;
                case FieldDefinitionEnum.EstimatedSuppliesCost:
                    return EstimatedSuppliesCost;
                case FieldDefinitionEnum.EstimatedTotalCost:
                    return EstimatedTotalCost;
                case FieldDefinitionEnum.EstimatedTravelCost:
                    return EstimatedTravelCost;
                case FieldDefinitionEnum.ExcludeFromFactSheet:
                    return ExcludeFromFactSheet;
                case FieldDefinitionEnum.ExpectedValue:
                    return ExpectedValue;
                case FieldDefinitionEnum.ExpirationDate:
                    return ExpirationDate;
                case FieldDefinitionEnum.ExternalLinks:
                    return ExternalLinks;
                case FieldDefinitionEnum.FederalFundCode:
                    return FederalFundCode;
                case FieldDefinitionEnum.FileResource:
                    return FileResource;
                case FieldDefinitionEnum.FocusArea:
                    return FocusArea;
                case FieldDefinitionEnum.FocusAreaCloseoutReportProjectList:
                    return FocusAreaCloseoutReportProjectList;
                case FieldDefinitionEnum.FocusAreaTotalCompletedFootprintAcres:
                    return FocusAreaTotalCompletedFootprintAcres;
                case FieldDefinitionEnum.FocusAreaTotalPlannedFootprintAcres:
                    return FocusAreaTotalPlannedFootprintAcres;
                case FieldDefinitionEnum.FocusAreaTotalProjectEstimatedTotalCosts:
                    return FocusAreaTotalProjectEstimatedTotalCosts;
                case FieldDefinitionEnum.FocusAreaTotalProjectReportedExpendiures:
                    return FocusAreaTotalProjectReportedExpendiures;
                case FieldDefinitionEnum.FundedAmount:
                    return FundedAmount;
                case FieldDefinitionEnum.GlobalInflationRate:
                    return GlobalInflationRate;
                case FieldDefinitionEnum.Grant:
                    return Grant;
                case FieldDefinitionEnum.GrantAllocation:
                    return GrantAllocation;
                case FieldDefinitionEnum.GrantAllocationAmount:
                    return GrantAllocationAmount;
                case FieldDefinitionEnum.GrantAllocationBudgetLineItem:
                    return GrantAllocationBudgetLineItem;
                case FieldDefinitionEnum.GrantAllocationName:
                    return GrantAllocationName;
                case FieldDefinitionEnum.GrantAllocationNote:
                    return GrantAllocationNote;
                case FieldDefinitionEnum.GrantAllocationNoteInternal:
                    return GrantAllocationNoteInternal;
                case FieldDefinitionEnum.GrantAllocationProjectCode:
                    return GrantAllocationProjectCode;
                case FieldDefinitionEnum.GrantEndDate:
                    return GrantEndDate;
                case FieldDefinitionEnum.GrantManager:
                    return GrantManager;
                case FieldDefinitionEnum.GrantName:
                    return GrantName;
                case FieldDefinitionEnum.GrantNote:
                    return GrantNote;
                case FieldDefinitionEnum.GrantNoteInternal:
                    return GrantNoteInternal;
                case FieldDefinitionEnum.GrantNumber:
                    return GrantNumber;
                case FieldDefinitionEnum.GrantShortName:
                    return GrantShortName;
                case FieldDefinitionEnum.GrantStartDate:
                    return GrantStartDate;
                case FieldDefinitionEnum.GrantStatus:
                    return GrantStatus;
                case FieldDefinitionEnum.GrantType:
                    return GrantType;
                case FieldDefinitionEnum.InteractionEvent:
                    return InteractionEvent;
                case FieldDefinitionEnum.InteractionEventContact:
                    return InteractionEventContact;
                case FieldDefinitionEnum.InteractionEventLocation:
                    return InteractionEventLocation;
                case FieldDefinitionEnum.InteractionEventProject:
                    return InteractionEventProject;
                case FieldDefinitionEnum.InteractionEventType:
                    return InteractionEventType;
                case FieldDefinitionEnum.Invoice:
                    return Invoice;
                case FieldDefinitionEnum.InvoiceApprovalComment:
                    return InvoiceApprovalComment;
                case FieldDefinitionEnum.InvoiceApprovalStatus:
                    return InvoiceApprovalStatus;
                case FieldDefinitionEnum.InvoiceDate:
                    return InvoiceDate;
                case FieldDefinitionEnum.InvoiceID:
                    return InvoiceID;
                case FieldDefinitionEnum.InvoiceIdentifyingName:
                    return InvoiceIdentifyingName;
                case FieldDefinitionEnum.InvoiceLineItem:
                    return InvoiceLineItem;
                case FieldDefinitionEnum.InvoiceStatus:
                    return InvoiceStatus;
                case FieldDefinitionEnum.IsPrimaryTaxonomyBranch:
                    return IsPrimaryTaxonomyBranch;
                case FieldDefinitionEnum.Job:
                    return Job;
                case FieldDefinitionEnum.JobImportTableType:
                    return JobImportTableType;
                case FieldDefinitionEnum.Landowner:
                    return Landowner;
                case FieldDefinitionEnum.LifecycleOperatingCost:
                    return LifecycleOperatingCost;
                case FieldDefinitionEnum.MatchAmount:
                    return MatchAmount;
                case FieldDefinitionEnum.MeasurementUnit:
                    return MeasurementUnit;
                case FieldDefinitionEnum.MonitoringApproach:
                    return MonitoringApproach;
                case FieldDefinitionEnum.MonitoringProgram:
                    return MonitoringProgram;
                case FieldDefinitionEnum.MonitoringProgramPartner:
                    return MonitoringProgramPartner;
                case FieldDefinitionEnum.MonitoringProgramUrl:
                    return MonitoringProgramUrl;
                case FieldDefinitionEnum.NormalUser:
                    return NormalUser;
                case FieldDefinitionEnum.Organization:
                    return Organization;
                case FieldDefinitionEnum.OrganizationPrimaryContact:
                    return OrganizationPrimaryContact;
                case FieldDefinitionEnum.OrganizationType:
                    return OrganizationType;
                case FieldDefinitionEnum.Partner:
                    return Partner;
                case FieldDefinitionEnum.Password:
                    return Password;
                case FieldDefinitionEnum.PerformanceMeasure:
                    return PerformanceMeasure;
                case FieldDefinitionEnum.PerformanceMeasureChartCaption:
                    return PerformanceMeasureChartCaption;
                case FieldDefinitionEnum.PerformanceMeasureChartTitle:
                    return PerformanceMeasureChartTitle;
                case FieldDefinitionEnum.PerformanceMeasureIsAggregatable:
                    return PerformanceMeasureIsAggregatable;
                case FieldDefinitionEnum.PerformanceMeasureSubcategory:
                    return PerformanceMeasureSubcategory;
                case FieldDefinitionEnum.PerformanceMeasureSubcategoryOption:
                    return PerformanceMeasureSubcategoryOption;
                case FieldDefinitionEnum.PerformanceMeasureType:
                    return PerformanceMeasureType;
                case FieldDefinitionEnum.PhotoCaption:
                    return PhotoCaption;
                case FieldDefinitionEnum.PhotoCredit:
                    return PhotoCredit;
                case FieldDefinitionEnum.PhotoTiming:
                    return PhotoTiming;
                case FieldDefinitionEnum.PreparedByPerson:
                    return PreparedByPerson;
                case FieldDefinitionEnum.PrimaryContact:
                    return PrimaryContact;
                case FieldDefinitionEnum.PrimaryContactOrganization:
                    return PrimaryContactOrganization;
                case FieldDefinitionEnum.PriorityArea:
                    return PriorityArea;
                case FieldDefinitionEnum.ProgramIndex:
                    return ProgramIndex;
                case FieldDefinitionEnum.ProgramManager:
                    return ProgramManager;
                case FieldDefinitionEnum.Project:
                    return Project;
                case FieldDefinitionEnum.ProjectCode:
                    return ProjectCode;
                case FieldDefinitionEnum.ProjectCostInYearOfExpenditure:
                    return ProjectCostInYearOfExpenditure;
                case FieldDefinitionEnum.ProjectCustomAttribute:
                    return ProjectCustomAttribute;
                case FieldDefinitionEnum.ProjectCustomAttributeDataType:
                    return ProjectCustomAttributeDataType;
                case FieldDefinitionEnum.ProjectDescription:
                    return ProjectDescription;
                case FieldDefinitionEnum.ProjectGrantAllocationRequestTotalAmount:
                    return ProjectGrantAllocationRequestTotalAmount;
                case FieldDefinitionEnum.ProjectInternalNote:
                    return ProjectInternalNote;
                case FieldDefinitionEnum.ProjectLocation:
                    return ProjectLocation;
                case FieldDefinitionEnum.ProjectName:
                    return ProjectName;
                case FieldDefinitionEnum.ProjectNote:
                    return ProjectNote;
                case FieldDefinitionEnum.ProjectPrimaryContact:
                    return ProjectPrimaryContact;
                case FieldDefinitionEnum.ProjectRelationshipType:
                    return ProjectRelationshipType;
                case FieldDefinitionEnum.ProjectsStewardOrganizationRelationshipToProject:
                    return ProjectsStewardOrganizationRelationshipToProject;
                case FieldDefinitionEnum.ProjectStage:
                    return ProjectStage;
                case FieldDefinitionEnum.ProjectSteward:
                    return ProjectSteward;
                case FieldDefinitionEnum.ProjectStewardOrganizationDisplayName:
                    return ProjectStewardOrganizationDisplayName;
                case FieldDefinitionEnum.ProjectStewardshipArea:
                    return ProjectStewardshipArea;
                case FieldDefinitionEnum.ProjectTotalCompletedFootprintAcres:
                    return ProjectTotalCompletedFootprintAcres;
                case FieldDefinitionEnum.ProjectType:
                    return ProjectType;
                case FieldDefinitionEnum.ProjectTypeDescription:
                    return ProjectTypeDescription;
                case FieldDefinitionEnum.ProjectTypeDisplayNameForProject:
                    return ProjectTypeDisplayNameForProject;
                case FieldDefinitionEnum.ProjectUpdateCloseOutDate:
                    return ProjectUpdateCloseOutDate;
                case FieldDefinitionEnum.ProjectUpdateKickOffDate:
                    return ProjectUpdateKickOffDate;
                case FieldDefinitionEnum.ProjectUpdateReminderInterval:
                    return ProjectUpdateReminderInterval;
                case FieldDefinitionEnum.PurchaseAuthority:
                    return PurchaseAuthority;
                case FieldDefinitionEnum.Region:
                    return Region;
                case FieldDefinitionEnum.ReportedExpenditure:
                    return ReportedExpenditure;
                case FieldDefinitionEnum.ReportedValue:
                    return ReportedValue;
                case FieldDefinitionEnum.ReportingYear:
                    return ReportingYear;
                case FieldDefinitionEnum.RequestorName:
                    return RequestorName;
                case FieldDefinitionEnum.RoleName:
                    return RoleName;
                case FieldDefinitionEnum.ShowApplicationsToThePublic:
                    return ShowApplicationsToThePublic;
                case FieldDefinitionEnum.ShowLeadImplementerLogoOnFactSheet:
                    return ShowLeadImplementerLogoOnFactSheet;
                case FieldDefinitionEnum.SpendingAssociatedWithPM:
                    return SpendingAssociatedWithPM;
                case FieldDefinitionEnum.StartApprovalDate:
                    return StartApprovalDate;
                case FieldDefinitionEnum.StatewideVendorNumber:
                    return StatewideVendorNumber;
                case FieldDefinitionEnum.TagDescription:
                    return TagDescription;
                case FieldDefinitionEnum.TagName:
                    return TagName;
                case FieldDefinitionEnum.TaxonomyBranch:
                    return TaxonomyBranch;
                case FieldDefinitionEnum.TaxonomyBranchDescription:
                    return TaxonomyBranchDescription;
                case FieldDefinitionEnum.TaxonomySystemName:
                    return TaxonomySystemName;
                case FieldDefinitionEnum.TaxonomyTrunk:
                    return TaxonomyTrunk;
                case FieldDefinitionEnum.TaxonomyTrunkDescription:
                    return TaxonomyTrunkDescription;
                case FieldDefinitionEnum.TotalAwardAmount:
                    return TotalAwardAmount;
                case FieldDefinitionEnum.TotalRequestedInvoicePaymentAmount:
                    return TotalRequestedInvoicePaymentAmount;
                case FieldDefinitionEnum.UnfundedNeed:
                    return UnfundedNeed;
                case FieldDefinitionEnum.UnsecuredFunding:
                    return UnsecuredFunding;
                case FieldDefinitionEnum.Username:
                    return Username;
                case FieldDefinitionEnum.Vendor:
                    return Vendor;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum FieldDefinitionEnum
    {
        ProjectType = 1,
        ExpectedValue = 4,
        TaxonomyTrunk = 5,
        PrimaryContactOrganization = 12,
        ProjectsStewardOrganizationRelationshipToProject = 13,
        Organization = 14,
        Password = 17,
        PerformanceMeasure = 18,
        PerformanceMeasureType = 19,
        MeasurementUnit = 21,
        PhotoCaption = 22,
        PhotoCredit = 23,
        PhotoTiming = 24,
        OrganizationPrimaryContact = 25,
        TaxonomyBranch = 26,
        CompletionDate = 28,
        ProjectDescription = 29,
        ProjectName = 30,
        ProjectNote = 31,
        ExpirationDate = 32,
        ReportedValue = 33,
        OrganizationType = 34,
        ProjectGrantAllocationRequestTotalAmount = 35,
        ProjectStage = 36,
        ClassificationName = 39,
        EstimatedTotalCost = 40,
        UnfundedNeed = 41,
        Username = 42,
        Project = 44,
        Classification = 46,
        PerformanceMeasureSubcategory = 49,
        PerformanceMeasureSubcategoryOption = 50,
        IsPrimaryTaxonomyBranch = 52,
        FundedAmount = 56,
        ProjectLocation = 57,
        ExcludeFromFactSheet = 64,
        ProjectCostInYearOfExpenditure = 74,
        GlobalInflationRate = 75,
        ReportingYear = 76,
        TagName = 77,
        TagDescription = 78,
        ReportedExpenditure = 80,
        Application = 81,
        SpendingAssociatedWithPM = 85,
        StartApprovalDate = 86,
        AssociatedTaxonomyBranches = 87,
        ExternalLinks = 88,
        CurrentYearForPVCalculations = 91,
        LifecycleOperatingCost = 92,
        PerformanceMeasureChartTitle = 97,
        RoleName = 182,
        Region = 184,
        PerformanceMeasureChartCaption = 228,
        MonitoringProgram = 236,
        MonitoringApproach = 237,
        MonitoringProgramPartner = 238,
        MonitoringProgramUrl = 239,
        ClassificationDescription = 240,
        ClassificationGoalStatement = 241,
        ClassificationNarrative = 242,
        TaxonomySystemName = 243,
        ProjectTypeDisplayNameForProject = 244,
        ProjectRelationshipType = 245,
        ProjectSteward = 246,
        ChartLastUpdatedDate = 247,
        UnsecuredFunding = 248,
        ProjectStewardOrganizationDisplayName = 249,
        ClassificationSystem = 250,
        ClassificationSystemName = 251,
        ProjectPrimaryContact = 252,
        CustomPageDisplayType = 253,
        TaxonomyTrunkDescription = 254,
        TaxonomyBranchDescription = 255,
        ProjectTypeDescription = 256,
        ShowApplicationsToThePublic = 257,
        ShowLeadImplementerLogoOnFactSheet = 258,
        ProjectCustomAttribute = 259,
        ProjectCustomAttributeDataType = 260,
        ProjectUpdateKickOffDate = 261,
        ProjectUpdateReminderInterval = 262,
        ProjectUpdateCloseOutDate = 263,
        PerformanceMeasureIsAggregatable = 264,
        GrantAllocationAmount = 265,
        NormalUser = 266,
        ProjectStewardshipArea = 267,
        ProjectInternalNote = 268,
        StatewideVendorNumber = 269,
        Contact = 270,
        ContactRelationshipType = 271,
        Contractor = 272,
        Landowner = 273,
        Partner = 274,
        PrimaryContact = 275,
        FocusArea = 276,
        Grant = 277,
        GrantAllocation = 278,
        CostType = 279,
        ProjectCode = 280,
        GrantAllocationProjectCode = 281,
        ProgramIndex = 282,
        GrantName = 283,
        GrantShortName = 284,
        GrantStatus = 285,
        GrantType = 286,
        GrantNumber = 287,
        CFDA = 288,
        TotalAwardAmount = 289,
        GrantStartDate = 290,
        GrantEndDate = 291,
        GrantNote = 292,
        PriorityArea = 293,
        Invoice = 294,
        Agreement = 295,
        FederalFundCode = 296,
        AllocationAmount = 297,
        AgreementType = 298,
        AgreementNumber = 299,
        AgreementTitle = 300,
        AgreementStartDate = 301,
        AgreementEndDate = 302,
        AgreementAmount = 303,
        ProgramManager = 304,
        AgreementNotes = 305,
        AgreementStatus = 306,
        GrantAllocationNote = 307,
        FileResource = 308,
        ProjectTotalCompletedFootprintAcres = 309,
        FocusAreaTotalProjectReportedExpendiures = 310,
        FocusAreaTotalProjectEstimatedTotalCosts = 311,
        FocusAreaTotalCompletedFootprintAcres = 312,
        FocusAreaTotalPlannedFootprintAcres = 313,
        FocusAreaCloseoutReportProjectList = 314,
        RequestorName = 315,
        InvoiceDate = 316,
        PurchaseAuthority = 317,
        TotalRequestedInvoicePaymentAmount = 318,
        PreparedByPerson = 319,
        InvoiceIdentifyingName = 320,
        GrantNoteInternal = 321,
        GrantAllocationNoteInternal = 322,
        InvoiceStatus = 323,
        InvoiceApprovalStatus = 324,
        InvoiceApprovalComment = 325,
        MatchAmount = 326,
        Vendor = 327,
        EstimatedIndirectCost = 328,
        EstimatedPersonnelAndBenefitsCost = 329,
        EstimatedSuppliesCost = 330,
        EstimatedTravelCost = 331,
        InvoiceID = 332,
        InvoiceLineItem = 333,
        InteractionEvent = 334,
        InteractionEventType = 335,
        DNRStaffPerson = 336,
        InteractionEventContact = 337,
        InteractionEventProject = 338,
        InteractionEventLocation = 339,
        GrantAllocationName = 340,
        Division = 341,
        GrantManager = 342,
        Job = 343,
        JobImportTableType = 344,
        GrantAllocationBudgetLineItem = 345
    }

    public partial class FieldDefinitionProjectType : FieldDefinition
    {
        private FieldDefinitionProjectType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectType Instance = new FieldDefinitionProjectType(1, @"ProjectType", @"Project Type", @"<p>The highest level record in the hierarchical project taxonomy system.</p>");
    }

    public partial class FieldDefinitionExpectedValue : FieldDefinition
    {
        private FieldDefinitionExpectedValue(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExpectedValue Instance = new FieldDefinitionExpectedValue(4, @"ExpectedValue", @"Expected Value", @"<p>The estimated cumulative Performance Measure value that the project or program is projected to achieve after implementation.</p>");
    }

    public partial class FieldDefinitionTaxonomyTrunk : FieldDefinition
    {
        private FieldDefinitionTaxonomyTrunk(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTaxonomyTrunk Instance = new FieldDefinitionTaxonomyTrunk(5, @"TaxonomyTrunk", @"Taxonomy Trunk", @"<p>The lowest level record in the hierarchical project taxonomy system.</p>");
    }

    public partial class FieldDefinitionPrimaryContactOrganization : FieldDefinition
    {
        private FieldDefinitionPrimaryContactOrganization(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPrimaryContactOrganization Instance = new FieldDefinitionPrimaryContactOrganization(12, @"PrimaryContactOrganization", @"Primary Contact Organization", @"<p>The entity with primary responsibility for organizing, planning, and executing implementation activities for a project or program. This is usually the lead implementer.</p>");
    }

    public partial class FieldDefinitionProjectsStewardOrganizationRelationshipToProject : FieldDefinition
    {
        private FieldDefinitionProjectsStewardOrganizationRelationshipToProject(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectsStewardOrganizationRelationshipToProject Instance = new FieldDefinitionProjectsStewardOrganizationRelationshipToProject(13, @"ProjectsStewardOrganizationRelationshipToProject", @"Projects Steward Organization Relationship To Project", @"<p>The relationship between a stewarding organization and a project.</p>");
    }

    public partial class FieldDefinitionOrganization : FieldDefinition
    {
        private FieldDefinitionOrganization(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionOrganization Instance = new FieldDefinitionOrganization(14, @"Organization", @"Organization", @"<p>A partner entity that is directly involved with implementation or funding a project.&nbsp;</p>");
    }

    public partial class FieldDefinitionPassword : FieldDefinition
    {
        private FieldDefinitionPassword(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPassword Instance = new FieldDefinitionPassword(17, @"Password", @"Password", @"<p>Password required to log into the ProjectFirma tool in order to access and edit project and program information.</p>");
    }

    public partial class FieldDefinitionPerformanceMeasure : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasure(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasure Instance = new FieldDefinitionPerformanceMeasure(18, @"PerformanceMeasure", @"Performance Measure", @"<p>A consistent and targeted method to track actions taken through completed projects to improve the environment. Also known as &quot;Indicators.&quot;&nbsp;</p>");
    }

    public partial class FieldDefinitionPerformanceMeasureType : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasureType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasureType Instance = new FieldDefinitionPerformanceMeasureType(19, @"PerformanceMeasureType", @"Performance Measure Type", @"<p>The type of a Performance Measure - either an Action or Outcome.</p>");
    }

    public partial class FieldDefinitionMeasurementUnit : FieldDefinition
    {
        private FieldDefinitionMeasurementUnit(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMeasurementUnit Instance = new FieldDefinitionMeasurementUnit(21, @"MeasurementUnit", @"Measurement Unit", @"<p>The unit of measure used by an Indicator (aka&nbsp;Performance Measure) to track the extent of implementation.</p>");
    }

    public partial class FieldDefinitionPhotoCaption : FieldDefinition
    {
        private FieldDefinitionPhotoCaption(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPhotoCaption Instance = new FieldDefinitionPhotoCaption(22, @"PhotoCaption", @"Photo Caption", @"<p>A concise yet descriptive explanation of an uploaded photo. Photo captions are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>");
    }

    public partial class FieldDefinitionPhotoCredit : FieldDefinition
    {
        private FieldDefinitionPhotoCredit(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPhotoCredit Instance = new FieldDefinitionPhotoCredit(23, @"PhotoCredit", @"Photo Credit", @"<p>If needed, credit is given to the photographer or owner of an image on the website. Photo credits are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>");
    }

    public partial class FieldDefinitionPhotoTiming : FieldDefinition
    {
        private FieldDefinitionPhotoTiming(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPhotoTiming Instance = new FieldDefinitionPhotoTiming(24, @"PhotoTiming", @"Photo Timing", @"<p>The phase in a project timeline during which the photograph was taken. Photo timing can be before, during or after project implementation.&nbsp;</p>");
    }

    public partial class FieldDefinitionOrganizationPrimaryContact : FieldDefinition
    {
        private FieldDefinitionOrganizationPrimaryContact(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionOrganizationPrimaryContact Instance = new FieldDefinitionOrganizationPrimaryContact(25, @"OrganizationPrimaryContact", @"Organization Primary Contact", @"<p>An individual at the listed organization responsible for reporting accomplishments and expenditures achieved by the project or program, and who should be contacted when there are questions related to any project associated to the organization.</p>");
    }

    public partial class FieldDefinitionTaxonomyBranch : FieldDefinition
    {
        private FieldDefinitionTaxonomyBranch(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTaxonomyBranch Instance = new FieldDefinitionTaxonomyBranch(26, @"TaxonomyBranch", @"Taxonomy Branch", @"<p>The second level record in the hierarchical project taxonomy system.</p>");
    }

    public partial class FieldDefinitionCompletionDate : FieldDefinition
    {
        private FieldDefinitionCompletionDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCompletionDate Instance = new FieldDefinitionCompletionDate(28, @"CompletionDate", @"Completion Date", @"<p>The year implementation of the project was completed or is anticipated to be completed. Projects are considered &quot;complete&quot; when all activities have been performed, including post-implementation activities such as monitoring vegetation establishment, and all&nbsp;reporting requirements have been satisfied. &nbsp;For more detailed information, see the definition for &quot;Stage&quot;.</p>");
    }

    public partial class FieldDefinitionProjectDescription : FieldDefinition
    {
        private FieldDefinitionProjectDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectDescription Instance = new FieldDefinitionProjectDescription(29, @"ProjectDescription", @"Project Description", @"<p>A concise/brief description for the project that includes the following: general locations of project, project area size, purpose and need of the project, and expected goals. &nbsp;Please not that project descriptions will be capped at 100 words.</p>");
    }

    public partial class FieldDefinitionProjectName : FieldDefinition
    {
        private FieldDefinitionProjectName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectName Instance = new FieldDefinitionProjectName(30, @"ProjectName", @"Project Name", @"<p>The name of a project or program given by the organization responsible for reporting. Project names should generally include a reference to 1) the location of the project, 2) the primary implementation activity, and 3) the project phase (if a multi-phase project).</p>");
    }

    public partial class FieldDefinitionProjectNote : FieldDefinition
    {
        private FieldDefinitionProjectNote(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectNote Instance = new FieldDefinitionProjectNote(31, @"ProjectNote", @"Project Note", @"<p>Any important information about a project that would be useful to staff or project implementers.</p>");
    }

    public partial class FieldDefinitionExpirationDate : FieldDefinition
    {
        private FieldDefinitionExpirationDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExpirationDate Instance = new FieldDefinitionExpirationDate(32, @"ExpirationDate", @"Expiration Date", @"<p>The date the project expires. For more detailed information on implementation stages, see the definition for &quot;Stage&quot;.</p>");
    }

    public partial class FieldDefinitionReportedValue : FieldDefinition
    {
        private FieldDefinitionReportedValue(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportedValue Instance = new FieldDefinitionReportedValue(33, @"ReportedValue", @"Reported Value", @"<p>The accomplishments achieved by a project after the accomplishments are realized. Accomplishments can be realized and reported throughout implementation and not only after the entire project is completed.</p>");
    }

    public partial class FieldDefinitionOrganizationType : FieldDefinition
    {
        private FieldDefinitionOrganizationType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionOrganizationType Instance = new FieldDefinitionOrganizationType(34, @"OrganizationType", @"Organization Type", @"<p>A categorization of an organization, e.g. Local, State, Federal or Private.</p>");
    }

    public partial class FieldDefinitionProjectGrantAllocationRequestTotalAmount : FieldDefinition
    {
        private FieldDefinitionProjectGrantAllocationRequestTotalAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectGrantAllocationRequestTotalAmount Instance = new FieldDefinitionProjectGrantAllocationRequestTotalAmount(35, @"ProjectGrantAllocationRequestTotalAmount", @"Total Amount", @"<p>Funding that has been acquired for a project.</p>");
    }

    public partial class FieldDefinitionProjectStage : FieldDefinition
    {
        private FieldDefinitionProjectStage(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectStage Instance = new FieldDefinitionProjectStage(36, @"ProjectStage", @"Project Stage", @"<p>Where a project exists in the project life cycle - Planned, Implementation, Complete, Cancelled, etc.</p>");
    }

    public partial class FieldDefinitionClassificationName : FieldDefinition
    {
        private FieldDefinitionClassificationName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassificationName Instance = new FieldDefinitionClassificationName(39, @"ClassificationName", @"Classification Name", @"<p>The name of the grouping in this classification system.</p>");
    }

    public partial class FieldDefinitionEstimatedTotalCost : FieldDefinition
    {
        private FieldDefinitionEstimatedTotalCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionEstimatedTotalCost Instance = new FieldDefinitionEstimatedTotalCost(40, @"EstimatedTotalCost", @"Estimated Total Cost", @"<p>The total estimated cost to complete all stages of project implementation.</p>");
    }

    public partial class FieldDefinitionUnfundedNeed : FieldDefinition
    {
        private FieldDefinitionUnfundedNeed(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionUnfundedNeed Instance = new FieldDefinitionUnfundedNeed(41, @"UnfundedNeed", @"Unfunded Need", @"<p>The difference between the Total Cost and Secured Funding for a project or program.</p>");
    }

    public partial class FieldDefinitionUsername : FieldDefinition
    {
        private FieldDefinitionUsername(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionUsername Instance = new FieldDefinitionUsername(42, @"Username", @"User name", @"<p>Password required to log into the system&nbsp;order to access and edit project and program information that is not allowed by public users.</p>");
    }

    public partial class FieldDefinitionProject : FieldDefinition
    {
        private FieldDefinitionProject(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProject Instance = new FieldDefinitionProject(44, @"Project", @"Project", @"<p>The core entity that ProjectFirma tracks - A collection of activities, with Performance Measures and Expenditures, that contribute to meeting program goals.</p>");
    }

    public partial class FieldDefinitionClassification : FieldDefinition
    {
        private FieldDefinitionClassification(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassification Instance = new FieldDefinitionClassification(46, @"Classification", @"Classification", @"<p>One of the groupings in a logical system used to group projects according to overarching program themes or goals.</p>");
    }

    public partial class FieldDefinitionPerformanceMeasureSubcategory : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasureSubcategory(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasureSubcategory Instance = new FieldDefinitionPerformanceMeasureSubcategory(49, @"PerformanceMeasureSubcategory", @"Performance Measure Subcategory", @"<p>The Performance Measure subcategory or subcategories that are relevant to the project. Subcategories are dimensions of a Performance Measure that are used to report performance measure accomplishments at a more granular level.</p>");
    }

    public partial class FieldDefinitionPerformanceMeasureSubcategoryOption : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasureSubcategoryOption(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasureSubcategoryOption Instance = new FieldDefinitionPerformanceMeasureSubcategoryOption(50, @"PerformanceMeasureSubcategoryOption", @"Performance Measure Subcategory Option", @"<p>The selected attribute of a Performance Measure subcategory.</p>");
    }

    public partial class FieldDefinitionIsPrimaryTaxonomyBranch : FieldDefinition
    {
        private FieldDefinitionIsPrimaryTaxonomyBranch(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionIsPrimaryTaxonomyBranch Instance = new FieldDefinitionIsPrimaryTaxonomyBranch(52, @"IsPrimaryTaxonomyBranch", @"Is Primary Taxonomy Branch", @"<p>If this box is checked, this is the primary area associated with a specific Performance Measure. The performance measure may also apply to other areas but this has been identified as the primary area for this performance measure.</p>");
    }

    public partial class FieldDefinitionFundedAmount : FieldDefinition
    {
        private FieldDefinitionFundedAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFundedAmount Instance = new FieldDefinitionFundedAmount(56, @"FundedAmount", @"Funded Amount", @"<p>The amount of funding, by grant allocation, expended on a project for a specific year. To see the total amount of funding expended on a project, click on the specific project.</p>");
    }

    public partial class FieldDefinitionProjectLocation : FieldDefinition
    {
        private FieldDefinitionProjectLocation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectLocation Instance = new FieldDefinitionProjectLocation(57, @"ProjectLocation", @"Project Location", @"<p>The location where the project was/is being implemented. Project locations can be set by picking a location description from a list or by plotting a point on the map. Project location information is used for summarizing project&nbsp;accomplishments by geospatial categories such as State, County, or GeospatialArea.</p>");
    }

    public partial class FieldDefinitionExcludeFromFactSheet : FieldDefinition
    {
        private FieldDefinitionExcludeFromFactSheet(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExcludeFromFactSheet Instance = new FieldDefinitionExcludeFromFactSheet(64, @"ExcludeFromFactSheet", @"Exclude from Fact Sheet", @"<p>Flags a photo so that it is not included in the photos shown on the project&#39;s Fact Sheet. Some projects have tons of photos -- use this checkbox to control which photos are included.</p>");
    }

    public partial class FieldDefinitionProjectCostInYearOfExpenditure : FieldDefinition
    {
        private FieldDefinitionProjectCostInYearOfExpenditure(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectCostInYearOfExpenditure Instance = new FieldDefinitionProjectCostInYearOfExpenditure(74, @"ProjectCostInYearOfExpenditure", @"Cost in Year of Expenditure", @"<p>This is the expected cost of the project in the year that it will be constructed, taking into account inflation.&nbsp;</p>");
    }

    public partial class FieldDefinitionGlobalInflationRate : FieldDefinition
    {
        private FieldDefinitionGlobalInflationRate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGlobalInflationRate Instance = new FieldDefinitionGlobalInflationRate(75, @"GlobalInflationRate", @"Global Inflation Rate", @"<p>Annual rate of inflation expected for project costs.&nbsp;</p>");
    }

    public partial class FieldDefinitionReportingYear : FieldDefinition
    {
        private FieldDefinitionReportingYear(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportingYear Instance = new FieldDefinitionReportingYear(76, @"ReportingYear", @"Reporting Year", @"<p>The current year used for reporting purposes, which is defined as the previous calendar year until after November 1st of the present calendar year.</p>");
    }

    public partial class FieldDefinitionTagName : FieldDefinition
    {
        private FieldDefinitionTagName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTagName Instance = new FieldDefinitionTagName(77, @"TagName", @"Tag Name", @"<p>A word or phrase for the tag. Keep it short, and if possible avoid spaces (use dashes or underscores) for cleaner URLs. Don&#39;t add tags for things already captured (e.g. the program under which the project lives).</p>");
    }

    public partial class FieldDefinitionTagDescription : FieldDefinition
    {
        private FieldDefinitionTagDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTagDescription Instance = new FieldDefinitionTagDescription(78, @"TagDescription", @"Tag Description", @"<p>A description of what this tag will be used for. Ideally the description should include the name of the user that created it, and an expected timeframe for use of this tag, if known.&nbsp;</p>");
    }

    public partial class FieldDefinitionReportedExpenditure : FieldDefinition
    {
        private FieldDefinitionReportedExpenditure(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportedExpenditure Instance = new FieldDefinitionReportedExpenditure(80, @"ReportedExpenditure", @"Reported Expenditure", @"<p>An expenditure, tied to a Grant Allocation, as reported by the project implementer.</p>");
    }

    public partial class FieldDefinitionApplication : FieldDefinition
    {
        private FieldDefinitionApplication(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionApplication Instance = new FieldDefinitionApplication(81, @"Application", @"Application", @"<p>A project suggested by an program partner that will be reviewed for inclusion in the program. The system administrators are responsible for reviewing, and if appropriate, approving proposals.</p>");
    }

    public partial class FieldDefinitionSpendingAssociatedWithPM : FieldDefinition
    {
        private FieldDefinitionSpendingAssociatedWithPM(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionSpendingAssociatedWithPM Instance = new FieldDefinitionSpendingAssociatedWithPM(85, @"SpendingAssociatedWithPM", @"Spending Associated with PM", @"<p>The estimated spending allotted to the Performance Measure.</p>");
    }

    public partial class FieldDefinitionStartApprovalDate : FieldDefinition
    {
        private FieldDefinitionStartApprovalDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionStartApprovalDate Instance = new FieldDefinitionStartApprovalDate(86, @"StartApprovalDate", @"Start/Approval Date", @"<p>The date on which project began.  Contrast with &quot;Approval/Start Date.&quot; For more detailed information, see the definition for &quot;Stage&quot;.</p>");
    }

    public partial class FieldDefinitionAssociatedTaxonomyBranches : FieldDefinition
    {
        private FieldDefinitionAssociatedTaxonomyBranches(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAssociatedTaxonomyBranches Instance = new FieldDefinitionAssociatedTaxonomyBranches(87, @"AssociatedTaxonomyBranches", @"Associated Taxonomy Branches", @"<p>External&nbsp;programs that are related to the content you are reviewing. You may wish to look up these programs to learn more.</p>");
    }

    public partial class FieldDefinitionExternalLinks : FieldDefinition
    {
        private FieldDefinitionExternalLinks(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalLinks Instance = new FieldDefinitionExternalLinks(88, @"ExternalLinks", @"External Links", @"<p>Links to external web pages where you might find additional information.</p>");
    }

    public partial class FieldDefinitionCurrentYearForPVCalculations : FieldDefinition
    {
        private FieldDefinitionCurrentYearForPVCalculations(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCurrentYearForPVCalculations Instance = new FieldDefinitionCurrentYearForPVCalculations(91, @"CurrentYearForPVCalculations", @"Current Year for PV Calculations", @"<p>The year to use as the starting point for inflation calculations.</p>");
    }

    public partial class FieldDefinitionLifecycleOperatingCost : FieldDefinition
    {
        private FieldDefinitionLifecycleOperatingCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionLifecycleOperatingCost Instance = new FieldDefinitionLifecycleOperatingCost(92, @"LifecycleOperatingCost", @"Lifecycle Operating Cost", @"<p>Sum of the annual operating cost from the Approval/Start Date to Completion Date. Not inflation adjusted.</p>");
    }

    public partial class FieldDefinitionPerformanceMeasureChartTitle : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasureChartTitle(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasureChartTitle Instance = new FieldDefinitionPerformanceMeasureChartTitle(97, @"PerformanceMeasureChartTitle", @"Performance Measure Chart Title", @"<p>A short title for the Indicator (aka Performance Measure) used for charts throughout ProjectFirma.</p>");
    }

    public partial class FieldDefinitionRoleName : FieldDefinition
    {
        private FieldDefinitionRoleName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionRoleName Instance = new FieldDefinitionRoleName(182, @"RoleName", @"Role Name", @"<p>The name or title describing&nbsp;function or set of permissions that can be assigned to a user.</p>");
    }

    public partial class FieldDefinitionRegion : FieldDefinition
    {
        private FieldDefinitionRegion(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionRegion Instance = new FieldDefinitionRegion(184, @"Region", @"Region (Geospatial)", @"<p>The region in which a project is located.</p>");
    }

    public partial class FieldDefinitionPerformanceMeasureChartCaption : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasureChartCaption(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasureChartCaption Instance = new FieldDefinitionPerformanceMeasureChartCaption(228, @"PerformanceMeasureChartCaption", @"Performance Measure Chart Caption", @"<p>The caption which will appear on Performance Measure charts throughout the system.</p>");
    }

    public partial class FieldDefinitionMonitoringProgram : FieldDefinition
    {
        private FieldDefinitionMonitoringProgram(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMonitoringProgram Instance = new FieldDefinitionMonitoringProgram(236, @"MonitoringProgram", @"Monitoring Program", @"<p>A on-going activity to collect environmental monitoring data.</p>");
    }

    public partial class FieldDefinitionMonitoringApproach : FieldDefinition
    {
        private FieldDefinitionMonitoringApproach(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMonitoringApproach Instance = new FieldDefinitionMonitoringApproach(237, @"MonitoringApproach", @"Monitoring Approach", @"<p>Monitoring Approach &ndash; provides a general description of the sampling design used to carry out the monitoring. Included is a description of the spatial distribution of sampling, sampling frequency, lab procedures, and references to data sources, monitoring plans, and protocols used to guide monitoring.</p>");
    }

    public partial class FieldDefinitionMonitoringProgramPartner : FieldDefinition
    {
        private FieldDefinitionMonitoringProgramPartner(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMonitoringProgramPartner Instance = new FieldDefinitionMonitoringProgramPartner(238, @"MonitoringProgramPartner", @"Monitoring Program Partner", @"<p>Monitoring Partners &ndash; provides a list of agencies and entities that fund, collect and analyze monitoring data.</p>");
    }

    public partial class FieldDefinitionMonitoringProgramUrl : FieldDefinition
    {
        private FieldDefinitionMonitoringProgramUrl(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMonitoringProgramUrl Instance = new FieldDefinitionMonitoringProgramUrl(239, @"MonitoringProgramUrl", @"Monitoring Program Home Page", @"<p>The external homepage of a related monitoring program.</p>");
    }

    public partial class FieldDefinitionClassificationDescription : FieldDefinition
    {
        private FieldDefinitionClassificationDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassificationDescription Instance = new FieldDefinitionClassificationDescription(240, @"ClassificationDescription", @"Classification Description", @"<p>The long-form description of the entries in the project classification system.</p>");
    }

    public partial class FieldDefinitionClassificationGoalStatement : FieldDefinition
    {
        private FieldDefinitionClassificationGoalStatement(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassificationGoalStatement Instance = new FieldDefinitionClassificationGoalStatement(241, @"ClassificationGoalStatement", @"Classification Goal Statement", @"<p>The goal of this classification system record.</p>");
    }

    public partial class FieldDefinitionClassificationNarrative : FieldDefinition
    {
        private FieldDefinitionClassificationNarrative(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassificationNarrative Instance = new FieldDefinitionClassificationNarrative(242, @"ClassificationNarrative", @"Classification Narrative", @"<p>Descriptive text describing the criteria for including a project in this classification system.</p>");
    }

    public partial class FieldDefinitionTaxonomySystemName : FieldDefinition
    {
        private FieldDefinitionTaxonomySystemName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTaxonomySystemName Instance = new FieldDefinitionTaxonomySystemName(243, @"TaxonomySystemName", @"Taxonomy System Name", @"<p>The customized name for the hierarchical project taxonomy system.<p>");
    }

    public partial class FieldDefinitionProjectTypeDisplayNameForProject : FieldDefinition
    {
        private FieldDefinitionProjectTypeDisplayNameForProject(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectTypeDisplayNameForProject Instance = new FieldDefinitionProjectTypeDisplayNameForProject(244, @"ProjectTypeDisplayNameForProject", @"Project Type Tier One Display Name For Project", @"<p>A custom label describing how a Project relates to it's highest Taxonomy tier..</p>");
    }

    public partial class FieldDefinitionProjectRelationshipType : FieldDefinition
    {
        private FieldDefinitionProjectRelationshipType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectRelationshipType Instance = new FieldDefinitionProjectRelationshipType(245, @"ProjectRelationshipType", @"Project Relationship Type", @"<p>A categorization of a relationship between an organization and a project, e.g. Funder, Implementer.</p>");
    }

    public partial class FieldDefinitionProjectSteward : FieldDefinition
    {
        private FieldDefinitionProjectSteward(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectSteward Instance = new FieldDefinitionProjectSteward(246, @"ProjectSteward", @"Project Steward", @"<p>A person who can approve Project Applications, create new Projects, approve Project Updates, and create Grant Allocations for their Organization.</p>");
    }

    public partial class FieldDefinitionChartLastUpdatedDate : FieldDefinition
    {
        private FieldDefinitionChartLastUpdatedDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionChartLastUpdatedDate Instance = new FieldDefinitionChartLastUpdatedDate(247, @"Chart Last Updated Date", @"ChartLastUpdatedDate", @"<p>The date this chart was last updated with current information.</p>");
    }

    public partial class FieldDefinitionUnsecuredFunding : FieldDefinition
    {
        private FieldDefinitionUnsecuredFunding(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionUnsecuredFunding Instance = new FieldDefinitionUnsecuredFunding(248, @"UnsecuredFunding", @"Unsecured Funding", @"<p>Funding that has been identified for a project but has not been acquired such as planned grant applications.</p>");
    }

    public partial class FieldDefinitionProjectStewardOrganizationDisplayName : FieldDefinition
    {
        private FieldDefinitionProjectStewardOrganizationDisplayName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectStewardOrganizationDisplayName Instance = new FieldDefinitionProjectStewardOrganizationDisplayName(249, @"ProjectStewardOrganizationDisplayName", @"Project Steward Organization Display Name", @"<p>Label for Organization types that can steward projects.</p>");
    }

    public partial class FieldDefinitionClassificationSystem : FieldDefinition
    {
        private FieldDefinitionClassificationSystem(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassificationSystem Instance = new FieldDefinitionClassificationSystem(250, @"ClassificationSystem", @"Classification System", @"<p>The type of logical system used to group projects according to overarching program themes or goals.</p>");
    }

    public partial class FieldDefinitionClassificationSystemName : FieldDefinition
    {
        private FieldDefinitionClassificationSystemName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassificationSystemName Instance = new FieldDefinitionClassificationSystemName(251, @"ClassificationSystemName", @"Classification System Name", @"<p>The name of the logical grouping used to bin projects.</p>");
    }

    public partial class FieldDefinitionProjectPrimaryContact : FieldDefinition
    {
        private FieldDefinitionProjectPrimaryContact(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectPrimaryContact Instance = new FieldDefinitionProjectPrimaryContact(252, @"ProjectPrimaryContact", @"Project Primary Contact", @"<p>An individual responsible for reporting accomplishments and expenditures achieved by the project, and who should be contacted when there are questions related to the project.</p>");
    }

    public partial class FieldDefinitionCustomPageDisplayType : FieldDefinition
    {
        private FieldDefinitionCustomPageDisplayType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCustomPageDisplayType Instance = new FieldDefinitionCustomPageDisplayType(253, @"CustomPageDisplayType", @"Custom Page Display Type", @"<p>The status of a custom About page, controls whether the page is visible to the public, protected and only visible for logged in users, or disabled and not shown on the About menu.</p>");
    }

    public partial class FieldDefinitionTaxonomyTrunkDescription : FieldDefinition
    {
        private FieldDefinitionTaxonomyTrunkDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTaxonomyTrunkDescription Instance = new FieldDefinitionTaxonomyTrunkDescription(254, @"TaxonomyTrunkDescription", @"Taxonomy Trunk Description", @"<p>The long-form description of the entries in the project taxonomy system.</p>");
    }

    public partial class FieldDefinitionTaxonomyBranchDescription : FieldDefinition
    {
        private FieldDefinitionTaxonomyBranchDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTaxonomyBranchDescription Instance = new FieldDefinitionTaxonomyBranchDescription(255, @"TaxonomyBranchDescription", @"Taxonomy Branch Description", @"<p>The long-form description of the entries in the project taxonomy system.</p>");
    }

    public partial class FieldDefinitionProjectTypeDescription : FieldDefinition
    {
        private FieldDefinitionProjectTypeDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectTypeDescription Instance = new FieldDefinitionProjectTypeDescription(256, @"ProjectTypeDescription", @"Project Type Description", @"<p>The long-form description of the entries in the project taxonomy system.</p>");
    }

    public partial class FieldDefinitionShowApplicationsToThePublic : FieldDefinition
    {
        private FieldDefinitionShowApplicationsToThePublic(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionShowApplicationsToThePublic Instance = new FieldDefinitionShowApplicationsToThePublic(257, @"ShowApplicationsToThePublic", @"Show Applications To The Public", @"<p>When this option is set, projects in the Pending Approval state will be shown on project maps and on the Application page. When not set, no proposals will be visible to anonymous users. All proposals should be shown on the proposals page for Normal+ users.</p>");
    }

    public partial class FieldDefinitionShowLeadImplementerLogoOnFactSheet : FieldDefinition
    {
        private FieldDefinitionShowLeadImplementerLogoOnFactSheet(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionShowLeadImplementerLogoOnFactSheet Instance = new FieldDefinitionShowLeadImplementerLogoOnFactSheet(258, @"ShowLeadImplementerLogoOnFactSheet", @"Show Lead Implementer Logo on Project Fact Sheet?", @"<p>When this option is set, project fact sheets will include the lead implementer's logo under the website logo. When not set, only the website logo will be shown on fact sheets.");
    }

    public partial class FieldDefinitionProjectCustomAttribute : FieldDefinition
    {
        private FieldDefinitionProjectCustomAttribute(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectCustomAttribute Instance = new FieldDefinitionProjectCustomAttribute(259, @"ProjectCustomAttribute", @"Custom Attribute", @"");
    }

    public partial class FieldDefinitionProjectCustomAttributeDataType : FieldDefinition
    {
        private FieldDefinitionProjectCustomAttributeDataType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectCustomAttributeDataType Instance = new FieldDefinitionProjectCustomAttributeDataType(260, @"ProjectCustomAttributeDataType", @"Data Type", @"");
    }

    public partial class FieldDefinitionProjectUpdateKickOffDate : FieldDefinition
    {
        private FieldDefinitionProjectUpdateKickOffDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectUpdateKickOffDate Instance = new FieldDefinitionProjectUpdateKickOffDate(261, @"ProjectUpdateKickOffDate", @"Kick-off Date", @"The date to send the initial notification about Project Updates to Primary Contacts");
    }

    public partial class FieldDefinitionProjectUpdateReminderInterval : FieldDefinition
    {
        private FieldDefinitionProjectUpdateReminderInterval(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectUpdateReminderInterval Instance = new FieldDefinitionProjectUpdateReminderInterval(262, @"ProjectUpdateReminderInterval", @"Reminder Interval (days)", @"The number of days between repeated Project Update Reminders");
    }

    public partial class FieldDefinitionProjectUpdateCloseOutDate : FieldDefinition
    {
        private FieldDefinitionProjectUpdateCloseOutDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectUpdateCloseOutDate Instance = new FieldDefinitionProjectUpdateCloseOutDate(263, @"ProjectUpdateCloseOutDate", @"Close-out Date", @"The date on which to send the final Project Update Reminder");
    }

    public partial class FieldDefinitionPerformanceMeasureIsAggregatable : FieldDefinition
    {
        private FieldDefinitionPerformanceMeasureIsAggregatable(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPerformanceMeasureIsAggregatable Instance = new FieldDefinitionPerformanceMeasureIsAggregatable(264, @"PerformanceMeasureIsAggregatable", @"Is Aggregatable", @"Indicates whether the values for this Performance Measure can be aggregated across subcategory options.");
    }

    public partial class FieldDefinitionGrantAllocationAmount : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAmount Instance = new FieldDefinitionGrantAllocationAmount(265, @"GrantAllocationAmount", @"Amount", @"<p>Grant Allocation Amount</p>");
    }

    public partial class FieldDefinitionNormalUser : FieldDefinition
    {
        private FieldDefinitionNormalUser(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionNormalUser Instance = new FieldDefinitionNormalUser(266, @"NormalUser", @"Normal User", @"Users with this role can propose new Projects, update existing Projects where their organization is the Lead Implementer, and view almost every page within the Project Tracker.");
    }

    public partial class FieldDefinitionProjectStewardshipArea : FieldDefinition
    {
        private FieldDefinitionProjectStewardshipArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectStewardshipArea Instance = new FieldDefinitionProjectStewardshipArea(267, @"ProjectStewardshipArea", @"Project Stewardship Area", @"Indicates which attribute of a project is used to determine if a Project Steward is permitted to edit that project.");
    }

    public partial class FieldDefinitionProjectInternalNote : FieldDefinition
    {
        private FieldDefinitionProjectInternalNote(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectInternalNote Instance = new FieldDefinitionProjectInternalNote(268, @"ProjectInternalNote", @"Internal Note", @"<p>Any important information about a project that should only be visible to Administrators.</p>");
    }

    public partial class FieldDefinitionStatewideVendorNumber : FieldDefinition
    {
        private FieldDefinitionStatewideVendorNumber(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionStatewideVendorNumber Instance = new FieldDefinitionStatewideVendorNumber(269, @"StatewideVendorNumber", @"Statewide Vendor Number", @"A number assigned by the State to vendors.");
    }

    public partial class FieldDefinitionContact : FieldDefinition
    {
        private FieldDefinitionContact(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionContact Instance = new FieldDefinitionContact(270, @"Contact", @"Contact", @"A person who is associated with a project who may or may not have an account in the system.");
    }

    public partial class FieldDefinitionContactRelationshipType : FieldDefinition
    {
        private FieldDefinitionContactRelationshipType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionContactRelationshipType Instance = new FieldDefinitionContactRelationshipType(271, @"ContactRelationshipType", @"Contact Relationship Type", @"<p>A categorization of a relationship between an organization and a contact, e.g. Landowner, Contractor.</p>");
    }

    public partial class FieldDefinitionContractor : FieldDefinition
    {
        private FieldDefinitionContractor(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionContractor Instance = new FieldDefinitionContractor(272, @"Contractor", @"Contractor", @"Placeholder definition for Contractor.");
    }

    public partial class FieldDefinitionLandowner : FieldDefinition
    {
        private FieldDefinitionLandowner(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionLandowner Instance = new FieldDefinitionLandowner(273, @"Landowner", @"Landowner", @"Placeholder definition for Landowner.");
    }

    public partial class FieldDefinitionPartner : FieldDefinition
    {
        private FieldDefinitionPartner(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPartner Instance = new FieldDefinitionPartner(274, @"Partner", @"Partner", @"Placeholder definition for Partner.");
    }

    public partial class FieldDefinitionPrimaryContact : FieldDefinition
    {
        private FieldDefinitionPrimaryContact(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPrimaryContact Instance = new FieldDefinitionPrimaryContact(275, @"PrimaryContact", @"Primary Contact", @"Placeholder definition for Primary Contact.");
    }

    public partial class FieldDefinitionFocusArea : FieldDefinition
    {
        private FieldDefinitionFocusArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFocusArea Instance = new FieldDefinitionFocusArea(276, @"FocusArea", @"Focus Area", @"Placeholder definition for Focus Area");
    }

    public partial class FieldDefinitionGrant : FieldDefinition
    {
        private FieldDefinitionGrant(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrant Instance = new FieldDefinitionGrant(277, @"Grant", @"Grant", @"Placeholder definition for Grant");
    }

    public partial class FieldDefinitionGrantAllocation : FieldDefinition
    {
        private FieldDefinitionGrantAllocation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocation Instance = new FieldDefinitionGrantAllocation(278, @"GrantAllocation", @"Grant Allocation", @"Placeholder definition for Grant Allocation.");
    }

    public partial class FieldDefinitionCostType : FieldDefinition
    {
        private FieldDefinitionCostType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCostType Instance = new FieldDefinitionCostType(279, @"CostType", @"CostType", @"Placeholder definition for CostType.");
    }

    public partial class FieldDefinitionProjectCode : FieldDefinition
    {
        private FieldDefinitionProjectCode(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectCode Instance = new FieldDefinitionProjectCode(280, @"ProjectCode", @"Project Code", @"Placeholder definition for Project Code.");
    }

    public partial class FieldDefinitionGrantAllocationProjectCode : FieldDefinition
    {
        private FieldDefinitionGrantAllocationProjectCode(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationProjectCode Instance = new FieldDefinitionGrantAllocationProjectCode(281, @"GrantAllocationProjectCode", @"Grant Allocation Project Code", @"Placeholder definition for Grant Allocation Project Code.");
    }

    public partial class FieldDefinitionProgramIndex : FieldDefinition
    {
        private FieldDefinitionProgramIndex(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProgramIndex Instance = new FieldDefinitionProgramIndex(282, @"ProgramIndex", @"Program Index", @"Placeholder definition for Program Index.");
    }

    public partial class FieldDefinitionGrantName : FieldDefinition
    {
        private FieldDefinitionGrantName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantName Instance = new FieldDefinitionGrantName(283, @"GrantName", @"Grant Name", @"<p>The name of a grant. Grant names should generally include a reference to 1) the location of the grant, 2) the primary implementation activity, and 3) the grant year</p>");
    }

    public partial class FieldDefinitionGrantShortName : FieldDefinition
    {
        private FieldDefinitionGrantShortName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantShortName Instance = new FieldDefinitionGrantShortName(284, @"GrantShortName", @"Short Name", @"<p>The short hand name to reference a grant</p>");
    }

    public partial class FieldDefinitionGrantStatus : FieldDefinition
    {
        private FieldDefinitionGrantStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantStatus Instance = new FieldDefinitionGrantStatus(285, @"GrantStatus", @"Status", @"<p>The status of a grant. This can be Active, Pending, Planned, or Closeout</p>");
    }

    public partial class FieldDefinitionGrantType : FieldDefinition
    {
        private FieldDefinitionGrantType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantType Instance = new FieldDefinitionGrantType(286, @"GrantType", @"Type", @"<p>The type of grant. This can either be Stand Alone, or CPG. </p>");
    }

    public partial class FieldDefinitionGrantNumber : FieldDefinition
    {
        private FieldDefinitionGrantNumber(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantNumber Instance = new FieldDefinitionGrantNumber(287, @"GrantNumber", @"Grant Number", @"<p>The grant number. </p>");
    }

    public partial class FieldDefinitionCFDA : FieldDefinition
    {
        private FieldDefinitionCFDA(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCFDA Instance = new FieldDefinitionCFDA(288, @"CFDA", @"CFDA", @"<p>The CFDA code for a grant. </p>");
    }

    public partial class FieldDefinitionTotalAwardAmount : FieldDefinition
    {
        private FieldDefinitionTotalAwardAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTotalAwardAmount Instance = new FieldDefinitionTotalAwardAmount(289, @"TotalAwardAmount", @"Total Award Amount", @"<p>The total amount of money awarded by the grant. This may include the sum of all associated grant allocations. </p>");
    }

    public partial class FieldDefinitionGrantStartDate : FieldDefinition
    {
        private FieldDefinitionGrantStartDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantStartDate Instance = new FieldDefinitionGrantStartDate(290, @"GrantStartDate", @"Start Date", @"<p>The start date of the grant. </p>");
    }

    public partial class FieldDefinitionGrantEndDate : FieldDefinition
    {
        private FieldDefinitionGrantEndDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantEndDate Instance = new FieldDefinitionGrantEndDate(291, @"GrantEndDate", @"End Date", @"<p>The end date of the grant. </p>");
    }

    public partial class FieldDefinitionGrantNote : FieldDefinition
    {
        private FieldDefinitionGrantNote(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantNote Instance = new FieldDefinitionGrantNote(292, @"GrantNote", @"Grant Note", @"<p>Any additional important information about the grant. </p>");
    }

    public partial class FieldDefinitionPriorityArea : FieldDefinition
    {
        private FieldDefinitionPriorityArea(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPriorityArea Instance = new FieldDefinitionPriorityArea(293, @"PriorityArea", @"Priority Area", @"Placeholder definition for Priority Area");
    }

    public partial class FieldDefinitionInvoice : FieldDefinition
    {
        private FieldDefinitionInvoice(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoice Instance = new FieldDefinitionInvoice(294, @"Invoice", @"Invoice", @"<p>Placeholder for Invoice</p>");
    }

    public partial class FieldDefinitionAgreement : FieldDefinition
    {
        private FieldDefinitionAgreement(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreement Instance = new FieldDefinitionAgreement(295, @"Agreement", @"Agreement", @"<p>Placeholder for Agreement.</p>");
    }

    public partial class FieldDefinitionFederalFundCode : FieldDefinition
    {
        private FieldDefinitionFederalFundCode(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFederalFundCode Instance = new FieldDefinitionFederalFundCode(296, @"FederalFundCode", @"Federal Fund Code", @"Placeholder definition for Federal Fund Code description.");
    }

    public partial class FieldDefinitionAllocationAmount : FieldDefinition
    {
        private FieldDefinitionAllocationAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAllocationAmount Instance = new FieldDefinitionAllocationAmount(297, @"AllocationAmount", @"Allocation Amount", @"Placeholder for GrantAllocation Allocation Amount.");
    }

    public partial class FieldDefinitionAgreementType : FieldDefinition
    {
        private FieldDefinitionAgreementType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementType Instance = new FieldDefinitionAgreementType(298, @"AgreementType", @"Agreement Type", @"<p>The type of Agreement.</p>");
    }

    public partial class FieldDefinitionAgreementNumber : FieldDefinition
    {
        private FieldDefinitionAgreementNumber(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementNumber Instance = new FieldDefinitionAgreementNumber(299, @"AgreementNumber", @"Agreement Number", @"<p>The number for referencing the Agreement</p>");
    }

    public partial class FieldDefinitionAgreementTitle : FieldDefinition
    {
        private FieldDefinitionAgreementTitle(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementTitle Instance = new FieldDefinitionAgreementTitle(300, @"AgreementTitle", @"Agreement Title", @"<p>The Agreement Title should generally include a 1) reference to the location, 2) the primary implementation activity, and 3) the year.</p>");
    }

    public partial class FieldDefinitionAgreementStartDate : FieldDefinition
    {
        private FieldDefinitionAgreementStartDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementStartDate Instance = new FieldDefinitionAgreementStartDate(301, @"AgreementStartDate", @"Agreement Start Date", @"<p>The start date of the Agreement.</p>");
    }

    public partial class FieldDefinitionAgreementEndDate : FieldDefinition
    {
        private FieldDefinitionAgreementEndDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementEndDate Instance = new FieldDefinitionAgreementEndDate(302, @"AgreementEndDate", @"Agreement End Date", @"<p>The end date of the Agreement.</p>");
    }

    public partial class FieldDefinitionAgreementAmount : FieldDefinition
    {
        private FieldDefinitionAgreementAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementAmount Instance = new FieldDefinitionAgreementAmount(303, @"AgreementAmount", @"Agreement Amount", @"<p>The amount of the Agreement.</p>");
    }

    public partial class FieldDefinitionProgramManager : FieldDefinition
    {
        private FieldDefinitionProgramManager(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProgramManager Instance = new FieldDefinitionProgramManager(304, @"ProgramManager", @"Program Manager", @"Placeholder for Program Manager.");
    }

    public partial class FieldDefinitionAgreementNotes : FieldDefinition
    {
        private FieldDefinitionAgreementNotes(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementNotes Instance = new FieldDefinitionAgreementNotes(305, @"AgreementNotes", @"Agreement Notes", @"Placeholder for Agreement Notes.");
    }

    public partial class FieldDefinitionAgreementStatus : FieldDefinition
    {
        private FieldDefinitionAgreementStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAgreementStatus Instance = new FieldDefinitionAgreementStatus(306, @"AgreementStatus", @"Agreement Status", @"Placeholder for Agreement Status.");
    }

    public partial class FieldDefinitionGrantAllocationNote : FieldDefinition
    {
        private FieldDefinitionGrantAllocationNote(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationNote Instance = new FieldDefinitionGrantAllocationNote(307, @"GrantAllocationNote", @"Grant Allocation Note", @"<p>Any additional important information about the grant allocation.</p>");
    }

    public partial class FieldDefinitionFileResource : FieldDefinition
    {
        private FieldDefinitionFileResource(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFileResource Instance = new FieldDefinitionFileResource(308, @"FileResource", @"File Resource", @"Placeholder for File Resource.");
    }

    public partial class FieldDefinitionProjectTotalCompletedFootprintAcres : FieldDefinition
    {
        private FieldDefinitionProjectTotalCompletedFootprintAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectTotalCompletedFootprintAcres Instance = new FieldDefinitionProjectTotalCompletedFootprintAcres(309, @"ProjectTotalCompletedFootprintAcres", @"Project Completed Footprint Acres", @"Sum of Footprint Acres on all completed Treatment Activities under a Project.");
    }

    public partial class FieldDefinitionFocusAreaTotalProjectReportedExpendiures : FieldDefinition
    {
        private FieldDefinitionFocusAreaTotalProjectReportedExpendiures(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFocusAreaTotalProjectReportedExpendiures Instance = new FieldDefinitionFocusAreaTotalProjectReportedExpendiures(310, @"FocusAreaTotalProjectReportedExpendiures", @"Sum of Project Reported Expendiures", @"Sum of reported expenditures on all Projects within a Focus Area that are in an Implementation, Post-Implementation, or Completed Project Stage.");
    }

    public partial class FieldDefinitionFocusAreaTotalProjectEstimatedTotalCosts : FieldDefinition
    {
        private FieldDefinitionFocusAreaTotalProjectEstimatedTotalCosts(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFocusAreaTotalProjectEstimatedTotalCosts Instance = new FieldDefinitionFocusAreaTotalProjectEstimatedTotalCosts(311, @"FocusAreaTotalProjectEstimatedTotalCosts", @"Sum of Project Estimated Total Costs", @"Sum of estimated total costs on all Projects within a Focus Area that are in an Implementation, Post-Implementation, or Completed Project Stage.");
    }

    public partial class FieldDefinitionFocusAreaTotalCompletedFootprintAcres : FieldDefinition
    {
        private FieldDefinitionFocusAreaTotalCompletedFootprintAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFocusAreaTotalCompletedFootprintAcres Instance = new FieldDefinitionFocusAreaTotalCompletedFootprintAcres(312, @"FocusAreaTotalCompletedFootprintAcres", @"Total Footprint Acres - Completed", @"Sum of Footprint Acres on all completed Treatment Activities under all Projects within a Focus Area that are in an Implementation, Post-Implementation, or Completed Project Stage.");
    }

    public partial class FieldDefinitionFocusAreaTotalPlannedFootprintAcres : FieldDefinition
    {
        private FieldDefinitionFocusAreaTotalPlannedFootprintAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFocusAreaTotalPlannedFootprintAcres Instance = new FieldDefinitionFocusAreaTotalPlannedFootprintAcres(313, @"FocusAreaTotalPlannedFootprintAcres", @"Total Footprint Acres - Planned", @"The value entered in the Planned Footprint Acres field of a Focus Area");
    }

    public partial class FieldDefinitionFocusAreaCloseoutReportProjectList : FieldDefinition
    {
        private FieldDefinitionFocusAreaCloseoutReportProjectList(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFocusAreaCloseoutReportProjectList Instance = new FieldDefinitionFocusAreaCloseoutReportProjectList(314, @"FocusAreaCloseoutReportProjectList", @"Project List", @"All projects within a Focus Area that are in an Implementation, Post-Implementation, or Completed Project Stage.");
    }

    public partial class FieldDefinitionRequestorName : FieldDefinition
    {
        private FieldDefinitionRequestorName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionRequestorName Instance = new FieldDefinitionRequestorName(315, @"RequestorName", @"Requestor Name", @"The name of the person/vendor preparing the invoice requesting payment.");
    }

    public partial class FieldDefinitionInvoiceDate : FieldDefinition
    {
        private FieldDefinitionInvoiceDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceDate Instance = new FieldDefinitionInvoiceDate(316, @"InvoiceDate", @"Invoice Date", @"The date the invoice was submitted.");
    }

    public partial class FieldDefinitionPurchaseAuthority : FieldDefinition
    {
        private FieldDefinitionPurchaseAuthority(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPurchaseAuthority Instance = new FieldDefinitionPurchaseAuthority(317, @"PurchaseAuthority", @"Purchase Authority", @"Typically describes an Agreement Number or that the invoice is part of landowner cost-share agreement.");
    }

    public partial class FieldDefinitionTotalRequestedInvoicePaymentAmount : FieldDefinition
    {
        private FieldDefinitionTotalRequestedInvoicePaymentAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTotalRequestedInvoicePaymentAmount Instance = new FieldDefinitionTotalRequestedInvoicePaymentAmount(318, @"TotalRequestedInvoicePaymentAmount", @"Total Invoice Amount", @"The total amount of funding requested by a given invoice");
    }

    public partial class FieldDefinitionPreparedByPerson : FieldDefinition
    {
        private FieldDefinitionPreparedByPerson(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPreparedByPerson Instance = new FieldDefinitionPreparedByPerson(319, @"PreparedByPerson", @"Prepared By", @"The person preparing the invoice for submission to IPR");
    }

    public partial class FieldDefinitionInvoiceIdentifyingName : FieldDefinition
    {
        private FieldDefinitionInvoiceIdentifyingName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceIdentifyingName Instance = new FieldDefinitionInvoiceIdentifyingName(320, @"InvoiceIdentifyingName", @"Invoice Nickname", @"This name is a nickname to make identification of particular invoices easier.");
    }

    public partial class FieldDefinitionGrantNoteInternal : FieldDefinition
    {
        private FieldDefinitionGrantNoteInternal(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantNoteInternal Instance = new FieldDefinitionGrantNoteInternal(321, @"GrantNoteInternal", @"Internal Grant Note", @"<p>Any additional important information about the grant. These notes are only visible to internal users </p>");
    }

    public partial class FieldDefinitionGrantAllocationNoteInternal : FieldDefinition
    {
        private FieldDefinitionGrantAllocationNoteInternal(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationNoteInternal Instance = new FieldDefinitionGrantAllocationNoteInternal(322, @"GrantAllocationNoteInternal", @"Internal Grant Allocation Note", @"<p>Any additional important information about the grant allocation. These notes are only visible to internal users </p>");
    }

    public partial class FieldDefinitionInvoiceStatus : FieldDefinition
    {
        private FieldDefinitionInvoiceStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceStatus Instance = new FieldDefinitionInvoiceStatus(323, @"InvoiceStatus", @"Invoice Status", @"<p>Any important information about the overall status of an invoice </p>");
    }

    public partial class FieldDefinitionInvoiceApprovalStatus : FieldDefinition
    {
        private FieldDefinitionInvoiceApprovalStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceApprovalStatus Instance = new FieldDefinitionInvoiceApprovalStatus(324, @"InvoiceApprovalStatus", @"Invoice Approval Status", @"<p>Important information about the approval status of an invoice</p>");
    }

    public partial class FieldDefinitionInvoiceApprovalComment : FieldDefinition
    {
        private FieldDefinitionInvoiceApprovalComment(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceApprovalComment Instance = new FieldDefinitionInvoiceApprovalComment(325, @"InvoiceApprovalComment", @"Invoice Approval Comment", @"<p>Important rationale about the approval status of an invoice</p>");
    }

    public partial class FieldDefinitionMatchAmount : FieldDefinition
    {
        private FieldDefinitionMatchAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMatchAmount Instance = new FieldDefinitionMatchAmount(326, @"MatchAmount", @"Match Amount", @"<p>The amount of this invoice matched by another agency</p>");
    }

    public partial class FieldDefinitionVendor : FieldDefinition
    {
        private FieldDefinitionVendor(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionVendor Instance = new FieldDefinitionVendor(327, @"Vendor", @"Vendor", @"Vendor Placeholder");
    }

    public partial class FieldDefinitionEstimatedIndirectCost : FieldDefinition
    {
        private FieldDefinitionEstimatedIndirectCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionEstimatedIndirectCost Instance = new FieldDefinitionEstimatedIndirectCost(328, @"EstimatedIndirectCost", @"Estimated Indirect Cost", @"<p>The estimated indirect cost to complete all stages of project implementation.</p>");
    }

    public partial class FieldDefinitionEstimatedPersonnelAndBenefitsCost : FieldDefinition
    {
        private FieldDefinitionEstimatedPersonnelAndBenefitsCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionEstimatedPersonnelAndBenefitsCost Instance = new FieldDefinitionEstimatedPersonnelAndBenefitsCost(329, @"EstimatedPersonnelAndBenefitsCost", @"Estimated Personnel & Benefits Cost", @"<p>The estimated personnel and benefits costs to complete all stages of project implementation.</p>");
    }

    public partial class FieldDefinitionEstimatedSuppliesCost : FieldDefinition
    {
        private FieldDefinitionEstimatedSuppliesCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionEstimatedSuppliesCost Instance = new FieldDefinitionEstimatedSuppliesCost(330, @"EstimatedSuppliesCost", @"Estimated Supplies Cost", @"<p>The estimated supplies cost to complete all stages of project implementation.</p>");
    }

    public partial class FieldDefinitionEstimatedTravelCost : FieldDefinition
    {
        private FieldDefinitionEstimatedTravelCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionEstimatedTravelCost Instance = new FieldDefinitionEstimatedTravelCost(331, @"EstimatedTravelCost", @"Estimated Travel Cost", @"<p>The estimated travel costs to complete all stages of project implementation.</p>");
    }

    public partial class FieldDefinitionInvoiceID : FieldDefinition
    {
        private FieldDefinitionInvoiceID(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceID Instance = new FieldDefinitionInvoiceID(332, @"InvoiceID", @"Invoice ID", @"<p>The System-unique identifier for a given Invoice.</p>");
    }

    public partial class FieldDefinitionInvoiceLineItem : FieldDefinition
    {
        private FieldDefinitionInvoiceLineItem(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceLineItem Instance = new FieldDefinitionInvoiceLineItem(333, @"InvoiceLineItem", @"Invoice Line Item", @"<p>A line item on an invoice which includes an amount and the associated grant allocation/grant.</p>");
    }

    public partial class FieldDefinitionInteractionEvent : FieldDefinition
    {
        private FieldDefinitionInteractionEvent(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInteractionEvent Instance = new FieldDefinitionInteractionEvent(334, @"InteractionEvent", @"Interaction/Event", @"<p>Placeholder definition for Interaction/Event description.</p>");
    }

    public partial class FieldDefinitionInteractionEventType : FieldDefinition
    {
        private FieldDefinitionInteractionEventType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInteractionEventType Instance = new FieldDefinitionInteractionEventType(335, @"InteractionEventType", @"Interaction/Event Type", @"<p>Placeholder definition for Interaction/Event Type description.</p>");
    }

    public partial class FieldDefinitionDNRStaffPerson : FieldDefinition
    {
        private FieldDefinitionDNRStaffPerson(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionDNRStaffPerson Instance = new FieldDefinitionDNRStaffPerson(336, @"DNRStaffPerson", @"DNR Staff Person", @"<p>Placeholder definition for DNR Staff Person assigned to an Interaction/Event.</p>");
    }

    public partial class FieldDefinitionInteractionEventContact : FieldDefinition
    {
        private FieldDefinitionInteractionEventContact(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInteractionEventContact Instance = new FieldDefinitionInteractionEventContact(337, @"InteractionEventContact", @"Interaction/Event Contact", @"<p>Placeholder definition for Interaction/Event Contact description.</p>");
    }

    public partial class FieldDefinitionInteractionEventProject : FieldDefinition
    {
        private FieldDefinitionInteractionEventProject(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInteractionEventProject Instance = new FieldDefinitionInteractionEventProject(338, @"InteractionEventProject", @"Interaction/Event Project", @"<p>Placeholder definition for Interaction/Event Project description.</p>");
    }

    public partial class FieldDefinitionInteractionEventLocation : FieldDefinition
    {
        private FieldDefinitionInteractionEventLocation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInteractionEventLocation Instance = new FieldDefinitionInteractionEventLocation(339, @"InteractionEventLocation", @"Interaction/Event Location", @"<p>Placeholder definition for Interaction/Event Location description.</p>");
    }

    public partial class FieldDefinitionGrantAllocationName : FieldDefinition
    {
        private FieldDefinitionGrantAllocationName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationName Instance = new FieldDefinitionGrantAllocationName(340, @"GrantAllocationName", @"Grant Allocation Name", @"<p>Placeholder definition for Grant Allocation Name description.</p>");
    }

    public partial class FieldDefinitionDivision : FieldDefinition
    {
        private FieldDefinitionDivision(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionDivision Instance = new FieldDefinitionDivision(341, @"Division", @"Division", @"<p>Placeholder definition for Division description.</p>");
    }

    public partial class FieldDefinitionGrantManager : FieldDefinition
    {
        private FieldDefinitionGrantManager(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantManager Instance = new FieldDefinitionGrantManager(342, @"GrantManager", @"Grant Manager", @"<p>Placeholder definition for Grant Manager description.</p>");
    }

    public partial class FieldDefinitionJob : FieldDefinition
    {
        private FieldDefinitionJob(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionJob Instance = new FieldDefinitionJob(343, @"Job", @"Job", @"<p>A foreground or background system job.</p>");
    }

    public partial class FieldDefinitionJobImportTableType : FieldDefinition
    {
        private FieldDefinitionJobImportTableType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionJobImportTableType Instance = new FieldDefinitionJobImportTableType(344, @"JobImportTableType", @"Job Import Table Type", @"<p>Type of the Job Import table imported.</p>");
    }

    public partial class FieldDefinitionGrantAllocationBudgetLineItem : FieldDefinition
    {
        private FieldDefinitionGrantAllocationBudgetLineItem(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationBudgetLineItem Instance = new FieldDefinitionGrantAllocationBudgetLineItem(345, @"GrantAllocationBudgetLineItem", @"Grant Allocation Budget Line Item", @"<p>Grant Allocation budget broken across line items</p>");
    }
}