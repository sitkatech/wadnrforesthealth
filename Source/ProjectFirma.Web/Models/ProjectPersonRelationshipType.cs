namespace ProjectFirma.Web.Models
{
    public partial class ProjectPersonRelationshipType
    {
        public bool IsViewableByUser(Person person)
        {
            if (!IsRestrictedToAdminAndProjectStewardAndCanViewLandownerInfo)
            {
                return true;
            }
            else if (person.HasRole(Role.Admin) || person.HasRole(Role.EsaAdmin) || person.HasRole(Role.ProjectSteward) || person.HasRole(Role.CanViewLandownerInfo))
            {
                return true;
            }

            return false;
        }
    }
}