using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    // RunGrantExpendituresImportJob

    public class SocrataDataMartUpdateBackgroundJob : ScheduledBackgroundJobBase
    {
        private static string VendorJsonSocrataBaseUrl = "https://data.wa.gov/resource/3j9d-77sr.json";
        private static string ProgramIndexJsonSocrataBaseUrl = "https://data.wa.gov/resource/quhu-28uh.json";
        private static string ProjectCodeJsonSocrataBaseUrl = "https://data.wa.gov/resource/6grp-8ghq.json";

        /// <summary>
        /// WARNING: This URL may turn out only to be temporary, or vary for Production. Hard coded for now, but
        /// may well need to vary by environment.
        ///
        /// By Biennium: https://test-fortress.wa.gov/dnr/admindev/FinanceAPI/ApiProxy/?a=get&q=GrantExpenditures/2019 
        /// By Fiscal month: https://test-fortress.wa.gov/dnr/admindev/FinanceAPI/ApiProxy/?a=get&q=GrantExpenditures/2019/22
        ///
        /// </summary>
        private static string GrantExpendituresTempBaseUrl = "https://test-fortress.wa.gov/dnr/admindev/FinanceAPI/ApiProxy/?a=get&q=GrantExpenditures/";

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

        private string AddMaxLimitTagToUrl(string baseSocrataJsonApiUrl)
        {
            return $"{baseSocrataJsonApiUrl}?$limit=9999999";
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
            var fullUrl = AddMaxLimitTagToUrl(VendorJsonSocrataBaseUrl);
            string vendorTempJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.Vendor);
            Logger.Info($"Vendor JSON length: {vendorTempJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID  = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.Vendor, vendorTempJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            // Use the JSON to refresh the Vendor table
            VendorImportJson(socrataDataMartRawJsonImportID);

            Logger.Info($"Ending '{JobName}' DownloadSocrataVendorTable");
        }

        public void DownloadSocrataProgramIndexTable()
        {
           Logger.Info($"Starting '{JobName}' DownloadSocrataProgramIndexTable");

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddMaxLimitTagToUrl(ProgramIndexJsonSocrataBaseUrl);
            string programIndexJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.ProgramIndex);
            Logger.Info($"ProgramIndex JSON length: {programIndexJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.ProgramIndex, programIndexJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            // Use the JSON to refresh the Vendor table
            ProgramIndexImportJson(socrataDataMartRawJsonImportID);

            Logger.Info($"Ending '{JobName}' DownloadSocrataProgramIndexTable");
        }

        public void DownloadSocrataProjectCodeTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataProjectCodeTable");

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddMaxLimitTagToUrl(ProjectCodeJsonSocrataBaseUrl);
            string projectCodeJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.ProjectCode);
            Logger.Info($"ProjectCode JSON length: {projectCodeJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.ProjectCode, projectCodeJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            // Use the JSON to refresh the Project Code table
            ProjectCodeImportJson(socrataDataMartRawJsonImportID);

            Logger.Info($"Ending '{JobName}' DownloadSocrataProjectCodeTable");
        }

        // RunGrantExpendituresImportJob

        public void DownloadGrantExpendituresTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadGrantExpendituresTable");

            // This is just a guess. Deserves vetting with WADNR staff.
            const int startingBienniumFiscalYear = 2009;

            int currentBienniumFiscalYear = CurrentBiennium.GetCurrentBienniumFiscalYear();

            // Always clear the data before doing the import, at least for now
            ClearGrantExpenditureTable();

            // Step through all the desired Bienniums
            for (int bienniumFiscalYear = startingBienniumFiscalYear; bienniumFiscalYear < currentBienniumFiscalYear; bienniumFiscalYear += 2)
            {
                ImportExpendituresForGivenBienniumFiscalYear(bienniumFiscalYear);
            }

            Logger.Info($"Ending '{JobName}' DownloadGrantExpendituresTable");
        }

        private void ImportExpendituresForGivenBienniumFiscalYear(int bienniumFiscalYear)
        {
            Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium Fiscal Year {bienniumFiscalYear}");

            var fullUrl = GetGrantExpendituresTempUrlWithAllParameters(bienniumFiscalYear);
            // Pull JSON off the page into a (possibly huge) string
            string grantExpenditureJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.GrantExpenditure);
            // The JSON coming off this particular function is wonky and pre-escaped. I may suggest Tammy fix it, but for the moment we'll work with it, and 
            // clean it up ourselves.
            grantExpenditureJson = grantExpenditureJson.Remove(grantExpenditureJson.IndexOf('"'), 1);
            grantExpenditureJson = grantExpenditureJson.Remove(grantExpenditureJson.LastIndexOf('"'), 1);
            // Optional? Needed? 
            grantExpenditureJson = Regex.Unescape(grantExpenditureJson);
            Logger.Info($"GrantExpenditure BienniumFiscalYear {bienniumFiscalYear} JSON length: {grantExpenditureJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.GrantExpenditure, grantExpenditureJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");

            // Import the given Biennium
            GrantExpenditureImportJson(socrataDataMartRawJsonImportID, bienniumFiscalYear);
        }

        private void ClearGrantExpenditureTable()
        {
            // Should delete all GrantAllocationExpenditures
            foreach (var gae in HttpRequestStorage.DatabaseEntities.GrantAllocationExpenditures)
            {
                gae.Delete(HttpRequestStorage.DatabaseEntities);
            }
        }

        /// <summary>
        /// Get the fully qualified URL for JSON GrantExpenditures
        /// </summary>
        /// <param name="biennium">Biennium is required</param>
        /// <param name="fiscalMonth">Fiscal Month is optional</param>
        /// <returns></returns>
        private string GetGrantExpendituresTempUrlWithAllParameters(int biennium, int? fiscalMonth = null)
        {
            // No fiscal month supplied
            if (fiscalMonth == null)
            {
                return AddMaxLimitTagToUrl($"{GrantExpendituresTempBaseUrl}{biennium.ToString()}");
            }
            // Fiscal month supplied
            return AddMaxLimitTagToUrl($"{GrantExpendituresTempBaseUrl}{biennium.ToString()}/{fiscalMonth.ToString()}");
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

        private void ProgramIndexImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ProgramIndexImportJson");
            string vendorImportProc = "pProgramIndexImportJson";
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

        private void ProjectCodeImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ProjectCodeImportJson");
            string vendorImportProc = "pProjectCodeImportJson";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ProjectCodeImportJson");
        }

        private void GrantExpenditureImportJson(int socrataDataMartRawJsonImportID, int bienniumToImport)
        {
            Logger.Info($"Starting '{JobName}' GrantExpenditureImportJson");
            string vendorImportProc = "pGrantExpenditureImportJson";
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
