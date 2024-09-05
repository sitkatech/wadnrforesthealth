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
    public class UsfsNepaBoundaryDataImportBackgroundJob : ScheduledBackgroundJobBase
    {
        public static UsfsNepaBoundaryDataImportBackgroundJob Instance;

        public static int UsfsNepaBoundaryGisUploadSourceOrganizationID = 15;

        public override List<FirmaEnvironmentType> RunEnvironments => new List<FirmaEnvironmentType>
        {
            FirmaEnvironmentType.Local,
            FirmaEnvironmentType.Prod,
            FirmaEnvironmentType.Qa
        };

        static UsfsNepaBoundaryDataImportBackgroundJob()
        {
            Instance = new UsfsNepaBoundaryDataImportBackgroundJob();
        }

        public UsfsNepaBoundaryDataImportBackgroundJob() : base("USFS NEPA Boundary Data Import Background Job", ConcurrencySetting.RunJobByItself)
        {
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadArcOnlineDataAndImportProjects(FirmaWebConfiguration.ArcGisUsfsNepaBoundaryDataUrl, jobCancellationToken);
        }

        private void DownloadArcOnlineDataAndImportProjects(string arcOnlineUrl, IJobCancellationToken jobCancellationToken)
        {
            var arcUtility = new ArcGisOnlineUtility();
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "WADNR Forest Health Tracker");

            var uploadSourceOrganization = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.SingleOrDefault(x => x.GisUploadSourceOrganizationID == UsfsNepaBoundaryGisUploadSourceOrganizationID);

            if (uploadSourceOrganization == null)
            {
                throw new ApplicationException($"GisUploadSourceOrganization(ID:{UsfsNepaBoundaryGisUploadSourceOrganizationID}) does not exist");
            }

            

            var outFields = "ID,NAME,SHAPE,PROJECTTYPE,REV_DATE,NEPA_DOC_TYPE_CODE,NEPA_SIGNED_DATE";
            var whereClause = $"1=1";
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
                var countResponse = arcUtility.ProcessResponse<UsfsNepaBoundaryApiCountResponse>(returnCountResponse.Result);
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

                // loop until we get all the records, the max returned is 500
                var featuresFromApi = new List<UsfsNepaBoundaryFeatureDto>();
                var batchSize = 100; // Process in batches of 100
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
                    var processedResponse = arcUtility.ProcessResponse<UsfsNepaBoundaryGeometriesDto>(response);
                    featuresFromApi.AddRange(processedResponse.features);

                    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                }


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
        private class UsfsNepaBoundaryApiCountResponse
        {
            public List<int> objectIds { get; set; }
        }

        private class UsfsNepaBoundaryGeometriesDto
        {
            public List<UsfsNepaBoundaryFeatureDto> features { get; set; }
        }

        private class UsfsNepaBoundaryFeatureDto
        {
            public UsfsNepaBoundaryAttributesDto attributes { get; set; }
            public GeometryDto geometry { get; set; }
        }

        private class UsfsNepaBoundaryAttributesDto
        {
            //DATE_COMPLETED,ACTIVITY,NEPA_DOC_NBR,NEPA_PROJECT_NAME,DATE_AWARDED,GIS_ACRES
            public string ID { get; set; }
            public string NAME { get; set; }

            public string PROJECTTYPE { get; set; }
            public string REV_DATE { get; set; }

            public string NEPA_DOC_TYPE_CODE { get; set; }
            public string NEPA_SIGNED_DATE { get; set; }



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
