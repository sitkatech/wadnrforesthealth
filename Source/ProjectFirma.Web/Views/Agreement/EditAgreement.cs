/*-----------------------------------------------------------------------
<copyright file="EditProject.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.Shared.ProjectControls;

namespace ProjectFirma.Web.Views.Agreement
{
    public abstract class EditAgreement : TypedWebPartialViewPage<EditAgreementViewData, EditAgreementViewModel>
    {
    }

    public abstract class EditAgreementType
    {
        public readonly string IntroductoryText;

        internal EditAgreementType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditAgreementTypeNewAgreement NewAgreement = EditAgreementTypeNewAgreement.Instance;
        public static readonly EditAgreementTypeExistingAgreement ExistingAgreement = EditAgreementTypeExistingAgreement.Instance;
    }

    public class EditAgreementTypeNewAgreement : EditAgreementType
    {
        private EditAgreementTypeNewAgreement(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditAgreementTypeNewAgreement Instance = new EditAgreementTypeNewAgreement(
            $"<p>Enter basic information about the {Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditAgreementTypeExistingAgreement : EditAgreementType
    {
        private EditAgreementTypeExistingAgreement(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditAgreementTypeExistingAgreement Instance =
            new EditAgreementTypeExistingAgreement(
                $"<p>Update this {Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}'s information.</p>");
    }

    
}
