using Hangfire;
using Newtonsoft.Json;
using ProjectFirma.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.GisProjectBulkUpdate;
using System.Net;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class UsfsDataImportBackgroundJob : ScheduledBackgroundJobBase
    {
        public static UsfsDataImportBackgroundJob Instance;

        public static int UsfsGisUploadSourceOrganizationID = 14;

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static UsfsDataImportBackgroundJob()
        {
            Instance = new UsfsDataImportBackgroundJob();
        }

        public UsfsDataImportBackgroundJob() : base("USFS Data Import Background Job", ConcurrencySetting.RunJobByItself)
        {
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadArcOnlineDataAndImportProjects(FirmaWebConfiguration.ArcGisUsfsDataUrl, jobCancellationToken);
        }

        private void DownloadArcOnlineDataAndImportProjects(string arcOnlineUrl, IJobCancellationToken jobCancellationToken)
        {
            var arcUtility = new ArcGisOnlineUtility();
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "WADNR Forest Health Tracker");

            var uploadSourceOrganization = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.SingleOrDefault(x => x.GisUploadSourceOrganizationID == UsfsGisUploadSourceOrganizationID);

            if (uploadSourceOrganization == null)
            {
                throw new ApplicationException($"GisUploadSourceOrganization(ID:{UsfsGisUploadSourceOrganizationID}) does not exist");
            }

            var activitiesForWhereClause = uploadSourceOrganization.GisCrossWalkDefaults.Select(x => $"'{x.GisCrossWalkSourceValue}'").Distinct().ToList();
            //GisDefaultMappingColumnName
            var mappedFieldNamesToQuery = uploadSourceOrganization.GisDefaultMappings.Select(x => x.GisDefaultMappingColumnName).ToList();
            var outFields = string.Join(",", mappedFieldNamesToQuery);

            //TK 5-22-25 - leaving this comment here that lists all the columns in the API endpoint as of this date. Feel free to use this if you want lots of data :shrug:
            //var outFields = "OBJECTID,SU_SECURITY_ID,AU_ORG,SUID,FACTS_ID,SUBUNIT,NAME,FEATURE_TYPE,SITE_NBR_OF_UNITS,UOM,ACTIVITY_CODE,ACTIVITY,LOCAL_QUALIFIER,METHOD_CODE,METHOD,EQUIPMENT_CODE,EQUIPMENT,FUND_CODES,COST_PER_UNIT,WORKFORCE_CODE,FISCAL_YEAR_PLANNED,FISCAL_YEAR_AWARDED,DATE_AWARDED,FISCAL_YEAR_COMPLETED,DATE_COMPLETED,NBR_UNITS_PLANNED,NBR_UNITS_ACCOMPLISHED,EXCLUDE_ACCOMPLISHMENT,TREATMENT_NAME,FUELS_KEYPOINT_AREA,ISWUI,FIREREGIME,CWPP,BIOMASS_UTILIZATION,NFPORS_CATEGORY,NFPORS_TREATMENT,PURPOSE_CODE,SALE_NAME,SALE_NUMBER,SALE_CATEGORY,UNIT_ID,PURCHASER_NAME,CONTRACT_PLANNED_TERM,AWARD_DATE,SALE_CLOSURE_DATE,BASE_YEAR,KV_NBR_UNITS_FUNDED,PERCENT_FUNDED,CAUSAL_AGENT,REFORESTATION_STATUS,EXAM_NBR,NEEDS_ADJUSTMENT,EVENT_NAME,EVENT_YEAR,IMPLEMENTATION_PROJECT,IMPL_PROJECT_NBR,IMPL_PROJECT_TYPE,NEPA_DOC_NBR,NEPA_DOC_TYPE,NEPA_PROJECT_NAME,NEPA_HFI,NEPA_HFRA,NEPA_SIGNED_DATE,ADMIN_REGION,ADMIN_FOREST,ADMIN_DISTRICT,AU_REGION,AU_FOREST,AU_DISTRICT,PROC_FOREST,OWNERSHIP,STATE_ABBR,PRODUCTIVITY_CLASS,LAND_SUITABILITY_CODE,COUNTY_NAME,CONG_DIST_NAME,LATITUDE,LONGITUDE,LEGAL_LOCATION,ASPECT,SLOPE,ELEVATION,WATERSHED_CODE,MGT_AREA_CODE,MGT_PRESCRIPTION_CODE,ACTIVITY_SITE_REMARKS,ACTIVITY_REMARKS,SU_CREATED_DATE,SU_MODIFIED_DATE,ACT_CREATED_DATE,ACT_MODIFIED_DATE,ACTIVITY_UNIT_CN,LU_CN,SUID_CN,EVENT_CN,NEPA_PROJECT_CN,PALS_PROJECT_CN,SALE_CN,IMPLEMENTATION_PROJECT_CN,UKCN,FS_UNIT_ID,CRC_VALUE,SHAPE,NEEDS,TSI,AU_NAME,GIS_ACRES";
            var whereClause = $"DATE_COMPLETED>= DATE '2017-01-01' AND ACTIVITY IN ({string.Join(",", activitiesForWhereClause)})";
            var waStateBoundary = UsfsDataImportBackgroundJobVariable.WaStateBorderJsonPolygon;
            var geometryType = "esriGeometryPolygon";
            var spatialRel = "esriSpatialRelIntersects";

            var returnCountNameValueCollection = new[]
            {
                new KeyValuePair<string, string>("where", whereClause),
                new KeyValuePair<string, string>("f", "json"),
                new KeyValuePair<string, string>("outSR", "4326"),
                new KeyValuePair<string, string>("geometry", waStateBoundary),
                new KeyValuePair<string, string>("geometryType", geometryType),
                new KeyValuePair<string, string>("inSR", "4326"),
                new KeyValuePair<string, string>("spatialRel", spatialRel),
                new KeyValuePair<string, string>("outFields", outFields),
                new KeyValuePair<string, string>("returnIdsOnly", "true")
            };

            try
            {
                var encodedItems = returnCountNameValueCollection.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
                var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

                //get the total records available
                var returnCountResponse = httpClient.PostAsync(arcOnlineUrl, encodedContent);
                returnCountResponse.Result.EnsureSuccessStatusCode();
                var countResponse = arcUtility.ProcessResponse<UsfsProjectApiCountResponse>(returnCountResponse.Result);
                var totalRecordCount = countResponse.objectIds.Count;//countResponse.count;

                Logger.Info($"DownloadArcOnlineDataAndImportProjects: Attempting to download {totalRecordCount} from API endpoint: {arcOnlineUrl}");


                var baseFormNameValueCollection = new[]
                {
                    //new KeyValuePair<string, string>("where", whereClause),
                    new KeyValuePair<string, string>("f", "json"),
                    new KeyValuePair<string, string>("outSR", "4326"),
                    //new KeyValuePair<string, string>("geometry", waStateBoundary),
                    //new KeyValuePair<string, string>("geometryType", geometryType),
                    //new KeyValuePair<string, string>("inSR", "4326"),
                    //new KeyValuePair<string, string>("spatialRel", spatialRel),
                    new KeyValuePair<string, string>("outFields", outFields),
                    //new KeyValuePair<string, string>("returnIdsOnly", "true")
                };

                // loop until we get all the records, the max returned is 2000
                var featuresFromApi = new List<UsfsProjectFeatureDto>();
                var batchSize = 1000; // Process in batches of 1000
                for (int i = 0; i < countResponse.objectIds.Count; i += batchSize)
                {
                    var batchIds = countResponse.objectIds.Skip(i).Take(batchSize).ToList();
                    // Adjust the whereClause or the request to include batchIds
                    // Example: var whereClause = $"OBJECTID IN ({string.Join(",", batchIds)})";
                    // Note: The actual implementation depends on the API's support for querying by IDs

                    var currentRequestFormData = baseFormNameValueCollection.Append(new KeyValuePair<string, string>("where", $"OBJECTID in ({string.Join(",", batchIds)})"));
                    var currentRequestEncodedItems = currentRequestFormData.Select(item => WebUtility.UrlEncode(item.Key) + "=" + WebUtility.UrlEncode(item.Value));
                    var currentRequestEncodedContent = new StringContent(String.Join("&", currentRequestEncodedItems), null, "application/x-www-form-urlencoded");

                    var response = httpClient.PostAsync(arcOnlineUrl, currentRequestEncodedContent).Result;
                    response.EnsureSuccessStatusCode();
                    var processedResponse = arcUtility.ProcessResponse<UsfsProjectGeometriesDto>(response);
                    featuresFromApi.AddRange(processedResponse.features);

                    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                }
                //do
                //{
                //    Logger.Info($"resultOffset is currently set to {resultOffset}");
                //    var currentRequestFormData = baseFormNameValueCollection.Append(new KeyValuePair<string, string>("resultOffset", resultOffset.ToString()));
                //    var currentRequestEncodedItems = currentRequestFormData.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
                //    var currentRequestEncodedContent = new StringContent(String.Join("&", currentRequestEncodedItems), null, "application/x-www-form-urlencoded");

                //    var response = httpClient.PostAsync(arcOnlineUrl, currentRequestEncodedContent).Result;
                //    response.EnsureSuccessStatusCode();
                //    processedResponse = arcUtility.ProcessResponse<UsfsProjectGeometriesDto>(response);
                //    featuresFromApi.AddRange(processedResponse.features);

                //    resultOffset += processedResponse.features.Count;

                //    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                //} while (processedResponse.features.Count > 0);// && featuresFromApi.Count < totalRecordCount);


                //Check.Require(featuresFromApi.Count == totalRecordCount,
                //    $"Expected {totalRecordCount} features but got actual {featuresFromApi.Count} features. Check for any errors in code logic.");
                Logger.Info($"Expected '{totalRecordCount}' features from endpoint. Got '{featuresFromApi.Count}' features from endpoint.");

                var systemUser = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();

                var gisAttempt = new GisUploadAttempt(uploadSourceOrganization, systemUser, DateTime.Now);

                HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();

                var featureList = new List<Feature>();
                foreach (var record in featuresFromApi)
                {
                    if (record.geometry != null && record.geometry.rings != null)
                    {

                        var feature = new Feature(new Polygon(record.geometry.rings), record.attributes);
                        featureList.Add(feature);

                    }

                    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                }

                var featureCollection =
                    new FeatureCollection(featureList.Where(GisProjectBulkUpdateController.IsUsableFeatureGeoJson)
                        .ToList());

                GisProjectBulkUpdateController.SaveGisUploadToNormalizedFieldsUsingGeoJson(gisAttempt,
                    featureCollection);

                var gisMetadataAttributeIDs = gisAttempt.GisUploadAttemptGisMetadataAttributes
                    .Select(x => x.GisMetadataAttributeID).ToList();
                var metadataAttributes =
                    HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Where(x =>
                        gisMetadataAttributeIDs.Contains(x.GisMetadataAttributeID));

                var viewModel = new GisMetadataViewModel(gisAttempt, metadataAttributes.ToList());

                GisProjectBulkUpdateController.ImportProjects(gisAttempt, viewModel, out var newProjectListLog,
                    out var skippedProjectListLog, out var existingProjectListLog, jobCancellationToken);

                var message = new StringBuilder();
                message.AppendLine(
                    $"Successfully imported {newProjectListLog.Count} new {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

                if (newProjectListLog.Count > 0)
                {
                    message.AppendLine(
                        $" New {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are: {string.Join(", ", newProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))} ");
                }

                message.AppendLine(
                    $"Successfully updated {existingProjectListLog.Count} existing {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

                if (existingProjectListLog.Count > 0)
                {
                    message.AppendLine(
                        $" Updated {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are: {string.Join(", ", existingProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))}  ");
                }

                message.AppendLine(
                    $"Skipped adding/updating {skippedProjectListLog.Count} {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}. ");
                if (skippedProjectListLog.Count > 0)
                {
                    message.AppendLine(
                        $"Skipped {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are: {string.Join(", ", skippedProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))}");
                }

                Logger.Info(message);

            }
            catch (Exception ex)
            {
                // the calling code will set back a 400
                throw new ApplicationException(
                    $"Exception: {ex.GetType()}; Message: {ex.Message}; Occurred in {nameof(DownloadArcOnlineDataAndImportProjects)} while processing with Arc URL {arcOnlineUrl}",
                    ex);
            }
            finally
            {
                httpClient.Dispose();
            }
        }

        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        // ReSharper disable ClassNeverInstantiated.Local
        // ReSharper disable CollectionNeverUpdated.Local
        // ReSharper disable UnusedMember.Local
#pragma warning disable IDE1006 // Naming Styles
        private class UsfsProjectApiCountResponse
        {
            public List<int> objectIds { get; set; }
        }

        private class UsfsProjectGeometriesDto
        {
            public List<UsfsProjectFeatureDto> features { get; set; }
        }

        private class UsfsProjectFeatureDto
        {
            public UsfsProjectAttributesDto attributes { get; set; }
            public GeometryDto geometry { get; set; }
        }

        private class UsfsProjectAttributesDto
        {
            //DATE_COMPLETED,ACTIVITY,NEPA_DOC_NBR,NEPA_PROJECT_NAME,DATE_AWARDED,GIS_ACRES
            public string OBJECTID { get; set; }
            public string SU_SECURITY_ID { get; set; }
            public string AU_ORG { get; set; }
            public string SUID { get; set; }
            public string FACTS_ID { get; set; }
            public string SUBUNIT { get; set; }
            public string NAME { get; set; }
            public string FEATURE_TYPE { get; set; }
            public string SITE_NBR_OF_UNITS { get; set; }
            public string UOM { get; set; }
            public string ACTIVITY_CODE { get; set; }
            public string ACTIVITY { get; set; }
            public string LOCAL_QUALIFIER { get; set; }
            public string METHOD_CODE { get; set; }
            public string METHOD { get; set; }
            public string EQUIPMENT_CODE { get; set; }
            public string EQUIPMENT { get; set; }
            public string FUND_CODES { get; set; }
            public string COST_PER_UNIT { get; set; }
            public string WORKFORCE_CODE { get; set; }
            public string FISCAL_YEAR_PLANNED { get; set; }
            public string FISCAL_YEAR_AWARDED { get; set; }
            public string DATE_AWARDED { get; set; }
            public string FISCAL_YEAR_COMPLETED { get; set; }
            public string DATE_COMPLETED { get; set; }
            public string NBR_UNITS_PLANNED { get; set; }
            public string NBR_UNITS_ACCOMPLISHED { get; set; }
            public string EXCLUDE_ACCOMPLISHMENT { get; set; }
            public string TREATMENT_NAME { get; set; }
            public string FUELS_KEYPOINT_AREA { get; set; }
            public string ISWUI { get; set; }
            public string FIREREGIME { get; set; }
            public string CWPP { get; set; }
            public string BIOMASS_UTILIZATION { get; set; }
            public string NFPORS_CATEGORY { get; set; }
            public string NFPORS_TREATMENT { get; set; }
            public string PURPOSE_CODE { get; set; }
            public string SALE_NAME { get; set; }
            public string SALE_NUMBER { get; set; }
            public string SALE_CATEGORY { get; set; }
            public string UNIT_ID { get; set; }
            public string PURCHASER_NAME { get; set; }
            public string CONTRACT_PLANNED_TERM { get; set; }
            public string AWARD_DATE { get; set; }
            public string SALE_CLOSURE_DATE { get; set; }
            public string BASE_YEAR { get; set; }
            public string KV_NBR_UNITS_FUNDED { get; set; }
            public string PERCENT_FUNDED { get; set; }
            public string CAUSAL_AGENT { get; set; }
            public string REFORESTATION_STATUS { get; set; }
            public string EXAM_NBR { get; set; }
            public string NEEDS_ADJUSTMENT { get; set; }
            public string EVENT_NAME { get; set; }
            public string EVENT_YEAR { get; set; }
            public string IMPLEMENTATION_PROJECT { get; set; }
            public string IMPL_PROJECT_NBR { get; set; }
            public string IMPL_PROJECT_TYPE { get; set; }
            public string NEPA_DOC_NBR { get; set; }
            public string NEPA_DOC_TYPE { get; set; }
            public string NEPA_PROJECT_NAME { get; set; }
            public string NEPA_HFI { get; set; }
            public string NEPA_HFRA { get; set; }
            public string NEPA_SIGNED_DATE { get; set; }
            public string ADMIN_REGION { get; set; }
            public string ADMIN_FOREST { get; set; }
            public string ADMIN_DISTRICT { get; set; }
            public string AU_REGION { get; set; }
            public string AU_FOREST { get; set; }
            public string AU_DISTRICT { get; set; }
            public string PROC_FOREST { get; set; }
            public string OWNERSHIP { get; set; }
            public string STATE_ABBR { get; set; }
            public string PRODUCTIVITY_CLASS { get; set; }
            public string LAND_SUITABILITY_CODE { get; set; }
            public string COUNTY_NAME { get; set; }
            public string CONG_DIST_NAME { get; set; }
            public string LATITUDE { get; set; }
            public string LONGITUDE { get; set; }
            public string LEGAL_LOCATION { get; set; }
            public string ASPECT { get; set; }
            public string SLOPE { get; set; }
            public string ELEVATION { get; set; }
            public string WATERSHED_CODE { get; set; }
            public string MGT_AREA_CODE { get; set; }
            public string MGT_PRESCRIPTION_CODE { get; set; }
            public string ACTIVITY_SITE_REMARKS { get; set; }
            public string ACTIVITY_REMARKS { get; set; }
            public string SU_CREATED_DATE { get; set; }
            public string SU_MODIFIED_DATE { get; set; }
            public string ACT_CREATED_DATE { get; set; }
            public string ACT_MODIFIED_DATE { get; set; }
            public string ACTIVITY_UNIT_CN { get; set; }
            public string LU_CN { get; set; }
            public string SUID_CN { get; set; }
            public string EVENT_CN { get; set; }
            public string NEPA_PROJECT_CN { get; set; }
            public string PALS_PROJECT_CN { get; set; }
            public string SALE_CN { get; set; }
            public string IMPLEMENTATION_PROJECT_CN { get; set; }
            public string UKCN { get; set; }
            public string FS_UNIT_ID { get; set; }
            public string CRC_VALUE { get; set; }
            public string SHAPE { get; set; }
            public string NEEDS { get; set; }
            public string TSI { get; set; }
            public string AU_NAME { get; set; }
            public string GIS_ACRES { get; set; }

        }

        private class GeometryDto
        {
            public List<List<List<double>>> rings { get; set; }
        }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
        // ReSharper restore InconsistentNaming
        // ReSharper restore ClassNeverInstantiated.Local
        // ReSharper restore CollectionNeverUpdated.Local
        // ReSharper restore UnusedMember.Local
#pragma warning restore IDE1006 // Naming Styles
    }
}
