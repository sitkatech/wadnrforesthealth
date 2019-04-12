using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class SocrataDataMartUpdateBackgroundJob : ScheduledBackgroundJobBase
    {
        private static string VendorJsonSocrataUrl = "https://data.wa.gov/resource/3j9d-77sr.json?$limit=9999999";
        private const int SqlCommandTimeoutInSeconds = 600;

        public SocrataDataMartUpdateBackgroundJob(string jobName) : base()
        {
        }

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            ProcessRemindersImpl();
        }


        protected virtual void ProcessRemindersImpl()
        {
            Logger.Info($"Starting '{JobName}' Socrata Data Mart updates");

            //// we're "tenant-agnostic" right now
            //var projectUpdateConfigurations = DbContext.ProjectUpdateConfigurations.ToList();
            //var reminderSubject = "Time to update your Projects";

            //    DbContext.Notifications.AddRange(notifications);
            //    DbContext.SaveChanges();
        }

        public void DownloadSocrataVendorTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataVendorTable");

            // Pull JSON off the page into a (possibly huge) string
            string vendorTempJson = DownloadSocrataUrlToString(VendorJsonSocrataUrl, SocrataDataMartRawJsonImportTableType.Vendor);
            Logger.Info($"Vendor JSON length: {vendorTempJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID  = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.Vendor, vendorTempJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            // Use the JSON to refresh the Vendor table
            VendorImportJson(socrataDataMartRawJsonImportID);

            Logger.Info($"Ending '{JobName}' DownloadSocrataVendorTable");
        }

        /// <summary>
        /// Download the contents of the given URL to a temp file
        /// </summary>
        /// <param name="urlToDownload"></param>
        /// <returns>Full path of the temp file created that contains the contents of the URL</returns>
        private string DownloadSocrataUrlToString(string urlToDownload, SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType)
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataUrlToString");
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    // This isn't needed with public APIs, but may help to prevent throttling, and let's the other side know who we are in a polite way.
                    // See: http://xxxxx
                    webClient.Headers.Add("X-App-Token", MultiTenantHelpers.GetSocrataAppToken());
                    string socrataJsonData = webClient.DownloadString(urlToDownload);

                    Logger.Info($"Ending '{JobName}' DownloadSocrataUrlToString");
                    return socrataJsonData;
                }
            }
            catch (Exception exception)
            {
                string errorMessage = $"Error downloading Socrata type {socrataDataMartRawJsonImportTableType.SocrataDataMartRawJsonImportTableTypeName}, URL {urlToDownload}: {exception.Message}";
                Logger.Error(errorMessage);
                throw;
            }
        }

        private int ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType, string rawJsonString)
        {
            Logger.Info($"Starting '{JobName}' ShoveRawJsonStringIntoTable");
            SocrataDataMartRawJsonImport newRawJsonImport = new SocrataDataMartRawJsonImport(DateTime.Now, socrataDataMartRawJsonImportTableType, rawJsonString);

            HttpRequestStorage.DatabaseEntities.SocrataDataMartRawJsonImports.Add(newRawJsonImport);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            // Normally we might return the object here, but this thing is potentially so huge we want to dump it just as soon as we no longer need it.
            Logger.Info($"Ending '{JobName}' ShoveRawJsonStringIntoTable");
            return newRawJsonImport.SocrataDataMartRawJsonImportID;
        }

        private void VendorImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' VendorImportJson");
            string vendorImportProc = "pVendorImportJson";
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

        /// <summary>
        /// Execute a block of ad-hoc SQL.
        /// Connection only lives the life of this call, and is opened and closed for it.
        /// </summary>
        /// <param name="sqlStatements"></param>
        /// <returns></returns>
        protected string ExecRawAdHocSql(string sqlStatements)
        {
            string result;
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                result = ExecuteRawArbitrarySql(sqlStatements, sqlConnection);
                sqlConnection.Close();
            }

            return result;
        }

        /// <summary>
        /// Execute a raw, arbitrary block of SQL
        /// </summary>
        /// <param name="sqlStatements">Raw SQL to execute</param>
        /// <param name="connection">SqlConnection. Must already be open.</param>
        /// <returns></returns>
        private string ExecuteRawArbitrarySql(string sqlStatements, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand(sqlStatements, connection))
            {
                var results = cmd.ExecuteNonQuery().ToString();
                return results;
            }
        }

        protected SqlConnection CreateAndOpenSqlConnection()
        {
            var db = new UnitTestCommon.ProjectFirmaSqlDatabase();
            var sqlConnection = db.CreateConnection();
            sqlConnection.Open();
            return sqlConnection;
        }
    }



}
