namespace ProjectFirma.Web.Models
{
    public partial class ProjectPersonRelationshipType
    {
        public bool IsViewableByUser(Person person)
        {
            if (!IsRestrictedToAdminAndProjectSteward)
            {
                return true;
            }
            else if (person.Role == Role.Admin || person.Role == Role.SitkaAdmin || person.Role == Role.ProjectSteward)
            {
                return true;
            }

            return false;
        }
    }
}