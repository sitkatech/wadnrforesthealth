using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using ApprovalTests;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using ProjectFirma.Web.Common;

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
            string vendorJsonTempFilePath = DownloadUrlToFile(VendorJsonSocrataUrl);
            ParseVendorJsonTableIntoTempTable(vendorJsonTempFilePath);
        }

        private string DownloadUrlToFile(string urlToDownload)
        {
            string tempFilename;
            using (WebClient webClient = new WebClient())
            {
                tempFilename = System.IO.Path.GetTempFileName();
                // This isn't needed with public APIs, but may help to prevent throttling, and let's the other side know who we are in a polite way.
                // See: 
                webClient.Headers.Add("X-App-Token", MultiTenantHelpers.GetSocrataAppToken());
                webClient.DownloadFile(urlToDownload, tempFilename);
            }

            return tempFilename;
        }

        private void ParseVendorJsonTableIntoTempTable(string pathToVendorJsonFile)
        {
            string vendorImportSql = @"
                                    if object_id('tempdb.dbo.#vendorSocrataTemp') is not null 
                                    begin 
                                        drop table #vendorSocrataTemp
                                    end

                                    SELECT vendorTemp.*
                                    into #vendorSocrataTemp
                                    FROM OPENROWSET (BULK '{pathToVendorJsonFile}', SINGLE_CLOB) as j
                                    CROSS APPLY OPENJSON(BulkColumn)
                                    WITH
                                    (
                                        vendor_num nvarchar(256),
                                        vendor_num_suffix nvarchar(256),
                                        vendor_name nvarchar(256),
                                        addr1 nvarchar(256),
                                        addr2 nvarchar(256),
                                        addr3 nvarchar(256),
                                        city nvarchar(256),
                                        state nvarchar(256),
                                        zip_code nvarchar(256),
                                        zip_plus_4 nvarchar(256),
                                        phone varchar(200),
                                        vendor_status nvarchar(256),
                                        vendor_type nvarchar(256),
                                        billing_agency nvarchar(256),
                                        billing_subagency nvarchar(256),
                                        billing_fund nvarchar(256),
                                        billing_fund_breakout nvarchar(256),
                                        ccd_ctx_flag nvarchar(256),
                                        taxpayer_id_num varchar(200),
                                        email nvarchar(256),
                                        remarks nvarchar(256),
                                        last_process_date DateTime
                                    )
                                    AS vendorTemp";
            vendorImportSql = vendorImportSql.Replace("{pathToVendorJsonFile}", pathToVendorJsonFile);
            var result = ExecAdHocSql(vendorImportSql);
            //Approvals.Verify(result.TableToHumanReadableString());
        }

        /// <summary>
        /// Execute a block of ad-hoc SQL.
        /// Connection only lives the life of this call, and is opened and closed for it.
        /// </summary>
        /// <param name="sqlStatements"></param>
        /// <returns></returns>
        protected DataTable ExecAdHocSql(string sqlStatements)
        {
            SqlConnection sqlConnection = CreateSqlConnection();
            DataTable result = ExecuteSql(sqlStatements, sqlConnection);
            sqlConnection.Close();
            return result;
        }

        private static DataTable ExecuteSql(string sqlStatements, SqlConnection connection)
        {
            using (var comm = new SqlCommand(sqlStatements, connection))
            {
                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = SqlCommandTimeoutInSeconds;
                var sqlResult = TempDbSqlDatabase.ExecuteSqlCommand(comm);
                if (sqlResult.Tables.Count > 0)
                {
                    return sqlResult.Tables[0];
                }
                return null;
            }
        }

        protected SqlConnection CreateSqlConnection()
        {
            var db = new TempDbSqlDatabase();
            var sqlConnection = db.CreateConnection();
            sqlConnection.Open();
            return sqlConnection;
        }
    }



}
