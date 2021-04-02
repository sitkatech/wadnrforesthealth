using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Models.ExcelUpload;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.UnitTestCommon;
using ProjectFirma.Web.Views.ExcelUpload;

namespace ProjectFirma.Web.Controllers
{
    public class ExcelUploadController : FirmaBaseController
    {


        [CrossAreaRoute]
        [HttpGet]
        [FirmaAdminFeature]
        public ViewResult ManageExcelUploadsAndEtl()
        {
            return ViewManageExcelUploadsAndEtl_Impl();
        }

        private ViewResult ViewManageExcelUploadsAndEtl_Impl()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.UploadLoaTabularDataExcel);
            var loaUploadFormID = GenerateUploadLoaFileUploadFormId();

            var newLoaNortheastExcelFileUpload = SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.ImportLoaNortheastExcelFile());
            var newLoaSoutheastExcelFileUpload = SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.ImportLoaSoutheastExcelFile());
            var doPublishingProcessingPostUrl = SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.DoPublishingProcessing(null));
            var listOfTabularDataImports = HttpRequestStorage.DatabaseEntities.TabularDataImports.ToList();
            var viewData = new ManageExcelUploadsAndEtlViewData(CurrentPerson,
                firmaPage,
                newLoaNortheastExcelFileUpload,
                newLoaSoutheastExcelFileUpload,
                doPublishingProcessingPostUrl,
                loaUploadFormID,
                listOfTabularDataImports);
            var viewModel = new ManageExcelUploadsAndEtlViewModel();
            return RazorView<ManageExcelUploadsAndEtl, ManageExcelUploadsAndEtlViewData, ManageExcelUploadsAndEtlViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FirmaAdminFeature]
        public ActionResult DoPublishingProcessing()
        {
            throw new NotImplementedException("Just here to provide signature; do not call.");
        }

        [HttpPost]
        [FirmaAdminFeature]
        public ActionResult DoPublishingProcessing(ManageExcelUploadsAndEtlViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new SitkaDisplayErrorException("Not expecting model state to be bad; not running publishing processing.");
            }

            bool wasErrorDuringProcessing = false;
            DateTime startTime = DateTime.Now;
            try
            {
                DoPublishingSql();
                var tabularImports = HttpRequestStorage.DatabaseEntities.TabularDataImports.ToList();
                var processedDateTime = DateTime.Now;
                var latestImportForNortheast = TabularDataImport.GetLatestImportProcessingForGivenType(tabularImports, TabularDataImportTableType.LoaNortheast);
                var latestImportForSoutheast = TabularDataImport.GetLatestImportProcessingForGivenType(tabularImports, TabularDataImportTableType.LoaSoutheast);
                if (latestImportForSoutheast != null)
                {
                    latestImportForSoutheast.LastProcessedDate = processedDateTime;
                    latestImportForSoutheast.LastProcessedPerson = CurrentPerson;
                }

                if (latestImportForNortheast != null)
                {
                    latestImportForNortheast.LastProcessedDate = processedDateTime;
                    latestImportForNortheast.LastProcessedPerson = CurrentPerson;
                }
                HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);
                
            }
            catch (Exception e)
            {
                SetInfoForDisplay($"Problem executing Publishing: {e.Message}");
                wasErrorDuringProcessing = true;
            }

            DateTime endTime = DateTime.Now;
            var elapsedTime = endTime - startTime;
            string processingTimeString = GetTaskTimeString("Publishing", elapsedTime);

            if (!wasErrorDuringProcessing)
            {
                SetMessageForDisplay($"Publishing Processing completed Successfully.<br/>{processingTimeString}");
            }
            else
            {
                // Apparently at the moment we can only have one error/warning, so since I want TWO messages, resorting to 
                // an error and a warning.
                SetErrorForDisplay($"Publishing Processing had problems.<br/>{processingTimeString}");
            }
            return RedirectToAction(new SitkaRoute<ExcelUploadController>(x => x.ManageExcelUploadsAndEtl()));
        }

        public static void DoPublishingSql()
        {
            try
            {
                var sqlDatabaseConnectionString = FirmaWebConfiguration.DatabaseConnectionString;
                var sqlQueryOne = $"dbo.pImportLoaTabularData";
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new SitkaDisplayErrorException($"Problem calling SQL: {e.Message}");
            }
        }

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult ImportLoaNortheastExcelFile()
        {
            var viewModel = new ImportLoaExcelFileViewModel();
            return ViewImportLoaExcelFile(viewModel, true);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ImportLoaNortheastExcelFile(ImportLoaExcelFileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewImportLoaExcelFile(viewModel, true);
            }

            var httpPostedFileBase = viewModel.FileResourceData;

            return DoLoaExcelImportForHttpPostedFile(httpPostedFileBase, true);
        }


        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult ImportLoaSoutheastExcelFile()
        {
            var viewModel = new ImportLoaExcelFileViewModel();
            return ViewImportLoaExcelFile(viewModel, false);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ImportLoaSoutheastExcelFile(ImportLoaExcelFileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewImportLoaExcelFile(viewModel, false);
            }

            var httpPostedFileBase = viewModel.FileResourceData;

            return DoLoaExcelImportForHttpPostedFile(httpPostedFileBase, false);
        }

        private PartialViewResult ViewImportLoaExcelFile(ImportLoaExcelFileViewModel viewModel, bool isNortheast)
        {
            var fmbsExcelFileUploadFormID = GenerateUploadLoaFileUploadFormId();
            string newGisUploadUrl = string.Empty;
            if (isNortheast)
            {
                newGisUploadUrl = SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.ImportLoaNortheastExcelFile(null));
            }
            else
            {
                newGisUploadUrl = SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.ImportLoaSoutheastExcelFile(null));
            }
            
            var viewData = new ImportLoaExcelFileViewData(fmbsExcelFileUploadFormID, newGisUploadUrl);
            return RazorPartialView<ImportLoaExcelFile, ImportLoaExcelFileViewData, ImportLoaExcelFileViewModel>(viewData, viewModel);
        }

        [FirmaAdminFeature]
        private ActionResult DoLoaExcelImportForHttpPostedFile(HttpPostedFileBase httpPostedFileBase, bool isNortheast)
        {
            return DoLoaExcelImportForFileStream(httpPostedFileBase.InputStream, httpPostedFileBase.FileName, isNortheast);
        }

        public static string GenerateUploadLoaFileUploadFormId()
        {
            return $"uploadLoaFileUpload";
        }
        private static string GetTaskTimeString(string taskString, TimeSpan elapsedTime)
        {
            return $"{taskString} took {elapsedTime.TotalSeconds:0.0} seconds ({elapsedTime.TotalMinutes:0.00} minutes)";
        }

        private ActionResult Common_LoadFromXls_ExceptionHandler(Stream excelFileAsStream,
                                                                string optionalOriginalFilename,
                                                                Exception ex)
        {
            string tempExcelFilename = Path.GetTempFileName() + ".xlsx";
            using (var excelFileStream = System.IO.File.Create(tempExcelFilename))
            {
                excelFileAsStream.Seek(0, SeekOrigin.Begin);
                excelFileAsStream.CopyTo(excelFileStream);

                // If this is something really weird...
                if (!(ex is SitkaDisplayErrorException))
                {
                    // We want to capture the Excel file for future reference, since this blew up. But we really should be logging it into the logging folder and not a temp folder.
                    var errorLogMessage =
                        $"Unexpected exception while uploading Excel file by PersonID {CurrentPerson.PersonID} ({CurrentPerson.FullNameFirstLast}). Original filename \"{optionalOriginalFilename}\" File saved at \"{tempExcelFilename}\".\r\nException Details:\r\n{ex}";
                    SitkaLogger.Instance.LogDetailedErrorMessage(errorLogMessage);
                }

                var errorMessage =
                    $"There was a problem uploading your spreadsheet \"{optionalOriginalFilename}\": <br/><div style=\"\">{ex.Message}</div><br/><div>Nothing was saved to the database.</div><br/>If you need help, please email your spreadsheet to <a href=\"mailto:{FirmaWebConfiguration.SitkaSupportEmail}\">{FirmaWebConfiguration.SitkaSupportEmail}</a> with a note and we will try to help out.";
                // We originally did not do this, assuming the user would self correct, but it turns out Dorothy was expecting us to see crashes and respond, which is fine.
                // So, instead, we'll send an error email for each and every problem, even the ones we understand. -- SLG 7/9/2020
                SitkaLogger.Instance.LogDetailedErrorMessage(errorMessage);
                SetErrorForDisplay(errorMessage);
            }

            // This is the right thing to return, since this starts off in a modal dialog
            return new ModalDialogFormJsonResult();
        }

        public const int LoaNortheastExcelFileHeaderRowOffset = 0;

        [FirmaAdminFeature]
        private ActionResult DoLoaExcelImportForFileStream(Stream excelFileAsStream, string optionalOriginalFilename, bool isNortheast)
        {
            DateTime startTime = DateTime.Now;
            var errorList = new List<string>();
            List<LoaStageImport> loaStages;
            try
            {
                loaStages = LoaStageImportsHelper.LoadLoaStagesFromXlsFile(excelFileAsStream, LoaNortheastExcelFileHeaderRowOffset, errorList);
            }
            catch (Exception ex)
            {
                return Common_LoadFromXls_ExceptionHandler(excelFileAsStream, optionalOriginalFilename, ex);
            }

            if (isNortheast)
            {
                var previousLoaStagesFromNortheast =
                    HttpRequestStorage.DatabaseEntities.LoaStages.Where(x => x.IsNortheast).ToList();
                previousLoaStagesFromNortheast.ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
            }
            else
            {
                var previousLoaStagesFromSoutheast =
                    HttpRequestStorage.DatabaseEntities.LoaStages.Where(x => x.IsSoutheast).ToList();
                previousLoaStagesFromSoutheast.ForEach(x => x.DeleteFull(HttpRequestStorage.DatabaseEntities));
            }



            var countAdded = 0;
            foreach (var loaStageImport in loaStages)
            {
                if (!string.IsNullOrEmpty(loaStageImport.ProjectID))
                {
                    var loaStage = new LoaStage(loaStageImport, isNortheast);
                    countAdded = countAdded + 1;
                    HttpRequestStorage.DatabaseEntities.LoaStages.Add(loaStage);
                }
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            
            DateTime endTime = DateTime.Now;
            var elapsedTime = endTime - startTime;
            string importTimeString = GetTaskTimeString("Import", elapsedTime);

            // Record that we uploaded
            var tabularDataImportTableTypeID = isNortheast ? TabularDataImportTableType.LoaNortheast.TabularDataImportTableTypeID : TabularDataImportTableType.LoaSoutheast.TabularDataImportTableTypeID;
            var newTabularDataImport = new TabularDataImport(tabularDataImportTableTypeID);
            newTabularDataImport.UploadDate = endTime;
            newTabularDataImport.UploadPerson = CurrentPerson;
            HttpRequestStorage.DatabaseEntities.TabularDataImports.Add(newTabularDataImport);
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            SetMessageForDisplay($"{countAdded.ToGroupedNumeric()} LOA records were successfully imported to database. </br>{importTimeString}.");
            if (errorList.Any())
            {
                SetInfoForDisplay(string.Join("<br>", errorList));
            }
            
            // This is the right thing to return, since this starts off in a modal dialog
            return new ModalDialogFormJsonResult();
        }
    }
}