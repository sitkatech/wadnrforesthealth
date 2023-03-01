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
            Logger.Info($"Starting '{JobName}' VendorImportJson");
            string vendorImportProc = "dbo.pVendorImportJson";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' VendorImportJson");
        }

        public void DownloadSocrataVendorTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataVendorTable");
            ClearOutdatedSocrataDataMartRawJsonImportsTableEntries();

            var arcUtility = new ArcGisOnlineUtility();
            var token = arcUtility.GetDataImportAuthTokenFromUser();

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate(token);

            var importInfo = LatestSuccessfulJsonImportInfoForBienniumAndImportTableType(ArcOnlineFinanceApiRawJsonImportTableType.Vendor.ArcOnlineFinanceApiRawJsonImportTableTypeID, null);

            // If we've already successfully imported the latest data available for this fiscal year, skip doing it again.
            if (importInfo != null && importInfo.FinanceApiLastLoadDate == lastFinanceApiLoadDate)
            {
                Logger.Info($"DownloadSocrataVendorTable - Vendor table already current. Last import: {importInfo.JsonImportDate} - LastFinanceApiLoadDate: {lastFinanceApiLoadDate}");
                return;
            }

            // Pull JSON off the page into a (possibly huge) string
            //var fullUrl = AddSocrataMaxLimitTagToUrl(VendorJsonSocrataBaseUrl);
            var outFields = "REMARKS,LAST_PROCESS_DATE,VENDOR_NUMBER,VENDOR_NUMBER_SUFFIX,VENDOR_NAME,ADDRESS_LINE1,ADDRESS_LINE2,ADDRESS_LINE3,CITY,STATE,ZIP_CODE,ZIP_PLUS_4,PHONE_NUMBER,VENDOR_STATUS,VENDOR_TYPE,BILLING_AGENCY,BILLING_SUBAGENCY,BILLING_FUND,BILLING_FUND_BREAKOUT,CCD_CTX_FLAG,EMAIL";
            var orderByFields = "";
            var whereClause = "1=1";
            var vendorJson = DownloadArcOnlineUrlToString(VendorJsonSocrataBaseUrl, token, whereClause, outFields, orderByFields, SocrataDataMartRawJsonImportTableType.Vendor);
            Logger.Info($"Vendor JSON length: {vendorJson.Count}");
            // Push that string into a raw JSON string in the raw staging table
            var socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(ArcOnlineFinanceApiRawJsonImportTableType.Vendor, lastFinanceApiLoadDate, null, vendorJson.FirstOrDefault());
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            
            try
            {
                // Use the JSON to refresh the Project Code table
                VendorImportJson(socrataDataMartRawJsonImportID);
            }
            catch (Exception e)
            {
                // Mark as failed in table
                MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingFailed);

                // add more debugging information to the exception and re-throw
                var exceptionWithMoreInfo = new ApplicationException($"VendorImportJson failed for SocrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID}", e);
                throw exceptionWithMoreInfo;
            }
            // If we get this far, it's successfully imported, and we can mark it as such
            MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingSuceeded);


            Logger.Info($"Ending '{JobName}' DownloadSocrataVendorTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadSocrataVendorTable();
        }
    }
}