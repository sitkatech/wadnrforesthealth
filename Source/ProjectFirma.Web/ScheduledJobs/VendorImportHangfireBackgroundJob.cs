using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class VendorImportHangfireBackgroundJob : SocrataDataMartUpdateBackgroundJob
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
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
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

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate();

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddSocrataMaxLimitTagToUrl(VendorJsonSocrataBaseUrl);
            var vendorJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.Vendor);
            Logger.Info($"Vendor JSON length: {vendorJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.Vendor, lastFinanceApiLoadDate, null, vendorJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            
            try
            {
                // Use the JSON to refresh the Project Code table
                VendorImportJson(socrataDataMartRawJsonImportID);
            }
            catch (Exception e)
            {
                // Log the error
                Logger.Error($"VendorImportJson failed for SocrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID}: {e.Message}");
                // Mark as failed in table
                MarkJsonImportStatus(socrataDataMartRawJsonImportID, JsonImportStatusType.ProcessingFailed);
                throw;
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