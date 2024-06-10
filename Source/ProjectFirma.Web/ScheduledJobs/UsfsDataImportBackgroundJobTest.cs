using System.Collections.Generic;
using Hangfire;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Mvc;

namespace ProjectFirma.Web.ScheduledJobs
{
    [TestFixture]
    public class UsfsDataImportBackgroundJobTest
    {
        [Ignore("mainly used for debugging purposes")]
        [Test, Description("Simple test to at least get some coverage of USFS. However test can take over 7 minutes. Can be useful for performance profiling.")]
        public void WholeJobShouldRunWithoutErrors()
        {
            var jobCancellationToken = new JobCancellationToken(false);
            Assert.DoesNotThrow(() => UsfsDataImportBackgroundJob.Instance.RunJob(jobCancellationToken), $"Job {nameof(UsfsDataImportBackgroundJob)} should complete without exceptions");
        }

        [Ignore("mainly used for debugging purposes")]
        [Test, Description("Simple test to at least get some coverage of USFS. sample post request")]
        public async void WhoCaresTest()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "WADNR Forest Health Tracker");
            var formContent = new FormUrlEncodedContent(new[]
            {
                //new KeyValuePair<string, string>("where", whereClause),
                new KeyValuePair<string, string>("where", "1=1"),
                new KeyValuePair<string, string>("f", "json"),
                new KeyValuePair<string, string>("outSR", "4326"),
                //new KeyValuePair<string, string>("geometry", waStateBoundary),
                //new KeyValuePair<string, string>("geometryType", geometryType),
                //new KeyValuePair<string, string>("inSR", "3857"),
                //new KeyValuePair<string, string>("spatialRel", spatialRel),
                new KeyValuePair<string, string>("returnCountOnly", "true"),
            });
            var response = await httpClient.PostAsync("https://apps.fs.usda.gov/arcx/rest/services/EDW/EDW_ActivityFactsCommonAttributes_01/MapServer/0/query", formContent);
            var jobCancellationToken = new JobCancellationToken(false);
            
        }
        
        
        

    }
}