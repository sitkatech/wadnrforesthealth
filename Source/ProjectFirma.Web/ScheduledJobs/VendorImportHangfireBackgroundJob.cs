using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class VendorImportHangfireBackgroundJob : ArcOnlineFinanceApiUpdateBackgroundJob
    {
        private static readonly Uri VendorJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.VendorJsonSocrataBaseUrl);

        public static VendorImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static VendorImportHangfireBackgroundJob()
        {
            Instance = new VendorImportHangfireBackgroundJob();
        }

        public VendorImportHangfireBackgroundJob() : base("Vendor Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void VendorImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ArcOnlineVendorImportJson");
            string vendorImportProc = "dbo.pArcOnlineVendorImportJson";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArcOnlineFinanceApiRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ArcOnlineVendorImportJson");
        }

        public void DownloadArcOnlineVendorTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadArcOnlineVendorTable");
            ClearOutdatedSocrataDataMartRawJsonImportsTableEntries();

            var arcUtility = new ArcGisOnlineUtility();
            var token = arcUtility.GetDataImportAuthTokenFromUser();

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate(token);

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi(ArcOnlineFinanceApiRawJsonImportTableType.Vendor.ArcOnlineFinanceApiRawJsonImportTableTypeID, null);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo != null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"DownloadArcOnlineVendorTable - Vendor table already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }

            // Pull JSON off the page into a (possibly huge) string
            //var fullUrl = AddSocrataMaxLimitTagToUrl(VendorJsonSocrataBaseUrl);
            var outFields = "REMARKS,LAST_PROCESS_DATE,VENDOR_NUMBER,VENDOR_NUMBER_SUFFIX,VENDOR_NAME,ADDRESS_LINE1,ADDRESS_LINE2,ADDRESS_LINE3,CITY,STATE,ZIP_CODE,ZIP_PLUS_4,PHONE_NUMBER,VENDOR_STATUS,VENDOR_TYPE,BILLING_AGENCY,BILLING_SUBAGENCY,BILLING_FUND,BILLING_FUND_BREAKOUT,CCD_CTX_FLAG,EMAIL";
            var orderByFields = "";
            var whereClause = "VENDOR_STATUS='A'";
            var vendorJson = DownloadArcOnlineUrlToString(VendorJsonSocrataBaseUrl, token, whereClause, outFields, orderByFields, SocrataDataMartRawJsonImportTableType.Vendor);
            Logger.Info($"Vendor JSON length: {vendorJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var arcOnlineFinanceApiRawJsonImportID = ShoveRawJsonStringIntoTable(ArcOnlineFinanceApiRawJsonImportTableType.Vendor, lastFinanceApiLoadDate, null, vendorJson);
            Logger.Info($"New ArcOnlineFinanceApiRawJsonImportID: {arcOnlineFinanceApiRawJsonImportID}");
            
            try
            {
                // Use the JSON to refresh the Project Code table
                VendorImportJson(arcOnlineFinanceApiRawJsonImportID);
            }
            catch (Exception e)
            {
                // Mark as failed in table
                MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingFailed);

                // add more debugging information to the exception and re-throw
                var exceptionWithMoreInfo = new ApplicationException($"ArcOnlineVendorImportJson failed for ArcOnlineFinanceApiRawJsonImportID {arcOnlineFinanceApiRawJsonImportID}", e);
                throw exceptionWithMoreInfo;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);


            Logger.Info($"Ending '{JobName}' DownloadArcOnlineVendorTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadArcOnlineVendorTable();
        }
    }
}