using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class TreatmentModelExtensions
    {
        public static readonly UrlTemplate<int> ProjectMapPopuUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.ProjectMapPopup(UrlTemplate.Parameter1Int)));
        public static string GetProjectMapPopupUrl(this Treatment treatment)
        {
            return ProjectMapPopuUrlTemplate.ParameterReplace(treatment.ProjectID);
        }

        public static readonly UrlTemplate<int> ProjectMapSimplePopuUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.ProjectSimpleMapPopup(UrlTemplate.Parameter1Int)));
        public static string GetProjectSimpleMapPopupUrl(this Treatment treatment)
        {
            return ProjectMapSimplePopuUrlTemplate.ParameterReplace(treatment.ProjectID);
        }

        public static readonly UrlTemplate<int> EditTreatmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentController>.BuildUrlFromExpression(t => t.EditTreatment(UrlTemplate.Parameter1Int)));
        public static string GetEditTreatmentUrl(this Treatment treatment)
        {
            return EditTreatmentUrlTemplate.ParameterReplace(treatment.TreatmentID);
        }





        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Treatment> treatments)
        {

            var treatmentsAsList = treatments.ToList();

            if (!treatmentsAsList.Any())
            {
                return null;
            }

            var featureCollection = new FeatureCollection();

            foreach (var treatment in treatmentsAsList.Where(ie => ie.ProjectLocation != null))
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatment.ProjectLocation.ProjectLocationGeometry);
                feature.Properties.Add("TreatmentID", treatment.TreatmentID);
                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }

        public static FeatureCollection ToGeoJsonFeatureCollectionWithPopupUrl(this IEnumerable<Treatment> treatments)
        {

            var treatmentsAsList = treatments.ToList();

            if (!treatmentsAsList.Any())
            {
                return null;
            }

            var featureCollection = new FeatureCollection();

            foreach (var treatment in treatmentsAsList.Where(ie => ie.ProjectLocation != null))
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatment.ProjectLocation.ProjectLocationGeometry);
                feature.Properties.Add("TreatmentID", treatment.TreatmentID);
                feature.Properties.Add("PopupUrl", treatment.GetProjectSimpleMapPopupUrl());
                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }
    }
}