namespace ProjectFirma.Web.Models
{
    public partial class ProjectPerson : IProjectPerson
    {
        public ProjectPerson(IProjectPerson projectPerson)
        {
            Person = projectPerson.Person;
            ProjectPersonRelationshipTypeID = projectPerson.ProjectPersonRelationshipType.ProjectPersonRelationshipTypeID;
        }

        public ProjectPerson(Person organization, ProjectPersonRelationshipType projectPersonRelationshipType, string displayCssClass)
        {
            Person = organization;
            ProjectPersonRelationshipTypeID = projectPersonRelationshipType.ProjectPersonRelationshipTypeID;
            DisplayCssClass = displayCssClass;
        }

        public string DisplayCssClass { get; set; }
    }
}