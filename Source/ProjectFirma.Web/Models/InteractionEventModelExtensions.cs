using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class InteractionEventModelExtensions
    {

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.DeleteInteractionEvent(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this InteractionEvent interactionEvent)
        {
            return DeleteUrlTemplate.ParameterReplace(interactionEvent.InteractionEventID);
        }


        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.InteractionEventDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this InteractionEvent interactionEvent)
        {
            return DetailUrlTemplate.ParameterReplace(interactionEvent.InteractionEventID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.EditInteractionEvent(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this InteractionEvent interactionEvent)
        {
            return EditUrlTemplate.ParameterReplace(interactionEvent.InteractionEventID);
        }

        public static LayerGeoJson GetInteractionEventsLayerGeoJson(this IEnumerable<InteractionEvent> interactionEvents)
        {
            var featureCollection = new FeatureCollection();

            foreach (var interactionEvent in interactionEvents)
            {
                if (interactionEvent.InteractionEventLocationSimple != null)
                {
                    featureCollection.Features.Add(DbGeometryToGeoJsonHelper.FromDbGeometry(interactionEvent.InteractionEventLocationSimple));
                }
            }

            var layerName = $"All {FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()} Locations";

            return new LayerGeoJson(layerName, featureCollection, "yellow", 1, LayerInitialVisibility.Hide);
        }


    }
}