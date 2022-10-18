using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System.Text.RegularExpressions;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Views.Vendor;

namespace ProjectFirma.Web.Views.User
{
    public class EditContactViewModel : FormViewModel, IValidatableObject, ISearchVendorViewModel
    {
        /// <summary>
        /// Only here so we have access to it in the Validator function. Not actually being edited. -- SLG
        /// </summary>
        [DisplayName("PersonID")]
        public int PersonID { get; set; }

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

        public string VendorDisplayName { get; set; }

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
            PersonID = person.PersonID;
            FirstName = person.FirstName;
            MiddleName = person.MiddleName;
            LastName = person.LastName;
            Email = person.Email;
            Address = person.PersonAddress;
            Phone = person.Phone;
            OrganizationID = person.OrganizationID;
            VendorID = person.VendorID;
            if (person.Vendor != null)
            {
                VendorDisplayName = person.Vendor.GetVendorNameWithFullStatewideVendorNumber();
            }
            Notes = person.Notes;
        }

        public void UpdateModel(Person person)
        {
            person.OrganizationID = OrganizationID;
            person.PersonAddress = Address;
            person.VendorID = VendorID;
            person.Phone = Phone;
            person.Notes = Notes;

            if (!person.IsFullUser()) // These fields come from SAW/ADFS and are disabled on the front-end when editing Legit Users
            {
                person.FirstName = FirstName;
                person.MiddleName = MiddleName;
                person.LastName = LastName;
                person.Email = Email;
            }

            SetAuthenticatorsForGivenEmailAddress(this, person);
        }

        public static void SetAuthenticatorsForGivenEmailAddress(EditContactViewModel viewModel, Person personBeingEdited)
        {
            if (!string.IsNullOrEmpty(viewModel.Email) && viewModel.Email != personBeingEdited.Email)
            {
                // Clear existing Authenticators (if any)
                personBeingEdited.PersonAllowedAuthenticators.Clear();
                Check.Ensure(!personBeingEdited.PersonAllowedAuthenticators.Any());

                // Set the allowed authenticators
                var authenticatorsToAllow = AuthenticatorHelper.GetAuthenticatorsForEmailAddress(viewModel.Email);
                var personAllowedAuthenticators = authenticatorsToAllow.Select(x => new PersonAllowedAuthenticator(personBeingEdited, x));
                HttpRequestStorage.DatabaseEntities.PersonAllowedAuthenticators.AddRange(personAllowedAuthenticators);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var editContactViewModel = (EditContactViewModel)validationContext.ObjectInstance;
            var thePersonBeingEdited = HttpRequestStorage.DatabaseEntities.People.SingleOrDefault(p => p.PersonID == editContactViewModel.PersonID);

            if (Phone != null)
            {
                Regex strip = new Regex(@"[()\-\s]"); // don't worry about whitespace characters or "phone-number" characters
                var phoneNumberToTest = strip.Replace(Phone, "");
                if (phoneNumberToTest.Length != 10 || //number of digits in an american phone number
                    !phoneNumberToTest.All(char.IsDigit)) // phone numbers must be digits
                {
                    yield return new SitkaValidationResult<EditContactViewModel, string>("Phone Number was invalid.", m => m.Phone);
                }
            }

            bool emailProvided = Email != null;
            if (emailProvided && !FirmaHelpers.IsValidEmail(Email))
            {
                yield return new SitkaValidationResult<EditContactViewModel, string>("Email Address was invalid.", m => m.Email);
            }

            // If Person record is being created, or email is being changed, make sure someone does not already have the given email address
            bool existingUserHasGivenEmailAddressAlready = HttpRequestStorage.DatabaseEntities.People.Any(p => p.Email == Email);
            bool personIsBeingCreated = thePersonBeingEdited == null;

            // Create case
            if (emailProvided && personIsBeingCreated && existingUserHasGivenEmailAddressAlready)
            {
                yield return new SitkaValidationResult<EditContactViewModel, string>($"Email Address \"{Email}\" already in use.", m => m.Email);
            }

            // Edit case
            if (!personIsBeingCreated)
            {
                bool personsEmailIsChanging = thePersonBeingEdited.Email != Email;
                if (emailProvided && personsEmailIsChanging && existingUserHasGivenEmailAddressAlready)
                {
                    yield return new SitkaValidationResult<EditContactViewModel, string>($"Email Address \"{Email}\" already in use.", m => m.Email);
                }
            }
        }
    }
}