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
            var sourceOrganization = gisUploadAttempt.GisUploadSourceOrganization;
            var gisUploadAttemptID = gisUploadAttempt.GisUploadAttemptID;
            var projectIdentifierMetadataAttributeID = viewModel.ProjectIdentifierMetadataAttributeID;
            var projectNameMetadataAttributeID = viewModel.ProjectNameMetadataAttributeID;
            var completionDateMetadataAttributeID = viewModel.CompletionDateMetadataAttributeID;
            var startDateMetadataAttributeID = viewModel.StartDateMetadataAttributeID;
            var projectStageMetadataAttributeID = viewModel.ProjectStageMetadataAttributeID;
            var treatmentTypeMetadataAttributeID = viewModel.TreatmentTypeMetadataAttributeID;
            var treatmentActivityTypeMetadataAttributeID = viewModel.TreatmentDetailedActivityTypeMetadataAttributeID;
            var treatedAcresMetadataAttributeID = viewModel.TreatedAcresMetadataAttributeID;
            var footprintAcresMetadataAttributeID = viewModel.FootprintAcresMetadataAttributeID;
            var privateLandownerMetadataAttributeID = viewModel.PrivateLandownerMetadataAttributeID;
            var programID = sourceOrganization.ProgramID;

            var projectIdentifierMetadataAttribute =
                gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.Single(x =>
                    x.GisMetadataAttributeID == projectIdentifierMetadataAttributeID).GisMetadataAttribute;

            var gisFeatureIDs = gisUploadAttempt.GisFeatures.Select(x => x.GisFeatureID);

            var completionDateDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == completionDateMetadataAttributeID).ToList().GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

           

            var projectNameDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == projectNameMetadataAttributeID).ToList().GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var privateLandOwnerDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == privateLandownerMetadataAttributeID).ToList().GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var startDateDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == startDateMetadataAttributeID).ToList().GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var projectStageDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == projectStageMetadataAttributeID).ToList().GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());
            

            var projectIdentifierValues =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x => gisFeatureIDs.Contains(x.GisFeatureID));
            var distinctProjectIdentifiers = projectIdentifierValues.Select(x => x.GisFeatureMetadataAttributeValue)
                .Where(y => !string.IsNullOrWhiteSpace(y)).Distinct().OrderBy(x => x).ToList();

            var otherProjectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList().Single(x =>
                string.Equals("Other", x.ProjectTypeName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            var projectList = new List<Project>();
            var newPersonList = new List<Person>();
            var newProjectPersonList = new List<ProjectPerson>();
            var projectLocationList = new List<ProjectLocation>();
            var existingPersons = HttpRequestStorage.DatabaseEntities.People.ToList();

            var gisCrossWalkDefaultList = HttpRequestStorage.DatabaseEntities.GisCrossWalkDefaults.ToList();

            var currentCounter = 1;
            var lastProjectCreatedThisYear = HttpRequestStorage.DatabaseEntities.Projects.Where(p => p.FhtProjectNumber.Contains(DateTime.Now.Year.ToString())).OrderByDescending(p => p.FhtProjectNumber).ToList().FirstOrDefault(p => p.FhtProjectNumber.StartsWith($"FHT-{DateTime.Now.Year}"));
            if (lastProjectCreatedThisYear != null)
            {
                var splitFhtProjectNumber = lastProjectCreatedThisYear.FhtProjectNumber.Split('-');
                Int32.TryParse(splitFhtProjectNumber[2], out currentCounter);
                currentCounter++;

            }

            foreach (var distinctProjectIdentifier in distinctProjectIdentifiers)
            {
                MakeProject(projectIdentifierMetadataAttribute, distinctProjectIdentifier, completionDateDictionary, startDateDictionary, projectNameDictionary, 
                    projectStageDictionary, gisCrossWalkDefaultList, gisUploadAttempt, otherProjectType, gisUploadAttemptID, projectList, sourceOrganization, projectLocationList
                    , privateLandOwnerDictionary, existingPersons, newPersonList, newProjectPersonList, programID,ref currentCounter);
            }

            HttpRequestStorage.DatabaseEntities.Projects.AddRange(projectList);
            HttpRequestStorage.DatabaseEntities.ProjectLocations.AddRange(projectLocationList);
            HttpRequestStorage.DatabaseEntities.People.AddRange(newPersonList);
            HttpRequestStorage.DatabaseEntities.ProjectPeople.AddRange(newProjectPersonList);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();


            var leadImplementerOrganization =
                gisUploadAttempt.GisUploadSourceOrganization.DefaultLeadImplementerOrganization;
            var projects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => x.CreateGisUploadAttemptID == gisUploadAttempt.GisUploadAttemptID).ToList();
            var relationshipType = gisUploadAttempt.GisUploadSourceOrganization.RelationshipTypeForDefaultOrganization;

            var projectOrganizations = projects
                .Select(x => new ProjectOrganization(x, leadImplementerOrganization, relationshipType)).ToList();

            HttpRequestStorage.DatabaseEntities.ProjectOrganizations.AddRange(projectOrganizations);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();


            var pruningAcresMetadataAttributeID = viewModel.PruningAcresMetadataAttributeID;
            var thinningAcresMetadataAttributeID = viewModel.ThinningAcresMetadataAttributeID;
            var chippingAcresMetadataAttributeID = viewModel.ChippingAcresMetadataAttributeID;
            var masticationAcresMetadataAttributeID = viewModel.MasticationAcresMetadataAttributeID;
            var grazingAcresMetadataAttributeID = viewModel.GrazingAcresMetadataAttributeID;
            var lopScatAcresMetadataAttributeID = viewModel.LopScatAcresMetadataAttributeID;
            var biomassRemovalAcresMetadataAttributeID = viewModel.BiomassRemovalAcresMetadataAttributeID;
            var handPileAcresMetadataAttributeID = viewModel.HandPileAcresMetadataAttributeID;
            var handPileBurnAcresMetadataAttributeID = viewModel.HandPileBurnAcresMetadataAttributeID;
            var machinePileBurnAcresMetadataAttributeID = viewModel.MachinePileBurnAcresMetadataAttributeID;
            var broadcastBurnAcresMetadataAttributeID = viewModel.BroadcastBurnAcresMetadataAttributeID;
            var otherAcresMetadataAttributeID = viewModel.OtherAcresMetadataAttributeID;

            if (!sourceOrganization.ImportAsDetailedLocationInsteadOfTreatments)
            {
                ExecProcImportTreatmentsFromGisUploadAttempt(gisUploadAttemptID
                    , projectIdentifierMetadataAttributeID
                    , footprintAcresMetadataAttributeID
                    , treatedAcresMetadataAttributeID
                    , treatmentTypeMetadataAttributeID
                    , treatmentActivityTypeMetadataAttributeID
                    , sourceOrganization
                    , pruningAcresMetadataAttributeID
                    , thinningAcresMetadataAttributeID
                    , chippingAcresMetadataAttributeID
                    , masticationAcresMetadataAttributeID
                    , grazingAcresMetadataAttributeID
                    , lopScatAcresMetadataAttributeID
                    , biomassRemovalAcresMetadataAttributeID
                    , handPileAcresMetadataAttributeID
                    , handPileBurnAcresMetadataAttributeID
                    , machinePileBurnAcresMetadataAttributeID
                    , broadcastBurnAcresMetadataAttributeID
                    , otherAcresMetadataAttributeID
                    , startDateMetadataAttributeID
                    , completionDateMetadataAttributeID);
            }

           

            var projectPriorityLandscapesCalculated = HttpRequestStorage.DatabaseEntities.GetfGetProjectPriorityLandscapes(gisUploadAttempt.GisUploadAttemptID).ToList();
            var projectPriorityLandscapes = projectPriorityLandscapesCalculated
                .Select(x => new ProjectPriorityLandscape(x.ProjectID, x.PriorityLandscapeID)).ToList();
            HttpRequestStorage.DatabaseEntities.GetObjectContext().CommandTimeout = 180;
            var projectUplandDnrRegionsCalculated = HttpRequestStorage.DatabaseEntities.GetfGetProjectDnrUploadRegions(gisUploadAttempt.GisUploadAttemptID).ToList();
            var projectRegions = projectUplandDnrRegionsCalculated
                .Select(x => new ProjectRegion(x.ProjectID, x.DNRUplandRegionID)).ToList();

            HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapes.AddRange(projectPriorityLandscapes);
            HttpRequestStorage.DatabaseEntities.ProjectRegions.AddRange(projectRegions);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();


            
            UpdateProjectTypesIfNeeded(gisUploadAttempt);
            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();


            return new ModalDialogFormJsonResult();
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
                    var treatments = project.Treatments;
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

        private static void MakeProject(GisMetadataAttribute projectIdentifierMetadataAttribute, 
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
                                        List<ProjectLocation> projectLocationList, 
                                        Dictionary<int, List<GisFeatureMetadataAttribute>> privateLandownerDictionary,
                                        List<Person> existingPersonList, 
                                        List<Person> newPersonList, 
                                        List<ProjectPerson> newProjectPersonList, 
                                        int programID, 
                                        ref int currentCounter)
        {

            var gisFeaturesIdListWithProjectIdentifier =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                    string.Equals(x.GisFeatureMetadataAttributeValue, distinctGisValue,
                        StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();
            

            var completionDateAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(x => completionDateDictionary.ContainsKey(x))
                .SelectMany(x => completionDateDictionary[x]).ToList();
            var completionAttributes = completionDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out var date)).Select(x => DateTime.Parse(x)).ToList();
            var completionDate = completionAttributes.Any() ? completionAttributes.Max() : (DateTime?)null;

            if (gisUploadAttempt.GisUploadSourceOrganization.RequireCompletionDate && !completionDate.HasValue)
            {
                return;
            }


            var projectNumber = $"FHT-{DateTime.Now.Year}-{currentCounter:00000}";



            var startDateAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => startDateDictionary.ContainsKey(x))
                .SelectMany(x => startDateDictionary[x]).ToList();
            var startAttributes = startDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out var date)).Select(x => DateTime.Parse(x)).ToList();
            var startDate = startAttributes.Any() ? startAttributes.Min() : (DateTime?)null;

            var privateLandownerAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => privateLandownerDictionary.ContainsKey(x))
                .SelectMany(x => privateLandownerDictionary[x]).ToList();
            var landOwners = privateLandownerAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();

            var projectNameAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => projectNameDictionary.ContainsKey(x))
                .SelectMany(x => projectNameDictionary[x]).ToList();
            var projectNames = projectNameAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();

            var projectStageAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(x => projectStageDictionary.ContainsKey(x))
                .SelectMany(x => projectStageDictionary[x]).ToList();

            if (projectNames.Count > 1)
            {
                var projectNameLower = projectNames.Select(x => x.ToLowerInvariant()).Distinct().ToList();
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
            
            ProjectStage projectStage = gisUploadAttempt.GisUploadSourceOrganization.ProjectStageDefault;
            projectStage = CalculateProjectStageIfNeeded(gisCrossWalkDefaultList, gisUploadAttempt, projectStageAttributes, completionDate, projectStage);

            var projectTypeMappedString = string.IsNullOrEmpty(gisUploadSourceOrganization.ProjectTypeDefaultName) ? string.Empty : gisUploadSourceOrganization.ProjectTypeDefaultName;
            var projectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.SingleOrDefault(x =>
                x.ProjectTypeName.Equals(projectTypeMappedString, StringComparison.InvariantCultureIgnoreCase));

            var projectDescription = gisUploadAttempt.GisUploadSourceOrganization.ProjectDescriptionDefaultText;

            var project = new Project(otherProjectType.ProjectTypeID, 
                                      projectStage.ProjectStageID, 
                                      projectName, 
                                      false, 
                                      ProjectLocationSimpleType.None.ProjectLocationSimpleTypeID, 
                                      ProjectApprovalStatus.Approved.ProjectApprovalStatusID, 
                                      projectNumber
                                      );

            if (gisUploadSourceOrganization.ApplyCompletedDateToProject)
            {
                project.CompletionDate = completionDate;
            }

            if (gisUploadSourceOrganization.ApplyStartDateToProject)
            {
                project.PlannedDate = startDate;
            }

            project.CreateGisUploadAttemptID = gisUploadAttemptID;
            project.ProjectGisIdentifier = distinctGisValue;
            project.ProjectDescription = projectDescription;
            if (projectType != null)
            {
                project.ProjectType = projectType;
            }

            projectList.Add(project);

            if (gisUploadAttempt.GisUploadSourceOrganization.ImportAsDetailedLocationInsteadOfTreatments || gisUploadAttempt.GisUploadSourceOrganization.ImportAsDetailedLocationInAdditionToTreatments)
            {
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

            if (landOwners.Any())
            {
                foreach (var landOwner in landOwners)
                {
                    var firstName = string.Empty;
                    string lastName = null;
                    var landownerSplit = landOwner.Split(',');
                    if (landownerSplit.Length == 1 || landownerSplit.Length > 2)
                    {
                        firstName = landOwner.Trim();
                    }

                    if (landownerSplit.Length == 2)
                    {
                        firstName = landownerSplit[1].Trim();
                        lastName = landownerSplit[0].Trim();
                    }

                    var existingPersons = existingPersonList.Where(x =>
                        string.Equals(x.FirstName, firstName, StringComparison.InvariantCultureIgnoreCase) &&
                        (string.Equals(x.LastName, lastName, StringComparison.InvariantCultureIgnoreCase) || (string.IsNullOrEmpty(x.LastName) && string.IsNullOrEmpty(lastName)))).ToList();

                    var existingNewPersons = newPersonList.Where(x =>
                        string.Equals(x.FirstName, firstName, StringComparison.InvariantCultureIgnoreCase) &&
                        (string.Equals(x.LastName, lastName, StringComparison.InvariantCultureIgnoreCase) || (string.IsNullOrEmpty(x.LastName) && string.IsNullOrEmpty(lastName)))).ToList();
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
                         var person = new Person(firstName, Role.Unassigned, DateTime.Now, true, false){LastName = lastName};
                         newPersonList.Add(person);
                         var projectPerson = new ProjectPerson(project, person,
                             ProjectPersonRelationshipType.PrivateLandowner);
                         newProjectPersonList.Add(projectPerson);
                    }
                }
            }

            currentCounter++;
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

                if (!completionDate.HasValue && gisUploadAttempt.GisUploadSourceOrganization.ImportIsFlattened.HasValue &&
                    gisUploadAttempt.GisUploadSourceOrganization.ImportIsFlattened.Value)
                {
                    projectStage = ProjectStage.Planned;
                }

                else if (!completionDate.HasValue)
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
            var listOfAllFeatures = reprojectedFeatureCollection.Features;
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

        private void SaveGisUploadToNormalizedFields( GisUploadAttempt gisUploadAttempt, bool importWasSuccessful)
        {
            if (importWasSuccessful)
            {
                var realColumns = HttpRequestStorage.DatabaseEntities.GetfGetColumnNamesForTables(GisUploadAttemptStaging.GISImportTableName).ToList();
                var dataColumns = realColumns.Where(x =>
                        x.PrimaryKey != 1 &&
                        !string.Equals(x.ColumnName, GisUploadAttemptStaging.GeomName, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
                var gisMetdataAttributes = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList();
                foreach (var fGetColumnNamesForTableResult in dataColumns)
                {
                    var existingGisMetdataAttribute = gisMetdataAttributes.SingleOrDefault(x => string.Equals(x.GisMetadataAttributeName, fGetColumnNamesForTableResult.ColumnName, StringComparison.InvariantCultureIgnoreCase));
                    if (existingGisMetdataAttribute == null)
                    {
                        HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Add(new GisMetadataAttribute(fGetColumnNamesForTableResult.ColumnName.ToLowerInvariant()));
                        HttpRequestStorage.DatabaseEntities.SaveChanges();
                    }

                    existingGisMetdataAttribute = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList().Single(x => string.Equals(x.GisMetadataAttributeName, fGetColumnNamesForTableResult.ColumnName, StringComparison.InvariantCultureIgnoreCase));

                    HttpRequestStorage.DatabaseEntities.GisUploadAttemptGisMetadataAttributes.Add(new GisUploadAttemptGisMetadataAttribute(gisUploadAttempt, existingGisMetdataAttribute, fGetColumnNamesForTableResult.PrimaryKey));
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                }
            }

            var gisRecords = GetGisRecordsFromGisUploadAttempt(gisUploadAttempt.GisUploadAttemptID);
            var listOfPossibleAttributes = HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.ToList();


            var attributeDictionary = listOfPossibleAttributes.ToDictionary(x => x.GisMetadataAttributeName, y => y);

            var listOfGisFeatureMetadataAttributesToAdd = gisRecords.AsParallel().SelectMany(y => y.GisColumnNames.AsParallel().Select(x => new GisFeatureMetadataAttribute(y.GisFeature.GisFeatureID, attributeDictionary[x.ColumnName].GisMetadataAttributeID) {GisFeatureMetadataAttributeValue = x.ColumnValue}));
            HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.AddRange(
                listOfGisFeatureMetadataAttributesToAdd);

            HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
        }

        private bool ImportGdbToSql(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            GisUploadAttemptStaging.ImportGdbIntoSqlTempTable(fileInfo);
            return true;
        }

        private bool ImportExtractedShapefileToSql(string shapeFilePath)
        {
            var importWasSuccesful = false;
            try
            {
                GisUploadAttemptStaging.ImportShapefileIntoSqlTempTable(shapeFilePath);
                importWasSuccesful = true;
                SetMessageForDisplay("The GIS file was imported. Please review the shape of the data");
            }
            catch (Ogr2OgrCommandLineException e)
            {
                SetErrorForDisplay(e.Message);
            }

            return importWasSuccesful;
        }

        
        private List<GisRecord> GetGisRecordsFromGisUploadAttempt(int gisUploadAttemptID)
        {
            var realColumns = HttpRequestStorage.DatabaseEntities.GetfGetColumnNamesForTables(GisUploadAttemptStaging.GISImportTableName).ToList();
            var dataColumns = realColumns.Where(x =>
                    x.PrimaryKey != 1 &&
                    !string.Equals(x.ColumnName, GisUploadAttemptStaging.GeomName, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            var idColumn = realColumns.Single(x => x.PrimaryKey == 1);
            var dictionary = new Dictionary<int, List<GisColumnName>>();
            var listOfIds = GetIDColumnValuesForGisImport(idColumn.ColumnName);
            ExecProcImportGisTableToGisFeature( gisUploadAttemptID);
            var gisUploadAttemptRefreshed = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
            var listOfFeatures = gisUploadAttemptRefreshed.GisFeatures.ToList();
            DropGeomColumnFromImportTable(GisUploadAttemptStaging.GeomName);
            foreach (var gisColumnName in listOfIds)
            {
                var parsedIntID = int.Parse(gisColumnName.ID);
                dictionary.Add(parsedIntID, new List<GisColumnName>());
            }

            foreach (var fGetColumnNamesForTableResult in dataColumns)
            {
                var columnValues = GetColumnValuesForGisImport(fGetColumnNamesForTableResult.ColumnName, fGetColumnNamesForTableResult.PrimaryKey, idColumn.ColumnName);
                foreach (var gisColumnName in columnValues)
                {
                    var parsedIntID = int.Parse(gisColumnName.ID);
                    dictionary[parsedIntID].Add(gisColumnName);
                }
            }

            var gisRecordList = dictionary.Select(x => x.Value).ToList().Select(y => new GisRecord(y)).ToList();

            foreach (var gisRecord in gisRecordList)
            {
                var gisRecordID = int.Parse(gisRecord.GisColumnNames.First().ID);
                gisRecord.ID = gisRecordID;
                var featureRecordsMatchingGisRecordID =
                    listOfFeatures.Where(x => x.GisImportFeatureKey == gisRecordID).ToList();
                var featureRecord = featureRecordsMatchingGisRecordID.Single();
                gisRecord.GisFeature = featureRecord;
            }

            return gisRecordList;
        }


        private List<GisColumnName> GetColumnValuesForGisImport( string dataColumName, int sortOrder, string idColumnName)
        {
            List<GisColumnName> listOfColumnNames;
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQuery = $"SELECT * from dbo.{GisUploadAttemptStaging.GISImportTableName}";
            using (var command = new SqlCommand(sqlQuery))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    using (var dt = ProjectFirmaSqlDatabase.ExecuteSqlCommand(command).Tables[0])
                    {
                        listOfColumnNames =
                            dt.Rows.Cast<DataRow>()
                                .Select(x => new GisColumnName(x[idColumnName].ToString(), x[dataColumName].ToString(), dataColumName, sortOrder))   
                                .ToList();
                    }
                }
            }

            return listOfColumnNames;
        }

        private List<GisColumnName> GetIDColumnValuesForGisImport(string idColumnName)
        {
            List<GisColumnName> listOfColumnNames;
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQuery = $"SELECT {idColumnName} from dbo.{GisUploadAttemptStaging.GISImportTableName}";
            using (var command = new SqlCommand(sqlQuery))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    using (var dt = ProjectFirmaSqlDatabase.ExecuteSqlCommand(command).Tables[0])
                    {
                        listOfColumnNames =
                            dt.Rows.Cast<DataRow>()
                                .Select(x => new GisColumnName(x[idColumnName].ToString(), x[idColumnName].ToString(), idColumnName, 1))
                                .ToList();
                    }
                }
            }

            return listOfColumnNames;
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

        private void DropGeomColumnFromImportTable( string dataColumName)
        {
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var indexName = $"ogr_dbo_{GisUploadAttemptStaging.GISImportTableName}_{dataColumName}_sidx";
            var sqlQueryOne = $"if exists(SELECT 1 FROM sys.indexes WHERE name='{indexName}' AND object_id = OBJECT_ID('dbo.{GisUploadAttemptStaging.GISImportTableName}')) begin DROP INDEX [{indexName}] ON [dbo].[{GisUploadAttemptStaging.GISImportTableName}] end ";
            using (var command = new SqlCommand(sqlQueryOne))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);
                   
                }
            }


            var sqlQueryTwo = $"ALTER TABLE dbo.{GisUploadAttemptStaging.GISImportTableName} DROP COLUMN {dataColumName}";
            using (var command = new SqlCommand(sqlQueryTwo))
            {
                var sqlConnection = new SqlConnection(sqlDatabaseConnectionString);
                using (var conn = sqlConnection)
                {
                    command.Connection = conn;
                    ProjectFirmaSqlDatabase.ExecuteSqlCommand(command);

                }
            }
        }

        private void ExecProcImportGisTableToGisFeature( int gisUploadAttemptID)
        {
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQueryOne = $"exec dbo.procImportGisTableToGisFeature @piGisUploadAttemptID = {gisUploadAttemptID}";
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