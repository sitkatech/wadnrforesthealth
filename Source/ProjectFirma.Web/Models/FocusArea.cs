using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Microsoft.SqlServer.Types;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FocusArea : IAuditableEntity, IHaveSqlGeometry
    {

        public List<Project> GetAssociatedProjects(Person person)
        {
            return Projects.ToList();
        }

        public string AuditDescriptionString => FocusAreaName;

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<FocusAreaController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));

        public DbGeometry DbGeometry
        {
            get { return FocusAreaLocation; }
            set { FocusAreaLocation = value; }
        }

        public SqlGeometry SqlGeometry
        {
            get { return FocusAreaLocation.ToSqlGeometry(); }
        }

        public FeatureCollection FocusAreaLocationToFeatureCollection()
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(FocusAreaLocation);
            //feature.Properties.Add(OrganizationType.OrganizationTypeName, OrganizationName);
            return new FeatureCollection(new List<Feature> { feature });
        }
        public List<Feature> FocusAreaLocationToFeature()
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(FocusAreaLocation);
            return new List<Feature> { feature };
        }
    }
}