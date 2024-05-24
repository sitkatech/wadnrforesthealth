using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Hangfire;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.GisProjectBulkUpdate;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class UsfsDataImportBackgroundJob : ScheduledBackgroundJobBase
    {
        public static UsfsDataImportBackgroundJob Instance;

        public static int LoaGisUploadSourceOrganizationID = 3;

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

        public UsfsDataImportBackgroundJob() : base("LOA Data Import Background Job", ConcurrencySetting.RunJobByItself)
        {
        }

        protected override void RunJobImplementation(IJobCancellationToken jobCancellationToken)
        {
            DownloadArcOnlineDataAndImportProjects(FirmaWebConfiguration.ArcGisLoaDataEasternUrl, jobCancellationToken);
        }

        private void DownloadArcOnlineDataAndImportProjects(string arcOnlineUrl, IJobCancellationToken jobCancellationToken)
        {
            var arcUtility = new ArcGisOnlineUtility();
            HttpClient httpClient = new HttpClient();
            if (!arcUtility.AddApplicationAuthToken(httpClient))
            {
                return;
            }

            var queryString = $"?f=json&outSr=4326&where=Approval_ID%20is%20not%20null";
            queryString += "&outFields=approval_id,date_completed,project_status,gis_acres,prune_acres,thin_acres,chip_acres,mast_mow_acres,graze_acres,lopscat_acres,biomass_acres,handpile_acres,rxburn_acres,handburn_acres,machburn_acres,other_acres,landowner,email";
            var arcOnlineUrlWithQueryString = arcOnlineUrl + queryString;
            try
            {
                //get the total records available
                var response = httpClient.GetAsync(arcOnlineUrlWithQueryString + "&returnCountOnly=true").Result;
                response.EnsureSuccessStatusCode();
                var countResponse = arcUtility.ProcessRepsonse<UsfsProjectApiCountResponse>(response);
                var totalRecordCount = countResponse.count;

                Logger.Info($"DownloadArcOnlineDataAndImportProjects: Attempting to download {totalRecordCount} from API endpoint: {arcOnlineUrlWithQueryString}");

                // loop until we get all the records, the max returned is 2000
                var featuresFromApi = new List<UsfsProjectFeatureDto>();
                var resultOffset = 0;
                UsfsProjectGeometriesDto processedResponse;
                do
                {
                    var requestUri = arcOnlineUrlWithQueryString + $"&resultOffset={resultOffset}";

                    response = httpClient.GetAsync(requestUri).Result;
                    response.EnsureSuccessStatusCode();
                    processedResponse = arcUtility.ProcessRepsonse<UsfsProjectGeometriesDto>(response);
                    featuresFromApi.AddRange(processedResponse.features);

                    resultOffset += processedResponse.features.Count;

                    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                } while (processedResponse.features.Count > 0 && featuresFromApi.Count < totalRecordCount);

                Check.Require(featuresFromApi.Count == totalRecordCount, $"Expected {totalRecordCount} features but got actual {featuresFromApi.Count} features. Check for any errors in code logic.");

                var systemUser = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();
                var uploadSourceOrganization = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.SingleOrDefault(x => x.GisUploadSourceOrganizationID == LoaGisUploadSourceOrganizationID);
                var gisAttempt = new GisUploadAttempt(uploadSourceOrganization, systemUser, DateTime.Now);

                HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
                
                var featureList = new List<Feature>();
                foreach (var record in featuresFromApi)
                {
                    if (record.geometry != null && record.geometry.rings != null)
                    {
                        var approvalAttribute = record.attributes.NEPA_DOC_NBR;
                        if (approvalAttribute.Length <= Project.FieldLengths.ProjectName)
                        {
                            var feature = new Feature(new Polygon(record.geometry.rings), record.attributes);
                            featureList.Add(feature);
                        }
                    }

                    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                }
                var featureCollection = new FeatureCollection(featureList.Where(GisProjectBulkUpdateController.IsUsableFeatureGeoJson).ToList());

                GisProjectBulkUpdateController.SaveGisUploadToNormalizedFieldsUsingGeoJson(gisAttempt, featureCollection);

                var gisMetadataAttributeIDs = gisAttempt.GisUploadAttemptGisMetadataAttributes.Select(x => x.GisMetadataAttributeID).ToList();
                var metadataAttributes =
                    HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Where(x =>
                        gisMetadataAttributeIDs.Contains(x.GisMetadataAttributeID));

                var viewModel = new GisMetadataViewModel(gisAttempt, metadataAttributes.ToList());

                GisProjectBulkUpdateController.ImportProjects(gisAttempt, viewModel, out var newProjectListLog, out var skippedProjectListLog, out var existingProjectListLog, jobCancellationToken);

                var message = new StringBuilder();
                message.AppendLine($"Successfully imported {newProjectListLog.Count} new {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

                if (newProjectListLog.Count > 0)
                {
                    message.AppendLine($" New {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are: {string.Join(", ", newProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))} ");
                }

                message.AppendLine($"Successfully updated {existingProjectListLog.Count} existing {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

                if (existingProjectListLog.Count > 0)
                {
                    message.AppendLine($" Updated {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are: {string.Join(", ", existingProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))}  ");
                }

                message.AppendLine($"Skipped adding/updating {skippedProjectListLog.Count} {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}. ");
                if (skippedProjectListLog.Count > 0)
                {
                    message.AppendLine($"Skipped {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} are: {string.Join(", ", skippedProjectListLog.Select(x => $"({x.ProjectGisIdentifier}){x.ProjectName}"))}");
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
        }

        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        // ReSharper disable ClassNeverInstantiated.Local
        // ReSharper disable CollectionNeverUpdated.Local
        // ReSharper disable UnusedMember.Local
#pragma warning disable IDE1006 // Naming Styles
        private class UsfsProjectApiCountResponse
        {
            public int count { get; set; }
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
            //DATE_COMPLETED, ACTIVITY,NEPA_DOC_NBR,NEPA_PROJECT_NAME,DATE_AWARDED,GIS_ACRES
            public string NEPA_DOC_NBR { get; set; }
            public string NEPA_PROJECT_NAME { get; set; }

            public string Date_Completed { get; set; }
            public string Activity { get; set; }

            public string DATE_AWARDED { get; set; }
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
