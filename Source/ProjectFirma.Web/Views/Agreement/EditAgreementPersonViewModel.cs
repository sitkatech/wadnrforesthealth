/*-----------------------------------------------------------------------
<copyright file="EditAgreementPersonViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using LtInfo.Common.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectFirma.Web.Views.Agreement
{
    public class EditAgreementPersonViewModel : FormViewModel
    {
        public int AgreementPersonID { get; set; }

        public int AgreementID { get; set; }

        [Required]
        [DisplayName("Agreement Role")]
        public int AgreementPersonRoleID { get; set; }

        [Required]
        [DisplayName("Contact")]
        public int PersonID { get; set; }



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementPersonViewModel()
        {
        }

        public EditAgreementPersonViewModel(int agreementId)
        {
            AgreementID = agreementId;
        }


        public EditAgreementPersonViewModel(Models.AgreementPerson agreementPerson)
        {
            AgreementPersonID = agreementPerson.AgreementPersonID;
            AgreementPersonRoleID = agreementPerson.AgreementPersonRoleID;
            PersonID = agreementPerson.PersonID;
            AgreementID = agreementPerson.AgreementID;
        }

        public void UpdateModel(Models.AgreementPerson agreementPerson)
        {
            agreementPerson.AgreementPersonID = AgreementPersonID;
            agreementPerson.PersonID = PersonID;
            agreementPerson.AgreementPersonRoleID = AgreementPersonRoleID;
            agreementPerson.AgreementID = AgreementID;
        }
    }
}