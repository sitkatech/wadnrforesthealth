using System.Collections.Generic;
using System.Globalization;
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

        public static readonly UrlTemplate<int> MapPopupUrlTemplate = new UrlTemplate<int>(SitkaRoute<InteractionEventController>.BuildUrlFromExpression(t => t.InteractionEventMapPopup(UrlTemplate.Parameter1Int)));
        public static string GetMapPopupUrl(this InteractionEvent interactionEvent)
        {
            return MapPopupUrlTemplate.ParameterReplace(interactionEvent.InteractionEventID);
        }

        public static LayerGeoJson GetInteractionEventsLayerGeoJson(this IEnumerable<InteractionEvent> interactionEvents)
        {
            var interactionEventFeatureCollection = interactionEvents.ToGeoJsonFeatureCollection();

            var layerName = $"All {FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()} Locations";

            return new LayerGeoJson(layerName, interactionEventFeatureCollection, "yellow", 1, LayerInitialVisibility.Hide);
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<InteractionEvent> interactionEvents)
        {
            var featureCollection = new FeatureCollection();

            foreach (var interactionEvent in interactionEvents.Where(ie => ie.HasLocationSet))
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(interactionEvent.InteractionEventLocationSimple);


                feature.Properties.Add("Title", interactionEvent.InteractionEventTitle);

                feature.Properties.Add("InteractionEventID", interactionEvent.InteractionEventID.ToString(CultureInfo.InvariantCulture));
                feature.Properties.Add("InteractionEventTypeID", interactionEvent.InteractionEventType.InteractionEventTypeID);
                feature.Properties.Add("Date", interactionEvent.InteractionEventDate.Date.ToString(CultureInfo.InvariantCulture));

                feature.Properties.Add("PopupUrl", interactionEvent.GetMapPopupUrl());

                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }
    }
}