using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateProjectContactModel : ReportTemplateBaseModel
    {
        private ProjectPerson ProjectPerson { get; set; }
        private Organization Organization { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactType { get; set; }

        public ReportTemplateProjectContactModel(ProjectPerson projectPerson)
        {
            // Private properties
            ProjectPerson = projectPerson;
            Organization = ProjectPerson.Person.Organization;

            // Public properties
            FullName = ProjectPerson.Person.FullNameFirstLast;
            FirstName = ProjectPerson.Person.FirstName;
            LastName = ProjectPerson.Person.LastName;
            Email = ProjectPerson.Person.Email;
            Phone = ProjectPerson.Person.Phone;
            ContactType = ProjectPerson.ProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName;
        }

        public ReportTemplateOrganizationModel GetOrganization()
        {
            return new ReportTemplateOrganizationModel(Organization);
        }

    }
}