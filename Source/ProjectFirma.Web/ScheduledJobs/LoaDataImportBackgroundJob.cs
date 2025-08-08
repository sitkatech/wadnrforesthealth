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
    public class LoaDataImportBackgroundJob : ScheduledBackgroundJobBase
    {
        public static LoaDataImportBackgroundJob Instance;

        public static int LoaGisUploadSourceOrganizationID = 3;

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
            DownloadArcOnlineDataAndImportProjects(FirmaWebConfiguration.ArcGisLoaDataEasternUrl, jobCancellationToken);
            DownloadArcOnlineDataAndImportProjects(FirmaWebConfiguration.ArcGisLoaDataWesternUrl, jobCancellationToken);
        }

        private void DownloadArcOnlineDataAndImportProjects(string arcOnlineUrl, IJobCancellationToken jobCancellationToken)
        {
            var arcUtility = new ArcGisOnlineUtility();
            HttpClient httpClient = new HttpClient();
            if (!arcUtility.AddApplicationAuthToken(httpClient))
            {
                return;
            }


            var uploadSourceOrganization = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.SingleOrDefault(x => x.GisUploadSourceOrganizationID == LoaGisUploadSourceOrganizationID);


            //outFields = approval_id,date_completed,project_status,gis_acres,prune_acres,thin_acres,chip_acres,mast_mow_acres,graze_acres,lopscat_acres,biomass_acres,handpile_acres,rxburn_acres,handburn_acres,machburn_acres,other_acres,landowner,email
            var mappedFieldNamesToQuery = uploadSourceOrganization.GisDefaultMappings.Select(x => x.GisDefaultMappingColumnName).ToList();
            var outFields = string.Join(",", mappedFieldNamesToQuery);
            var queryString = $"?f=json&outSr=4326&where=Approval_ID%20is%20not%20null";
            queryString += $"&outFields={outFields}";
            var arcOnlineUrlWithQueryString = arcOnlineUrl + queryString;
            try
            {
                //get the total records available
                var response = httpClient.GetAsync(arcOnlineUrlWithQueryString + "&returnCountOnly=true").Result;
                response.EnsureSuccessStatusCode();
                var countResponse = arcUtility.ProcessResponse<LoaProjectApiCountResponse>(response);
                var totalRecordCount = countResponse.count;

                Logger.Info($"DownloadArcOnlineDataAndImportProjects: Attempting to download {totalRecordCount} from API endpoint: {arcOnlineUrlWithQueryString}");

                // loop until we get all the records, the max returned is 5000
                var featuresFromApi = new List<LoaProjectFeatureDto>();
                var resultOffset = 0;
                LoaProjectGeometriesDto processedResponse;
                do
                {
                    var requestUri = arcOnlineUrlWithQueryString + $"&resultOffset={resultOffset}";

                    response = httpClient.GetAsync(requestUri).Result;
                    response.EnsureSuccessStatusCode();
                    processedResponse = arcUtility.ProcessResponse<LoaProjectGeometriesDto>(response);
                    featuresFromApi.AddRange(processedResponse.features);

                    resultOffset += processedResponse.features.Count;

                    jobCancellationToken.ThrowIfCancellationRequestedDoNothingIfNull();
                } while (processedResponse.features.Count > 0 && featuresFromApi.Count < totalRecordCount);

                Check.Require(featuresFromApi.Count == totalRecordCount, $"Expected {totalRecordCount} features but got actual {featuresFromApi.Count} features. Check for any errors in code logic.");

                var systemUser = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();
                
                var gisAttempt = new GisUploadAttempt(uploadSourceOrganization, systemUser, DateTime.Now);

                HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
                
                var featureList = new List<Feature>();
                foreach (var record in featuresFromApi)
                {
                    if (record.geometry != null && record.geometry.rings != null)
                    {
                        var approvalAttribute = record.attributes.Approval_ID;
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
        private class LoaProjectApiCountResponse
        {
            public int count { get; set; }
        }

        private class LoaProjectGeometriesDto
        {
            public List<LoaProjectFeatureDto> features { get; set; }
        }

        private class LoaProjectFeatureDto
        {
            public LoaProjectAttributesDto attributes { get; set; }
            public GeometryDto geometry { get; set; }
        }

        private class LoaProjectAttributesDto
        {
            public string OBJECTID { get; set; }
            public string Landowner { get; set; }
            public string Approval_ID { get; set; }
            public string Project_Status { get; set; }
            public string Date_Time_Collected { get; set; }
            public string Forester { get; set; }
            public string Collection_Method { get; set; }
            public string FundSource_Name { get; set; }
            public string Fund_Source { get; set; }
            public string Agreement_Number { get; set; }
            public string Project_Index_Code { get; set; }
            public string Region_ID { get; set; }
            public string GIS_Acres { get; set; }
            public string Homes { get; set; }
            public string Other_Structures { get; set; }
            public string Comments { get; set; }
            public string Brush_Treatment { get; set; }
            public string Thin_Treatment { get; set; }
            public string Prune_Treatment { get; set; }
            public string Slash_Treatment { get; set; }
            public string Burn_Treatment { get; set; }
            public string Other_Treatment { get; set; }
            public string Prune_Acres { get; set; }
            public string Brush_Acres { get; set; }
            public string Thin_Acres { get; set; }
            public string Chip_Acres { get; set; }
            public string Mast_Mow_Acres { get; set; }
            public string LopScat_Acres { get; set; }
            public string Biomass_Acres { get; set; }
            public string Handpile_Acres { get; set; }
            public string Graze_Acres { get; set; }
            public string HandBurn_Acres { get; set; }
            public string RxBurn_Acres { get; set; }
            public string MachBurn_Acres { get; set; }
            public string Other_Acres { get; set; }
            public string Date_Completed { get; set; }
            public string Year_Completed { get; set; }
            public string Maint_Period { get; set; }
            public string Rate_Funded { get; set; }
            public string FHT_Project_Number { get; set; }
            public string created_user { get; set; }
            public string created_date { get; set; }
            public string last_edited_user { get; set; }
            public string last_edited_date { get; set; }
            public string GlobalID { get; set; }
            public string Shape__Area { get; set; }
            public string Shape__Length { get; set; }
            public string email { get; set; }
            public string Ref_Acres { get; set; }
            public string Seeding_Acres { get; set; }
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
