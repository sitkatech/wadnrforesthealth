using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;

namespace ProjectFirma.Web.Models
{
    public static class TreatmentModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Treatment> treatments)
        {

            var treatmentsAsList = treatments.ToList();

            if (!treatmentsAsList.Any())
            {
                return null;
            }

            var featureCollection = new FeatureCollection();

            foreach (var treatmnent in treatmentsAsList.Where(ie => ie.TreatmentFeature != null))
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatmnent.TreatmentFeature);
                feature.Properties.Add("TreatmentID", treatmnent.TreatmentID);
                //feature.Properties.Add("PopupUrl", treatmnent.GetMapPopupUrl());
                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }
    }
}