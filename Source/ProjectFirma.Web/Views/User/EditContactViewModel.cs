using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewModel : FormViewModel, IValidatableObject
    {
        /// <summary>
        /// Needed by ModdelBinder
        ///     </summary>
        public EditContactViewModel()
        {

        }

        public EditContactViewModel(Person person)
        {
            FirstName = person.FirstName;
            MiddleName = person.MiddleName;
            LastName = person.LastName;
            Email = person.Email;
            Address = person.PersonAddress;
            Phone = person.Phone;
            OrganizationID = person.OrganizationID;
            StatewideVendorNumber = person.StatewideVendorNumber;
            Notes = person.Notes;
        }

        public void UpdateModel(Person person)
        {
            person.FirstName = FirstName;
            person.MiddleName = MiddleName;
            person.LastName = LastName;
            person.Email = Email;
            person.PersonAddress = Address;
            person.Phone = Phone;
            person.OrganizationID = OrganizationID;
            person.StatewideVendorNumber = StatewideVendorNumber;
            person.Notes = Notes;
        }

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
        [StringLength(Person.FieldLengths.PersonAddress)]
        public string Address { get; set; }

        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Organization")]
        public int? OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.StatewideVendorNumber)]
        public string StatewideVendorNumber { get; set; }

        [DisplayName("Notes")]
        [StringLength(Person.FieldLengths.Notes)]
        public string Notes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Phone.Length != 10 || //number of digits in an american phone number
                !Phone.All(char.IsDigit)) // phone numbers must be digits
            {
                yield return new SitkaValidationResult<EditContactViewModel, string>("Phone number was invalid.", m=>m.Phone);
            }
        }
    }
}