using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using log4net;
using LtInfo.Common;
using Newtonsoft.Json;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.ScheduledJobs
{
    /// <summary>
    /// This is a utility class for dealing with load dates for the so-called Finance API from Tammy.
    /// This is distinct from the Socrata APIs.
    /// 
    /// </summary>
    public class FinanceApiLastLoadUtil
    {
        public class LastLoadDateJsonResponse
        {
            public DateTime LoadCompleteDt;
        }


        public static DateTime GetLastLoadDate(string token)
        {
            DateTime lastLoadDate;
            var logger = LogManager.GetLogger(typeof(FinanceApiLastLoadUtil));


            using (var hc = new HttpClient())
            {
                var queryUrl =
                    $"{FirmaWebConfiguration.LastLoadDateUrl}?token={token}" +
                    $"&f=json" +
                    $"&where=1=1" +
                    $"&outFields=LOAD_FREQUENCY,FINANCIAL_LOAD_HISTORY_ID,LOAD_COMPLETE_DATE" +
                    $"&orderByFields=LOAD_COMPLETE_DATE DESC" +
                    $"&resultRecordCount=1";// +
                    //$"&resultOffset={offset}" +

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
                    

                

                var respTxt = hc.GetStringAsync(queryUrl).Result;
                dynamic respObj = JsonConvert.DeserializeObject(respTxt);
                var feature = respObj?.features[0];
                //long loadCompleteDate = 0;

                if (feature != null)// && long.TryParse(feature.attributes.LOAD_COMPLETE_DATE.Value, out loadCompleteDate))
                {
                    var lastLoadDateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(feature.attributes.LOAD_COMPLETE_DATE.Value);
                    lastLoadDate = lastLoadDateTimeOffset.DateTime;
                }
                else
                {
                    throw new Exception("Could not get the LastLoadDate of the finance API");
                }

            }

            return lastLoadDate;
        }
    }
}