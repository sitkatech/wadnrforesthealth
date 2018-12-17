namespace ProjectFirma.Web.Models
{
    public class ProjectPersonRelationshipTypeSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectPersonRelationshipTypeSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPersonRelationshipTypeSimple(int relationshipTypeID, string relationshipTypeName)
            : this()
        {
            ProjectPersonRelationshipTypeID = relationshipTypeID;
            ProjectPersonRelationshipTypeName = relationshipTypeName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public ProjectPersonRelationshipTypeSimple(ProjectPersonRelationshipType relationshipType)
            : this()
        {
            ProjectPersonRelationshipTypeID = relationshipType.ProjectPersonRelationshipTypeID;
            ProjectPersonRelationshipTypeName = relationshipType.ProjectPersonRelationshipTypeName;
        }

        public int ProjectPersonRelationshipTypeID { get; set; }
        public string ProjectPersonRelationshipTypeName { get; set; }
    }
}