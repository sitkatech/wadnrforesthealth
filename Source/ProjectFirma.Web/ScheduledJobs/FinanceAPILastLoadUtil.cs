using System;
using System.Collections.Generic;
using System.Linq;
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
                var respObj = JsonConvert.DeserializeObject<FinanaceApiLastLoadObject>(respTxt);
                var feature = respObj?.features.First();

                if (feature != null)
                {
                    var lastLoadDateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(feature.attributes.LOAD_COMPLETE_DATE);
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



    public class FinanaceApiLastLoadObject
    {
        public string objectIdFieldName { get; set; }
        public string globalIdFieldName { get; set; }
        public bool exceededTransferLimit { get; set; }
        public List<FinanceApiLastLoadFeatures>  features { get; set; }

    }

    public class FinanceApiLastLoadFeatures
    {
        public FinanceApiLastLoadAttributes attributes { get; set; }
    }

    public class FinanceApiLastLoadAttributes
    {
        public string FINANCIAL_LOAD_HISTORY_ID { get; set; }
        public string LOAD_FREQUENCY { get; set; }
        public long LOAD_COMPLETE_DATE { get; set; }
    }


    /*
    3/2/2023 TK - This is an example response from the Finance API Last Load call

     {
    "objectIdFieldName": "",
    "globalIdFieldName": "",
    "fields": [
        {
            "name": "FINANCIAL_LOAD_HISTORY_ID",
            "alias": "FINANCIAL_LOAD_HISTORY_ID",
            "type": "esriFieldTypeInteger"
        },
        {
            "name": "LOAD_FREQUENCY",
            "alias": "LOAD_FREQUENCY",
            "type": "esriFieldTypeString",
            "length": 30
        },
        {
            "name": "LOAD_COMPLETE_DATE",
            "alias": "LOAD_COMPLETE_DATE",
            "type": "esriFieldTypeDate",
            "length": 8
        }
    ],
    "features": [
        {
            "attributes": {
                "FINANCIAL_LOAD_HISTORY_ID": 14443,
                "LOAD_FREQUENCY": "DAILY",
                "LOAD_COMPLETE_DATE": 1677741246000
            }
        }
    ],
    "exceededTransferLimit": true
}
     */
}