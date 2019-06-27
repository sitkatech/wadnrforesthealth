using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class ProjectCodeImportHangfireBackgroundJob : SocrataDataMartUpdateBackgroundJob
    {
        private static readonly Uri ProjectCodeJsonSocrataBaseUrl = new Uri(FirmaWebConfiguration.ProjectCodeJsonSocrataBaseUrl);

        public static ProjectCodeImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static ProjectCodeImportHangfireBackgroundJob()
        {
            Instance = new ProjectCodeImportHangfireBackgroundJob();
        }

        public ProjectCodeImportHangfireBackgroundJob() : base("Project Code Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void ProjectCodeImportJson(int socrataDataMartRawJsonImportID)
        {
            Logger.Info($"Starting '{JobName}' ProjectCodeImportJson");
            string vendorImportProc = "dbo.pProjectCodeImportJson";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ProjectCodeImportJson");
        }


        public void DownloadSocrataProjectCodeTable()
        {
            Logger.Info($"Starting '{JobName}' DownloadSocrataProjectCodeTable");

            // See how current the data is
            DateTime lastFinanceApiLoadDate = FinanceApiLastLoadUtil.GetLastLoadDate();

            // Pull JSON off the page into a (possibly huge) string
            var fullUrl = AddSocrataMaxLimitTagToUrl(ProjectCodeJsonSocrataBaseUrl);
            var projectCodeJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.ProjectCode);
            Logger.Info($"ProjectCode JSON length: {projectCodeJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            var socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.ProjectCode, lastFinanceApiLoadDate, null, projectCodeJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");
            // Use the JSON to refresh the Project Code table
            ProjectCodeImportJson(socrataDataMartRawJsonImportID);

            Logger.Info($"Ending '{JobName}' DownloadSocrataProjectCodeTable");
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadSocrataProjectCodeTable();
        }
    }
}