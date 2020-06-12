﻿using System;
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
            return ViewUploadGisMetadata(gisUploadAttempt);
        }

        private ViewResult ViewUploadGisMetadata(GisUploadAttempt gisUploadAttempt)
        {
            var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
            var realColumns = gisUploadAttempt.GisUploadAttemptGisMetadataAttributes.ToList();
            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns);
            var viewData = new GisMetadataViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, gridSpec);
            return RazorView<GisMetadata, GisMetadataViewData>(viewData);
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
            var fileEnding = ".gdb.zip";
            var shapeFileSuccessfullyExtractedToDisk = false;
            var shapeFilePath = GisUploadAttemptStaging.UnzipAndSaveFileToDiskIfShapefile(httpPostedFileBase, gisUploadAttempt, ref shapeFileSuccessfullyExtractedToDisk);
            var importWasSuccesful = false;

            if (shapeFileSuccessfullyExtractedToDisk)
            {
                importWasSuccesful = ImportExtractedShapefileToSql(shapeFilePath);
            }

            else
            {
                importWasSuccesful = ImportGdbToSql(fileEnding, httpPostedFileBase);
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
                        !string.Equals(x.ColumnName, GisUploadAttemptStaging.FIDName, StringComparison.InvariantCultureIgnoreCase) &&
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

        private bool ImportGdbToSql(string fileEnding, HttpPostedFileBase httpPostedFileBase)
        {
            var importWasSuccessful = false;
            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(fileEnding))
            {
                var gdbFile = disposableTempFile.FileInfo;
                httpPostedFileBase.SaveAs(gdbFile.FullName);
                GisUploadAttemptStaging.ImportGdbIntoSqlTempTable(gdbFile);
                importWasSuccessful = true;
                SetMessageForDisplay("The GIS file was imported. Please review the shape of the data");
            }

            return importWasSuccessful;
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

            var dictionary = new Dictionary<int, List<GisColumnName>>();
            var listOfIds = GetIDColumnValuesForGisImport();
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
                var columnValues = GetColumnValuesForGisImport(fGetColumnNamesForTableResult.ColumnName, fGetColumnNamesForTableResult.PrimaryKey);
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
                var featureRecord = listOfFeatures.Single(x => x.GisImportFeatureKey == gisRecordID);
                gisRecord.GisFeature = featureRecord;
            }

            return gisRecordList;
        }


        private List<GisColumnName> GetColumnValuesForGisImport( string dataColumName, int sortOrder)
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
                                .Select(x => new GisColumnName(x[GisUploadAttemptStaging.FIDName].ToString(), x[dataColumName].ToString(), dataColumName, sortOrder))   
                                .ToList();
                    }
                }
            }

            return listOfColumnNames;
        }

        private List<GisColumnName> GetIDColumnValuesForGisImport()
        {
            List<GisColumnName> listOfColumnNames;
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQuery = $"SELECT {GisUploadAttemptStaging.FIDName} from dbo.{GisUploadAttemptStaging.GISImportTableName}";
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
                                .Select(x => new GisColumnName(x[GisUploadAttemptStaging.FIDName].ToString(), x[GisUploadAttemptStaging.FIDName].ToString(), GisUploadAttemptStaging.FIDName, 1))
                                .ToList();
                    }
                }
            }

            return listOfColumnNames;
        }

        private List<GisShape> GetFeaturesForGisImport(string importedTableName, string idColumnName, string dataColumName)
        {
            List<GisShape> listOfColumnNames;
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQuery = $"SELECT {idColumnName}, {dataColumName}.Serialize() as {dataColumName}  from dbo.{importedTableName}";
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
                                .Select(x => new GisShape(x[idColumnName].ToString(), (byte[]) x[dataColumName], dataColumName))
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
            var sqlQueryOne = $"DROP INDEX [ogr_dbo_{GisUploadAttemptStaging.GISImportTableName}_{dataColumName}_sidx] ON [dbo].[{GisUploadAttemptStaging.GISImportTableName}]";
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