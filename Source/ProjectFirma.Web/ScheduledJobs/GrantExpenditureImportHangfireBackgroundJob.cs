﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Hangfire;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class GrantExpenditureImportHangfireBackgroundJob : SocrataDataMartUpdateBackgroundJob
    {
        private static readonly Uri GrantExpendituresJsonApiBaseUrl = new Uri(FirmaWebConfiguration.GrantExpendituresTempBaseUrl);

        public static GrantExpenditureImportHangfireBackgroundJob Instance;
        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static GrantExpenditureImportHangfireBackgroundJob()
        {
            Instance = new GrantExpenditureImportHangfireBackgroundJob();
        }

        public GrantExpenditureImportHangfireBackgroundJob() : base("Grant Expenditure Import", ConcurrencySetting.RunJobByItself)
        {
        }

        private void ClearGrantAllocationExpenditureTables()
        {
            Logger.Info($"Starting '{JobName}' ClearGrantAllocationExpenditureTables");
            string vendorImportProc = "pClearGrantAllocationExpenditureTables";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (var cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' ClearGrantAllocationExpenditureTables");
        }

        public void DownloadGrantExpendituresTableForAllFiscalYears()
        {
            Logger.Info($"Starting '{JobName}' DownloadGrantExpendituresTableForAllFiscalYears");

            // 2001 is the first biennium for which API returned data. This is WAY farther back than needed, but
            // harmless to overreach, since we only use data for which we have matching PI/PC.
            const int beginBienniumFiscalYear = 2001;

            const int bienniumStep = 2;

            // Go at least one biennium beyond the current one
            var endBienniumFiscalYear = CurrentBiennium.GetCurrentBienniumFiscalYear() + bienniumStep;

            // Always clear the expenditure data before doing the import, at least for now
            ClearGrantAllocationExpenditureTables();

            // Step through all the desired Bienniums
            for (var bienniumFiscalYear = beginBienniumFiscalYear; bienniumFiscalYear <= endBienniumFiscalYear; bienniumFiscalYear += bienniumStep)
            {
                ImportExpendituresForGivenBienniumFiscalYear(bienniumFiscalYear);
            }

            Logger.Info($"Ending '{JobName}' DownloadGrantExpendituresTableForAllFiscalYears");
        }

        private void GrantExpenditureImportJson(int socrataDataMartRawJsonImportID, int bienniumToImport)
        {
            Logger.Info($"Starting '{JobName}' GrantExpenditureImportJson");
            string vendorImportProc = "dbo.pGrantExpenditureImportJson";
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(vendorImportProc, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SocrataDataMartRawJsonImportID", socrataDataMartRawJsonImportID);
                    cmd.Parameters.AddWithValue("@BienniumToImport", bienniumToImport);
                    cmd.ExecuteNonQuery();
                }
            }
            Logger.Info($"Ending '{JobName}' GrantExpenditureImportJson");
        }

        private void ImportExpendituresForGivenBienniumFiscalYear(int bienniumFiscalYear)
        {
            Logger.Info($"ImportExpendituresForGivenBienniumFiscalYear - Biennium Fiscal Year {bienniumFiscalYear}");

            var fullUrl = GetGrantExpendituresJsonApiUrlWithAllParameters(bienniumFiscalYear);
            // Pull JSON off the page into a (possibly huge) string
            string grantExpenditureJson = DownloadSocrataUrlToString(fullUrl, SocrataDataMartRawJsonImportTableType.GrantExpenditure);
            // The JSON coming off this particular function is wonky and pre-escaped. I may suggest Tammy fix it, but for the moment we'll work with it, and 
            // clean it up ourselves.
            grantExpenditureJson = grantExpenditureJson.Remove(grantExpenditureJson.IndexOf('"'), 1);
            grantExpenditureJson = grantExpenditureJson.Remove(grantExpenditureJson.LastIndexOf('"'), 1);
            // Optional? Needed? 
            grantExpenditureJson = Regex.Unescape(grantExpenditureJson);
            Logger.Info($"GrantExpenditure BienniumFiscalYear {bienniumFiscalYear} JSON length: {grantExpenditureJson.Length}");
            // Push that string into a raw JSON string in the raw staging table
            int socrataDataMartRawJsonImportID = ShoveRawJsonStringIntoTable(SocrataDataMartRawJsonImportTableType.GrantExpenditure, grantExpenditureJson);
            Logger.Info($"New SocrataDataMartRawJsonImportID: {socrataDataMartRawJsonImportID}");

            // Import the given Biennium
            GrantExpenditureImportJson(socrataDataMartRawJsonImportID, bienniumFiscalYear);
        }

        /// <summary>
        /// Get the fully qualified URL for JSON GrantExpenditures
        /// </summary>
        /// <param name="biennium">Biennium is required</param>
        /// <returns></returns>
        public static Uri GetGrantExpendituresJsonApiUrlWithAllParameters(int biennium)
        {
            var builder = new UriBuilder(GrantExpendituresJsonApiBaseUrl);
            builder.Query += $"/{biennium}";
            return builder.Uri;
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadGrantExpendituresTableForAllFiscalYears();
        }
    }
}