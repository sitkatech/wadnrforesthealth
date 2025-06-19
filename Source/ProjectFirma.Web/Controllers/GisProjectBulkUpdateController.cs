using GeoJSON.Net;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.UnitTestCommon;
using ProjectFirma.Web.Views.GisProjectBulkUpdate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using Hangfire;
using log4net;
using ProjectFirma.Web.ScheduledJobs;

namespace ProjectFirma.Web.Controllers
{
    public class GisProjectBulkUpdateController : FirmaBaseController
    {
        [HttpGet]
        [GisAttemptUploadViewFeature]
        public PartialViewResult SourceOrganizationSelection()
        {
            var gisSourceOrganizations = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.ToList();
            var viewData = new SourceOrganizationTypeSelectionViewData(gisSourceOrganizations);
            var viewModel = new SourceOrganizationTypeSelectionViewModel();
            return RazorPartialView<SourceOrganizationTypeSelection, SourceOrganizationTypeSelectionViewData, SourceOrganizationTypeSelectionViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [GisAttemptUploadViewFeature]
        public ActionResult SourceOrganizationSelection(SourceOrganizationTypeSelectionViewModel viewModel)
        {
            var gisSourceOrganizations = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.ToList();
            var viewData = new SourceOrganizationTypeSelectionViewData(gisSourceOrganizations);

            if (!ModelState.IsValid)
            {
                return RazorPartialView<SourceOrganizationTypeSelection, SourceOrganizationTypeSelectionViewData, SourceOrganizationTypeSelectionViewModel>(viewData, viewModel);
            }

            // ReSharper disable once PossibleInvalidOperationException
            var gisAttempt = new GisUploadAttempt(viewModel.SourceOrganizationID.Value, CurrentPerson.PersonID, DateTime.Now);

            HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
            HttpRequestStorage.DatabaseEntities.SaveChanges(IsolationLevel.ReadUncommitted);
            return new ModalDialogFormJsonResult(
                        SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.InstructionsGisImport(gisAttempt.GisUploadAttemptID)));

        }

        private static GisImportSectionStatus GetGisImportSectionStatus(GisUploadAttempt gisUploadAttempt)
        {
            return new GisImportSectionStatus(gisUploadAttempt);
        }


        [GisAttemptUploadViewFeature]
        public ActionResult InstructionsGisImport(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey)
        {
            var firmaPageType = FirmaPageType.ToType(FirmaPageTypeEnum.GisUploadAttemptInstructions);
            var firmaPage = FirmaPage.GetFirmaPageByPageType(firmaPageType);
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
            var viewData = new InstructionsGisImportViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, firmaPage, false);

            return RazorView<InstructionsGisImport, InstructionsGisImportViewData>(viewData);

        }

        [HttpGet]
        [GisAttemptUploadViewFeature]
        public ActionResult UploadGisFile(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey)
        {
            var uploadGisFileViewModel = new UploadGisFileViewModel();
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;

            return ViewUploadGisFile(uploadGisFileViewModel, gisUploadAttempt);
        }

        [HttpGet]
        [GisAttemptUploadViewFeature]
        public ActionResult GisMetadata(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            var gisMetadataAttributeIDs = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.Select(x => x.GisMetadataAttributeID).ToList();
            var metadataAttributes =
                HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Where(x =>
                    gisMetadataAttributeIDs.Contains(x.GisMetadataAttributeID));


            var viewModel = new GisMetadataViewModel(gisUploadAttempt, metadataAttributes.ToList());
            return ViewUploadGisMetadata(gisUploadAttempt, viewModel);
        }

        private ViewResult ViewUploadGisMetadata(GisUploadAttempt gisUploadAttempt, GisMetadataViewModel gisMetadataViewModel)
        {
            var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
            var realColumns = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.ToList();
            var gisUploadAttemptID = gisUploadAttempt.GisUploadAttemptID;
            var gisFeatureList = HttpRequestStorage.DatabaseEntities.GisFeatures
                .Where(x => x.GisUploadAttemptID == gisUploadAttemptID).Include(x => x.GisFeatureMetadataAttributes).ToList();
            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns, gisFeatureList, gisUploadAttempt);

            var gisMetadataPostUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.GisMetadata(gisUploadAttempt.GisUploadAttemptID, null));
            var projectIndexUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Index());
            var gisMetadataAttributeIDs = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.Select(x => x.GisMetadataAttributeID).ToList();
            var metadataAttributes =
                HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Where(x =>
                    gisMetadataAttributeIDs.Contains(x.GisMetadataAttributeID));

            var viewData = new GisMetadataViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, gridSpec, metadataAttributes.ToList(), gisMetadataPostUrl, projectIndexUrl);
            return RazorView<GisMetadata, GisMetadataViewData, GisMetadataViewModel>(viewData, gisMetadataViewModel);
        }


        [GisAttemptUploadViewFeature]
        public GridJsonNetJObjectResult<GisFeature> GisRecordGridJsonData(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            var gisUploadAttemptID = gisUploadAttempt.GisUploadAttemptID;
            var realColumns = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.ToList();
            var gisFeatureList = HttpRequestStorage.DatabaseEntities.GisFeatures
                .Where(x => x.GisUploadAttemptID == gisUploadAttemptID).Include(x => x.GisFeatureMetadataAttributes).ToList();

            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns, gisFeatureList, gisUploadAttempt);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GisFeature>(gisFeatureList, gridSpec);
            return gridJsonNetJObjectResult;
        }


        public static void ImportProjects(GisUploadAttempt gisUploadAttempt, GisMetadataViewModel viewModel, out List<ProjectSimpleForGisLogging> newProjectListLog, out List<ProjectSimpleForGisLogging> skippedProjectListLog, out List<ProjectSimpleForGisLogging> existingProjectListLog, IJobCancellationToken jobCancellationToken)
        {
            SitkaHttpApplication.Logger.Info($"starting ImportProjects() for gisUploadAttempt:{gisUploadAttempt.GisUploadAttemptID}");
            var projectIdentifierMetadataAttribute = GenerateSingleMetadataAttribute(gisUploadAttempt, viewModel.ProjectIdentifierMetadataAttributeID);
            var gisFeatureIDs = gisUploadAttempt.GisFeatures.Select(x => x.GisFeatureID).ToHashSet();

            var gisExcludeIncludeList = gisUploadAttempt.GisUploadSourceOrganization.GisExcludeIncludeColumns.ToList();
            if (gisExcludeIncludeList.Any())
            {
                gisFeatureIDs = FilterListBasedOnIncludeExcludeCriteria(gisExcludeIncludeList, gisFeatureIDs);
            }


            var completionDateDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.CompletionDateMetadataAttributeID);
            var projectNameDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.ProjectNameMetadataAttributeID);
            var privateLandOwnerDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.PrivateLandownerMetadataAttributeID);
            var primaryContactDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.PrimaryContactMetadataAttributeID);
            var startDateDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.StartDateMetadataAttributeID);
            var projectStageDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.ProjectStageMetadataAttributeID);

            var distinctProjectIdentifiers = GetDistinctProjectIdentifiers(projectIdentifierMetadataAttribute, gisFeatureIDs);
            var projectImportBlockList = HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists.Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();


            var otherProjectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList().Single(x => string.Equals("Other", x.ProjectTypeName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            newProjectListLog = new List<ProjectSimpleForGisLogging>();
            existingProjectListLog = new List<ProjectSimpleForGisLogging>();
            skippedProjectListLog = new List<ProjectSimpleForGisLogging>();

            var projectList = new List<Project>();
            var newPersonList = new List<Person>();
            var newProjectPersonList = new List<ProjectPerson>();
            var projectLocationList = new List<ProjectLocation>();
            var existingPersons = HttpRequestStorage.DatabaseEntities.People.ToList();

            var gisCrossWalkDefaultList = HttpRequestStorage.DatabaseEntities.GisCrossWalkDefaults.ToList();

            var currentCounter = CalculateNextProjectNumberForThisYear();

            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var existingProjects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => projectProgramList.Contains(x.ProjectID)).ToList();
            if (gisUploadAttempt.GisUploadSourceOrganization.GisUploadProgramMergeGroupingID.HasValue)
            {
                var mergeGrouping = gisUploadAttempt.GisUploadSourceOrganization.GisUploadProgramMergeGrouping;
                var programsFromMergeGrouping = mergeGrouping.GisUploadSourceOrganizations.Select(x => x.Program).ToList();
                var programIdList = programsFromMergeGrouping.Select(x => x.ProgramID).ToList();
                var projectPrograms = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                    .Where(x => programIdList.Contains(x.ProgramID));
                var allMergeGroupingProjects = projectPrograms.Select(x => x.ProjectID).ToList();
                existingProjects = HttpRequestStorage.DatabaseEntities.Projects.Include(x => x.Treatments).Where(x => allMergeGroupingProjects.Contains(x.ProjectID)).ToList();
            }

            //Skip projects by removing distinctProjectIdentifiers which are matches in import block list, strictly by ProjectGisIdentifier (no ProjectName criteria)
            foreach (var blockListEntry in projectImportBlockList)
            {
                if (!string.IsNullOrEmpty(blockListEntry.ProjectGisIdentifier) && string.IsNullOrEmpty(blockListEntry.ProjectName))
                {
                    if (distinctProjectIdentifiers.Remove(blockListEntry.ProjectGisIdentifier))
                    {
                        skippedProjectListLog.Add(new ProjectSimpleForGisLogging(blockListEntry.ProjectGisIdentifier, string.Empty));
                    }
                }
                jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
            }

            MakeProjectsAndSave(distinctProjectIdentifiers, existingProjects, projectImportBlockList, projectIdentifierMetadataAttribute, completionDateDictionary, startDateDictionary, projectNameDictionary, projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, otherProjectType, projectList, gisUploadAttempt.GisUploadSourceOrganization.Program, currentCounter, ref existingProjectListLog, ref skippedProjectListLog, ref newProjectListLog, jobCancellationToken);

            MakeProjectLocationsAndSave(gisUploadAttempt, distinctProjectIdentifiers, projectIdentifierMetadataAttribute, projectLocationList, gisUploadAttempt.GisUploadSourceOrganization.ProgramID, jobCancellationToken);

            MakeProjectPeopleAndSave(gisUploadAttempt, distinctProjectIdentifiers, projectIdentifierMetadataAttribute, privateLandOwnerDictionary, primaryContactDictionary, existingPersons, newPersonList, newProjectPersonList);

            var leadImplementerDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.LeadImplementerMetadataAttributeID);
            MakeProjectOrganizationsAndSave(gisUploadAttempt, distinctProjectIdentifiers, gisCrossWalkDefaultList, leadImplementerDictionary, projectIdentifierMetadataAttribute);

            if (!gisUploadAttempt.GisUploadSourceOrganization.ImportAsDetailedLocationInsteadOfTreatments)
            {
                ExecProcImportTreatmentsFromGisUploadAttempt(gisUploadAttempt.GisUploadAttemptID
                    , viewModel.ProjectIdentifierMetadataAttributeID
                    , viewModel.FootprintAcresMetadataAttributeID
                    , viewModel.TreatedAcresMetadataAttributeID
                    , viewModel.TreatmentTypeMetadataAttributeID
                    , viewModel.TreatmentDetailedActivityTypeMetadataAttributeID
                    , gisUploadAttempt.GisUploadSourceOrganization
                    , viewModel.PruningAcresMetadataAttributeID
                    , viewModel.ThinningAcresMetadataAttributeID
                    , viewModel.ChippingAcresMetadataAttributeID
                    , viewModel.MasticationAcresMetadataAttributeID
                    , viewModel.GrazingAcresMetadataAttributeID
                    , viewModel.LopScatAcresMetadataAttributeID
                    , viewModel.BiomassRemovalAcresMetadataAttributeID
                    , viewModel.HandPileAcresMetadataAttributeID
                    , viewModel.HandPileBurnAcresMetadataAttributeID
                    , viewModel.MachinePileBurnAcresMetadataAttributeID
                    , viewModel.BroadcastBurnAcresMetadataAttributeID
                    , viewModel.OtherAcresMetadataAttributeID
                    , viewModel.StartDateMetadataAttributeID
                    , viewModel.CompletionDateMetadataAttributeID);
            }

            UpdateProjectPriorityLandscapes(gisUploadAttempt, distinctProjectIdentifiers, viewModel.ProjectIdentifierMetadataAttributeID);

            UpdateProjectRegions(gisUploadAttempt, distinctProjectIdentifiers, viewModel.ProjectIdentifierMetadataAttributeID);

            UpdateProjectCounties(gisUploadAttempt, distinctProjectIdentifiers, viewModel.ProjectIdentifierMetadataAttributeID);

            UpdateProjectTypesIfNeeded(gisUploadAttempt);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            ExecPClearGisImportTables();

            SitkaHttpApplication.Logger.Info($"ending ImportProjects() for gisUploadAttempt:{gisUploadAttempt.GisUploadAttemptID}");
        }

        [HttpPost]
        [GisAttemptUploadViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult GisMetadata(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey, GisMetadataViewModel viewModel)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            ImportProjects(gisUploadAttempt, viewModel, out var newProjectListLog, out var skippedProjectListLog, out var existingProjectListLog, null);

            var message = new StringBuilder();
            message.Append($"Successfully imported {newProjectListLog.Count} new {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

            if (newProjectListLog.Count > 0)
            {
                message.Append($" New {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are:<br/> {string.Join("<br/>", newProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))} ");
            }

            message.Append($"<br/>Successfully updated {existingProjectListLog.Count} existing {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

            if (existingProjectListLog.Count > 0)
            {
                message.Append($" Updated {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are:<br/> {string.Join("<br/>", existingProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))}  ");
            }

            message.Append($"<br/>Skipped adding/updating {skippedProjectListLog.Count} {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}. ");
            if (skippedProjectListLog.Count > 0)
            {
                message.Append($"Skipped {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are:<br/> {string.Join("<br/>", skippedProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))}");
            }
            
            SetMessageForDisplay(message.ToString());
            Logger.Info(message.Replace("<br/>", " "));

            return new ModalDialogFormJsonResult();
        }

        private static HashSet<int> FilterListBasedOnIncludeExcludeCriteria(List<GisExcludeIncludeColumn> gisExcludeIncludeList, HashSet<int> gisFeatureIDs)
        {
            foreach (var gisExcludeIncludeColumn in gisExcludeIncludeList)
            {
                var gisMetadataAttribute = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.SingleOrDefault(
                    x => string.Equals(x.GisMetadataAttributeName,
                        gisExcludeIncludeColumn.GisDefaultMappingColumnName));
                if (gisMetadataAttribute != null)
                {
                    var includeExcludeValues = gisExcludeIncludeColumn.GisExcludeIncludeColumnValues.ToList();
                    var excludeIncludeList = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes
                        .Where(x => gisFeatureIDs.Contains(x.GisFeatureID) &&
                                    x.GisMetadataAttributeID == gisMetadataAttribute.GisMetadataAttributeID).ToList();
                    var filteredIncludeExcludeList = new List<GisFeatureMetadataAttribute>();
                    foreach (var gisExcludeIncludeColumnValue in includeExcludeValues)
                    {
                        var listToAdd = excludeIncludeList.Where(x =>
                            string.Equals(x.GisFeatureMetadataAttributeValue,
                                gisExcludeIncludeColumnValue.GisExcludeIncludeColumnValueForFiltering)).ToList();
                        filteredIncludeExcludeList.AddRange(listToAdd);
                    }

                    if (gisExcludeIncludeColumn.IsWhitelist)
                    {
                        var listOfGisFeaturesToKeep = filteredIncludeExcludeList.Select(x => x.GisFeatureID).ToList();
                        var distinctListOfFeaturesToKeep = listOfGisFeaturesToKeep.Distinct().ToHashSet();
                        gisFeatureIDs = distinctListOfFeaturesToKeep;
                    }

                    if (!gisExcludeIncludeColumn.IsWhitelist)
                    {
                        var listOfGisFeaturesToNix = filteredIncludeExcludeList.Select(x => x.GisFeatureID).ToList();
                        var distinctListOfFeaturesToNix = listOfGisFeaturesToNix.Distinct().ToHashSet();
                        gisFeatureIDs = gisFeatureIDs.Except(distinctListOfFeaturesToNix).ToHashSet();
                    }
                }
            }

            return gisFeatureIDs;
        }

        private static void MakeProjectOrganizationsAndSave(GisUploadAttempt gisUploadAttempt, List<string> distinctProjectIdentifiers, List<GisCrossWalkDefault> gisCrossWalkDefaultList, Dictionary<int, List<GisFeatureMetadataAttribute>> leadImplementerDictionary, GisMetadataAttribute projectIdentifierMetadataAttribute)
        {
            SitkaHttpApplication.Logger.Info($"starting MakeProjectOrganizationsAndSave() for gisUploadAttempt:{gisUploadAttempt.GisUploadAttemptID}");
            var projectOrganizationsToAdd = UpdateProjectOrganizationRecords(gisUploadAttempt, distinctProjectIdentifiers, gisCrossWalkDefaultList, leadImplementerDictionary,  projectIdentifierMetadataAttribute);
            HttpRequestStorage.DatabaseEntities.ProjectOrganizations.AddRange(projectOrganizationsToAdd);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void MakeProjectPeopleAndSave(GisUploadAttempt gisUploadAttempt,
            List<string> distinctProjectIdentifiers,
            GisMetadataAttribute projectIdentifierMetadataAttribute,
            Dictionary<int, List<GisFeatureMetadataAttribute>> privateLandOwnerDictionary,
            Dictionary<int, List<GisFeatureMetadataAttribute>> primaryContactDictionary,
            List<Person> existingPersons, List<Person> newPersonList, List<ProjectPerson> newProjectPersonList)
        {
            SitkaHttpApplication.Logger.Info($"starting MakeProjectPeopleAndSave() for gisUploadAttempt:{gisUploadAttempt.GisUploadAttemptID}");
            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var projectsInProjectProgramList = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectPeople)
                .Where(x => projectProgramList.Contains(x.ProjectID))
                .ToList();
            var existingProjectsAfterSaveWithProjectPeople = projectsInProjectProgramList
                .Where(x => distinctProjectIdentifiers.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            foreach (var distinctProjectIdentifier in distinctProjectIdentifiers)
            {
                var project = existingProjectsAfterSaveWithProjectPeople.SingleOrDefault(x => string.Equals(x.ProjectGisIdentifier,
                    distinctProjectIdentifier, StringComparison.InvariantCultureIgnoreCase));
                if (project != null)
                {
                    var gisFeaturesIdListWithProjectIdentifier =
                        projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                            string.Equals(x.GisFeatureMetadataAttributeValue, distinctProjectIdentifier,
                                StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();

                    var privateLandownerAttributes = gisFeaturesIdListWithProjectIdentifier
                        .Where(privateLandOwnerDictionary.ContainsKey)
                        .SelectMany(x => privateLandOwnerDictionary[x]).ToList();
                    var landOwners = privateLandownerAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                    GenerateProjectPersonListForPrivateLandowners(existingPersons, newPersonList, newProjectPersonList, landOwners, project);

                    var primaryContactAttributes = gisFeaturesIdListWithProjectIdentifier
                        .Where(primaryContactDictionary.ContainsKey)
                        .SelectMany(x => primaryContactDictionary[x]).ToList();
                    var primaryContactEmails = primaryContactAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                    GenerateProjectPersonListForPrimaryContact(existingPersons, newProjectPersonList, primaryContactEmails, project);
                }
            }

            HttpRequestStorage.DatabaseEntities.People.AddRange(newPersonList);
            HttpRequestStorage.DatabaseEntities.ProjectPeople.AddRange(newProjectPersonList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void MakeProjectLocationsAndSave(GisUploadAttempt gisUploadAttempt, List<string> distinctProjectIdentifiers,
            GisMetadataAttribute projectIdentifierMetadataAttribute, List<ProjectLocation> projectLocationList, int programID, IJobCancellationToken jobCancellationToken)
        {
            SitkaHttpApplication.Logger.Info($"starting MakeProjectLocationsAndSave() for gisUploadAttempt:{gisUploadAttempt.GisUploadAttemptID}");
            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var existingProjectsAfterSaveWithProjectLocations = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectLocations)
                .Where(x => projectProgramList.Contains(x.ProjectID))
                .ToList()
                .Where(x => x.ProjectGisIdentifier != null)
                .Where(x => distinctProjectIdentifiers.Contains(x.ProjectGisIdentifier.Trim(),
                    StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            foreach (var distinctProjectIdentifier in distinctProjectIdentifiers)
            {
                if (gisUploadAttempt.GisUploadSourceOrganization.ImportAsDetailedLocationInsteadOfTreatments || gisUploadAttempt
                    .GisUploadSourceOrganization.ImportAsDetailedLocationInAdditionToTreatments)
                {
                    var project = existingProjectsAfterSaveWithProjectLocations.Single(x =>
                        string.Equals(x.ProjectGisIdentifier,
                            distinctProjectIdentifier, StringComparison.InvariantCultureIgnoreCase));
                    project.ProjectLocations.Where(x => x.ProjectLocationType == ProjectLocationType.ProjectArea && x.ProgramID == programID).ToList()
                        .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
                    var gisFeaturesIdListWithProjectIdentifier =
                        projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                            string.Equals(x.GisFeatureMetadataAttributeValue, distinctProjectIdentifier,
                                StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToHashSet();
                    var gisFeatures = gisUploadAttempt.GisFeatures
                        .Where(x => gisFeaturesIdListWithProjectIdentifier.Contains(x.GisFeatureID)).ToList();
                    var importedProjectAreaIndex = 1;
                    foreach (var gisFeature in gisFeatures)
                    {
                        var newProjectLocation = new ProjectLocation(project, gisFeature.GisFeatureGeometry,
                            ProjectLocationType.ProjectArea, $"Imported Project Area {importedProjectAreaIndex}");
                        newProjectLocation.ProgramID = programID;
                        projectLocationList.Add(newProjectLocation);
                        importedProjectAreaIndex++;
                    }

                    var features = gisFeatures.Select(x => x.GisFeatureGeometry).Where(x => x != null).ToList();
                    features.AddRange(project.ProjectLocations.Where(x => x.ProjectLocationType.ProjectLocationTypeID == ProjectLocationType.ProjectArea.ProjectLocationTypeID && x.ProgramID != programID).Select(x => x.ProjectLocationGeometry));
                        
                    var combinedGeometry = features.FirstOrDefault();

                    if (combinedGeometry != null)
                    {
                        features.ToList().ForEach(x => combinedGeometry.Union(x));
                    }

                    var centroid = combinedGeometry?.Centroid;
                    if (centroid != null)
                    {
                        project.ProjectLocationPoint = centroid;
                        project.ProjectLocationSimpleTypeID = ProjectLocationSimpleType.PointOnMap.ProjectLocationSimpleTypeID;
                    }
                }
                jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
            }

            HttpRequestStorage.DatabaseEntities.ProjectLocations.AddRange(projectLocationList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void MakeProjectsAndSave(List<string> distinctProjectIdentifiers, List<Project> existingProjects, List<ProjectImportBlockList> projectImportBlockList,
            GisMetadataAttribute projectIdentifierMetadataAttribute, Dictionary<int, List<GisFeatureMetadataAttribute>> completionDateDictionary,
            Dictionary<int, List<GisFeatureMetadataAttribute>> startDateDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> projectNameDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> projectStageDictionary,
            List<GisCrossWalkDefault> gisCrossWalkDefaultList, GisUploadAttempt gisUploadAttempt, ProjectType otherProjectType, List<Project> projectList, Program program,
            int currentCounter, ref List<ProjectSimpleForGisLogging> existingProjectListLog, ref List<ProjectSimpleForGisLogging> skippedProjectListLog, ref List<ProjectSimpleForGisLogging> newProjectListLog, IJobCancellationToken jobCancellationToken)
        {
            SitkaHttpApplication.Logger.Info($"starting MakeProjectsAndSave() for gisUploadAttempt:{gisUploadAttempt.GisUploadAttemptID}");
            foreach (var distinctProjectIdentifier in distinctProjectIdentifiers)
            {
                MakeProject(existingProjects, projectImportBlockList, projectIdentifierMetadataAttribute, distinctProjectIdentifier,
                    completionDateDictionary, startDateDictionary, projectNameDictionary,
                    projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, otherProjectType,
                    gisUploadAttempt.GisUploadAttemptID, projectList, gisUploadAttempt.GisUploadSourceOrganization, program,
                    ref currentCounter, ref existingProjectListLog, ref skippedProjectListLog, ref newProjectListLog);

                jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
            }

            HttpRequestStorage.DatabaseEntities.Projects.AddRange(projectList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void UpdateProjectRegions(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt, int? gisMetadataAttributeIdentier)
        {
            HttpRequestStorage.DatabaseEntities.GetObjectContext().CommandTimeout = 500;
            var projectUplandDnrRegionsCalculated = HttpRequestStorage.DatabaseEntities
                .GetfGetProjectDnrUploadRegions(gisUploadAttempt.GisUploadAttemptID, gisMetadataAttributeIdentier, gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();
            var projectRegions = projectUplandDnrRegionsCalculated
                .Select(x => new ProjectRegion(x.ProjectID, x.DNRUplandRegionID)).ToList();

            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var projectsWithUplandDnrRegions = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectRegions)
                .Where(x => projectProgramList.Contains(x.ProjectID))
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var projectsWithUplandRegionsToDeleteAkList = projectsWithUplandDnrRegions
                .SelectMany(p => p.ProjectRegions
                    .Select(pr => new { pr.ProjectID, pr.DNRUplandRegionID })).ToHashSet();

            var allProjectUplandRegionsAkList = HttpRequestStorage.DatabaseEntities.ProjectRegions.Select(x => new {x.ProjectID, x.DNRUplandRegionID}).ToHashSet();
            var allProjectUplandRegionsAkListExceptDeletes = allProjectUplandRegionsAkList.Except(projectsWithUplandRegionsToDeleteAkList).ToHashSet();

            projectsWithUplandDnrRegions.SelectMany(x => x.ProjectRegions)
                .ToList()
                .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));

            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            var projectUplandRegionsToAdd = projectRegions.Where(x =>
                    !allProjectUplandRegionsAkListExceptDeletes.Contains(new {x.ProjectID, x.DNRUplandRegionID}))
                .ToList();

            HttpRequestStorage.DatabaseEntities.ProjectRegions.AddRange(projectUplandRegionsToAdd);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            AddProjectCoordinators(gisUploadAttempt, projectsWithUplandDnrRegions);
        }

        /// <summary>For any new LOA projects that have an associated DNR Upland Region, add the associated DNR Service Forestry Regional Coordinator to the project with the Coordinator role.</summary>
        private static void AddProjectCoordinators(GisUploadAttempt gisUploadAttempt, List<Project> projectsWithDnrUplandRegions)
        {
            List<ProjectPerson> coordinatorsToAdd = new List<ProjectPerson>();

            var newLoaProjects = projectsWithDnrUplandRegions
                .Where(x => x.IsInLandownerAssistanceProgram
                            && x.CreateGisUploadAttemptID != null
                            && x.CreateGisUploadAttemptID == gisUploadAttempt.GisUploadAttemptID);

            var dnrUplandRegions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions;

            foreach (var project in newLoaProjects)
            {
                var coordinators = project.ProjectRegions
                    .Join(dnrUplandRegions,
                        pr => pr.DNRUplandRegionID,
                        dur => dur.DNRUplandRegionID,
                        (pr, dur) => dur)
                    .Select(x => x.DNRUplandRegionCoordinator)
                    .Distinct();

                foreach (var coordinator in coordinators)
                {
                    if (coordinator != null)
                        coordinatorsToAdd.Add(new ProjectPerson(project, coordinator,
                            ProjectPersonRelationshipType.ServiceForestryRegionalCoordinator));
                }
            }

            if (coordinatorsToAdd.Count > 0)
            {
                HttpRequestStorage.DatabaseEntities.ProjectPeople.AddRange(coordinatorsToAdd);
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            }
        }

        private static void UpdateProjectPriorityLandscapes(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt, int? gisMetadataAttributeIdentier)
        {
            HttpRequestStorage.DatabaseEntities.GetObjectContext().CommandTimeout = 500;
            var projectPriorityLandscapesCalculated = HttpRequestStorage.DatabaseEntities
                .GetfGetProjectPriorityLandscapes(gisUploadAttempt.GisUploadAttemptID, gisMetadataAttributeIdentier, gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();
            var projectPriorityLandscapes = projectPriorityLandscapesCalculated
                .Select(x => new ProjectPriorityLandscape(x.ProjectID, x.PriorityLandscapeID)).ToList();
            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var projectsWithPriorityLandscapes = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectPriorityLandscapes)
                .Where(x => projectProgramList.Contains(x.ProjectID))
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var projectsWithPriorityLandscapesToDelete = projectsWithPriorityLandscapes
                .SelectMany(x => x.ProjectPriorityLandscapes)
                .ToList();

            var projectsWithPriorityLandscapesToDeleteAkList = projectsWithPriorityLandscapesToDelete
                .Select(x => new {x.ProjectID, x.PriorityLandscapeID}).ToHashSet();

            var allProjectPriorityLandscapesAkList = HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapes
                .Select(x => new { x.ProjectID, x.PriorityLandscapeID }).ToHashSet();
            var allProjectPriorityLandscapesExceptDeletes = allProjectPriorityLandscapesAkList.Where(x =>
                !projectsWithPriorityLandscapesToDeleteAkList.Contains(x));

            projectsWithPriorityLandscapesToDelete.ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            var projectPriorityLandscapesToAdd = projectPriorityLandscapes.Where(x =>
                    !allProjectPriorityLandscapesExceptDeletes.Contains(new { x.ProjectID, x.PriorityLandscapeID }))
                .ToList();

            HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapes.AddRange(projectPriorityLandscapesToAdd);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void UpdateProjectCounties(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt, int? gisMetadataAttributeIdentier)
        {
            HttpRequestStorage.DatabaseEntities.GetObjectContext().CommandTimeout = 500;
            var projectCountiesCalculated = HttpRequestStorage.DatabaseEntities
                .GetfGetUploadProgramCountys(gisUploadAttempt.GisUploadAttemptID, gisMetadataAttributeIdentier, gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();
            var projectCounties = projectCountiesCalculated
                .Select(x => new ProjectCounty(x.ProjectID, x.CountyID)).ToList();
            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var projectsWithCounties = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectCounties)
                .Where(x => projectProgramList.Contains(x.ProjectID))
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var projectsWithCountiesToDelete = projectsWithCounties
                .SelectMany(x => x.ProjectCounties)
                .ToList();

            var projectsWithCountiesToDeleteAkList = projectsWithCountiesToDelete
                .Select(x => new {x.ProjectID, x.CountyID}).ToHashSet();

            var allProjectCounties = HttpRequestStorage.DatabaseEntities.ProjectCounties.ToList().Select(x => new {x.ProjectID, x.CountyID}).ToHashSet();
            var allProjectCountiesExceptDeletes = allProjectCounties.Where(x =>
                !projectsWithCountiesToDeleteAkList.Contains(x)).ToHashSet();

            projectsWithCountiesToDelete.ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));

            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            var projectCountiesToAdd = projectCounties.Where(x =>
                    !allProjectCountiesExceptDeletes.Contains(new {x.ProjectID, x.CountyID}))
                .ToList();

            HttpRequestStorage.DatabaseEntities.ProjectCounties.AddRange(projectCountiesToAdd);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static List<ProjectOrganization> UpdateProjectOrganizationRecords(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt, List<GisCrossWalkDefault> gisCrossWalkDefaultList, Dictionary<int, List<GisFeatureMetadataAttribute>> leadImplementerDictionary, GisMetadataAttribute projectIdentifierMetadataAttribute)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList();
            var defaultLeadImplementerOrganization = gisUploadAttempt.GisUploadSourceOrganization.DefaultLeadImplementerOrganization;
            var projectProgramList = HttpRequestStorage.DatabaseEntities.ProjectPrograms
                .Where(x => x.ProgramID == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).Select(x => x.ProjectID).ToList();
            var projectsWithOrganization = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectOrganizations)
                .Where(x => projectProgramList.Contains(x.ProjectID))
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            var relationshipType = gisUploadAttempt.GisUploadSourceOrganization.RelationshipTypeForDefaultOrganization;

            projectsWithOrganization.SelectMany(x => x.ProjectOrganizations)
                .Where(x => x.RelationshipTypeID == gisUploadAttempt.GisUploadSourceOrganization.RelationshipTypeForDefaultOrganizationID)
                .ToList()
                .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));

            var leadImplementerCrossWalks = gisCrossWalkDefaultList.Where(x =>
                x.FieldDefinitionID == FieldDefinition.LeadImplementerOrganization.FieldDefinitionID &&
                x.GisUploadSourceOrganizationID == gisUploadAttempt.GisUploadSourceOrganizationID).ToList();

            var projectOrganizationsToAdd = new List<ProjectOrganization>();
            foreach (var project in projectsWithOrganization)
            {

                var trimmedDistinctGisValue = project.ProjectGisIdentifier.Trim();

                var gisFeaturesIdListWithProjectIdentifier =
                    projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x => x.GisFeatureMetadataAttributeValue != null).Where(x =>
                        string.Equals(x.GisFeatureMetadataAttributeValue.Trim(), trimmedDistinctGisValue,
                            StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();


                var existingProjectOrganizations = project.ProjectOrganizations.Where(x => x.RelationshipTypeID ==
                                                    gisUploadAttempt.GisUploadSourceOrganization.RelationshipTypeForDefaultOrganizationID);
                var existingProjectOrganization = existingProjectOrganizations.FirstOrDefault(x => x.OrganizationID == defaultLeadImplementerOrganization.OrganizationID);
                if (existingProjectOrganization == null)
                {
                    var leadImplementerAttributes = gisFeaturesIdListWithProjectIdentifier
                        .Where(leadImplementerDictionary.ContainsKey)
                        .SelectMany(x => leadImplementerDictionary[x]).ToList();

                    var leadImplementers = leadImplementerAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();
                    var leadImplementerString = leadImplementers.FirstOrDefault();
                    var successfullyMappedLeadImplementer = false;
                    if (!string.IsNullOrEmpty(leadImplementerString))
                    {
                        var leadImplementerMappedString = leadImplementerCrossWalks.SingleOrDefault(x =>
                                x.GisCrossWalkSourceValue.Equals(leadImplementerString, StringComparison.InvariantCultureIgnoreCase))?
                            .GisCrossWalkMappedValue;

                        if (!string.IsNullOrEmpty(leadImplementerMappedString))
                        {
                            var organization = organizations.FirstOrDefault(x =>
                                x.OrganizationName.Equals(leadImplementerMappedString,
                                    StringComparison.CurrentCultureIgnoreCase));
                            if (organization != null)
                            {
                                successfullyMappedLeadImplementer = true;
                                projectOrganizationsToAdd.Add(new ProjectOrganization(project, organization, relationshipType));
                            }
                        }
                    }

                    if (!successfullyMappedLeadImplementer)
                    {
                        projectOrganizationsToAdd.Add(new ProjectOrganization(project, defaultLeadImplementerOrganization, relationshipType));
                    }
                }
            }

            return projectOrganizationsToAdd;


        }

        private static List<string> GetDistinctProjectIdentifiers(GisMetadataAttribute projectIdentifierMetadataAttribute,
            HashSet<int> gisFeatureIDs)
        {
            var projectIdentifierValues =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                    gisFeatureIDs.Contains(x.GisFeatureID));
            var distinctProjectIdentifiers = projectIdentifierValues.Select(x => x.GisFeatureMetadataAttributeValue).Where(y => !string.IsNullOrWhiteSpace(y)).Select(x => x.ToUpperInvariant().Trim()).Distinct().OrderBy(x => x).ToList();
            return distinctProjectIdentifiers;
        }

        private static GisMetadataAttribute GenerateSingleMetadataAttribute(GisUploadAttempt gisUploadAttempt,
            int viewModelProjectIdentifierMetadataAttributeID)
        {
            var projectIdentifierMetadataAttribute =
                gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.Single(x =>
                    x.GisMetadataAttributeID == viewModelProjectIdentifierMetadataAttributeID).GisMetadataAttribute;
            return projectIdentifierMetadataAttribute;
        }

        private static Dictionary<int, List<GisFeatureMetadataAttribute>> GenerateMetadataValueDictionary(HashSet<int> gisFeatureIDs,
            int? metadataAttributeID)
        {
            var metadataValueDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                    gisFeatureIDs.Contains(x.GisFeatureID) &&
                    x.GisMetadataAttributeID == metadataAttributeID).ToList().GroupBy(y => y.GisFeatureID)
                .ToDictionary(y => y.Key, x => x.ToList());
            return metadataValueDictionary;
        }

        private static int CalculateNextProjectNumberForThisYear()
        {
            var currentCounter = 1;
            var lastProjectCreatedThisYear = HttpRequestStorage.DatabaseEntities.Projects
                .Where(p => p.FhtProjectNumber.Contains(DateTime.Now.Year.ToString()))
                .OrderByDescending(p => p.FhtProjectNumber).ToList()
                .FirstOrDefault(p => p.FhtProjectNumber.StartsWith($"FHT-{DateTime.Now.Year}"));
            if (lastProjectCreatedThisYear != null)
            {
                var splitFhtProjectNumber = lastProjectCreatedThisYear.FhtProjectNumber.Split('-');
                Int32.TryParse(splitFhtProjectNumber[2], out currentCounter);
                currentCounter++;
            }

            return currentCounter;
        }

        private static void UpdateProjectTypesIfNeeded(GisUploadAttempt gisUploadAttempt)
        {
            if (gisUploadAttempt.GisUploadSourceOrganization.AdjustProjectTypeBasedOnTreatmentTypes)
            {
                var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList();
                var projects = HttpRequestStorage.DatabaseEntities.Projects
                    .Where(x => (x.CreateGisUploadAttemptID == gisUploadAttempt.GisUploadAttemptID && !x.LastUpdateGisUploadAttemptID.HasValue) || (x.LastUpdateGisUploadAttemptID.HasValue && x.LastUpdateGisUploadAttemptID.Value == gisUploadAttempt.GisUploadAttemptID))
                    .Include(x => x.Treatments).ToList();
                foreach (var project in projects)
                {
                    var treatments = project.Treatments.ToList();

                    if (gisUploadAttempt.GisUploadSourceOrganization.ImportIsFlattened.HasValue &&
                        gisUploadAttempt.GisUploadSourceOrganization.ImportIsFlattened.Value)
                    {
                        treatments = treatments.Where(x => x.TreatmentTreatedAcres > 0).ToList();
                    }

                    var distinctTreatmentTypes = treatments.Select(x => x.TreatmentTypeID).Distinct().ToList();
                    if (distinctTreatmentTypes.Count == 1)
                    {
                        var treatmentTypeID = distinctTreatmentTypes.Single();
                        var treatmentType = TreatmentType.AllLookupDictionary[treatmentTypeID];
                        if (treatmentType == TreatmentType.Commercial)
                        {
                            var projectType = projectTypes.SingleOrDefault(x =>
                                string.Equals("Commercial vegetation treatment", x.ProjectTypeName.Trim(),
                                    StringComparison.InvariantCultureIgnoreCase));
                            if (projectType != null)
                            {
                                project.ProjectType = projectType;
                            }
                        }

                        if (treatmentType == TreatmentType.NonCommercial)
                        {
                            var projectType = projectTypes.SingleOrDefault(x =>
                                string.Equals("Non-commercial vegetation treatment", x.ProjectTypeName.Trim(),
                                    StringComparison.InvariantCultureIgnoreCase));
                            if (projectType != null)
                            {
                                project.ProjectType = projectType;
                            }
                        }

                        if (treatmentType == TreatmentType.PrescribedFire)
                        {
                            var projectType = projectTypes.SingleOrDefault(x =>
                                string.Equals("Prescribed fire treatment", x.ProjectTypeName.Trim(),
                                    StringComparison.InvariantCultureIgnoreCase));
                            if (projectType != null)
                            {
                                project.ProjectType = projectType;
                            }
                        }

                    }
                }
            }
        }

        private static void MakeProject(List<Project> existingProjects,
                                        List<ProjectImportBlockList> projectImportBlockList,
                                        GisMetadataAttribute projectIdentifierMetadataAttribute,
                                        string distinctGisValue,
                                        Dictionary<int, List<GisFeatureMetadataAttribute>> completionDateDictionary,
                                        Dictionary<int, List<GisFeatureMetadataAttribute>> startDateDictionary,
                                        Dictionary<int, List<GisFeatureMetadataAttribute>> projectNameDictionary,
                                        Dictionary<int, List<GisFeatureMetadataAttribute>> projectStageDictionary,
                                        List<GisCrossWalkDefault> gisCrossWalkDefaultList,
                                        GisUploadAttempt gisUploadAttempt,
                                        ProjectType otherProjectType,
                                        int gisUploadAttemptID,
                                        List<Project> projectList,
                                        GisUploadSourceOrganization gisUploadSourceOrganization,
                                        Program program,
                                        ref int newProjectNumberCounter,
                                        ref List<ProjectSimpleForGisLogging> existingProjectListLog,
                                        ref List<ProjectSimpleForGisLogging> skippedProjectListLog,
                                        ref List<ProjectSimpleForGisLogging> newProjectListLog)
        {

            var trimmedDistinctGisValue = distinctGisValue.Trim();
            var gisFeaturesIdListWithProjectIdentifier =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x => x.GisFeatureMetadataAttributeValue != null).Where(x =>
                    string.Equals(x.GisFeatureMetadataAttributeValue.Trim(), trimmedDistinctGisValue,
                        StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();
            

            var project = existingProjects.Where(x => x.ProjectGisIdentifier != null).SingleOrDefault(x => string.Equals(x.ProjectGisIdentifier.Trim(), trimmedDistinctGisValue, StringComparison.InvariantCultureIgnoreCase));

            var completionDate = CalculateCompletionDate(completionDateDictionary, gisFeaturesIdListWithProjectIdentifier, project, program.ProgramID);

            if (gisUploadAttempt.GisUploadSourceOrganization.RequiresCompletionDate() && !completionDate.HasValue)
            {
                return;
            }

            var projectAlreadyExists = project != null;

            var projectNumber = $"FHT-{DateTime.Now.Year}-{newProjectNumberCounter:00000}";
            var startDate = CalculateStartDate(startDateDictionary, gisFeaturesIdListWithProjectIdentifier, project, program.ProgramID);
            var projectName = CalculateProjectName(projectNameDictionary, gisFeaturesIdListWithProjectIdentifier);
            var projectStage = CalculateProjectStage(projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, gisFeaturesIdListWithProjectIdentifier, completionDate);
            var projectType = CalculateProjectType(gisUploadSourceOrganization);
            var projectDescription = gisUploadAttempt.GisUploadSourceOrganization.ProjectDescriptionDefaultText;


            //Skip projects which match the import block list on ProjectName and, if provided on the block list, ProjectGisIdentifier
            var blockListEntryNameMatch = projectImportBlockList.FirstOrDefault(x => x.ProjectName == projectName &&
                                                                            (x.ProjectGisIdentifier == null || x.ProjectGisIdentifier == distinctGisValue));
            if (blockListEntryNameMatch != null)
            {
                skippedProjectListLog.Add(new ProjectSimpleForGisLogging(blockListEntryNameMatch.ProjectGisIdentifier, blockListEntryNameMatch.ProjectName));
                return;
            }

            if (projectAlreadyExists)
            {
                //Skip projects which match on the FK ProjectID
                var blockListEntryProjectIdMatch = projectImportBlockList.FirstOrDefault(x => x.ProjectID == project.ProjectID);
                if (blockListEntryProjectIdMatch != null)
                {
                    skippedProjectListLog.Add(new ProjectSimpleForGisLogging(blockListEntryProjectIdMatch.ProjectGisIdentifier, blockListEntryProjectIdMatch.ProjectName));
                    return;
                }

                project.ProjectType = otherProjectType;
                project.ProjectTypeID = otherProjectType.ProjectTypeID;
                project.ProjectStageID = projectStage.ProjectStageID;
                project.ProjectName = projectName;
                project.LastUpdateGisUploadAttemptID = gisUploadAttemptID;
                if ((projectStage != ProjectStage.Planned)  
                    && (project.ProjectApprovalStatusID == ProjectApprovalStatus.Draft.ProjectApprovalStatusID
                    || project.ProjectApprovalStatusID == ProjectApprovalStatus.PendingApproval.ProjectApprovalStatusID))
                {
                    project.ProjectApprovalStatusID = ProjectApprovalStatus.Approved.ProjectApprovalStatusID;
                }
            }
            else
            {
                var approvalStatusID = ProjectApprovalStatus.Approved.ProjectApprovalStatusID;

                project = new Project(otherProjectType.ProjectTypeID,
                    projectStage.ProjectStageID,
                    projectName,
                    false,
                    ProjectLocationSimpleType.None.ProjectLocationSimpleTypeID,
                    approvalStatusID,
                    projectNumber
                );
                project.CreateGisUploadAttemptID = gisUploadAttemptID;
                project.ProjectGisIdentifier = trimmedDistinctGisValue;
            }

            var existingProjectProgram = project.ProjectPrograms.SingleOrDefault(x => x.ProgramID == program.ProgramID);
            if (existingProjectProgram == null)
            {
                project.ProjectPrograms.Add(new ProjectProgram(project.ProjectID, program.ProgramID));
            }


            if (gisUploadSourceOrganization.ApplyCompletedDateToProject)
            {
                project.CompletionDate = completionDate;
            }

            if (gisUploadSourceOrganization.ApplyStartDateToProject)
            {
                project.PlannedDate = startDate;
            }

            var existingProjectDescription = project.ProjectDescription;
            if (string.IsNullOrEmpty(existingProjectDescription))
            {
                project.ProjectDescription = projectDescription;
            }

            if (projectType != null)
            {
                project.ProjectType = projectType;
                project.ProjectTypeID = projectType.ProjectTypeID;
            }

            if (!projectAlreadyExists)
            {
                projectList.Add(project);
                newProjectListLog.Add(new ProjectSimpleForGisLogging(project));
                newProjectNumberCounter++;
            }
            else
            {
                existingProjectListLog.Add(new ProjectSimpleForGisLogging(project));
            }
        }

        private static ProjectType CalculateProjectType(GisUploadSourceOrganization gisUploadSourceOrganization)
        {
            var projectTypeMappedString = string.IsNullOrEmpty(gisUploadSourceOrganization.ProjectTypeDefaultName)
                ? string.Empty
                : gisUploadSourceOrganization.ProjectTypeDefaultName;
            var projectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.SingleOrDefault(x =>
                x.ProjectTypeName.Equals(projectTypeMappedString, StringComparison.InvariantCultureIgnoreCase));
            return projectType;
        }

        private static ProjectStage CalculateProjectStage(Dictionary<int, List<GisFeatureMetadataAttribute>> projectStageDictionary, List<GisCrossWalkDefault> gisCrossWalkDefaultList,
            GisUploadAttempt gisUploadAttempt, List<int> gisFeaturesIdListWithProjectIdentifier, DateTime? completionDate)
        {
            var projectStageAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(projectStageDictionary.ContainsKey)
                .SelectMany(x => projectStageDictionary[x]).ToList();
            ProjectStage projectStage = gisUploadAttempt.GisUploadSourceOrganization.ProjectStageDefault;
            projectStage = CalculateProjectStageIfNeeded(gisCrossWalkDefaultList, gisUploadAttempt, projectStageAttributes,
                completionDate, projectStage);
            return projectStage;
        }

        private static string CalculateProjectName(Dictionary<int, List<GisFeatureMetadataAttribute>> projectNameDictionary,
            List<int> gisFeaturesIdListWithProjectIdentifier)
        {
            var projectNameAttributes = gisFeaturesIdListWithProjectIdentifier.Where(projectNameDictionary.ContainsKey)
                .SelectMany(x => projectNameDictionary[x]).ToList();
            var projectNames = projectNameAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();

            if (projectNames.Count > 1)
            {
                var projectNameLower = projectNames.Select(x => x.ToUpperInvariant()).Distinct().ToList();
                if (projectNameLower.Count == 1)
                {
                    projectNames = projectNames.Take(1).ToList();
                }
                else
                {
                    var logger = LogManager.GetLogger(typeof(GisProjectBulkUpdateController));
                    logger.Warn($"Multiple Project Names found while trying to CalculateProjectName(). They are:{string.Join(", ", projectNames)}. Taking the first one for this project.");
                    projectNames = projectNames.Take(1).ToList();
                }
            }

            //GUT SHOT from USFS Silvitculture is bad SMILEY WOOD I also bad
            var projectName = projectNames.Where(x => x != "SMILEY WOOD I" && x != "(PALS)EAST FORK HUMPTULIPS VEGETATION MANAGEMENT PROJECT" && x != "GUT SHOT").SingleOrDefault();
            if (string.IsNullOrEmpty(projectName))
            {
                projectName = "Default Project Name";
            }

            return projectName;
        }

        private static void GenerateProjectPersonListForPrivateLandowners(List<Person> existingPersonList, List<Person> newPersonList, List<ProjectPerson> newProjectPersonList,
            List<string> landOwners, Project project)
        {
            if (landOwners.Any())
            {
                project.ProjectPeople.Where(x => x.ProjectPersonRelationshipType == ProjectPersonRelationshipType.PrivateLandowner).ToList().ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
                foreach (var landOwner in landOwners)
                {
                    var firstName = ExtractFirstName(landOwner);
                    var lastName = ExtractLastName(landOwner);

                    var existingPersons = existingPersonList.Where(x =>
                        string.Equals(x.FirstName, firstName, StringComparison.InvariantCultureIgnoreCase) &&
                        (string.Equals(x.LastName, lastName, StringComparison.InvariantCultureIgnoreCase) ||
                         (string.IsNullOrEmpty(x.LastName) && string.IsNullOrEmpty(lastName)))).ToList();

                    var existingNewPersons = newPersonList.Where(x =>
                        string.Equals(x.FirstName, firstName, StringComparison.InvariantCultureIgnoreCase) &&
                        (string.Equals(x.LastName, lastName, StringComparison.InvariantCultureIgnoreCase) ||
                         (string.IsNullOrEmpty(x.LastName) && string.IsNullOrEmpty(lastName)))).ToList();
                    if (existingPersons.Any())
                    {
                        Person existingPerson;
                        if (existingPersons.Count() == 1)
                        {
                            existingPerson = existingPersons.Single();
                        }
                        else
                        {
                            existingPerson = existingPersons.OrderBy(x => x.CreateDate).First();
                        }

                        var projectPerson = new ProjectPerson(project, existingPerson,
                            ProjectPersonRelationshipType.PrivateLandowner);
                        newProjectPersonList.Add(projectPerson);
                    }
                    else if (existingNewPersons.Any())
                    {
                        Person existingNewPerson;
                        if (existingNewPersons.Count() == 1)
                        {
                            existingNewPerson = existingNewPersons.Single();
                        }
                        else
                        {
                            existingNewPerson = existingNewPersons.OrderBy(x => x.CreateDate).First();
                        }
                        var projectPerson = new ProjectPerson(project, existingNewPerson,
                            ProjectPersonRelationshipType.PrivateLandowner);
                        newProjectPersonList.Add(projectPerson);
                    }
                    else
                    {
                        var person = new Person(firstName, DateTime.Now, true, false) { LastName = lastName };
                        person.PersonRoles.Add(new PersonRole(person, Role.Unassigned));
                        newPersonList.Add(person);
                        var projectPerson = new ProjectPerson(project, person,
                            ProjectPersonRelationshipType.PrivateLandowner);
                        newProjectPersonList.Add(projectPerson);
                    }
                }
            }
        }

        private static void GenerateProjectPersonListForPrimaryContact(List<Person> existingPersonList, List<ProjectPerson> newProjectPersonList,
            List<string> primaryContactEmails, Project project)
        {
            if (!primaryContactEmails.Any())
            {
                return;
            }
            
            project.ProjectPeople.Where(x => x.ProjectPersonRelationshipType == ProjectPersonRelationshipType.PrimaryContact).ToList().ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
            var contactEmail = primaryContactEmails.First();// 7/17/23 TK - There should only be one Primary Contact on a project.
            
            var email = contactEmail.Trim();

            var existingPersons = existingPersonList.Where(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (existingPersons.Any())
            {
                Person existingPerson;
                if (existingPersons.Count() == 1)
                {
                    existingPerson = existingPersons.Single();
                }
                else
                {
                    existingPerson = existingPersons.OrderBy(x => x.CreateDate).First();
                }

                var projectPerson = new ProjectPerson(project, existingPerson, ProjectPersonRelationshipType.PrimaryContact);
                newProjectPersonList.Add(projectPerson);
            }
            
        }

        private static string ExtractLastName(string landOwner)
        {
            var landownerSplit = landOwner.Split(',');
            string lastName = null;
            if (landownerSplit.Length == 2)
            {
                lastName = landownerSplit[0].Trim();
            }

            return lastName;
        }

        private static string ExtractFirstName(string landOwner)
        {
            var landownerSplit = landOwner.Split(',');
            var firstName = string.Empty;
            if (landownerSplit.Length == 1 || landownerSplit.Length > 2)
            {
                firstName = landOwner.Trim();
            }

            if (landownerSplit.Length == 2)
            {
                firstName = landownerSplit[1].Trim();
            }

            return firstName;
        }

        private static DateTime? CalculateStartDate(Dictionary<int, List<GisFeatureMetadataAttribute>> startDateDictionary, List<int> gisFeaturesIdListWithProjectIdentifier, Project existingProject, int programID)
        {
            var startDateAttributes = gisFeaturesIdListWithProjectIdentifier.Where(startDateDictionary.ContainsKey)
                .SelectMany(x => startDateDictionary[x]).ToList();
            var startAttributes = startDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out _)).Select(DateTime.Parse).ToList();

            // 6/20/23 TK - we need this extra check for the two paths for importing projects. The GDB upload gives us a dateTime, but the ArcOnline endpoints provide an epoch date. So if the initial dateTime parsing fails we will attempt the epoch method here.
            if (!startAttributes.Any())
            {
                startAttributes = startDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                    .Where(x => long.TryParse(x, out _)).Select(y => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(y)).DateTime).ToList();
            }

            var startDate = startAttributes.Any() ? startAttributes.Min() : (DateTime?)null;

            if (existingProject != null)
            {
                var otherTreatments =
                    existingProject.Treatments.Where(x => x.ProgramID.HasValue && x.ProgramID.Value != programID).ToList();
                if (otherTreatments.Any())
                {
                    var otherTreatmentStartDates = otherTreatments.Min(x => x.TreatmentStartDate);
                    if (!startDate.HasValue || (otherTreatmentStartDates.HasValue &&
                                                otherTreatmentStartDates.Value < startDate.Value))
                    {
                        startDate = otherTreatmentStartDates;
                    }
                }
            }
            return startDate;
        }

        private static DateTime? CalculateCompletionDate(Dictionary<int, List<GisFeatureMetadataAttribute>> completionDateDictionary,
            List<int> gisFeaturesIdListWithProjectIdentifier, Project existingProject, int programID)
        {
            var completionDateAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(completionDateDictionary.ContainsKey)
                .SelectMany(x => completionDateDictionary[x]).ToList();

            var completionAttributes = completionDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out _)).Select(DateTime.Parse).ToList();

            // 6/20/23 TK - we need this extra check for the two paths for importing projects. The GDB upload gives us a dateTime, but the ArcOnline endpoints provide an epoch date. So if the initial dateTime parsing fails we will attempt the epoch method here.
            if (!completionAttributes.Any())
            {
                completionAttributes = completionDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                    .Where(x => long.TryParse(x, out _)).Select(y => DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(y)).DateTime).ToList();
            }

            var completionDate = completionAttributes.Any() ? completionAttributes.Max() : (DateTime?)null;

            if (existingProject != null)
            {
                var otherTreatments = existingProject.Treatments.Where(x => x.ProgramID.HasValue && x.ProgramID.Value != programID).ToList();
                if (otherTreatments.Any())
                {
                    var otherTreatmentEndDates = otherTreatments.Max(x => x.TreatmentEndDate);
                    if (!completionDate.HasValue || (otherTreatmentEndDates.HasValue &&
                                                     otherTreatmentEndDates.Value > completionDate.Value))
                    {
                        completionDate = otherTreatmentEndDates;
                    }
                }


            }
            return completionDate;
        }

        private static ProjectStage CalculateProjectStageIfNeeded(List<GisCrossWalkDefault> gisCrossWalkDefaultList,
            GisUploadAttempt gisUploadAttempt, List<GisFeatureMetadataAttribute> projectStageAttributes, DateTime? completionDate, ProjectStage projectStage)
        {
            if (gisUploadAttempt.GisUploadSourceOrganization.DataDeriveProjectStage)
            {
                var projectStages = projectStageAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();
                var projectStageString = projectStages.FirstOrDefault();


                var projectStageCrossWalks = gisCrossWalkDefaultList.Where(x =>
                    x.FieldDefinitionID == FieldDefinition.ProjectStage.FieldDefinitionID &&
                    x.GisUploadSourceOrganizationID == gisUploadAttempt.GisUploadSourceOrganizationID);

                if (!completionDate.HasValue)
                {
                    projectStage = ProjectStage.Planned;
                }

                if (!string.IsNullOrEmpty(projectStageString))
                {
                    var projectStageMappedString = projectStageCrossWalks.Single(x =>
                            x.GisCrossWalkSourceValue.Equals(projectStageString, StringComparison.InvariantCultureIgnoreCase))
                        .GisCrossWalkMappedValue;

                    if (!string.IsNullOrEmpty(projectStageMappedString))
                    {
                        projectStage = ProjectStage.All.SingleOrDefault(x =>
                            x.ProjectStageName.Equals(projectStageMappedString, StringComparison.InvariantCultureIgnoreCase));
                    }

                    if (projectStage == null)
                    {
                        projectStage = ProjectStage.Completed;
                    }
                }
            }

            return projectStage;
        }

        private ViewResult ViewUploadGisFile(UploadGisFileViewModel viewModel, GisUploadAttempt gisUploadAttempt)
        {
            var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
            var newGisUploadUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.UploadGisFile(gisUploadAttempt.GisUploadAttemptID, null));

            var viewData = new UploadGisFileViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, newGisUploadUrl);

            return RazorView<UploadGisFile, UploadGisFileViewData, UploadGisFileViewModel>(viewData, viewModel);
        }





        /// <summary>
        /// Dummy get signature so that it can find the post action
        /// </summary>
        [HttpGet]
        [GisAttemptUploadViewFeature]
        public ActionResult CheckStatusOfGisUploadAttempt(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            return Json(new GisUploadAttemptJson(gisUploadAttempt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [GisAttemptUploadViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UploadGisFile(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey, UploadGisFileViewModel viewModel)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            var gisUploadAttemptID = gisUploadAttempt.GisUploadAttemptID;
            return UploadGisFilePostImpl(gisUploadAttemptID, viewModel);
        }


        private ActionResult UploadGisFilePostImpl(int gisUploadAttemptID, UploadGisFileViewModel viewModel)
        {
            var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
            if (!ModelState.IsValid)
            {
                return ViewUploadGisFile(viewModel, gisUploadAttempt);
            }

            DropGisImportTable();

            var httpPostedFileBase = viewModel.FileResourceData;
            var shapeFileSuccessfullyExtractedToDisk = false;
            var shapeFilePath = GisUploadAttemptStaging.UnzipAndSaveFileToDiskIfShapefile(httpPostedFileBase, gisUploadAttempt, ref shapeFileSuccessfullyExtractedToDisk);
            FeatureCollection featureCollection;

            if (shapeFileSuccessfullyExtractedToDisk)
            {
                featureCollection = ImportShpToGeoJson(shapeFilePath);
            }

            else
            {
                var filePath = GisUploadAttemptStaging.SaveFileToDiskIfGdb(httpPostedFileBase, gisUploadAttempt);
                var fileInfo = new FileInfo(filePath);
                featureCollection = ImportGdbToGeoJson(fileInfo);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            gisUploadAttempt.ImportedToGeoJson = true;
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            SaveGisUploadToNormalizedFieldsUsingGeoJson(gisUploadAttempt, featureCollection);



            return new ModalDialogFormJsonResult();
        }

        private DatabaseEntities AddToContext(DatabaseEntities context,
            GisFeatureMetadataAttribute entity, int count, int commitCount, bool recreateContext)
        {
            context.Set<GisFeatureMetadataAttribute>().Add(entity);
            var connectionString = context.Database.Connection.ConnectionString;
            if (count % commitCount == 0)
            {
                context.SaveChangesWithNoAuditing();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new DatabaseEntities(connectionString);
                    context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            return context;
        }

        public static void SaveGisUploadToNormalizedFieldsUsingGeoJson(GisUploadAttempt gisUploadAttempt, FeatureCollection featureCollection)
        {
            ExecPClearGisImportTables();
            var gisMetdataAttributes = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList();
            var listOfPropertiesFromFirstFeature = featureCollection.Features.FirstOrDefault() != null
                ? featureCollection.Features.First().Properties.Select(x => x.Key).ToList()
                : new List<string>();
            var distinctListOfFields = featureCollection.Features.SelectMany(x => x.Properties.Select(y => y.Key)).OrderBy(x => x).Distinct().ToList();
            listOfPropertiesFromFirstFeature.AddRange(distinctListOfFields.Except(listOfPropertiesFromFirstFeature, StringComparer.InvariantCultureIgnoreCase));
            for (int i = 0; i < listOfPropertiesFromFirstFeature.Count(); i++)
            {
                var columnName = listOfPropertiesFromFirstFeature[i];
                var existingGisMetdataAttribute = gisMetdataAttributes.SingleOrDefault(x => string.Equals(x.GisMetadataAttributeName, columnName, StringComparison.InvariantCultureIgnoreCase));
                if (existingGisMetdataAttribute == null)
                {
                    HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Add(new GisMetadataAttribute(columnName.ToLowerInvariant()));
                    HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
                }

                existingGisMetdataAttribute = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList().Single(x => string.Equals(x.GisMetadataAttributeName, columnName, StringComparison.InvariantCultureIgnoreCase));

                HttpRequestStorage.DatabaseEntities.GisUploadAttemptGisMetadataAttributes.Add(new GisUploadAttemptGisMetadataAttribute(gisUploadAttempt, existingGisMetdataAttribute, i));
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            }


            var listOfPossibleAttributes = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList();


            var attributeDictionary = listOfPossibleAttributes.ToDictionary(x => x.GisMetadataAttributeName, y => y);

            var features = featureCollection.Features;

            var gisFeatures = new List<GisFeature>();

            for (int j = 0; j < features.Count; j++)
            {
                var feature = features[j];
                var sqlGeom = feature.ToSqlGeometry().MakeValid();
                var gisFeature = new GisFeature(gisUploadAttempt, sqlGeom.ToDbGeometryWithCoordinateSystem(), j);
                gisFeatures.Add(gisFeature);
                feature.Properties.Add("GisFeature", gisFeature);
            }

            HttpRequestStorage.DatabaseEntities.GisFeatures.AddRange(gisFeatures);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditingInsertOnly();
            gisUploadAttempt.FeaturesSaved = true;
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            var listOfAllAttributesAboutToBeAdded = new List<GisFeatureMetadataAttribute>();



            for (int i = 0; i < features.Count; i++)
            {
                var feature = features[i];
                var gisFeature = ((GisFeature)feature.Properties["GisFeature"]);
                var gisFeatureID = gisFeature.GisFeatureID;
                var featureProperties = feature.Properties.Where(z => !string.Equals(z.Key, "GisFeature"));
                var gisFeatureMetadataAttributes = featureProperties.Select(x =>
                    new GisFeatureMetadataAttribute(gisFeatureID,
                        attributeDictionary[x.Key.ToLowerInvariant()].GisMetadataAttributeID)
                    {
                        GisFeatureMetadataAttributeValue = x.Value != null ? x.Value.ToString().Trim() : null
                    }).ToList();
                listOfAllAttributesAboutToBeAdded.AddRange(gisFeatureMetadataAttributes);
            }


            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;

            var concattedSqlStatement = string.Empty;
            for (int index = 0; index < listOfAllAttributesAboutToBeAdded.Count; index++)
            {
                var attributeToAdd = listOfAllAttributesAboutToBeAdded[index];
                var sqlStatement = attributeToAdd.MakeSqlInsertStatement();

                concattedSqlStatement = $"{concattedSqlStatement} \r\n {sqlStatement}";

                if (index % 1000 == 0)
                {
                    using (var command = new SqlCommand(concattedSqlStatement))
                    {
                        var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                        using (var conn = sqlConnection)
                        {
                            command.Connection = conn;
                            ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);

                        }
                    }

                    concattedSqlStatement = string.Empty;
                }
            }

            if (!string.IsNullOrEmpty(concattedSqlStatement))
            {
                using (var command = new SqlCommand(concattedSqlStatement))
                {
                    var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                    using (var conn = sqlConnection)
                    {
                        command.Connection = conn;
                        ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);

                    }
                }
            }

            gisUploadAttempt.AttributesSaved = true;
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();


            var sqlQuery = $"select * from dbo.GisFeature where GisUploadAttemptID = {gisUploadAttempt.GisUploadAttemptID}";

            var reprojectedFeatureCollection = ImportSqlToGeoJson(sqlQuery, 2927, sqlDatabaseConnectionString);
            var dictionaryReprojectedFeatures =
                reprojectedFeatureCollection.Features.ToDictionary(x =>
                    int.Parse(x.Properties["GisImportFeatureKey"].ToString()));

            var gisFeatureList = HttpRequestStorage.DatabaseEntities.GisFeatures
                .Where(x => x.GisUploadAttemptID == gisUploadAttempt.GisUploadAttemptID).ToList();

            for (var gisFeatureIndex = 0; gisFeatureIndex < gisFeatureList.Count; gisFeatureIndex++)
            {
                var gisFeature = gisFeatureList[gisFeatureIndex];
                if (dictionaryReprojectedFeatures.ContainsKey(gisFeature.GisImportFeatureKey))
                {
                    var geojsonFeature = dictionaryReprojectedFeatures[gisFeature.GisImportFeatureKey];
                    var reprojectedGeom = geojsonFeature.ToSqlGeometry();
                    if (reprojectedGeom.STIsValid())
                    {
                        var area = reprojectedGeom.STArea().Value;
                        var areaInAcres = area * 2.29568e-5;
                        gisFeature.CalculatedArea = (decimal)areaInAcres;
                    }
                }
                else
                {
                    var logger = LogManager.GetLogger(typeof(GisProjectBulkUpdateController));
                    logger.Warn($"Cannot find key gisFeature.GisImportFeatureKey:\"{gisFeature.GisImportFeatureKey}\" in dictionaryReprojectedFeatures. Related to GisUploadAttemptID:{gisUploadAttempt.GisUploadAttemptID}.");
                }

            }

            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            gisUploadAttempt.AreaCalculationComplete = true;
            gisUploadAttempt.FileUploadSuccessful = true;
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();


        }



        private void DropGisImportTable()
        {
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQueryOne = $"if exists(select 1 from INFORMATION_SCHEMA.TABLES x where x.TABLE_NAME = '{GisUploadAttemptStaging.GISImportTableName}')begin drop table dbo.{GisUploadAttemptStaging.GISImportTableName} end";
            using (var command = new SqlCommand(sqlQueryOne))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);

                }
            }
        }


        public static List<GeoJSONObjectType> ListOfValidGeoJsonTypes = new List<GeoJSONObjectType>
            {GeoJSONObjectType.Polygon, GeoJSONObjectType.MultiPolygon};

        public static bool IsUsableFeatureGeoJson(Feature feature)
        {
            if (feature == null)
            {
                return false;
            }

            if (feature.Geometry == null)
            {
                return false;
            }

            var isValidType = ListOfValidGeoJsonTypes.Contains(feature.Geometry.Type);

            if (!isValidType)
            {
                return false;
            }
            return true;
        }

        public static bool IsUsableFeatureCollectionGeoJson(FeatureCollection featureCollection)
        {
            return featureCollection.Features.Any(IsUsableFeatureGeoJson);
        }

        public static FeatureCollection ImportGdbToGeoJson(
            FileInfo gisFile)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

            var classNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(
                new FileInfo(FirmaWebConfiguration.OgrInfoExecutable), gisFile,
                Ogr2OgrCommandLineRunner.DefaultTimeOut);
            var allFeatureClasses =
                classNames.Select(x => ogr2OgrCommandLineRunner.ImportFileGdbToGeoJson(gisFile, x, false));
            var goodFeatureClasses = allFeatureClasses.Where(x =>
                IsUsableFeatureCollectionGeoJson(JsonTools.DeserializeObject<FeatureCollection>(x)));
            var goodFeatures = goodFeatureClasses.Select(x =>
                new FeatureCollection(JsonTools.DeserializeObject<FeatureCollection>(x).Features
                    .Where(IsUsableFeatureGeoJson).ToList())).ToList();

            Check.Assert(goodFeatures.Count != 0, "Number of usable Feature Classes in uploaded file must be greater than 0.");

            return goodFeatures.First();
        }

        public static FeatureCollection ImportSqlToGeoJson(
            string sqlQuery, int coordinateSystemID, string connectionString)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);
            var allFeatureClasses = ogr2OgrCommandLineRunner.ImportSqlToGeoJson(sqlQuery, connectionString, coordinateSystemID);
            var goodFeatures = new FeatureCollection(JsonTools.DeserializeObject<FeatureCollection>(allFeatureClasses).Features.Where(IsUsableFeatureGeoJson).ToList());
            return goodFeatures;
        }


        public static FeatureCollection ImportShpToGeoJson(
            string shapeFilePath)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

            var classNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromShapefile(
                new FileInfo(FirmaWebConfiguration.OgrInfoExecutable), shapeFilePath,
                Ogr2OgrCommandLineRunner.DefaultTimeOut);
            var allFeatureClasses =
                classNames.Select(x => ogr2OgrCommandLineRunner.ImportShapeFileToGeoJson(shapeFilePath, x, false));
            var goodFeatureClasses = allFeatureClasses.Where(x =>
                IsUsableFeatureCollectionGeoJson(JsonTools.DeserializeObject<FeatureCollection>(x)));
            var goodFeatures = goodFeatureClasses.Select(x =>
                new FeatureCollection(JsonTools.DeserializeObject<FeatureCollection>(x).Features
                    .Where(IsUsableFeatureGeoJson).ToList())).ToList();

            Check.Assert(goodFeatures.Count != 0, "Number of usable Feature Classes in uploaded file must be greater than 0.");

            return goodFeatures.First();
        }


        private static void ExecPClearGisImportTables()
        {
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQueryOne = $"dbo.pClearGisImportTables";
            using (var command = new SqlCommand(sqlQueryOne))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    command.CommandTimeout = 400;
                    ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);
                }
            }

        }

        private static void ExecProcImportTreatmentsFromGisUploadAttempt(int gisUploadAttemptID
                                                                , int projectIdentifierMetadataAttributeID
                                                                , int? footprintAcresMetadataAttributeID
                                                                , int? treatedAcresMetadataAttributeID
                                                                , int? treatmentTypeMetadataAttributeID
                                                                , int? treatmentDetailedActivityTypeMetadataAttributeID
                                                                , GisUploadSourceOrganization gisUploadSourceOrganization
                                                                , int? pruningAcresMetadataAttributeID
                                                                , int? thinningAcresMetadataAttributeID
                                                                , int? chippingAcresMetadataAttributeID
                                                                , int? masticationAcresMetadataAttributeID
                                                                , int? grazingAcresMetadataAttributeID
                                                                , int? lopScatAcresMetadataAttributeID
                                                                , int? biomassRemovalAcresMetadataAttributeID
                                                                , int? handPileAcresMetadataAttributeID
                                                                , int? handPileBurnAcresMetadataAttributeID
                                                                , int? machinePileBurnAcresMetadataAttributeID
                                                                , int? broadcastBurnAcresMetadataAttributeID
                                                                , int? otherAcresMetadataAttributeID
                                                                , int? startDateMetadataAttributeID
                                                                , int? completedDateMetadataAttributeID
                                                                )
        {

            SitkaHttpApplication.Logger.Info($"starting ExecProcImportTreatmentsFromGisUploadAttempt() for gisUploadAttempt:{gisUploadAttemptID}");
            int footprintAcresMetadataAttributeSqlID;
            if (!footprintAcresMetadataAttributeID.HasValue)
            {
                footprintAcresMetadataAttributeSqlID = -1;
            }
            else
            {
                footprintAcresMetadataAttributeSqlID = footprintAcresMetadataAttributeID.Value;
            }

            var treatedAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(treatedAcresMetadataAttributeID);
            var treatmentTypeMetadataAttributeSqlID = GetMetadataAttributeSqlID(treatmentTypeMetadataAttributeID);
            var treatmentDetailedActivityTypeMetadataAttributeSqlID = GetMetadataAttributeSqlID(treatmentDetailedActivityTypeMetadataAttributeID);
            var pruningAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(pruningAcresMetadataAttributeID);
            var thinningAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(thinningAcresMetadataAttributeID);
            var chippingAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(chippingAcresMetadataAttributeID);
            var masticationAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(masticationAcresMetadataAttributeID);
            var grazingAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(grazingAcresMetadataAttributeID);
            var lopScatAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(lopScatAcresMetadataAttributeID);
            var biomassRemovalAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(biomassRemovalAcresMetadataAttributeID);
            var handPileAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(handPileAcresMetadataAttributeID);
            var handPileBurnAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(handPileBurnAcresMetadataAttributeID);
            var machinePileBurnAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(machinePileBurnAcresMetadataAttributeID);
            var broadcastBurnAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(broadcastBurnAcresMetadataAttributeID);
            var otherAcresMetadataAttributeSqlID = GetMetadataAttributeSqlID(otherAcresMetadataAttributeID);

            var startDateMetadataAttributeSqlID = GetMetadataAttributeSqlID(startDateMetadataAttributeID);
            var completedDateMetadataAttributeSqlID = GetMetadataAttributeSqlID(completedDateMetadataAttributeID);

            var isFlattened = gisUploadSourceOrganization.ImportIsFlattened.HasValue
                ? gisUploadSourceOrganization.ImportIsFlattened.Value
                : false;
            var isFlattenedSqlVal = isFlattened ? 1 : 0;

            var treatmentTypeID = TreatmentType.Other.TreatmentTypeID;

            var defaultTreatmentTypeString = gisUploadSourceOrganization.TreatmentTypeDefaultName;
            if (!string.IsNullOrEmpty(defaultTreatmentTypeString))
            {
                var treatmentType = TreatmentType.All.SingleOrDefault(x =>
                    x.TreatmentTypeDisplayName.Equals(defaultTreatmentTypeString, StringComparison.InvariantCultureIgnoreCase));
                if (treatmentType != null)
                {
                    treatmentTypeID = treatmentType.TreatmentTypeID;
                }
            }

            var treatmentDetailedActivityTypeID = TreatmentDetailedActivityType.Other.TreatmentDetailedActivityTypeID;


            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQueryOne = $"exec dbo.procImportTreatmentsFromGisUploadAttempt " +
                              $"@piGisUploadAttemptID = {gisUploadAttemptID}" +
                              $", @projectIdentifierGisMetadataAttributeID = {projectIdentifierMetadataAttributeID}" +
                              $", @footprintAcresMetadataAttributeID = {footprintAcresMetadataAttributeSqlID}" +
                              $", @treatedAcresMetadataAttributeID = {treatedAcresMetadataAttributeSqlID}" +
                              $", @treatmentTypeMetadataAttributeID = {treatmentTypeMetadataAttributeSqlID}" +
                              $", @treatmentDetailedActivityTypeMetadataAttributeID = {treatmentDetailedActivityTypeMetadataAttributeSqlID}" +
                              $", @treatmentTypeID = {treatmentTypeID}" +
                              $", @treatmentDetailedActivityTypeID = {treatmentDetailedActivityTypeID}" +
                              $", @isFlattened = {isFlattenedSqlVal}" +
                              $", @pruningAcresMetadataAttributeID = {pruningAcresMetadataAttributeSqlID}" +
                              $", @thinningAcresMetadataAttributeID = {thinningAcresMetadataAttributeSqlID}" +
                              $", @chippingAcresMetadataAttributeID = {chippingAcresMetadataAttributeSqlID}" +
                              $", @masticationAcresMetadataAttributeID = {masticationAcresMetadataAttributeSqlID}" +
                              $", @grazingAcresMetadataAttributeID = {grazingAcresMetadataAttributeSqlID}" +
                              $", @lopScatterAcresMetadataAttributeID = {lopScatAcresMetadataAttributeSqlID}" +
                              $", @biomassRemovalAcresMetadataAttributeID = {biomassRemovalAcresMetadataAttributeSqlID}" +
                              $", @handPileAcresMetadataAttributeID = {handPileAcresMetadataAttributeSqlID}" +
                              $", @handPileBurnAcresMetadataAttributeID = {handPileBurnAcresMetadataAttributeSqlID}" +
                              $", @machineBurnAcresMetadataAttributeID = {machinePileBurnAcresMetadataAttributeSqlID}" +
                              $", @broadcastBurnAcresMetadataAttributeID = {broadcastBurnAcresMetadataAttributeSqlID}" +
                              $", @otherBurnAcresMetadataAttributeID = {otherAcresMetadataAttributeSqlID}" +
                              $", @startDateMetadataAttributeID = {startDateMetadataAttributeSqlID}" +
                              $", @endDateMetadataAttributeID = {completedDateMetadataAttributeSqlID}" +
                              $"";
            using (var command = new SqlCommand(sqlQueryOne))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    command.CommandTimeout = 400;
                    ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);
                }
            }
        }

        private static int GetMetadataAttributeSqlID(int? metadataAttributeID)
        {
            int metadataAttributeSqlID;
            if (!metadataAttributeID.HasValue)
            {
                metadataAttributeSqlID = -1;
            }
            else
            {
                metadataAttributeSqlID = metadataAttributeID.Value;
            }

            return metadataAttributeSqlID;
        }


        public class GisRecord
        {
            public List<GisColumnName> GisColumnNames { get; set; }

            public GisFeature GisFeature { get; set; }

            public DbGeometry DbGeometry { get; set; }

            public int ID { get; set; }

            public GisRecord(List<GisColumnName> gisColumnNames)
            {
                GisColumnNames = gisColumnNames;
            }
        }

        public class GisColumnName
        {
            public string ID { get; set; }
            public string ColumnValue { get; set; }

            public string ColumnName { get; set; }

            public int SortOrder { get; set; }


            public GisColumnName(string idValue, string columnValue, string columnName, int sortOrder)
            {
                ID = idValue;
                ColumnValue = columnValue;
                ColumnName = columnName.ToLowerInvariant();
                SortOrder = sortOrder;
            }

        }


        public class GisShape
        {
            public string ID { get; set; }

            public byte[] SqlGeometryAsByteArray { get; set; }

            public string ColumnnName { get; set; }


            public GisShape(string idValue, byte[] featureAsWellKnownText, string columnnName)
            {

                ID = idValue;
                SqlGeometryAsByteArray = featureAsWellKnownText;
                ColumnnName = columnnName;
            }

        }

    }
}