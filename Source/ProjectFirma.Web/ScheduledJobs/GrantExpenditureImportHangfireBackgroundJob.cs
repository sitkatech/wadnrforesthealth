using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class GrantExpenditureImportHangfireBackgroundJob : SocrataDataMartUpdateBackgroundJob
    {
        private static readonly Uri GrantExpendituresJsonApiBaseUrl = new Uri(FirmaWebConfiguration.GrantExpendituresTempBaseUrl);

        public static GrantExpenditureImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static GrantExpenditureImportHangfireBackgroundJob()
        {
            Instance = new GrantExpenditureImportHangfireBackgroundJob();
        }

        public GrantExpenditureImportHangfireBackgroundJob() : base("Grant Expenditure Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void ClearGrantAllocationExpenditureTables()
        {
            Logger.Info($"Starting '{JobName}' ClearGrantAllocationExpenditureTables");
            string vendorImportProc = "pClearGrantAllocationExpenditureTables";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ClearGrantAllocationExpenditureTables");
        }

        public void DownloadGrantExpendituresTableForAllFiscalYears()
        {
            Logger.Info($"Starting '{JobName}' DownloadGrantExpendituresTableForAllFiscalYears");

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate();

            // 2001 is the first biennium for which API returned data. This is WAY farther back than needed, but
            // harmless to overreach, since we only use data for which we have matching PI/PC.
            const int beginBienniumFiscalYear = 2001;

            const int bienniumStep = 2;

            // Go at least one biennium beyond the current one
            var endBienniumFiscalYear = CurrentBiennium.GetCurrentBienniumFiscalYear() + bienniumStep;

            // Always clear the expenditure data before doing the import, at least for now
            ClearGrantAllocationExpenditureTables();

            // Step through all the desired Bienniums
            for (var bienniumFiscalYear = beginBienniumFiscalYear; bienniumFiscalYear <= endBienniumFiscalYear; bienniumFiscalYear += bienniumStep)
            {
                // Since only 2007 current blows up, we want to process all the other available years in the meantime.
                // This sends us and email reminding us something is wrong, and there is also good data about success in the tables itself.
                // -- SLG 6/27/2019 -- https://projects.sitkatech.com/projects/wa_dnr_forest_health_tracker/cards/1635
                try
                {
                    ImportExpendituresForGivenBienniumFiscalYear(bienniumFiscalYear, lastFinanceApiLoadDate);
                }
                catch (Exception e)
                {
                    Logger.Error($"Error importing Expenditures for Biennium Fiscal Year {bienniumFiscalYear}: {e.Message}");
                }
                
            }

            Logger.Info($"Ending '{JobName}' DownloadGrantExpendituresTableForAllFiscalYears");
        }

        private void GrantExpenditureImportJson(int socrataDataMartRawJsonImportID, int bienniumToImport)
        {
            Logger.Info($"Starting '{JobName}' GrantExpenditureImportJson");
            string vendorImportProc = "dbo.pGrantExpenditureImportJson";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.Parameters.AddWithValue("@BienniumToImport", bienniumToImport);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' GrantExpenditureImportJson");
        }

        private void ImportExpendituresForGivenBienniumFiscalYear(int bienniumFiscalYear, DateTime lastFinanceApiLoadDate)
        {
            Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium Fiscal Year {bienniumFiscalYear}");

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableType(SocrataDataMartRawJsonImportTableType.GrantExpenditure.SocrataDataMartRawJsonImportTableTypeID, bienniumFiscalYear);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo!= null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium {bienniumFiscalYear} already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }

            var fullUrl = GetGrantExpendituresJsonApiUrlWithAllParameters(bienniumFiscalYear);
            // Pull JSON off the page into a (possibly huge) string
            Logger.Info($"Attempting to retrieve Expenditures for Biennium Fiscal Year {bienniumFiscalYear} from URL {fullUrl}...");
            string grantExpenditureJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.GrantExpenditure);

            Logger.Info($"GrantExpenditure BienniumFiscalYear {bienniumFiscalYear} JSON length: {grantExpenditureJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.GrantExpenditure, lastFinanceApiLoadDate, bienniumFiscalYear, grantExpenditureJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");

            try
            {
                // Import the given Biennium
                GrantExpenditureImportJson(socrataDataMartRawJsonImportID, bienniumFiscalYear);
            }
            catch (Exception e)
            {
                // Log the error
                Logger.Error($"ImportExpendituresForGivenBienniumFiscalYear failed for SocrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID}: {e.Message}");
                // Mark as failed in table
                MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingFailed);
                throw;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);
        }

        /// <summary>
        /// Get the fully qualified URL for JSON GrantExpenditures
        /// </summary>
        /// <param name="biennium">Biennium is required</param>
        /// <returns></returns>
        public static Uri GetGrantExpendituresJsonApiUrlWithAllParameters(int biennium)
        {
            var builder = new UriBuilder(GrantExpendituresJsonApiBaseUrl);
            //builder.Query += $"/{biennium}";
            builder.Query += $"q={biennium}";
            return builder.Uri;
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadGrantExpendituresTableForAllFiscalYears();
        }




    }
}