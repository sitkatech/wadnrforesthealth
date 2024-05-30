namespace ProjectFirma.Web.ScheduledJobs
{
    public class AgolPostRequestObject
    {
        //var outFields = "DATE_COMPLETED,ACTIVITY,NEPA_DOC_NBR,NEPA_PROJECT_NAME,DATE_AWARDED,GIS_ACRES";
        //var whereClause = $"DATE_COMPLETED>= DATE '2017-01-01' AND ACTIVITY  IN ({string.Join(",", activitiesForWhereClause)})";
        //var waStateBoundary = "-1.390677682508256E7,5697587.764863089,-1.2991930881749049E7,6283237.237876828";
        //var geometryType = "esriGeometryEnvelope";
        //var spatialRel = "esriSpatialRelIntersects";

        //var queryString = $"?f=json&outSr=4326&geometry={waStateBoundary}&geometryType={geometryType}&inSR=3857&spatialRel={spatialRel}&outFields={outFields}&where={whereClause}";
        public string where { get; set; }
        public string outSR { get; set; }
        public string inSR { get; set; }
        public string geometry { get; set; }
        public string geometryType { get; set; }
        public string spatialRel { get; set; }
        public string outFields { get; set; }
        public string f { get; set; }
        public bool returnCountOnly { get; set; }



    }
}