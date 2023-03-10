using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using Hangfire;
using log4net;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
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
        public string DownloadArcOnlineUrlToString(Uri urlToDownload, string token, string whereClause, string outFields, string orderByFields, ArcOnlineFinanceApiRawJsonImportTableType arcOnlineFinanceApiRawJsonImportTableType)
        {
            Logger.Info($"Starting '{JobName}' DownloadArcOnlineUrlToString");

            var ret = new List<string>();
            var PAGE_SIZE = 1000;
            var offset = 0;
            var hasDataToQuery = true;

            while (hasDataToQuery)
            {
                using (var hc = new HttpClient())
                {
                    var where = $"{whereClause}";
                    var queryUrl =
                        $"{urlToDownload}?token={token}" +
                        $"&f=json" +
                        $"&where={where}" +
                        $"&outFields={outFields}" +
                        $"&orderByFields={orderByFields}" +
                        $"&resultRecordCount={PAGE_SIZE}" +
                        $"&resultOffset={offset}" +

                        // the followings are optional.
                        //$"&datumTransformation=" +
                        //$"&featureEncoding=esriDefault" +
                        //$"&gdbVersion=" +
                        //$"&geometry=" +
                        //$"&geometryPrecision=" +
                        //$"&geometryType=esriGeometryEnvelope" +
                        //$"&groupByFieldsForStatistics=" +
                        //$"&having=" +
                        //$"&historicMoment=" +
                        //$"&inSR=" +
                        //$"&maxAllowableOffset=" +
                        //$"&objectIds=" +
                        //$"&outSR=" +
                        //$"&outStatistics=" +
                        //$"&parameterValues=" +
                        //$"&queryByDistance=" +
                        //$"&quantizationParameters=" +
                        //$"&rangeValues=" +
                        //$"&relationParam=" +
                        //$"&returnCountOnly=false" +
                        //$"&returnDistinctValues=true" +
                        //$"&returnExtentOnly=false" +
                        //$"&returnGeometry=false" +
                        //$"&returnIdsOnly=false" +
                        //$"&returnM=false" +
                        //$"&returnTrueCurves=false" +
                        //$"&returnZ=false" +
                        //$"&spatialRel=esriSpatialRelIntersects" +
                        //$"&text=" +
                        //$"&time=" +
                        string.Empty;

                    //Console.WriteLine($"\r\n-==Query String==-\r\n{queryUrl}");

                    var respTxt = hc.GetStringAsync(queryUrl).Result;
                    dynamic respObj = JsonConvert.DeserializeObject(respTxt);
                    hasDataToQuery = Convert.ToBoolean(respObj.exceededTransferLimit);
                    foreach (var item in respObj.features)
                    {
                        ret.Add(JsonConvert.SerializeObject(item.attributes));
                        offset += 1;
                    }
                    //Console.WriteLine($"Queried {ret.Count} so far..");
                }
            }

            return $"[{string.Join(",", ret)}]";


        }



        public int ShoveRawJsonStringIntoTable(ArcOnlineFinanceApiRawJsonImportTableType arcOnlineFinanceApiRawJsonImportTableType,
                                               DateTime lastFinanceApiLoadDate,
                                               int? optionalBienniumFiscalYear,
                                               string rawJsonString)
        {
            Logger.Info($"Starting '{JobName}' ArcOnline ShoveRawJsonStringIntoTable");

            var newRawJsonImport = new ArcOnlineFinanceApiRawJsonImport(DateTime.Now,
                arcOnlineFinanceApiRawJsonImportTableType, rawJsonString, JsonImportStatusType.NotYetProcessed);
            newRawJsonImport.FinanceApiLastLoadDate = lastFinanceApiLoadDate;
            newRawJsonImport.BienniumFiscalYear = optionalBienniumFiscalYear;

            HttpRequestStorage.DatabaseEntities.ArcOnlineFinanceApiRawJsonImports.Add(newRawJsonImport);

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
            Logger.Info($"Ending '{JobName}' ArcOnline ShoveRawJsonStringIntoTable");
            return newRawJsonImport.ArcOnlineFinanceApiRawJsonImportID;
        }

        public void MarkJsonImportStatus(int arcOnlineFinanceApiRawJsonImportID, JsonImportStatusType jsonImportStatusType)
        {
            // Because these objects are so huge, we try to avoid bringing them into memory directly, hence 
            // the proc to keep it at arm's length.
            Logger.Info($"Starting '{JobName}' ArcOnline MarkJsonImportStatus");
            string markJsonImportStatus = "dbo.pArcOnlineMarkJsonImportStatus";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                Logger.Info($"'{JobName}' ArcOnline MarkJsonImportStatus - Marking arcOnlineFinanceApiRawJsonImportID {arcOnlineFinanceApiRawJsonImportID} as JsonImportStatusType {jsonImportStatusType.JsonImportStatusTypeName}");
                using (SqlCommand cmd = new SqlCommand(markJsonImportStatus, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArcOnlineFinanceApiRawJsonImportID", arcOnlineFinanceApiRawJsonImportID);
                    cmd.Parameters.AddWithValue("@JsonImportStatusTypeID", jsonImportStatusType.JsonImportStatusTypeID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ArcOnline MarkJsonImportStatus");
        }

        /// <summary>
        /// Clears *ALL* entries in the table.
        /// </summary>
        public static void ClearArcOnlineFinanceApiRawJsonImportsTable()
        {
            ILog logger = LogManager.GetLogger(typeof(SocrataDataMartUpdateBackgroundJob));

            logger.Info($"Starting  pClearArcOnlineFinanceApiRawJsonImportsTable");
            string vendorImportProc = "pClearArcOnlineFinanceApiRawJsonImportsTable";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            logger.Info($"Ending pClearArcOnlineFinanceApiRawJsonImportsTable");
        }


        // Anything older than this number of days before the last API load date in the table will be cleared.
        // This could be made configurable, and should be if this value proves to be
        // in any way controversial. -- SLG 7/18/2019
        public const int StaleEntriesDayCutoff = 5;

        // 
        /// <summary>
        /// Clears only *OLD* entries in the ArcOnline imports table
        /// </summary>
        public static void ClearOutdatedArcOnlineFinanceApiRawJsonImportsTableEntries()
        {
            ILog logger = LogManager.GetLogger(typeof(SocrataDataMartUpdateBackgroundJob));

            logger.Info($"Starting pClearOutdatedArcOnlineFinanceApiRawJsonImports({StaleEntriesDayCutoff} days)");
            string vendorImportProc = "pClearOutdatedArcOnlineFinanceApiRawJsonImports";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@daysOldToRemove", StaleEntriesDayCutoff);
                    cmd.ExecuteNonQuery();
                }
            }
            logger.Info($"Ending pClearOutdatedArcOnlineFinanceApiRawJsonImports({StaleEntriesDayCutoff} days)");
        }

        public class SuccessfulJsonImportInfo
        {
            public int ArcOnlineFinanceApiRawJsonImportTableTypeID;
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
            public SuccessfulJsonImportInfo(int arcOnlineFinanceApiRawJsonImportTableTypeID, int bienniumFiscalYear, DateTime jsonImportDate, DateTime financeApiLastLoadDate)
            {
                ArcOnlineFinanceApiRawJsonImportTableTypeID = arcOnlineFinanceApiRawJsonImportTableTypeID;
                BienniumFiscalYear = bienniumFiscalYear;
                JsonImportDate = jsonImportDate;
                FinanceApiLastLoadDate = financeApiLastLoadDate;
            }
        }


        public SuccessfulJsonImportInfo LatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi(int arcOnlineFinanceApiRawJsonImportTableTypeID, int? optionalBienniumFiscalYear)
        {
            // Because these objects are so huge, we try to avoid bringing them into memory directly, hence 
            // the proc to keep it at arm's length.
            Logger.Info($"Starting '{JobName}' LatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi");
            string vendorImportProc = "dbo.pLatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi";
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;//ArcOnlineFinanceApiRawJsonImportTableType
                    cmd.Parameters.AddWithValue("@ArcOnlineFinanceApiRawJsonImportTableTypeID", arcOnlineFinanceApiRawJsonImportTableTypeID);
                    cmd.Parameters.AddWithValue("@OptionalBienniumFiscalYear", optionalBienniumFiscalYear);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        SuccessfulJsonImportInfo importInfo = new SuccessfulJsonImportInfo();

                        bool queryReturnedData = dataReader.Read();
                        if (!queryReturnedData)
                        {
                            return null;
                        }

                        importInfo.ArcOnlineFinanceApiRawJsonImportTableTypeID = (int)dataReader["ArcOnlineFinanceApiRawJsonImportTableTypeID"];
                        if (dataReader["BienniumFiscalYear"] != System.DBNull.Value)
                        {
                            importInfo.BienniumFiscalYear = (int?)dataReader["BienniumFiscalYear"];
                        }
                        importInfo.JsonImportDate = (DateTime)dataReader["JsonImportDate"];
                        importInfo.FinanceApiLastLoadDate = (DateTime)dataReader["FinanceApiLastLoadDate"];

                        Logger.Info($"Ending '{JobName}' pLatestSuccessfulJsonImportInfoForBienniumAndImportTableTypeFromArcOnlineFinanceApi");

                        return importInfo;
                    }
                }
            }
        }

     }
}
