using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.PerformanceMeasure;
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
            feature.Properties.Add("PriorityLandscape", GetDisplayNameAsUrl().ToString());
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
            return new LayerGeoJson("20-Year Plan for eastern WA", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetPriorityLandscapeWmsLayerName(), layerColor, layerOpacity,
                layerInitialVisibility);
        }

        public static List<LayerGeoJson> GetPriorityLandscapeAndAssociatedProjectLayers(PriorityLandscape priorityLandscape, List<Project> projects)
        {
            var projectLayerGeoJson = new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Simple",
                Project.MappedPointsToGeoJsonFeatureCollection(projects, true, false),
                "#ffff00", 1, LayerInitialVisibility.Show);
            var priorityLandscapeLayerGeoJson = new LayerGeoJson(priorityLandscape.DisplayName,
                new List<PriorityLandscape> { priorityLandscape }.ToGeoJsonFeatureCollection(), "#2dc3a1", 1,
                LayerInitialVisibility.Show);

            var layerGeoJsons = new List<LayerGeoJson>
            {
                projectLayerGeoJson,
                priorityLandscapeLayerGeoJson,
                GetPriorityLandscapeWmsLayerGeoJson("#59ACFF", 0.6m,
                    LayerInitialVisibility.Show)
            };

            return layerGeoJsons;
        }

        public PerformanceMeasureChartViewData GetPerformanceMeasureChartViewData(PerformanceMeasure performanceMeasure, Person currentPerson)
        {
            var projects = GetAssociatedProjectsVisibleToUser(currentPerson);
            return new PerformanceMeasureChartViewData(performanceMeasure, currentPerson, false, projects);
        }

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var priorityLandscapeFileResource = new PriorityLandscapeFileResource(this, fileResource, displayName) { Description = description };
            PriorityLandscapeFileResources.Add(priorityLandscapeFileResource);
        }
    }
}