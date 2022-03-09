namespace ProjectFirma.Web.Models
{
    public class RoleSimple
    {
        public int RoleID { get; set; }

        public string RoleDisplayName { get; set; }


        public RoleSimple(Role role)
        {
            RoleID = role.RoleID;
            RoleDisplayName = role.RoleDisplayName;
        }

        public RoleSimple()
        {

        }
    }
}