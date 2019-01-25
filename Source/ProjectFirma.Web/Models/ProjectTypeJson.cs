namespace ProjectFirma.Web.Models
{
    public class ProjectTypeJson
    {
        public int ProjectTypeID { get; set; }
        public string ProjectTypeName { get; set; }
        public bool Selected { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectTypeJson() { }

        public ProjectTypeJson(ProjectType projectType) : this()
        {
            ProjectTypeName = projectType.ProjectTypeName;
            ProjectTypeID = projectType.ProjectTypeID;
        }

        public ProjectTypeJson(string projectTypeName, int projectTypeID) : this()
        {
            ProjectTypeName = projectTypeName;
            ProjectTypeID = projectTypeID;
        }

        public ProjectTypeJson(string projectTypeName, int projectTypeID, bool selected) : this()
        {
            ProjectTypeName = projectTypeName;
            ProjectTypeID = projectTypeID;
            Selected = selected;
        }
    }
}