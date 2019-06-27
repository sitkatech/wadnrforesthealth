using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class SocrataDataMartUpdateBackgroundJob : ScheduledBackgroundJobBase
    {
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
            throw new NotImplementedException("You probably want to run derived versions of this class, not this class itself.");
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

        public int ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType,
                                               DateTime lastFinanceApiLoadDate,
                                               int? optionalBienniumFiscalYear,
                                               string rawJsonString)
        {
            Logger.Info($"Starting '{JobName}' ShoveRawJsonStringIntoTable");
            SocrataDataMartRawJsonImport newRawJsonImport = new SocrataDataMartRawJsonImport(DateTime.Now, socrataDataMartRawJsonImportTableType, rawJsonString, JsonImportStatusType.NotYetProcessed);
            newRawJsonImport.FinanceApiLastLoadDate = lastFinanceApiLoadDate;
            newRawJsonImport.BienniumFiscalYear = optionalBienniumFiscalYear;

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

        public void MarkJsonImportStatus(int socrataDataMartRawJsonImportID, JsonImportStatusType jsonImportStatusType)
        {
            // Because these objects are so huge, we try to avoid bringing them into memory directly, hence 
            // the proc to keep it at arm's length.
            Logger.Info($"Starting '{JobName}' MarkJsonImportStatus");
            string vendorImportProc = "dbo.pMarkJsonImportStatus";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                Logger.Info($"'{JobName}' MarkJsonImportStatus - Marking socrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID} as JsonImportStatusType {jsonImportStatusType.JsonImportStatusTypeName}");
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.Parameters.AddWithValue("@JsonImportStatusTypeID", jsonImportStatusType.JsonImportStatusTypeID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' MarkJsonImportStatus");
        }

        public class SuccessfulJsonImportInfo
        {
            public int SocrataDataMartRawJsonImportTableTypeID;
            public int BienniumFiscalYear;
            public DateTime JsonImportDate;
            public DateTime FinanceApiLastLoadDate;

            /// <summary>
            /// Empty constructor
            /// </summary>
            public SuccessfulJsonImportInfo()
            {
            }

            /// <summary>
            /// Complete Constructor
            /// </summary>
            public SuccessfulJsonImportInfo(int socrataDataMartRawJsonImportTableTypeID, int bienniumFiscalYear, DateTime jsonImportDate, DateTime financeApiLastLoadDate)
            {
                SocrataDataMartRawJsonImportTableTypeID = socrataDataMartRawJsonImportTableTypeID;
                BienniumFiscalYear = bienniumFiscalYear;
                JsonImportDate = jsonImportDate;
                FinanceApiLastLoadDate = financeApiLastLoadDate;
            }
        }


        public SuccessfulJsonImportInfo LatestSuccessfulJsonImportInfoForBienniumAndImportTableType(int socrataDataMartRawJsonImportTableTypeID, int optionalBienniumFiscalYear)
        {
            // Because these objects are so huge, we try to avoid bringing them into memory directly, hence 
            // the proc to keep it at arm's length.
            Logger.Info($"Starting '{JobName}' LatestSuccessfulJsonImportInfoForBienniumAndImportTableType");
            string vendorImportProc = "dbo.pLatestSuccessfulJsonImportInfoForBienniumAndImportTableType";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportTableTypeID", socrataDataMartRawJsonImportTableTypeID);
                    cmd.Parameters.AddWithValue("@OptionalBienniumFiscalYear", optionalBienniumFiscalYear);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        SuccessfulJsonImportInfo importInfo = new SuccessfulJsonImportInfo();

                        bool queryReturnedData = dataReader.Read();
                        if (!queryReturnedData)
                        {
                            return null;
                        }

                        importInfo.SocrataDataMartRawJsonImportTableTypeID = (int)dataReader["SocrataDataMartRawJsonImportTableTypeID"];
                        importInfo.BienniumFiscalYear = (int)dataReader["BienniumFiscalYear"];
                        importInfo.JsonImportDate = (DateTime)dataReader["JsonImportDate"];
                        importInfo.FinanceApiLastLoadDate = (DateTime)dataReader["FinanceApiLastLoadDate"];

                        Logger.Info($"Ending '{JobName}' pLatestSuccessfulJsonImportInfoForBienniumAndImportTableType");

                        return importInfo;
                    }
                }
            }
        }

        //exec dbo.pLatestSuccessfulJsonImportInfoForBienniumAndImportTableType @SocrataDataMartRawJsonImportTableTypeID= 4, @OptionalBienniumFiscalYear = 2003

        protected SqlConnection CreateAndOpenSqlConnection()
        {
            var db = new UnitTestCommon.ProjectFirmaSqlDatabase();
            var sqlConnection = db.CreateConnection();
            sqlConnection.Open();
            return sqlConnection;
        }

    }
}
