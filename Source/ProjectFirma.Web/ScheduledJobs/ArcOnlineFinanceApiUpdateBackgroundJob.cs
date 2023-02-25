using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Hangfire;
using log4net;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ArcOnlineFinanceApiUpdateBackgroundJob : ScheduledBackgroundJobBase
    {
        public ArcOnlineFinanceApiUpdateBackgroundJob(string jobName, ConcurrencySetting concurrencySetting) : base(jobName, concurrencySetting)
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

        //public Uri AddSocrataMaxLimitTagToUrl(Uri baseSocrataJsonApiUrl)
        //{
        //    var uriBuilder = new UriBuilder(baseSocrataJsonApiUrl);
        //    uriBuilder.Query += "$limit=9999999";
        //    return uriBuilder.Uri;
        //}

        /// <summary>
        /// Download the contents of the given URL to a temp file
        /// </summary>
        /// <returns>Full path of the temp file created that contains the contents of the URL</returns>
        public string DownloadArcOnlineUrlToString(Uri urlToDownload, SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType)
        {
            Logger.Info($"Starting '{JobName}' DownloadArcOnlineUrlToString");
            try
            {
                using (var webClient = new WebClient())
                {
                    // This isn't needed with public APIs, but may help to prevent throttling, and let's the other side know who we are in a polite way.
                    // See: http://xxxxx
                    webClient.Headers.Add("X-App-Token", MultiTenantHelpers.GetSocrataAppToken());
                    string socrataJsonData = webClient.DownloadString(urlToDownload);

                    Logger.Info($"Ending '{JobName}' DownloadArcOnlineUrlToString");
                    return socrataJsonData;
                }
            }
            catch (Exception exception)
            {
                string errorMessage = $"Error downloading ArcOnline type {socrataDataMartRawJsonImportTableType.SocrataDataMartRawJsonImportTableTypeName}, URL {urlToDownload}: {exception.Message}";
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
            string markJsonImportStatus = "dbo.pMarkJsonImportStatus";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                Logger.Info($"'{JobName}' MarkJsonImportStatus - Marking socrataDataMartRawJsonImportID {socrataDataMartRawJsonImportID} as JsonImportStatusType {jsonImportStatusType.JsonImportStatusTypeName}");
                using (SqlCommand cmd = new SqlCommand(markJsonImportStatus, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.Parameters.AddWithValue("@JsonImportStatusTypeID", jsonImportStatusType.JsonImportStatusTypeID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' MarkJsonImportStatus");
        }

        /// <summary>
        /// Clears *ALL* entries in the table.
        /// </summary>
        public static void ClearSocrataDataMartRawJsonImportsTable()
        {
            ILog logger = LogManager.GetLogger(typeof(SocrataDataMartUpdateBackgroundJob));

            logger.Info($"Starting  pClearSocrataDataMartRawJsonImportsTable");
            string vendorImportProc = "pClearSocrataDataMartRawJsonImportsTable";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            logger.Info($"Ending pClearSocrataDataMartRawJsonImportsTable");
        }


        // Anything older than this number of days before the last API load date in the table will be cleared.
        // This could be made configurable, and should be if this value proves to be
        // in any way controversial. -- SLG 7/18/2019
        public const int StaleEntriesDayCutoff = 5;

        // 
        /// <summary>
        /// Clears only *OLD* entries in the Socrata imports table
        /// </summary>
        public static void ClearOutdatedSocrataDataMartRawJsonImportsTableEntries()
        {
            ILog logger = LogManager.GetLogger(typeof(SocrataDataMartUpdateBackgroundJob));

            logger.Info($"Starting pClearOutdatedSocrataDataMartRawJsonImports({StaleEntriesDayCutoff} days)");
            string vendorImportProc = "pClearOutdatedSocrataDataMartRawJsonImports";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@daysOldToRemove", StaleEntriesDayCutoff);
                    cmd.ExecuteNonQuery();
                }
            }
            logger.Info($"Ending pClearOutdatedSocrataDataMartRawJsonImports({StaleEntriesDayCutoff} days)");
        }

        public class SuccessfulJsonImportInfo
        {
            public int SocrataDataMartRawJsonImportTableTypeID;
            public int? BienniumFiscalYear;
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


        public SuccessfulJsonImportInfo LatestSuccessfulJsonImportInfoForBienniumAndImportTableType(int socrataDataMartRawJsonImportTableTypeID, int? optionalBienniumFiscalYear)
        {
            // Because these objects are so huge, we try to avoid bringing them into memory directly, hence 
            // the proc to keep it at arm's length.
            Logger.Info($"Starting '{JobName}' LatestSuccessfulJsonImportInfoForBienniumAndImportTableType");
            string vendorImportProc = "dbo.pLatestSuccessfulJsonImportInfoForBienniumAndImportTableType";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
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
                        if (dataReader["BienniumFiscalYear"] != System.DBNull.Value)
                        {
                            importInfo.BienniumFiscalYear = (int?)dataReader["BienniumFiscalYear"];
                        }
                        importInfo.JsonImportDate = (DateTime)dataReader["JsonImportDate"];
                        importInfo.FinanceApiLastLoadDate = (DateTime)dataReader["FinanceApiLastLoadDate"];

                        Logger.Info($"Ending '{JobName}' pLatestSuccessfulJsonImportInfoForBienniumAndImportTableType");

                        return importInfo;
                    }
                }
            }
        }

     }
}
