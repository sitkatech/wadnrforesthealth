using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProjectCodeImportHangfireBackgroundJob : ArcOnlineFinanceApiUpdateBackgroundJob
    {
        private static readonly Uri ProjectCodeJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.ProjectCodeJsonApiBaseUrl);

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

        private void ProjectCodeImportJson(int arcOnlineFinanceApiRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ProjectCodeImportJson");
            string vendorImportProc = "dbo.pArcOnlineProjectCodeImportJson";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArcOnlineFinanceApiRawJsonImportID", arcOnlineFinanceApiRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ProjectCodeImportJson");
        }

        
        public void DownloadArcOnlineFinanceApiProjectCodeTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadArcOnlineFinanceApiProjectCodeTable");
            ClearOutdatedArcOnlineFinanceApiRawJsonImportsTableEntries();

            var arcUtility = new ArcGisOnlineUtility();
            var token = arcUtility.GetDataImportAuthTokenFromUser();
            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate(token);

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi(ArcOnlineFinanceApiRawJsonImportTableType.ProjectCode.ArcOnlineFinanceApiRawJsonImportTableTypeID, null);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo != null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"DownloadArcOnlineFinanceApiProjectCodeTable - ProjectCode table already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }


            // Pull JSON off the page into a (possibly huge) string
            var outFields = "PROJECT_END_DATE,CREATE_DATE,PROJECT_CODE,TITLE,PROJECT_START_DATE";
            var orderByFields = "";
            var whereClause = "1=1";
            var projectCodeJson = DownloadArcOnlineUrlToString(ProjectCodeJsonSocrataBaseUrl, token, whereClause, outFields, orderByFields, ArcOnlineFinanceApiRawJsonImportTableType.ProjectCode);
            Logger.Info($"ProjectCode JSON length: {projectCodeJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var arcOnlineFinanceApiRawJsonImportID = ShoveRawJsonStringIntoTable(ArcOnlineFinanceApiRawJsonImportTableType.ProjectCode, lastFinanceApiLoadDate, null, projectCodeJson);
            Logger.Info($"New ArcOnlineFinanceApiRawJsonImportID: {arcOnlineFinanceApiRawJsonImportID}");

            try
            {
                // Use the JSON to refresh the Project Code table
                ProjectCodeImportJson(arcOnlineFinanceApiRawJsonImportID);
            }
            catch (Exception e)
            {
                // Mark as failed in table
                MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingFailed);

                // add more debugging information to the exception and re-throw
                var exceptionWithMoreInfo = new ApplicationException($"ProjectCodeImportJson failed for ArcOnlineFinanceApiRawJsonImportID {arcOnlineFinanceApiRawJsonImportID}", e);
                throw exceptionWithMoreInfo;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);

            Logger.Info($"Ending '{JobName}' DownloadArcOnlineFinanceApiProjectCodeTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadArcOnlineFinanceApiProjectCodeTable();
        }
    }
}