using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProgramIndexImportHangfireBackgroundJob : ArcOnlineFinanceApiUpdateBackgroundJob
    {
        private static readonly Uri ProgramIndexJsonApiBaseUrl = new Uri(FirmaWebConfiguration.ProgramIndexJsonApiBaseUrl);

        public static ProgramIndexImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static ProgramIndexImportHangfireBackgroundJob()
        {
            Instance = new ProgramIndexImportHangfireBackgroundJob();
        }

        public ProgramIndexImportHangfireBackgroundJob() : base("Program Index Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void ProgramIndexImportJson(int arcOnlineFinanceApiRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ArcOnlineProgramIndexImportJson");
            string vendorImportProc = "dbo.pArcOnlineProgramIndexImportJson";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArcOnlineFinanceApiRawJsonImportID", arcOnlineFinanceApiRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ArcOnlineProgramIndexImportJson");
        }

        public void DownloadArcOnlineProgramIndexTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadArcOnlineProgramIndexTable");
            ClearOutdatedArcOnlineFinanceApiRawJsonImportsTableEntries();

            var arcUtility = new ArcGisOnlineUtility();
            var token = arcUtility.GetDataImportAuthTokenFromUser();
            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate(token);

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi(ArcOnlineFinanceApiRawJsonImportTableType.ProgramIndex.ArcOnlineFinanceApiRawJsonImportTableTypeID, null);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo != null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"DownloadArcOnlineProgramIndexTable - ProgramIndex table already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }


            // Pull JSON off the page into a (possibly huge) string
            var outFields = "ACTIVITY_CODE,SUB_ACTIVITY_CODE,BIENNIUM,PROGRAM_INDEX_CODE,TITLE,PROGRAM_CODE,SUB_PROGRAM_CODE";
            var orderByFields = "";
            var whereClause = "1=1";
            var programIndexJson = DownloadArcOnlineUrlToString(ProgramIndexJsonApiBaseUrl, token, whereClause, outFields, orderByFields, ArcOnlineFinanceApiRawJsonImportTableType.ProgramIndex);
            Logger.Info($"ProgramIndex JSON length: {programIndexJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var arcOnlineFinanceApiRawJsonImportID = ShoveRawJsonStringIntoTable(ArcOnlineFinanceApiRawJsonImportTableType.ProgramIndex, lastFinanceApiLoadDate, null, programIndexJson);
            Logger.Info($"New ArcOnlineFinanceApiRawJsonImportID: {arcOnlineFinanceApiRawJsonImportID}");


            try
            {
                // Use the JSON to refresh the Vendor table
                ProgramIndexImportJson(arcOnlineFinanceApiRawJsonImportID);
            }
            catch (Exception e)
            {
                // Mark as failed in table
                MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingFailed);

                // add more debugging information to the exception and re-throw
                var exceptionWithMoreInfo = new ApplicationException($"ProgramIndexImportJson failed for ArcOnlineFinanceApiRawJsonImportID {arcOnlineFinanceApiRawJsonImportID}", e);
                throw exceptionWithMoreInfo;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);

            Logger.Info($"Ending '{JobName}' DownloadArcOnlineProgramIndexTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadArcOnlineProgramIndexTable();
        }
    }
}