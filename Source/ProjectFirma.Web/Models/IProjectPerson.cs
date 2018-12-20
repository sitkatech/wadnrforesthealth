namespace ProjectFirma.Web.Models
{
    public interface IProjectPerson
    {
        Person Person { get; set; }
        ProjectPersonRelationshipType ProjectPersonRelationshipType { get; }
    }
}