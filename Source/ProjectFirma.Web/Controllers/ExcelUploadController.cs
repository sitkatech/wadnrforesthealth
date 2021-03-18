using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Models.ExcelUpload;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.ExcelUpload;

namespace ProjectFirma.Web.Controllers
{
    public class ExcelUploadController : FirmaBaseController
    {

        [HttpGet]
        [FirmaAdminFeature]
        public PartialViewResult ImportLoaExcelFile()
        {
            var viewModel = new ImportLoaExcelFileViewModel();
            return ViewImportLoaExcelFile(viewModel);
        }

        [HttpPost]
        [FirmaAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ImportLoaExcelFile(ImportLoaExcelFileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewImportLoaExcelFile(viewModel);
            }

            var httpPostedFileBase = viewModel.FileResourceData;

            return DoLoaExcelImportForHttpPostedFile(httpPostedFileBase);
        }

        private PartialViewResult ViewImportLoaExcelFile(ImportLoaExcelFileViewModel viewModel)
        {
            var fmbsExcelFileUploadFormID = GenerateUploadLoaFileUploadFormId();
            var newGisUploadUrl = SitkaRoute<ExcelUploadController>.BuildUrlFromExpression(x => x.ImportLoaExcelFile(null));
            var viewData = new ImportLoaExcelFileViewData(fmbsExcelFileUploadFormID, newGisUploadUrl);
            return RazorPartialView<ImportLoaExcelFile, ImportLoaExcelFileViewData, ImportLoaExcelFileViewModel>(viewData, viewModel);
        }

        [FirmaAdminFeature]
        private ActionResult DoLoaExcelImportForHttpPostedFile(HttpPostedFileBase httpPostedFileBase)
        {
            return DoLoaExcelImportForFileStream(httpPostedFileBase.InputStream, httpPostedFileBase.FileName);
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

        public const int FbmsExcelFileHeaderRowOffset = 2;

        [FirmaAdminFeature]
        private ActionResult DoLoaExcelImportForFileStream(Stream excelFileAsStream, string optionalOriginalFilename)
        {
            DateTime startTime = DateTime.Now;

            List<LoaStageImport> loaStages;
            try
            {
                loaStages = LoaStageImportsHelper.LoadLoaStagesFromXlsFile(excelFileAsStream, FbmsExcelFileHeaderRowOffset);
            }
            catch (Exception ex)
            {
                return Common_LoadFromXls_ExceptionHandler(excelFileAsStream, optionalOriginalFilename, ex);
            }

            //LoadFbmsRecordsFromExcelFileObjectsIntoPairedStagingTables(budgetStageImports, out var countAddedBudgets, this.CurrentFirmaSession);
            //DateTime endTime = DateTime.Now;
            //var elapsedTime = endTime - startTime;
            //string importTimeString = GetTaskTimeString("Import", elapsedTime);

            //// Record that we uploaded
            //var newImpProcessingForFbms = new ImpProcessing(ImpProcessingTableType.FBMS);
            //newImpProcessingForFbms.UploadDate = endTime;
            //newImpProcessingForFbms.UploadPerson = this.CurrentFirmaSession.Person;
            //HttpRequestStorage.DatabaseEntities.ImpProcessings.Add(newImpProcessingForFbms);
            //HttpRequestStorage.DatabaseEntities.SaveChanges(this.CurrentFirmaSession);

            //SetMessageForDisplay($"{countAddedBudgets.ToGroupedNumeric()} FBMS records were successfully imported to database. </br>{importTimeString}.");
            // This is the right thing to return, since this starts off in a modal dialog
            return new ModalDialogFormJsonResult();
        }
    }
}