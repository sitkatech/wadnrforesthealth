using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Hangfire;
using Newtonsoft.Json;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class LoaDataImportBackgroundJob : ScheduledBackgroundJobBase
    {
        public static LoaDataImportBackgroundJob Instance;

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static LoaDataImportBackgroundJob()
        {
            Instance = new LoaDataImportBackgroundJob();
        }

        public LoaDataImportBackgroundJob() : base("LOA Data Import Background Job", ConcurrencySetting.RunJobByItself)
        {
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadLoaEasternData();
        }

        private void DownloadLoaEasternData()
        {
            HttpClient httpClient = new HttpClient();
            if (!ArcGisOnlineUtility.AddApplicationAuthToken(httpClient))
            {
                return;
            }
            //var metroArcGIS = new MetroArcGIS(_configuration, _logger, "metro");
            //metroArcGIS.UpdateFeatures(httpClient);
        }
    }
}
