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
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public abstract partial class FieldDefinition : IHavePrimaryKey
    {
        public static readonly FieldDefinitionProjectType ProjectType = FieldDefinitionProjectType.Instance;
        public static readonly FieldDefinitionTaxonomyTrunk TaxonomyTrunk = FieldDefinitionTaxonomyTrunk.Instance;
        public static readonly FieldDefinitionPrimaryContactOrganization PrimaryContactOrganization = FieldDefinitionPrimaryContactOrganization.Instance;
        public static readonly FieldDefinitionProjectsStewardOrganizationRelationshipToProject ProjectsStewardOrganizationRelationshipToProject = FieldDefinitionProjectsStewardOrganizationRelationshipToProject.Instance;
        public static readonly FieldDefinitionOrganization Organization = FieldDefinitionOrganization.Instance;
        public static readonly FieldDefinitionPassword Password = FieldDefinitionPassword.Instance;
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
        public static readonly FieldDefinitionProjectInitiationDate ProjectInitiationDate = FieldDefinitionProjectInitiationDate.Instance;
        public static readonly FieldDefinitionAssociatedTaxonomyBranches AssociatedTaxonomyBranches = FieldDefinitionAssociatedTaxonomyBranches.Instance;
        public static readonly FieldDefinitionExternalLinks ExternalLinks = FieldDefinitionExternalLinks.Instance;
        public static readonly FieldDefinitionCurrentYearForPVCalculations CurrentYearForPVCalculations = FieldDefinitionCurrentYearForPVCalculations.Instance;
        public static readonly FieldDefinitionLifecycleOperatingCost LifecycleOperatingCost = FieldDefinitionLifecycleOperatingCost.Instance;
        public static readonly FieldDefinitionRoleName RoleName = FieldDefinitionRoleName.Instance;
        public static readonly FieldDefinitionDNRUplandRegion DNRUplandRegion = FieldDefinitionDNRUplandRegion.Instance;
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
        public static readonly FieldDefinitionProjectUpdateKickOffDate ProjectUpdateKickOffDate = FieldDefinitionProjectUpdateKickOffDate.Instance;
        public static readonly FieldDefinitionProjectUpdateReminderInterval ProjectUpdateReminderInterval = FieldDefinitionProjectUpdateReminderInterval.Instance;
        public static readonly FieldDefinitionProjectUpdateCloseOutDate ProjectUpdateCloseOutDate = FieldDefinitionProjectUpdateCloseOutDate.Instance;
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
        public static readonly FieldDefinitionPriorityLandscape PriorityLandscape = FieldDefinitionPriorityLandscape.Instance;
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
        public static readonly FieldDefinitionPaymentAmount PaymentAmount = FieldDefinitionPaymentAmount.Instance;
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
        public static readonly FieldDefinitionGrantModificationPurpose GrantModificationPurpose = FieldDefinitionGrantModificationPurpose.Instance;
        public static readonly FieldDefinitionGrantModificationStatus GrantModificationStatus = FieldDefinitionGrantModificationStatus.Instance;
        public static readonly FieldDefinitionGrantModificationAmount GrantModificationAmount = FieldDefinitionGrantModificationAmount.Instance;
        public static readonly FieldDefinitionGrantModificationDescription GrantModificationDescription = FieldDefinitionGrantModificationDescription.Instance;
        public static readonly FieldDefinitionGrantModificationStartDate GrantModificationStartDate = FieldDefinitionGrantModificationStartDate.Instance;
        public static readonly FieldDefinitionGrantModificationEndDate GrantModificationEndDate = FieldDefinitionGrantModificationEndDate.Instance;
        public static readonly FieldDefinitionGrantModificationName GrantModificationName = FieldDefinitionGrantModificationName.Instance;
        public static readonly FieldDefinitionGrantModification GrantModification = FieldDefinitionGrantModification.Instance;
        public static readonly FieldDefinitionGrantModificationNoteInternal GrantModificationNoteInternal = FieldDefinitionGrantModificationNoteInternal.Instance;
        public static readonly FieldDefinitionProgramIndexProjectCode ProgramIndexProjectCode = FieldDefinitionProgramIndexProjectCode.Instance;
        public static readonly FieldDefinitionFhtProjectNumber FhtProjectNumber = FieldDefinitionFhtProjectNumber.Instance;
        public static readonly FieldDefinitionGrantCurrentBalance GrantCurrentBalance = FieldDefinitionGrantCurrentBalance.Instance;
        public static readonly FieldDefinitionGrantAllocationCurrentBalance GrantAllocationCurrentBalance = FieldDefinitionGrantAllocationCurrentBalance.Instance;
        public static readonly FieldDefinitionGrantAllocationChangeLogNote GrantAllocationChangeLogNote = FieldDefinitionGrantAllocationChangeLogNote.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareStartDate GrantAllocationAwardLandownerCostShareStartDate = FieldDefinitionGrantAllocationAwardLandownerCostShareStartDate.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareEndDate GrantAllocationAwardLandownerCostShareEndDate = FieldDefinitionGrantAllocationAwardLandownerCostShareEndDate.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareFootprintAcres GrantAllocationAwardLandownerCostShareFootprintAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareFootprintAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareChippingAcres GrantAllocationAwardLandownerCostShareChippingAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareChippingAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostSharePruningAcres GrantAllocationAwardLandownerCostSharePruningAcres = FieldDefinitionGrantAllocationAwardLandownerCostSharePruningAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareThinningAcres GrantAllocationAwardLandownerCostShareThinningAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareThinningAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareMasticationAcres GrantAllocationAwardLandownerCostShareMasticationAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareMasticationAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareGrazingAcres GrantAllocationAwardLandownerCostShareGrazingAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareGrazingAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareLopAndScatterAcres GrantAllocationAwardLandownerCostShareLopAndScatterAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareLopAndScatterAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareBiomassRemovalAcres GrantAllocationAwardLandownerCostShareBiomassRemovalAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareBiomassRemovalAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileAcres GrantAllocationAwardLandownerCostShareHandPileAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareBroadcastBurnAcres GrantAllocationAwardLandownerCostShareBroadcastBurnAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareBroadcastBurnAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileBurnAcres GrantAllocationAwardLandownerCostShareHandPileBurnAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileBurnAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareMachinePileBurnAcres GrantAllocationAwardLandownerCostShareMachinePileBurnAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareMachinePileBurnAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareOtherTreatmentAcres GrantAllocationAwardLandownerCostShareOtherTreatmentAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareOtherTreatmentAcres.Instance;
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareSlashAcres GrantAllocationAwardLandownerCostShareSlashAcres = FieldDefinitionGrantAllocationAwardLandownerCostShareSlashAcres.Instance;
        public static readonly FieldDefinitionProjectIdentifier ProjectIdentifier = FieldDefinitionProjectIdentifier.Instance;
        public static readonly FieldDefinitionPlannedDate PlannedDate = FieldDefinitionPlannedDate.Instance;
        public static readonly FieldDefinitionTreatedAcres TreatedAcres = FieldDefinitionTreatedAcres.Instance;
        public static readonly FieldDefinitionTreatmentType TreatmentType = FieldDefinitionTreatmentType.Instance;
        public static readonly FieldDefinitionTreatmentDetailedActivityType TreatmentDetailedActivityType = FieldDefinitionTreatmentDetailedActivityType.Instance;
        public static readonly FieldDefinitionFootprintAcres FootprintAcres = FieldDefinitionFootprintAcres.Instance;
        public static readonly FieldDefinitionFundingSource FundingSource = FieldDefinitionFundingSource.Instance;
        public static readonly FieldDefinitionFundingSourceNote FundingSourceNote = FieldDefinitionFundingSourceNote.Instance;
        public static readonly FieldDefinitionProjectTotalCompletedTreatmentAcres ProjectTotalCompletedTreatmentAcres = FieldDefinitionProjectTotalCompletedTreatmentAcres.Instance;
        public static readonly FieldDefinitionLimitVisibilityToAdmin LimitVisibilityToAdmin = FieldDefinitionLimitVisibilityToAdmin.Instance;
        public static readonly FieldDefinitionProgram Program = FieldDefinitionProgram.Instance;
        public static readonly FieldDefinitionProjectGrantAllocationRequestMatchAmount ProjectGrantAllocationRequestMatchAmount = FieldDefinitionProjectGrantAllocationRequestMatchAmount.Instance;
        public static readonly FieldDefinitionProjectGrantAllocationRequestPayAmount ProjectGrantAllocationRequestPayAmount = FieldDefinitionProjectGrantAllocationRequestPayAmount.Instance;
        public static readonly FieldDefinitionProjectApplicationDate ProjectApplicationDate = FieldDefinitionProjectApplicationDate.Instance;
        public static readonly FieldDefinitionProjectDecisionDate ProjectDecisionDate = FieldDefinitionProjectDecisionDate.Instance;
        public static readonly FieldDefinitionServiceForester ServiceForester = FieldDefinitionServiceForester.Instance;
        public static readonly FieldDefinitionStewardshipFishAndWildlifeBiologist StewardshipFishAndWildlifeBiologist = FieldDefinitionStewardshipFishAndWildlifeBiologist.Instance;
        public static readonly FieldDefinitionRegulationAssistanceForester RegulationAssistanceForester = FieldDefinitionRegulationAssistanceForester.Instance;
        public static readonly FieldDefinitionFamilyForestFishPassageProgram FamilyForestFishPassageProgram = FieldDefinitionFamilyForestFishPassageProgram.Instance;
        public static readonly FieldDefinitionForestryRiparianEasementProgram ForestryRiparianEasementProgram = FieldDefinitionForestryRiparianEasementProgram.Instance;
        public static readonly FieldDefinitionRiversAndHabitatOpenSpaceProgramManager RiversAndHabitatOpenSpaceProgramManager = FieldDefinitionRiversAndHabitatOpenSpaceProgramManager.Instance;
        public static readonly FieldDefinitionCommunityResilienceCoordinator CommunityResilienceCoordinator = FieldDefinitionCommunityResilienceCoordinator.Instance;
        public static readonly FieldDefinitionUrbanForestryTechnician UrbanForestryTechnician = FieldDefinitionUrbanForestryTechnician.Instance;
        public static readonly FieldDefinitionForestPracticesForester ForestPracticesForester = FieldDefinitionForestPracticesForester.Instance;
        public static readonly FieldDefinitionSmallForestLandownerOfficeProgramManager SmallForestLandownerOfficeProgramManager = FieldDefinitionSmallForestLandownerOfficeProgramManager.Instance;
        public static readonly FieldDefinitionSmallForestLandownerProgramManager SmallForestLandownerProgramManager = FieldDefinitionSmallForestLandownerProgramManager.Instance;
        public static readonly FieldDefinitionUcfStatewideSpecialist UcfStatewideSpecialist = FieldDefinitionUcfStatewideSpecialist.Instance;
        public static readonly FieldDefinitionServiceForestrySpecialist ServiceForestrySpecialist = FieldDefinitionServiceForestrySpecialist.Instance;
        public static readonly FieldDefinitionExternalMapLayer ExternalMapLayer = FieldDefinitionExternalMapLayer.Instance;
        public static readonly FieldDefinitionExternalMapLayerDisplayName ExternalMapLayerDisplayName = FieldDefinitionExternalMapLayerDisplayName.Instance;
        public static readonly FieldDefinitionExternalMapLayerUrl ExternalMapLayerUrl = FieldDefinitionExternalMapLayerUrl.Instance;
        public static readonly FieldDefinitionExternalMapLayerDescription ExternalMapLayerDescription = FieldDefinitionExternalMapLayerDescription.Instance;
        public static readonly FieldDefinitionExternalMapLayerFeatureNameField ExternalMapLayerFeatureNameField = FieldDefinitionExternalMapLayerFeatureNameField.Instance;
        public static readonly FieldDefinitionExternalMapLayerDisplayOnPriorityLandscape ExternalMapLayerDisplayOnPriorityLandscape = FieldDefinitionExternalMapLayerDisplayOnPriorityLandscape.Instance;
        public static readonly FieldDefinitionExternalMapLayerDisplayOnProjectMap ExternalMapLayerDisplayOnProjectMap = FieldDefinitionExternalMapLayerDisplayOnProjectMap.Instance;
        public static readonly FieldDefinitionExternalMapLayerDisplayOnAllOthers ExternalMapLayerDisplayOnAllOthers = FieldDefinitionExternalMapLayerDisplayOnAllOthers.Instance;
        public static readonly FieldDefinitionExternalMapLayerIsATiledMapService ExternalMapLayerIsATiledMapService = FieldDefinitionExternalMapLayerIsATiledMapService.Instance;
        public static readonly FieldDefinitionExternalMapLayerIsActive ExternalMapLayerIsActive = FieldDefinitionExternalMapLayerIsActive.Instance;
        public static readonly FieldDefinitionUpdatesFromImport UpdatesFromImport = FieldDefinitionUpdatesFromImport.Instance;
        public static readonly FieldDefinitionTreatmentCode TreatmentCode = FieldDefinitionTreatmentCode.Instance;
        public static readonly FieldDefinitionTreatmentCostPerAcre TreatmentCostPerAcre = FieldDefinitionTreatmentCostPerAcre.Instance;
        public static readonly FieldDefinitionTreatmentTotalCost TreatmentTotalCost = FieldDefinitionTreatmentTotalCost.Instance;
        public static readonly FieldDefinitionServiceForestryRegionalCoordinator ServiceForestryRegionalCoordinator = FieldDefinitionServiceForestryRegionalCoordinator.Instance;
        public static readonly FieldDefinitionCounty County = FieldDefinitionCounty.Instance;
        public static readonly FieldDefinitionPercentageMatch PercentageMatch = FieldDefinitionPercentageMatch.Instance;
        public static readonly FieldDefinitionReportTitle ReportTitle = FieldDefinitionReportTitle.Instance;
        public static readonly FieldDefinitionReportDescription ReportDescription = FieldDefinitionReportDescription.Instance;
        public static readonly FieldDefinitionReportFile ReportFile = FieldDefinitionReportFile.Instance;
        public static readonly FieldDefinitionReportModel ReportModel = FieldDefinitionReportModel.Instance;
        public static readonly FieldDefinitionSelectedReportTemplate SelectedReportTemplate = FieldDefinitionSelectedReportTemplate.Instance;
        public static readonly FieldDefinitionInvoicePaymentRequest InvoicePaymentRequest = FieldDefinitionInvoicePaymentRequest.Instance;
        public static readonly FieldDefinitionDUNS DUNS = FieldDefinitionDUNS.Instance;
        public static readonly FieldDefinitionOrganizationCode OrganizationCode = FieldDefinitionOrganizationCode.Instance;
        public static readonly FieldDefinitionInvoiceNumber InvoiceNumber = FieldDefinitionInvoiceNumber.Instance;
        public static readonly FieldDefinitionFund Fund = FieldDefinitionFund.Instance;
        public static readonly FieldDefinitionAppn Appn = FieldDefinitionAppn.Instance;
        public static readonly FieldDefinitionSubObject SubObject = FieldDefinitionSubObject.Instance;
        public static readonly FieldDefinitionServiceForestryProgramManager ServiceForestryProgramManager = FieldDefinitionServiceForestryProgramManager.Instance;
        public static readonly FieldDefinitionForestRegulationFishAndWildlifeBiologist ForestRegulationFishAndWildlifeBiologist = FieldDefinitionForestRegulationFishAndWildlifeBiologist.Instance;
        public static readonly FieldDefinitionGrantAllocationFundFSPs GrantAllocationFundFSPs = FieldDefinitionGrantAllocationFundFSPs.Instance;
        public static readonly FieldDefinitionGrantAllocationSource GrantAllocationSource = FieldDefinitionGrantAllocationSource.Instance;
        public static readonly FieldDefinitionGrantAllocationAllocation GrantAllocationAllocation = FieldDefinitionGrantAllocationAllocation.Instance;
        public static readonly FieldDefinitionGrantAllocationTotalGrantFunds GrantAllocationTotalGrantFunds = FieldDefinitionGrantAllocationTotalGrantFunds.Instance;
        public static readonly FieldDefinitionGrantAllocationOverallBalance GrantAllocationOverallBalance = FieldDefinitionGrantAllocationOverallBalance.Instance;
        public static readonly FieldDefinitionGrantAllocationContractualBalance GrantAllocationContractualBalance = FieldDefinitionGrantAllocationContractualBalance.Instance;
        public static readonly FieldDefinitionGrantAllocationTravelBalance GrantAllocationTravelBalance = FieldDefinitionGrantAllocationTravelBalance.Instance;
        public static readonly FieldDefinitionGrantAllocationStaffBalance GrantAllocationStaffBalance = FieldDefinitionGrantAllocationStaffBalance.Instance;
        public static readonly FieldDefinitionGrantAllocationLikelyToUse GrantAllocationLikelyToUse = FieldDefinitionGrantAllocationLikelyToUse.Instance;
        public static readonly FieldDefinitionGrantAllocationCompleted GrantAllocationCompleted = FieldDefinitionGrantAllocationCompleted.Instance;
        public static readonly FieldDefinitionGrantAllocationPriority GrantAllocationPriority = FieldDefinitionGrantAllocationPriority.Instance;
        public static readonly FieldDefinitionLeadImplementerOrganization LeadImplementerOrganization = FieldDefinitionLeadImplementerOrganization.Instance;

        public static readonly List<FieldDefinition> All;
        public static readonly ReadOnlyDictionary<int, FieldDefinition> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FieldDefinition()
        {
            All = new List<FieldDefinition> { ProjectType, TaxonomyTrunk, PrimaryContactOrganization, ProjectsStewardOrganizationRelationshipToProject, Organization, Password, MeasurementUnit, PhotoCaption, PhotoCredit, PhotoTiming, OrganizationPrimaryContact, TaxonomyBranch, CompletionDate, ProjectDescription, ProjectName, ProjectNote, ExpirationDate, ReportedValue, OrganizationType, ProjectGrantAllocationRequestTotalAmount, ProjectStage, ClassificationName, EstimatedTotalCost, UnfundedNeed, Username, Project, Classification, FundedAmount, ProjectLocation, ExcludeFromFactSheet, ProjectCostInYearOfExpenditure, GlobalInflationRate, ReportingYear, TagName, TagDescription, ReportedExpenditure, Application, ProjectInitiationDate, AssociatedTaxonomyBranches, ExternalLinks, CurrentYearForPVCalculations, LifecycleOperatingCost, RoleName, DNRUplandRegion, MonitoringProgram, MonitoringApproach, MonitoringProgramPartner, MonitoringProgramUrl, ClassificationDescription, ClassificationGoalStatement, ClassificationNarrative, TaxonomySystemName, ProjectTypeDisplayNameForProject, ProjectRelationshipType, ProjectSteward, ChartLastUpdatedDate, UnsecuredFunding, ProjectStewardOrganizationDisplayName, ClassificationSystem, ClassificationSystemName, ProjectPrimaryContact, CustomPageDisplayType, TaxonomyTrunkDescription, TaxonomyBranchDescription, ProjectTypeDescription, ShowApplicationsToThePublic, ShowLeadImplementerLogoOnFactSheet, ProjectUpdateKickOffDate, ProjectUpdateReminderInterval, ProjectUpdateCloseOutDate, GrantAllocationAmount, NormalUser, ProjectStewardshipArea, ProjectInternalNote, StatewideVendorNumber, Contact, ContactRelationshipType, Contractor, Landowner, Partner, PrimaryContact, FocusArea, Grant, GrantAllocation, CostType, ProjectCode, GrantAllocationProjectCode, ProgramIndex, GrantName, GrantShortName, GrantStatus, GrantType, GrantNumber, CFDA, TotalAwardAmount, GrantStartDate, GrantEndDate, GrantNote, PriorityLandscape, Invoice, Agreement, FederalFundCode, AllocationAmount, AgreementType, AgreementNumber, AgreementTitle, AgreementStartDate, AgreementEndDate, AgreementAmount, ProgramManager, AgreementNotes, AgreementStatus, GrantAllocationNote, FileResource, ProjectTotalCompletedFootprintAcres, FocusAreaTotalProjectReportedExpendiures, FocusAreaTotalProjectEstimatedTotalCosts, FocusAreaTotalCompletedFootprintAcres, FocusAreaTotalPlannedFootprintAcres, FocusAreaCloseoutReportProjectList, RequestorName, InvoiceDate, PurchaseAuthority, PaymentAmount, PreparedByPerson, InvoiceIdentifyingName, GrantNoteInternal, GrantAllocationNoteInternal, InvoiceStatus, InvoiceApprovalStatus, InvoiceApprovalComment, MatchAmount, Vendor, EstimatedIndirectCost, EstimatedPersonnelAndBenefitsCost, EstimatedSuppliesCost, EstimatedTravelCost, InvoiceID, InvoiceLineItem, InteractionEvent, InteractionEventType, DNRStaffPerson, InteractionEventContact, InteractionEventProject, InteractionEventLocation, GrantAllocationName, Division, GrantManager, Job, JobImportTableType, GrantAllocationBudgetLineItem, GrantModificationPurpose, GrantModificationStatus, GrantModificationAmount, GrantModificationDescription, GrantModificationStartDate, GrantModificationEndDate, GrantModificationName, GrantModification, GrantModificationNoteInternal, ProgramIndexProjectCode, FhtProjectNumber, GrantCurrentBalance, GrantAllocationCurrentBalance, GrantAllocationChangeLogNote, GrantAllocationAwardLandownerCostShareStartDate, GrantAllocationAwardLandownerCostShareEndDate, GrantAllocationAwardLandownerCostShareFootprintAcres, GrantAllocationAwardLandownerCostShareChippingAcres, GrantAllocationAwardLandownerCostSharePruningAcres, GrantAllocationAwardLandownerCostShareThinningAcres, GrantAllocationAwardLandownerCostShareMasticationAcres, GrantAllocationAwardLandownerCostShareGrazingAcres, GrantAllocationAwardLandownerCostShareLopAndScatterAcres, GrantAllocationAwardLandownerCostShareBiomassRemovalAcres, GrantAllocationAwardLandownerCostShareHandPileAcres, GrantAllocationAwardLandownerCostShareBroadcastBurnAcres, GrantAllocationAwardLandownerCostShareHandPileBurnAcres, GrantAllocationAwardLandownerCostShareMachinePileBurnAcres, GrantAllocationAwardLandownerCostShareOtherTreatmentAcres, GrantAllocationAwardLandownerCostShareSlashAcres, ProjectIdentifier, PlannedDate, TreatedAcres, TreatmentType, TreatmentDetailedActivityType, FootprintAcres, FundingSource, FundingSourceNote, ProjectTotalCompletedTreatmentAcres, LimitVisibilityToAdmin, Program, ProjectGrantAllocationRequestMatchAmount, ProjectGrantAllocationRequestPayAmount, ProjectApplicationDate, ProjectDecisionDate, ServiceForester, StewardshipFishAndWildlifeBiologist, RegulationAssistanceForester, FamilyForestFishPassageProgram, ForestryRiparianEasementProgram, RiversAndHabitatOpenSpaceProgramManager, CommunityResilienceCoordinator, UrbanForestryTechnician, ForestPracticesForester, SmallForestLandownerOfficeProgramManager, SmallForestLandownerProgramManager, UcfStatewideSpecialist, ServiceForestrySpecialist, ExternalMapLayer, ExternalMapLayerDisplayName, ExternalMapLayerUrl, ExternalMapLayerDescription, ExternalMapLayerFeatureNameField, ExternalMapLayerDisplayOnPriorityLandscape, ExternalMapLayerDisplayOnProjectMap, ExternalMapLayerDisplayOnAllOthers, ExternalMapLayerIsATiledMapService, ExternalMapLayerIsActive, UpdatesFromImport, TreatmentCode, TreatmentCostPerAcre, TreatmentTotalCost, ServiceForestryRegionalCoordinator, County, PercentageMatch, ReportTitle, ReportDescription, ReportFile, ReportModel, SelectedReportTemplate, InvoicePaymentRequest, DUNS, OrganizationCode, InvoiceNumber, Fund, Appn, SubObject, ServiceForestryProgramManager, ForestRegulationFishAndWildlifeBiologist, GrantAllocationFundFSPs, GrantAllocationSource, GrantAllocationAllocation, GrantAllocationTotalGrantFunds, GrantAllocationOverallBalance, GrantAllocationContractualBalance, GrantAllocationTravelBalance, GrantAllocationStaffBalance, GrantAllocationLikelyToUse, GrantAllocationCompleted, GrantAllocationPriority, LeadImplementerOrganization };
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
                case FieldDefinitionEnum.Appn:
                    return Appn;
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
                case FieldDefinitionEnum.CommunityResilienceCoordinator:
                    return CommunityResilienceCoordinator;
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
                case FieldDefinitionEnum.County:
                    return County;
                case FieldDefinitionEnum.CurrentYearForPVCalculations:
                    return CurrentYearForPVCalculations;
                case FieldDefinitionEnum.CustomPageDisplayType:
                    return CustomPageDisplayType;
                case FieldDefinitionEnum.Division:
                    return Division;
                case FieldDefinitionEnum.DNRStaffPerson:
                    return DNRStaffPerson;
                case FieldDefinitionEnum.DNRUplandRegion:
                    return DNRUplandRegion;
                case FieldDefinitionEnum.DUNS:
                    return DUNS;
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
                case FieldDefinitionEnum.ExpirationDate:
                    return ExpirationDate;
                case FieldDefinitionEnum.ExternalLinks:
                    return ExternalLinks;
                case FieldDefinitionEnum.ExternalMapLayer:
                    return ExternalMapLayer;
                case FieldDefinitionEnum.ExternalMapLayerDescription:
                    return ExternalMapLayerDescription;
                case FieldDefinitionEnum.ExternalMapLayerDisplayName:
                    return ExternalMapLayerDisplayName;
                case FieldDefinitionEnum.ExternalMapLayerDisplayOnAllOthers:
                    return ExternalMapLayerDisplayOnAllOthers;
                case FieldDefinitionEnum.ExternalMapLayerDisplayOnPriorityLandscape:
                    return ExternalMapLayerDisplayOnPriorityLandscape;
                case FieldDefinitionEnum.ExternalMapLayerDisplayOnProjectMap:
                    return ExternalMapLayerDisplayOnProjectMap;
                case FieldDefinitionEnum.ExternalMapLayerFeatureNameField:
                    return ExternalMapLayerFeatureNameField;
                case FieldDefinitionEnum.ExternalMapLayerIsActive:
                    return ExternalMapLayerIsActive;
                case FieldDefinitionEnum.ExternalMapLayerIsATiledMapService:
                    return ExternalMapLayerIsATiledMapService;
                case FieldDefinitionEnum.ExternalMapLayerUrl:
                    return ExternalMapLayerUrl;
                case FieldDefinitionEnum.FamilyForestFishPassageProgram:
                    return FamilyForestFishPassageProgram;
                case FieldDefinitionEnum.FederalFundCode:
                    return FederalFundCode;
                case FieldDefinitionEnum.FhtProjectNumber:
                    return FhtProjectNumber;
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
                case FieldDefinitionEnum.FootprintAcres:
                    return FootprintAcres;
                case FieldDefinitionEnum.ForestPracticesForester:
                    return ForestPracticesForester;
                case FieldDefinitionEnum.ForestRegulationFishAndWildlifeBiologist:
                    return ForestRegulationFishAndWildlifeBiologist;
                case FieldDefinitionEnum.ForestryRiparianEasementProgram:
                    return ForestryRiparianEasementProgram;
                case FieldDefinitionEnum.Fund:
                    return Fund;
                case FieldDefinitionEnum.FundedAmount:
                    return FundedAmount;
                case FieldDefinitionEnum.FundingSource:
                    return FundingSource;
                case FieldDefinitionEnum.FundingSourceNote:
                    return FundingSourceNote;
                case FieldDefinitionEnum.GlobalInflationRate:
                    return GlobalInflationRate;
                case FieldDefinitionEnum.Grant:
                    return Grant;
                case FieldDefinitionEnum.GrantAllocation:
                    return GrantAllocation;
                case FieldDefinitionEnum.GrantAllocationAllocation:
                    return GrantAllocationAllocation;
                case FieldDefinitionEnum.GrantAllocationAmount:
                    return GrantAllocationAmount;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareBiomassRemovalAcres:
                    return GrantAllocationAwardLandownerCostShareBiomassRemovalAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareBroadcastBurnAcres:
                    return GrantAllocationAwardLandownerCostShareBroadcastBurnAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareChippingAcres:
                    return GrantAllocationAwardLandownerCostShareChippingAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareEndDate:
                    return GrantAllocationAwardLandownerCostShareEndDate;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareFootprintAcres:
                    return GrantAllocationAwardLandownerCostShareFootprintAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareGrazingAcres:
                    return GrantAllocationAwardLandownerCostShareGrazingAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareHandPileAcres:
                    return GrantAllocationAwardLandownerCostShareHandPileAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareHandPileBurnAcres:
                    return GrantAllocationAwardLandownerCostShareHandPileBurnAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareLopAndScatterAcres:
                    return GrantAllocationAwardLandownerCostShareLopAndScatterAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareMachinePileBurnAcres:
                    return GrantAllocationAwardLandownerCostShareMachinePileBurnAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareMasticationAcres:
                    return GrantAllocationAwardLandownerCostShareMasticationAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareOtherTreatmentAcres:
                    return GrantAllocationAwardLandownerCostShareOtherTreatmentAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostSharePruningAcres:
                    return GrantAllocationAwardLandownerCostSharePruningAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareSlashAcres:
                    return GrantAllocationAwardLandownerCostShareSlashAcres;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareStartDate:
                    return GrantAllocationAwardLandownerCostShareStartDate;
                case FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareThinningAcres:
                    return GrantAllocationAwardLandownerCostShareThinningAcres;
                case FieldDefinitionEnum.GrantAllocationBudgetLineItem:
                    return GrantAllocationBudgetLineItem;
                case FieldDefinitionEnum.GrantAllocationChangeLogNote:
                    return GrantAllocationChangeLogNote;
                case FieldDefinitionEnum.GrantAllocationCompleted:
                    return GrantAllocationCompleted;
                case FieldDefinitionEnum.GrantAllocationContractualBalance:
                    return GrantAllocationContractualBalance;
                case FieldDefinitionEnum.GrantAllocationCurrentBalance:
                    return GrantAllocationCurrentBalance;
                case FieldDefinitionEnum.GrantAllocationFundFSPs:
                    return GrantAllocationFundFSPs;
                case FieldDefinitionEnum.GrantAllocationLikelyToUse:
                    return GrantAllocationLikelyToUse;
                case FieldDefinitionEnum.GrantAllocationName:
                    return GrantAllocationName;
                case FieldDefinitionEnum.GrantAllocationNote:
                    return GrantAllocationNote;
                case FieldDefinitionEnum.GrantAllocationNoteInternal:
                    return GrantAllocationNoteInternal;
                case FieldDefinitionEnum.GrantAllocationOverallBalance:
                    return GrantAllocationOverallBalance;
                case FieldDefinitionEnum.GrantAllocationPriority:
                    return GrantAllocationPriority;
                case FieldDefinitionEnum.GrantAllocationProjectCode:
                    return GrantAllocationProjectCode;
                case FieldDefinitionEnum.GrantAllocationSource:
                    return GrantAllocationSource;
                case FieldDefinitionEnum.GrantAllocationStaffBalance:
                    return GrantAllocationStaffBalance;
                case FieldDefinitionEnum.GrantAllocationTotalGrantFunds:
                    return GrantAllocationTotalGrantFunds;
                case FieldDefinitionEnum.GrantAllocationTravelBalance:
                    return GrantAllocationTravelBalance;
                case FieldDefinitionEnum.GrantCurrentBalance:
                    return GrantCurrentBalance;
                case FieldDefinitionEnum.GrantEndDate:
                    return GrantEndDate;
                case FieldDefinitionEnum.GrantManager:
                    return GrantManager;
                case FieldDefinitionEnum.GrantModification:
                    return GrantModification;
                case FieldDefinitionEnum.GrantModificationAmount:
                    return GrantModificationAmount;
                case FieldDefinitionEnum.GrantModificationDescription:
                    return GrantModificationDescription;
                case FieldDefinitionEnum.GrantModificationEndDate:
                    return GrantModificationEndDate;
                case FieldDefinitionEnum.GrantModificationName:
                    return GrantModificationName;
                case FieldDefinitionEnum.GrantModificationNoteInternal:
                    return GrantModificationNoteInternal;
                case FieldDefinitionEnum.GrantModificationPurpose:
                    return GrantModificationPurpose;
                case FieldDefinitionEnum.GrantModificationStartDate:
                    return GrantModificationStartDate;
                case FieldDefinitionEnum.GrantModificationStatus:
                    return GrantModificationStatus;
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
                case FieldDefinitionEnum.InvoiceNumber:
                    return InvoiceNumber;
                case FieldDefinitionEnum.InvoicePaymentRequest:
                    return InvoicePaymentRequest;
                case FieldDefinitionEnum.InvoiceStatus:
                    return InvoiceStatus;
                case FieldDefinitionEnum.Job:
                    return Job;
                case FieldDefinitionEnum.JobImportTableType:
                    return JobImportTableType;
                case FieldDefinitionEnum.Landowner:
                    return Landowner;
                case FieldDefinitionEnum.LeadImplementerOrganization:
                    return LeadImplementerOrganization;
                case FieldDefinitionEnum.LifecycleOperatingCost:
                    return LifecycleOperatingCost;
                case FieldDefinitionEnum.LimitVisibilityToAdmin:
                    return LimitVisibilityToAdmin;
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
                case FieldDefinitionEnum.OrganizationCode:
                    return OrganizationCode;
                case FieldDefinitionEnum.OrganizationPrimaryContact:
                    return OrganizationPrimaryContact;
                case FieldDefinitionEnum.OrganizationType:
                    return OrganizationType;
                case FieldDefinitionEnum.Partner:
                    return Partner;
                case FieldDefinitionEnum.Password:
                    return Password;
                case FieldDefinitionEnum.PaymentAmount:
                    return PaymentAmount;
                case FieldDefinitionEnum.PercentageMatch:
                    return PercentageMatch;
                case FieldDefinitionEnum.PhotoCaption:
                    return PhotoCaption;
                case FieldDefinitionEnum.PhotoCredit:
                    return PhotoCredit;
                case FieldDefinitionEnum.PhotoTiming:
                    return PhotoTiming;
                case FieldDefinitionEnum.PlannedDate:
                    return PlannedDate;
                case FieldDefinitionEnum.PreparedByPerson:
                    return PreparedByPerson;
                case FieldDefinitionEnum.PrimaryContact:
                    return PrimaryContact;
                case FieldDefinitionEnum.PrimaryContactOrganization:
                    return PrimaryContactOrganization;
                case FieldDefinitionEnum.PriorityLandscape:
                    return PriorityLandscape;
                case FieldDefinitionEnum.Program:
                    return Program;
                case FieldDefinitionEnum.ProgramIndex:
                    return ProgramIndex;
                case FieldDefinitionEnum.ProgramIndexProjectCode:
                    return ProgramIndexProjectCode;
                case FieldDefinitionEnum.ProgramManager:
                    return ProgramManager;
                case FieldDefinitionEnum.Project:
                    return Project;
                case FieldDefinitionEnum.ProjectApplicationDate:
                    return ProjectApplicationDate;
                case FieldDefinitionEnum.ProjectCode:
                    return ProjectCode;
                case FieldDefinitionEnum.ProjectCostInYearOfExpenditure:
                    return ProjectCostInYearOfExpenditure;
                case FieldDefinitionEnum.ProjectDecisionDate:
                    return ProjectDecisionDate;
                case FieldDefinitionEnum.ProjectDescription:
                    return ProjectDescription;
                case FieldDefinitionEnum.ProjectGrantAllocationRequestMatchAmount:
                    return ProjectGrantAllocationRequestMatchAmount;
                case FieldDefinitionEnum.ProjectGrantAllocationRequestPayAmount:
                    return ProjectGrantAllocationRequestPayAmount;
                case FieldDefinitionEnum.ProjectGrantAllocationRequestTotalAmount:
                    return ProjectGrantAllocationRequestTotalAmount;
                case FieldDefinitionEnum.ProjectIdentifier:
                    return ProjectIdentifier;
                case FieldDefinitionEnum.ProjectInitiationDate:
                    return ProjectInitiationDate;
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
                case FieldDefinitionEnum.ProjectTotalCompletedTreatmentAcres:
                    return ProjectTotalCompletedTreatmentAcres;
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
                case FieldDefinitionEnum.RegulationAssistanceForester:
                    return RegulationAssistanceForester;
                case FieldDefinitionEnum.ReportDescription:
                    return ReportDescription;
                case FieldDefinitionEnum.ReportedExpenditure:
                    return ReportedExpenditure;
                case FieldDefinitionEnum.ReportedValue:
                    return ReportedValue;
                case FieldDefinitionEnum.ReportFile:
                    return ReportFile;
                case FieldDefinitionEnum.ReportingYear:
                    return ReportingYear;
                case FieldDefinitionEnum.ReportModel:
                    return ReportModel;
                case FieldDefinitionEnum.ReportTitle:
                    return ReportTitle;
                case FieldDefinitionEnum.RequestorName:
                    return RequestorName;
                case FieldDefinitionEnum.RiversAndHabitatOpenSpaceProgramManager:
                    return RiversAndHabitatOpenSpaceProgramManager;
                case FieldDefinitionEnum.RoleName:
                    return RoleName;
                case FieldDefinitionEnum.SelectedReportTemplate:
                    return SelectedReportTemplate;
                case FieldDefinitionEnum.ServiceForester:
                    return ServiceForester;
                case FieldDefinitionEnum.ServiceForestryProgramManager:
                    return ServiceForestryProgramManager;
                case FieldDefinitionEnum.ServiceForestryRegionalCoordinator:
                    return ServiceForestryRegionalCoordinator;
                case FieldDefinitionEnum.ServiceForestrySpecialist:
                    return ServiceForestrySpecialist;
                case FieldDefinitionEnum.ShowApplicationsToThePublic:
                    return ShowApplicationsToThePublic;
                case FieldDefinitionEnum.ShowLeadImplementerLogoOnFactSheet:
                    return ShowLeadImplementerLogoOnFactSheet;
                case FieldDefinitionEnum.SmallForestLandownerOfficeProgramManager:
                    return SmallForestLandownerOfficeProgramManager;
                case FieldDefinitionEnum.SmallForestLandownerProgramManager:
                    return SmallForestLandownerProgramManager;
                case FieldDefinitionEnum.StatewideVendorNumber:
                    return StatewideVendorNumber;
                case FieldDefinitionEnum.StewardshipFishAndWildlifeBiologist:
                    return StewardshipFishAndWildlifeBiologist;
                case FieldDefinitionEnum.SubObject:
                    return SubObject;
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
                case FieldDefinitionEnum.TreatedAcres:
                    return TreatedAcres;
                case FieldDefinitionEnum.TreatmentCode:
                    return TreatmentCode;
                case FieldDefinitionEnum.TreatmentCostPerAcre:
                    return TreatmentCostPerAcre;
                case FieldDefinitionEnum.TreatmentDetailedActivityType:
                    return TreatmentDetailedActivityType;
                case FieldDefinitionEnum.TreatmentTotalCost:
                    return TreatmentTotalCost;
                case FieldDefinitionEnum.TreatmentType:
                    return TreatmentType;
                case FieldDefinitionEnum.UcfStatewideSpecialist:
                    return UcfStatewideSpecialist;
                case FieldDefinitionEnum.UnfundedNeed:
                    return UnfundedNeed;
                case FieldDefinitionEnum.UnsecuredFunding:
                    return UnsecuredFunding;
                case FieldDefinitionEnum.UpdatesFromImport:
                    return UpdatesFromImport;
                case FieldDefinitionEnum.UrbanForestryTechnician:
                    return UrbanForestryTechnician;
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
        TaxonomyTrunk = 5,
        PrimaryContactOrganization = 12,
        ProjectsStewardOrganizationRelationshipToProject = 13,
        Organization = 14,
        Password = 17,
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
        ProjectInitiationDate = 86,
        AssociatedTaxonomyBranches = 87,
        ExternalLinks = 88,
        CurrentYearForPVCalculations = 91,
        LifecycleOperatingCost = 92,
        RoleName = 182,
        DNRUplandRegion = 184,
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
        ProjectUpdateKickOffDate = 261,
        ProjectUpdateReminderInterval = 262,
        ProjectUpdateCloseOutDate = 263,
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
        PriorityLandscape = 293,
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
        PaymentAmount = 318,
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
        GrantAllocationBudgetLineItem = 345,
        GrantModificationPurpose = 347,
        GrantModificationStatus = 348,
        GrantModificationAmount = 349,
        GrantModificationDescription = 350,
        GrantModificationStartDate = 351,
        GrantModificationEndDate = 352,
        GrantModificationName = 353,
        GrantModification = 354,
        GrantModificationNoteInternal = 355,
        ProgramIndexProjectCode = 356,
        FhtProjectNumber = 357,
        GrantCurrentBalance = 358,
        GrantAllocationCurrentBalance = 359,
        GrantAllocationChangeLogNote = 360,
        GrantAllocationAwardLandownerCostShareStartDate = 416,
        GrantAllocationAwardLandownerCostShareEndDate = 417,
        GrantAllocationAwardLandownerCostShareFootprintAcres = 418,
        GrantAllocationAwardLandownerCostShareChippingAcres = 419,
        GrantAllocationAwardLandownerCostSharePruningAcres = 420,
        GrantAllocationAwardLandownerCostShareThinningAcres = 421,
        GrantAllocationAwardLandownerCostShareMasticationAcres = 422,
        GrantAllocationAwardLandownerCostShareGrazingAcres = 423,
        GrantAllocationAwardLandownerCostShareLopAndScatterAcres = 424,
        GrantAllocationAwardLandownerCostShareBiomassRemovalAcres = 425,
        GrantAllocationAwardLandownerCostShareHandPileAcres = 426,
        GrantAllocationAwardLandownerCostShareBroadcastBurnAcres = 427,
        GrantAllocationAwardLandownerCostShareHandPileBurnAcres = 428,
        GrantAllocationAwardLandownerCostShareMachinePileBurnAcres = 429,
        GrantAllocationAwardLandownerCostShareOtherTreatmentAcres = 430,
        GrantAllocationAwardLandownerCostShareSlashAcres = 431,
        ProjectIdentifier = 465,
        PlannedDate = 466,
        TreatedAcres = 467,
        TreatmentType = 468,
        TreatmentDetailedActivityType = 469,
        FootprintAcres = 470,
        FundingSource = 471,
        FundingSourceNote = 472,
        ProjectTotalCompletedTreatmentAcres = 473,
        LimitVisibilityToAdmin = 474,
        Program = 475,
        ProjectGrantAllocationRequestMatchAmount = 476,
        ProjectGrantAllocationRequestPayAmount = 477,
        ProjectApplicationDate = 478,
        ProjectDecisionDate = 479,
        ServiceForester = 480,
        StewardshipFishAndWildlifeBiologist = 481,
        RegulationAssistanceForester = 482,
        FamilyForestFishPassageProgram = 483,
        ForestryRiparianEasementProgram = 484,
        RiversAndHabitatOpenSpaceProgramManager = 485,
        CommunityResilienceCoordinator = 486,
        UrbanForestryTechnician = 487,
        ForestPracticesForester = 488,
        SmallForestLandownerOfficeProgramManager = 489,
        SmallForestLandownerProgramManager = 490,
        UcfStatewideSpecialist = 491,
        ServiceForestrySpecialist = 492,
        ExternalMapLayer = 493,
        ExternalMapLayerDisplayName = 494,
        ExternalMapLayerUrl = 495,
        ExternalMapLayerDescription = 496,
        ExternalMapLayerFeatureNameField = 497,
        ExternalMapLayerDisplayOnPriorityLandscape = 498,
        ExternalMapLayerDisplayOnProjectMap = 499,
        ExternalMapLayerDisplayOnAllOthers = 500,
        ExternalMapLayerIsATiledMapService = 501,
        ExternalMapLayerIsActive = 502,
        UpdatesFromImport = 503,
        TreatmentCode = 504,
        TreatmentCostPerAcre = 505,
        TreatmentTotalCost = 506,
        ServiceForestryRegionalCoordinator = 507,
        County = 508,
        PercentageMatch = 509,
        ReportTitle = 510,
        ReportDescription = 511,
        ReportFile = 512,
        ReportModel = 513,
        SelectedReportTemplate = 514,
        InvoicePaymentRequest = 515,
        DUNS = 516,
        OrganizationCode = 517,
        InvoiceNumber = 518,
        Fund = 519,
        Appn = 520,
        SubObject = 521,
        ServiceForestryProgramManager = 522,
        ForestRegulationFishAndWildlifeBiologist = 523,
        GrantAllocationFundFSPs = 524,
        GrantAllocationSource = 525,
        GrantAllocationAllocation = 526,
        GrantAllocationTotalGrantFunds = 527,
        GrantAllocationOverallBalance = 528,
        GrantAllocationContractualBalance = 529,
        GrantAllocationTravelBalance = 530,
        GrantAllocationStaffBalance = 531,
        GrantAllocationLikelyToUse = 532,
        GrantAllocationCompleted = 533,
        GrantAllocationPriority = 534,
        LeadImplementerOrganization = 535
    }

    public partial class FieldDefinitionProjectType : FieldDefinition
    {
        private FieldDefinitionProjectType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectType Instance = new FieldDefinitionProjectType(1, @"ProjectType", @"Project Type", @"<p>The highest level record in the hierarchical project taxonomy system.</p>");
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

    public partial class FieldDefinitionMeasurementUnit : FieldDefinition
    {
        private FieldDefinitionMeasurementUnit(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionMeasurementUnit Instance = new FieldDefinitionMeasurementUnit(21, @"MeasurementUnit", @"Measurement Unit", @"<p>The unit of measure used.</p>");
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
        public static readonly FieldDefinitionProject Instance = new FieldDefinitionProject(44, @"Project", @"Project", @"<p>The core entity that ProjectFirma tracks.</p>");
    }

    public partial class FieldDefinitionClassification : FieldDefinition
    {
        private FieldDefinitionClassification(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionClassification Instance = new FieldDefinitionClassification(46, @"Classification", @"Classification", @"<p>One of the groupings in a logical system used to group projects according to overarching program themes or goals.</p>");
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

    public partial class FieldDefinitionProjectInitiationDate : FieldDefinition
    {
        private FieldDefinitionProjectInitiationDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectInitiationDate Instance = new FieldDefinitionProjectInitiationDate(86, @"ProjectInitiationDate", @"Project Initiation Date", @"<p>The date on which project began.  Contrast with &quot;Approval.&quot; For more detailed information, see the definition for &quot;Stage&quot;.</p>");
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

    public partial class FieldDefinitionRoleName : FieldDefinition
    {
        private FieldDefinitionRoleName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionRoleName Instance = new FieldDefinitionRoleName(182, @"RoleName", @"Role Name", @"<p>The name or title describing&nbsp;function or set of permissions that can be assigned to a user.</p>");
    }

    public partial class FieldDefinitionDNRUplandRegion : FieldDefinition
    {
        private FieldDefinitionDNRUplandRegion(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionDNRUplandRegion Instance = new FieldDefinitionDNRUplandRegion(184, @"DNRUplandRegion", @"DNR Upland Region", @"<p>The DNR Upland Region in which a project is located.</p>");
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
        public static readonly FieldDefinitionCostType Instance = new FieldDefinitionCostType(279, @"CostType", @"Cost Type", @"Placeholder definition for Cost Type.");
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

    public partial class FieldDefinitionPriorityLandscape : FieldDefinition
    {
        private FieldDefinitionPriorityLandscape(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPriorityLandscape Instance = new FieldDefinitionPriorityLandscape(293, @"PriorityLandscape", @"Priority Landscape", @"Placeholder definition for Priority Landscape");
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

    public partial class FieldDefinitionPaymentAmount : FieldDefinition
    {
        private FieldDefinitionPaymentAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPaymentAmount Instance = new FieldDefinitionPaymentAmount(318, @"PaymentAmount", @"Payment Amount", @"The total amount of funding requested by a given invoice");
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

    public partial class FieldDefinitionGrantModificationPurpose : FieldDefinition
    {
        private FieldDefinitionGrantModificationPurpose(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationPurpose Instance = new FieldDefinitionGrantModificationPurpose(347, @"GrantModificationPurpose", @"Grant Modification Purpose", @"<p>Placeholder definition for Grant Modification Purpose</p>");
    }

    public partial class FieldDefinitionGrantModificationStatus : FieldDefinition
    {
        private FieldDefinitionGrantModificationStatus(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationStatus Instance = new FieldDefinitionGrantModificationStatus(348, @"GrantModificationStatus", @"Grant Modification Status", @"<p>Placeholder definition for Grant Modification Status</p>");
    }

    public partial class FieldDefinitionGrantModificationAmount : FieldDefinition
    {
        private FieldDefinitionGrantModificationAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationAmount Instance = new FieldDefinitionGrantModificationAmount(349, @"GrantModificationAmount", @"Grant Modification Amount", @"<p>Placeholder definition for Grant Modification Amount</p>");
    }

    public partial class FieldDefinitionGrantModificationDescription : FieldDefinition
    {
        private FieldDefinitionGrantModificationDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationDescription Instance = new FieldDefinitionGrantModificationDescription(350, @"GrantModificationDescription", @"Grant Modification Description", @"<p>Placeholder definition for Grant Modification Description</p>");
    }

    public partial class FieldDefinitionGrantModificationStartDate : FieldDefinition
    {
        private FieldDefinitionGrantModificationStartDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationStartDate Instance = new FieldDefinitionGrantModificationStartDate(351, @"GrantModificationStartDate", @"Grant Modification Start Date", @"<p>Placeholder definition for Grant Modification Start Date</p>");
    }

    public partial class FieldDefinitionGrantModificationEndDate : FieldDefinition
    {
        private FieldDefinitionGrantModificationEndDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationEndDate Instance = new FieldDefinitionGrantModificationEndDate(352, @"GrantModificationEndDate", @"Grant Modification End Date", @"<p>Placeholder definition for Grant Modification End Date</p>");
    }

    public partial class FieldDefinitionGrantModificationName : FieldDefinition
    {
        private FieldDefinitionGrantModificationName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationName Instance = new FieldDefinitionGrantModificationName(353, @"GrantModificationName", @"Grant Modification Name", @"<p>Placeholder definition for Grant Modification Name</p>");
    }

    public partial class FieldDefinitionGrantModification : FieldDefinition
    {
        private FieldDefinitionGrantModification(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModification Instance = new FieldDefinitionGrantModification(354, @"GrantModification", @"Grant Modification", @"<p>Placeholder definition for Grant Modification</p>");
    }

    public partial class FieldDefinitionGrantModificationNoteInternal : FieldDefinition
    {
        private FieldDefinitionGrantModificationNoteInternal(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantModificationNoteInternal Instance = new FieldDefinitionGrantModificationNoteInternal(355, @"GrantModificationNoteInternal", @"Internal Grant Modification Note", @"<p>Any additional important information about the grant modification. These notes are only visible to internal users </p>");
    }

    public partial class FieldDefinitionProgramIndexProjectCode : FieldDefinition
    {
        private FieldDefinitionProgramIndexProjectCode(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProgramIndexProjectCode Instance = new FieldDefinitionProgramIndexProjectCode(356, @"ProgramIndexProjectCode", @"Program Index - Project Code", @"Placeholder definition for Program Index-Project Code.");
    }

    public partial class FieldDefinitionFhtProjectNumber : FieldDefinition
    {
        private FieldDefinitionFhtProjectNumber(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFhtProjectNumber Instance = new FieldDefinitionFhtProjectNumber(357, @"FhtProjectNumber", @"FHT Project Number", @"<p>Placeholder definition for FHT Project Number description.</p>");
    }

    public partial class FieldDefinitionGrantCurrentBalance : FieldDefinition
    {
        private FieldDefinitionGrantCurrentBalance(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantCurrentBalance Instance = new FieldDefinitionGrantCurrentBalance(358, @"GrantCurrentBalance", @"Grant Current Balance", @"<p>Placeholder definition for Grant Current Balance description.</p>");
    }

    public partial class FieldDefinitionGrantAllocationCurrentBalance : FieldDefinition
    {
        private FieldDefinitionGrantAllocationCurrentBalance(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationCurrentBalance Instance = new FieldDefinitionGrantAllocationCurrentBalance(359, @"GrantAllocationCurrentBalance", @"Grant Allocation Current Balance", @"<p>Placeholder definition for Grant Allocation Current Balance description.</p>");
    }

    public partial class FieldDefinitionGrantAllocationChangeLogNote : FieldDefinition
    {
        private FieldDefinitionGrantAllocationChangeLogNote(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationChangeLogNote Instance = new FieldDefinitionGrantAllocationChangeLogNote(360, @"GrantAllocationChangeLogNote", @"Allocation Amount Change Log Note", @"<p>Placeholder definition for Allocation Amount Change Log Note description.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareStartDate : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareStartDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareStartDate Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareStartDate(416, @"GrantAllocationAwardLandownerCostShareStartDate", @"Grant Allocation Award Landowner Cost Share Start Date", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Start Date.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareEndDate : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareEndDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareEndDate Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareEndDate(417, @"GrantAllocationAwardLandownerCostShareEndDate", @"Grant Allocation Award Landowner Cost Share End Date", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share End Date.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareFootprintAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareFootprintAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareFootprintAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareFootprintAcres(418, @"GrantAllocationAwardLandownerCostShareFootprintAcres", @"Grant Allocation Award Landowner Cost Share Footprint Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Footprint Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareChippingAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareChippingAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareChippingAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareChippingAcres(419, @"GrantAllocationAwardLandownerCostShareChippingAcres", @"Grant Allocation Award Landowner Cost Share Chipping Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Chipping Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostSharePruningAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostSharePruningAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostSharePruningAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostSharePruningAcres(420, @"GrantAllocationAwardLandownerCostSharePruningAcres", @"Grant Allocation Award Landowner Cost Share Pruning Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Pruning Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareThinningAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareThinningAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareThinningAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareThinningAcres(421, @"GrantAllocationAwardLandownerCostShareThinningAcres", @"Grant Allocation Award Landowner Cost Share Thinning Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Thinning Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareMasticationAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareMasticationAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareMasticationAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareMasticationAcres(422, @"GrantAllocationAwardLandownerCostShareMasticationAcres", @"Grant Allocation Award Landowner Cost Share Mastication Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Mastication Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareGrazingAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareGrazingAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareGrazingAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareGrazingAcres(423, @"GrantAllocationAwardLandownerCostShareGrazingAcres", @"Grant Allocation Award Landowner Cost Share Grazing Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Grazing Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareLopAndScatterAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareLopAndScatterAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareLopAndScatterAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareLopAndScatterAcres(424, @"GrantAllocationAwardLandownerCostShareLopAndScatterAcres", @"Grant Allocation Award Landowner Cost Share Lop And Scatter Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Lop and Scatter Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareBiomassRemovalAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareBiomassRemovalAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareBiomassRemovalAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareBiomassRemovalAcres(425, @"GrantAllocationAwardLandownerCostShareBiomassRemovalAcres", @"Grant Allocation Award Landowner Cost Share Biomass Removal Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Biomass Removal Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileAcres(426, @"GrantAllocationAwardLandownerCostShareHandPileAcres", @"Grant Allocation Award Landowner Cost Share Hand Pile Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Hand Pile Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareBroadcastBurnAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareBroadcastBurnAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareBroadcastBurnAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareBroadcastBurnAcres(427, @"GrantAllocationAwardLandownerCostShareBroadcastBurnAcres", @"Grant Allocation Award Landowner Cost Share Broadcast Burn Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Broadcast Burn Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileBurnAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileBurnAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileBurnAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareHandPileBurnAcres(428, @"GrantAllocationAwardLandownerCostShareHandPileBurnAcres", @"Grant Allocation Award Landowner Cost Share Hand Pile Burn Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Hand Pile Burn Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareMachinePileBurnAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareMachinePileBurnAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareMachinePileBurnAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareMachinePileBurnAcres(429, @"GrantAllocationAwardLandownerCostShareMachinePileBurnAcres", @"Grant Allocation Award Landowner Cost Share Machine Pile Burn Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Machine Pile Burn Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareOtherTreatmentAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareOtherTreatmentAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareOtherTreatmentAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareOtherTreatmentAcres(430, @"GrantAllocationAwardLandownerCostShareOtherTreatmentAcres", @"Grant Allocation Award Landowner Cost Share Other Treatment Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Other Treatment Acres.</p>");
    }

    public partial class FieldDefinitionGrantAllocationAwardLandownerCostShareSlashAcres : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAwardLandownerCostShareSlashAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAwardLandownerCostShareSlashAcres Instance = new FieldDefinitionGrantAllocationAwardLandownerCostShareSlashAcres(431, @"GrantAllocationAwardLandownerCostShareSlashAcres", @"Grant Allocation Award Landowner Cost Share Slash Acres", @"<p>Placeholder definition for Grant Allocation Award Landowner Cost Share Slash Acres.</p>");
    }

    public partial class FieldDefinitionProjectIdentifier : FieldDefinition
    {
        private FieldDefinitionProjectIdentifier(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectIdentifier Instance = new FieldDefinitionProjectIdentifier(465, @"ProjectIdentifier", @"Project Identifier", @"<p>Project Identifier for mapping to GIS source data</p>");
    }

    public partial class FieldDefinitionPlannedDate : FieldDefinition
    {
        private FieldDefinitionPlannedDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPlannedDate Instance = new FieldDefinitionPlannedDate(466, @"PlannedDate", @"Planned Date", @"<p>The date work is planned to start for a Project</p>");
    }

    public partial class FieldDefinitionTreatedAcres : FieldDefinition
    {
        private FieldDefinitionTreatedAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTreatedAcres Instance = new FieldDefinitionTreatedAcres(467, @"TreatedAcres", @"Treated Acres", @"<p>The amount of acres completed under a treatment</p>");
    }

    public partial class FieldDefinitionTreatmentType : FieldDefinition
    {
        private FieldDefinitionTreatmentType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTreatmentType Instance = new FieldDefinitionTreatmentType(468, @"TreatmentType", @"Treatment Type", @"<p>The type of treatment</p>");
    }

    public partial class FieldDefinitionTreatmentDetailedActivityType : FieldDefinition
    {
        private FieldDefinitionTreatmentDetailedActivityType(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTreatmentDetailedActivityType Instance = new FieldDefinitionTreatmentDetailedActivityType(469, @"TreatmentDetailedActivityType", @"Treatment Detailed Activity Type", @"<p>The type of treatment detailed activity</p>");
    }

    public partial class FieldDefinitionFootprintAcres : FieldDefinition
    {
        private FieldDefinitionFootprintAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFootprintAcres Instance = new FieldDefinitionFootprintAcres(470, @"FootprintAcres", @"Footprint Acres", @"<p>The footprint acres of a treatment</p>");
    }

    public partial class FieldDefinitionFundingSource : FieldDefinition
    {
        private FieldDefinitionFundingSource(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFundingSource Instance = new FieldDefinitionFundingSource(471, @"FundingSource", @"Funding Source", @"<p>The funding source for the expected funding.</p>");
    }

    public partial class FieldDefinitionFundingSourceNote : FieldDefinition
    {
        private FieldDefinitionFundingSourceNote(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFundingSourceNote Instance = new FieldDefinitionFundingSourceNote(472, @"FundingSourceNote", @"Funding Source Note", @"<p>A note about the funding source(s) selected.</p>");
    }

    public partial class FieldDefinitionProjectTotalCompletedTreatmentAcres : FieldDefinition
    {
        private FieldDefinitionProjectTotalCompletedTreatmentAcres(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectTotalCompletedTreatmentAcres Instance = new FieldDefinitionProjectTotalCompletedTreatmentAcres(473, @"ProjectTotalCompletedTreatmentAcres", @"Completed Treatment Acres", @"Sum of Treated Acres on all completed Treatments under a Project.");
    }

    public partial class FieldDefinitionLimitVisibilityToAdmin : FieldDefinition
    {
        private FieldDefinitionLimitVisibilityToAdmin(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionLimitVisibilityToAdmin Instance = new FieldDefinitionLimitVisibilityToAdmin(474, @"LimitVisibilityToAdmin", @"Project Type Visibility Limited To Admin", @"Limit Visibility of this Project Type to just Administrators (Program Manager, Admin, Sitka Admin)");
    }

    public partial class FieldDefinitionProgram : FieldDefinition
    {
        private FieldDefinitionProgram(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProgram Instance = new FieldDefinitionProgram(475, @"Program", @"Program", @"Program");
    }

    public partial class FieldDefinitionProjectGrantAllocationRequestMatchAmount : FieldDefinition
    {
        private FieldDefinitionProjectGrantAllocationRequestMatchAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectGrantAllocationRequestMatchAmount Instance = new FieldDefinitionProjectGrantAllocationRequestMatchAmount(476, @"ProjectGrantAllocationRequestMatchAmount", @"MatchAmount", @"<p>Funding that has been acquired for a project.</p>");
    }

    public partial class FieldDefinitionProjectGrantAllocationRequestPayAmount : FieldDefinition
    {
        private FieldDefinitionProjectGrantAllocationRequestPayAmount(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectGrantAllocationRequestPayAmount Instance = new FieldDefinitionProjectGrantAllocationRequestPayAmount(477, @"ProjectGrantAllocationRequestPayAmount", @"PayAmount", @"<p>Funding that has been acquired for a project.</p>");
    }

    public partial class FieldDefinitionProjectApplicationDate : FieldDefinition
    {
        private FieldDefinitionProjectApplicationDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectApplicationDate Instance = new FieldDefinitionProjectApplicationDate(478, @"ProjectApplicationDate", @"Project Application Date", @"<p>The date on which the application for the project was submitted.");
    }

    public partial class FieldDefinitionProjectDecisionDate : FieldDefinition
    {
        private FieldDefinitionProjectDecisionDate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionProjectDecisionDate Instance = new FieldDefinitionProjectDecisionDate(479, @"ProjectDecisionDate", @"Project Decision Date", @"<p>The date on which the project was decided/approved.");
    }

    public partial class FieldDefinitionServiceForester : FieldDefinition
    {
        private FieldDefinitionServiceForester(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionServiceForester Instance = new FieldDefinitionServiceForester(480, @"ServiceForester", @"Service Forester", @"Placeholder definition for forester role, Service Forester");
    }

    public partial class FieldDefinitionStewardshipFishAndWildlifeBiologist : FieldDefinition
    {
        private FieldDefinitionStewardshipFishAndWildlifeBiologist(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionStewardshipFishAndWildlifeBiologist Instance = new FieldDefinitionStewardshipFishAndWildlifeBiologist(481, @"StewardshipFishAndWildlifeBiologist", @"Stewardship Fish & Wildlife Biologist", @"Placeholder definition for forester role, Stewardship Fish & Wildlife Biologist");
    }

    public partial class FieldDefinitionRegulationAssistanceForester : FieldDefinition
    {
        private FieldDefinitionRegulationAssistanceForester(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionRegulationAssistanceForester Instance = new FieldDefinitionRegulationAssistanceForester(482, @"RegulationAssistanceForester", @"Regulation Assistance Forester", @"Placeholder definition for forester role, Regulation Assistance Forester");
    }

    public partial class FieldDefinitionFamilyForestFishPassageProgram : FieldDefinition
    {
        private FieldDefinitionFamilyForestFishPassageProgram(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFamilyForestFishPassageProgram Instance = new FieldDefinitionFamilyForestFishPassageProgram(483, @"FamilyForestFishPassageProgram", @"Family Forest Fish Passage Program", @"Placeholder definition for forester role, Family Forest Fish Passage Program");
    }

    public partial class FieldDefinitionForestryRiparianEasementProgram : FieldDefinition
    {
        private FieldDefinitionForestryRiparianEasementProgram(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionForestryRiparianEasementProgram Instance = new FieldDefinitionForestryRiparianEasementProgram(484, @"ForestryRiparianEasementProgram", @"Forestry Riparian Easement Program", @"Placeholder definition for forester role, Forestry Riparian Easement Program");
    }

    public partial class FieldDefinitionRiversAndHabitatOpenSpaceProgramManager : FieldDefinition
    {
        private FieldDefinitionRiversAndHabitatOpenSpaceProgramManager(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionRiversAndHabitatOpenSpaceProgramManager Instance = new FieldDefinitionRiversAndHabitatOpenSpaceProgramManager(485, @"RiversAndHabitatOpenSpaceProgramManager", @"Rivers and Habitat Open Space Program Manager", @"Placeholder definition for forester role, Rivers and Habitat Open Space Program Manager");
    }

    public partial class FieldDefinitionCommunityResilienceCoordinator : FieldDefinition
    {
        private FieldDefinitionCommunityResilienceCoordinator(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCommunityResilienceCoordinator Instance = new FieldDefinitionCommunityResilienceCoordinator(486, @"CommunityResilienceCoordinator", @"Community Resilience Coordinator", @"Placeholder definition for forester role, Community Resilience Coordinator");
    }

    public partial class FieldDefinitionUrbanForestryTechnician : FieldDefinition
    {
        private FieldDefinitionUrbanForestryTechnician(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionUrbanForestryTechnician Instance = new FieldDefinitionUrbanForestryTechnician(487, @"UrbanForestryTechnician", @"Urban Forestry Technician", @"Placeholder definition for forester role, Urban Forestry Technician");
    }

    public partial class FieldDefinitionForestPracticesForester : FieldDefinition
    {
        private FieldDefinitionForestPracticesForester(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionForestPracticesForester Instance = new FieldDefinitionForestPracticesForester(488, @"ForestPracticesForester", @"Forest Practices Forester", @"Placeholder definition for forester role, Forest Practices Forester");
    }

    public partial class FieldDefinitionSmallForestLandownerOfficeProgramManager : FieldDefinition
    {
        private FieldDefinitionSmallForestLandownerOfficeProgramManager(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionSmallForestLandownerOfficeProgramManager Instance = new FieldDefinitionSmallForestLandownerOfficeProgramManager(489, @"SmallForestLandownerOfficeProgramManager", @"Small Forest Landowner Office Program Manager", @"Placeholder definition for forester role, Small Forest Landowner Office Program Manager");
    }

    public partial class FieldDefinitionSmallForestLandownerProgramManager : FieldDefinition
    {
        private FieldDefinitionSmallForestLandownerProgramManager(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionSmallForestLandownerProgramManager Instance = new FieldDefinitionSmallForestLandownerProgramManager(490, @"SmallForestLandownerProgramManager", @"Small Forest Landowner Program Manager", @"Placeholder definition for forester role, Small Forest Landowner Program Manager");
    }

    public partial class FieldDefinitionUcfStatewideSpecialist : FieldDefinition
    {
        private FieldDefinitionUcfStatewideSpecialist(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionUcfStatewideSpecialist Instance = new FieldDefinitionUcfStatewideSpecialist(491, @"UcfStatewideSpecialist", @"UCF Statewide Specialist", @"Placeholder definition for forester role, UCF Statewide Specialist");
    }

    public partial class FieldDefinitionServiceForestrySpecialist : FieldDefinition
    {
        private FieldDefinitionServiceForestrySpecialist(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionServiceForestrySpecialist Instance = new FieldDefinitionServiceForestrySpecialist(492, @"ServiceForestrySpecialist", @"Service Forestry Specialist", @"Placeholder definition for forester role, Service Forestry Specialist");
    }

    public partial class FieldDefinitionExternalMapLayer : FieldDefinition
    {
        private FieldDefinitionExternalMapLayer(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayer Instance = new FieldDefinitionExternalMapLayer(493, @"ExternalMapLayer", @"External Map Layer", @"An administrator can add a connection to a web service link (feature service) provided by ESRI ArcGIS Online to pull in spatial information that is stored in ArcGIS Online. Once the connection is added the reference layer will be available on maps throughout the system.");
    }

    public partial class FieldDefinitionExternalMapLayerDisplayName : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerDisplayName(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerDisplayName Instance = new FieldDefinitionExternalMapLayerDisplayName(494, @"ExternalMapLayerDisplayName", @"Display Name", @"The layer display name will appear in map legends and popups.");
    }

    public partial class FieldDefinitionExternalMapLayerUrl : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerUrl(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerUrl Instance = new FieldDefinitionExternalMapLayerUrl(495, @"ExternalMapLayerUrl", @"Url", @"The external map web service Url.");
    }

    public partial class FieldDefinitionExternalMapLayerDescription : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerDescription Instance = new FieldDefinitionExternalMapLayerDescription(496, @"ExternalMapLayerDescription", @"Internal Layer Description", @"Add helpful background information for other administrators (E.g who to contact if there is a problem with this external map service in the future).");
    }

    public partial class FieldDefinitionExternalMapLayerFeatureNameField : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerFeatureNameField(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerFeatureNameField Instance = new FieldDefinitionExternalMapLayerFeatureNameField(497, @"ExternalMapLayerFeatureNameField", @"Field to use as source for feature names", @"This setting will populate the map pop-ups with names from the feature service. This is field is case-sensitive (E.g. Name or NAME) and must match a field in the web service. Please leave this field blank for tile services since pop-ups are not supported.");
    }

    public partial class FieldDefinitionExternalMapLayerDisplayOnPriorityLandscape : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerDisplayOnPriorityLandscape(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerDisplayOnPriorityLandscape Instance = new FieldDefinitionExternalMapLayerDisplayOnPriorityLandscape(498, @"ExternalMapLayerDisplayOnPriorityLandscape", @"Display on Priority Landscape Maps?", @"When this option is set, the external map layer will appear on Priority Landscape maps.");
    }

    public partial class FieldDefinitionExternalMapLayerDisplayOnProjectMap : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerDisplayOnProjectMap(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerDisplayOnProjectMap Instance = new FieldDefinitionExternalMapLayerDisplayOnProjectMap(499, @"ExternalMapLayerDisplayOnProjectMap", @"Display on Project Map?", @"When this option is set, the external map layer will appear on the Project Map.");
    }

    public partial class FieldDefinitionExternalMapLayerDisplayOnAllOthers : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerDisplayOnAllOthers(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerDisplayOnAllOthers Instance = new FieldDefinitionExternalMapLayerDisplayOnAllOthers(500, @"ExternalMapLayerDisplayOnAllOthers", @"Display on All Other Maps?", @"When this option is set, the external map layer will appear on maps throughout the system, except for the Priority Landscape and Project maps (which have their own settings).");
    }

    public partial class FieldDefinitionExternalMapLayerIsATiledMapService : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerIsATiledMapService(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerIsATiledMapService Instance = new FieldDefinitionExternalMapLayerIsATiledMapService(501, @"ExternalMapLayerIsATiledMapService", @"Is a Tiled Map Service?", @"Please turn on this setting if the external map layer is a tiled layer (raster). Note: Pop-ups will not appear on maps for tiled layers.");
    }

    public partial class FieldDefinitionExternalMapLayerIsActive : FieldDefinition
    {
        private FieldDefinitionExternalMapLayerIsActive(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionExternalMapLayerIsActive Instance = new FieldDefinitionExternalMapLayerIsActive(502, @"ExternalMapLayerIsActive", @"Is Active?", @"If this is not set, the layer will not appear on any maps. If there is a problem with the external map service an administrator can uncheck this box to hide the layer until it is resolved.");
    }

    public partial class FieldDefinitionUpdatesFromImport : FieldDefinition
    {
        private FieldDefinitionUpdatesFromImport(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionUpdatesFromImport Instance = new FieldDefinitionUpdatesFromImport(503, @"UpdatesFromImport", @"Updates From Import", @"<p>Projects can be updated via the bulk-import process. They can be blocked from updating in this way.</p>");
    }

    public partial class FieldDefinitionTreatmentCode : FieldDefinition
    {
        private FieldDefinitionTreatmentCode(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTreatmentCode Instance = new FieldDefinitionTreatmentCode(504, @"TreatmentCode", @"Treatment Code", @"<p>The code for the treatment, e.g., BR-1: Brush Control.</p>");
    }

    public partial class FieldDefinitionTreatmentCostPerAcre : FieldDefinition
    {
        private FieldDefinitionTreatmentCostPerAcre(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTreatmentCostPerAcre Instance = new FieldDefinitionTreatmentCostPerAcre(505, @"TreatmentCostPerAcre", @"Treatment Cost Per Acre", @"<p>The cost of the treatment on a per acre basis.</p>");
    }

    public partial class FieldDefinitionTreatmentTotalCost : FieldDefinition
    {
        private FieldDefinitionTreatmentTotalCost(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionTreatmentTotalCost Instance = new FieldDefinitionTreatmentTotalCost(506, @"TreatmentTotalCost", @"Treatment Total Cost", @"<p>The treatment acres multiplied by the cost per acre.</p>");
    }

    public partial class FieldDefinitionServiceForestryRegionalCoordinator : FieldDefinition
    {
        private FieldDefinitionServiceForestryRegionalCoordinator(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionServiceForestryRegionalCoordinator Instance = new FieldDefinitionServiceForestryRegionalCoordinator(507, @"ServiceForestryRegionalCoordinator", @"Service Forestry Regional Coordinator", @"Placeholder definition for Service Forestry Regional Coordinator.");
    }

    public partial class FieldDefinitionCounty : FieldDefinition
    {
        private FieldDefinitionCounty(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionCounty Instance = new FieldDefinitionCounty(508, @"County", @"County", @"<p>A political and administrative division of a state, providing certain local governmental services.</p>");
    }

    public partial class FieldDefinitionPercentageMatch : FieldDefinition
    {
        private FieldDefinitionPercentageMatch(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionPercentageMatch Instance = new FieldDefinitionPercentageMatch(509, @"PercentageMatch", @"Percentage Match", @"<p>The percentage matched by WADNR for Service Forestry specific projects.</p>");
    }

    public partial class FieldDefinitionReportTitle : FieldDefinition
    {
        private FieldDefinitionReportTitle(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportTitle Instance = new FieldDefinitionReportTitle(510, @"ReportTitle", @"Report Title", @"Report Title");
    }

    public partial class FieldDefinitionReportDescription : FieldDefinition
    {
        private FieldDefinitionReportDescription(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportDescription Instance = new FieldDefinitionReportDescription(511, @"ReportDescription", @"Report Description", @"Report Description");
    }

    public partial class FieldDefinitionReportFile : FieldDefinition
    {
        private FieldDefinitionReportFile(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportFile Instance = new FieldDefinitionReportFile(512, @"ReportFile", @"Report File", @"Report File");
    }

    public partial class FieldDefinitionReportModel : FieldDefinition
    {
        private FieldDefinitionReportModel(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionReportModel Instance = new FieldDefinitionReportModel(513, @"ReportModel", @"Report Model", @"Report Model");
    }

    public partial class FieldDefinitionSelectedReportTemplate : FieldDefinition
    {
        private FieldDefinitionSelectedReportTemplate(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionSelectedReportTemplate Instance = new FieldDefinitionSelectedReportTemplate(514, @"SelectedReportTemplate", @"Selected Report Template", @"Selected Report Template");
    }

    public partial class FieldDefinitionInvoicePaymentRequest : FieldDefinition
    {
        private FieldDefinitionInvoicePaymentRequest(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoicePaymentRequest Instance = new FieldDefinitionInvoicePaymentRequest(515, @"InvoicePaymentRequest", @"Invoice Payment Request", @"Invoice Payment Request");
    }

    public partial class FieldDefinitionDUNS : FieldDefinition
    {
        private FieldDefinitionDUNS(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionDUNS Instance = new FieldDefinitionDUNS(516, @"DUNS", @"DUNS", @"DUNS");
    }

    public partial class FieldDefinitionOrganizationCode : FieldDefinition
    {
        private FieldDefinitionOrganizationCode(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionOrganizationCode Instance = new FieldDefinitionOrganizationCode(517, @"OrganizationCode", @"Organization Code", @"Organization Code used in invoicing");
    }

    public partial class FieldDefinitionInvoiceNumber : FieldDefinition
    {
        private FieldDefinitionInvoiceNumber(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionInvoiceNumber Instance = new FieldDefinitionInvoiceNumber(518, @"InvoiceNumber", @"Invoice Number", @"The number of an invoice");
    }

    public partial class FieldDefinitionFund : FieldDefinition
    {
        private FieldDefinitionFund(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionFund Instance = new FieldDefinitionFund(519, @"Fund", @"Fund", @"The fund of an invoice");
    }

    public partial class FieldDefinitionAppn : FieldDefinition
    {
        private FieldDefinitionAppn(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionAppn Instance = new FieldDefinitionAppn(520, @"Appn", @"Appn", @"The Appn of an invoice");
    }

    public partial class FieldDefinitionSubObject : FieldDefinition
    {
        private FieldDefinitionSubObject(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionSubObject Instance = new FieldDefinitionSubObject(521, @"SubObject", @"SubObject", @"The SubObject of an invoice");
    }

    public partial class FieldDefinitionServiceForestryProgramManager : FieldDefinition
    {
        private FieldDefinitionServiceForestryProgramManager(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionServiceForestryProgramManager Instance = new FieldDefinitionServiceForestryProgramManager(522, @"ServiceForestryProgramManager", @"Service Forestry Program Manager", @"Placeholder definition for forester role, Service Forestry Program Manager");
    }

    public partial class FieldDefinitionForestRegulationFishAndWildlifeBiologist : FieldDefinition
    {
        private FieldDefinitionForestRegulationFishAndWildlifeBiologist(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionForestRegulationFishAndWildlifeBiologist Instance = new FieldDefinitionForestRegulationFishAndWildlifeBiologist(523, @"ForestRegulationFishAndWildlifeBiologist", @"Forest Regulation Fish & Wildlife Biologist", @"Placeholder definition for forester role, Forest Regulation Fish & Wildlife Biologist");
    }

    public partial class FieldDefinitionGrantAllocationFundFSPs : FieldDefinition
    {
        private FieldDefinitionGrantAllocationFundFSPs(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationFundFSPs Instance = new FieldDefinitionGrantAllocationFundFSPs(524, @"GrantAllocationFundFSPs", @"Fund FSPs?", @"Placeholder definition for Grant Allocation Fund FSPs");
    }

    public partial class FieldDefinitionGrantAllocationSource : FieldDefinition
    {
        private FieldDefinitionGrantAllocationSource(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationSource Instance = new FieldDefinitionGrantAllocationSource(525, @"GrantAllocationSource", @"Source", @"Placeholder definition for Grant Allocation Source");
    }

    public partial class FieldDefinitionGrantAllocationAllocation : FieldDefinition
    {
        private FieldDefinitionGrantAllocationAllocation(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationAllocation Instance = new FieldDefinitionGrantAllocationAllocation(526, @"GrantAllocationAllocation", @"Allocation", @"Placeholder definition for Grant Allocation Allocation");
    }

    public partial class FieldDefinitionGrantAllocationTotalGrantFunds : FieldDefinition
    {
        private FieldDefinitionGrantAllocationTotalGrantFunds(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationTotalGrantFunds Instance = new FieldDefinitionGrantAllocationTotalGrantFunds(527, @"GrantAllocationTotalGrantFunds", @"Total Grant Funds", @"Placeholder definition for Grant Allocation Total Grant Funds");
    }

    public partial class FieldDefinitionGrantAllocationOverallBalance : FieldDefinition
    {
        private FieldDefinitionGrantAllocationOverallBalance(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationOverallBalance Instance = new FieldDefinitionGrantAllocationOverallBalance(528, @"GrantAllocationOverallBalance", @"Overall Balance", @"Placeholder definition for Grant Allocation Overall Balance");
    }

    public partial class FieldDefinitionGrantAllocationContractualBalance : FieldDefinition
    {
        private FieldDefinitionGrantAllocationContractualBalance(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationContractualBalance Instance = new FieldDefinitionGrantAllocationContractualBalance(529, @"GrantAllocationContractualBalance", @"Contractual Balance", @"Placeholder definition for Grant Allocation Contractual Balance");
    }

    public partial class FieldDefinitionGrantAllocationTravelBalance : FieldDefinition
    {
        private FieldDefinitionGrantAllocationTravelBalance(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationTravelBalance Instance = new FieldDefinitionGrantAllocationTravelBalance(530, @"GrantAllocationTravel Balance", @"Travel Balance", @"Placeholder definition for Grant Allocation Travel Balance");
    }

    public partial class FieldDefinitionGrantAllocationStaffBalance : FieldDefinition
    {
        private FieldDefinitionGrantAllocationStaffBalance(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationStaffBalance Instance = new FieldDefinitionGrantAllocationStaffBalance(531, @"GrantAllocationStaffBalance", @"Staff Balance", @"Placeholder definition for Grant Allocation Staff Balance");
    }

    public partial class FieldDefinitionGrantAllocationLikelyToUse : FieldDefinition
    {
        private FieldDefinitionGrantAllocationLikelyToUse(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationLikelyToUse Instance = new FieldDefinitionGrantAllocationLikelyToUse(532, @"GrantAllocationLikelyToUse", @"Likely To Use", @"Placeholder definition for Grant Allocation Likely To Use");
    }

    public partial class FieldDefinitionGrantAllocationCompleted : FieldDefinition
    {
        private FieldDefinitionGrantAllocationCompleted(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationCompleted Instance = new FieldDefinitionGrantAllocationCompleted(533, @"GrantAllocationCompleted", @"Completed", @"Placeholder definition for Grant Allocation Completed");
    }

    public partial class FieldDefinitionGrantAllocationPriority : FieldDefinition
    {
        private FieldDefinitionGrantAllocationPriority(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionGrantAllocationPriority Instance = new FieldDefinitionGrantAllocationPriority(534, @"GrantAllocationPriority", @"Priority", @"Placeholder definition for Grant Allocation Priority");
    }

    public partial class FieldDefinitionLeadImplementerOrganization : FieldDefinition
    {
        private FieldDefinitionLeadImplementerOrganization(int fieldDefinitionID, string fieldDefinitionName, string fieldDefinitionDisplayName, string defaultDefinition) : base(fieldDefinitionID, fieldDefinitionName, fieldDefinitionDisplayName, defaultDefinition) {}
        public static readonly FieldDefinitionLeadImplementerOrganization Instance = new FieldDefinitionLeadImplementerOrganization(535, @"LeadImplementerOrganization", @"Lead Implementer", @"Placeholder definition for Lead Implementer");
    }
}