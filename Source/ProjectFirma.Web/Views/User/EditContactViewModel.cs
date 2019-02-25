using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System.Text.RegularExpressions;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewModel : FormViewModel, IValidatableObject
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
        [StringLength(Person.FieldLengths.PersonAddress)]
        public string Address { get; set; }

        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Organization")]
        public int? OrganizationID { get; set; }

        [DisplayName("Vendor")]
        public int? VendorID { get; set; }

        [DisplayName("Notes")]
        [StringLength(Person.FieldLengths.Notes)]
        public string Notes { get; set; }

        /// <summary>
        /// Needed by ModelBinder
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
            VendorID = person.VendorID;
            Notes = person.Notes;
        }

        public void UpdateModel(Person person)
        {
            person.OrganizationID = OrganizationID;
            person.PersonAddress = Address;
            person.VendorID = VendorID;
            person.Phone = Phone;
            person.Notes = Notes;

            if (string.IsNullOrWhiteSpace(person.PersonUniqueIdentifier)) // These fields come from SAW/ADFS and are disabled on the front-end when editing Legit Users
            {
                person.FirstName = FirstName;
                person.MiddleName = MiddleName;
                person.LastName = LastName;
                person.Email = Email;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Phone != null)
            {
                Regex strip = new Regex(@"[()\-\s]"); // don't worry about whitespace characters or "phone-number" characters
                var phoneNumberToTest = strip.Replace(Phone, "");
                if (phoneNumberToTest.Length != 10 || //number of digits in an american phone number
                    !phoneNumberToTest.All(char.IsDigit)) // phone numbers must be digits
                {
                    yield return new SitkaValidationResult<EditContactViewModel, string>("Phone Number was invalid.",
                        m => m.Phone);
                }
            }

            if (Email != null && !FirmaHelpers.IsValidEmail(Email))
            {
                yield return new SitkaValidationResult<EditContactViewModel, string>("Email Address was invalid.",
                    m => m.Email);
            }
        }
    }
}