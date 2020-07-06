using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProjectCodeImportHangfireBackgroundJob : SocrataDataMartUpdateBackgroundJob
    {
        private static readonly Uri ProjectCodeJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.ProjectCodeJsonSocrataBaseUrl);

        public static ProjectCodeImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static ProjectCodeImportHangfireBackgroundJob()
        {
            Instance = new ProjectCodeImportHangfireBackgroundJob();
        }

        public ProjectCodeImportHangfireBackgroundJob() : base("Project Code Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void ProjectCodeImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ProjectCodeImportJson");
            string vendorImportProc = "dbo.pProjectCodeImportJson";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ProjectCodeImportJson");
        }

        public void DownloadSocrataProjectCodeTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataProjectCodeTable");
            ClearOutdatedSocrataDataMartRawJsonImportsTableEntries();

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate();

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableType(SocrataDataMartRawJsonImportTableType.ProjectCode.SocrataDataMartRawJsonImportTableTypeID, null);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo != null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"DownloadSocrataProjectCodeTable - ProjectCode table already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddSocrataMaxLimitTagToUrl(ProjectCodeJsonSocrataBaseUrl);
            Logger.Info($"Retrieving ProjectCode JSON from URL: {fullUrl}");
            var projectCodeJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.ProjectCode);
            Logger.Info($"ProjectCode JSON length: {projectCodeJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.ProjectCode, lastFinanceApiLoadDate, null, projectCodeJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");

            try
            {
                // Use the JSON to refresh the Project Code table
                ProjectCodeImportJson(socrataDataMartRawJsonImportID);
            }
            catch (Exception e)
            {
                // Mark as failed in table
                MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingFailed);

                // add more debugging information to the exception and re-throw
                var exceptionWithMoreInfo = new ApplicationException($"ProjectCodeImportJson failed for SocrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID}", e);
                throw exceptionWithMoreInfo;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);

            Logger.Info($"Ending '{JobName}' DownloadSocrataProjectCodeTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadSocrataProjectCodeTable();
        }
    }
}