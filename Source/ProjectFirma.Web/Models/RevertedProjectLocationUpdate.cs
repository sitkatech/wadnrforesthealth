using System.Data.Entity.Spatial;

namespace ProjectFirma.Web.Models
{
    public partial class RevertedProjectLocationUpdate
    {
        public int ProjectLocationID { get; set; }
        public DbGeometry ProjectLocationUpdateGeometry { get; set; }
        public string ProjectLocationUpdateNotes { get; set; }
        public ProjectLocationType ProjectLocationType { get; set; }
        public string ProjectLocationUpdateName { get; set; }
        public int? ArcGisObjectID { get; set; }
        public string ArcGisGlobalID { get; set; }

        public RevertedProjectLocationUpdate(int projectLocationID, DbGeometry projectLocationUpdateGeometry, string projectLocationUpdateNotes, ProjectLocationType projectLocationType, string projectLocationUpdateName, int? arcGisObjectID, string arcGisGlobalID)
        {
            ProjectLocationID = projectLocationID;
            ProjectLocationUpdateGeometry = projectLocationUpdateGeometry;
            ProjectLocationUpdateNotes = projectLocationUpdateNotes;
            ProjectLocationType = projectLocationType;
            ProjectLocationUpdateName = projectLocationUpdateName;
            ArcGisObjectID = arcGisObjectID;
            ArcGisGlobalID = arcGisGlobalID;
        }

    }
}