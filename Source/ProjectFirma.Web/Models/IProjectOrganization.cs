
namespace ProjectFirma.Web.Models
{
    public interface IProjectOrganization
    {
        Organization Organization { get; set; }

        RelationshipType RelationshipType { get; set; }

    }

    public interface IProjectPerson
    {
        Person Person { get; set; }
        ProjectPersonRelationshipType ProjectPersonRelationshipType { get; }
    }
    
}