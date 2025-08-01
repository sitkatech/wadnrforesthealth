﻿/*-----------------------------------------------------------------------
<copyright file="EditGrant.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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

namespace ProjectFirma.Web.Views.Grant
{
    public abstract class EditGrant : TypedWebPartialViewPage<EditGrantViewData, EditGrantViewModel>
    {
    }

    public abstract class EditGrantType
    {
        public readonly string IntroductoryText;

        internal EditGrantType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditGrantTypeNewGrant NewGrant = EditGrantTypeNewGrant.Instance;
        public static readonly EditGrantTypeExistingGrant ExistingGrant = EditGrantTypeExistingGrant.Instance;
    }

    public class EditGrantTypeNewGrant : EditGrantType
    {
        private EditGrantTypeNewGrant(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantTypeNewGrant Instance = new EditGrantTypeNewGrant(
            $"<p>Enter basic information about the {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditGrantTypeExistingGrant : EditGrantType
    {
        private EditGrantTypeExistingGrant(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantTypeExistingGrant Instance =
            new EditGrantTypeExistingGrant(
                $"<p>Update this {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}'s information.</p>");
    }

    
}
