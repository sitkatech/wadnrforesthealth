using System.Web;

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
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public ProjectPersonRelationshipTypeSimple(ProjectPersonRelationshipType relationshipType)
            : this()
        {
            ProjectPersonRelationshipTypeID = relationshipType.ProjectPersonRelationshipTypeID;
            ProjectPersonRelationshipTypeName = relationshipType.ProjectPersonRelationshipTypeName;
            ProjectPersonRelationshipTypeDisplayName = relationshipType.ProjectPersonRelationshipTypeDisplayName;
            IsRequired = relationshipType.IsRequired;
            ProjectPersonRelationshipTypeDescription = relationshipType.FieldDefinition.GetFieldDefinitionDescription().ToHtmlString().Replace("<p>", "").Replace("</p>","");
        }

        public bool IsRequired { get; set; }
        public int ProjectPersonRelationshipTypeID { get; set; }
        public string ProjectPersonRelationshipTypeName { get; set; }
        public string ProjectPersonRelationshipTypeDisplayName { get; set; }
        public string ProjectPersonRelationshipTypeDescription { get; set; }
    }
}