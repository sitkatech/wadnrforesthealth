using System;
using System.Collections.Generic;
using System.Data;
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
        public ActionResult InstructionsGisImport(int gisUploadAttemptID)
        {
            var firmaPageType = FirmaPageType.ToType(FirmaPageTypeEnum.GisUploadAttemptInstructions);
            var firmaPage = FirmaPage.GetFirmaPageByPageType(firmaPageType);
                var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
                var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
                var viewData = new InstructionsGisImportViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, firmaPage, false);

                return RazorView<InstructionsGisImport, InstructionsGisImportViewData>(viewData);

        }



        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult UploadGisFile(int gisUploadAttemptID)
        {
            var uploadGisFileViewModel = new UploadGisFileViewModel();
            var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);

            return ViewUploadGisFile(uploadGisFileViewModel, gisUploadAttempt);
        }


        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult GisMetadata(int gisUploadAttemptID)
        {
            var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
            return ViewUploadGisMetadata(gisUploadAttempt);
        }

        private ViewResult ViewUploadGisMetadata(GisUploadAttempt gisUploadAttempt)
        {
            var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
            var realColumns = HttpRequestStorage.DatabaseEntities.GetfGetColumnNamesForTables(gisUploadAttempt.ImportTableName).ToList();
            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns);
            var viewData = new GisMetadataViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, gridSpec);
            return RazorView<GisMetadata, GisMetadataViewData>(viewData);
        }


        [ProjectsViewFullListFeature]
        public GridJsonNetJObjectResult<GisRecord> GisRecordGridJsonData(int gisUploadAttemptID)
        {
            var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
            var realColumns = HttpRequestStorage.DatabaseEntities.GetfGetColumnNamesForTables(gisUploadAttempt.ImportTableName).ToList();
            var gisRecords = GetGisRecordsFromGisUploadAttempt(gisUploadAttemptID);
            var gridSpec = new GisRecordGridSpec(CurrentPerson, realColumns);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GisRecord>(gisRecords, gridSpec);
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
        public ActionResult UploadGisFile(int gisUploadAttemptID, UploadGisFileViewModel viewModel)
        {
            return UploadGisFilePostImpl(gisUploadAttemptID, viewModel);
        }


        private ActionResult UploadGisFilePostImpl(int gisUploadAttemptID, UploadGisFileViewModel viewModel)
        {
            var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
            if (!ModelState.IsValid)
            {
                return ViewUploadGisFile(viewModel, gisUploadAttempt);
            }

            var httpPostedFileBase = viewModel.FileResourceData;
            var fileEnding = ".gdb.zip";
            var importTableName = string.Empty;
            var shapeFileSuccessfullyExtractedToDisk = false;

            var shapeFilePath = GisUploadAttemptStaging.UnzipAndSaveFileToDiskIfShapefile(httpPostedFileBase, gisUploadAttempt, ref shapeFileSuccessfullyExtractedToDisk);

            if (shapeFileSuccessfullyExtractedToDisk)
            {
                importTableName = ImportExtractedShapefileToSql(shapeFilePath, gisUploadAttempt, importTableName);
            }

            else
            {
                importTableName = ImportGdbToSql(fileEnding, httpPostedFileBase, gisUploadAttempt);
            }
            gisUploadAttempt.ImportTableName = importTableName;
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return new ModalDialogFormJsonResult();
        }

        private string ImportGdbToSql(string fileEnding, HttpPostedFileBase httpPostedFileBase,
            GisUploadAttempt gisUploadAttempt)
        {
            string importTableName;
            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(fileEnding))
            {
                var gdbFile = disposableTempFile.FileInfo;
                httpPostedFileBase.SaveAs(gdbFile.FullName);
                GisUploadAttemptStaging.ImportGdbIntoSqlTempTable(gdbFile, gisUploadAttempt, out importTableName);
                SetMessageForDisplay("The GIS file was imported. Please review the shape of the data");
            }

            return importTableName;
        }

        private string ImportExtractedShapefileToSql(string shapeFilePath, GisUploadAttempt gisUploadAttempt,
            string importTableName)
        {
            try
            {
                GisUploadAttemptStaging.ImportShapefileIntoSqlTempTable(shapeFilePath, gisUploadAttempt, out importTableName);
                SetMessageForDisplay("The GIS file was imported. Please review the shape of the data");
            }
            catch (Ogr2OgrCommandLineException e)
            {
                SetErrorForDisplay(e.Message);
            }

            return importTableName;
        }

        
        private List<GisRecord> GetGisRecordsFromGisUploadAttempt(int gisUploadAttemptID)
        {
            var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
            var importTableName = gisUploadAttempt.ImportTableName;
            var realColumns = HttpRequestStorage.DatabaseEntities.GetfGetColumnNamesForTables(importTableName).ToList();

            var idColumn = realColumns.Where(x => x.PrimaryKey == 1).Single();
            var dataColumns = realColumns.Where(x =>
                    x.PrimaryKey != 1 &&
                    !string.Equals(x.ColumnName, GisUploadAttemptStaging.GeomName, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            var dictionary = new Dictionary<int, List<GisColumnName>>();
            var listOfIds = GetColumnValuesForGisImport(importTableName, idColumn.ColumnName, idColumn.ColumnName, 1);
            var listOfFeatures = GetFeaturesForGisImport(importTableName, idColumn.ColumnName, GisUploadAttemptStaging.GeomName);
            foreach (var gisColumnName in listOfIds)
            {
                var parsedIntID = int.Parse(gisColumnName.ID);
                dictionary.Add(parsedIntID, new List<GisColumnName>());
            }

            foreach (var fGetColumnNamesForTableResult in dataColumns)
            {
                var columnValues = GetColumnValuesForGisImport(importTableName, idColumn.ColumnName,
                    fGetColumnNamesForTableResult.ColumnName, fGetColumnNamesForTableResult.PrimaryKey);
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
                var featureRecord = listOfFeatures.Single(x => int.Parse(x.ID) == gisRecordID);
                gisRecord.Feature = featureRecord.Feature;
            }

            return gisRecordList;
        }


        private List<GisColumnName> GetColumnValuesForGisImport(string importedTableName, string idColumnName, string dataColumName, int sortOrder)
        {
            List<GisColumnName> listOfColumnNames;
            var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var sqlQuery = $"SELECT {idColumnName}, {dataColumName}  from dbo.{importedTableName}";
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



        public class GisRecord
        {
            public List<GisColumnName> GisColumnNames { get; set; }

            public SqlGeometry Feature { get; set; }

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

            public string ColumnnName { get; set; }

            public int SortOrder { get; set; }


            public GisColumnName(string idValue, string columnValue, string columnnName, int sortOrder)
            {
                ID = idValue;
                ColumnValue = columnValue;
                ColumnnName = columnnName;
                SortOrder = sortOrder;
            }

        }


        public class GisShape
        {
            public string ID { get; set; }
            public SqlGeometry Feature { get; set; }

            public string ColumnnName { get; set; }


            public GisShape(string idValue, byte[] featureAsWellKnownText, string columnnName)
            {
                //var buffer = featureAsWellKnownText.ToList().Select(x => x.Value).ToArray();
                var sqlBytes = new SqlBytes(featureAsWellKnownText);
                var feature = SqlGeometry.Deserialize(sqlBytes);
                ID = idValue;
                Feature = feature;
                ColumnnName = columnnName;
            }

        }

    }
}