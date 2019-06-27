using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProgramIndexImportHangfireBackgroundJob : SocrataDataMartUpdateBackgroundJob
    {
        private static readonly Uri ProgramIndexJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.ProgramIndexJsonSocrataBaseUrl);

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

        private void ProgramIndexImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ProgramIndexImportJson");
            string vendorImportProc = "dbo.pProgramIndexImportJson";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ProgramIndexImportJson");
        }

        public void DownloadSocrataProgramIndexTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataProgramIndexTable");

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate();

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddSocrataMaxLimitTagToUrl(ProgramIndexJsonSocrataBaseUrl);
            string programIndexJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.ProgramIndex);
            Logger.Info($"Program Index JSON length: {programIndexJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.ProgramIndex, lastFinanceApiLoadDate, null, programIndexJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");

            try
            {
                // Use the JSON to refresh the Vendor table
                ProgramIndexImportJson(socrataDataMartRawJsonImportID);
            }
            catch (Exception e)
            {
                // Log the error
                Logger.Error($"ProgramIndexImportJson failed for SocrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID}: {e.Message}");
                // Mark as failed in table
                MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingFailed);
                throw;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);

            Logger.Info($"Ending '{JobName}' DownloadSocrataProgramIndexTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadSocrataProgramIndexTable();
        }
    }
}