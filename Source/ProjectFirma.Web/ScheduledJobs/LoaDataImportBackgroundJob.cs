using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApprovalUtilities.Reflection;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Hangfire;
using Newtonsoft.Json;
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
            DownloadLoaEasternData();
        }

        private void DownloadLoaEasternData()
        {
            var arcUtility = new ArcGisOnlineUtility();
            HttpClient httpClient = new HttpClient();
            if (!arcUtility.AddApplicationAuthToken(httpClient))
            {
                return;
            }

            var result = new LoaProjectGeometriesDto();

            var queryString = "?f=json&where=Approval_ID is not null&outSr=4326";
            queryString += "&outFields=approval_id,date_completed,project_status,gis_acres,prune_acres,thin_acres,chip_acres,mast_mow_acres,graze_acres,lopscat_acres,biomass_acres,handpile_acres,rxburn_acres,handburn_acres,machburn_acres,other_acres,landowner";
            var easternUrl = FirmaWebConfiguration.ArcGisLoaDataEasternUrl; //_configuration["ARCGIS_METRO_SITE_URL"];
            easternUrl += queryString;
            HttpResponseMessage response;
            LoaProjectGeometriesDto processedResponse;
            try
            {
                response = httpClient.GetAsync(easternUrl).Result;
                response.EnsureSuccessStatusCode();
                processedResponse = arcUtility.ProcessRepsonse<LoaProjectGeometriesDto>(response);

                var systemUser = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();
                var uploadSourceOrganization = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.SingleOrDefault(x => x.GisUploadSourceOrganizationID == LoaGisUploadSourceOrganizationID);
                var gisAttempt = new GisUploadAttempt(uploadSourceOrganization, systemUser, DateTime.Now);

                HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
                
                
                //Feature = new Feature(new Polygon(siteFeature.geometry.rings), siteFeature.attributes)
                
                var featureList = new List<Feature>();
                foreach (var record in processedResponse.features)
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
                }
                var featureCollection = new FeatureCollection(featureList.Where(GisProjectBulkUpdateController.IsUsableFeatureGeoJson).ToList());

                GisProjectBulkUpdateController.SaveGisUploadToNormalizedFieldsUsingGeoJson(gisAttempt, featureCollection);



                var gisMetadataAttributeIDs = gisAttempt.GisUploadAttemptGisMetadataAttributes.Select(x => x.GisMetadataAttributeID).ToList();
                var metadataAttributes =
                    HttpRequestStorage.DatabaseEntities.GisMetadataAttributes.Where(x =>
                        gisMetadataAttributeIDs.Contains(x.GisMetadataAttributeID));


                var viewModel = new GisMetadataViewModel(gisAttempt, metadataAttributes.ToList());

                GisProjectBulkUpdateController.ImportProjects(gisAttempt, viewModel, out var projectListCount, out var skippedProjectCount, out var existingProjectCount);

               var message = $"Successfully imported {projectListCount} new {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}. Successfully updated {existingProjectCount} existing {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}. Skipped adding/updating {skippedProjectCount} {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.";

               Logger.Info(message);

            }
            catch (Exception)
            {
                return;  // the calling code will set back a 400
            }

            if (processedResponse != null)
            {
                result = processedResponse;
            }




            
            return;



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
            public string Grant_Name { get; set; }
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
        }

        private class GeometryDto
        {
            public List<List<List<double>>> rings { get; set; }
        }

    }



}
