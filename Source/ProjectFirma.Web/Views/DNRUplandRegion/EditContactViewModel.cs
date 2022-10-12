using System;
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

namespace ProjectFirma.Web.Views.DNRUplandRegion
{
    public class EditContactViewModel : FormViewModel, IValidatableObject
    {
        /// <summary>
        /// Only here so we have access to it in the Validator function. Not actually being edited. -- SLG
        /// </summary>
        [DisplayName("RegionID")]
        public int RegionID { get; set; }

        [DisplayName("Mailing Address")]
        [StringLength(Models.DNRUplandRegion.FieldLengths.RegionAddress)]
        public string Address { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Zip")]
        public string Zip { get; set; }

        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        ///     </summary>
        public EditContactViewModel()
        {

        }

        public EditContactViewModel(Models.DNRUplandRegion region)
        {
            RegionID = region.DNRUplandRegionID;
            State = region.RegionState;
            City = region.RegionCity;
            Zip = region.RegionZip;
            Email = region.RegionEmail;
            Address = region.RegionAddress;
            Phone = region.RegionPhone;
           
        }

        public void UpdateModel(Models.DNRUplandRegion region)
        {
            region.RegionState = State;
            region.RegionCity = City;
            region.RegionZip = Zip;
            region.RegionEmail = Email;
            region.RegionAddress = Address;
            region.RegionPhone = Phone;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var editContactViewModel = (EditContactViewModel)validationContext.ObjectInstance;

            if (Phone != null)
            {
                Regex strip = new Regex(@"[()\-\s]"); // don't worry about whitespace characters or "phone-number" characters
                var phoneNumberToTest = strip.Replace(Phone, "");
                if (phoneNumberToTest.Length != 10 || //number of digits in an american phone number
                    !phoneNumberToTest.All(char.IsDigit)) // phone numbers must be digits
                {
                    yield return new SitkaValidationResult<EditContactViewModel, string>("Phone Number is invalid.", m => m.Phone);
                }
            }

            bool emailProvided = Email != null;
            if (emailProvided && !FirmaHelpers.IsValidEmail(Email))
            {
                yield return new SitkaValidationResult<EditContactViewModel, string>("Email Address is invalid.", m => m.Email);
            }
        }
    }
}