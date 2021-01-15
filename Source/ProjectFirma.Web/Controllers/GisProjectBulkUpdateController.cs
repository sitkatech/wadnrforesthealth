using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApprovalUtilities.Utilities;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Microsoft.SqlServer.Types;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.UnitTestCommon;
using ProjectFirma.Web.Views.GisProjectBulkUpdate;
using ProjectFirma.Web.Views.ProjectCreate;

namespace ProjectFirma.Web.Controllers
{
    public class GisProjectBulkUpdateController : FirmaBaseController
    {
        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public PartialViewResult SourceOrganizationSelection()
        {
            var gisSourceOrganizations = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.ToList();
            var viewData = new SourceOrganizationTypeSelectionViewData(gisSourceOrganizations);
            var viewModel = new SourceOrganizationTypeSelectionViewModel();
            return RazorPartialView<SourceOrganizationTypeSelection, SourceOrganizationTypeSelectionViewData, SourceOrganizationTypeSelectionViewModel>(viewData, viewModel);
        }


        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult SourceOrganizationSelection(SourceOrganizationTypeSelectionViewModel viewModel)
        {
            var gisSourceOrganizations = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.ToList();
            var viewData = new SourceOrganizationTypeSelectionViewData(gisSourceOrganizations);

            if (!ModelState.IsValid)
            {
                return RazorPartialView<SourceOrganizationTypeSelection, SourceOrganizationTypeSelectionViewData, SourceOrganizationTypeSelectionViewModel>(viewData, viewModel);
            }

            var gisAttempt = new GisUploadAttempt(viewModel.SourceOrganizationID.Value, CurrentPerson.PersonID, DateTime.Now);

            HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return new ModalDialogFormJsonResult(
                        SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.InstructionsGisImport(gisAttempt.GisUploadAttemptID)));

        }

        private static GisImportSectionStatus GetGisImportSectionStatus(GisUploadAttempt gisUploadAttempt)
        {
            return new GisImportSectionStatus(gisUploadAttempt);
        }


        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
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
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult UploadGisFile(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey)
        {
            var uploadGisFileViewModel = new UploadGisFileViewModel();
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;

            return ViewUploadGisFile(uploadGisFileViewModel, gisUploadAttempt);
        }


        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
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


        [ProjectsViewFullListFeature]
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

        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult GisMetadata(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey, GisMetadataViewModel viewModel)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            var projectIdentifierMetadataAttribute = GenerateSingleMetadataAttribute(gisUploadAttempt, viewModel.ProjectIdentifierMetadataAttributeID);
            var gisFeatureIDs = gisUploadAttempt.GisFeatures.Select(x => x.GisFeatureID).ToList();

            var completionDateDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.CompletionDateMetadataAttributeID);
            var projectNameDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.ProjectNameMetadataAttributeID);
            var privateLandOwnerDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.PrivateLandownerMetadataAttributeID);
            var startDateDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.StartDateMetadataAttributeID);
            var projectStageDictionary = GenerateMetadataValueDictionary(gisFeatureIDs, viewModel.ProjectStageMetadataAttributeID);

            var distinctProjectIdentifiers = GetDistinctProjectIdentifiers(projectIdentifierMetadataAttribute, gisFeatureIDs);

            var otherProjectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList().Single(x => string.Equals("Other", x.ProjectTypeName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            var projectList = new List<Project>();
            var newPersonList = new List<Person>();
            var newProjectPersonList = new List<ProjectPerson>();
            var projectLocationList = new List<ProjectLocation>();
            var existingPersons = HttpRequestStorage.DatabaseEntities.People.ToList();

            var gisCrossWalkDefaultList = HttpRequestStorage.DatabaseEntities.GisCrossWalkDefaults.ToList();

            var currentCounter = CalculateNextProjectNumberForThisYear();

            var existingProjects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => x.ProgramID.HasValue && x.ProgramID.Value == gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();


            MakeProjectsAndSave(distinctProjectIdentifiers, existingProjects, projectIdentifierMetadataAttribute, completionDateDictionary, startDateDictionary, projectNameDictionary, projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, otherProjectType, projectList, currentCounter);

            MakeProjectLocationsAndSave(gisUploadAttempt, distinctProjectIdentifiers, projectIdentifierMetadataAttribute, projectLocationList);


            MakeProjectPeopleAndSave(gisUploadAttempt, distinctProjectIdentifiers, projectIdentifierMetadataAttribute, privateLandOwnerDictionary, existingPersons, newPersonList, newProjectPersonList);


            MakeProjectOrganizationsAndSave(gisUploadAttempt, distinctProjectIdentifiers);

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


            UpdateProjectTypesIfNeeded(gisUploadAttempt);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            SetMessageForDisplay($"Successfully imported {projectList.Count} {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

            return new ModalDialogFormJsonResult();
        }

        private static void MakeProjectOrganizationsAndSave(GisUploadAttempt gisUploadAttempt, List<string> distinctProjectIdentifiers)
        {
            var projectOrganizationsToAdd = UpdateProjectOrganizationRecords(gisUploadAttempt, distinctProjectIdentifiers);
            HttpRequestStorage.DatabaseEntities.ProjectOrganizations.AddRange(projectOrganizationsToAdd);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void MakeProjectPeopleAndSave(GisUploadAttempt gisUploadAttempt, List<string> distinctProjectIdentifiers,
            GisMetadataAttribute projectIdentifierMetadataAttribute, Dictionary<int, List<GisFeatureMetadataAttribute>> privateLandOwnerDictionary,
            List<Person> existingPersons, List<Person> newPersonList, List<ProjectPerson> newProjectPersonList)
        {
            var existingProjectsAfterSaveWithProjectPeople = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectPeople)
                .Where(x => x.ProgramID.HasValue && x.ProgramID.Value == gisUploadAttempt.GisUploadSourceOrganization.ProgramID)
                .ToList()
                .Where(x => distinctProjectIdentifiers.Contains(x.ProjectGisIdentifier,
                    StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            foreach (var distinctProjectIdentifier in distinctProjectIdentifiers)
            {
                var project = existingProjectsAfterSaveWithProjectPeople.Single(x => string.Equals(x.ProjectGisIdentifier,
                    distinctProjectIdentifier, StringComparison.InvariantCultureIgnoreCase));
                var gisFeaturesIdListWithProjectIdentifier =
                    projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                        string.Equals(x.GisFeatureMetadataAttributeValue, distinctProjectIdentifier,
                            StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();
                var privateLandownerAttributes = gisFeaturesIdListWithProjectIdentifier
                    .Where(x => privateLandOwnerDictionary.ContainsKey(x))
                    .SelectMany(x => privateLandOwnerDictionary[x]).ToList();
                var landOwners = privateLandownerAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();
                GenerateProjectPersonList(existingPersons, newPersonList, newProjectPersonList, landOwners, project);
            }

            HttpRequestStorage.DatabaseEntities.People.AddRange(newPersonList);
            HttpRequestStorage.DatabaseEntities.ProjectPeople.AddRange(newProjectPersonList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void MakeProjectLocationsAndSave(GisUploadAttempt gisUploadAttempt, List<string> distinctProjectIdentifiers,
            GisMetadataAttribute projectIdentifierMetadataAttribute, List<ProjectLocation> projectLocationList)
        {
            var existingProjectsAfterSaveWithProjectLocations = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectLocations)
                .Where(x => x.ProgramID.HasValue && x.ProgramID.Value == gisUploadAttempt.GisUploadSourceOrganization.ProgramID)
                .ToList()
                .Where(x => distinctProjectIdentifiers.Contains(x.ProjectGisIdentifier,
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
                    project.ProjectLocations.Where(x => x.ProjectLocationType == ProjectLocationType.ProjectArea)
                        .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
                    var gisFeaturesIdListWithProjectIdentifier =
                        projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                            string.Equals(x.GisFeatureMetadataAttributeValue, distinctProjectIdentifier,
                                StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();
                    var gisFeatures = gisUploadAttempt.GisFeatures
                        .Where(x => gisFeaturesIdListWithProjectIdentifier.Contains(x.GisFeatureID)).ToList();
                    foreach (var gisFeature in gisFeatures)
                    {
                        var newProjectLocation = new ProjectLocation(project, gisFeature.GisFeatureGeometry,
                            ProjectLocationType.ProjectArea, "Imported Project Area");
                        projectLocationList.Add(newProjectLocation);
                    }

                    var centroid = gisFeatures.Select(x => x.GisFeatureGeometry).FirstOrDefault()?.Centroid;
                    if (centroid != null)
                    {
                        project.ProjectLocationPoint = centroid;
                        project.ProjectLocationSimpleTypeID = ProjectLocationSimpleType.PointOnMap.ProjectLocationSimpleTypeID;
                    }
                }
            }

            HttpRequestStorage.DatabaseEntities.ProjectLocations.AddRange(projectLocationList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void MakeProjectsAndSave(List<string> distinctProjectIdentifiers, List<Project> existingProjects,
            GisMetadataAttribute projectIdentifierMetadataAttribute, Dictionary<int, List<GisFeatureMetadataAttribute>> completionDateDictionary,
            Dictionary<int, List<GisFeatureMetadataAttribute>> startDateDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> projectNameDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> projectStageDictionary,
            List<GisCrossWalkDefault> gisCrossWalkDefaultList, GisUploadAttempt gisUploadAttempt, ProjectType otherProjectType, List<Project> projectList,
            int currentCounter)
        {
            foreach (var distinctProjectIdentifier in distinctProjectIdentifiers)
            {
                MakeProject(existingProjects, projectIdentifierMetadataAttribute, distinctProjectIdentifier,
                    completionDateDictionary, startDateDictionary, projectNameDictionary,
                    projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, otherProjectType,
                    gisUploadAttempt.GisUploadAttemptID, projectList, gisUploadAttempt.GisUploadSourceOrganization,
                    ref currentCounter);
            }

            HttpRequestStorage.DatabaseEntities.Projects.AddRange(projectList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void UpdateProjectRegions(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt,int? gisMetadataAttributeIdentier)
        {
            HttpRequestStorage.DatabaseEntities.GetObjectContext().CommandTimeout = 180;
            var projectUplandDnrRegionsCalculated = HttpRequestStorage.DatabaseEntities
                .GetfGetProjectDnrUploadRegions(gisUploadAttempt.GisUploadAttemptID, gisMetadataAttributeIdentier, gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();
            var projectRegions = projectUplandDnrRegionsCalculated
                .Select(x => new ProjectRegion(x.ProjectID, x.DNRUplandRegionID)).ToList();

            var projectsWithUplandDnrRegions = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectRegions)
                .Where(x => x.ProgramID.HasValue && x.ProgramID.Value == gisUploadAttempt.GisUploadSourceOrganization.ProgramID)
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            projectsWithUplandDnrRegions.SelectMany(x => x.ProjectRegions)
                .ToList()
                .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));

            HttpRequestStorage.DatabaseEntities.ProjectRegions.AddRange(projectRegions);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static void UpdateProjectPriorityLandscapes(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt, int? gisMetadataAttributeIdentier)
        {
            var projectPriorityLandscapesCalculated = HttpRequestStorage.DatabaseEntities
                .GetfGetProjectPriorityLandscapes(gisUploadAttempt.GisUploadAttemptID, gisMetadataAttributeIdentier, gisUploadAttempt.GisUploadSourceOrganization.ProgramID).ToList();
            var projectPriorityLandscapes = projectPriorityLandscapesCalculated
                .Select(x => new ProjectPriorityLandscape(x.ProjectID, x.PriorityLandscapeID)).ToList();
            var projectsWithPriorityLandscapes = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectPriorityLandscapes)
                .Where(x => x.ProgramID.HasValue && x.ProgramID.Value == gisUploadAttempt.GisUploadSourceOrganization.ProgramID)
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            projectsWithPriorityLandscapes.SelectMany(x => x.ProjectPriorityLandscapes)
                .ToList()
                .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
            HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapes.AddRange(projectPriorityLandscapes);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private static List<ProjectOrganization> UpdateProjectOrganizationRecords(GisUploadAttempt gisUploadAttempt, List<string> distinctIdentifiersFromGisUploadAttempt)
        {
            var leadImplementerOrganization = gisUploadAttempt.GisUploadSourceOrganization.DefaultLeadImplementerOrganization;
            var projectsWithOrganization = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.ProjectOrganizations)
                .Where(x => x.ProgramID.HasValue && x.ProgramID.Value == gisUploadAttempt.GisUploadSourceOrganization.ProgramID)
                .ToList()
                .Where(x => distinctIdentifiersFromGisUploadAttempt.Contains(x.ProjectGisIdentifier, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            var relationshipType = gisUploadAttempt.GisUploadSourceOrganization.RelationshipTypeForDefaultOrganization;

            projectsWithOrganization.SelectMany(x => x.ProjectOrganizations)
                .Where(x => x.RelationshipTypeID == gisUploadAttempt.GisUploadSourceOrganization
                    .RelationshipTypeForDefaultOrganizationID)
                .ToList()
                .ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));

            var projectOrganizationsToAdd = projectsWithOrganization
                .Select(x => new ProjectOrganization(x, leadImplementerOrganization, relationshipType)).ToList();
            return projectOrganizationsToAdd;


        }

        private static List<string> GetDistinctProjectIdentifiers(GisMetadataAttribute projectIdentifierMetadataAttribute,
            List<int> gisFeatureIDs)
        {
            var projectIdentifierValues =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                    gisFeatureIDs.Contains(x.GisFeatureID));
            var distinctProjectIdentifiers = projectIdentifierValues.Select(x => x.GisFeatureMetadataAttributeValue)
                .Where(y => !string.IsNullOrWhiteSpace(y)).Select(x => x.ToUpperInvariant()).Distinct().OrderBy(x => x).ToList();
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

        private static Dictionary<int, List<GisFeatureMetadataAttribute>> GenerateMetadataValueDictionary(IEnumerable<int> gisFeatureIDs,
            int? completionDateMetadataAttributeID)
        {
            var completionDateDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                    gisFeatureIDs.Contains(x.GisFeatureID) &&
                    x.GisMetadataAttributeID == completionDateMetadataAttributeID).ToList().GroupBy(y => y.GisFeatureID)
                .ToDictionary(y => y.Key, x => x.ToList());
            return completionDateDictionary;
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
                var projects = HttpRequestStorage.DatabaseEntities.Projects.Where(x =>
                    x.CreateGisUploadAttemptID == gisUploadAttempt.GisUploadAttemptID).Include(x => x.Treatments).ToList();
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
                                        ref int currentCounter)
        {

            var gisFeaturesIdListWithProjectIdentifier =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                    string.Equals(x.GisFeatureMetadataAttributeValue, distinctGisValue,
                        StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();

            var completionDate = CalculateCompletionDate(completionDateDictionary, gisFeaturesIdListWithProjectIdentifier);

            if (gisUploadAttempt.GisUploadSourceOrganization.RequiresCompletionDate() && !completionDate.HasValue)
            {
                return;
            }

            var project = existingProjects.SingleOrDefault(x => string.Equals(x.ProjectGisIdentifier, distinctGisValue));

            var projectAlreadyExists = project != null;

            


            var projectNumber = $"FHT-{DateTime.Now.Year}-{currentCounter:00000}";
            var startDate = CalculateStartDate(startDateDictionary, gisFeaturesIdListWithProjectIdentifier);
            var projectName = CalculateProjectName(projectNameDictionary, gisFeaturesIdListWithProjectIdentifier);
            var projectStage = CalculateProjectStage(projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, gisFeaturesIdListWithProjectIdentifier, completionDate);
            var projectType = CalculateProjectType(gisUploadSourceOrganization);
            var projectDescription = gisUploadAttempt.GisUploadSourceOrganization.ProjectDescriptionDefaultText;


            if (projectAlreadyExists)
            {
                project.ProjectType = otherProjectType;
                project.ProjectTypeID = otherProjectType.ProjectTypeID;
                project.ProjectStageID = projectStage.ProjectStageID;
                project.ProjectName = projectName;
                project.LastUpdateGisUploadAttemptID = gisUploadAttemptID;
            }
            else
            {
                project = new Project(otherProjectType.ProjectTypeID,
                    projectStage.ProjectStageID,
                    projectName,
                    false,
                    ProjectLocationSimpleType.None.ProjectLocationSimpleTypeID,
                    ProjectApprovalStatus.Approved.ProjectApprovalStatusID,
                    projectNumber
                );
                project.CreateGisUploadAttemptID = gisUploadAttemptID;
                project.ProjectGisIdentifier = distinctGisValue;
            }

            
            project.Program = gisUploadAttempt.GisUploadSourceOrganization.Program;
            project.ProgramID = gisUploadAttempt.GisUploadSourceOrganization.ProgramID;

            if (gisUploadSourceOrganization.ApplyCompletedDateToProject)
            {
                project.CompletionDate = completionDate;
            }

            if (gisUploadSourceOrganization.ApplyStartDateToProject)
            {
                project.PlannedDate = startDate;
            }

            
            project.ProjectDescription = projectDescription;
            if (projectType != null)
            {
                project.ProjectType = projectType;
                project.ProjectTypeID = projectType.ProjectTypeID;
            }

            if (!projectAlreadyExists)
            {
                projectList.Add(project);
                currentCounter++;
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
                .Where(x => projectStageDictionary.ContainsKey(x))
                .SelectMany(x => projectStageDictionary[x]).ToList();
            ProjectStage projectStage = gisUploadAttempt.GisUploadSourceOrganization.ProjectStageDefault;
            projectStage = CalculateProjectStageIfNeeded(gisCrossWalkDefaultList, gisUploadAttempt, projectStageAttributes,
                completionDate, projectStage);
            return projectStage;
        }

        private static string CalculateProjectName(Dictionary<int, List<GisFeatureMetadataAttribute>> projectNameDictionary,
            List<int> gisFeaturesIdListWithProjectIdentifier)
        {
            var projectNameAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => projectNameDictionary.ContainsKey(x))
                .SelectMany(x => projectNameDictionary[x]).ToList();
            var projectNames = projectNameAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();

            if (projectNames.Count > 1)
            {
                var projectNameLower = projectNames.Select(x => x.ToUpperInvariant()).Distinct().ToList();
                if (projectNameLower.Count == 1)
                {
                    projectNames = projectNames.Take(1).ToList();
                }
            }

            var projectName = projectNames.SingleOrDefault();
            if (string.IsNullOrEmpty(projectName))
            {
                projectName = "Default Project Name";
            }

            return projectName;
        }

        private static void GenerateProjectPersonList(List<Person> existingPersonList, List<Person> newPersonList, List<ProjectPerson> newProjectPersonList,
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
                    if (existingPersons.Count() == 1)
                    {
                        var projectPerson = new ProjectPerson(project, existingPersons.Single(),
                            ProjectPersonRelationshipType.PrivateLandowner);
                        newProjectPersonList.Add(projectPerson);
                    }
                    else if (existingNewPersons.Count() == 1)
                    {
                        var projectPerson = new ProjectPerson(project, existingNewPersons.Single(),
                            ProjectPersonRelationshipType.PrivateLandowner);
                        newProjectPersonList.Add(projectPerson);
                    }
                    else
                    {
                        var person = new Person(firstName, Role.Unassigned, DateTime.Now, true, false) {LastName = lastName};
                        newPersonList.Add(person);
                        var projectPerson = new ProjectPerson(project, person,
                            ProjectPersonRelationshipType.PrivateLandowner);
                        newProjectPersonList.Add(projectPerson);
                    }
                }
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

        private static DateTime? CalculateStartDate(Dictionary<int, List<GisFeatureMetadataAttribute>> startDateDictionary, List<int> gisFeaturesIdListWithProjectIdentifier)
        {
            var startDateAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => startDateDictionary.ContainsKey(x))
                .SelectMany(x => startDateDictionary[x]).ToList();
            var startAttributes = startDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out var date)).Select(x => DateTime.Parse(x)).ToList();
            var startDate = startAttributes.Any() ? startAttributes.Min() : (DateTime?) null;
            return startDate;
        }

        private static DateTime? CalculateCompletionDate(Dictionary<int, List<GisFeatureMetadataAttribute>> completionDateDictionary,
            List<int> gisFeaturesIdListWithProjectIdentifier)
        {
            var completionDateAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(x => completionDateDictionary.ContainsKey(x))
                .SelectMany(x => completionDateDictionary[x]).ToList();

            var completionAttributes = completionDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out var date)).Select(x => DateTime.Parse(x)).ToList();

            var completionDate = completionAttributes.Any() ? completionAttributes.Max() : (DateTime?) null;
            return completionDate;
        }

        private static ProjectStage CalculateProjectStageIfNeeded(List<GisCrossWalkDefault> gisCrossWalkDefaultList,
            GisUploadAttempt gisUploadAttempt, List<GisFeatureMetadataAttribute> projectStageAttributes, DateTime? completionDate, ProjectStage projectStage)
        {
            if (gisUploadAttempt.GisUploadSourceOrganization.DataDeriveProjectStage)
            {
                var projectStages = projectStageAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();
                var projectStageString =
                    gisUploadAttempt.GisUploadSourceOrganization.ImportIsFlattened.HasValue &&
                    gisUploadAttempt.GisUploadSourceOrganization.ImportIsFlattened.Value
                        ? string.Empty
                        : projectStages.SingleOrDefault();


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
                        ?.GisCrossWalkMappedValue;

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

        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
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
            FeatureCollection featureCollection = null;

            if (shapeFileSuccessfullyExtractedToDisk)
            {
                //importWasSuccesful = ImportExtractedShapefileToSql(shapeFilePath);
                featureCollection = ImportShpToGeoJson(shapeFilePath);
            }

            else
            {
                var filePath = GisUploadAttemptStaging.SaveFileToDiskIfGdb(httpPostedFileBase, gisUploadAttempt);
                var fileInfo = new FileInfo(filePath);
                featureCollection = ImportGdbToGeoJson(fileInfo);
            }
            
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SaveGisUploadToNormalizedFieldsUsingGeoJson(gisUploadAttempt, featureCollection);
            


            return new ModalDialogFormJsonResult();
        }


        private void SaveGisUploadToNormalizedFieldsUsingGeoJson(GisUploadAttempt gisUploadAttempt, FeatureCollection featureCollection)
        {
            var gisMetdataAttributes = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList();
            var listOfPropertiesFromFirstFeature = featureCollection.Features.FirstOrDefault() != null
                ? featureCollection.Features.First().Properties.Select(x => x.Key).ToList()
                : new List<string>();
            var distinctListOfFields = featureCollection.Features.SelectMany(x => x.Properties.Select(y => y.Key)).OrderBy(x => x).Distinct().ToList();
            distinctListOfFields.Where(x => !listOfPropertiesFromFirstFeature.Contains(x, StringComparer.InvariantCultureIgnoreCase)).ForEach(x => listOfPropertiesFromFirstFeature.Add(x));
            for (int i = 0; i< listOfPropertiesFromFirstFeature.Count();i++)
            {
                var columnName = listOfPropertiesFromFirstFeature[i];
                var existingGisMetdataAttribute = gisMetdataAttributes.SingleOrDefault(x => string.Equals(x.GisMetadataAttributeName, columnName, StringComparison.InvariantCultureIgnoreCase));
                if (existingGisMetdataAttribute == null)
                {
                    HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Add(new GisMetadataAttribute(columnName.ToLowerInvariant()));
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                }

                existingGisMetdataAttribute = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList().Single(x => string.Equals(x.GisMetadataAttributeName, columnName, StringComparison.InvariantCultureIgnoreCase));

                HttpRequestStorage.DatabaseEntities.GisUploadAttemptGisMetadataAttributes.Add(new GisUploadAttemptGisMetadataAttribute(gisUploadAttempt, existingGisMetdataAttribute, i));
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }


            var listOfPossibleAttributes = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList();


            var attributeDictionary = listOfPossibleAttributes.ToDictionary(x => x.GisMetadataAttributeName, y => y);

            var features = featureCollection.Features;

            var gisFeatures = new List<GisFeature>();

            for (int j = 0; j<features.Count; j++)
            {
                var feature = features[j];
                var sqlGeom = feature.ToSqlGeometry().MakeValid();
                var gisFeature = new GisFeature(gisUploadAttempt, sqlGeom.ToDbGeometryWithCoordinateSystem(), j);
                gisFeatures.Add(gisFeature);
                feature.Properties.Add("GisFeature", gisFeature);
            }

            HttpRequestStorage.DatabaseEntities.GisFeatures.AddRange(gisFeatures);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            var listOfGisFeatureMetadataAttributesToAdd = features.AsParallel()
                .SelectMany(y => y.Properties.Where(z => !string.Equals(z.Key, "GisFeature"))
                    .AsParallel().Select(x => new GisFeatureMetadataAttribute(((GisFeature) y.Properties["GisFeature"]).GisFeatureID
                        , attributeDictionary[x.Key.ToLowerInvariant()].GisMetadataAttributeID) 
                        { GisFeatureMetadataAttributeValue = x.Value != null ? x.Value.ToString() : null }
                    ));
            HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.AddRange(
                listOfGisFeatureMetadataAttributesToAdd);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;

            var sqlQuery =
                $"select * from dbo.GisFeature where GisUploadAttemptID = {gisUploadAttempt.GisUploadAttemptID}";

            var reprojectedFeatureCollection = ImportSqlToGeoJson(sqlQuery, 2927, sqlDatabaseConnectionString);
            var gisFeatureList = HttpRequestStorage.DatabaseEntities.GisFeatures
                .Where(x => x.GisUploadAttemptID == gisUploadAttempt.GisUploadAttemptID).ToList();
            foreach (var gisFeature in gisFeatureList)
            {
                

                var geojsonFeature = reprojectedFeatureCollection.Features.Single(x =>
                   int.Parse(x.Properties["GisImportFeatureKey"].ToString())  == gisFeature.GisImportFeatureKey);
                var reprojectedGeom = geojsonFeature.ToSqlGeometry();
                if (reprojectedGeom.STIsValid())
                {
                    var area = reprojectedGeom.STArea().Value;
                    var areaInAcres = area * 2.29568e-5;
                    gisFeature.CalculatedArea = (decimal)areaInAcres;
                }
            }
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

        private void ExecProcImportTreatmentsFromGisUploadAttempt(int gisUploadAttemptID
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

            public  GisFeature GisFeature { get; set; }

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