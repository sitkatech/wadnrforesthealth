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
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GdalOgr;
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
            var viewModel = new GisMetadataViewModel();
            return ViewUploadGisMetadata(gisUploadAttempt, viewModel);
        }

        private ViewResult ViewUploadGisMetadata(GisUploadAttempt gisUploadAttempt, GisMetadataViewModel gisMetadataViewModel)
        {
            var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
            var realColumns = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.ToList();
            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns);
            
            var gisMetadataPostUrl = SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.GisMetadata(gisUploadAttempt.GisUploadAttemptID, null));
            var projectIndexUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Index());
            var gisMetadataAttributeIDs = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.Select(x => x.GisMetadataAttributeID).ToList();
            var metadataAttributes =
                HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Where(x =>
                    gisMetadataAttributeIDs.Contains(x.GisMetadataAttributeID));
            var viewData = new GisMetadataViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, gridSpec, metadataAttributes, gisMetadataPostUrl, projectIndexUrl);
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

            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GisFeature>(gisFeatureList, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult GisMetadata(GisUploadAttemptPrimaryKey gisUploadAttemptPrimaryKey, GisMetadataViewModel viewModel)
        {
            var gisUploadAttempt = gisUploadAttemptPrimaryKey.EntityObject;
            var gisUploadAttemptID = gisUploadAttempt.GisUploadAttemptID;
            var projectIdentifierMetadataAttributeID = viewModel.ProjectIdentifierMetadataAttributeID;
            var projectNameMetadataAttributeID = viewModel.ProjectNameMetadataAttributeID;
            var completionDateMetadataAttributeID = viewModel.CompletionDateMetadataAttributeID;
            var startDateMetadataAttributeID = viewModel.StartDateMetadataAttributeID;
            var projectStageMetadataAttributeID = viewModel.ProjectStageMetadataAttributeID;
            var projectTypeMetadataAttributeID = viewModel.ProjectTypeMetadataAttributeID;
            var otherTreatmentAcresMetadataAttributeID = viewModel.OtherTreatmentAcresMetadataAttributeID;

            var projectIdentifierMetadataAttribute =
                gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.Single(x =>
                    x.GisMetadataAttributeID == projectIdentifierMetadataAttributeID).GisMetadataAttribute;

            var gisFeatureIDs = gisUploadAttempt.GisFeatures.Select(x => x.GisFeatureID);

            var completionDateDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == completionDateMetadataAttributeID).GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var projectNameDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == projectNameMetadataAttributeID).GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var startDateDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == startDateMetadataAttributeID).GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var projectStageDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == projectStageMetadataAttributeID).GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());

            var projectTypeDictionary = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x =>
                gisFeatureIDs.Contains(x.GisFeatureID) &&
                x.GisMetadataAttributeID == projectTypeMetadataAttributeID).GroupBy(y => y.GisFeatureID).ToDictionary(y => y.Key, x => x.ToList());


            

            var gisValues =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x => gisFeatureIDs.Contains(x.GisFeatureID));
            var distinctGisValues = gisValues.Select(x => x.GisFeatureMetadataAttributeValue).Where(y => !string.IsNullOrEmpty(y)).Distinct();

            var otherProjectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList().Single(x =>
                string.Equals("Other", x.ProjectTypeName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            var projectList = new List<Project>();

            var gisCrossWalkDefaultList = HttpRequestStorage.DatabaseEntities.GisCrossWalkDefaults.ToList();

            foreach (var distinctGisValue in distinctGisValues)
            {
                MakeProject(projectIdentifierMetadataAttribute, distinctGisValue, completionDateDictionary, startDateDictionary, projectNameDictionary, 
                    projectStageDictionary, projectTypeDictionary, gisCrossWalkDefaultList, gisUploadAttempt, otherProjectType, gisUploadAttemptID, projectList);
            }

            ExecProcImportTreatmentsFromGisUploadAttempt(gisUploadAttemptID, projectIdentifierMetadataAttributeID, otherTreatmentAcresMetadataAttributeID);



            var projects = HttpRequestStorage.DatabaseEntities.Projects.Where(x =>
                x.CreateGisUploadAttemptID.HasValue && x.CreateGisUploadAttemptID.Value == gisUploadAttemptID).ToList();

            var projectIDList = projects.Select(x => x.ProjectID).ToList();

            var treatments =
                HttpRequestStorage.DatabaseEntities.Treatments.Where(x => projectIDList.Contains(x.ProjectID)).ToList();

            var treatmentDictionary = treatments.GroupBy(x => x.ProjectID).ToDictionary(y => y.Key, x => x.ToList().Select(z => z.TreatmentFeature).ToList());

            projects.ForEach(x => x.AutoAssignProjectPriorityLandscapes(treatmentDictionary[x.ProjectID]));

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        private static void MakeProject(GisMetadataAttribute projectIdentifierMetadataAttribute, string distinctGisValue,
            Dictionary<int, List<GisFeatureMetadataAttribute>> completionDateDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> startDateDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> projectNameDictionary,
           Dictionary<int, List<GisFeatureMetadataAttribute>> projectStageDictionary, Dictionary<int, List<GisFeatureMetadataAttribute>> projectTypeDictionary,


            List<GisCrossWalkDefault> gisCrossWalkDefaultList,
            GisUploadAttempt gisUploadAttempt, ProjectType otherProjectType, int gisUploadAttemptID, List<Project> projectList)
        {
            var gisFeaturesIdListWithProjectIdentifier =
                projectIdentifierMetadataAttribute.GisFeatureMetadataAttributes.Where(x =>
                    string.Equals(x.GisFeatureMetadataAttributeValue, distinctGisValue,
                        StringComparison.InvariantCultureIgnoreCase)).Select(x => x.GisFeatureID).ToList();
            var completionDateAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(x => completionDateDictionary.ContainsKey(x))
                .SelectMany(x => completionDateDictionary[x]).ToList();

            var startDateAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => startDateDictionary.ContainsKey(x))
                .SelectMany(x => startDateDictionary[x]).ToList();

            var projectNameAttributes = gisFeaturesIdListWithProjectIdentifier.Where(x => projectNameDictionary.ContainsKey(x))
                .SelectMany(x => projectNameDictionary[x]).ToList();

            var projectStageAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(x => projectStageDictionary.ContainsKey(x))
                .SelectMany(x => projectStageDictionary[x]).ToList();

            var projectTypeAttributes = gisFeaturesIdListWithProjectIdentifier
                .Where(x => projectTypeDictionary.ContainsKey(x))
                .SelectMany(x => projectTypeDictionary[x]).ToList();

            var completionAttributes = completionDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out var date)).Select(x => DateTime.Parse(x)).ToList();
            var startAttributes = startDateAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct()
                .Where(x => DateTime.TryParse(x, out var date)).Select(x => DateTime.Parse(x)).ToList();
            var projectNames = projectNameAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();
            var projectStages = projectStageAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();
            var projectTypes = projectTypeAttributes.Select(x => x.GisFeatureMetadataAttributeValue).Distinct().ToList();

            var completionDate = completionAttributes.Any() ? completionAttributes.Max() : (DateTime?) null;
            var startDate = startAttributes.Any() ? startAttributes.Min() : (DateTime?) null;
            var projectName = projectNames.Single();
            var projectStageString = projectStages.Single();
            var projectTypeString = projectTypes.First();

            var projectStageCrossWalks = gisCrossWalkDefaultList.Where(x =>
                x.FieldDefinitionID == FieldDefinition.ProjectStage.FieldDefinitionID &&
                x.GisUploadSourceOrganizationID == gisUploadAttempt.GisUploadSourceOrganizationID);

            var projectTypeCrossWalks = gisCrossWalkDefaultList.Where(x =>
                x.FieldDefinitionID == FieldDefinition.ProjectType.FieldDefinitionID &&
                x.GisUploadSourceOrganizationID == gisUploadAttempt.GisUploadSourceOrganizationID);

            var projectStageMappedString = projectStageCrossWalks.Single(x =>
                    x.GisCrossWalkSourceValue.Equals(projectStageString, StringComparison.InvariantCultureIgnoreCase))
                ?.GisCrossWalkMappedValue;

            var projectTypeMappedString = projectTypeCrossWalks.Single(x =>
                    x.GisCrossWalkSourceValue.Equals(projectTypeString, StringComparison.InvariantCultureIgnoreCase))
                ?.GisCrossWalkMappedValue;


            var projectStage = ProjectStage.All.Single(x =>
                x.ProjectStageName.Equals(projectStageMappedString, StringComparison.InvariantCultureIgnoreCase));

            var projectType = HttpRequestStorage.DatabaseEntities.ProjectTypes.Single(x =>
                x.ProjectTypeName.Equals(projectTypeMappedString, StringComparison.InvariantCultureIgnoreCase));

            var project = new Project(otherProjectType.ProjectTypeID
                , projectStage.ProjectStageID
                , projectName
                , "fake description"
                , false
                , ProjectLocationSimpleType.None.ProjectLocationSimpleTypeID
                , ProjectApprovalStatus.Approved.ProjectApprovalStatusID
                , Project.CreateNewFhtProjectNumber());
            project.CompletionDate = completionDate;
            project.PlannedDate = startDate;
            project.CreateGisUploadAttemptID = gisUploadAttemptID;
            project.ProjectGisIdentifier = distinctGisValue;
            project.ProjectType = projectType;

            var projectGeometries = gisUploadAttempt.GisFeatures
                .Where(x => gisFeaturesIdListWithProjectIdentifier.Contains(x.GisFeatureID))
                .Select(x => x.GisFeatureGeometry)
                .ToList();

            
            
            HttpRequestStorage.DatabaseEntities.Projects.Add(project);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            projectList.Add(project);
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
            var importWasSuccesful = false;

            if (shapeFileSuccessfullyExtractedToDisk)
            {
                importWasSuccesful = ImportExtractedShapefileToSql(shapeFilePath);
            }

            else
            {
                var filePath = GisUploadAttemptStaging.SaveFileToDiskIfGdb(httpPostedFileBase, gisUploadAttempt);
                importWasSuccesful = ImportGdbToSql(filePath);
            }

            if (importWasSuccesful)
            {
                gisUploadAttempt.ImportTableName = GisUploadAttemptStaging.GISImportTableName;
            }
            
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SaveGisUploadToNormalizedFields(gisUploadAttempt, importWasSuccesful);
            return new ModalDialogFormJsonResult();
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

        private void ExecProcImportTreatmentsFromGisUploadAttempt(int gisUploadAttemptID, int projectIdentifierMetadataAttributeID, int? otherTreatmentAcresMetdataAttributeID)
        {
            int otherTreatmentAcresID;
            if (!otherTreatmentAcresMetdataAttributeID.HasValue)
            {
                otherTreatmentAcresID = -1;
            }
            else
            {
                otherTreatmentAcresID = otherTreatmentAcresMetdataAttributeID.Value;
            }
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQueryOne = $"exec dbo.procImportTreatmentsFromGisUploadAttempt @piGisUploadAttemptID = {gisUploadAttemptID}, @projectIdentifierGisMetadataAttributeID = {projectIdentifierMetadataAttributeID}, @otherTreatmentAcresMetadataAttributeID = {otherTreatmentAcresID}";
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