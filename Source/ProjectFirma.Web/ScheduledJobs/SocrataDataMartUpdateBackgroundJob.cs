using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GdalOgr;
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
            // Pull JSON off the page into a local file
            string vendorTempJson = DownloadSocrataUrlToString(VendorJsonSocrataUrl);
            // Push that local file into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID  = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.Vendor, vendorTempJson);
            // Use the JSON to refresh the Vendor table
            VendorImportJson(socrataDataMartRawJsonImportID);
        }

        /// <summary>
        /// Download the contents of the given URL to a temp file
        /// </summary>
        /// <param name="urlToDownload"></param>
        /// <returns>Full path of the temp file created that contains the contents of the URL</returns>
        private string DownloadSocrataUrlToString(string urlToDownload)
        {
            using (WebClient webClient = new WebClient())
            {
                // This isn't needed with public APIs, but may help to prevent throttling, and let's the other side know who we are in a polite way.
                // See: http://xxxxx
                webClient.Headers.Add("X-App-Token", MultiTenantHelpers.GetSocrataAppToken());
                return webClient.DownloadString(urlToDownload);
            }
        }

        private int ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType, string rawJsonString)
        {
            SocrataDataMartRawJsonImport newRawJsonImport = new SocrataDataMartRawJsonImport(DateTime.Now, socrataDataMartRawJsonImportTableType, rawJsonString);

            HttpRequestStorage.DatabaseEntities.SocrataDataMartRawJsonImports.Add(newRawJsonImport);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            // Normally we might return the object here, but this thing is potentially so huge we want to dump it just as soon as we no longer need it.
            return newRawJsonImport.SocrataDataMartRawJsonImportID;
        }

        // exec pVendorImportJson @SocrataDataMartRawJsonImportID = 2
        


        private void VendorImportJson(int socrataDataMartRawJsonImportID)
        {
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
