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
        public ProjectPersonRelationshipTypeSimple(int relationshipTypeID, string relationshipTypeName, bool isRequired)
            : this()
        {
            ProjectPersonRelationshipTypeID = relationshipTypeID;
            ProjectPersonRelationshipTypeName = relationshipTypeName;
            IsRequired = isRequired;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public ProjectPersonRelationshipTypeSimple(ProjectPersonRelationshipType relationshipType)
            : this()
        {
            ProjectPersonRelationshipTypeID = relationshipType.ProjectPersonRelationshipTypeID;
            ProjectPersonRelationshipTypeName = relationshipType.ProjectPersonRelationshipTypeName;
            ProjectPersonRelationshipTypeDisplayName = relationshipType.ProjectPersonRelationshipTypeDisplayName;
            IsRequired = relationshipType.IsRequired;
        }

        public bool IsRequired { get; set; }
        public int ProjectPersonRelationshipTypeID { get; set; }
        public string ProjectPersonRelationshipTypeName { get; set; }
        public string ProjectPersonRelationshipTypeDisplayName { get; set; }
    }
}