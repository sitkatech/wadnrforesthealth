using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewModel : FormViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int OrganizationID { get; set; }
        public string MiddleName { get; set; }
        public string StatewideVendorNumber { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
    }
}