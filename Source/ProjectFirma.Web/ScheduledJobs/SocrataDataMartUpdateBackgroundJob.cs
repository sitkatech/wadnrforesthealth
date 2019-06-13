using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    // RunGrantExpendituresImportJob

    public class SocrataDataMartUpdateBackgroundJob : ScheduledBackgroundJobBase
    {
        private static readonly Uri VendorJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.VendorJsonSocrataBaseUrl);
        private static readonly Uri ProgramIndexJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.ProgramIndexJsonSocrataBaseUrl);
        private static readonly Uri GrantExpendituresJsonApiBaseUrl = new Uri(FirmaWebConfiguration.GrantExpendituresTempBaseUrl);

        /// <summary>
        /// Default constructor. Assumes job can run with others; this is a short-term assumption to get compiling.
        /// </summary>
        /// <param name="jobName"></param>
        public SocrataDataMartUpdateBackgroundJob(string jobName) : base(jobName, ConcurrencySetting.OkToRunWhileOtherJobsAreRunning)
        {
        }



        public SocrataDataMartUpdateBackgroundJob(string jobName, ConcurrencySetting concurrencySetting) : base(jobName, concurrencySetting)
        {
        }

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            throw new NotImplementedException();
        }



        protected virtual void ProcessRemindersImpl()
        {
            Logger.Info($"Starting '{JobName}' Socrata Data Mart updates");
        }

        public void DownloadSocrataVendorTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataVendorTable");

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddSocrataMaxLimitTagToUrl(VendorJsonSocrataBaseUrl);
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
            var fullUrl = AddSocrataMaxLimitTagToUrl(ProgramIndexJsonSocrataBaseUrl);
            string programIndexJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.ProgramIndex);
            Logger.Info($"ProgramIndex JSON length: {programIndexJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.ProgramIndex, programIndexJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            // Use the JSON to refresh the Vendor table
            ProgramIndexImportJson(socrataDataMartRawJsonImportID);

            Logger.Info($"Ending '{JobName}' DownloadSocrataProgramIndexTable");
        }



        public void DownloadGrantExpendituresTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadGrantExpendituresTable");

            // Starting biennium is just a guess.
            // 2001 is the first biennium for which API returned data but 2007 has bad dates in it so using 2009 for now
            //const int beginBienniumFiscalYear = 2009;

            // HACK for testing, date fixing, etc.
            const int beginBienniumFiscalYear = 2001;

            const int bienniumStep = 2;

            // Go at least one biennium beyond the current one
            var endBienniumFiscalYear = CurrentBiennium.GetCurrentBienniumFiscalYear() + bienniumStep;
            
            // Always clear the expenditure data before doing the import, at least for now
            ClearGrantAllocationExpenditureTables();

            // Step through all the desired Bienniums
            for (var bienniumFiscalYear = beginBienniumFiscalYear; bienniumFiscalYear <= endBienniumFiscalYear; bienniumFiscalYear += bienniumStep)
            {
                ImportExpendituresForGivenBienniumFiscalYear(bienniumFiscalYear);
            }

            Logger.Info($"Ending '{JobName}' DownloadGrantExpendituresTable");
        }

        private void ImportExpendituresForGivenBienniumFiscalYear(int bienniumFiscalYear)
        {
            Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium Fiscal Year {bienniumFiscalYear}");

            var fullUrl = GetGrantExpendituresJsonApiUrlWithAllParameters(bienniumFiscalYear);
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

        /// <summary>
        /// Get the fully qualified URL for JSON GrantExpenditures
        /// </summary>
        /// <param name="biennium">Biennium is required</param>
        /// <returns></returns>
        public static Uri GetGrantExpendituresJsonApiUrlWithAllParameters(int biennium)
        {
            var builder = new UriBuilder(GrantExpendituresJsonApiBaseUrl);
            builder.Query += $"/{biennium}";
            return builder.Uri;
        }



        public Uri AddSocrataMaxLimitTagToUrl(Uri baseSocrataJsonApiUrl)
        {
            var uriBuilder = new UriBuilder(baseSocrataJsonApiUrl);
            uriBuilder.Query += "$limit=9999999";
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Download the contents of the given URL to a temp file
        /// </summary>
        /// <returns>Full path of the temp file created that contains the contents of the URL</returns>
        public string DownloadSocrataUrlToString(Uri urlToDownload, SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType)
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataUrlToString");
            try
            {
                using (var webClient = new WebClient())
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

        public int ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType, string rawJsonString)
        {
            Logger.Info($"Starting '{JobName}' ShoveRawJsonStringIntoTable");
            SocrataDataMartRawJsonImport newRawJsonImport = new SocrataDataMartRawJsonImport(DateTime.Now, socrataDataMartRawJsonImportTableType, rawJsonString);

            HttpRequestStorage.DatabaseEntities.SocrataDataMartRawJsonImports.Add(newRawJsonImport);

            // We use the System Person if none is available, because that indicates we are running from an automated context (Hangfire)
            if (HttpRequestStorage.PersonIsSet())
            {
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }
            else
            {
                var systemUser = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();
                HttpRequestStorage.DatabaseEntities.SaveChanges(systemUser);
            }

            // Normally we might return the object here, but this thing is potentially so huge we want to dump it just as soon as we no longer need it.
            Logger.Info($"Ending '{JobName}' ShoveRawJsonStringIntoTable");
            return newRawJsonImport.SocrataDataMartRawJsonImportID;
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
        
        protected SqlConnection CreateAndOpenSqlConnection()
        {
            var db = new UnitTestCommon.ProjectFirmaSqlDatabase();
            var sqlConnection = db.CreateConnection();
            sqlConnection.Open();
            return sqlConnection;
        }

    }
}
