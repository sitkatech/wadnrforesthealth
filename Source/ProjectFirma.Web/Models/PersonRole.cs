namespace ProjectFirma.Web.Models
{
    public partial class PersonRole : IAuditableEntity
    {

        public string AuditDescriptionString => $"PersonRole(Person ID:{this.PersonID}, Role ID:{this.RoleID})";


    }
}