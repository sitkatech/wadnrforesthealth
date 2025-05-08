using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class PriorityLandscape : IAuditableEntity, ICanUploadNewFiles
    {
        public string DisplayName => PriorityLandscapeName;

        public List<Project> GetAssociatedProjectsVisibleToUser(Person currentPerson)
        {
            var associatedProjectsVisibleToUser = ProjectPriorityLandscapes.Select(ptc => ptc.Project).ToList().GetActiveProjectsAndProposalsVisibleToUser(currentPerson);
            return associatedProjectsVisibleToUser;
        }

        public string AuditDescriptionString => PriorityLandscapeName;

        public Feature MakeFeatureWithRelevantProperties()
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(PriorityLandscapeLocation);
            feature.Properties.Add("Priority Landscape", GetDisplayNameAsUrl().ToString());
            return feature;
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), DisplayName);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<PriorityLandscapeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));

        public string GetDetailUrl()
        {
            return DetailUrlTemplate.ParameterReplace(PriorityLandscapeID);
        }

        public static LayerGeoJson GetPriorityLandscapeWmsLayerGeoJson(string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            return new LayerGeoJson("Priority Landscapes", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetPriorityLandscapeWmsLayerName(), layerColor, layerOpacity,
                layerInitialVisibility, "/Content/leaflet/images/washington_priority_landscape.png");
        }

        public static LayerGeoJson GetPriorityLandscapeWmsLayerGeoJson(decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, PriorityLandscapeCategory priorityLandscapeCategory)
        {
            return new LayerGeoJson(priorityLandscapeCategory.PriorityLandscapeCategoryDisplayName, FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetPriorityLandscapeWmsLayerName(), "", layerOpacity,
                layerInitialVisibility, $"PriorityLandscapeCategoryID={priorityLandscapeCategory.PriorityLandscapeCategoryID}", true, priorityLandscapeCategory.GetPriorityLandscapeMapLayerIconImagePath());
        }


        public static List<LayerGeoJson> GetPriorityLandscapeAndAssociatedProjectLayers(PriorityLandscape priorityLandscape, List<Project> projects)
        {
            var projectLayerGeoJson = new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Simple", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetAllProjectLocationsSimpleWmsLayerName(), "#ffff00", 1, LayerInitialVisibility.Show, $"ProjectID in ({string.Join(",", projects.Select(x => x.ProjectID).ToList())})", true);
            var priorityLandscapeLayerGeoJson = new LayerGeoJson(priorityLandscape.DisplayName,
                new List<PriorityLandscape> { priorityLandscape }.ToGeoJsonFeatureCollection(), "#2dc3a1", 1,
                LayerInitialVisibility.Show);

            var layerGeoJsons = new List<LayerGeoJson>
            {
                projectLayerGeoJson,
                priorityLandscapeLayerGeoJson,
                GetPriorityLandscapeWmsLayerGeoJson(0.6m,
                    LayerInitialVisibility.Show, PriorityLandscapeCategory.East),
                GetPriorityLandscapeWmsLayerGeoJson(0.6m,
                    LayerInitialVisibility.Show, PriorityLandscapeCategory.West)
            };


            if (projects.Any())
            {
                var projectDetailedLocationsLayerGeoJson = Project.ProjectDetailedLocationsToGeoJsonFeatureCollection(projects);
                layerGeoJsons.Add(projectDetailedLocationsLayerGeoJson);

                var projectTreatmentAreasLayerGeoJson = Project.ProjectTreatmentAreasToGeoJsonFeatureCollection(projects);
                if (projectTreatmentAreasLayerGeoJson != null)
                {
                    layerGeoJsons.Add(projectTreatmentAreasLayerGeoJson);
                }
            }

            return layerGeoJsons;
        }


        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var priorityLandscapeFileResource = new PriorityLandscapeFileResource(this, fileResource, displayName) { Description = description };
            PriorityLandscapeFileResources.Add(priorityLandscapeFileResource);
        }

    }
}