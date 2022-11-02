using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Logging;
using Newtonsoft.Json;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ArcGisOnlineUtility
    {
        

        public Task UpdateFeatures(int tenantID)
        {
            //HttpClient httpClient = new HttpClient();

            //if (tenantID == 2)
            //{
            //    if (!AddUserAuthToken(httpClient))
            //    {
            //        return;
            //    }
            //    var cwsArcGIS = new CWSArcGIS(_configuration, _logger, "cws");
            //    cwsArcGIS.UpdateFeatures(httpClient);
            //}
            //else if (tenantID == 1)
            //{
            //    if (!AddApplicationAuthToken(httpClient, _configuration["ARCGIS_METRO_CLIENT_ID"], _configuration["ARCGIS_METRO_CLIENT_SECRET"]))
            //    {
            //        return;
            //    }
            //    var metroArcGIS = new MetroArcGIS(_configuration, _logger, "metro");
            //    metroArcGIS.UpdateFeatures(httpClient);
            //}
            //else
            //{
            //    _logger.Warning("Not Implemented Yet");
            //}
            throw new NotImplementedException();
        }

        public T ProcessRepsonse<T>(HttpResponseMessage response)
        {
            using (var sr = new StreamReader(response.Content.ReadAsStreamAsync().Result))
            {
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    T responseObject;
                    try
                    {
                        responseObject = serializer.Deserialize<T>(jsonTextReader);
                    }
                    catch (Exception ex)
                    {
                        ILog Logger = LogManager.GetLogger(typeof(ArcGisOnlineUtility));
                        Logger.Error($"Unable to deserialize response: {ex}");
                        throw ex;
                    }

                    return responseObject;
                }
            }
        }



        public bool AddApplicationAuthToken(HttpClient httpClient)
        {
            var url = FirmaWebConfiguration.ArcGisAuthUrl;

            var body = new Dictionary<string, string>();
            body.Add("client_id", FirmaWebConfiguration.ArcGisClientId);
            body.Add("client_secret", FirmaWebConfiguration.ArcGisClientSecret);
            body.Add("grant_type", "client_credentials");

            var htmlContent = new FormUrlEncodedContent(body);
            HttpResponseMessage response;

            ArcGISApplicationAuthorizationDto arcGISAuthorization;
            try
            {
                response = httpClient.PostAsync(url, htmlContent).Result;
                response.EnsureSuccessStatusCode();
                arcGISAuthorization = ProcessRepsonse<ArcGISApplicationAuthorizationDto>(response);
            }
            catch (Exception e)
            {
                ILog Logger = LogManager.GetLogger(typeof(ArcGisOnlineUtility));
                Logger.Error("ArcGIS: Unable to get token.", e);
                return false;
            }

            if (httpClient.DefaultRequestHeaders.Contains("X-Esri-Authorization"))
            {
                httpClient.DefaultRequestHeaders.Remove("X-Esri-Authorization");
            }
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Esri-Authorization", $"Bearer {arcGISAuthorization.access_token}");
            return true;
        }


        public class ArcGISApplicationAuthorizationDto
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }
    }
}