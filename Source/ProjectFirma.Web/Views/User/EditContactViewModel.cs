using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewModel : FormViewModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name or Initial")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DisplayName("Mailing Address")]
        public string Address { get; set; }

        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Organization")]
        public int? OrganizationID { get; set; }

        [DisplayName("Statewide Vendor Number")]
        public string StatewideVendorNumber { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }
    }
}