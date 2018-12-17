namespace ProjectFirma.Web.Models
{
    public class ProjectPersonRelationship
    {
        public Project Project { get; }
        public Person Person { get; }
        public RelationshipType RelationshipType { get; }
        public string DisplayCssClass { get; }


        public ProjectPersonRelationship(Project project, Person organization, RelationshipType relationshipType)
        {
            Project = project;
            Person = organization;
            RelationshipType = relationshipType;
        }

        public ProjectPersonRelationship(Project project, Person organization, RelationshipType relationshipType, string displayCssClass) : this(project, organization, relationshipType)
        {
            DisplayCssClass = displayCssClass;
        }
    }
}