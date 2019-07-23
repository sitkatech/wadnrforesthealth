using System;
using System.Net;
using log4net;
using LtInfo.Common;
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


        public static DateTime GetLastLoadDate()
        {
            DateTime lastLoadDate;

            Uri lastLoadDateUrl = new Uri(FirmaWebConfiguration.LastLoadDateUrl);

            try
            {
                using (var webClient = new WebClient())
                {
                    string response = webClient.DownloadString(lastLoadDateUrl);
                    var lastLoadDateJson = JsonTools.DeserializeObject<LastLoadDateJsonResponse>(response);
                    lastLoadDate = lastLoadDateJson.LoadCompleteDt;
                }
            }
            catch (Exception exception)
            {
                var logger = LogManager.GetLogger(typeof(FinanceApiLastLoadUtil));

                string errorMessage = $"Error retrieving URL {lastLoadDateUrl}: {exception.Message}";
                logger.Error(errorMessage);
                throw;
            }

            return lastLoadDate;
        }
    }
}