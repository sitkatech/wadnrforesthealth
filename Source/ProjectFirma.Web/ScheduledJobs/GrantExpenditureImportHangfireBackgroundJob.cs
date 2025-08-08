using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class FundSourceExpenditureImportHangfireBackgroundJob : ArcOnlineFinanceApiUpdateBackgroundJob
    {
        private static readonly Uri FundSourceExpendituresJsonApiBaseUrl = new Uri(FirmaWebConfiguration.FundSourceExpendituresJsonApiBaseUrl);

        public static FundSourceExpenditureImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static FundSourceExpenditureImportHangfireBackgroundJob()
        {
            Instance = new FundSourceExpenditureImportHangfireBackgroundJob();
        }

        public FundSourceExpenditureImportHangfireBackgroundJob() : base("FundSource Expenditure Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void ClearFundSourceAllocationExpenditureTables(int bienniumFiscalYear)
        {
            Logger.Info($"Starting '{JobName}' ClearFundSourceAllocationExpenditureTables");
            string vendorImportProc = "pClearFundSourceAllocationExpenditureTables";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bienniumFiscalYear", bienniumFiscalYear);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ClearFundSourceAllocationExpenditureTables");
        }


        public void DownloadFundSourceExpendituresTableForAllFiscalYears()
        {
            Logger.Info($"Starting '{JobName}' DownloadFundSourceExpendituresTableForAllFiscalYears");
            ClearOutdatedArcOnlineFinanceApiRawJsonImportsTableEntries();

            var arcUtility = new ArcGisOnlineUtility();
            var token = arcUtility.GetDataImportAuthTokenFromUser();
            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate(token);

            // 2001 is the first biennium for which API returned data. This is WAY farther back than needed, but
            // harmless to overreach, since we only use data for which we have matching PI/PC.
            const int beginBienniumFiscalYear = 2001;

            const int bienniumStep = 2;

            // Go at least one biennium beyond the current one
            var endBienniumFiscalYear = CurrentBiennium.GetCurrentBienniumFiscalYearFromDatabase() + bienniumStep;

            // Step through all the desired Bienniums
            for (var bienniumFiscalYear = beginBienniumFiscalYear; bienniumFiscalYear <= endBienniumFiscalYear; bienniumFiscalYear += bienniumStep)
            {
                // Since only 2007 current blows up, we want to process all the other available years in the meantime.
                // This sends us and email reminding us something is wrong, and there is also good data about success in the tables itself.
                // -- SLG 6/27/2019 -- https://projects.sitkatech.com/projects/wa_dnr_forest_health_tracker/cards/1635
                try
                {
                    ImportExpendituresForGivenBienniumFiscalYear(bienniumFiscalYear, lastFinanceApiLoadDate, token);
                }
                catch (Exception e)
                {
                    Logger.Error($"Error importing Expenditures for Biennium Fiscal Year {bienniumFiscalYear}: {e}");
                }
                
            }

            Logger.Info($"Ending '{JobName}' DownloadFundSourceExpendituresTableForAllFiscalYears");
        }

        private void FundSourceExpenditureImportJson(int arcOnlineFinanceApiRawJsonImportID, int bienniumToImport)
        {
            Logger.Info($"Starting '{JobName}' ArcOnlineFundSourceExpenditureImportJson");
            string vendorImportProc = "dbo.pArcOnlineFundSourceExpenditureImportJson";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArcOnlineFinanceApiRawJsonImportID", arcOnlineFinanceApiRawJsonImportID);
                    cmd.Parameters.AddWithValue("@BienniumToImport", bienniumToImport);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ArcOnlineFundSourceExpenditureImportJson");
        }

        private void ImportExpendituresForGivenBienniumFiscalYear(int bienniumFiscalYear,
            DateTime lastFinanceApiLoadDate, string token)
        {
            Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium Fiscal Year {bienniumFiscalYear}");

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi(ArcOnlineFinanceApiRawJsonImportTableType.FundSourceExpenditure.ArcOnlineFinanceApiRawJsonImportTableTypeID, bienniumFiscalYear);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo!= null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium {bienniumFiscalYear} already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }

            // Clear the expenditure data for the given Biennium before doing the import
            ClearFundSourceAllocationExpenditureTables(bienniumFiscalYear);

            var outFields = "FTE_AMOUNT,TAR_HR_AMOUNT,BIENNIUM,FISCAL_MONTH,FISCAL_ADJUSTMENT_MONTH,CALENDAR_YEAR,MONTH_NAME,SOURCE_SYSTEM,DOCUMENT_NUMBER,DOCUMENT_SUFFIX,DOCUMENT_DATE,DOCUMENT_INVOICE_NUMBER,INVOICE_DESCRIPTION,INVOICE_DATE,INVOICE_NUMBER,GL_ACCOUNT_NUMBER,OBJECT_CODE,OBJECT_NAME,SUB_OBJECT_CODE,SUB_OBJECT_NAME,SUB_SUB_OBJECT_CODE,SUB_SUB_OBJECT_NAME,APPROPRIATION_CODE,APPROPRIATION_NAME,FUND_CODE,FUND_NAME,ORG_CODE,ORG_NAME,PROGRAM_INDEX_CODE,PROGRAM_INDEX_NAME,PROGRAM_CODE,PROGRAM_NAME,SUB_PROGRAM_CODE,SUB_PROGRAM_NAME,ACTIVITY_CODE,ACTIVITY_NAME,SUB_ACTIVITY_CODE,SUB_ACTIVITY_NAME,PROJECT_CODE,PROJECT_NAME,VENDOR_NUMBER,VENDOR_NAME,EXPENDITURE_ACCURED,ENCUMBRANCE";
            var orderByFields = "";
            var whereClause = $"BIENNIUM='{bienniumFiscalYear}'";
            var fundSourceExpenditureJson = DownloadArcOnlineUrlToString(FundSourceExpendituresJsonApiBaseUrl, token, whereClause, outFields, orderByFields, ArcOnlineFinanceApiRawJsonImportTableType.ProgramIndex);
            Logger.Info($"FundSourceExpenditure BienniumFiscalYear {bienniumFiscalYear} JSON length: {fundSourceExpenditureJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var arcOnlineFinanceApiRawJsonImportID = ShoveRawJsonStringIntoTable(ArcOnlineFinanceApiRawJsonImportTableType.FundSourceExpenditure, lastFinanceApiLoadDate, bienniumFiscalYear, fundSourceExpenditureJson);
            Logger.Info($"New ArcOnlineFinanceApiRawJsonImportID: {arcOnlineFinanceApiRawJsonImportID}");



            try
            {
                // Import the given Biennium
                FundSourceExpenditureImportJson(arcOnlineFinanceApiRawJsonImportID, bienniumFiscalYear);
            }
            catch (Exception e)
            {
                // Mark as failed in table
                MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingFailed);

                // add more debugging information to the exception and re-throw
                var exceptionWithMoreInfo = new ApplicationException($"ImportExpendituresForGivenBienniumFiscalYear failed for ArcOnlineFinanceApiRawJsonImportID {arcOnlineFinanceApiRawJsonImportID}", e);
                throw exceptionWithMoreInfo;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);
        }

        /// <summary>
        /// Get the fully qualified URL for JSON FundSourceExpenditures
        /// </summary>
        /// <param name="biennium">Biennium is required</param>
        /// <returns></returns>
        public static Uri GetFundSourceExpendituresJsonApiUrlWithAllParameters(int biennium)
        {
            var builder = new UriBuilder(FundSourceExpendituresJsonApiBaseUrl);
            //builder.Query += $"/{biennium}";
            builder.Query += $"q={biennium}";
            return builder.Uri;
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadFundSourceExpendituresTableForAllFiscalYears();
        }




    }
}