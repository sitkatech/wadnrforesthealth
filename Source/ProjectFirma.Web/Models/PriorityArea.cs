using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.PerformanceMeasure;

namespace ProjectFirma.Web.Models
{
    public partial class PriorityArea : IAuditableEntity
    {
        public string DisplayName => PriorityAreaName;

        public List<Project> GetAssociatedProjects(Person person)
        {
            return ProjectPriorityAreas.Select(ptc => ptc.Project).ToList().GetActiveProjectsAndProposals(person.CanViewProposals);
        }

        public string AuditDescriptionString => PriorityAreaName;

        public Feature MakeFeatureWithRelevantProperties()
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(PriorityAreaLocation);
            feature.Properties.Add("PriorityArea", GetDisplayNameAsUrl().ToString());
            return feature;
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), DisplayName);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<PriorityAreaController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));

        public string GetDetailUrl()
        {
            return DetailUrlTemplate.ParameterReplace(PriorityAreaID);
        }

        public static readonly UrlTemplate<int> MapTooltipUrlTemplate = new UrlTemplate<int>(SitkaRoute<PriorityAreaController>.BuildUrlFromExpression(t => t.MapTooltip(UrlTemplate.Parameter1Int)));

        public static LayerGeoJson GetPriorityAreaWmsLayerGeoJson(string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            return new LayerGeoJson("PriorityAreas", FirmaWebConfiguration.GetMapServiceUrl(),
                FirmaWebConfiguration.GetPriorityAreaWmsLayerName(), MapTooltipUrlTemplate.UrlTemplateString, layerColor, layerOpacity,
                layerInitialVisibility);
        }

        public static List<LayerGeoJson> GetPriorityAreaAndAssociatedProjectLayers(PriorityArea priorityArea, List<Project> projects)
        {
            var projectLayerGeoJson = new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Simple",
                Project.MappedPointsToGeoJsonFeatureCollection(projects, true, false),
                "#ffff00", 1, LayerInitialVisibility.Show);
            var priorityAreaLayerGeoJson = new LayerGeoJson(priorityArea.DisplayName,
                new List<PriorityArea> { priorityArea }.ToGeoJsonFeatureCollection(), "#2dc3a1", 1,
                LayerInitialVisibility.Show);

            var layerGeoJsons = new List<LayerGeoJson>
            {
                projectLayerGeoJson,
                priorityAreaLayerGeoJson,
                GetPriorityAreaWmsLayerGeoJson("#59ACFF", 0.6m,
                    LayerInitialVisibility.Show)
            };

            return layerGeoJsons;
        }     

        public FancyTreeNode ToFancyTreeNode(Person currentPerson)
        {
            var fancyTreeNode = new FancyTreeNode(PriorityAreaName, PriorityAreaName, false) {MapUrl = null};

            var projectChildren = GetAssociatedProjects(currentPerson).OrderBy(x => x.DisplayName)
                .Select(x => x.ToFancyTreeNode()).ToList();
            fancyTreeNode.Children = projectChildren.ToList();

            return fancyTreeNode;
        }

        public PerformanceMeasureChartViewData GetPerformanceMeasureChartViewData(PerformanceMeasure performanceMeasure, Person currentPerson)
        {
            var projects = GetAssociatedProjects(currentPerson);
            return new PerformanceMeasureChartViewData(performanceMeasure, currentPerson, false, projects);
        }
    }
}