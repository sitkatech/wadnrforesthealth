namespace ProjectFirma.Web.Models
{
    public class ProjectPersonRelationship
    {
        public Project Project { get; }
        public Person Person { get; }
        public ProjectPersonRelationshipType ProjectPersonRelationshipType { get; }
        public string DisplayCssClass { get; }


        public ProjectPersonRelationship(Project project, Person person, ProjectPersonRelationshipType relationshipType)
        {
            Project = project;
            Person = person;
            ProjectPersonRelationshipType = relationshipType;
        }

        public ProjectPersonRelationship(Project project, Person person, ProjectPersonRelationshipType relationshipType, string displayCssClass) : this(project, person, relationshipType)
        {
            DisplayCssClass = displayCssClass;
        }

        public ProjectPersonRelationship(ProjectPerson x) : this(x.Project, x.Person, x.ProjectPersonRelationshipType)
        {
        }
    }
}